Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Module OrionDocs
    'Para ocultar la consola
    Private Declare Auto Function GetConsoleWindow Lib "kernel32.dll" () As IntPtr
    Private Declare Auto Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
    Private Const SW_HIDE As Integer = 0
    Private Const SW_SHOW As Integer = 5

#Region "Variables de version"
    Public major As String = My.Application.Info.Version.Major.ToString '# Incrementar con grandes cambios que no son compatibles con la versión previa
    Public minor As String = My.Application.Info.Version.Minor.ToString '# Incrementar con nuevas características, compatibles con la versión previa
    Public patch As String = My.Application.Info.Version.Build.ToString '# Incrementar con parches
    Public revision As String = My.Application.Info.Version.Revision.ToString
    Public preRelease As String = "β" '# Modificar con cada etapa de desarrollo, actualmente beta [alpha | beta | rc (release candidate)]
    Public buildDate As String = "2017.12.10" '# Cambiar con cada build de trabajo diaria
    Public orionDocsVersion As String = major + "." + minor + "." + patch + "." + revision + preRelease
    Public orionDocsBuild As String = buildDate
#End Region

#Region "Variables públicas"
    Public orionDocsFileContent As String = ""
    Public orionDocsFileSplitted As String()
    Public orionDocsProjectFile As String = ""
    Public templateConfiguration As Dictionary(Of String, String)
    Private debugFile As String = ""
#End Region

#Region "Descripción de las extensiones de archivo"
    '# OTT          Orion Tag Template
    '# OTD          Orion Tag Definition
    '# OTC          Orion Template Configuration
#End Region

    Sub Main()
        Dim argv As String() = Environment.GetCommandLineArgs()
        Dim argc As Integer = argv.Count
        Dim hWndConsole As Integer
        hWndConsole = GetConsoleWindow()
        Dim GUID As String = New Guid(
                        CType(My.Application.GetType.Assembly.GetCustomAttributes(
                            GetType(Runtime.InteropServices.GuidAttribute),
                            False
                        )(0),
                            Runtime.InteropServices.GuidAttribute).Value
                    ).ToString

        If argc - 1 > 0 Then
            If argv(1).Substring(0, 1) = "-" Or argv(1).Substring(0, 1) = "/" Then
                If argv(1).ToLower() = "-h" Or argv(1) = "/?" Or argv(1).ToLower() = "/h" Or argv(1).ToLower() = "--help" Then
                    ShowHelp()
                ElseIf argv(1).ToLower() = "-v" Or argv(1).ToLower() = "/v" Or argv(1).ToLower() = "/#" Or argv(1).ToLower() = "--version" Then
                    Console.WriteLine()
                    Console.WriteLine("OrionDocs v" + orionDocsVersion + " ∫ Build " + orionDocsBuild)
                    Console.WriteLine("Víctor Cruz - 2017")
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
                    Console.WriteLine("v" + orionDocsVersion + " @ " + orionDocsBuild + " Víctor Cruz - 2017")
                ElseIf argv(1).ToLower = "--config" Then
                    If hWndConsole <> IntPtr.Zero Then
                        ' Hide the console window...
                        ShowWindow(hWndConsole, SW_HIDE)
                        Dim c As New frmConfig
                        c.ShowDialog()
                        'ShowWindow(hWndConsole, SW_SHOW)
                        End
                    End If
                Else
                    Console.WriteLine("OrionDocs Err: Unknown command")
                End If
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
        templateConfiguration.Add("FIRST_LINE", My.Settings.FirstLine)
        templateConfiguration.Add("FILE_EXTENSION", My.Settings.FileExtension)
        templateConfiguration.Add("GOTO_REDIR", My.Settings.GotoRedir)
        templateConfiguration.Add("DEFAULT_PROJECT_TITLE", My.Settings.DefaultTitle)

        '# Nombre del archivo
        Dim DFile As String = argv(1)
        Dim isDebug As Boolean = False
        Dim isFullDebug As Boolean = False
        Dim isOnlyDebug As Boolean = False

        '# Habilitar el debug agregando ? al nombre del archivo a documentar, útil para ver cómo está obteniendo los grupos

        If Right(DFile, 1) = "?" Then
            isDebug = True
            DFile = DFile.Replace("?", "")
            Debug("OrionDocs Inf: <Debug> mode enabled")
        ElseIf Right(DFile, 1) = "!" Then
            isDebug = True
            isFullDebug = True
            DFile = DFile.Replace("!", "")
            Debug("OrionDocs Inf: <Full debug> mode enabled")
        ElseIf Right(DFile, 1) = "#" Then
            isDebug = True
            isFullDebug = True
            isOnlyDebug = True
            DFile = DFile.Replace("#", "")
            Debug("OrionDocs Inf: <Only full debug> mode enabled.")
        End If

        '# Nombre del tema a usar, si no existe usar @default
        Dim themeName As String = My.Settings.DefaultTemplate
        Dim theme As String = "@" + My.Settings.DefaultTemplate
        Dim projectTitleArg As Boolean = False

        If argc >= 3 Then
            If argv(2).Substring(0, 1) = "?" Then
                argv(2) = InputBox("Enter the name of the template:", "Template Name", "@default")
                If argv(2).Length > 0 Then
                    If argv(2).Substring(0, 1) <> "@" Then
                        argv(2) = "@" + argv(2)
                    End If
                Else
                    argv(2) = "@default"
                End If
            End If

            If argc = 4 AndAlso argv(3).Substring(0, 1) = "?" Then
                argv(3) = InputBox("Enter the title of the project:", "Project Title", "{FILENAME}")
                If argv(3).Length = 0 Then
                    argv(3) = "{FILENAME}"
                End If
            End If

            If argv(2).Substring(0, 1) = "@" AndAlso argv(2).Length > 1 Then
                theme = argv(2)
                If argc = 4 Then
                    templateConfiguration("DEFAULT_PROJECT_TITLE") = argv(3)
                    projectTitleArg = True
                End If
            Else
                templateConfiguration("DEFAULT_PROJECT_TITLE") = argv(2)
                projectTitleArg = True
                If argc = 4 Then
                    If argv(3).Substring(0, 1) = "@" AndAlso argv(3).Length > 1 Then
                        theme = argv(3)
                    Else
                        Debug("OrionDocs Err(1): Don't know how to handle '" + argv(3) + "' instruction, will be ignored. Try --help.")
                    End If
                End If
            End If
        Else
            If isDebug Then Debug("OrionDocs Wrn: Undefined Project Name and Template. Using ""Reference"" as Project Name, and @default as Template.")
        End If

        themeName = theme.Substring(1)

        If isDebug Then Debug("OrionDocs Inf: '" + themeName + "' template selected")
        Dim oTemplate As OTemplate
        Try
            '# Cargar las definiciones y el template de los tags que usa el tema
            oTemplate = New OTemplate(themeName)
            oTemplate.LoadTemplateFiles()
        Catch e As Exception
            Debug("System Error(1): " + e.Message)
            If isDebug Then
                sw.Close()
                Dim standardOutput As New StreamWriter(Console.OpenStandardOutput())
                Console.SetOut(standardOutput)
            End If
            End
        End Try

        If isDebug Then Debug("OrionDocs Inf: '" + themeName + "' template loaded")

        Dim themePath As String = AppDomain.CurrentDomain.BaseDirectory() + "templates\" + themeName
        Dim configExists As Boolean = False
        Dim configContent As String = ""

        If oTemplate.ConfigExists("config") Then
            configExists = True
            If isDebug Then Debug("OrionDocs Inf: 'config' section loaded")

            If oTemplate.GetConfig("config", "copydir").Length > 0 Then
                templateConfiguration("COPY_DIR") = oTemplate.GetConfig("config", "copydir")
                If isDebug Then Debug("OrionDocs Inf: Directories: " + templateConfiguration("COPY_DIR") + " will be copied at the end")
            Else
                If isDebug Then Debug("OrionDocs Wrn: 'copydir' config is not set. No directories will be copied")
            End If

            If oTemplate.GetConfig("config", "firstline").Length > 0 Then
                templateConfiguration("FIRST_LINE") = oTemplate.GetConfig("config", "firstline")
                If isDebug Then Debug("OrionDocs Inf: First line will be treated as " + templateConfiguration("FIRST_LINE"))
            Else
                If isDebug Then Debug("OrionDocs Wrn: 'firstline' config is not set. Using the default: '" + templateConfiguration("FIRST_LINE") + "'")
            End If

            If oTemplate.GetConfig("config", "fileext").Length > 0 Then
                templateConfiguration("FILE_EXTENSION") = oTemplate.GetConfig("config", "fileext")
                If isDebug Then Debug("OrionDocs Inf: The project files will have '" + templateConfiguration("FILE_EXTENSION") + "' extension")
            Else
                If isDebug Then Debug("OrionDocs Wrn: 'fileext' config is not set. Using the default: '" + templateConfiguration("FILE_EXTENSION") + "'")
            End If

            If oTemplate.ContainsTemplate("goto") Then
                templateConfiguration("GOTO_REDIR") = oTemplate.GetTemplate("goto")
                If isDebug Then Debug("OrionDocs Inf: 'goto' special tag loaded from goto.ott")
            Else
                If isDebug Then Debug("OrionDocs Wrn: 'goto.ott' file doesn't exist. Using the default goto special tag template: '" + templateConfiguration("GOTO_REDIR") + "'")
            End If

            If Not projectTitleArg Then
                If oTemplate.GetConfig("config", "title").Length > 0 Then
                    If templateConfiguration("DEFAULT_PROJECT_TITLE") = "{FILENAME}" Then
                        templateConfiguration("DEFAULT_PROJECT_TITLE") = oTemplate.GetConfig("config", "title")
                    End If
                    If isDebug Then Debug("OrionDocs Inf: Default project title is: '" + templateConfiguration("DEFAULT_PROJECT_TITLE") + "'")
                Else
                    If isDebug Then Debug("OrionDocs Wrn: 'title' config is not set. Using the default: '" + templateConfiguration("DEFAULT_PROJECT_TITLE") + "'")
                End If
            Else
                If isDebug Then Debug("OrionDocs Inf: Project title set to: '" + templateConfiguration("DEFAULT_PROJECT_TITLE") + "'")
            End If
        Else
            If isDebug Then Debug("OrionDocs Wrn: 'config' section doesn't exist in '" + themeName + ".otc'. Default values will be used.")
        End If

        '# Template principal, esqueleto para colocar el contenido de los tags
        Dim mainOTT As String = oTemplate.GetTemplate(themeName)

        '# Crea el menu usando el archivo *.ott indicado
        Dim searchMenu As Regex = New Regex("{\s*foreachblock\s*:\s*(\w+).ott\s*}", RegexOptions.Singleline + RegexOptions.IgnoreCase)
        Dim myForEach As MatchCollection = searchMenu.Matches(mainOTT)
        Dim foreachTemplates As New Dictionary(Of String, String)

        For f = 0 To myForEach.Count - 1
            If myForEach(f).Groups.Count = 2 AndAlso oTemplate.ContainsTemplate(myForEach(f).Groups(1).ToString()) Then
                foreachTemplates.Add(myForEach(f).Groups(0).ToString(), oTemplate.GetTemplate(myForEach(f).Groups(1).ToString()))
            End If
        Next

        Dim mergedFiles As String() = DFile.Split("+")
        Array.Sort(mergedFiles)

        For m = 0 To mergedFiles.Count - 1
            If File.Exists(mergedFiles(m)) Then
                Try
                    Dim oBlock As OBlock
                    Dim oBlocks As List(Of OBlock) = New List(Of OBlock)
                    Dim foreachParsedTemplates As New Dictionary(Of String, String)
                    Dim f As StreamReader = New StreamReader(mergedFiles(m))

                    '# Cargar el contenido del archivo a documentar
                    orionDocsFileContent = f.ReadToEnd()
                    orionDocsFileSplitted = orionDocsFileContent.Split(vbCrLf)
                    orionDocsProjectFile = mergedFiles(m)

                    Dim fixedProjectTitle = templateConfiguration("DEFAULT_PROJECT_TITLE").Replace("{FILENAME}", Path.GetFileNameWithoutExtension(mergedFiles(m)))

                    f.Close()
                    If isDebug Then Debug("OrionDocs Inf: File '" + mergedFiles(m) + "' loaded")

                    '# Obtener los bloques
                    Dim regexBlocks As Regex = New Regex("/\*\*(.+?)\*/", RegexOptions.Singleline)

                    Dim commentBlock As MatchCollection = regexBlocks.Matches(orionDocsFileContent)

                    If isDebug Then
                        If commentBlock.Count = 1 Then
                            Debug("OrionDocs Inf: 1 comment block was found")
                        Else
                            Debug("OrionDocs Inf: " + commentBlock.Count.ToString() + " comment blocks were found")
                        End If
                    End If

                    '# Cargar cada bloque de comentarios individualmente, con sus parámetros
                    '# Solo se cargan aquellos bloques que tengan al menos un parámetro
                    For i = 0 To commentBlock.Count - 1
                        If isDebug Then Debug("OrionDocs Inf: Working on comment block #" + (i + 1).ToString())
                        Dim blockContent As String() = commentBlock(i).Groups(1).ToString().Trim().Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
                        Dim wasFirstLineFixed As Boolean = False
                        Dim params As List(Of String) = New List(Of String)
                        Dim wasMultiple As Boolean = False

                        For j = 0 To blockContent.Count - 1
                            Dim blockContentTrim As String = blockContent(j).Trim()
                            blockContent(j) = blockContent(j).Replace(vbTab, "    ")
                            If blockContent(j).Trim() <> "" Then
                                If Left(blockContentTrim, 1) = "@" Or j = 0 Then
                                    If wasMultiple Then
                                        params(params.Count - 1) = FixIndent(params(params.Count - 1))
                                        wasMultiple = False
                                    End If

                                    If templateConfiguration("FIRST_LINE") <> "" AndAlso Left(blockContentTrim, 1) <> "@" AndAlso j = 0 Then
                                        blockContentTrim = templateConfiguration("FIRST_LINE") + " " + blockContentTrim
                                        wasFirstLineFixed = True
                                    End If

                                    params.Add(blockContentTrim)
                                ElseIf params.Count > 0 Then
                                    params(params.Count - 1) += vbCrLf + blockContent(j)
                                    wasMultiple = True
                                End If
                            End If
                        Next

                        If wasMultiple Then
                            params(params.Count - 1) = FixIndent(params(params.Count - 1))
                        End If

                        If wasFirstLineFixed AndAlso params.Count >= 2 Then
                            Dim tmp As String = params(1)
                            params(1) = params(0)
                            params(0) = tmp
                        End If

                        If isDebug Then
                            If params.Count = 1 Then
                                Debug("OrionDocs Inf: 1 tag was found")
                            Else
                                Debug("OrionDocs Inf: " + params.Count.ToString() + " tags were found")
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

                                    If tagName.Length > 0 AndAlso tagContent.Length > 0 AndAlso oTemplate.ContainsDefinition(tagName) Then
                                        Dim regexParam As Regex = New Regex(oTemplate.GetTagRegex(tagName), RegexOptions.Singleline)
                                        Dim match As Match = regexParam.Match(tagContent)
                                        If match.Groups.Count > 1 Then
                                            Dim oTag As OTag = New OTag(tagName, match.Groups)
                                            oBlock.Add(oTag)
                                        Else
                                            If isDebug Then Debug("OrionDocs Wrn: The tag content """ + tagContent + """ doesn't match the regex (" + tagName + ") """ + oTemplate.GetTagRegex(tagName) + """. Will be ignored.")
                                            If (j = 0 AndAlso params(0).Substring(0, 1) = "@") Or (j = 1 AndAlso params(0).Substring(0, 1) <> "@") Then j = params.Count
                                        End If
                                    Else
                                        If isDebug Then Debug("OrionDocs Wrn: The tag definition for tag """ + tagName + """ doesn't exist. Will be ignored.")
                                    End If
                                Else
                                    If isDebug Then Debug("OrionDocs Wrn: Invalid tag format for """ + params(j) + """. Will be ignored.")
                                End If
                            Next j

                            If oBlock.TagCount() > 0 Then
                                If isFullDebug Then Debug("OrionDocs Inf: Comment Block #" + (i + 1).ToString() + " is:")
                                If isFullDebug Then Debug(oBlock.ToString())

                                For Each pair As KeyValuePair(Of String, String) In foreachTemplates
                                    If foreachParsedTemplates.ContainsKey(pair.Key) Then
                                        foreachParsedTemplates(pair.Key) += oTemplate.GetBlockTemplate(oBlock, pair.Value) & vbCrLf
                                    Else
                                        foreachParsedTemplates.Add(pair.Key, oTemplate.GetBlockTemplate(oBlock, pair.Value) & vbCrLf)
                                    End If
                                Next
                                oBlocks.Add(oBlock)
                            End If
                        End If
                    Next i

                    If isOnlyDebug = False AndAlso commentBlock.Count > 0 Then
                        '# Se crea el directorio del proyecto
                        Dim project As String = (New Regex("[^\w]+")).Replace(fixedProjectTitle, "_") 'Path.GetFileNameWithoutExtension(DFile)
                        If Not Directory.Exists(project) Then
                            Directory.CreateDirectory(project)
                            If isDebug Then Debug("OrionDocs Inf: Project directory '" + project + "' has been created.")
                        End If

                        '# Todos los bloques que sí tienen parámetros
                        For i = 0 To oBlocks.Count - 1
                            If oBlocks(i).GetBlockType <> "" Then
                                Dim name As String = oBlocks(i).GetTag(0).GetGroup(0)
                                Dim properName As String = (New Regex("[^\w]+")).Replace(name, "_")

                                Dim HTML As StreamWriter = New StreamWriter(project + "\" + properName + templateConfiguration("FILE_EXTENSION"), False, Encoding.UTF8)
                                Dim htmlContent As String = ""

                                If isDebug Then Debug("OrionDocs Inf: StreamWriter for file '" + project + "\" + properName + templateConfiguration("FILE_EXTENSION") + "' has been created.")

                                htmlContent = mainOTT
                                For Each pair As KeyValuePair(Of String, String) In foreachParsedTemplates
                                    htmlContent = htmlContent.Replace(pair.Key, pair.Value)
                                Next

                                htmlContent = htmlContent.Replace("{VERSION}", orionDocsVersion)
                                htmlContent = htmlContent.Replace("{BUILD}", orionDocsBuild)

                                htmlContent = htmlContent.Replace("{PROJECT_TITLE}", fixedProjectTitle)
                                htmlContent = htmlContent.Replace("{FILE_EXTENSION}", templateConfiguration("FILE_EXTENSION"))
                                htmlContent = htmlContent.Replace("{CONTENT}", oTemplate.GetBlockTemplate(oBlocks(i)))

                                HTML.Write(htmlContent)
                                HTML.Close()

                                If isDebug Then Debug("OrionDocs Inf: StreamWriter has finished with not errors.")
                            End If
                        Next

                        '#Checamos COPY_DIR por si hay que copiar directorios
                        If templateConfiguration("COPY_DIR") <> "" Then
                            Dim folders As String() = templateConfiguration("COPY_DIR").Split(New String() {"+"}, StringSplitOptions.RemoveEmptyEntries)
                            For i = 0 To folders.Count - 1
                                Try
                                    My.Computer.FileSystem.CopyDirectory(themePath + "\" + folders(i).Trim(), project + "\" + folders(i).Trim(), True)
                                    If isDebug Then Debug("OrionDocs Inf: Dir copied from: '" + themePath + "\" + folders(i).Trim() + "' to '" + project + "\" + folders(i).Trim() + "'.")
                                Catch e As Exception
                                    Debug("System Error(3) :   " + e.Message)
                                End Try
                            Next
                        End If
                    End If
                Catch e As Exception
                    Debug("System Error(2): " + e.Message)
                End Try
            Else
                Debug("OrionDocs Err(2): The file '" + mergedFiles(m) + "' doesn't exist.")
            End If

            If isDebug AndAlso My.Settings.SaveDebug Then
                fs = New FileStream(Path.GetFileName(mergedFiles(m)) + ".log", FileMode.Create)
                sw = New StreamWriter(fs)
                sw.Write(debugFile)
                sw.Close()
                fs.Close()
            End If
        Next m
    End Sub

    Public Sub ShowHelp()
        Console.WriteLine(vbCrLf + "OrionDocs v" + orionDocsVersion + " - Víctor Cruz 2017")
        Console.WriteLine("Usage: " + Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName).Replace(".exe", "") + " file[+file2+file3...][?|!|#] [""Title""] [@templateName]")
        Console.WriteLine("   file           The file to be processed.")
        Console.WriteLine("                     > Appending + and another filename will add it to the documentation.")
        Console.WriteLine("                       Do not leave blank spaces between the filenames.")
        Console.WriteLine("                     > Appending ? enables basic debug mode.")
        Console.WriteLine("                     > Appending ! enables full debug mode.")
        Console.WriteLine("                     > Appending # enables only full debug mode. Project files are not created.")
        Console.WriteLine("   ""Title""        Sets the project title. If not it will use the title defined in template.otd(config.default_project_title).")
        Console.WriteLine("                  Else, OrionDocs will use 'Reference' as title.")
        Console.WriteLine("   @templateName  Specifies which template will be used. If omitted uses @default.")
    End Sub

    Public Function FixIndent(ByVal Text As String) As String
        Dim realParam As String() = Text.Split(vbCrLf)
        Dim retText As String = ""

        If realParam.Count >= 2 Then
            Dim minIndent As String = realParam(1).Replace(realParam(1).TrimStart(), "")
            Dim currIndent As String = ""

            For i = 2 To realParam.Count - 1
                currIndent = realParam(i).Replace(realParam(i).TrimStart(), "")
                If currIndent.Length < minIndent.Length Then minIndent = currIndent
            Next

            retText = realParam(0)
            For k = 1 To realParam.Count - 1
                If realParam(k) <> "" Then
                    retText += vbCrLf + realParam(k).Substring(minIndent.Length)
                End If
            Next
        Else
            retText = realParam(0).Trim()
        End If

        Return retText
    End Function

    Private Sub Debug(ByVal Text As String)
        debugFile += Format(Now, "hh:mm:ss.fff") + " | " + Text + vbCrLf
    End Sub
End Module
