Class OBlock
    Private tags As List(Of OTag)
    Private tagDict As Dictionary(Of String, List(Of OTag))

    Public Sub New()
        tags = New List(Of OTag)
        tagDict = New Dictionary(Of String, List(Of OTag))
    End Sub

    Public Sub Add(ByVal oTag As OTag)
        tags.Add(oTag)
        If Not tagDict.ContainsKey(oTag.GetName) Then tagDict.Add(oTag.GetName, New List(Of OTag))
        tagDict(oTag.GetName).Add(oTag)
    End Sub

    Public Function GetBlockType() As String
        If tags.Count > 0 Then
            Return tags(0).GetName
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

    Public Function GetTags(ByVal TagName As String) As List(Of OTag)
        If tagDict.ContainsKey(TagName) Then
            Return tagDict(TagName)
        End If
        Return New List(Of OTag)
    End Function

    Public Function GetTag(ByVal index As Integer) As OTag
        Return tags(index)
    End Function

    Public Function TagCount() As Integer
        Return tags.Count
    End Function
End Class