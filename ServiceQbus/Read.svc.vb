Public Class Read
    Implements IRead

    Private objDbTransaction As New FunctionDb.DbTransactions
    Private objQbusComminication As New FunctionQbus.QbusCommunication

    Public Function VwZoneList() As DataTable Implements IRead.VwZoneList
        Try
            Dim dtbVwZoneAddress As DataTable = objDbTransaction.Selection("select mo.address from module_output mo inner join output_vwzone ov on mo.module_output_id = ov.module_output_id")

            Dim dtbResult As DataTable = New DataTable("VWzone")

            Dim dtcVWzone_id As DataColumn = New DataColumn("VWzone_id", System.Type.GetType("System.String"))
            dtbResult.Columns.Add(dtcVWzone_id)
            Dim dtcProfiel As DataColumn = New DataColumn("Profiel", System.Type.GetType("System.String"))
            dtbResult.Columns.Add(dtcProfiel)
            Dim dtcGewenst As DataColumn = New DataColumn("Gewenst", System.Type.GetType("System.String"))
            dtbResult.Columns.Add(dtcGewenst)
            Dim dtcGemeten As DataColumn = New DataColumn("Gemeten", System.Type.GetType("System.String"))
            dtbResult.Columns.Add(dtcGemeten)
            Dim dtcStatus As DataColumn = New DataColumn("Status", System.Type.GetType("System.String"))
            dtbResult.Columns.Add(dtcStatus)

            objQbusComminication.QbusConnect

            dim arrVwZoneData as string()
            Dim dtrZone As DataRow

            For i=0 To dtbVwZoneAddress.rows.count
                arrVwZoneData = objQbusCommunication.VwZone_Read(cint(dtbVwZoneAddress.rows(i)(0).tostring))
                
                dtrZone = dtbResult.NewRow 
                dtrZone.Item("VWzone_id") = arrVwZoneData(0)
                dtrZone.Item("Profiel") = arrVwZoneData(1)
                dtrZone.Item("Gewenst") = arrVwZoneData(2) 
                dtrZone.Item("Gemeten") = arrVwZoneData(3) 
                dtrZone.Item("Status") = arrVwZoneData(4)
                dtbResult.Rows.Add(dtrZone) 
            Next

            objQbusComminication.QbusDisconnect

            return dtbResult
        Catch ex As Exception
            return nothing
        End Try
    End Function

End Class