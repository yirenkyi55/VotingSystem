Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Public Class frmRegisterCan
    Private blnGetPosition As Boolean = False

    Private Sub frmRegisterCan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        generateID()
        LoadCombo()
        enableRemove()
        loadTitle()
    End Sub


    Sub generateID()
        Dim strRecordName As String = "CandidateID"
        Dim strTableName As String = "tblCandidate"
        lblRecordID.Text = CStr(eGenerateID(strRecordName, strTableName))
    End Sub

    Sub LoadCombo()

        cboPosition.DataSource = Nothing
        Dim strSelect As String = "SELECT * FROM tblPositions ORDER BY PositionID"
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)
        Dim mySqlDataReader As SqlDataReader = Nothing
        Dim comboSource As New Dictionary(Of String, String)()
        Dim blnRead As Boolean = False
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            mySqlDataReader = mySqlSelect.ExecuteReader
            While mySqlDataReader.Read
                comboSource.Add(CStr(mySqlDataReader.Item("PositionID")), CStr(mySqlDataReader.Item("Name")))
                blnRead = True
            End While

            If blnRead Then
                cboPosition.DataSource = New BindingSource(comboSource, Nothing)
                cboPosition.DisplayMember = "value"
                cboPosition.ValueMember = "key"

            End If
            mySqlDataReader.Close()
        Catch ex As Exception
        Finally
            myConnectionString.Close()
        End Try
        If cboPosition.Items.Count = 0 Then
            lblStatus.Text = "No Position Registered.!! Register Positions"
        End If

    End Sub







    '********************Functions and Sub procedures *********************
    Private Function isValid() As Boolean
        If cboPosition.Items.Count = 0 Then
            MessageBox.Show("Please Register Elections Positions/Portfolios", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
            lblStatus.Text = "No Positions Found. Register Portfolios"
            Return False
        End If
        If txtFirstName.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtFirstName, "Please Enter First Name")
            lblStatus.Text = "Please Enter First Name"
            txtFirstName.Focus()
            Return False
        End If

        If txtSurname.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtSurname, "Please Enter Surname")
            lblStatus.Text = "Please Enter Surname"
            txtSurname.Focus()
            Return False
        End If


        If CInt(dtpDOB.Value.Year) >= CInt(Now.ToString("yyyy")) Then
            ErrorProvider1.SetError(dtpDOB, "Invalid Date of Birth")
            lblStatus.Text = "Date of Birth is required"
            dtpDOB.Focus()
            Return False
        End If

        Dim intAge As Integer = calculateAge()
        If intAge <= 17 Then
            ErrorProvider1.SetError(dtpDOB, "Invalid age. Age must exceed 18 years")
            lblStatus.Text = "Invalid age. Age must exceed 18 years"
            dtpDOB.Focus()
            Return False
        End If

        If cboPosition.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(cboPosition, "Please Select Position")
            lblStatus.Text = "Please select Position"
            cboPosition.Focus()
            Return False
        End If

        If picProfile.Image Is Nothing Then
            ErrorProvider1.SetError(picProfile, "Please Select Candidate Profile")
            lblStatus.Text = "Please Select Candidate Profile"
            btnBrowse.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function calculateAge() As Integer
        Dim intYear As Integer = CInt(dtpDOB.Value.Year)
        Dim intSystemYear As Integer = CInt(Now.ToString("yyyy"))
        Dim intSystemMonth As Integer = CInt(Now.ToString("MM"))
        Dim intMonth As Integer = CInt(dtpDOB.Value.Month)
        Dim intDay As Integer = CInt(dtpDOB.Value.Day)
        Dim intSystemDay As Integer = CInt(Now.ToString("dd"))
        Dim intAge As Integer = intSystemYear - intYear

        If intSystemMonth = intMonth Then
            If intSystemDay >= intDay Then
                Return intAge
            Else
                Return intAge - 1
            End If

        ElseIf intSystemMonth > intMonth Then
            Return intAge

        ElseIf intSystemMonth < intMonth Then
            Return intAge - 1
        End If

        Return intAge
    End Function

    Sub loadTitle()
        cboTitle.Items.Clear()
        cboTitle.Items.Add("MR")
        cboTitle.Items.Add("MRS")
        cboTitle.Items.Add("MISS")
        cboTitle.Items.Add("DR")
        cboTitle.Items.Add("PROF")
    End Sub
    Sub clear()
        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
        txtFirstName.Clear()
        txtOtherName.Clear()
        txtSurname.Clear()
        dtpDOB.Value = Now.ToString("dd/MMM/yyyy")
        LoadCombo()
        loadTitle()
        lblAge.Text = String.Empty
        picProfile.Image = Nothing
        cboTitle.Focus()
        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
        generateID()
        enableRemove()
        radMale.Checked = True
        blnGetPosition = False
        enableButtons()
    End Sub

    Sub enableRemove()
        If picProfile.Image IsNot Nothing Then
            btnRemove.Enabled = True
        Else
            btnRemove.Enabled = False
        End If
    End Sub
    '************************************************
    Private Sub dtpDOB_ValueChanged(sender As Object, e As EventArgs) Handles dtpDOB.ValueChanged
        Dim intAge As Integer = calculateAge()
        If intAge <= 0 Then
            lblAge.Text = "Invalid Age"
        ElseIf intAge = 1 Then
            lblAge.Text = intAge.ToString & " year"
        Else

            lblAge.Text = intAge.ToString & " years"
        End If

    End Sub






    Sub enableButtons()
        If blnGetPosition = True Then
            btnDelete.Enabled = True
            btnUpdate.Enabled = True
            btnRegister.Enabled = False
        Else
            btnDelete.Enabled = False
            btnUpdate.Enabled = False
            btnRegister.Enabled = True
        End If
    End Sub

    Private Sub frmRegisterCan_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        isValidCode = False
    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
        If isValid() Then
            Dim strFirstName As String = CStr(txtFirstName.Text.Trim).ToUpper
            Dim strOtherName As String = String.Empty
            If txtOtherName.Text.Trim <> String.Empty Then
                strOtherName = CStr(txtOtherName.Text.Trim).ToUpper
            End If
            Dim strLastName As String = CStr(txtSurname.Text.Trim).ToUpper
            Dim strGender As String = String.Empty
            If radMale.Checked Then
                strGender = radMale.Text.ToUpper
            Else
                strGender = radFemale.Text.ToUpper
            End If
            Dim intRecordID As Long = CLng(lblRecordID.Text.Trim)
            Dim strDateOfBirth As String = CStr(dtpDOB.Value.ToString("dd/MMM/yyyy"))
            Dim strProfile As String = "\Images\Candidate" & intRecordID & ".jpg"
            Dim Selectedkey As String = DirectCast(cboPosition.SelectedItem, KeyValuePair(Of String, String)).Key

            Dim intPositionID As Integer = CInt(Selectedkey)
            Dim strTitle As String = String.Empty
            If cboTitle.Text.Trim <> String.Empty Then strTitle = cboTitle.SelectedItem.ToString
            Dim strInsert As String = "INSERT INTO tblCandidate(CandidateID,Title,FirstName,OtherName,LastName,Gender,DOB,PositionID,Profile) " &
                " VALUES(@CandidateID,@Title,@FirstName,@OtherName,@LastName,@Gender,@DOB,@PositionID,@Profile)"
            Dim mySqlInsert As New SqlCommand(strInsert, myConnectionString)
            Try
                'Create the Images directory if it does not exist
                If System.IO.Directory.Exists(strPath & "\Images") = False Then
                    MkDir(strPath & "\Images")
                End If

                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                mySqlInsert.Parameters.AddWithValue("@CandidateID", intRecordID)
                mySqlInsert.Parameters.AddWithValue("@Title", strTitle)
                mySqlInsert.Parameters.AddWithValue("@FirstName", strFirstName)
                mySqlInsert.Parameters.AddWithValue("@OtherName", strOtherName)
                mySqlInsert.Parameters.AddWithValue("@LastName", strLastName)
                mySqlInsert.Parameters.AddWithValue("@Gender", strGender)
                mySqlInsert.Parameters.AddWithValue("@DOB", strDateOfBirth)
                mySqlInsert.Parameters.AddWithValue("@PositionID", intPositionID)
                mySqlInsert.Parameters.AddWithValue("@Profile", strProfile)
                picProfile.Image.Save(strPath & strProfile)

                Dim nRecord As Integer = mySqlInsert.ExecuteNonQuery
                MessageBox.Show(nRecord & " Candidate Registered", "Register", MessageBoxButtons.OK, MessageBoxIcon.Information)
                clear()

            Catch ex As Exception
                MessageBox.Show("Sorry An Error Occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Finally
                myConnectionString.Close()
            End Try
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
        If isValid() Then
            Dim strFirstName As String = CStr(txtFirstName.Text.Trim).ToUpper
            Dim strOtherName As String = String.Empty
            If txtOtherName.Text.Trim <> String.Empty Then
                strOtherName = CStr(txtOtherName.Text.Trim).ToUpper
            End If
            Dim strLastName As String = CStr(txtSurname.Text.Trim)
            Dim strGender As String = String.Empty
            If radMale.Checked Then
                strGender = radMale.Text.ToUpper
            Else
                strGender = radFemale.Text.ToUpper
            End If
            Dim intRecordID As Long = CLng(lblRecordID.Text.Trim)
            Dim strDateOfBirth As String = CStr(dtpDOB.Value.ToString("dd/MMM/yyyy"))
            Dim strProfile As String = "\Images\Candidate" & intRecordID & ".jpg"

            Dim Selectedkey As String = DirectCast(cboPosition.SelectedItem, KeyValuePair(Of String, String)).Key

            Dim intPositionID As Integer = CInt(Selectedkey)
            Dim strTitle As String = String.Empty
            If cboTitle.Text.Trim <> String.Empty Then strTitle = cboTitle.SelectedItem.ToString
            Dim strUpdate As String = "UPDATE tblCandidate SET " &
                "Title=@Title,FirstName=@FirstName,OtherName=@OtherName,LastName=@LastName,Gender=@Gender,DOB=@DOB,PositionID=@PositionID,Profile=@Profile WHERE CandidateID=@CandidateID"
            Dim mySqlUpdate As New SqlCommand(strUpdate, myConnectionString)
            If MessageBox.Show("Do you want to update record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Try
                    If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                    mySqlUpdate.Parameters.AddWithValue("@CandidateID", intRecordID)
                    mySqlUpdate.Parameters.AddWithValue("@Title", strTitle)
                    mySqlUpdate.Parameters.AddWithValue("@FirstName", strFirstName)
                    mySqlUpdate.Parameters.AddWithValue("@OtherName", strOtherName)
                    mySqlUpdate.Parameters.AddWithValue("@LastName", strLastName)
                    mySqlUpdate.Parameters.AddWithValue("@Gender", strGender)
                    mySqlUpdate.Parameters.AddWithValue("@DOB", strDateOfBirth)
                    mySqlUpdate.Parameters.AddWithValue("@PositionID", intPositionID)
                    mySqlUpdate.Parameters.AddWithValue("@Profile", strProfile)
                    picProfile.Image.Save(strPath & strProfile)
                    Dim nRecord As Integer = mySqlUpdate.ExecuteNonQuery
                    MessageBox.Show(nRecord & " Candidate Information successfully Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    clear()

                Catch ex As Exception
                    MessageBox.Show("Sorry An Error Occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Finally
                    myConnectionString.Close()
                End Try
            End If

        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim myData As New GetData
        Dim intCandidateID As Integer = CInt(lblRecordID.Text.Trim)
        If myData.DeleteCandidate(intCandidateID) Then
            clear()
        End If
    End Sub

    Private Sub btnGetData_Click(sender As Object, e As EventArgs) Handles btnGetData.Click
        Using frm As frmRegisterCanGet = New frmRegisterCanGet
            If frm.ShowDialog = DialogResult.OK Then
                '#####################
                lblRecordID.Text = frm.candidateID
                txtFirstName.Text = frm.FirstName
                If frm.Title <> String.Empty Then cboTitle.Text = frm.Title
                If frm.OtherName IsNot Nothing Then txtOtherName.Text = frm.OtherName
                txtSurname.Text = frm.LastName
                If frm.Gender = "MALE" Then
                    radMale.Checked = True
                Else
                    radFemale.Checked = True
                End If
                dtpDOB.Value = frm.DOB
                Try
                    Dim BMP As Bitmap
                    BMP = Bitmap.FromFile(strPath & frm.Profile)
                    picProfile.Image = New Bitmap(BMP)
                    BMP.Dispose()
                    GC.Collect()
                    enableRemove()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                For i = 0 To cboPosition.Items.Count - 1
                    Dim PositionKey As String = DirectCast(cboPosition.Items(i), KeyValuePair(Of String, String)).Key
                    If frm.Position = PositionKey Then
                        cboPosition.SelectedItem = cboPosition.Items(i)
                    End If
                Next
                blnGetPosition = True
                enableButtons()
                '####################
            End If
        End Using
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        clear()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        With OpenFileDialog1
            .Title = "Select Candidate Profile"
            .FileName = String.Empty
            .Filter = "JPG(*.jpg)|*.jpg|PNG(*.png)|*.png|JPEG(*.jpeg)|*.jpeg|GIF(*.gif)|*.gif"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                picProfile.Image = Image.FromFile(.FileName)
                enableRemove()
            End If
        End With
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        picProfile.Image = Nothing
    End Sub
End Class