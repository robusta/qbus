Public Class QbusCommunication
    Private Shared objConnQbus As Object
    Private Shared objQbusTools As New QbusTools

    Private Function QbusConnect() As Boolean
        Dim bytResult As Byte

        Try
            objConnQbus = CreateObject("qbuscom.serial")
            bytResult = objConnQbus.OpenTCP(My.Settings.Qbus_Ip, My.Settings.Qbus_Port)

            If bytResult = 0 Then           'controle van de return van de Qbus DLL
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            QbusDisconnect()
            Return False
        End Try
    End Function
    Private Sub QbusDisconnect()
        objConnQbus.CloseTCP()
    End Sub

    Public Function VWzone_Read(ByVal arrVWaddress() As Integer) As DataTable
        'Functie gaat voor een array van zones de gegevens ophalen
        'INPUT:	array	VWzone_id
        'OUTPUT:	datatable	VWzone_id, profiel, temp_gewenst, temp_gemeten, heating

        Try
            Dim strData As String = ""
            Dim dtbResult As DataTable = New DataTable("VWzone")
            Dim dtrZone As DataRow

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
            
            QbusConnect()

            For i = 0 To arrVWaddress.GetUpperBound(0)
                objConnQbus.ctl_online("R", CByte(arrVWaddress(i)), "TH", strData, 4)

                dtrZone = dtbResult.NewRow
                dtrZone.Item("VWzone_id") = arrVWaddress(i).ToString
                dtrZone.Item("Profiel") = objQbusTools.QbusCode("TempProfiel", Asc(Mid(strData, 2, 1)))
                dtrZone.Item("Gewenst") = Asc(Mid(strData, 1, 1)) / 2 & " °"
                dtrZone.Item("Gemeten") = Asc(Mid(strData, 3, 1)) / 2 & " °"
                dtrZone.Item("Status") = objQbusTools.QbusCode("TempStatus", Asc(Mid(strData, 4, 1)))
                dtbResult.Rows.Add(dtrZone)
            Next
            QbusDisconnect()

            Return dtbResult
        Catch ex As Exception
            QbusDisconnect()
            Return Nothing
        End Try
    End Function

End Class
