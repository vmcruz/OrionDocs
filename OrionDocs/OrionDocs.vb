Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Module OrionDocs
#Region "Variables de version"
    Public major As String = My.Application.Info.Version.Major.ToString '# Incrementar con grandes cambios que no son compatibles con la versión previa
    Public minor As String = My.Application.Info.Version.Minor.ToString '# Incrementar con nuevas características, compatibles con la versión previa
    Public patch As String = My.Application.Info.Version.Build.ToString '# Incrementar con parches
    Public revision As String = My.Application.Info.Version.Revision.ToString
    Public preRelease As String = "β" '# Modificar con cada etapa de desarrollo, actualmente beta [alpha | beta | rc (release candidate)]
    Public buildDate As String = "2017.11.13" '# Cambiar con cada build de trabajo diaria
    Public orionDocsVersion As String = major + "." + minor + "." + patch + "." + revision + preRelease
    Public orionDocsBuild As String = buildDate
#End Region

#Region "Variables públicas"
    Public orionDocsFileContent As String = ""
    Public orionDocsFileSplitted As String()
    Public orionDocsProjectFile As String = ""
    Public templateConfiguration As Dictionary(Of String, String)
#End Region

#Region "Descripción de las extensiones de archivo"
    '# OTT          Orion Tag Template
    '# OTD          Orion Tag Definition
    '# OTC          Orion Template Configuration
#End Region

    Sub Main()
        Dim argv As String() = Environment.GetCommandLineArgs()
        Dim argc As Integer = argv.Count
        Console.OutputEncoding = System.Text.Encoding.Unicode
        'CompileProject()
        If argc - 1 > 0 Then
            If argv(1).ToLower() = "-h" Or argv(1).ToLower() = "/?" Or argv(1).ToLower() = "/h" Or argv(1).ToLower() = "--help" Then
                ShowHelp()
            ElseIf argv(1).ToLower() = "-v" Or argv(1).ToLower() = "/v" Or argv(1).ToLower() = "/#" Or argv(1).ToLower() = "--version" Then
                Console.WriteLine()
                Console.WriteLine("OrionDocs v" + orionDocsVersion + " ∫ Build " + orionDocsBuild)
                Console.WriteLine("Víctor Cruz - 2016")
            ElseIf argv(1).ToLower = "--easteregg" Then
                Console.WriteLine(" ▒█████   ██▀███   ██▓ ▒█████   ███▄    █ ▓█████▄  ▒█████   ▄████▄    ██████ ")
                Console.WriteLine("▒██▒  ██▒▓██ ▒ ██▒▓██▒▒██▒  ██▒ ██ ▀█   █ ▒██▀ ██▌▒██▒  ██▒▒██▀ ▀█  ▒██    ▒ ")
                Console.WriteLine("▒██░  ██▒▓██ ░▄█ ▒▒██▒▒██░  ██▒▓██  ▀█ ██▒░██   █▌▒██░  ██▒▒▓█    ▄ ░ ▓██▄   ")
                Console.WriteLine("▒██   ██░▒██▀▀█▄  ░██░▒██   ██░▓██▒  ▐▌██▒░▓█▄   ▌▒██   ██░▒▓▓▄ ▄██▒  ▒   ██▒")
                Console.WriteLine("░ ████▓▒░░██▓ ▒██▒░██░░ ████▓▒░▒██░   ▓██░░▒████▓ ░ ████▓▒░▒ ▓███▀ ░▒██████▒▒")
                Console.WriteLine("░ ▒░▒░▒░ ░ ▒▓ ░▒▓░░▓  ░ ▒░▒░▒░ ░ ▒░   ▒ ▒  ▒▒▓  ▒ ░ ▒░▒░▒░ ░ ░▒ ▒  ░▒ ▒▓▒ ▒ ░")
                Console.WriteLine("░ ▒ ▒░   ░▒ ░ ▒░ ▒ ░  ░ ▒ ▒░ ░ ░░   ░ ▒░ ░ ▒  ▒   ░ ▒ ▒░   ░  ▒   ░ ░▒  ░ ░")
                Console.WriteLine("░ ░ ░ ▒    ░░   ░  ▒ ░░ ░ ░ ▒     ░   ░ ░  ░ ░  ░ ░ ░ ░ ▒  ░        ░  ░  ░")
                Console.WriteLine("    ░ ░     ░      ░      ░ ░           ░    ░        ░ ░  ░ ░            ░  ")
                Console.WriteLine("                                           ░               ░                 ")
                Console.WriteLine("v" + orionDocsVersion + " @ " + orionDocsBuild + " Copyright Víctor Cruz - 2016")
            Else
                CompileProject(argv)
            End If
        Else
            ShowHelp()
        End If
        End
    End Sub

    Sub CompileProject(ByVal argumentList As String())
        Dim argv() As String = argumentList
        Dim argc As Integer = argv.Count
        Dim fs As FileStream = Nothing
        Dim sw As StreamWriter = Nothing

        '#Configuración en caso de que no se encuentre el archivo @template.otc
        templateConfiguration = New Dictionary(Of String, String)
        templateConfiguration.Add("COPY_DIR", "")
        templateConfiguration.Add("FIRST_LINE", "@description")
        templateConfiguration.Add("FILE_EXTENSION", ".html")
        templateConfiguration.Add("GOTO_REDIR", "<a href='{PROPERURL}.{FILE_EXTENSION}#{PROPERURL}'>{URL}</a>")
        templateConfiguration.Add("DEFAULT_PROJECT_TITLE", "Reference")

        '# Nombre del archivo
        Dim DFile As String = argv(1)
        Dim isDebug As Boolean = False
        Dim isFullDebug As Boolean = False
        Dim isOnlyDebug As Boolean = False

        '# Habilitar el debug agregando ? al nombre del archivo a documentar, útil para ver cómo está obteniendo los grupos
        If Right(DFile, 1) = "?" Then
            isDebug = True
            DFile = DFile.Replace("?", "")
            Console.WriteLine("OrionDocs: <Debug> mode enabled")
        ElseIf Right(DFile, 1) = "!" Then
            isDebug = True
            isFullDebug = True
            DFile = DFile.Replace("!", "")
            Console.WriteLine("OrionDocs: <Full debug> mode enabled")
        ElseIf Right(DFile, 1) = "#" Then
            isDebug = True
            isFullDebug = True
            isOnlyDebug = True
            DFile = DFile.Replace("#", "")
            Console.WriteLine("OrionDocs: <Only full debug> mode enabled.")
        End If

        If isDebug Then
            fs = New FileStream("Debug.log", FileMode.Create)
            sw = New StreamWriter(fs)
            Console.SetOut(sw)
        End If

        '# Nombre del tema a usar, si no existe usar @default
        Dim themeName As String = "default"
        Dim theme As String = "@default"
        Dim isThemeDefined As Boolean = False
        If argc = 5 Then
            theme = argv(argc - 3)
            If argv(argc - 2) = "-t" Then
                templateConfiguration("DEFAULT_PROJECT_TITLE") = argv(argc - 1)
            Else
                Console.WriteLine("OrionDocs: Don't know how to handle '" + argv(argc - 2) + "' instruction. Try --help. Can't continue...")
                End
            End If
        ElseIf argc = 4 Then
            If argv(argc - 2) = "-t" Then
                templateConfiguration("DEFAULT_PROJECT_TITLE") = argv(argc - 1)
            Else
                Console.WriteLine("OrionDocs: Don't know how to handle '" + argv(argc - 2) + "' instruction. Try --help. Can't continue...")
                End
            End If
        ElseIf argc = 3 Then
            theme = argv(argc - 1)
        ElseIf argc = 2 Then
            theme = "@default"
        Else
            ShowHelp()
            End
        End If

        isThemeDefined = (Left(theme, 1) = "@" And theme.Length > 1)

        If isThemeDefined Then
            themeName = theme.Replace("@", "")
        Else
            Console.WriteLine("OrionDocs: Invalid template name. Please verify.")
            End
        End If

        If isDebug Then Console.WriteLine("OrionDocs: " + themeName + " template selected")
        Dim oTemplate As OTemplate
        Try
            '# Cargar las definiciones y el template de los tags que usa el tema
            oTemplate = New OTemplate(themeName)
            oTemplate.LoadTemplateFiles()
        Catch e As System.Exception
            Console.WriteLine("System error: " + e.Message + ". Can't continue...")
            End
        End Try
        If isDebug Then Console.WriteLine("OrionDocs: " + themeName + " template loaded")

        Dim themePath As String = System.AppDomain.CurrentDomain.BaseDirectory() + "templates\" + themeName
        Dim configExists As Boolean = False
        Dim configContent As String = ""

        If oTemplate.ConfigExists("config") Then
            configExists = True
            If isDebug Then Console.WriteLine("OrionDocs: 'config' section loaded")

            If oTemplate.GetConfig("config", "copydir").Length > 0 Then
                templateConfiguration("COPY_DIR") = oTemplate.GetConfig("config", "copydir")
                If isDebug Then Console.WriteLine("OrionDocs: Directories: " + templateConfiguration("COPY_DIR") + " will be copied at the end")
            Else
                If isDebug Then Console.WriteLine("OrionDocs: 'copydir' config is not set. No directories will be copied")
            End If

            If oTemplate.GetConfig("config", "firstline").Length > 0 Then
                templateConfiguration("FIRST_LINE") = oTemplate.GetConfig("config", "firstline")
                If isDebug Then Console.WriteLine("OrionDocs: First line will be treated as " + templateConfiguration("FIRST_LINE"))
            Else
                If isDebug Then Console.WriteLine("OrionDocs: 'firstline' config is not set. Using the default: '" + templateConfiguration("FIRST_LINE") + "'")
            End If

            If oTemplate.GetConfig("config", "file_ext").Length > 0 Then
                templateConfiguration("FILE_EXTENSION") = oTemplate.GetConfig("config", "file_ext")
                If isDebug Then Console.WriteLine("OrionDocs: The project files will have '." + templateConfiguration("FILE_EXTENSION") + "' extension")
            Else
                If isDebug Then Console.WriteLine("OrionDocs: 'file_extension' config is not set. Using the default: '" + templateConfiguration("FILE_EXTENSION") + "'")
            End If

            If oTemplate.ContainsTemplate("goto") Then 'oTemplate.getConfig("config", "fn_goto_template").Length > 0 
                templateConfiguration("GOTO_REDIR") = oTemplate.GetTemplate("goto")
                If isDebug Then Console.WriteLine("OrionDocs: '[goto]' special tag set to " + templateConfiguration("GOTO_REDIR"))
            Else
                If isDebug Then Console.WriteLine("OrionDocs: 'goto.ott' file doesn't exist. Using the default goto special tag template: '" + templateConfiguration("GOTO_REDIR") + "'")
            End If

            If oTemplate.GetConfig("config", "default_project_title").Length > 0 Then
                If templateConfiguration("DEFAULT_PROJECT_TITLE") = "Reference" Then
                    templateConfiguration("DEFAULT_PROJECT_TITLE") = oTemplate.GetConfig("config", "default_project_title")
                End If
                If isDebug Then Console.WriteLine("OrionDocs: Default project title is: '" + templateConfiguration("DEFAULT_PROJECT_TITLE") + "'")
            Else
                If isDebug Then Console.WriteLine("OrionDocs: 'default_project_title' config is not set. Using the default: '" + templateConfiguration("DEFAULT_PROJECT_TITLE") + "'")
            End If
        Else
            Console.WriteLine("OrionDocs: Configuration file '" + themeName + ".otc' doesn't exist. Default values will be used.")
        End If

        Dim orionMenu As String = ""

        '# Template principal, esqueleto para colocar el contenido de los tags
        Dim mainOTT As String = oTemplate.GetTemplate(themeName)

        '# Crea el menu usando el archivo *.ott indicado
        Dim searchMenu As Regex = New Regex("{\s*LOADMENU\s*:\s*(\w+).ott\s*}")
        Dim myMenu As Match = searchMenu.Match(mainOTT)
        Dim menuOTT As String = ""

        If myMenu.Groups.Count = 2 And oTemplate.ContainsTemplate(myMenu.Groups(1).ToString()) Then
            menuOTT = oTemplate.GetTemplate(myMenu.Groups(1).ToString())
        ElseIf mainOTT.IndexOf("{LOADMENU:") > -1 Then
            Console.WriteLine("OrionDocs: Couldn't find the menu template.")
        End If

        Dim mergedFiles As String() = DFile.Split("+")
        Array.Sort(mergedFiles)
        Dim oBlock As OBlock
        Dim oBlocks As List(Of OBlock) = New List(Of OBlock)

        For m = 0 To mergedFiles.Count - 1
            If File.Exists(mergedFiles(m)) Then
                Try
                    Dim f As StreamReader = New StreamReader(mergedFiles(m))

                    '# Cargar el contenido del archivo a documentar
                    orionDocsFileContent = f.ReadToEnd()
                    orionDocsFileSplitted = orionDocsFileContent.Split(vbCrLf)
                    orionDocsProjectFile = mergedFiles(m)

                    f.Close()
                    If isDebug Then Console.WriteLine("OrionDocs: File '" + mergedFiles(m) + "' loaded")

                    '# Obtener los bloques
                    Dim regexBlocks As Regex = New Regex("/\*\*(.+?)\*/", RegexOptions.Singleline)

                    Dim commentBlock As MatchCollection = regexBlocks.Matches(orionDocsFileContent)

                    If isDebug Then
                        If commentBlock.Count = 1 Then
                            Console.WriteLine("OrionDocs: 1 comment block was found")
                        Else
                            Console.WriteLine("OrionDocs: " + commentBlock.Count.ToString() + " comment blocks were found")
                        End If
                    End If

                    '# Cargar cada bloque de comentarios individualmente, con sus parámetros
                    '# Solo se cargan aquellos bloques que tengan al menos un parámetro
                    For i = 0 To commentBlock.Count - 1
                        If isDebug Then Console.WriteLine("OrionDocs: Working on comment block #" + (i + 1).ToString())
                        Dim blockContent As String() = commentBlock(i).Groups(1).ToString().Trim().Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
                        Dim wasFirstLineFixed As Boolean = False
                        Dim params As List(Of String) = New List(Of String)
                        Dim indent As String = ""
                        Dim indentTmp As String = ""
                        Dim wasMultiple As Boolean = False

                        For j = 0 To blockContent.Count - 1
                            Dim blockContentTrim As String = blockContent(j).Trim()
                            blockContent(j) = blockContent(j).Replace(vbTab, "    ")
                            If blockContent(j).Trim() <> "" Then
                                If Left(blockContentTrim, 1) = "@" Or j = 0 Then
                                    If wasMultiple Then
                                        Dim realParam As String() = params(params.Count - 1).Split(vbCrLf)
                                        params(params.Count - 1) = realParam(0)
                                        For k = 1 To realParam.Count - 1
                                            If realParam(k) <> "" Then
                                                If realParam.Count > 2 Then
                                                    params(params.Count - 1) += vbCrLf + realParam(k).Substring(indent.Length() + 1)
                                                Else
                                                    params(params.Count - 1) += vbCrLf + realParam(k).Trim()
                                                End If
                                            End If
                                        Next
                                        wasMultiple = False
                                    End If

                                    If templateConfiguration("FIRST_LINE") <> "" And Left(blockContentTrim, 1) <> "@" And j = 0 Then
                                        blockContentTrim = templateConfiguration("FIRST_LINE") + " " + blockContentTrim
                                        wasFirstLineFixed = True
                                    End If

                                    indentTmp = blockContent(j).Replace(blockContent(j).TrimStart(), "")
                                    indent = indentTmp
                                    params.Add(blockContentTrim)
                                ElseIf params.Count > 0 Then
                                    indentTmp = blockContent(j).Replace(blockContent(j).TrimStart(), "")
                                    If indentTmp.Length < indent.Length Then indent = indentTmp
                                    params(params.Count - 1) += vbCrLf + blockContent(j)
                                    wasMultiple = True
                                End If
                            End If
                        Next

                        If wasFirstLineFixed And params.Count >= 2 Then
                            Dim tmp As String = params(1)
                            params(1) = params(0)
                            params(0) = tmp
                        End If

                        If isDebug Then
                            If params.Count = 1 Then
                                Console.WriteLine("OrionDocs: 1 tag was found")
                            Else
                                Console.WriteLine("OrionDocs: " + params.Count.ToString() + " tags were found")
                            End If
                        End If

                        '#Creación de los tags de cada bloque
                        Dim tagEndsAt As Integer, tagName As String, tagContent As String
                        If params.Count > 0 Then
                            oBlock = New OBlock()
                            For j = 0 To params.Count - 1
                                tagEndsAt = params(j).IndexOf(" ")
                                If tagEndsAt > -1 Then
                                    tagName = params(j).Substring(0, tagEndsAt).Trim()
                                    tagContent = params(j).Substring(tagEndsAt + 1).Trim()

                                    If tagName.Length > 0 And tagContent.Length > 0 And oTemplate.ContainsDefinition(tagName) Then
                                        Dim regexParam As Regex = New Regex(oTemplate.GetTagRegex(tagName))
                                        Dim match As Match = regexParam.Match(tagContent)
                                        If match.Groups.Count > 1 Then
                                            Dim oTag As OTag = New OTag(tagName, match.Groups)
                                            oBlock.add(oTag)
                                        Else
                                            Console.WriteLine("OrionDocs: The tag content """ + tagContent + """ doesn't match the regex (" + tagName + ") """ + oTemplate.GetTagRegex(tagName) + """. Will be ignored.")
                                            If (j = 0 AndAlso params(0).Substring(0, 1) = "@") Or (j = 1 AndAlso params(0).Substring(0, 1) <> "@") Then j = params.Count
                                        End If
                                    Else
                                        Console.WriteLine("OrionDocs: The tag definition for tag """ + tagName + """ doesn't exist. Will be ignored.")
                                    End If
                                Else
                                    Console.WriteLine("OrionDocs: Invalid tag format for """ + params(j) + """")
                                End If
                            Next j

                            If oBlock.Count() > 0 Then
                                If isFullDebug Then Console.WriteLine("OrionDocs: Comment Block #" + (i + 1).ToString() + " is:")
                                If isFullDebug Then Console.WriteLine(oBlock.ToString())

                                orionMenu += oTemplate.GetBlockTemplate(oBlock, menuOTT) & vbCrLf
                                oBlocks.Add(oBlock)
                            End If
                        End If
                    Next i
                Catch e As Exception
                    Console.WriteLine("System Error: " + e.Message + ". Can't continue...")
                    End
                End Try
            Else
                Console.WriteLine("OrionDocs: The file '" + mergedFiles(m) + "' doesn't exist. Can't continue...")
                End
            End If
        Next m

        If isDebug Then
            sw.Close()
            Dim standardOutput As New StreamWriter(Console.OpenStandardOutput())
            Console.SetOut(standardOutput)
        End If

        If isOnlyDebug = False Then
            '# Se crea el directorio del proyecto
            Dim project As String = (New Regex("[^\w]+")).Replace(templateConfiguration("DEFAULT_PROJECT_TITLE"), "_") 'Path.GetFileNameWithoutExtension(DFile)
            If Not Directory.Exists(project) Then
                Directory.CreateDirectory(project)
            End If

            '# Todos los bloques que sí tienen parámetros
            For i = 0 To oBlocks.Count - 1
                If oBlocks(i).getBlockType <> "" Then
                    Dim name As String = oBlocks(i).getTag(0).GetGroup(0)
                    Dim properName As String = (New Regex("[^\w]+")).Replace(name, "_")

                    Dim HTML As StreamWriter = New StreamWriter(project + "\" + properName + templateConfiguration("FILE_EXTENSION"), False, Encoding.UTF8)
                    Dim htmlContent As String = ""
                    If myMenu.Groups.Count = 2 Then
                        htmlContent = mainOTT.Replace(myMenu.Groups(0).ToString(), orionMenu)
                    End If
                    htmlContent = htmlContent.Replace("{CONTENT}", oTemplate.GetBlockTemplate(oBlocks(i)))

                    HTML.Write(htmlContent)
                    HTML.Close()
                End If
            Next

            '#Checamos COPY_DIR por si hay que copiar directorios
            If templateConfiguration("COPY_DIR") <> "" Then
                Dim folders As String() = templateConfiguration("COPY_DIR").Split(New String() {"+"}, StringSplitOptions.RemoveEmptyEntries)
                For i = 0 To folders.Count - 1
                    Try
                        My.Computer.FileSystem.CopyDirectory(themePath + "\" + folders(i).Trim(), project + "\" + folders(i).Trim(), True)
                    Catch e As Exception
                        Console.WriteLine(themePath + "\" + folders(i).Trim())
                        Console.WriteLine(project + "\" + folders(i).Trim())
                        Console.WriteLine("System Error: " + e.Message + ". Can't continue...")
                        End
                    End Try
                Next
            End If
        End If
    End Sub

    Public Sub ShowHelp()
        Console.WriteLine(vbCrLf + "OrionDocs v" + orionDocsVersion + " - Víctor Cruz 2016")
        Console.WriteLine("Usage: " + Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName).Replace(".exe", "") + " thefile[+file2+file3...][?|!|#] [@templateName] [-t ""Project Title""]")
        Console.WriteLine("   thefile        The file to be processed")
        Console.WriteLine("                     > Appending + and another filename, will add it to the documentation.")
        Console.WriteLine("                       Do not leave blank spaces between the filenames")
        Console.WriteLine("                     > Appending ? enables basic debug mode")
        Console.WriteLine("                     > Appending ! enables full debug mode")
        Console.WriteLine("                     > Appending # enables only full debug mode. Project files are not created")
        Console.WriteLine("   @templateName  Specifies which template will be used. If omitted uses @default")
        Console.WriteLine("   -t             Sets the project title. If not it will use the title defined in template.otd(config.default_project_title).")
        Console.WriteLine("                  Else, OrionDocs will use 'Reference' as title")
    End Sub
End Module
