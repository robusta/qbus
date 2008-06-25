Public Class DbQueryBuilder
    Private Shared objDbTransaction As New DbTransactions
    Private Shared objGenericConversion As New FunctionGeneric.GenericConversion

    Public Function QueryBuilder_Selection(ByVal strTablesColumns As String, _
                                        Optional ByVal strWhere As String = Nothing, _
                                        Optional ByVal strGroup As String = Nothing, _
                                        Optional ByVal strOrder As String = Nothing) As String
        Try
            Dim arrTables As String() = Nothing

            Dim strResult As String = "SELECT " & QueryBuilder_Selection_Fields(strTablesColumns, arrTables) & " FROM " & QueryBuilder_Selection_From(arrTables)

            'TODO - builders maken voor WHERE, GROUP and ORDER
            'If arrWhere <> Nothing Then strResult = strResult & " WHERE " & arrWhere
            'If arrGroup <> Nothing Then strResult = strResult & " GROUP BY " & arrGroup
            'If arrOrder <> Nothing Then strResult = strResult & "  ORDER BY " & arrOrder

            Return strResult
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function QueryBuilder_Selection_Fields(ByVal strTablesColumns As String, ByRef arrTables As String()) As String
        Try
            Dim arrTableField As String()
            Dim intFound As Integer = 1
            Dim blnFound As Boolean = False
            Dim strResult As String = Nothing
            Dim arrColumns As String() = objGenericConversion.StringToArray(strTablesColumns, "|")

            'For i = 0 To arrColumns.GetUpperBound(0)
            'arrTableField = objGenericConversion.StringToArray(arrFields(i), ".")

            'If i = 0 Then
            'strResult = arrTableField(0) & "." & arrTableField(1)

            '   ReDim arrSysObject(0)
            '  arrSysObject(0) = arrTableField(0)
            'Else
            'strResult = strResult & ", " & arrTableField(0) & "." & arrTableField(1)

            ' For j = 0 To arrSysObject.GetUpperBound(0)
            'If arrTableField(0) = arrSysObject(j) Then blnFound = True
            '       Next
            'If blnFound = False Then
            'ReDim Preserve arrSysObject(intFound)
            'arrSysObject(intFound) = arrTableField(0)
            'intFound = intFound + 1
            'Else
            'blnFound = False
            'End If
            'End If
            'Next

            Return strResult
        Catch ex As Exception
            'arrSysObject = Nothing
            Return Nothing
        End Try
    End Function

    Private Function QueryBuilder_Selection_From(ByVal arrTablesOrig As String()) As String
        Try
            Dim dtbDbSelection As DataTable

            Dim arrStructure As String(,)
            Dim arrLinkJoin, arrLinkUsed, arrTablesPath As String()

            Dim blnExtend As Boolean = True
            Dim blnMatch As Boolean = False
            Dim intLinkFound As Integer = 0
            Dim intStructureCount = 0

            If arrTablesOrig.GetUpperBound(0) = 0 Then                                                                                 'selectie uit 1 tabel is FROM de tabel
                Return arrTablesOrig(0)
            Else                                                                                                                                                         'meerdere tabellen moeten gejoind worden in FROM
                dtbDbSelection = objDbTransaction.Selection("SELECT * FROM sysrelation WHERE from_sysobject IN ('" & objGenericConversion.ArrayToString(arrTablesOrig, "', '") & "') AND to_sysobject IN ('" & objGenericConversion.ArrayToString(arrTablesOrig, "', '") & "')")
                If dtbDbSelection.Rows.Count = arrTablesOrig.Length - 1 Then                                              'als er 1 link minder gevonden wordt dan er tabellen zijn, dan is dit voldoende
                    ReDim arrLinkJoin(dtbDbSelection.Rows.Count - 1)
                    For i = 0 To dtbDbSelection.Rows.Count - 1
                        arrLinkJoin(i) = dtbDbSelection.Rows(i)(0).ToString
                    Next
                Else
                    arrLinkUsed = Nothing
                    arrTablesPath = Nothing

                    If Not dtbDbSelection Is Nothing Then                                                                                     'als er links te weinig zijn, dan toch alle gevonden links gebruiken
                        For i = 0 To dtbDbSelection.Rows.Count
                            ReDim Preserve arrLinkUsed(i)
                            ReDim Preserve arrTablesPath(i)

                            arrLinkUsed(i) = dtbDbSelection.Rows(i)(0).ToString
                            arrTablesPath(i) = dtbDbSelection.Rows(i)(2).ToString
                        Next
                    End If
                    While blnExtend = True And blnMatch = False                                                                      'loop tot er een connectie gevonden is tussen de tabellen
                        If arrLinkUsed Is Nothing Then
                            dtbDbSelection = objDbTransaction.Selection("SELECT * FROM sysrelation WHERE from_sysobject IN ('" & objGenericConversion.ArrayToString(arrTablesPath, "', '") & "')")
                        Else
                            dtbDbSelection = objDbTransaction.Selection("SELECT * FROM sysrelation WHERE from_sysobject IN ('" & objGenericConversion.ArrayToString(arrTablesPath, "', '") & "') AND sysrelation_id NOT IN (" & objGenericConversion.ArrayToString(arrLinkUsed, ", ") & ")")
                        End If
                        ReDim Preserve arrStructure(arrStructure.GetUpperBound(0) + dtbDbSelection.Rows.Count, 3)
                        For i = 0 To dtbDbSelection.Rows.Count
                            arrStructure(intStructureCount + i, 0) = dtbDbSelection.Rows(i)(0).ToString
                            arrStructure(intStructureCount + i, 1) = dtbDbSelection.Rows(i)(2).ToString
                            arrStructure(intStructureCount + i, 2) = dtbDbSelection.Rows(i)(4).ToString
                        Next
                        intStructureCount = intStructureCount + dtbDbSelection.Rows.Count

                        If arrLinkUsed Is Nothing Then
                            dtbDbSelection = objDbTransaction.Selection("SELECT * FROM sysrelation WHERE to_sysobject IN ('" & objGenericConversion.ArrayToString(arrTablesPath, "', '") & "')")
                        Else
                            dtbDbSelection = objDbTransaction.Selection("SELECT * FROM sysrelation WHERE to_sysobject IN ('" & objGenericConversion.ArrayToString(arrTablesPath, "', '") & "') AND sysrelation_id NOT IN (" & objGenericConversion.ArrayToString(arrLinkUsed, ", ") & ")")
                        End If
                        ReDim Preserve arrStructure(arrStructure.GetUpperBound(0) + dtbDbSelection.Rows.Count, 3)
                        For i = 0 To dtbDbSelection.Rows.Count
                            arrStructure(intStructureCount + i, 0) = dtbDbSelection.Rows(i)(0).ToString
                            arrStructure(intStructureCount + i, 1) = dtbDbSelection.Rows(i)(4).ToString
                            arrStructure(intStructureCount + i, 2) = dtbDbSelection.Rows(i)(2).ToString
                        Next
                        intStructureCount = intStructureCount + dtbDbSelection.Rows.Count

                        If intLinkFound = intStructureCount Then
                            blnExtend = False
                            blnMatch = False
                        Else
                            For i = intLinkFound To intStructureCount
                                For j = 0 To intLinkFound
                                    If arrStructure(i, 1) = arrStructure(j, 2) Then arrStructure(i, 3) = arrStructure(j, 0)
                                Next
                            Next

                            For i = intLinkFound To intStructureCount
                                ReDim Preserve arrLinkUsed(arrLinkUsed.GetUpperBound(0) + 1)
                                ReDim Preserve arrTablesPath(arrTablesPath.GetUpperBound(0) + 1)

                                arrLinkUsed(arrLinkUsed.GetUpperBound(0)) = arrStructure(i, 0)
                                arrTablesPath(arrTablesPath.GetUpperBound(0)) = arrStructure(i, 2)
                            Next

                            For i = 0 To arrStructure.GetUpperBound(0)
                                For j = 0 To arrStructure.GetUpperBound(0)
                                    If j <> i Then
                                        If arrStructure(j, 2) = arrStructure(i, 2) Then
                                            blnMatch = True
                                            'hier nog de gebruikte joinlinks invullen 
                                        End If
                                    End If
                                Next
                            Next
                            intLinkFound = intStructureCount
                        End If
                    End While
                End If

                'de te joinen links overlopen en verwerken   
            End If

            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function QueryBuilder_Selection_From_Join(ByVal arrLinks As String(,)) As String
        Dim strResult As String

        strResult = arrLinks(0, 2)

        For i = 0 To arrLinks.GetUpperBound(0)
            Select Case arrLinks(i, 6)
                Case "one"
                    strResult = strResult & " JOIN " & arrLinks(i, 4) & " ON " & arrLinks(i, 2) & "." & arrLinks(i, 3) & " = " & arrLinks(i, 4) & "." & arrLinks(i, 5)
                Case "link"
                    strResult = strResult & " JOIN " & arrLinks(i, 7) & " ON " & arrLinks(i, 2) & "." & arrLinks(i, 3) & " = " & arrLinks(i, 7) & "." & arrLinks(i, 3) & " JOIN " & arrLinks(i, 4) & " ON " & arrLinks(i, 7) & "." & arrLinks(i, 5) & " = " & arrLinks(i, 4) & "." & arrLinks(i, 5)
                Case Else
                    MsgBox("FOUT: linktype '" & arrLinks(0, 6) & "' is nog niet ondersteund!!!!")
            End Select
        Next

        Return strResult
    End Function



    Public Sub InsertBuilder(ByRef arrStatements As String())
        Dim i, newID As Integer
        Dim strFields As String = ""
        Dim strValues As String = ""
        Dim arrayFields As String(,) = Nothing

        Try
            'DbReadSetSQL("SA.naam,SA.datatype,SA.display", "sysobject SO JOIN sysattribute SA ON SO.sysobject_id = SA.sysobject_id", "SO.naam = '" & sysobject & "'", "SA.sequence", arrayFields)

            For i = 0 To arrayFields.GetUpperBound(0)
                strFields = strFields & arrayFields(i, 0) & ", "

                Select Case arrayFields(i, 1)
                    Case "keyid"
                        '    newID = DbNewID(sysobject)
                        strValues = strValues & newID & ", "
                    Case "refid"
                        If arrayFields(i, 0) = "user_id" Then
                            ' strValues = strValues & frmMain.user_id & ", "
                        Else
                            '       strValues = strValues & ConvertToDb(arrayFields(i, 1), arrayFields(i, 2), arrayData(i)) & ", "
                        End If
                    Case "enum"
                        ' strValues = strValues & DbReadEnumID(Left(arrayFields(i, 0), Len(arrayFields(i, 0)) - 5), arrayData(i)) & ", "
                    Case "timestamp"
                        If arrayFields(i, 0) = "timestamp" Then
                            '      strValues = strValues & DbCurrentTimestamp() & ", "
                        Else
                            '     strValues = strValues & ConvertToDb(arrayFields(i, 1), arrayFields(i, 2), arrayData(i)) & ", "
                        End If
                    Case Else
                        '            strValues = strValues & ConvertToDb(arrayFields(i, 1), arrayFields(i, 2), arrayData(i)) & ", "
                End Select
            Next

            'DbInsertSQL(sysobject, Left(strFields, Len(strFields) - 2), Left(strValues, Len(strValues) - 2))

            'Return newID
        Catch ex As Exception
            'LogError("modDbAction - DbInsertRow", ex)
            'Return Nothing
        End Try
    End Sub

End Class
