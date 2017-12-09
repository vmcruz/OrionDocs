Imports System.Text.RegularExpressions
Imports System.Text
Imports System.IO

Class OTemplate
    'Private tagDefinition As Dictionary(Of String, String)
    Private tagTemplate As Dictionary(Of String, String)
    Private tConfiguration As New Dictionary(Of String, Dictionary(Of String, String))
    Private appPath As String = System.AppDomain.CurrentDomain.BaseDirectory()
    Private themePath As String
    Private themeName As String

    Public Sub New(ByVal themeName As String)
        'tagDefinition = New Dictionary(Of String, String)
        tagTemplate = New Dictionary(Of String, String)
        Me.themeName = themeName

        Me.themePath = appPath + "templates\" + themeName
        Dim content As String

        Dim definition As String = themePath + "\template.otd"
        If File.Exists(definition) Then
            Dim OTD As StreamReader = New StreamReader(definition, Encoding.UTF8)
            content = OTD.ReadToEnd()
            OTD.Close()

            Dim data As String = "", key As String = ""
            Dim keyValue As String()
            Dim tmpDict As Dictionary(Of String, String)

            Dim grupos As Regex = New Regex("\#section\s*(\w+)(.+?)\#end_section", RegexOptions.Singleline + RegexOptions.IgnoreCase)
            Dim matchGrupos As MatchCollection = grupos.Matches(content)

            For i = 0 To matchGrupos.Count - 1
                key = matchGrupos(i).Groups(1).ToString().ToLower()
                data = matchGrupos(i).Groups(2).ToString()
                keyValue = data.Split(vbCrLf)
                tmpDict = New Dictionary(Of String, String)
                For j = 0 To keyValue.Count - 1
                    Dim context As String = keyValue(j).Trim()
                    If context.Length > 0 AndAlso context.Substring(0, 1) <> "#" Then
                        Dim startKey = context.IndexOf(":")
                        If startKey > 0 Then
                            Dim subKey As String = context.Substring(0, startKey).Trim()
                            If startKey + 1 < context.Length Then
                                Dim subValue As String = context.Substring(startKey + 1).Trim()

                                If tmpDict.ContainsKey(subKey) Then
                                    tmpDict(subKey) = subValue
                                Else
                                    If subKey <> "" AndAlso subValue <> "" Then tmpDict.Add(subKey, subValue)
                                End If
                            Else
                                Throw New System.Exception("Malformed template.otd configuration. Only the key was provided: '" + context + "'.")
                            End If
                        Else
                            Throw New System.Exception("Malformed template.otd configuration. The key wasn't provided: '" + context + "'.")
                        End If
                    End If
                Next

                If tConfiguration.ContainsKey(key) Then
                    tConfiguration(key) = tmpDict
                Else
                    tConfiguration.Add(key, tmpDict)
                End If
            Next

            If Not tConfiguration.ContainsKey("definition") Then
                Throw New System.Exception("Malformed template.otd configuration. The 'definition' section wasn't found.")
            Else
                Dim k As Integer
                Try
                    For k = 0 To tConfiguration("definition").Count() - 1
                        Dim regex As String = tConfiguration("definition")(tConfiguration("definition").Keys.ElementAt(k))
                        Dim testRegex As New Regex(regex)
                    Next
                Catch ex As Exception
                    Throw New System.Exception("Malformed RegEx in template.otd: " + tConfiguration("definition").Keys.ElementAt(k) + ". RegEx parser: " + ex.Message)
                End Try
            End If
        Else
            Throw New System.Exception("Main template config file 'template.otd' doesn't exist.")
        End If
    End Sub

    Public Sub LoadTemplateFiles()
        Dim files() As String = Directory.GetFiles(themePath, "*.ott", SearchOption.TopDirectoryOnly)
        Dim content As String
        For Each f In files
            If File.Exists(f) Then
                Dim OTT As StreamReader = New StreamReader(f, Encoding.UTF8)
                content = OTT.ReadToEnd()

                Dim name As String = Path.GetFileName(f).Replace(".ott", "")
                OTT.Close()

                tagTemplate.Add(name, content)
            End If
        Next f

        If Not ContainsTemplate(themeName) Then
            Throw New System.Exception("Main template file " + themeName + ".ott doesn't exist.")
        End If
    End Sub

    Public Function GetTemplateName() As String
        Return Me.themeName
    End Function

    Public Function GetTagRegex(ByVal tagName As String) As String
        If ConfigExists("definition") Then
            Return Me.tConfiguration("definition")(tagName)
        End If
        Return ""
    End Function

    Public Function GetTemplate(ByVal templateName As String) As String
        If ContainsTemplate(templateName) Then
            Return Me.tagTemplate(templateName)
        End If
        Return ""
    End Function

    Public Function ContainsDefinition(ByVal tagName As String) As Boolean
        If ConfigExists("definition") Then
            Return Me.tConfiguration("definition").ContainsKey(tagName)
        End If
        Return False
    End Function

    Public Function ContainsTemplate(ByVal tagName As String) As Boolean
        Return Me.tagTemplate.ContainsKey(tagName)
    End Function

    Public Function GetConfig(ByVal section As String, ByVal keyName As String) As String
        If ConfigExists(section) Then
            If Me.tConfiguration(section).ContainsKey(keyName) Then Return Me.tConfiguration(section)(keyName)
        End If
        Return ""
    End Function

    Public Function ConfigExists(ByVal section As String) As Boolean
        Return Me.tConfiguration.ContainsKey(section)
    End Function

    Public Function GetBlockTemplate(ByVal block As OBlock, Optional ByVal template As String = "") As String
        Dim myBlockTemplate As String = ""

        If template <> "" Then
            myBlockTemplate = GetTagTemplate(block.GetTag(0), template)
        Else
            myBlockTemplate = GetTagTemplate(block.GetTag(0))
        End If

        Dim searchTags As Regex = New Regex("{(@\w+)}")
        Dim myTags As MatchCollection = searchTags.Matches(myBlockTemplate)

        For i = 0 To myTags.Count - 1
            Dim tagReplacement As String = ""
            For j = 0 To block.GetTags(myTags(i).Groups(1).ToString()).Count() - 1
                tagReplacement += GetTagTemplate(block.GetTags(myTags(i).Groups(1).ToString())(j))
            Next
            myBlockTemplate = myBlockTemplate.Replace(
                        myTags(i).Groups(0).ToString(),
                        tagReplacement
                    )
        Next

        Return myBlockTemplate
    End Function

    Public Function GetTagTemplate(ByVal tag As OTag, Optional ByVal template As String = "") As String
        Dim myTemplate As String = ""

        If template <> "" Then
            myTemplate = template
        Else
            myTemplate = GetTemplate(tag.GetName)
        End If

        Dim searchGroups As Regex
        Dim myMatches As MatchCollection
        Dim specialRegexes = New SortedDictionary(Of String, String)
        Dim ele As KeyValuePair(Of String, String)
        Dim replaceBy As String = ""
        Dim groupValue As String = ""

        '#FUNCIONES ESPECIALES DENTRO DEL TEMPLATE
        specialRegexes.Add("groupvalue", "{(\d+)}")
        specialRegexes.Add("tolower", "{\s*tolower\(\s*(.+?)\s*\)\s*}")
        specialRegexes.Add("toupper", "{\s*toupper\(\s*(.+?)\s*\)\s*}")
        specialRegexes.Add("padleft", "{\s*padleft\(\s*(.+?)\s*,\s*(\d+)\s*,\s*(.+?)\)\s*}")
        specialRegexes.Add("padright", "{\s*padright\(\s*(.+?)\s*,\s*(\d+)\s*,\s*(.+?)\)\s*}")
        specialRegexes.Add("trim", "{\s*trim\(\s*(.+?)\s*\)\s*}")
        specialRegexes.Add("replace", "{\s*replace\(\s*(.+?)\s*,\s*(.+?)\s*,\s*(.+?)\)\s*}")
        specialRegexes.Add("substring", "{\s*substring\(\s*(.+?)\s*,\s*(.+?)\s*,\s*(.+?)\)\s*}")
        specialRegexes.Add("length", "{\s*length\(\s*(.+?)\s*\)\s*}")
        specialRegexes.Add("removeinvalid", "{\s*removeinvalid\(\s*(.+?)\s*\)\s*}")
        specialRegexes.Add("tagname", "{\s*tagname\s*}")
        specialRegexes.Add("template", "{\s*template\.(\w+?)\.(\w+?)\s*}")

        For i = 0 To specialRegexes.Count - 1
            ele = specialRegexes.ElementAt(i)
            searchGroups = New Regex(ele.Value, RegexOptions.IgnoreCase + RegexOptions.Singleline)
            myMatches = searchGroups.Matches(myTemplate)

            For j = 0 To myMatches.Count - 1
                replaceBy = ""
                groupValue = myMatches(j).Groups(1).ToString()
                Select Case ele.Key.ToString()
                    Case "groupvalue"
                        replaceBy = tag.GetGroup(
                                        Convert.ToInt32(
                                            groupValue
                                        )
                                    )
                    Case "tolower"
                        replaceBy = groupValue.ToLower()
                    Case "toupper"
                        replaceBy = groupValue.ToUpper()
                    Case "padleft"
                        replaceBy = groupValue.PadLeft(Integer.Parse(myMatches(j).Groups(2).ToString()), myMatches(j).Groups(3).ToString())
                    Case "padright"
                        replaceBy = groupValue.PadRight(Integer.Parse(myMatches(j).Groups(2).ToString()), myMatches(j).Groups(3).ToString())
                    Case "trim"
                        replaceBy = groupValue.Trim()
                    Case "replace"
                        Dim m() As String = myMatches(j).Groups(2).ToString().Split("|")
                        Dim n() As String = myMatches(j).Groups(3).ToString().Split("|")
                        Dim k As Integer = 0

                        If m.Count = n.Count Then
                            For k = 0 To m.Count - 1
                                replaceBy = groupValue.Replace(m(k), n(k))
                            Next
                        End If
                    Case "substring"
                        replaceBy = groupValue.Substring(myMatches(j).Groups(2).ToString(), myMatches(j).Groups(3).ToString())
                    Case "length"
                        replaceBy = groupValue.Length
                    Case "removeinvalid"
                        replaceBy = (New Regex("[^\w]+")).Replace(groupValue, "_")
                    Case "tagname"
                        replaceBy = tag.GetName
                    Case "template"
                        Dim section As String = myMatches(j).Groups(1).ToString()
                        Dim key As String = myMatches(j).Groups(2).ToString()
                        If section <> "" AndAlso key <> "" AndAlso tConfiguration.ContainsKey(section) Then
                            Dim t As Dictionary(Of String, String) = tConfiguration(section)
                            If t.ContainsKey(key) Then
                                replaceBy = t(key)
                            End If
                        End If
                End Select

                myTemplate = myTemplate.Replace(
                            myMatches(j).Groups(0).ToString(),
                            replaceBy
                        )
            Next
        Next
        Return myTemplate
    End Function
End Class