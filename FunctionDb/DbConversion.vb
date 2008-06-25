Public Class DbConversion

    Private Shared objDbTransaction As FunctionDb.DbTransactions

    Public Function DbToApp(ByVal intSysAttribute As integer, ByVal objDbValue As object) As String
        Dim dtbSysAttribute, dtbSysEnumeration As DataTable

        Try
            dim strResult as string = objDbValue

            If strResult = Nothing Or strResult = "" Then
                Return strResult
            Else
                dtbSysAttribute = objDbTransaction.Selection("SELECT datatype, enumtype, display FROM sysattribute WHERE sysattribute_id = " & intSysAttribute)

                Select Case dtbSysAttribute.Rows(0)(0).ToString
                    Case "keyid", "refid", "integer", "string", "date", "timestamp"
                        strResult = strResult
                    Case "time"
                        strResult = Hour(strResult) & ":" & Minute(strResult)
                    Case "double"
                        strResult = Format(CDbl(strResult), "###0.00")
                    Case "enum"
                        dtbSysEnumeration = objDbTransaction.Selection("SELECT sysenumeration_id FROM sysenumeration WHERE enumtype = '" & dtbSysAttribute.Rows(0)(1).ToString & "' AND naam = '" & strResult & "'")
                        strResult = dtbSysEnumeration.Rows(0)(0).ToString
                    Case "boolean"
                        If strResult = 1 Then
                            strResult = "True"
                        Else
                            strResult = "False"
                        End If

                    Case Else
                        'Throw New Exception("Functie ondersteunt datatype '" & datatype & "' niet")
                End Select

                'If display <> Nothing Then
                'Select Case display
                '   Case "euro"
                'strResult = strResult & " €"
                '   Case "percentage"
                'strResult = strResult & "%"
                '    Case "cm"
                'strResult = strResult & " cm"

                '   Case Else
                'Throw New Exception("Functie ondersteunt display '" & display & "' niet")
                'End Select
            End If

            Return strResult
            'End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function DbConvertToDb(ByVal datatype As String, ByVal display As String, ByVal input As String) As String
        Dim strConversion As String

        Try
            If input = "" Or input = Nothing Then
                Return "null"
            Else
                strConversion = input

                If display <> Nothing Then
                    Select Case display
                        Case "euro"
                            strConversion = Left(strConversion, Len(strConversion) - 2)
                        Case "percentage"
                            strConversion = Left(strConversion, Len(strConversion) - 1)
                        Case "cm"
                            strConversion = Left(strConversion, Len(strConversion) - 3)

                        Case Else
                            Throw New Exception("Functie ondersteunt display '" & display & "' niet")
                    End Select
                End If

                Select Case datatype
                    Case "keyid", "refid", "integer", "timestamp", "enum"
                        strConversion = strConversion
                    Case "string"
                        strConversion = "'" & strConversion & "'"
                    Case "double"
                        strConversion = CStr(Replace(strConversion, ",", "."))
                    Case "date"
                        strConversion = "'" & DateAndTime.Year(strConversion) & "-" & DateAndTime.Month(strConversion) & "-" & DateAndTime.Day(strConversion) & "'"
                    Case "time"
                        strConversion = "'" & DateAndTime.Hour(strConversion) & ":" & DateAndTime.Minute(strConversion) & ":" & DateAndTime.Second(strConversion) & "'"
                    Case "boolean"
                        If strConversion = "1" Or strConversion = "True" Then
                            strConversion = "True"
                        Else
                            strConversion = "False"
                        End If

                    Case Else
                        Throw New Exception("Functie ondersteunt datatype '" & datatype & "' niet")
                End Select

                Return strConversion
            End If
        Catch ex As Exception
            'LogError("modDbConversion - DbConvertToDb", ex)
            Return "null"
        End Try
    End Function


End Class
