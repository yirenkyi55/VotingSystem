Imports System.Data.SqlClient
Public Class ReportInformation

    'Properties for Association info
    Public Property AssociationName = String.Empty

    Public Sub LoadAssocInfo()
        Dim strSearch As String = "SELECT AssociationName FROM tblAssociationInfo"
        Dim command As SqlCommand = New SqlCommand(strSearch, myConnectionString)
        If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
        Try
            Dim reader As SqlDataReader = command.ExecuteReader
            If reader.Read Then
                AssociationName = CStr(reader.Item("AssociationName"))
                reader.Close()

            End If
        Catch ex As Exception
            MessageBox.Show("Sorry an Error occured " & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            myConnectionString.Close()
        End Try

    End Sub


End Class
