Imports System.Text.RegularExpressions

Class OBlock
    Private tags As List(Of OTag)

    Public Sub New()
        Me.tags = New List(Of OTag)
    End Sub

    Public Sub Add(ByVal oTag As OTag)
        Me.tags.Add(oTag)
    End Sub

    Public Function GetBlockType() As String
        If Me.tags.Count > 0 Then
            Return Me.tags(0).GetName
        Else
            Return ""
        End If
    End Function

    Public Overrides Function ToString() As String
        Dim ret As String = "Type: " + GetBlockType() + vbCrLf + "Tags: {" + vbCrLf
        For Each tag As OTag In tags
            ret += tag.ToString()
        Next
        ret += "}" & vbCrLf
        Return ret
    End Function

    Public Function GetTag(index) As OTag
        Return Me.tags(index)
    End Function

    Public Function Count() As Integer
        Return Me.tags.Count
    End Function

    Public Function ContainsTag(ByVal TagName As String) As Integer
        For j = 0 To Me.Count() - 1
            If Me.GetTag(j).GetName = TagName Then
                Return j
            End If
        Next
        Return -1
    End Function
End Class