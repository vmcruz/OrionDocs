Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class frmConfig
    Private templateList As List(Of OTemplate)
    Private templateFolder As List(Of String)
    Private templateLanguageList As Dictionary(Of String, String)
    Private selectedListIndex As Integer

    Private Sub frmConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblAbout.Text = "OrionDocs v" + orionDocsVersion + " (c) by Víctor Cruz" & vbCrLf & vbCrLf
        lblAbout.Text += "OrionDocs v" + orionDocsVersion + " Is licensed under a Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License." & vbCrLf & vbCrLf
        lblAbout.Text += "You should have received a copy of the license along with this work. If not, see <http://creativecommons.org/licenses/by-nc-nd/4.0/>."

        txtTitle.Text = My.Settings.DefaultTitle
        txtFirstLine.Text = My.Settings.FirstLine
        txtGoto.Text = My.Settings.GotoRedir
        txtExtension.Text = My.Settings.FileExtension
        chkDebug.Checked = My.Settings.SaveDebug
        chkCreateIndex.Checked = My.Settings.CreateIndex
        txtIndexContent.Text = My.Settings.IndexContent
        lstTemplate.MultiSelect = False
        selectedListIndex = -1

        Try
            templateLanguageList = New Dictionary(Of String, String)
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

                    templateItem.Text = templateTmp.GetConfig("info", "name")

                    If templateTmp.GetConfig("info", "screenshot") <> "" Then
                        If File.Exists(template + "\" + templateTmp.GetConfig("info", "screenshot")) Then
                            templateImgList.Images.Add(templateTmp.GetConfig("info", "name"), Bitmap.FromFile(template + "\" + templateTmp.GetConfig("info", "screenshot")))
                        Else
                            templateImgList.Images.Add(templateTmp.GetConfig("info", "name"), pctError.Image)
                        End If
                        templateItem.ImageKey = templateTmp.GetConfig("info", "name")
                    End If

                    lstTemplate.Items.Add(templateItem)
                    templateList.Add(templateTmp)
                    templateFolder.Add(template.Replace(Application.StartupPath + "\templates\", ""))

                    If templateTmp.GetConfig("info", "name") = My.Settings.DefaultTemplate AndAlso templateTmp.fileHash = My.Settings.TemplateHash Then
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

            Try
                templateLanguageList.Clear()
                cboLanguages.Items.Clear()
                Dim selectedLanguage As Integer = 0
                If info.GetConfig("info", "languages") <> "" Then
                    Dim languages() As String = info.GetConfig("info", "languages").Split(",")
                    For Each language As String In languages
                        Dim langConfig() As String = language.Trim().Split(":")
                        If langConfig.Count = 2 Then
                            templateLanguageList.Add(langConfig(0).Trim(), langConfig(1).Trim())
                            cboLanguages.Items.Add(langConfig(0).Trim())
                            If langConfig(1).Trim() = My.Settings.TemplateLang Then selectedLanguage = cboLanguages.Items.Count - 1
                        End If
                    Next
                    cboLanguages.SelectedIndex = selectedLanguage
                End If
            Catch ex As Exception
                MsgBox(ex.GetType().ToString + ": " + ex.Message, MsgBoxStyle.Critical, "Error loading template")
                End
            End Try
        Else
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
                .TemplateHash = templateList(selectedListIndex).fileHash
                .TemplateLang = templateLanguageList(cboLanguages.Text)
                .DefaultTitle = txtTitle.Text
                .GotoRedir = txtGoto.Text
                .FirstLine = txtFirstLine.Text
                .FileExtension = txtExtension.Text
                .SaveDebug = chkDebug.Checked
                .CreateIndex = chkCreateIndex.Checked
                .IndexContent = txtIndexContent.Text
                .Save()
                MsgBox("Default settings have been updated", vbInformation, "OrionDocs Settings Configuration")
                FocusLstView()
            End With
        Else
            MsgBox("Please reselect the template for default configuration.", vbExclamation)
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
        chkCreateIndex.Checked = True
        txtIndexContent.Text = "<script>location.href='{PAGE(0)}';</script>"

        Dim i As Integer = 0
        selectedListIndex = 0
        For i = 0 To lstTemplate.Items.Count - 1
            If templateList(i).fileHash = My.Settings.TemplateHash Then
                selectedListIndex = i
                Exit For
            End If
        Next

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