' a class which implements IService1 (see Service1), 
' and configuration entries that specify behaviors associated with 
' that implementation (see <system.serviceModel> in web.config)
Public Class Service1
    Implements IService1

    Public Sub New()
    End Sub

    Public Function GetData(ByVal intParam As Integer) As String Implements IService1.GetData
        Return String.Format("You entered: {0}", intParam)
    End Function

    Public Function GetDataUsingDataContract(ByVal composite As CompositeType) As CompositeType Implements IService1.GetDataUsingDataContract
        Return composite
    End Function


End Class




