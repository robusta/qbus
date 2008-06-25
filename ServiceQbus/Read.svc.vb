Public Class Read
    Implements IRead

    Dim objDbTransaction As New FunctionDb.DbTransactions
    Dim objQbusComminication As New FunctionQbus.QbusCommunication

    Public Function VwZoneList(Optional ByRef dtbList As DataTable = Nothing) As DataTable Implements IRead.VwZoneList
        Try
            Dim dtbVwZoneAddress As DataTable
            If dtbList Is Nothing Then
                dtbVwZoneAddress = objDbTransaction.Selection("select mo.address from module_output mo inner join output_vwzone ov on mo.module_output_id = ov.module_output_id")
            Else
                dtbVwZoneAddress = dtbList
            End If

            Dim arrVwZones(dtbVwZoneAddress.Rows.Count) As Integer
            For i = 0 To dtbVwZoneAddress.Rows.Count
                arrVwZones(i) = dtbVwZoneAddress.Rows(i)(0).ToString
            Next

            Return objQbusComminication.VWzone_Read(arrVwZones)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function VwZoneDetail(ByVal intVwZoneId As integer) As String() Implements IRead.VwZoneDetail
        'functie om alle details VWzone door te geven

        Return Nothing
    End Function

End Class
