document.addEventListener('DOMContentLoaded', function(e) {
	var isCollapsed = 1;
	if(isMobile())
		isCollapsed = 0;
	
	if(!readCookie('iscollapsed'))
		document.cookie = 'iscollapsed=' + isCollapsed + '; max-age=3600';
	var isEmptyElements = ['description', 'param', 'prop', 'return', 'notice', 'warning', 'examples', 'tutorials', 'sourcecode', 'changelog'];
	for(var i = 0; i < isEmptyElements.length; i++) {
		var e = document.querySelector('[data-parent="' + isEmptyElements[i] + '"]');
		if(e) {
			if(e.innerHTML.trim().length == 0) {
				var parent = document.getElementById(isEmptyElements[i]);
				parent.setAttribute('style', 'display: none;');
			}
		}
	}
	
	var addedElements = 0;
	for(var i = 0; i < navBar.length; i++) {
		var span = document.createElement('span');
		span.classList.add('navBar-element');
		if(navBar[i].length > 0) {
			span.innerText = navBar[i];
			document.querySelector('.nav-bar').appendChild(span);
			addedElements++;
			if(i + 2 < navBar.length) {
				var fa = document.createElement('i');
				fa.setAttribute('class', 'fa fa-ellipsis-v fa-icon-16 fa-margin-sides10');
				document.querySelector('.nav-bar').appendChild(fa);
			}
		}
	}
	
	var s = document.querySelectorAll("i.show");
	for(var i = 0; i < s.length; i++) {
		(function(e) {
			e.addEventListener("click", function() {
				var c = document.querySelectorAll("[data-parent='" + e.getAttribute("data-child") + "']");
				var isShowing = false;
				for(var k = 0; k < c.length; k++) {
					if(!c[k].classList.contains("show-div")) {
						c[k].classList.add("show-div");
						isShowing = true;
					} else
						c[k].classList.remove("show-div");
				}
				if(isShowing && !this.classList.contains("fa-bars"))
					this.classList.add("showing");
				else
					this.classList.remove("showing");
			});
		})(s[i]);
	}
	
	if(addedElements == 0)
		document.querySelector('.nav-bar').setAttribute('style', 'display:none');

	menuMaker(menuItems, readCookie('iscollapsed'));
	var search = document.querySelector('#search');
	search.addEventListener('keyup', function() {
		var nav = document.querySelector('#nav-menu');
		nav.innerHTML = '';
		if(this.value.length >= 3) {
			var newMenu = new Array();
			for(var i = 0; i < menuItems.length; i++) {
				if(menuItems[i].name.toLowerCase().indexOf(this.value.toLowerCase()) > -1)
					newMenu.push(menuItems[i]);
			}
			
			var menuCollapsed = readCookie('iscollapsed');
			if(newMenu.length == 0) {
				newMenu.push({name : 'Sin resultados', url: '#'});
				menuCollapsed = false;
			}

			menuMaker(newMenu, menuCollapsed);
		} else
			menuMaker(menuItems, readCookie('iscollapsed'));
	});
	
	var e = document.querySelector('span[data-current]');
	var tree = e.getAttribute("data-current");
	var selectedMethod = document.querySelector('li[data-method="' +  tree + '"]');
	selectedMethod.classList.add('fancy-selection');
	collapseParentOf(selectedMethod);
});

function collapseParentOf(child) {
	if(child.parentNode) {
		if(child.parentNode.getAttribute("id") != "nav-menu")
			collapseParentOf(child.parentNode)
	}
	
	if(child.nodeName.toLowerCase() == "li")
		child.classList.add("fancy-menu-collapsed");
}

function isMobile() {
	if(window.innerWidth <= 800)
		return true;
	return false;
}

function goto(url) {
	for(var i = 0; i < menuItems.length; i++) {
		if(menuItems[i].url.indexOf(url) > -1)
			location.href = menuItems[i].url;
	}
}

function menuMaker(items, isMenuCollapsed) {
	var nav = document.querySelector('#nav-menu');
	var ul = document.createElement('ul');
	ul.classList.add('fancy-menu');
	ul.setAttribute('data-menu', 'global');
	
	for(var i = 0; i < items.length; i++) {
		var menuData = items[i].name.split('.');
		var menuUrl = items[i].url;
		var li = document.createElement('li');
		var j = 0;
		var lastUl = ul;
		
		if(menuData.length > 1) {
			for(j; j < menuData.length - 1; j++) {
				var e = document.querySelector('ul[data-menu="' + menuData[j] + '"]');
				if(!e) {
					var classLi = document.createElement('li');
					var spanLi = document.createElement('span');
					
					classLi.classList.add('fancy-menu-list');
					if(isMenuCollapsed || isMenuCollapsed == "1")
						classLi.classList.add('fancy-menu-collapsed');

					spanLi.innerText = menuData[j];
					classLi.appendChild(spanLi);
					
					(function(li, className) {
						className.addEventListener('click', function(event) {
													event.preventDefault();
													if(li.classList.contains('fancy-menu-collapsed')) {
														li.classList.remove('fancy-menu-collapsed');
														document.cookie = 'iscollapsed=0; max-age=3600';
													}
													else {
														li.classList.add('fancy-menu-collapsed');
														document.cookie = 'iscollapsed=1; max-age=3600';	
													}
												});
					})(classLi, spanLi);
					
					var fancyUl = document.createElement('ul');
					fancyUl.classList.add('fancy-menu');
					fancyUl.setAttribute('data-menu', menuData[j]);
					classLi.appendChild(fancyUl);
					lastUl.appendChild(classLi);
					lastUl = fancyUl;
				} else
					lastUl = e;
			}
		}
		
		li.classList.add('fancy-menu-method');
		li.setAttribute('data-method', items[i].name);
		li.innerText = menuData[j];
		
		if(menuData[j] == 'Sin resultados')
			li.classList.add('fancy-search-err');
		
		if(li) {
			(function(li, url) {
				li.addEventListener('click', function(event) {
									event.preventDefault();
									location.href = url;
								});
			})(li, menuUrl);
		}
		lastUl.appendChild(li);
		nav.appendChild(ul);
	}
}

function readCookie(name) {
	var cookies = document.cookie.split(';');
	for(var i = 0; i < cookies.length; i++) {
		if(cookies[i].indexOf(name) > -1 && !isMobile()) {
			return cookies[i].split("=")[1];
		}
	}
	return false;
}