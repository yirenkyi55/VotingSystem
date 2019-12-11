Public Class frmProgress

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ProgressBar1.Value = 100 Then
            Timer1.Enabled = False
            ProgressBar1.Value = 0

            Me.Close()
        Else
            ProgressBar1.Value += 1
            lblMessage.Text = "Loading Required Operation. Please Wait..." & ProgressBar1.Value & "%"
        End If
    End Sub

    Private Sub frmProgress_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        ProgressBar1.Value = 0
    End Sub
End Class