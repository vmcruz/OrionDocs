<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.openFile = New System.Windows.Forms.OpenFileDialog()
        Me.lblStep1 = New System.Windows.Forms.Label()
        Me.lblStep2 = New System.Windows.Forms.Label()
        Me.lblStep3 = New System.Windows.Forms.Label()
        Me.panelAbout = New System.Windows.Forms.Panel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.btnToStep1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.panelStep1 = New System.Windows.Forms.Panel()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lvProject = New System.Windows.Forms.ListView()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnBackToAbout = New System.Windows.Forms.Button()
        Me.btnToStep2 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.panelStep2 = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.groupInfo = New System.Windows.Forms.GroupBox()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblWeb = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblContact = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblAuthor = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lvTemplate = New System.Windows.Forms.ListView()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnBackToStep1 = New System.Windows.Forms.Button()
        Me.btnToStep3 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.panelStep3 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtOdocs = New System.Windows.Forms.TextBox()
        Me.cboDebugMode = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtProjectTitle = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnBackToStep2 = New System.Windows.Forms.Button()
        Me.btnCompile = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.consoleImgList = New System.Windows.Forms.ImageList(Me.components)
        Me.selectFolder = New System.Windows.Forms.FolderBrowserDialog()
        Me.panelAbout.SuspendLayout()
        Me.panelStep1.SuspendLayout()
        Me.panelStep2.SuspendLayout()
        Me.groupInfo.SuspendLayout()
        Me.panelStep3.SuspendLayout()
        Me.SuspendLayout()
        '
        'imgList
        '
        Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList.Images.SetKeyName(0, "asp")
        Me.imgList.Images.SetKeyName(1, "aspx")
        Me.imgList.Images.SetKeyName(2, "bat")
        Me.imgList.Images.SetKeyName(3, "cgi")
        Me.imgList.Images.SetKeyName(4, "class")
        Me.imgList.Images.SetKeyName(5, "cpp")
        Me.imgList.Images.SetKeyName(6, "css")
        Me.imgList.Images.SetKeyName(7, "dtd")
        Me.imgList.Images.SetKeyName(8, "htm")
        Me.imgList.Images.SetKeyName(9, "html")
        Me.imgList.Images.SetKeyName(10, "js")
        Me.imgList.Images.SetKeyName(11, "jsp")
        Me.imgList.Images.SetKeyName(12, "lua")
        Me.imgList.Images.SetKeyName(13, "php")
        Me.imgList.Images.SetKeyName(14, "pl")
        Me.imgList.Images.SetKeyName(15, "py")
        Me.imgList.Images.SetKeyName(16, "sql")
        Me.imgList.Images.SetKeyName(17, "demon")
        '
        'openFile
        '
        Me.openFile.FileName = "OpenFileDialog1"
        '
        'lblStep1
        '
        Me.lblStep1.BackColor = System.Drawing.Color.IndianRed
        Me.lblStep1.Location = New System.Drawing.Point(240, 98)
        Me.lblStep1.Name = "lblStep1"
        Me.lblStep1.Size = New System.Drawing.Size(3, 34)
        Me.lblStep1.TabIndex = 13
        Me.lblStep1.Visible = False
        '
        'lblStep2
        '
        Me.lblStep2.BackColor = System.Drawing.Color.IndianRed
        Me.lblStep2.Location = New System.Drawing.Point(240, 157)
        Me.lblStep2.Name = "lblStep2"
        Me.lblStep2.Size = New System.Drawing.Size(3, 34)
        Me.lblStep2.TabIndex = 14
        Me.lblStep2.Visible = False
        '
        'lblStep3
        '
        Me.lblStep3.BackColor = System.Drawing.Color.IndianRed
        Me.lblStep3.Location = New System.Drawing.Point(240, 213)
        Me.lblStep3.Name = "lblStep3"
        Me.lblStep3.Size = New System.Drawing.Size(3, 34)
        Me.lblStep3.TabIndex = 15
        Me.lblStep3.Visible = False
        '
        'panelAbout
        '
        Me.panelAbout.BackColor = System.Drawing.Color.Transparent
        Me.panelAbout.Controls.Add(Me.LinkLabel1)
        Me.panelAbout.Controls.Add(Me.btnToStep1)
        Me.panelAbout.Controls.Add(Me.Label4)
        Me.panelAbout.Controls.Add(Me.Label1)
        Me.panelAbout.Location = New System.Drawing.Point(10, 12)
        Me.panelAbout.Name = "panelAbout"
        Me.panelAbout.Size = New System.Drawing.Size(103, 71)
        Me.panelAbout.TabIndex = 17
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(343, 106)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(229, 13)
        Me.LinkLabel1.TabIndex = 9
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "http://victor.cr/uz/static/odocs_v0.16.2.3b.zip"
        '
        'btnToStep1
        '
        Me.btnToStep1.Location = New System.Drawing.Point(556, 459)
        Me.btnToStep1.Name = "btnToStep1"
        Me.btnToStep1.Size = New System.Drawing.Size(75, 23)
        Me.btnToStep1.TabIndex = 8
        Me.btnToStep1.Text = "Next"
        Me.btnToStep1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(13, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(544, 247)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = resources.GetString("Label4.Text")
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.Label1.Location = New System.Drawing.Point(13, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(276, 20)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "About"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(883, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 21)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "X"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(857, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 21)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "-"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'panelStep1
        '
        Me.panelStep1.BackColor = System.Drawing.Color.Transparent
        Me.panelStep1.Controls.Add(Me.btnSearch)
        Me.panelStep1.Controls.Add(Me.lvProject)
        Me.panelStep1.Controls.Add(Me.Label9)
        Me.panelStep1.Controls.Add(Me.btnBackToAbout)
        Me.panelStep1.Controls.Add(Me.btnToStep2)
        Me.panelStep1.Controls.Add(Me.Label6)
        Me.panelStep1.Location = New System.Drawing.Point(28, 331)
        Me.panelStep1.Name = "panelStep1"
        Me.panelStep1.Size = New System.Drawing.Size(139, 35)
        Me.panelStep1.TabIndex = 18
        Me.panelStep1.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(16, 459)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 12
        Me.btnSearch.Text = "Search..."
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lvProject
        '
        Me.lvProject.AllowDrop = True
        Me.lvProject.LargeImageList = Me.imgList
        Me.lvProject.Location = New System.Drawing.Point(16, 139)
        Me.lvProject.Name = "lvProject"
        Me.lvProject.ShowItemToolTips = True
        Me.lvProject.Size = New System.Drawing.Size(615, 301)
        Me.lvProject.TabIndex = 11
        Me.lvProject.UseCompatibleStateImageBehavior = False
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(13, 41)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(572, 87)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = resources.GetString("Label9.Text")
        '
        'btnBackToAbout
        '
        Me.btnBackToAbout.Location = New System.Drawing.Point(475, 459)
        Me.btnBackToAbout.Name = "btnBackToAbout"
        Me.btnBackToAbout.Size = New System.Drawing.Size(75, 23)
        Me.btnBackToAbout.TabIndex = 9
        Me.btnBackToAbout.Text = "Back"
        Me.btnBackToAbout.UseVisualStyleBackColor = True
        '
        'btnToStep2
        '
        Me.btnToStep2.Location = New System.Drawing.Point(556, 459)
        Me.btnToStep2.Name = "btnToStep2"
        Me.btnToStep2.Size = New System.Drawing.Size(75, 23)
        Me.btnToStep2.TabIndex = 8
        Me.btnToStep2.Text = "Next"
        Me.btnToStep2.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.Label6.Location = New System.Drawing.Point(13, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(308, 20)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Load the project files"
        '
        'panelStep2
        '
        Me.panelStep2.BackColor = System.Drawing.Color.Transparent
        Me.panelStep2.Controls.Add(Me.btnRefresh)
        Me.panelStep2.Controls.Add(Me.groupInfo)
        Me.panelStep2.Controls.Add(Me.lvTemplate)
        Me.panelStep2.Controls.Add(Me.Label10)
        Me.panelStep2.Controls.Add(Me.btnBackToStep1)
        Me.panelStep2.Controls.Add(Me.btnToStep3)
        Me.panelStep2.Controls.Add(Me.Label5)
        Me.panelStep2.Location = New System.Drawing.Point(28, 240)
        Me.panelStep2.Name = "panelStep2"
        Me.panelStep2.Size = New System.Drawing.Size(151, 65)
        Me.panelStep2.TabIndex = 20
        Me.panelStep2.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(16, 458)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 14
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'groupInfo
        '
        Me.groupInfo.Controls.Add(Me.lblCopyright)
        Me.groupInfo.Controls.Add(Me.Label13)
        Me.groupInfo.Controls.Add(Me.lblWeb)
        Me.groupInfo.Controls.Add(Me.Label17)
        Me.groupInfo.Controls.Add(Me.lblContact)
        Me.groupInfo.Controls.Add(Me.Label19)
        Me.groupInfo.Controls.Add(Me.lblAuthor)
        Me.groupInfo.Controls.Add(Me.Label16)
        Me.groupInfo.Controls.Add(Me.lblDescription)
        Me.groupInfo.Controls.Add(Me.Label14)
        Me.groupInfo.Controls.Add(Me.lblName)
        Me.groupInfo.Controls.Add(Me.Label11)
        Me.groupInfo.Location = New System.Drawing.Point(16, 352)
        Me.groupInfo.Name = "groupInfo"
        Me.groupInfo.Size = New System.Drawing.Size(615, 90)
        Me.groupInfo.TabIndex = 13
        Me.groupInfo.TabStop = False
        '
        'lblCopyright
        '
        Me.lblCopyright.Location = New System.Drawing.Point(414, 67)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(187, 13)
        Me.lblCopyright.TabIndex = 11
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(329, 67)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(54, 13)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "Copyright:"
        '
        'lblWeb
        '
        Me.lblWeb.Location = New System.Drawing.Point(414, 50)
        Me.lblWeb.Name = "lblWeb"
        Me.lblWeb.Size = New System.Drawing.Size(187, 13)
        Me.lblWeb.TabIndex = 9
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(329, 50)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(33, 13)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "Web:"
        '
        'lblContact
        '
        Me.lblContact.Location = New System.Drawing.Point(414, 33)
        Me.lblContact.Name = "lblContact"
        Me.lblContact.Size = New System.Drawing.Size(187, 13)
        Me.lblContact.TabIndex = 7
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(329, 33)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(47, 13)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "Contact:"
        '
        'lblAuthor
        '
        Me.lblAuthor.Location = New System.Drawing.Point(414, 16)
        Me.lblAuthor.Name = "lblAuthor"
        Me.lblAuthor.Size = New System.Drawing.Size(187, 13)
        Me.lblAuthor.TabIndex = 5
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(329, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(41, 13)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "Author:"
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(91, 39)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(223, 41)
        Me.lblDescription.TabIndex = 3
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 39)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(63, 13)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Description:"
        '
        'lblName
        '
        Me.lblName.Location = New System.Drawing.Point(91, 16)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(223, 13)
        Me.lblName.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(85, 13)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Template Name:"
        '
        'lvTemplate
        '
        Me.lvTemplate.Location = New System.Drawing.Point(16, 106)
        Me.lvTemplate.MultiSelect = False
        Me.lvTemplate.Name = "lvTemplate"
        Me.lvTemplate.Size = New System.Drawing.Size(615, 240)
        Me.lvTemplate.TabIndex = 12
        Me.lvTemplate.UseCompatibleStateImageBehavior = False
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(13, 41)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(572, 62)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = resources.GetString("Label10.Text")
        '
        'btnBackToStep1
        '
        Me.btnBackToStep1.Location = New System.Drawing.Point(475, 459)
        Me.btnBackToStep1.Name = "btnBackToStep1"
        Me.btnBackToStep1.Size = New System.Drawing.Size(75, 23)
        Me.btnBackToStep1.TabIndex = 9
        Me.btnBackToStep1.Text = "Back"
        Me.btnBackToStep1.UseVisualStyleBackColor = True
        '
        'btnToStep3
        '
        Me.btnToStep3.Location = New System.Drawing.Point(556, 459)
        Me.btnToStep3.Name = "btnToStep3"
        Me.btnToStep3.Size = New System.Drawing.Size(75, 23)
        Me.btnToStep3.TabIndex = 8
        Me.btnToStep3.Text = "Next"
        Me.btnToStep3.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.Label5.Location = New System.Drawing.Point(13, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(308, 20)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Select the template"
        '
        'panelStep3
        '
        Me.panelStep3.BackColor = System.Drawing.Color.Transparent
        Me.panelStep3.Controls.Add(Me.Label8)
        Me.panelStep3.Controls.Add(Me.Label15)
        Me.panelStep3.Controls.Add(Me.txtOdocs)
        Me.panelStep3.Controls.Add(Me.cboDebugMode)
        Me.panelStep3.Controls.Add(Me.Label20)
        Me.panelStep3.Controls.Add(Me.Label21)
        Me.panelStep3.Controls.Add(Me.txtProjectTitle)
        Me.panelStep3.Controls.Add(Me.Label12)
        Me.panelStep3.Controls.Add(Me.btnBackToStep2)
        Me.panelStep3.Controls.Add(Me.btnCompile)
        Me.panelStep3.Controls.Add(Me.Label7)
        Me.panelStep3.Location = New System.Drawing.Point(255, 35)
        Me.panelStep3.Name = "panelStep3"
        Me.panelStep3.Size = New System.Drawing.Size(650, 490)
        Me.panelStep3.TabIndex = 21
        Me.panelStep3.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.Label8.Location = New System.Drawing.Point(13, 219)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(308, 20)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "What the wizard does"
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(13, 246)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(572, 28)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "This is purely informative. This line is what you would execute in the console mo" & _
    "de."
        '
        'txtOdocs
        '
        Me.txtOdocs.Enabled = False
        Me.txtOdocs.Location = New System.Drawing.Point(16, 277)
        Me.txtOdocs.Multiline = True
        Me.txtOdocs.Name = "txtOdocs"
        Me.txtOdocs.Size = New System.Drawing.Size(608, 147)
        Me.txtOdocs.TabIndex = 23
        '
        'cboDebugMode
        '
        Me.cboDebugMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDebugMode.FormattingEnabled = True
        Me.cboDebugMode.Items.AddRange(New Object() {"No debug", "Basic debug mode", "Full debug mode", "Only debug"})
        Me.cboDebugMode.Location = New System.Drawing.Point(16, 167)
        Me.cboDebugMode.Name = "cboDebugMode"
        Me.cboDebugMode.Size = New System.Drawing.Size(176, 21)
        Me.cboDebugMode.TabIndex = 22
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.Label20.Location = New System.Drawing.Point(13, 99)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(308, 20)
        Me.Label20.TabIndex = 21
        Me.Label20.Text = "Debug mode"
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(13, 126)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(572, 28)
        Me.Label21.TabIndex = 20
        Me.Label21.Text = "You can configure the level of OrionDocs debugger. The Basic debug mode alerts yo" & _
    "u only in warnings or fatal errors. The Full debug mode will display how the com" & _
    "ment blocks are processed."
        '
        'txtProjectTitle
        '
        Me.txtProjectTitle.Location = New System.Drawing.Point(16, 64)
        Me.txtProjectTitle.Name = "txtProjectTitle"
        Me.txtProjectTitle.Size = New System.Drawing.Size(608, 20)
        Me.txtProjectTitle.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(13, 44)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(572, 28)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "This title will be displayed in the ""title bar"" of the web browser."
        '
        'btnBackToStep2
        '
        Me.btnBackToStep2.Location = New System.Drawing.Point(475, 459)
        Me.btnBackToStep2.Name = "btnBackToStep2"
        Me.btnBackToStep2.Size = New System.Drawing.Size(75, 23)
        Me.btnBackToStep2.TabIndex = 9
        Me.btnBackToStep2.Text = "Back"
        Me.btnBackToStep2.UseVisualStyleBackColor = True
        '
        'btnCompile
        '
        Me.btnCompile.Location = New System.Drawing.Point(556, 459)
        Me.btnCompile.Name = "btnCompile"
        Me.btnCompile.Size = New System.Drawing.Size(75, 23)
        Me.btnCompile.TabIndex = 8
        Me.btnCompile.Text = "Compile"
        Me.btnCompile.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.Label7.Location = New System.Drawing.Point(13, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(308, 20)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Project title"
        '
        'consoleImgList
        '
        Me.consoleImgList.ImageStream = CType(resources.GetObject("consoleImgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.consoleImgList.TransparentColor = System.Drawing.Color.Transparent
        Me.consoleImgList.Images.SetKeyName(0, "yellow")
        Me.consoleImgList.Images.SetKeyName(1, "red")
        Me.consoleImgList.Images.SetKeyName(2, "blue")
        Me.consoleImgList.Images.SetKeyName(3, "black")
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.OrionDocs.My.Resources.Resources.bgform_ff
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(908, 537)
        Me.Controls.Add(Me.panelStep2)
        Me.Controls.Add(Me.panelStep3)
        Me.Controls.Add(Me.panelStep1)
        Me.Controls.Add(Me.panelAbout)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblStep3)
        Me.Controls.Add(Me.lblStep2)
        Me.Controls.Add(Me.lblStep1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OrionDocs"
        Me.panelAbout.ResumeLayout(False)
        Me.panelAbout.PerformLayout()
        Me.panelStep1.ResumeLayout(False)
        Me.panelStep2.ResumeLayout(False)
        Me.groupInfo.ResumeLayout(False)
        Me.groupInfo.PerformLayout()
        Me.panelStep3.ResumeLayout(False)
        Me.panelStep3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents openFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents lblStep1 As System.Windows.Forms.Label
    Friend WithEvents lblStep2 As System.Windows.Forms.Label
    Friend WithEvents lblStep3 As System.Windows.Forms.Label
    Friend WithEvents panelAbout As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnToStep1 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents panelStep1 As System.Windows.Forms.Panel
    Friend WithEvents btnToStep2 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnBackToAbout As System.Windows.Forms.Button
    Friend WithEvents panelStep2 As System.Windows.Forms.Panel
    Friend WithEvents btnBackToStep1 As System.Windows.Forms.Button
    Friend WithEvents btnToStep3 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents panelStep3 As System.Windows.Forms.Panel
    Friend WithEvents btnBackToStep2 As System.Windows.Forms.Button
    Friend WithEvents btnCompile As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lvProject As System.Windows.Forms.ListView
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lvTemplate As System.Windows.Forms.ListView
    Friend WithEvents groupInfo As System.Windows.Forms.GroupBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblWeb As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblContact As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblAuthor As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtProjectTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents consoleImgList As System.Windows.Forms.ImageList
    Friend WithEvents selectFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cboDebugMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtOdocs As System.Windows.Forms.TextBox

End Class
