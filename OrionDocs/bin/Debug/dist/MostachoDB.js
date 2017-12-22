﻿
var MostachoDB = function(idbName, idbVersion) {
	if(!idbName)
		return undefined;
	this.idbName = idbName;
	this.idbVersion = idbVersion;
	this.idbConnection = undefined;
	this.tableConfig = [];
	this.tables = [];
	this.fakeIDs = [];
	this.triggers = {};
}


MostachoDB.prototype.preparetable = function(tName, primaryKey) {
	this.tableConfig[tName] = primaryKey;
}


MostachoDB.prototype.connect = function(onupgrade, onready) {
	if(this.idbName != undefined) {
		var request = (this.idbVersion) ? window.indexedDB.open(this.idbName, this.idbVersion) : window.indexedDB.open(this.idbName);
		var instance = this;
		request.onupgradeneeded = function(e) {
			instance.idbConnection = request.result;
			for(var table in instance.tableConfig) {
				var skeyPath = instance.tableConfig[table] || table + 'id';
				if(!instance.idbConnection.objectStoreNames.contains(table))
					instance.idbConnection.createObjectStore(table, { keyPath: skeyPath, autoIncrement: true });
			}
			if(!instance.idbConnection.objectStoreNames.contains('_sys_trigger'))
				instance.idbConnection.createObjectStore('_sys_trigger', { keyPath: 'tid', autoIncrement: true });
			
			if(onupgrade)
				onupgrade(instance);
		}
		
		request.onsuccess = function(e) {
			instance.idbConnection = request.result;
			instance.sync(onready);
		}
	} else {
		console.error('MostachoDB Error: Nombre de base de datos no valido');
		return false;
	}
}

		
MostachoDB.prototype.sync = function(onready) {
	var tables = this.idbConnection.objectStoreNames;
	var counter = 0;
	
	for(var i = 0; i < tables.length; i++) {
		(function(instance, table) {
			var trans = instance.idbConnection.transaction([table], 'readonly').objectStore(table);
			instance.tables[table] = [];
			instance.fakeIDs[table] = 0;
			instance.tableConfig[table] = trans.keyPath;
			trans.openCursor().onsuccess = function(e) {
				var cursor = e.target.result;
				if(cursor) {
					instance.tables[table].push(cursor.value);
					if(instance.fakeIDs[table] < cursor.key)
						instance.fakeIDs[table] = cursor.key;
					cursor.continue();
				} else {
					counter++;
					if(counter == instance.idbConnection.objectStoreNames.length && onready)
						onready(instance);
				}
			}
		})(this, tables[i]);
	}
}


MostachoDB.prototype.close = function() {
	if(this.isConnected()) {
		this.idbConnection.close();
		this.idbConnection = undefined;
		this.tableConfig = [];
		this.tables = [];
		this.fakeIDs = [];
		this.triggers = {};
		return true;
	}
	return false;
}


MostachoDB.prototype.dropdb = function() {
	if(this.isConnected()) {
		this.close();
		var ondelete = window.indexedDB.deleteDatabase(this.idbName);
		var that = this;
		ondelete.onsuccess = function(e) {
			console.info('MostachoDB Info: Base de datos "' + that.idbName + '" eliminada correctamente.');
			that.idbName = undefined;
			that.idbVersion = undefined;
			that.idbConnection = undefined;
			that.tableConfig = [];
			that.tables = [];
			that.fakeIDs = [];
			that.triggers = {};
		}
		
		ondelete.onerror = function(e) {
			console.error('MostachoDB Error: No se pudo eliminar la base de datos. Asegúrese que no está en uso');
		}
		return true;
	}
	console.error('MostachoDB Error: No hay ninguna conexión activa a una base de datos');
	return false;
}


MostachoDB.prototype.isConnected = function() {
	return !(this.idbConnection == undefined);
}


MostachoDB.prototype.droptable = function(tName) {
	if(this.isConnected()) {
		if(this.tableexists(tName)) {
			this.idbConnection.deleteObjectStore(tName);
			console.info('MostachoDB Info: La tabla ' + tName + ' ha sido eliminada correctamente');
			return true;
		}
		console.error('MostachoDB Error: La tabla no existe');
		return false;
	}
	console.error('MostachoDB Error: No hay ninguna conexión activa');
	return false;
}


MostachoDB.prototype.showtables = function() {
	if(this.isConnected()) {
		var tables = this.idbConnection.objectStoreNames;
		var retTables = [];
		for(var table in tables) {
			var table = parseInt(table);
			if(!isNaN(table))
				retTables.push(tables[table]);
		}
		return retTables;
	}
	console.error('MostachoDB Error: No hay ninguna conexion activa');
	return false;
}


MostachoDB.prototype.tableexists = function(tName) {
	if(this.isConnected())
		return this.idbConnection.objectStoreNames.contains(tName);
	
	console.error('MostachoDB Error: No hay ninguna conexion activa');
	return false;
}


MostachoDB.prototype.with = function(tName) {
	return new (function(instance, tName) {
		this.tName = tName;
		this.data = instance.tables[tName] || [];
		
		
		function getTransaction(mode) {
			try {
				return instance.idbConnection.transaction([tName], mode).objectStore(tName);
			} catch(e) {
				if(instance.isConnected()) {
					if(tName == '_sys_trigger')
						console.error('MostachoDB Error: No ha permitido el uso de triggers en su base de datos');
				} else
					console.error('MostachoDB Error: No hay ninguna conexion activa');
				return false;
			}
		};
		
		//Devuelve un objeto nuevo quitando aquellos campos no especificados en "fields". Asigna aliases si los hubiere
		function getSpecificFields(o, fields) {
			var tmpo = {};
			var splitted = fields.split(',');
			for(var key in o) {
				var keyStartsAt = fields.indexOf(key);
				if(o.hasOwnProperty(key) && (keyStartsAt > -1 || fields.indexOf('*') == 0)) {
					var keyEndsAt = fields.indexOf(',', keyStartsAt) + 1;
					if(keyEndsAt == 0)
						keyEndsAt = fields.length;
					else
						keyEndsAt -= 1;
					var alias = fields.substring(keyStartsAt, keyEndsAt).split(' ')[1] || undefined;
					if(alias && fields.indexOf('*') == -1)
						tmpo[alias] = o[key];
					else
						tmpo[key] = o[key];
				}
			}
			
			for(var i = 0; i < splitted.length; i++) {
				if(splitted[i].trim().indexOf('"') == 0 || splitted[i].trim().indexOf("'") == 0) {
					var data = splitted[i].trim().split(' ');
					tmpo[data[1]] = data[0].replace(/'|"/g, '');
				}
			}
			return tmpo;
		};
		
		//Devuelve un objeto con todos sus campos excepto aquellos especificados en "exceptions"
		function getFieldsExcept(o, exceptions) {
			var tmpo = {};
			for(var key in o) {
				if(o.hasOwnProperty(key) && exceptions.indexOf(key) == -1)
					tmpo[key] = o[key];
			}
			return tmpo;
		}
		
		//Elimina las funciones de un objeto no especificadas en "args"
		function deleteFunctionsExcept(o, args) {
			for(var key in o) {
				if(args.indexOf(key) == -1 && typeof o[key] == 'function')
					delete o[key];
			}
		};
		
		//Clona un objeto
		function cloneObject(o) {
			var tmpo = {};
			for(var key in o) {
				if(o.hasOwnProperty(key))
					tmpo[key] = o[key];
			}
			return tmpo;
		}
		
		//Elimina propiedades innecesarias de un conjunto de objetos
		function cleanObjects(recordset, fields) {
			var retRecordset = [];
			for(var i = 0; i < recordset.length; i++)
				retRecordset.push(getSpecificFields(recordset[i], fields));
			return retRecordset;
		};
		
		//Verifica si un objeto existe en un arreglo
		function objectExistsIn(arr, o) {
			var exists = true;
			for(var i = 0; i < arr.length; i++) {
				for(var key in o) {
					if(arr[i].hasOwnProperty(key) && arr[i][key] == o[key])
						exists = true;
					else {
						exists = false;
						break;
					}
				}
				
				if(exists)
					return true;
			}
			return false;
		};
		
		//Conservamos una referencia al objectStore
		var that = this;
		
				
		this.trigger = function(f) {
			return new (function(f) {
				this.oninsert = function() { instance.with('_sys_trigger').insert({table: tName, f: f.toString(), event: 'oninsert'}); };
				this.onupdate = function() { instance.with('_sys_trigger').insert({table: tName, f: f.toString(), event: 'onupdate'}); };
				this.ondelete = function() { instance.with('_sys_trigger').insert({table: tName, f: f.toString(), event: 'ondelete'}); };
			})(f)
		};
	
		
		this.truncate = function() {
			instance.tables[this.tName] = this.data = [];
			var trans = getTransaction('readwrite');
			trans.oncomplete = function() {
				console.log("MostachoDB Info: Datos de " + that.tName + " eliminados correctamente");
			}
			trans.clear();
		};
		
		
		this.insert = function(o) {
			var trans = getTransaction('readwrite');
			if(trans !== false) {
				var key = trans.keyPath;
				if(!o.hasOwnProperty(key)) {
					var newFakeID = parseInt(++instance.fakeIDs[this.tName])
					o[key] = newFakeID;
				}
				else {
					if(typeof o[key] == 'number' && o[key] > instance.fakeIDs[this.tName])
						instance.fakeIDs[this.tName] = o[key];
				}
				
				try {
						var triggerOnInsert = instance.with('_sys_trigger').select('f').where('event = oninsert').and('table = ' + tName).orderby('tid').desc()[0];
						if(triggerOnInsert)
							eval('(' + triggerOnInsert.f + ')')(o);
				} catch(e) {
					console.error('MostachoDB: Ocurrio un error al insertar el objeto');
				}
				
				trans.put(o);
				this.data.push(o);
				return true;
			}
			console.error('MostachoDB: Ocurrio un error al insertar el objeto');
			return false;
		};
		
		
		this.select = function(fields) {
			if(fields != '') {
				var ret = instance.with('TMPTABLE');
				
				var tmpo = {};
				for(var i = 0; i < this.data.length; i++) {
					tmpo = getSpecificFields(this.data[i], fields);
					if(Object.keys(tmpo).length > 0)
						ret.data.push(tmpo);
				}
				
				ret.where = function(args) { return new whereClause(that.data, args, fields); };
				ret.orderby = function(field) { return new orderByClause(that.data, fields, field); };
				ret.unionall = function(recordsetB) { return unionallClause(ret.data, recordsetB, fields); };
				ret.union = function(recordsetB) { return unionClause(ret.data, recordsetB, fields); };
				ret.innerjoin = function(secondStorage, join) { return joinClause(that, instance.with(secondStorage), join, fields, 'innerjoin') };
				ret.leftjoin = function(secondStorage, join) { return joinClause(that, instance.with(secondStorage), join, fields, 'leftjoin') };
				ret.rightjoin = function(secondStorage, join) { return joinClause(that, instance.with(secondStorage), join, fields, 'rightjoin') };
				ret.fulljoin = function(secondStorage, join) { return joinClause(that, instance.with(secondStorage), join, fields, 'fulljoin') };
				deleteFunctionsExcept(ret, 'where, orderby, union, unionall, innerjoin, leftjoin, rightjoin, fulljoin');
				return ret;
			}
			
			return null;
		};
		
		
		function joinClause(firstStorage, secondStorage, join, fields, joinType) {
			var joinDataFirst = [];
			var joinDataSecond = [];
			var fData = firstStorage.data;
			var sData = secondStorage.data;
			var tmpo = {};
			var ret = instance.with('TMPTABLE');
			var removeFields = '';
			
			var ijData = instance.with('TMPTABLE');
			deleteFunctionsExcept(ijData, '');
			
			//Agregar prefix de la tabla al primer almacén
			for(var i = 0; i < fData.length; i++) {
				removeFields = '';
				tmpo = cloneObject(fData[i]);
				for(var key in fData[i]) {
					if(fData[i].hasOwnProperty(key) && firstStorage.tName != 'TMPTABLE') {
						tmpo[firstStorage.tName + '.' + key] = fData[i][key];
						removeFields += key + ',';
					}
				}
				joinDataFirst.push(getFieldsExcept(tmpo, removeFields));
			}
			
			//Agregar prefix de la tabla al segundo almacén
			for(var i = 0; i < sData.length; i++) {
				removeFields = '';
				tmpo = cloneObject(sData[i]);
				for(var key in sData[i]) {
					if(sData[i].hasOwnProperty(key) && secondStorage.tName != 'TMPTABLE') {
						tmpo[secondStorage.tName + '.' + key] = sData[i][key];
						removeFields += key + ',';
					}
				}
				joinDataSecond.push(getFieldsExcept(tmpo, removeFields));
			}
			
			switch(joinType) {
				case 'innerjoin':
					for(var i = 0; i < joinDataFirst.length; i++) {			
						for(var j = 0; j < joinDataSecond.length; j++) {
							if(joinDataFirst[i][join.on] == joinDataSecond[j][join.equals] && joinDataFirst[i][join.on] != undefined) {
								tmpo = {};
								for(var key in joinDataFirst[i]) {
									if(joinDataFirst[i].hasOwnProperty(key))
										tmpo[key] = joinDataFirst[i][key];
								}
								
								for(var key in joinDataSecond[j]) {
									if(joinDataSecond[j].hasOwnProperty(key))
										tmpo[key] = joinDataSecond[j][key];
								}
								
								ijData.data.push(tmpo);
							
								tmpo = getSpecificFields(tmpo, fields);
								if(Object.keys(tmpo).length > 0)
									ret.data.push(tmpo);
								break;
							}
						}
					}
					break;
				case 'fulljoin':
					ret.data = firstStorage.select(fields).leftjoin(secondStorage.tName, join).union(firstStorage.select(fields).rightjoin(secondStorage.tName, join)).data;
					break;
				case 'leftjoin':
					var found = false;
					for(var i = 0; i < joinDataFirst.length; i++) {
						tmpo = {};
						found = false;
						for(var key in joinDataFirst[i]) {
							if(joinDataFirst[i].hasOwnProperty(key))
								tmpo[key] = joinDataFirst[i][key];
						}
						
						for(var j = 0; j < joinDataSecond.length; j++) {
							if(joinDataFirst[i][join.on] == joinDataSecond[j][join.equals] && joinDataFirst[i][join.on] != undefined) {
								for(var key in joinDataSecond[j]) {
									if(joinDataSecond[j].hasOwnProperty(key))
										tmpo[key] = joinDataSecond[j][key];
								}
								found = true;
								break;
							}
						}
						
						if(!found) {
							for(var key in joinDataSecond[0]) {
								if(joinDataSecond[0].hasOwnProperty(key))
									tmpo[key] = 'NULL';
							}
						}
						
						ijData.data.push(tmpo);
						
						tmpo = getSpecificFields(tmpo, fields);
						if(Object.keys(tmpo).length > 0)
							ret.data.push(tmpo);
					}
					break;
				case 'rightjoin':
					var found = false;
					for(var i = 0; i < joinDataSecond.length; i++) {
						tmpo = {};
						found = false;
						for(var key in joinDataSecond[i]) {
							if(joinDataSecond[i].hasOwnProperty(key))
								tmpo[key] = joinDataSecond[i][key];
						}
						
						for(var j = 0; j < joinDataFirst.length; j++) {
							if(joinDataSecond[i][join.equals] == joinDataFirst[j][join.on] && joinDataSecond[i][join.equals] != undefined) {
								for(var key in joinDataFirst[j]) {
									if(joinDataFirst[j].hasOwnProperty(key))
										tmpo[key] = joinDataFirst[j][key];
								}
								found = true;
								break;
							}
						}
						
						if(!found) {
							for(var key in joinDataFirst[0]) {
								if(joinDataFirst[0].hasOwnProperty(key))
									tmpo[key] = 'NULL';
							}
						}
						
						ijData.data.push(tmpo);
						
						tmpo = getSpecificFields(tmpo, fields);
						if(Object.keys(tmpo).length > 0)
							ret.data.push(tmpo);
					}
					break;
			}
			
			ret.where = function(args) { return new whereClause(ret.data, args, fields); };
			ret.orderby = function(field) { return new orderByClause(ret.data, fields, field); };
			ret.unionall = function(recordsetB) { return unionallClause(ret.data, recordsetB, fields); };
			ret.union = function(recordsetB) { return unionClause(ret.data, recordsetB, fields); };
			if(joinType != 'fulljoin') {
				ret.innerjoin = function(secondStorage, join) { return joinClause(ijData, instance.with(secondStorage), join, fields, 'innerjoin') };
				ret.leftjoin = function(secondStorage, join) { return joinClause(ijData, instance.with(secondStorage), join, fields, 'leftjoin') };
				ret.rightjoin = function(secondStorage, join) { return joinClause(ijData, instance.with(secondStorage), join, fields, 'rightjoin') };
				ret.fulljoin = function(secondStorage, join) { return joinClause(ijData, instance.with(secondStorage), join, fields, 'fulljoin') };
				deleteFunctionsExcept(ret, 'where, orderby, union, unionall, innerjoin, leftjoin, rightjoin, fulljoin');
			} else
				deleteFunctionsExcept(ret, 'where, orderby, union, unionall');
			return ret;
		}
		
		
		function whereClause(recordset, args, fields) {
			var param = args.split(' ');
			if(param.length == 3) {
				for(var i = 0; i < param.length; i++)
					param[i] = param[i].trim();
				
				var ret = instance.with('TMPTABLE');
				var whereData = [];
				
				switch(param[1]) {
					case '>':
						for(var i = 0; i < recordset.length; i++) {
							if(recordset[i][param[0]] > param[2])
								pushData(recordset[i]);
						}
					break;
					case '<':
						for(var i = 0; i < recordset.length; i++) {
							if(recordset[i][param[0]] < param[2])
								pushData(recordset[i]);
						}
					break;
					case '>=':
						for(var i = 0; i < recordset.length; i++) {
							if(recordset[i][param[0]] >= param[2])
								pushData(recordset[i]);
						}
					break;
					case '<=':
						for(var i = 0; i < recordset.length; i++) {
							if(recordset[i][param[0]] <= param[2])
								pushData(recordset[i]);
						}
					break;
					case '=':
						for(var i = 0; i < recordset.length; i++) {
							if(recordset[i][param[0]] == param[2])
								pushData(recordset[i]);
						}
					break;
					case 'like':
						for(var i = 0; i < recordset.length; i++) {
							var cleanParam = '' + param[2].replace(/%/g, '');
							if(param[2].substr(-1) == '%' && param[2].charAt(0) == '%') {
								if(recordset[i][param[0]].indexOf(cleanParam) > -1)
									pushData(recordset[i]);
							} else if(param[2].substr(-1) == '%') {
								if(recordset[i][param[0]].indexOf(cleanParam) == 0)
									pushData(recordset[i]);
							} else if(param[2].charAt(0) == '%') {
								if(recordset[i][param[0]].substr(-cleanParam.length) == cleanParam)
									pushData(recordset[i]);
							}
						}
					break;
					case '<>':
						for(var i = 0; i < recordset.length; i++) {
							if(recordset[i][param[0]] != param[2])
								pushData(recordset[i]);
						}
					break;
				}
				
				function pushData(object) {
					var tmpo = getSpecificFields(object, fields);
					whereData.push(object);
					if(Object.keys(tmpo).length > 0)
						ret.data.push(tmpo);
				}
				
				ret.orderby = function(field) { return new orderByClause(whereData, fields, field); }
				ret.and = function(args) { return new whereClause(whereData, args, fields); };
				ret.unionall = function(recordsetB) { return unionallClause(whereData, recordsetB, fields); };
				ret.union = function(recordsetB) { return unionClause(whereData, recordsetB, fields); };
				ret.delete = function() {
					var trans = getTransaction('readwrite');
					var keyPath = instance.tableConfig[tName];
					for(var i = 0; i < whereData.length; i++) {
						var index = getIndex(whereData[i][keyPath], keyPath, that.data);
						try {
							var triggerOnDelete = instance.with('_sys_trigger').select('f').where('event = ondelete').and('table = ' + tName).orderby('tid').desc()[0];
							if(triggerOnDelete)
								eval('(' + triggerOnDelete.f + ')')(whereData[i]);
							that.data.splice(index, 1);
							trans.delete(whereData[i][keyPath]);
							console.log('MostachoDB: Eliminacion correcta');
						} catch(e) {
							console.error('MostachoDB: Ocurrio un error al eliminar el objeto');
						}
					}
				};
				ret.update = function(o) {
					var trans = getTransaction('readwrite');
					for(var i = 0; i < whereData.length; i++) {
						var tmpo = whereData[i];
						var oldo = cloneObject(tmpo);
						for(var j = 0; j < o.fields.length; j++)
							tmpo[o.fields[j]] = o.values[j];
						try {
							var triggerOnUpdate = instance.with('_sys_trigger').select('f').where('event = onupdate').and('table = ' + tName).orderby('tid').desc()[0];
							if(triggerOnUpdate)
								eval('(' + triggerOnUpdate.f + ')')(oldo, tmpo);							
							trans.put(tmpo);
							console.log('MostachoDB: Actualizacion correcta');
						} catch(e) {
							console.error('MostachoDB: Ocurrio un error al actualizar el objeto');
						}
					}
				};
				
				deleteFunctionsExcept(ret, 'orderby, and, delete, update, union, unionall');
				return ret;
				
				function getIndex(key, keyPath, data) {
					for(var i = 0; i < data.length; i++) {
						if(data[i][keyPath] == key)
							return i;
					}
					return false;
				};
			}
			return null;
		};
		
		
		function orderByClause(recordset, fields, field) {
			

			this.asc = function(nocleaning) {
				recordset.sort(function(a, b) {
					if(a[field] > b[field])
						return 1;
					
					if(a[field] < b[field])
						return -1;
					return 0;
				});
				return (nocleaning) ? recordset : cleanObjects(recordset, fields);
			};
			
			
			this.desc = function(nocleaning) {
				recordset.sort(function(a, b) {
					if(a[field] > b[field])
						return -1;
					
					if(a[field] < b[field])
						return 1;
					return 0;
				});
				return (nocleaning) ? recordset : cleanObjects(recordset, fields);
			};
		};
		
				
		function unionClause(recordsetA, recordsetB, fields) {
			var ret = unionallClause(recordsetA, recordsetB, fields);
			var unique = [];
			for(var i = 0; i < ret.data.length; i++) {
				if(!objectExistsIn(unique, ret.data[i]))
					unique.push(ret.data[i]);
			}
			
			ret.data = unique;
			
			//Agregamos orderby
			ret.orderby = function(field) { return new orderByClause(ret.data, fields, field); };
			ret.where = function(args) { return new whereClause(ret.data, args, fields); };
			
			deleteFunctionsExcept(ret, 'orderby, where');
			
			return ret;
		}
		
		
		function unionallClause(recordsetA, recordsetB, fields) {
			try {
				var recordsetA = (Array.isArray(recordsetA)) ? recordsetA : recordsetA.data;
				var recordsetB = (Array.isArray(recordsetB)) ? recordsetB : recordsetB.data;
				var dataTypesA = [];
				var dataTypesB = [];
				
				//Creación del arreglo de tipo de datos del recordsetA
				var dtObject = recordsetA[0];
				for(var key in dtObject) {
					if(dtObject.hasOwnProperty(key))
						dataTypesA.push(typeof dtObject[key]);	
				}
				
				//Creación del arreglo de tipo de datos del recordsetB
				var dtObject = recordsetB[0];
				for(var key in dtObject) {
					if(dtObject.hasOwnProperty(key))
						dataTypesB.push(typeof dtObject[key]);	
				}
				
				//Comparación de tipos de datos del primero con el segundo select
				//Para realizar "union" deben existir la misma cantidad de campos y mismos tipos de datos en ambos arreglos
				if(dataTypesA.length == dataTypesB.length || (dataTypesA.length == 0 || dataTypesB.length == 0) ) {
					for(var i = 0; i < dataTypesA.length; i++) {
						if(dataTypesA[i] != dataTypesB[i]) {
							console.error("MostachoDB: Los datos no se corresponden en tipo");
							return false;
						}
					}
					
					//Si la función de union se sigue ejecutando después de la comprobación
					//de tipos de datos y cantidad de campos, entonces todo está bien
					var ret = instance.with('TMPTABLE'); //creamos un objeto de retorno que contenga los datos de union
					for(var i = 0; i < recordsetA.length; i++)
						ret.data.push(recordsetA[i]);
					
					for(var i = 0; i < recordsetB.length; i++)
						ret.data.push(recordsetB[i]);
					
					//Agregamos orderby y where
					ret.orderby = function(field) { return new orderByClause(ret.data, fields, field); };
					ret.where = function(args) { return new whereClause(ret.data, args, fields); };
					
					deleteFunctionsExcept(ret, 'orderby, where');
					
					return ret;
				} else {
					console.error("MostachoDB: Los tipos de datos no se corresponden en numero");
					return false;
				}
			}  catch(e) {
				console.error('MostachoDB: Parametros incorrectos en Union');
				return false;
			}
		};
	})(this, tName);
}