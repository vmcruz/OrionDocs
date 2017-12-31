Imports System.IO

Public Class Debugger
    Public Filename As String
    Private debug As List(Of String)
    Private errors As List(Of String)

    Public Sub New(ByVal Filename As String)
        Me.Filename = Filename
        debug = New List(Of String)
        errors = New List(Of String)
    End Sub

    Public Sub Write(ByVal Message As String)
        debug.Add(Format(Now, "hh:mm:ss.fff") + " | " + Message)
    End Sub

    Public Sub WriteError(ByVal Message As String)
        errors.Add(Format(Now, "hh:mm:ss.fff") + " | " + Message)
    End Sub

    Public Sub Save()
        If errors.Count > 0 Then
            debug.Add("    *** Warnings and errors encountered ***    ")
        End If

        If My.Settings.SaveDebug Then
            Dim fs As FileStream = Nothing
            Dim sw As StreamWriter = Nothing
            Dim fichero As String = Path.GetFileName(Filename) + ".log"
            Try
                fs = New FileStream(fichero, FileMode.Create)
                sw = New StreamWriter(fs)
                sw.Write(String.Join(vbCrLf, debug))
                If errors.Count > 0 Then sw.WriteLine(String.Join(vbCrLf, errors))
                sw.Close()
                fs.Close()
            Catch e As Exception
                Console.Write(String.Join(vbCrLf, debug))
                If errors.Count > 0 Then sw.WriteLine(String.Join(vbCrLf, errors))
                Console.Write("! System Error(6): Invalid log filename '" + fichero + "'. " + e.Message)
                Console.ReadLine()
            End Try
        Else
            Console.Write(String.Join(vbCrLf, debug))
            If errors.Count > 0 Then Console.WriteLine(String.Join(vbCrLf, errors))
        End If
    End Sub
End Class
