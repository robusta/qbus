Public Class QbusLogging

    Dim objDbTransaction As New FunctionDb.DbTransactions
    Dim objDbFunction As New FunctionDb.DbFunction
    Dim svcQbusRead As New ServiceQbus.Read

    Private Sub InitialiseHomeLogging()

    End Sub



    Private Sub QbusLogging()
        Try
            Dim dtbVwList As DataTable = objDbTransaction.Selection("select mo.address from module_output mo inner join output_vwzone ov on mo.module_output_id = ov.module_output_id")
            Dim dtbVwData As DataTable = svcQbusRead.VwZoneList(dtbVwList)

            Dim arrInserts As String()

            ReDim arrInserts(dtbVwList.Rows.Count)

            For i = 0 To dtbVwList.Rows.Count
                arrInserts(i) = "INSERT INTO logging_vwzone (logging_vwzone_id, output_vwzone_id, datum, tijd, profiel, temp_gewenst, temp_gemeten, status) values (" & 
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class
