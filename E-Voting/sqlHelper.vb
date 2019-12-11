Imports System.Data
Imports System.Data.SqlClient
Public Class sqlHelper
    Dim cn As SqlConnection

    'Creating a constructor
    Public Sub New(ByVal connectionString As String)
        cn = New SqlConnection(connectionString)
    End Sub

    Public Property IsConnection As Boolean
        Get
            If cn.State = ConnectionState.Closed Then
                cn.Open()

            End If
            Return True
        End Get
        Set

        End Set
    End Property


End Class
