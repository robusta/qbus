

Public Class QbusTools
    Dim objDbTransaction As New FunctionDb.DbTransactions

    Public Function QbusCode(ByVal strType As String, ByVal strCode As String) As String
        Dim strQuery As String
        Dim dtbResult As DataTable

        Try
            strQuery = "SELECT vertaling FROM qbus_code WHERE codetype = '" & strType & "' AND code = '" & strCode & "'"
            dtbResult = objDbTransaction.Selection(strQuery)

            Return dtbResult.Rows(0)(0).ToString
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
