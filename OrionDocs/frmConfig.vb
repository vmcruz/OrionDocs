Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class frmConfig
    Private templateList As List(Of OTemplate)
    Private templateFolder As List(Of String)
    Private selectedListIndex As Integer

    Private Sub frmConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtTitle.Text = My.Settings.DefaultTitle
        txtFirstLine.Text = My.Settings.FirstLine
        txtGoto.Text = My.Settings.GotoRedir
        txtExtension.Text = My.Settings.FileExtension
        chkDebug.Checked = My.Settings.SaveDebug
        lstTemplate.MultiSelect = False
        selectedListIndex = -1

        Try
            Dim templates As String() = Directory.GetDirectories(Application.StartupPath + "\templates\")
            templateList = New List(Of OTemplate)
            templateFolder = New List(Of String)
            Dim templateImgList As New ImageList()
            templateImgList.ImageSize = New Size(150, 100)
            lstTemplate.Clear()
            lstTemplate.LargeImageList = templateImgList


            For Each template As String In templates
                Dim templateTmp As New OTemplate(template.Replace(Application.StartupPath + "\templates\", ""))
                If templateTmp.GetConfig("info", "name") <> "" Then
                    Dim templateItem As New ListViewItem()
                    Dim langText As String = ""

                    If templateTmp.GetConfig("info", "language").Length > 0 Then
                        langText = " (" + templateTmp.GetConfig("info", "language") + ")"
                    End If

                    templateItem.Text = templateTmp.GetConfig("info", "name") + langText

                    If templateTmp.GetConfig("info", "screenshot") <> "" Then
                        templateImgList.Images.Add(templateTmp.GetConfig("info", "name"), Bitmap.FromFile(template + "\" + templateTmp.GetConfig("info", "screenshot")))
                        templateItem.ImageKey = templateTmp.GetConfig("info", "name")
                    End If

                    lstTemplate.Items.Add(templateItem)
                    templateList.Add(templateTmp)
                    templateFolder.Add(template.Replace(Application.StartupPath + "\templates\", ""))

                    If templateTmp.GetConfig("info", "name") = My.Settings.DefaultTemplate Then
                        selectedListIndex = templateList.Count - 1
                    End If
                End If
            Next

            If lstTemplate.Items.Count > 0 Then
                FocusLstView()
            End If
        Catch ex As Exception
            MsgBox(ex.GetType().ToString + ": " + ex.Message, MsgBoxStyle.Critical, "Error loading template list")
            End
        End Try
    End Sub

    Private Sub lstTemplate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTemplate.SelectedIndexChanged
        If lstTemplate.SelectedItems.Count > 0 Then
            selectedListIndex = lstTemplate.SelectedItems(0).Index
            Dim info As OTemplate = templateList(selectedListIndex)
            If info.GetConfig("info", "name") <> "" Then
                lblName.Text = info.GetConfig("info", "name")
            Else
                lblName.Text = ""
            End If

            If info.GetConfig("info", "description") <> "" Then
                lblDescription.Text = info.GetConfig("info", "description")
            Else
                lblDescription.Text = ""
            End If

            If info.GetConfig("info", "author") <> "" Then
                lblAuthor.Text = info.GetConfig("info", "author")
            Else
                lblAuthor.Text = ""
            End If

            If info.GetConfig("info", "contact") <> "" Then
                lblContact.Text = info.GetConfig("info", "contact")
            Else
                lblContact.Text = ""
            End If

            If info.GetConfig("info", "web") <> "" Then
                lblWeb.Text = info.GetConfig("info", "web")
            Else
                lblWeb.Text = ""
            End If

            If info.GetConfig("info", "language") <> "" Then
                lblLang.Text = info.GetConfig("info", "language")
            Else
                lblLang.Text = ""
            End If
        Else
            lblLang.Text = ""
            lblWeb.Text = ""
            lblContact.Text = ""
            lblAuthor.Text = ""
            lblDescription.Text = ""
            lblName.Text = ""
            selectedListIndex = -1
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If selectedListIndex > -1 Then
            With My.Settings
                .DefaultTemplate = templateFolder(selectedListIndex)
                .DefaultTitle = txtTitle.Text
                .GotoRedir = txtGoto.Text
                .FirstLine = txtFirstLine.Text
                .FileExtension = txtExtension.Text
                .SaveDebug = chkDebug.Checked
                .Save()
                MsgBox("Default settings have been updated", vbInformation, "OrionDocs Settings Configuration")
                FocusLstView()
            End With
        Else
            MsgBox("Is a must to select a default template!", vbCritical)
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        FocusLstView()
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        txtTitle.Text = "{FILENAME}"
        txtGoto.Text = "<a href='{PROPERURL}.{FILE_EXTENSION}#{PROPERURL}'>{URL}</a>"
        txtFirstLine.Text = "@description"
        txtExtension.Text = ".html"
        chkDebug.Checked = True
        selectedListIndex = 0
        FocusLstView()
    End Sub

    Private Sub FocusLstView()
        If lstTemplate.Items.Count > 0 AndAlso selectedListIndex > -1 Then
            lstTemplate.FocusedItem = lstTemplate.Items(selectedListIndex)
            lstTemplate.Items(selectedListIndex).Selected = True
            lstTemplate.Items(selectedListIndex).Focused = True
            ActiveControl = lstTemplate
        End If
    End Sub
End Class