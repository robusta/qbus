Public Class DbFunction

    Dim objDbTransaction As New DbTransactions

    Public Function NewId(ByRef strSysObject As String) As String
        Try
            Dim dtbLastId As DataTable = objDbTransaction.Selection("SELECT last_id FROM sysobject WHERE naam = '" & strSysObject & "'")
            Dim strLastId As String = dtbLastId.Rows(0)(0).ToString

            Dim arrUpdate As String()
            ReDim arrUpdate(0)
            arrUpdate(0) = "UPDATE sysobject SET last_id = " & CInt(strLastId) + 1 & " WHERE naam = '" & strSysObject & "'"
            objDbTransaction.Transaction(arrUpdate)

            Return CStr(CInt(strLastId) + 1)

            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
