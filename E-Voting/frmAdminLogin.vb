Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.XtraSplashScreen

Public Class frmAdminLogin



    Private Function isValid() As Boolean
        If txtUsername.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtUsername, "Enter Username")
            lblStatus.Text = "Enter Username"
            txtUsername.Focus()
            Return False
        End If

        If txtPassword.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtPassword, "Enter your Password")
            lblStatus.Text = "Password Required"
            txtPassword.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            txtPassword.UseSystemPasswordChar = False
        Else
            txtPassword.UseSystemPasswordChar = True
        End If
    End Sub

    '*****************************Control the appearance of the buttons *****************


    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        lblStatus.Text = String.Empty
        ErrorProvider1.Clear()
        If isValid() Then
            'Show a wait form
            SplashScreenManager.ShowForm(Me, GetType(frmWait), True, True, False, 100)

            SplashScreenManager.Default.SetWaitFormCaption("Logging in...")


            Dim strUsername As String = CStr(txtUsername.Text.Trim)
            Dim strPassword As String = CStr(txtPassword.Text.Trim)


            Dim strSearch As String = "SELECT * FROM tblAdmin WHERE UserName='" & strUsername & "'"
            Dim mySqlSearch As New SqlCommand(strSearch, myConnectionString)
            Dim mySqlDataReader As SqlDataReader

            Try
                lblStatus.Text = "Please Wait...."
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                mySqlDataReader = mySqlSearch.ExecuteReader
                'Was the Password and Username found
                If mySqlDataReader.Read Then
                    'Check whether the password also exist
                    If strPassword = mySqlDataReader.Item("Password") Then
                        'Log the user in 
                        lblStatus.Text = "Login Successful"
                        'Create the Images directory if it does not exist
                        If System.IO.Directory.Exists(strPath & "\Images") = False Then
                            MkDir(strPath & "\Images")
                        End If
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("Login Successful", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Assign components to certain variables to login.
                        mySqlDataReader.Close()
                        frmInterface.Show()
                        Me.Close()
                        Exit Sub

                    Else
                        'Incorrect Password
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("Incorrect username and/or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtUsername.Focus()
                        ErrorProvider1.SetError(txtPassword, "Incorrect username and/or password")
                        lblStatus.Text = "Incorrect username and/or password"
                        Exit Sub
                    End If
                    mySqlDataReader.Close()
                Else
                    'The username does not exist
                    SplashScreenManager.CloseForm()
                    MessageBox.Show("Incorrect username and/or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtUsername.Focus()
                    ErrorProvider1.SetError(txtUsername, "Incorrect username and/or password")
                    lblStatus.Text = "Incorrect username and/or password"
                    mySqlDataReader.Close()
                End If
                mySqlDataReader.Close()
            Catch ex As Exception
                SplashScreenManager.CloseForm()
                lblStatus.Text = ex.Message
                MessageBox.Show("Sorry An Error Occured." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                myConnectionString.Close()
            End Try
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtPassword.Clear()
        txtUsername.Clear()
        txtUsername.Focus()
        lblStatus.Text = String.Empty
        ErrorProvider1.Clear()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If MessageBox.Show("Are you sure you want to quit this application?", "Cofirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        frmConn.ShowDialog()
    End Sub

    Private Sub frmAdminLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class