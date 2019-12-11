Public Class frmAdminGet

    Private strAdminName As String
    Private strAdminPassword As String
    Private strAdminCode As String
    Dim myData As New GetData
    Private Sub frmAdminGet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ds As New DataSet
        ds = myData.GetAllRecords("tblAdmin")
        dgvAdmin.DataSource = ds.Tables(0)
        If dgvAdmin.Rows.Count < 1 Then
            btnTransfer.Enabled = False
        End If
    End Sub

    'Properties
    Public ReadOnly Property AdminName
        Get
            Return strAdminName
        End Get

    End Property

    Public ReadOnly Property AdminPassword
        Get
            Return strAdminPassword
        End Get

    End Property

    Public ReadOnly Property AdminCode
        Get
            Return strAdminCode
        End Get

    End Property

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Try

            strAdminName = dgvAdmin.SelectedRows.Item(0).Cells(0).Value
            strAdminPassword = dgvAdmin.SelectedRows.Item(0).Cells(1).Value
            strAdminCode = dgvAdmin.SelectedRows.Item(0).Cells(2).Value


        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    'End of properties

End Class