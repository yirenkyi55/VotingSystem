Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.XtraSplashScreen
Public Class frmVoterLogin
    Private strSystemEndTime As String = String.Empty
    Private strSystemStartTime As String = String.Empty

    Private Sub lnkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkLabel.LinkClicked
        frmCode.ShowDialog()
        If isValidCode Then
            frmAdminLogin.Show()
            Me.Close()
        End If
    End Sub


    Private Sub txtCode4_TextChanged(sender As Object, e As EventArgs)

    End Sub
    '***************** Sub Procedures and Functions ********************
    Private Function isValid() As Boolean
        If strSystemStartTime <> String.Empty Then
            Dim dtmSystemTime As DateTime = CDate(strSystemStartTime).ToShortTimeString
            Dim dtmNowTime As DateTime = CDate(TimeOfDay.ToShortTimeString)
            If dtmNowTime < dtmSystemTime Then
                MessageBox.Show("Election Time not yet Started. Contact your Administrator", "Time", MessageBoxButtons.OK, MessageBoxIcon.Information)
                lblStatus.Text = "Election Time not yet Started"
                Return False
            End If
        End If

        If strSystemEndTime <> String.Empty Then
            Dim dtmSystemTime As DateTime = CDate(strSystemEndTime)
            ' Dim dtmNowTime As DateTime = CDate(TimeOfDay.ToShortTimeString)
            If CDate(TimeOfDay.ToShortTimeString) > dtmSystemTime Then
                MessageBox.Show("Election Time Passed. Contact your Administrator", "Time", MessageBoxButtons.OK, MessageBoxIcon.Information)
                lblStatus.Text = "Election Time Passed"
                Return False
            End If
        End If
        If txtID.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtID, "Please Enter Your ID")
            lblStatus.Text = "Please Enter Your ID"
            txtID.Focus()
            Return False
        End If

        If txtPin1.Text = String.Empty Then
            ErrorProvider1.SetError(txtPin1, "Please Enter Voter PIN 1")
            lblStatus.Text = "Please Enter Voter PIN 1"
            txtPin1.Focus()
            Return False
        End If

        If txtPin2.Text = String.Empty Then
            ErrorProvider1.SetError(txtPin2, "Please Enter Voter PIN 2")
            lblStatus.Text = "Please Enter Voter PIN 2"
            txtPin2.Focus()
            Return False
        End If

        If txtPin3.Text = String.Empty Then
            ErrorProvider1.SetError(txtPin1, "Please Enter Voter PIN 3")
            lblStatus.Text = "Please Enter Voter PIN 3"
            txtPin3.Focus()
            Return False
        End If

        If txtPin4.Text = String.Empty Then
            ErrorProvider1.SetError(txtPin1, "Please Enter Voter PIN 4")
            lblStatus.Text = "Please Enter Voter PIN 4"
            txtPin4.Focus()
            Return False
        End If
        Return True
    End Function




    Sub reset()
        txtID.Text.Trim()
        txtID.Focus()
    End Sub

    Sub retrieveSystemTime()
        Dim strSelect As String = "SELECT * FROM tblAssociationInfo"
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)
        Dim mySqlDataReader As SqlDataReader = Nothing
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            mySqlDataReader = mySqlSelect.ExecuteReader
            If mySqlDataReader.Read Then
                strSystemStartTime = CStr(mySqlDataReader.Item("StartTime"))
                strSystemEndTime = CStr(mySqlDataReader.Item("EndTime"))
                mySqlDataReader.Close()
            Else
                mySqlDataReader.Close()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            myConnectionString.Close()
        End Try
    End Sub
    '*******************************************************************





    Private Sub frmVoterLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblMessage.Left = Me.Width
        Timer2.Start()
        retrieveSystemTime()
        Dim reportInfo As New ReportInformation
        reportInfo.LoadAssocInfo()
        If reportInfo.AssociationName <> String.Empty Then
            lblMessage.Text = "WELCOME TO " & reportInfo.AssociationName & " VOTING PROCESS!! ENTER YOUR VOTER ID AND PIN TO START VOTING PROCCESS"
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblTimer.Text = Now.ToString("HH:mm:ss tt")
    End Sub


    Private Sub frmVoterLogin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        isValidCode = False
    End Sub


    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
        If isValid() Then
            resetMvVariables()
            'Show a wait form
            SplashScreenManager.ShowForm(Me, GetType(frmWait), True, True, False, 100)
            SplashScreenManager.Default.SetWaitFormCaption("Logging in...")
            Dim strVoterID As String = CStr(txtID.Text.Trim)
            Dim intPinNumber As Integer = CInt(txtPin1.Text & txtPin2.Text & txtPin3.Text & txtPin4.Text)
            Dim strSearch As String = "SELECT * FROM tblVoters WHERE VoterID=@VoterID AND PinNumber=@PinNumber"
            Dim mySqlSearch As New SqlCommand(strSearch, myConnectionString)
            Dim strVerify As String = "SELECT * FROM tblElections WHERE VoterID=@VoterID"
            Dim mySqlVerify As New SqlCommand(strVerify, myConnectionString)
            mySqlVerify.Parameters.AddWithValue("@VoterID", strVoterID)
            mySqlSearch.Parameters.AddWithValue("@VoterID", strVoterID)
            mySqlSearch.Parameters.AddWithValue("@PinNumber", intPinNumber)
            Dim mySqlDataReader As SqlDataReader = Nothing
            Try
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                mySqlDataReader = mySqlSearch.ExecuteReader()
                If mySqlDataReader.Read Then
                    'Verify if a voting has already been performed
                    mySqlDataReader.Close()
                    mySqlDataReader = mySqlVerify.ExecuteReader
                    If mySqlDataReader.Read Then
                        'The user has already cast his/her vote hence notify the user
                        mySqlDataReader.Close()
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("Your Vote Has Already Been Cast" & vbCrLf & "Multiple Voting is Disallowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ErrorProvider1.SetError(txtID, "Your Vote Has Already Been Cast")
                        lblStatus.Text = "Your Vote Has Already Been Cast"
                    Else
                        'Allow the user to cast his/her vote

                        mySqlDataReader.Close()

                        mySqlDataReader = mySqlSearch.ExecuteReader
                        If mySqlDataReader.Read Then
                            mvFirstName = mySqlDataReader.Item("FirstName")
                            mvLastName = mySqlDataReader.Item("LastName")
                            If mySqlDataReader.Item("OtherName") IsNot DBNull.Value Then
                                mvOtherName = mySqlDataReader.Item("OtherName")
                            End If
                            mvVoterID = mySqlDataReader.Item("VoterID")
                            mySqlDataReader.Close()

                            'Show voting form only when the voterID is not active
                            Dim data As New GetData
                            If data.isVoterIDFound Then
                                SplashScreenManager.CloseForm()
                                MessageBox.Show("The VoterID entered is not active. A Voter is currently using this ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Else
                                SplashScreenManager.CloseForm()
                                data.ActivateVoter()
                                reset()
                                frmVote.Show()
                                Me.Close()
                            End If

                        End If


                    End If
                Else
                    'The VoterID is not a valid VoterID. Not a registered member
                    mySqlDataReader.Close()
                    SplashScreenManager.CloseForm()
                    lblStatus.Text = "You are not a valid Member. Invalid voterID and PIN Combination"
                    ErrorProvider1.SetError(txtID, "Voter ID not Found")
                    MessageBox.Show("Invalid Voter ID and PIN combination" & vbCrLf & "You are not a valid Member", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show("Sorry an Error occured " & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Try
                    SplashScreenManager.CloseForm()
                Catch ex1 As Exception

                End Try
            Finally
                myConnectionString.Close()
            End Try
        End If
    End Sub

    Private Sub txtPin1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPin4.KeyPress, txtPin3.KeyPress, txtPin2.KeyPress, txtPin1.KeyPress
        If Char.IsControl(e.KeyChar) Then Exit Sub
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPin1_TextChanged(sender As Object, e As EventArgs) Handles txtPin1.TextChanged
        If txtPin1.Text.Length = 1 Then
            txtPin2.Focus()
        End If
    End Sub

    Private Sub txtPin2_TextChanged(sender As Object, e As EventArgs) Handles txtPin2.TextChanged
        If txtPin2.Text.Length = 1 Then
            txtPin3.Focus()
        End If
    End Sub

    Private Sub txtPin3_TextChanged(sender As Object, e As EventArgs) Handles txtPin3.TextChanged
        If txtPin3.Text.Length = 1 Then
            txtPin4.Focus()
        End If
    End Sub



    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtID.Clear()
        txtPin1.Clear()
        txtPin2.Clear()
        txtPin3.Clear()
        txtPin4.Clear()
        txtID.Focus()
        lblStatus.Text = String.Empty
        ErrorProvider1.Clear()
    End Sub

    Private Sub Timer2_Tick_1(sender As Object, e As EventArgs) Handles Timer2.Tick
        lblMessage.Left -= 5
        If lblMessage.Left = Me.Left Then

            Timer2.Stop()
            lblMessage.Left = Me.Width
        End If
    End Sub
End Class