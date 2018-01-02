<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig))
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnRestore = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tbpTemplate = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblLang = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblWeb = New System.Windows.Forms.LinkLabel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblContact = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblAuthor = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstTemplate = New System.Windows.Forms.ListView()
        Me.tbpDocumentation = New System.Windows.Forms.TabPage()
        Me.txtGoto = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtExtension = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFirstLine = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkDebug = New System.Windows.Forms.CheckBox()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbpAbout = New System.Windows.Forms.TabPage()
        Me.lblAbout = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.tbpTemplate.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tbpDocumentation.SuspendLayout()
        Me.tbpAbout.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(231, 333)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(98, 23)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnRestore
        '
        Me.btnRestore.Location = New System.Drawing.Point(127, 333)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(98, 23)
        Me.btnRestore.TabIndex = 2
        Me.btnRestore.Text = "Restore defaults"
        Me.btnRestore.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tbpTemplate)
        Me.TabControl1.Controls.Add(Me.tbpDocumentation)
        Me.TabControl1.Controls.Add(Me.tbpAbout)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(317, 315)
        Me.TabControl1.TabIndex = 13
        '
        'tbpTemplate
        '
        Me.tbpTemplate.Controls.Add(Me.GroupBox1)
        Me.tbpTemplate.Controls.Add(Me.lstTemplate)
        Me.tbpTemplate.Location = New System.Drawing.Point(4, 22)
        Me.tbpTemplate.Name = "tbpTemplate"
        Me.tbpTemplate.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpTemplate.Size = New System.Drawing.Size(309, 289)
        Me.tbpTemplate.TabIndex = 0
        Me.tbpTemplate.Text = "Template"
        Me.tbpTemplate.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblLang)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.lblWeb)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.lblContact)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.lblAuthor)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblDescription)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 142)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(295, 141)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Information"
        '
        'lblLang
        '
        Me.lblLang.AutoSize = True
        Me.lblLang.Location = New System.Drawing.Point(85, 117)
        Me.lblLang.Name = "lblLang"
        Me.lblLang.Size = New System.Drawing.Size(0, 13)
        Me.lblLang.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(21, 117)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Language:"
        '
        'lblWeb
        '
        Me.lblWeb.AutoSize = True
        Me.lblWeb.Location = New System.Drawing.Point(85, 104)
        Me.lblWeb.Name = "lblWeb"
        Me.lblWeb.Size = New System.Drawing.Size(0, 13)
        Me.lblWeb.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(30, 104)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 13)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Website:"
        '
        'lblContact
        '
        Me.lblContact.AutoSize = True
        Me.lblContact.Location = New System.Drawing.Point(85, 91)
        Me.lblContact.Name = "lblContact"
        Me.lblContact.Size = New System.Drawing.Size(0, 13)
        Me.lblContact.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(32, 91)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Contact:"
        '
        'lblAuthor
        '
        Me.lblAuthor.AutoSize = True
        Me.lblAuthor.Location = New System.Drawing.Point(85, 78)
        Me.lblAuthor.Name = "lblAuthor"
        Me.lblAuthor.Size = New System.Drawing.Size(0, 13)
        Me.lblAuthor.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(38, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Author:"
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(85, 29)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(204, 49)
        Me.lblDescription.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 29)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Description:"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(85, 16)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(0, 13)
        Me.lblName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(41, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Name:"
        '
        'lstTemplate
        '
        Me.lstTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstTemplate.Location = New System.Drawing.Point(6, 6)
        Me.lstTemplate.Name = "lstTemplate"
        Me.lstTemplate.Size = New System.Drawing.Size(295, 130)
        Me.lstTemplate.TabIndex = 1
        Me.lstTemplate.UseCompatibleStateImageBehavior = False
        '
        'tbpDocumentation
        '
        Me.tbpDocumentation.Controls.Add(Me.txtGoto)
        Me.tbpDocumentation.Controls.Add(Me.Label5)
        Me.tbpDocumentation.Controls.Add(Me.txtExtension)
        Me.tbpDocumentation.Controls.Add(Me.Label4)
        Me.tbpDocumentation.Controls.Add(Me.txtFirstLine)
        Me.tbpDocumentation.Controls.Add(Me.Label3)
        Me.tbpDocumentation.Controls.Add(Me.chkDebug)
        Me.tbpDocumentation.Controls.Add(Me.txtTitle)
        Me.tbpDocumentation.Controls.Add(Me.Label1)
        Me.tbpDocumentation.Location = New System.Drawing.Point(4, 22)
        Me.tbpDocumentation.Name = "tbpDocumentation"
        Me.tbpDocumentation.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpDocumentation.Size = New System.Drawing.Size(309, 289)
        Me.tbpDocumentation.TabIndex = 1
        Me.tbpDocumentation.Text = "Documentation"
        Me.tbpDocumentation.UseVisualStyleBackColor = True
        '
        'txtGoto
        '
        Me.txtGoto.Location = New System.Drawing.Point(6, 131)
        Me.txtGoto.Multiline = True
        Me.txtGoto.Name = "txtGoto"
        Me.txtGoto.Size = New System.Drawing.Size(292, 105)
        Me.txtGoto.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(122, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "{goto} fallback template:"
        '
        'txtExtension
        '
        Me.txtExtension.Location = New System.Drawing.Point(171, 81)
        Me.txtExtension.Name = "txtExtension"
        Me.txtExtension.Size = New System.Drawing.Size(130, 20)
        Me.txtExtension.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(159, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "File extension of documentation:"
        '
        'txtFirstLine
        '
        Me.txtFirstLine.Location = New System.Drawing.Point(133, 46)
        Me.txtFirstLine.Name = "txtFirstLine"
        Me.txtFirstLine.Size = New System.Drawing.Size(168, 20)
        Me.txtFirstLine.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(121, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "First block line behavior:"
        '
        'chkDebug
        '
        Me.chkDebug.AutoSize = True
        Me.chkDebug.Location = New System.Drawing.Point(6, 252)
        Me.chkDebug.Name = "chkDebug"
        Me.chkDebug.Size = New System.Drawing.Size(112, 17)
        Me.chkDebug.TabIndex = 13
        Me.chkDebug.Text = "Save debug to file"
        Me.chkDebug.UseVisualStyleBackColor = True
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(78, 11)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(223, 20)
        Me.txtTitle.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Project Title:"
        '
        'tbpAbout
        '
        Me.tbpAbout.Controls.Add(Me.lblAbout)
        Me.tbpAbout.Location = New System.Drawing.Point(4, 22)
        Me.tbpAbout.Name = "tbpAbout"
        Me.tbpAbout.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpAbout.Size = New System.Drawing.Size(309, 289)
        Me.tbpAbout.TabIndex = 2
        Me.tbpAbout.Text = "About"
        Me.tbpAbout.UseVisualStyleBackColor = True
        '
        'lblAbout
        '
        Me.lblAbout.Location = New System.Drawing.Point(6, 3)
        Me.lblAbout.Name = "lblAbout"
        Me.lblAbout.Size = New System.Drawing.Size(297, 171)
        Me.lblAbout.TabIndex = 0
        Me.lblAbout.Text = resources.GetString("lblAbout.Text")
        '
        'frmConfig
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(340, 364)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnRestore)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Default Settings Configuration"
        Me.TabControl1.ResumeLayout(False)
        Me.tbpTemplate.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tbpDocumentation.ResumeLayout(False)
        Me.tbpDocumentation.PerformLayout()
        Me.tbpAbout.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As Windows.Forms.Button
    Friend WithEvents btnRestore As Windows.Forms.Button
    Friend WithEvents TabControl1 As Windows.Forms.TabControl
    Friend WithEvents tbpDocumentation As Windows.Forms.TabPage
    Friend WithEvents txtGoto As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents txtExtension As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents txtFirstLine As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents chkDebug As Windows.Forms.CheckBox
    Friend WithEvents txtTitle As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents tbpTemplate As Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents lblWeb As Windows.Forms.LinkLabel
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents lblContact As Windows.Forms.Label
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents lblAuthor As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents lblDescription As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents lblName As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents lstTemplate As Windows.Forms.ListView
    Friend WithEvents lblLang As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents tbpAbout As Windows.Forms.TabPage
    Friend WithEvents lblAbout As Windows.Forms.Label
End Class
