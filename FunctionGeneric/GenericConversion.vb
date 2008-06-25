Public Class GenericConversion

    Public Function StringToArray(ByVal input As String, ByVal separator As String) As String()
        Dim cursor As Integer = 1
        Dim pipe As Integer = 1
        Dim counter As Integer = 0
        Dim arrResult As String() = Nothing

        Try
          if input = "" or input = nothing then  
             return nothing
          else
             While pipe <> 0
                ReDim Preserve arrResult(counter)
                pipe = InStr(cursor, input, separator)

                If pipe = 0 Then
                    arrResult(counter) = Right(input, Len(input) - cursor + 1)
                Else
                    arrResult(counter) = Mid(input, cursor, pipe - cursor)
                End If

                counter = counter + 1
                cursor = pipe + 1
            End While

            Return arrResult
          end if
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ArrayToString(ByVal input As String(), ByVal separator As String) As String
        Dim intTeller As Integer = Nothing
        Dim strReturn As String = Nothing

        Try
            If input Is Nothing Then
                Return Nothing
            Else
                For intTeller = 0 To input.GetUpperBound(0)
                    If intTeller = 0 Then
                        strReturn = input(intTeller)
                    Else
                        strReturn = strReturn & separator & input(intTeller)
                    End If
                Next

                Return strReturn
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


End Class
