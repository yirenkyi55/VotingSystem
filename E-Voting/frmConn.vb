Public Class frmConn
    Private Sub frmConnect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboServer.Properties.Items.Add("(local)")
        cboServer.Properties.Items.Add(".")
        cboServer.Properties.Items.Add(".\SQLEXPRESS")
        cboServer.Properties.Items.Add(String.Format("{0}", Environment.MachineName))
        cboServer.SelectedIndex = 3
    End Sub


    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Try
            Dim connectionString As String
            If chkSqlServer.Checked Then
                connectionString = String.Format("Data Source={0};Initial Catalog={1};User Id={2}; Password={3};", cboServer.Text.Trim, txtDatabase.Text.Trim, txtUsername.Text.Trim, txtPassword.Text.Trim)


            Else
                connectionString = String.Format("Data Source={0};Initial Catalog={1};Integrated security=true;", cboServer.Text.Trim, txtDatabase.Text.Trim)

            End If
            Dim helper As sqlHelper = New sqlHelper(connectionString)
            If helper.IsConnection Then

                MessageBox.Show("Test Connection Succeeded")
            Else
                MessageBox.Show("Unable to Connect")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Dim connectionString As String
            If chkSqlServer.Checked Then
                connectionString = String.Format("Data Source={0};Initial Catalog={1};User Id={2}; Password={3};", cboServer.Text.Trim, txtDatabase.Text.Trim, txtUsername.Text.Trim, txtPassword.Text.Trim)


            Else
                connectionString = String.Format("Data Source={0};Initial Catalog={1};Integrated security=true;", cboServer.Text.Trim, txtDatabase.Text.Trim)

            End If
            Dim helper As sqlHelper = New sqlHelper(connectionString)
            If helper.IsConnection Then
                Dim setting As AppSettings = New AppSettings()
                setting.SaveConnectionString("E_VotingCstr", connectionString)
                MessageBox.Show("Your Connection Has Been Successfully Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Unable to Connect")
            End If
        Catch ex As Exception
            MessageBox.Show("Sorry an Error Occured." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub chkSqlServer_CheckedChanged(sender As Object, e As EventArgs) Handles chkSqlServer.CheckedChanged
        If chkSqlServer.Checked Then
            txtUsername.Enabled = True
            txtPassword.Enabled = True

        Else
            txtUsername.Enabled = False
            txtPassword.Enabled = False
        End If
    End Sub
End Class