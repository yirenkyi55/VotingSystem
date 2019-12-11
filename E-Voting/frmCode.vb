Imports System.Data
Imports System.Data.SqlClient
Public Class frmCode

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub


    Sub reSet()
        ErrorProvider1.Clear()
        txtCode1.Clear()
        txtCode2.Clear()
        txtCode3.Clear()
        txtCode4.Clear()
        txtCode1.Focus()
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


    Private Function isValid() As Boolean
        If txtCode1.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtCode1, "Enter code")
            txtCode1.Focus()
            Return False
        End If

        If txtCode2.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtCode2, "Enter code")
            txtCode2.Focus()
            Return False
        End If

        If txtCode3.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtCode3, "Enter code")
            txtCode3.Focus()
            Return False
        End If

        If txtCode4.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtCode4, "Enter code")
            txtCode4.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub txtCode1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCode4.KeyPress, txtCode3.KeyPress, txtCode2.KeyPress, txtCode1.KeyPress
        If Char.IsControl(e.KeyChar) Then Exit Sub
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub



    '*****************************Control the appearance of the buttons *****************


    Private Sub frmCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reSet()
    End Sub

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        If isValid() Then
            Dim strSearch As String = "SELECT EntryCode FROM tblAdmin"
            Dim strCode As String = CStr(txtCode1.Text) & CStr(txtCode2.Text) & CStr(txtCode3.Text) & CStr(txtCode4.Text)
            Dim mySqlSearch As New SqlCommand(strSearch, myConnectionString)
            Dim mySqlDataReader As SqlDataReader = Nothing
            Try
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                mySqlDataReader = mySqlSearch.ExecuteReader
                If mySqlDataReader.Read Then
                    If strCode = mySqlDataReader.Item("EntryCode") Then
                        reSet()
                        isValidCode = True
                        mySqlDataReader.Close()
                        Me.Close()
                        Exit Sub
                    Else
                        mySqlDataReader.Close()
                        MessageBox.Show("Invalid Entry Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        isValidCode = False
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("An Error Occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                myConnectionString.Close()
            End Try
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        isValidCode = False
        Me.Close()
    End Sub

    Private Sub chkShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkShow.CheckedChanged
        If chkShow.Checked Then
            txtCode1.UseSystemPasswordChar = False
            txtCode2.UseSystemPasswordChar = False
            txtCode3.UseSystemPasswordChar = False
            txtCode4.UseSystemPasswordChar = False
        Else
            txtCode1.UseSystemPasswordChar = True
            txtCode2.UseSystemPasswordChar = True
            txtCode3.UseSystemPasswordChar = True
            txtCode4.UseSystemPasswordChar = True

        End If
    End Sub
End Class