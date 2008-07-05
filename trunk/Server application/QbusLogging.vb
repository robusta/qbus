Public Class QbusLogging
    Dim objDbTransaction As New FunctionDb.DbTransactions
    Dim svcQbusRead As New ServiceQbus.Read

    Private Sub QbusLogging()
        Try
            Dim dtbVwData As DataTable = svcQbusRead.VwZoneList()

            Dim arrInserts As String()

            ReDim arrInserts(dtbVwData.Rows.Count)

            For i = 0 To dtbVwData.Rows.Count
                arrInserts(i) = "INSERT INTO logging_vwzone (logging_vwzone_id, output_vwzone_id, datum, tijd, profiel, temp_gewenst, temp_gemeten, status) values (" & 
            Next
        Catch ex As Exception

        End Try
    end sub
end class
    