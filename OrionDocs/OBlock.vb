Imports System.Text.RegularExpressions

Class OBlock
    Private tags As List(Of OTag)

    Public Sub New()
        Me.tags = New List(Of OTag)
    End Sub

    Public Sub add(ByVal oTag As OTag)
        Me.tags.Add(oTag)
    End Sub

    Public Function getBlockType() As String
        If Me.tags.Count > 0 Then
            Return Me.tags(0).getName
        Else
            Return ""
        End If
    End Function

    Public Overrides Function ToString() As String
        Dim ret As String = "Type: " + getBlockType() + vbCrLf + "Tags: {" + vbCrLf
        For Each tag As OTag In tags
            ret += tag.ToString()
        Next
        ret += "}" & vbCrLf
        Return ret
    End Function

    Public Function getTag(index) As OTag
        Return Me.tags(index)
    End Function

    Public Function Count() As Integer
        Return Me.tags.Count
    End Function
End Class