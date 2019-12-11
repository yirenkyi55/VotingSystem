Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Public Class AppSettings
    Dim config As Configuration

    Public Sub New()
        config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

    End Sub

    Public Function getConnectionSring(ByVal key As String) As String
        Return config.ConnectionStrings.ConnectionStrings(key).ConnectionString
    End Function

    Public Sub SaveConnectionString(ByVal key As String, value As String)
        config.ConnectionStrings.ConnectionStrings(key).ConnectionString = value
        config.ConnectionStrings.ConnectionStrings(key).ProviderName = "System.Data.SqlClient"
        config.Save(ConfigurationSaveMode.Modified)
    End Sub
End Class
