Public Class QbusCommunication
    private objConnQbus As Object
    Private objQbusTools As New QbusTools

    Public sub QbusConnect()
        Try
            Dim bytResult As Byte

            objConnQbus = CreateObject("qbuscom.serial")
            bytResult = objConnQbus.OpenTCP(My.Settings.Qbus_Ip, My.Settings.Qbus_Port)

            If bytResult <> 0 Then           'controle van de return van de Qbus DLL
                msgbox("nog een exceptie werpen")
            End If
        Catch ex As Exception
            QbusDisconnect()
            Return False
        End Try
    End Function
    Public Sub QbusDisconnect()
        objConnQbus.CloseTCP()
    End Sub

    Public Function VwZone_Read(ByVal intVwZoneAddress as integer) As string()
        'Functie gaat de verwarminggegevens ophalen voor een bepaald adres
        'INPUT:	integer       	VWzone_id
        'OUTPUT:	array strings 	VWzone_id, profiel, temp_gewenst, temp_gemeten, status

        Try
            Dim strQbusData As String = ""
            Dim arrResult(4) as String

            objConnQbus.ctl_online("R", CByte(intVwZoneAddress), "TH", strData, 4)

            arrResult(0) = intVwZoneAddress
            arrResult(1) = objQbusTools.QbusCode("TempProfiel", Asc(Mid(strQbusData, 2, 1)))
            arrResult(2) = Asc(Mid(strQbusData, 1, 1)) / 2 & " °"
            arrResult(3) = Asc(Mid(strQbusData, 3, 1)) / 2 & " °"
            arrResult(4) = objQbusTools.QbusCode("TempStatus", Asc(Mid(strQbusData, 4, 1)))

            Return arrResult
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
