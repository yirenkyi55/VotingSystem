Imports System.Data
Imports System.Data.SqlClient
Public Class frmCreateAdmin
    Private mAdminId As Integer

    '********************Sub procedures and Functions *********************

    Function isValid() As Boolean
        If txtUsername.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtUsername, "Usernamem is required")
            lblStatus.Text = "Username is required"
            Return False
        End If

        If txtPassword.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtPassword, "Password is required")
            lblStatus.Text = "Password is required"
            Return False
        End If

        If txtPassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
            ErrorProvider1.SetError(txtConfirmPassword, "Password Mismatch")
            lblStatus.Text = "Password Mismatch"
            Return False
        End If
        Return True
    End Function

    Private Sub reSet()
        txtUsername.Clear()
        txtPassword.Clear()
        txtConfirmPassword.Clear()
        txtCode1.Clear()
        txtCode2.Clear()
        txtCode3.Clear()
        txtCode4.Clear()
        txtUsername.Focus()
    End Sub
    '**********************************************************************

    Private Sub txtCode1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCode4.KeyPress, txtCode3.KeyPress, txtCode2.KeyPress, txtCode1.KeyPress
        If Char.IsControl(e.KeyChar) Then Exit Sub
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmCreateAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtCode1_TextChanged(sender As Object, e As EventArgs) Handles txtCode1.TextChanged
        If txtCode1.Text.Length = 1 Then
            txtCode2.Focus()
        End If
    End Sub

    Private Sub txtCode2_TextChanged(sender As Object, e As EventArgs) Handles txtCode2.TextChanged
        If txtCode2.Text.Length = 1 Then
            txtCode3.Focus()
        End If
    End Sub

    Private Sub txtCode3_TextChanged(sender As Object, e As EventArgs) Handles txtCode3.TextChanged
        If txtCode3.Text.Length = 1 Then
            txtCode4.Focus()
        End If
    End Sub


    Private Sub chkShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkShow.CheckedChanged
        If chkShow.Checked Then
            txtPassword.UseSystemPasswordChar = False
            txtConfirmPassword.UseSystemPasswordChar = False
        Else
            txtPassword.UseSystemPasswordChar = True
            txtConfirmPassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub txtConfirmPassword_TextChanged(sender As Object, e As EventArgs) Handles txtConfirmPassword.TextChanged
        If txtPassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
            lblStatus.Text = "Password Mismatch"
        Else
            lblStatus.Text = "Password Match"
        End If
    End Sub


    Private Sub frmCreateAdmin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        isValidCode = False
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If isValid() Then

            Dim strUsername As String = CStr(txtUsername.Text.Trim)
            Dim strPassword As String = CStr(txtPassword.Text.Trim)
            Dim strCode As String = CStr(txtCode1.Text) & CStr(txtCode2.Text) & CStr(txtCode3.Text) & CStr(txtCode4.Text)
            Dim strInsert As String = "INSERT INTO tblAdmin(UserName,Password,EntryCode) VALUES(@UserName,@Password,@EntryCode)"
            Dim strUpdate As String = "UPDATE tblAdmin SET UserName=@UserName, Password=@Password, EntryCode=@EntryCode"
            Dim strSelect As String = "SELECT * FROM tblAdmin"
            Dim MysqlSearch As New SqlCommand(strSelect, myConnectionString)
            Dim mySqlUpdate As New SqlCommand(strUpdate, myConnectionString)
            Dim mySqlInsert As New SqlCommand(strInsert, myConnectionString)
            Dim mySqlDataReader As SqlDataReader = Nothing

            Try
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                mySqlDataReader = MysqlSearch.ExecuteReader
                If mySqlDataReader.Read Then
                    mySqlDataReader.Close()

                    If MessageBox.Show("Systen Administrator Already Exist." & vbCrLf & " Do you want to set new?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        'Update the records

                        mySqlDataReader.Close()
                        mySqlUpdate.Parameters.AddWithValue("@UserName", strUsername)
                        mySqlUpdate.Parameters.AddWithValue("@Password", strPassword)
                        mySqlUpdate.Parameters.AddWithValue("@EntryCode", strCode)
                        mySqlUpdate.ExecuteNonQuery()
                        MessageBox.Show("System Administrator has been successfully set", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        reSet()
                    End If

                Else

                    mySqlDataReader.Close()

                    mySqlInsert.Parameters.AddWithValue("@Username", strUsername)
                    mySqlInsert.Parameters.AddWithValue("@Password", strPassword)
                    mySqlInsert.Parameters.AddWithValue("@EntryCode", strCode)
                    mySqlInsert.ExecuteNonQuery()
                    MessageBox.Show("Admin has been set", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    reSet()

                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                myConnectionString.Close()
            End Try

        End If
    End Sub

    Private Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
        Using frm As frmAdminGet = New frmAdminGet()
            If frm.ShowDialog = DialogResult.OK Then
                txtUsername.Text = frm.AdminName
                txtPassword.Text = frm.AdminPassword
                txtConfirmPassword.Text = frm.AdminPassword
                txtCode1.Text = CStr(frm.AdminCode).Substring(0, 1)
                txtCode2.Text = CStr(frm.AdminCode).Substring(1, 1)
                txtCode3.Text = CStr(frm.AdminCode).Substring(2, 1)
                txtCode4.Text = CStr(frm.AdminCode).Substring(3, 1)
                lblStatus.Text = ""
            End If
        End Using
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reSet()
    End Sub

    Private Sub btnExit_Click_1(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class