Imports System.Speech.Synthesis
Public Class frmThanks
    Private mySysnthesizer As New SpeechSynthesizer
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ProgressBar1.Value = 100 Then
            Timer1.Enabled = False
            ProgressBar1.Value = 0
            frmVoterLogin.Show()
            Me.Close()
        Else
            ProgressBar1.Value += 1
        End If
    End Sub

    Private Sub frmThanks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mySysnthesizer.SelectVoiceByHints(VoiceGender.Female)
        mySysnthesizer.SpeakAsync("All your votes were successfully casted. Thanks for your vote")
    End Sub

    Private Sub frmThanks_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        mySysnthesizer.Dispose()
    End Sub
End Class