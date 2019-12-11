Imports System.Data
Imports System.Data.SqlClient
Public Class frmSettings



    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub txtStartTime_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles txtStartTime.MaskInputRejected

    End Sub



    '******************Functions and Sub Procedures**********************
    Private Function isValid() As Boolean
        If txtAssociationName.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtAssociationName, "Association Name is required")
            lblStatus.Text = "Association Name is required"
            Return False
        End If


        If txtStartTime.Text.Trim = ":" Then
            ErrorProvider1.SetError(cboStart, "Election Start time is required")
            lblStatus.Text = "Election Start time is required"
            Return False
        End If

        If txtStartTime.Text.Length <> 5 Then
            ErrorProvider1.SetError(cboEnd, "Invalid Time")
            lblStatus.Text = "Invalid Start time"
            Return False
        End If

        If cboStart.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(cboStart, "Select time format")
            lblStatus.Text = "Select time format"
            Return False
        End If

        If txtEndTime.Text.Trim = ":" Then
            ErrorProvider1.SetError(cboEnd, "Election End time is required")
            lblStatus.Text = "Election End time is required"
            Return False
        End If

        If txtEndTime.Text.Length <> 5 Then
            ErrorProvider1.SetError(cboEnd, "Invalid End time")
            lblStatus.Text = "Invalid End time"
            Return False
        End If
        If cboEnd.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(cboEnd, "Select time format")
            lblStatus.Text = "Select time format"
            Return False
        End If
        Return True
    End Function

    Sub reSet()
        txtAssociationName.Clear()
        txtSlogan.Clear()
        txtYear.Clear()
        txtStartTime.Clear()
        txtEndTime.Clear()
        chkAllowSkip.Checked = True
        chkUseID.Checked = True
        txtAssociationName.Focus()
        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
        cboStart.SelectedItem = cboStart.Items(0)
        cboEnd.SelectedItem = cboEnd.Items(1)
    End Sub

    Sub setSettings()
        Dim strSlogan As String = String.Empty
        Dim strSelect As String = "SELECT * FROM tblAssociationInfo"
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)
        Dim mySqlDataReader As SqlDataReader = Nothing
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            mySqlDataReader = mySqlSelect.ExecuteReader
            If mySqlDataReader.Read Then
                Dim strAssociationName As String = CStr(mySqlDataReader.Item("AssociationName"))
                strSlogan = CStr(mySqlDataReader.Item("Slogan"))
                Dim strStartTime As String = CStr(mySqlDataReader.Item("StartTime")).Trim
                Dim strEndTime As String = CStr(mySqlDataReader.Item("EndTime")).Trim
                Dim strSystemID As String = CStr(mySqlDataReader.Item("SystemID")).Trim
                Dim strSkipPosition As String = CStr(mySqlDataReader.Item("SkipPositions"))
                Dim strSystemPin As String = CStr(mySqlDataReader.Item("SystemPIN"))
                txtAssociationName.Text = strAssociationName
                txtSlogan.Text = strSlogan
                Dim strStartAM As String = strStartTime.Remove(0, 5)
                Dim strEndAM As String = strEndTime.Remove(0, 5)
                strStartTime = strStartTime.Remove(strStartTime.Length - 2, 2)
                strEndTime = strEndTime.Remove(strEndTime.Length - 2, 2)
                txtStartTime.Text = strStartTime
                txtEndTime.Text = strEndTime

                For i = 0 To cboStart.Items.Count - 1
                    If strStartAM = cboStart.Items(i) Then
                        cboStart.SelectedItem = cboStart.Items(i)
                    End If
                Next

                For i = 0 To cboEnd.Items.Count - 1
                    If strEndAM = cboEnd.Items(i) Then
                        cboEnd.SelectedItem = cboEnd.Items(i)
                    End If
                Next

                If strSystemID = "YES" Then
                    chkUseID.Checked = True
                Else
                    chkUseID.Checked = False
                End If

                If strSkipPosition = "YES" Then
                    chkAllowSkip.Checked = True
                Else
                    chkAllowSkip.Checked = False
                End If

                If strSystemPin = "YES" Then
                    chkPin.Checked = True
                Else
                    chkPin.Checked = False
                End If
                mySqlDataReader.Close()
            Else
                mySqlDataReader.Close()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("Sorry An Error Occured " & vbCrLf & ex.Message)
        Finally
            myConnectionString.Close()
        End Try
    End Sub
    '***************************************************************


    Private Sub txtStartTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStartTime.KeyPress
        If Not Char.IsNumber(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub


    Private Sub txtEndTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEndTime.KeyPress
        If Not Char.IsNumber(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setSettings()
    End Sub


    Private Sub frmSettings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        isValidCode = False
    End Sub

    Private Sub btnExit_MouseEnter(sender As Object, e As EventArgs)
        btnExit.ForeColor = Color.White
        btnExit.BackColor = Color.FromArgb(3, 31, 69)
    End Sub

    Private Sub btnExit_MouseLeave(sender As Object, e As EventArgs)
        btnExit.ForeColor = Color.FromArgb(3, 31, 69)
        btnExit.BackColor = Color.White
    End Sub

    Private Sub btnReset_MouseEnter(sender As Object, e As EventArgs)
        btnReset.ForeColor = Color.White
        btnReset.BackColor = Color.FromArgb(3, 31, 69)
    End Sub

    Private Sub btnReset_MouseLeave(sender As Object, e As EventArgs)
        btnReset.ForeColor = Color.FromArgb(3, 31, 69)
        btnReset.BackColor = Color.White
    End Sub

    Private Sub btnSave_MouseEnter(sender As Object, e As EventArgs)
        btnSave.ForeColor = Color.White
        btnSave.BackColor = Color.FromArgb(3, 31, 69)
    End Sub

    Private Sub btnSave_MouseLeave(sender As Object, e As EventArgs)
        btnSave.ForeColor = Color.FromArgb(3, 31, 69)
        btnSave.BackColor = Color.White
    End Sub

    Private Sub btnSave_Click_1(sender As Object, e As EventArgs) Handles btnSave.Click

        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
        If isValid() Then
            Dim strAssociatonName As String = CStr(txtAssociationName.Text.Trim).ToUpper
            Dim strSlogan As String = String.Empty
            If txtSlogan.Text.Trim <> String.Empty Then
                strSlogan = CStr(txtSlogan.Text.Trim).ToUpper
            End If
            Dim strStartTime As String = CStr(txtStartTime.Text.Trim) & CStr(cboStart.Text.Trim)
            Dim strEndTime As String = CStr(txtEndTime.Text.Trim) & CStr(cboEnd.Text.Trim)
            Dim strUseSystemID As String = String.Empty
            Dim strAllowSkip As String = String.Empty
            Dim strSystemPin As String = String.Empty
            If chkUseID.Checked Then
                strUseSystemID = "YES"
            Else
                strUseSystemID = "NO"
            End If
            If chkAllowSkip.Checked = True Then
                strAllowSkip = "YES"
            Else
                strAllowSkip = "NO"
            End If

            If chkPin.Checked Then
                strSystemPin = "YES"
            Else
                strSystemPin = "NO"
            End If


            'Insert Into the database
            Dim strSearch As String = "SELECT * FROM tblAssociationInfo"
            Dim strInsert As String = "INSERT INTO tblAssociationInfo(AssociationName,Slogan,StartTime,EndTime,SystemID,SkipPositions,SystemPIN) VALUES(@AssociationName,@Slogan,@StartTime,@EndTime,@SystemID,@SkipPositions,@SystemPIN)"
            Dim mySqlSearch As New SqlCommand(strSearch, myConnectionString)
            Dim mySqlInsert As New SqlCommand(strInsert, myConnectionString)
            Dim mySqlDataReader As SqlDataReader = Nothing
            Dim strUpdate As String = "UPDATE tblAssociationInfo SET AssociationName=@AssociationName,Slogan=@Slogan,StartTime=@StartTime,EndTime=@EndTime,SystemID=@SystemID,SkipPositions=@SkipPositions,SystemPIN=@SystemPIN"
            Dim mySqlUpdate As New SqlCommand(strUpdate, myConnectionString)
            Try
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                mySqlDataReader = mySqlSearch.ExecuteReader
                If mySqlDataReader.Read Then

                    mySqlDataReader.Close()
                    'A Record Already Exist so prompt the user and Update
                    If MessageBox.Show("Settings already Exist." & vbCrLf & "Do you want to Update Settings?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        'Update the settings

                        mySqlUpdate.Parameters.AddWithValue("@AssociationName", strAssociatonName)
                        mySqlUpdate.Parameters.AddWithValue("@Slogan", strSlogan)
                        mySqlUpdate.Parameters.AddWithValue("@StartTime", strStartTime)
                        mySqlUpdate.Parameters.AddWithValue("@EndTime", strEndTime)
                        mySqlUpdate.Parameters.AddWithValue("@SystemID", strUseSystemID)
                        mySqlUpdate.Parameters.AddWithValue("@SkipPositions", strAllowSkip)
                        mySqlUpdate.Parameters.AddWithValue("@SystemPIN", strSystemPin)
                        mySqlUpdate.ExecuteNonQuery()
                        MessageBox.Show(" Election Settings Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    'No Settings Exist so Insert

                    mySqlDataReader.Close()
                    mySqlInsert.Parameters.AddWithValue("@AssociationName", strAssociatonName)
                    mySqlInsert.Parameters.AddWithValue("@Slogan", strSlogan)
                    mySqlInsert.Parameters.AddWithValue("@StartTime", strStartTime)
                    mySqlInsert.Parameters.AddWithValue("@EndTime", strEndTime)
                    mySqlInsert.Parameters.AddWithValue("@SystemID", strUseSystemID)
                    mySqlInsert.Parameters.AddWithValue("@SkipPositions", strAllowSkip)
                    mySqlInsert.Parameters.AddWithValue("@SystemPIN", strSystemPin)
                    Dim nRecord As Integer = mySqlInsert.ExecuteNonQuery()
                    MessageBox.Show(" Election Settings Registered", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                myConnectionString.Close()
            End Try
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reSet()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class