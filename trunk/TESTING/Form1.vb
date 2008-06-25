Public Class Form1




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim objTest As ServiceReference.IRead
        'Dim strTest As String()

        'objTest = New ServiceReference.ReadClient
        'strTest = objTest.VwZoneList

        'MsgBox(strTest(0))

        Dim objDbFunction As New FunctionDb.DbQueryBuilder


        MsgBox(objDbFunction.QueryBuilder_Selection("module.module_id|module_output.module_output_id"))
    End Sub
End Class
