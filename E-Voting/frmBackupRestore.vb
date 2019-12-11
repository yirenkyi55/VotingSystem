Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports System.Data.SqlClient
Imports DevExpress.XtraSplashScreen

Public Class frmBackupRestore
    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        SaveFileDialog1.FileName = txtDatabase.Text.Trim & ".bak"
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Dim connection As SqlConnection = New SqlConnection("Data Source = .; Integrated Security = True; Database = Master")
        Try

            If connection.State = ConnectionState.Closed Then connection.Open()
            SplashScreenManager.ShowForm(Me, GetType(frmWait), True, True, False, 100)

            SplashScreenManager.Default.SetWaitFormCaption("Backing Up Dabase...")

            Dim strBackup As String = "BACKUP DATABASE " & txtDatabase.Text.Trim & " TO DISK='" & SaveFileDialog1.FileName & "'"
            Dim command As New SqlCommand(strBackup, connection)
            If command.ExecuteNonQuery Then
                SplashScreenManager.CloseForm()
                MessageBox.Show("Backup successfully performed", "Backup")
            End If

        Catch ex As Exception
            Try
                SplashScreenManager.CloseForm()
            Catch ex1 As Exception

            End Try

            MessageBox.Show(ex.Message, "Error")
        Finally

            connection.Close()
        End Try


    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        Dim connection As SqlConnection = New SqlConnection("Data Source = .; Integrated Security = True; Database = Master")
        Try



            With OpenFileDialog1
                .Filter = "Database(.bak)|*.bak"
                .FileName = ""
                If .ShowDialog = DialogResult.OK And .FileName IsNot Nothing Then
                    SplashScreenManager.ShowForm(Me, GetType(frmWait), True, True, False, 100)
                    SplashScreenManager.Default.SetWaitFormCaption("Restoring Dabase...")
                    If connection.State = ConnectionState.Closed Then connection.Open()

                    Dim strRestore As String = "DROP DATABASE " & txtDatabase.Text.Trim & "; RESTORE DATABASE " & txtDatabase.Text.Trim & " FROM DISK='" & .FileName & "'"
                    Dim command As New SqlCommand(strRestore, connection)

                    If command.ExecuteNonQuery Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("Restore successfully performed", "Backup")
                    End If
                End If
            End With


        Catch ex As Exception
            Try
                SplashScreenManager.CloseForm()
            Catch ex1 As Exception

            End Try

            MessageBox.Show(ex.Message, "Error")
        Finally

            connection.Close()
        End Try
    End Sub

    Private Sub frmBackupRestore_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        isValidCode = False
    End Sub
End Class