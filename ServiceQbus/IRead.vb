Imports System.ServiceModel

<ServiceContract()> _
Public Interface IRead

    <OperationContract()> _
    Function VwZoneList(Optional ByRef dtbList As DataTable = Nothing) As DataTable

    <OperationContract()> _
    Function VwZoneDetail(ByVal intVwZoneId as integer) As String()

End Interface
