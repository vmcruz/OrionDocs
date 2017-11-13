﻿Imports System.Text.RegularExpressions
Imports System.IO

Class OTag
    Private Name As String
    Private Groups As List(Of String)

    Public Sub New(ByVal name As String, ByVal groups As GroupCollection)
        Me.Name = name
        Me.Groups = New List(Of String)
        Dim theData As String = ""
        Dim isCode As Boolean = False
        For i = 0 To groups.Count - 1
            theData = groups(i).ToString().Trim()

            '#GOTO
            Dim searchGroups As Regex = New Regex("\[\s*goto:\s*(.+?)\s*\]", RegexOptions.IgnoreCase)
            Dim myMatches As MatchCollection = searchGroups.Matches(theData)

            For j = 0 To myMatches.Count - 1
                Dim methodName As String = myMatches(j).Groups(1).ToString()
                Dim properName As String = (New Regex("[^\w]+")).Replace(myMatches(j).Groups(1).ToString(), "_")
                theData = theData.Replace(
                            myMatches(j).Groups(0).ToString(),
                            OrionDocs.templateConfiguration("GOTO_REDIR").Replace("{METHOD_NAME}", methodName).Replace("{PROPERURL}", properName).Replace("{FILE_EXTENSION}", OrionDocs.templateConfiguration("FILE_EXTENSION"))
                        )
            Next

            '#CODE
            searchGroups = New Regex("\[\s*(\d+)\s*\-\s*(\d+)\s*\]", RegexOptions.IgnoreCase)
            myMatches = searchGroups.Matches(theData)

            For j = 0 To myMatches.Count - 1
                Dim startLine As Integer = Integer.Parse(myMatches(j).Groups(1).ToString())
                Dim endLine As Integer = Integer.Parse(myMatches(j).Groups(2).ToString())

                If startLine > 0 And endLine <= OrionDocs.orionDocsFileSplitted.Count() Then
                    Dim code As String = ""
                    Dim isFirstLine As Boolean = True
                    Dim indent As String = ""
                    Dim indentTmp As String = ""
                    For k = startLine To endLine
                        Dim codeLine As String = OrionDocs.orionDocsFileSplitted(k - 1).Replace(vbTab, "    ").Replace(vbCr, "").Replace(vbLf, "")
                        If codeLine.Length = charInstances(codeLine, " "c) Then
                            codeLine += "&nbsp;"
                        End If
                        If codeLine.TrimStart().Length <> 0 Then indentTmp = codeLine.Replace(codeLine.TrimStart(), "")

                        code += codeLine + vbCrLf
                        If isFirstLine Then
                            indent = indentTmp
                            isFirstLine = False
                        Else
                            If indentTmp.Length < indent.Length Then indent = indentTmp
                        End If
                    Next

                    Dim realCode As String() = code.Split(vbCrLf)
                    code = ""
                    For k = 0 To realCode.Count - 1
                        If realCode(k).Trim() <> "" Then
                            code += realCode(k).Substring(indent.Length()).Replace(vbCr, "").Replace(vbLf, "") + vbCrLf
                        End If
                    Next

                    isCode = True
                    theData = theData.Replace(
                                myMatches(j).Groups(0).ToString(),
                                code
                            )
                Else
                    Throw New System.Exception("Can't find this lines in the file: " + startLine.ToString() + " - " + endLine.ToString())
                End If
            Next
            Me.Groups.Add(theData)
        Next

        If isCode Then Me.Groups.Add(Path.GetFileName(OrionDocs.orionDocsProjectFile))
    End Sub

    Public Function getGroup(ByVal index As Integer) As String
        Return Me.Groups(index)
    End Function

    Public Function getName() As String
        Return Me.Name
    End Function

    Public Overrides Function ToString() As String
        Dim ret As String = vbTab + Me.Name + vbCrLf + vbTab + "Groups: {" + vbCrLf

        For i = 0 To Me.Groups.Count - 1
            Dim groupData As String = Me.Groups(i).ToString()
            ret += vbTab + vbTab + i.ToString() + " = " + Me.Groups(i) + vbCrLf
        Next

        ret += vbTab + "}" + vbCrLf
        Return ret
    End Function


    Public Function charInstances(ByVal str As String, ByVal c As Char) As Integer
        Dim d As String() = str.Split(c)
        Return d.Count - 1
    End Function
End Class