Imports System.Data
Imports MySql.Data.MySqlClient

Public Class DbTransactions
    Private objConnDb As New MySql.Data.MySqlClient.MySqlConnection

    Private Sub DbConnect()
        Try
            Dim strServer As String = My.Settings.Db_Server
            Dim strUser As String = My.Settings.Db_User
            Dim strPword As String = My.Settings.Db_Paswoord
            Dim strSchema As String = My.Settings.Db_Schema

            objConnDb.ConnectionString = "server=" & strServer & ";user id=" & strUser & ";password=" & strPword & ";database=" & strSchema & ";"
            objConnDb.Open()
        Catch ex As Exception
            objConnDb.Close()
        End Try
    End Sub
    Private Sub DbDisconnect()
        Try
            objConnDb.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Function Selection(ByVal strQuery As String) As DataTable
        Try
            DbConnect()

            Dim adpSelection As New MySqlDataAdapter(strQuery, objConnDb)
            Dim dtbSelection As New DataTable
            adpSelection.Fill(dtbSelection)

            DbDisconnect()

            Return dtbSelection
        Catch ex As Exception
            DbDisconnect()
            Return Nothing
        End Try
    End Function

    Public Function Transaction(ByVal arrStatements As String()) As Exception
        Try
            DbConnect()

            Dim cmdStatement As New MySqlCommand
            Dim i As Integer

            cmdStatement.Connection = objConnDb
            For i = 0 To arrStatements.GetUpperBound(0)
                cmdStatement.CommandText = arrStatements(i)
                cmdStatement.ExecuteNonQuery()
            Next

            cmdStatement.CommandText = "commit"
            cmdStatement.ExecuteNonQuery()

            Return Nothing
        Catch ex As Exception
            Return ex
        End Try
    End Function

End Class
