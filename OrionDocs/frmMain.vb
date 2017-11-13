Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Text.RegularExpressions

Public Class frmMain

    Private drag As Boolean
    Private mousex As Integer
    Private mousey As Integer
    Private conMenu As New ContextMenu
    Private templateList As List(Of OTemplate)
    Private selectedTemplate As Integer = 0

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DoubleBuffered = True
        drag = False

        panelAbout.Size = New Size(648, 498)
        panelAbout.Location = New Point(248, 27)

        panelStep1.Size = panelAbout.Size
        panelStep1.Location = panelAbout.Location

        panelStep2.Size = panelAbout.Size
        panelStep2.Location = panelAbout.Location

        panelStep3.Size = panelAbout.Size
        panelStep3.Location = panelAbout.Location

        Dim removeSelected As New MenuItem
        Dim clear As New MenuItem
        Dim separator As New MenuItem
        removeSelected.Text = "&Remove"
        clear.Text = "&Clear"
        separator.Text = "-"

        AddHandler removeSelected.Click, AddressOf removeSelected_Click
        AddHandler clear.Click, AddressOf clear_Click
        AddHandler conMenu.Popup, AddressOf conMenu_Opening


        conMenu.MenuItems.Add(removeSelected)
        conMenu.MenuItems.Add(separator)
        conMenu.MenuItems.Add(clear)
        lvProject.ContextMenu = conMenu
    End Sub

    Private Sub conMenu_Opening(ByVal sender As Object, ByVal e As System.EventArgs)

        If lvProject.SelectedItems().Count = 0 Then
            conMenu.MenuItems.Item(0).Visible = False
            conMenu.MenuItems.Item(1).Visible = False
        Else
            conMenu.MenuItems.Item(0).Text = "&Remove (" + lvProject.SelectedItems().Count.ToString + ")"
            conMenu.MenuItems.Item(0).Visible = True
            conMenu.MenuItems.Item(1).Visible = True
        End If


        If lvProject.Items.Count = 0 Then
            conMenu.MenuItems.Item(2).Visible = False
        Else
            conMenu.MenuItems.Item(2).Visible = True
        End If
    End Sub

    Private Sub removeSelected_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each item As ListViewItem In lvProject.SelectedItems()
            lvProject.Items.RemoveAt(item.Index)
        Next

        If lvProject.Items.Count = 0 Then
            btnToStep2.Enabled = False
        End If
    End Sub

    Private Sub clear_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lvProject.Clear()
        btnToStep2.Enabled = False
    End Sub

    Private Sub frmMain_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Private Sub frmMain_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub frmMain_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        drag = False
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        End
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start(LinkLabel1.Text)
    End Sub

    Private Sub btnToStep1_Click(sender As Object, e As EventArgs) Handles btnToStep1.Click
        Me.SuspendLayout()
        panelAbout.Visible = False
        lblStep1.Visible = True
        panelStep1.Visible = True
        If lvProject.Items.Count = 0 Then
            btnToStep2.Enabled = False
        Else
            btnToStep2.Enabled = True
        End If
        Me.ResumeLayout()
    End Sub

    Private Sub btnBackToAbout_Click(sender As Object, e As EventArgs) Handles btnBackToAbout.Click
        Me.SuspendLayout()
        panelAbout.Visible = True
        lblStep1.Visible = False
        panelStep1.Visible = False
        Me.ResumeLayout()
    End Sub

    Private Sub btnToStep2_Click(sender As Object, e As EventArgs) Handles btnToStep2.Click
        Me.SuspendLayout()
        panelStep1.Visible = False
        lblStep2.Visible = True
        lblStep1.Visible = False
        panelStep2.Visible = True
        btnToStep3.Enabled = False
        btnRefresh.PerformClick()
        Me.ResumeLayout()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim templates As String() = Directory.GetDirectories(Application.StartupPath + "\templates\")
            templateList = New List(Of OTemplate)
            Dim templateImgList As New ImageList()
            Dim atLeastOne As Boolean = False
            templateImgList.ImageSize = New Size(230, 153)
            lvTemplate.Clear()
            lvTemplate.LargeImageList = templateImgList


            For Each template As String In templates
                Dim templateTmp As New OTemplate(template.Replace(Application.StartupPath + "\templates\", ""))
                If templateTmp.getConfig("info", "name") <> "" Then
                    Dim templateItem As New ListViewItem
                    templateItem.Text = templateTmp.getConfig("info", "name")
                    If templateTmp.getConfig("info", "screenshot") <> "" Then
                        templateImgList.Images.Add(templateTmp.getConfig("info", "name"), Bitmap.FromFile(template + "\" + templateTmp.getConfig("info", "screenshot")))
                        templateItem.ImageKey = templateTmp.getConfig("info", "name")
                    End If
                    lvTemplate.Items.Add(templateItem)
                    atLeastOne = True
                    templateList.Add(templateTmp)
                End If
            Next

            If atLeastOne Then
                lvTemplate.FocusedItem = lvTemplate.Items(0)
                lvTemplate.Focus()
                lvTemplate.Items(0).Selected = True
                lvTemplate.Items(0).Focused = True
            End If
        Catch ex As Exception
            MsgBox(ex.GetType().ToString + ": " + ex.Message, MsgBoxStyle.Critical, "Error loading template list")
        End Try
    End Sub

    Private Sub btnBackToStep1_Click(sender As Object, e As EventArgs) Handles btnBackToStep1.Click
        Me.SuspendLayout()
        panelStep1.Visible = True
        lblStep2.Visible = False
        lblStep1.Visible = True
        panelStep2.Visible = False
        Me.ResumeLayout()
    End Sub

    Private Sub btnToStep3_Click(sender As Object, e As EventArgs) Handles btnToStep3.Click
        Me.SuspendLayout()
        panelStep2.Visible = False
        lblStep3.Visible = True
        lblStep2.Visible = False
        panelStep3.Visible = True
        Dim argv As String() = constructArgv()
        txtOdocs.Text = argv(0) + " " + argv(1) + " " + argv(2) + " " + argv(3) + " """ + argv(4) + """"
        Me.ResumeLayout()
    End Sub

    Private Sub btnBackToStep2_Click(sender As Object, e As EventArgs) Handles btnBackToStep2.Click
        Me.SuspendLayout()
        panelStep2.Visible = True
        lblStep3.Visible = False
        lblStep2.Visible = True
        panelStep3.Visible = False
        btnRefresh.PerformClick()
        Me.ResumeLayout()
    End Sub

    Private Sub btnCompile_Click(sender As Object, e As EventArgs) Handles btnCompile.Click
        OrionDocs.CompileProject(constructArgv())
        Me.Dispose()
    End Sub

    Private Function constructArgv() As String()
        Dim argv(4) As String
        Dim debugMode As String = ""

        Select Case cboDebugMode.SelectedIndex
            Case 0
                debugMode = ""
            Case 1
                debugMode = "?"
            Case 2
                debugMode = "!"
            Case 3
                debugMode = "#"
        End Select
        Dim fileList As New List(Of String)
        For Each item As ListViewItem In lvProject.Items
            fileList.Add(item.ToolTipText)
        Next
        argv(0) = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName).Replace(".exe", "")
        argv(1) = String.Join("+", fileList) + debugMode
        argv(2) = "@" + templateList(selectedTemplate).getTemplateName()
        argv(3) = "-t"
        argv(4) = txtProjectTitle.Text
        Return argv
    End Function

    Private Sub lvProject_DragDrop(sender As Object, e As DragEventArgs) Handles lvProject.DragDrop
        Dim filenames As List(Of String) = New List(Of String)
        For Each f In e.Data.GetData(DataFormats.FileDrop)
            filenames.Add(f)
        Next
        filenames.Sort()
        If filenames.Count > 0 Then
            readFileList(filenames)
        End If
    End Sub

    Private Sub lvProject_DragEnter(sender As Object, e As DragEventArgs) Handles lvProject.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub readFileList(ByVal fileList As List(Of String))
        For Each file In fileList
            Dim item As New ListViewItem
            item.Text = Path.GetFileName(file)
            item.ToolTipText = file
            If imgList.Images.ContainsKey(Path.GetExtension(file).Replace(".", "")) Then
                item.ImageKey = Path.GetExtension(file).Replace(".", "")
            Else
                item.ImageKey = "demon"
            End If

            If lvProject.Items.Count > 0 Then
                If Not TypeOf lvProject.FindItemWithText(item.Text, False, 0, False) Is ListViewItem Then
                    lvProject.Items.Add(item)
                End If
            Else
                lvProject.Items.Add(item)
            End If
            btnToStep2.Enabled = True
        Next
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        With openFile
            .Filter = "All files (*.*)|*.*"
            .FileName = ""
            .Multiselect = True
            .Title = "Load project files"
            .ShowDialog()
            If (.FileNames.Count = 1 And .FileNames(0) = "") Or .FileNames.Count = 0 Then
                Exit Sub
            End If

            Dim filenames As List(Of String) = New List(Of String)
            For Each f In .FileNames
                filenames.Add(f)
            Next
            filenames.Sort()
            readFileList(filenames)
        End With
    End Sub

    Private Sub lvTemplate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvTemplate.SelectedIndexChanged
        If lvTemplate.SelectedItems.Count = 1 Then
            selectedTemplate = lvTemplate.SelectedItems(0).Index
            Dim info As OTemplate = templateList(selectedTemplate)
            If info.getConfig("info", "name") <> "" Then
                lblName.Text = info.getConfig("info", "name")
            Else
                lblName.Text = ""
            End If

            If info.getConfig("info", "description") <> "" Then
                lblDescription.Text = info.getConfig("info", "description")
            Else
                lblDescription.Text = ""
            End If

            If info.getConfig("info", "author") <> "" Then
                lblAuthor.Text = info.getConfig("info", "author")
            Else
                lblAuthor.Text = ""
            End If

            If info.getConfig("info", "contact") <> "" Then
                lblContact.Text = info.getConfig("info", "contact")
            Else
                lblContact.Text = ""
            End If

            If info.getConfig("info", "web") <> "" Then
                lblWeb.Text = info.getConfig("info", "web")
            Else
                lblWeb.Text = ""
            End If

            If info.getConfig("info", "copyright") <> "" Then
                lblCopyright.Text = info.getConfig("info", "copyright")
            Else
                lblCopyright.Text = ""
            End If

            If info.getConfig("config", "default_project_title") <> "" Then
                txtProjectTitle.Text = info.getConfig("config", "default_project_title")
            Else
                txtProjectTitle.Text = "Default project title"
            End If

            btnToStep3.Enabled = True
            cboDebugMode.SelectedIndex = 0
        Else
            btnToStep3.Enabled = False
            txtProjectTitle.Text = "Default project title"
            cboDebugMode.SelectedIndex = 0
            lblCopyright.Text = ""
            lblWeb.Text = ""
            lblContact.Text = ""
            lblAuthor.Text = ""
            lblDescription.Text = ""
            lblName.Text = ""
        End If
    End Sub

    Private Sub btnSearchTags_Click(sender As Object, e As EventArgs)
        Dim r As Integer
        r = MsgBox("This action will scan every file looking for tags and may take a while." + vbCrLf + vbCrLf + "Are you sure you want to continue?", vbOKCancel + vbExclamation, "Scanning tags...")
        If r = vbOK Then
            MsgBox("OK")
        End If
    End Sub

    Private Sub txtProjectTitle_TextChanged(sender As Object, e As EventArgs) Handles txtProjectTitle.TextChanged
        If txtProjectTitle.Text = "" Then
            btnCompile.Enabled = False
        Else
            btnCompile.Enabled = True
            Dim argv As String() = constructArgv()
            Dim debugFile As String = ""
            If Strings.Right(argv(1), 1) = "?" Or Strings.Right(argv(1), 1) = "!" Or Strings.Right(argv(1), 1) = "#" Then
                debugFile = " > Debug.log"
            End If
            txtOdocs.Text = argv(0) + " " + argv(1) + " " + argv(2) + " " + argv(3) + " """ + argv(4) + """" + debugFile
        End If
    End Sub

    Private Sub cboDebugMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDebugMode.SelectedIndexChanged
        Dim argv As String() = constructArgv()
        Dim debugFile As String = ""
        If Strings.Right(argv(1), 1) = "?" Or Strings.Right(argv(1), 1) = "!" Or Strings.Right(argv(1), 1) = "#" Then
            debugFile = " > Debug.log"
        End If
        txtOdocs.Text = argv(0) + " " + argv(1) + " " + argv(2) + " " + argv(3) + " """ + argv(4) + """" + debugFile
    End Sub
End Class