Imports System.Data.OleDb
Imports System.Data
Imports DevExpress.XtraSplashScreen
Imports System.Data.SqlClient
Public Class frmCreateVoters
    Dim blnGetData As Boolean = False
    Private blnUseSystemID As Boolean = False
    Private blnUseSystemPin As Boolean = False
    Private blnHadData As Boolean = False
    Private intPreviousRand As Integer = 1
    Private intPreviousPin As Integer = 1

    Private Sub cboWorkSheet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWorkSheet.SelectedIndexChanged
        btnLoad.Enabled = True


    End Sub

    Private Sub frmCreateVoters_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        checkSystemID("SystemID")
        checkSystemID("SystemPIN")
        If blnUseSystemID Then

            generateID()
        End If

        If blnUseSystemPin Then
            generatePIN()
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ProgressBar1.Value = 100 Then
            Timer1.Enabled = False
            blnGetData = True
            lblCount.Text = "Please Wait a Moment...."
            If blnGetData Then
                Try

                    'Display the data from the selected worksheet
                    Dim sourceConnectionString As String = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES'", txtExcelFile.Text)
                    Dim adapter As OleDbDataAdapter = New OleDbDataAdapter(String.Format("SELECT * FROM [{0}]", cboWorkSheet.SelectedItem.ToString()), sourceConnectionString)
                    Dim currentSheet As DataTable = New DataTable
                    adapter.Fill(currentSheet)
                    adapter.Dispose()
                    DataGridView1.DataSource = currentSheet
                    btnReset.Enabled = True
                Catch ex As Exception
                    Timer1.Enabled = False
                    MessageBox.Show("Sorry an Error Occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    btnReset.Enabled = True
                End Try


            End If
            ProgressBar1.Value = 0
            ProgressBar1.Visible = False
            lblCount.Visible = False
            btnLoad.Enabled = False
        Else
            ProgressBar1.Visible = True
            lblCount.Visible = True
            ProgressBar1.Value += 1
            lblCount.Text = "Loading Excel Worksheet... " & ProgressBar1.Value.ToString & "%"
            blnGetData = False

        End If
    End Sub





    '**********************Functions and Sub procedures **********************
    'Function to Validate the controls
    Private Function isValid() As Boolean
        If TabControl1.SelectedIndex = 0 Then
            If txtVoterID.ReadOnly = False And txtVoterID.Text.Trim = String.Empty Then
                ErrorProvider1.SetError(txtVoterID, "Please Enter Voters ID")
                txtVoterID.Focus()
                lblStatus.Text = "Please Enter Voters ID"
                Return False
            End If

            If txtPin1.Text = "" Then
                ErrorProvider1.SetError(txtPin1, "Please Enter Pin 1")
                lblStatus.Text = "Please Enter Pin 1"
                txtPin1.Focus()
                Return False
            End If

            If txtPin2.Text = "" Then
                ErrorProvider1.SetError(txtPin2, "Please Enter Pin 2")
                lblStatus.Text = "Please Enter Pin 2"
                txtPin2.Focus()
                Return False
            End If
            If txtPin3.Text = "" Then
                ErrorProvider1.SetError(txtPin3, "Please Enter Pin 3")
                lblStatus.Text = "Please Enter Pin 3"
                txtPin3.Focus()
                Return False
            End If
            If txtPin4.Text = "" Then
                ErrorProvider1.SetError(txtPin4, "Please Enter Pin 4")
                lblStatus.Text = "Please Enter Pin 4"
                txtPin4.Focus()
                Return False
            End If

            If txtFirstName.Text = String.Empty Then
                ErrorProvider1.SetError(txtFirstName, "Enter first name")
                lblStatus.Text = "Enter First Name"
                txtFirstName.Focus()
                Return False
            End If

            If txtLastName.Text = String.Empty Then
                ErrorProvider1.SetError(txtLastName, "Enter Last Name")
                lblStatus.Text = "Enter Last Name"
                txtLastName.Focus()
                Return False
            End If

        ElseIf TabControl1.SelectedIndex = 1 Then
            If DataGridView1.Rows.Count = 0 Then
                MessageBox.Show("Please Load Excel Files", "Load", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
        End If

        Return True
    End Function

    'A Sub procedure to generate recordID
    Sub generateID()
        Dim strSelect As String = "SELECT MAX(VoterID) FROM tblVoters"
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)

        Dim intRand As Integer = 0
        Dim rand As New Random
        intRand = rand.Next(99) + 100
        Dim randSecond As New Random
        intPreviousRand = (randSecond.Next(9) + 1) + intPreviousRand
        Dim intRecordId As Integer = intRand + 1001001
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()

            If mySqlSelect.ExecuteScalar IsNot DBNull.Value Then
                Dim strVoterID As String = mySqlSelect.ExecuteScalar.ToString
                Dim strRemoveFirst As String = strVoterID.Substring(0, 3)
                Dim strRemoveSecond As String = strVoterID.Substring(3, strVoterID.Length - 4)
                Dim intVoterID As Integer

                Dim intConvert As Integer

                If Integer.TryParse(strRemoveFirst, intConvert) Then


                    If Integer.TryParse(strRemoveSecond, intConvert) Then
                        Dim intRemoveFirst As Integer = CInt((strRemoveFirst)) + intPreviousRand
                        Dim intRemoveSecond As Integer = CInt(strRemoveSecond) + 17
                        intVoterID = intRemoveFirst & intRemoveSecond
                    Else
                        MessageBox.Show("System failed to Generate a Voter ID." & vbCrLf & " Info: System generates ID only if already store ID's are in number format. " & "Use Manual Generated ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("System failed to Generate a VoterID." & vbCrLf & " Info: System generates ID only if already store ID's are in number format. " & "Use Manual Generated ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                txtVoterID.Text = intVoterID

            Else

                txtVoterID.Text = intRecordId

            End If
        Catch ex As Exception
            MessageBox.Show("Sorry An Error Occured " & vbCrLf & ex.Message)

        Finally
            myConnectionString.Close()
        End Try

    End Sub

    'A Sub procedure to generate PinNumber
    Sub generatePIN()
        Dim strSelect As String = "SELECT MAX(PinNumber) FROM tblVoters"
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)

        Dim randSecond As New Random



        Dim intRecordId As Integer = 1000
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()

            If mySqlSelect.ExecuteScalar IsNot DBNull.Value Then
                Dim intPinNumber As Integer = CInt(mySqlSelect.ExecuteScalar) + 7
                Dim strPin As String = CStr(intPinNumber)
                txtPin1.Text = strPin.Substring(0, 1)
                txtPin2.Text = strPin.Substring(1, 1)
                txtPin3.Text = strPin.Substring(2, 1)
                txtPin4.Text = strPin.Substring(3, 1)

            Else
                Dim strPin As String = intRecordId.ToString
                txtPin1.Text = strPin.Substring(0, 1)
                txtPin2.Text = strPin.Substring(1, 1)
                txtPin3.Text = strPin.Substring(2, 1)
                txtPin4.Text = strPin.Substring(3, 1)


            End If
        Catch ex As Exception
            MessageBox.Show("Sorry An Error Occured " & vbCrLf & ex.Message)

        Finally
            myConnectionString.Close()
        End Try

    End Sub

    'Sub to Clear Error Provider
    Sub clearProvider()
        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
    End Sub

    'Sub procedure to Reset The controls
    Sub reSet()
        If TabControl1.SelectedIndex = 0 Then

            If blnUseSystemID Then
                txtFirstName.Focus()
                txtVoterID.ReadOnly = True
                generateID()
            Else
                txtVoterID.Clear()
                txtVoterID.ReadOnly = False
                txtVoterID.Focus()
            End If

            If blnUseSystemPin Then
                generatePIN()

            Else
                txtPin1.Clear()
                txtPin2.Clear()
                txtPin3.Clear()
                txtPin4.Clear()
            End If
            txtFirstName.Clear()
            txtOtherName.Clear()
            txtLastName.Clear()

        ElseIf TabControl1.SelectedIndex = 1 Then
            txtExcelFile.Clear()
            cboWorkSheet.Items.Clear()
            cboWorkSheet.Text = String.Empty
            DataGridView1.DataSource = String.Empty
        End If
        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
        blnHadData = False
        enableButtons()
    End Sub

    'Check System Settings to Verify if use system GeneratedID is checked
    Sub checkSystemID(ByVal strFieldName As String)
        Dim strSelect As String = "SELECT " & strFieldName & " FROM tblAssociationInfo"
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)
        Dim mySqlDataReader As SqlDataReader = Nothing

        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            mySqlDataReader = mySqlSelect.ExecuteReader
            If mySqlDataReader.Read Then
                Dim systemID As String = (mySqlDataReader.Item(strFieldName))
                If strFieldName = "SystemID" Then
                    If systemID = "YES" Then
                        blnUseSystemID = True

                    Else
                        blnUseSystemID = False

                    End If
                ElseIf strFieldName = "SystemPIN" Then
                    'Perform use system pin
                    If systemID = "YES" Then
                        blnUseSystemPin = True

                    Else
                        blnUseSystemPin = False

                    End If
                End If



                mySqlDataReader.Close()
            Else
                mySqlDataReader.Close()
                ' blnUseSystemID = True

            End If
        Catch ex As Exception
            MessageBox.Show("An Error Occured " & vbCrLf & ex.Message)

        Finally
            myConnectionString.Close()
        End Try

        If blnUseSystemID Then
            txtVoterID.ReadOnly = True
        Else
            txtVoterID.ReadOnly = False
            txtVoterID.Focus()
        End If

        If blnUseSystemPin Then
            txtPin1.ReadOnly = True
            txtPin2.ReadOnly = True
            txtPin3.ReadOnly = True
            txtPin4.ReadOnly = True
        Else
            txtPin1.ReadOnly = False
            txtPin2.ReadOnly = False
            txtPin3.ReadOnly = False
            txtPin4.ReadOnly = False
        End If
    End Sub

    'A sub procedure to register Data from gridview to database
    Sub insertGridViewData()
        checkSystemID("SystemID")
        checkSystemID("SystemPIN")
        Dim count As Integer = 1
        SplashScreenManager.ShowForm(Me, GetType(frmWait), True, True, False, 100)
        SplashScreenManager.Default.SetWaitFormCaption("Inseting records...")
        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim strVoterID As String = String.Empty
            Dim strVoterPin As String = String.Empty
            Dim strFirstName As String = String.Empty
            Dim strOtherName As String = String.Empty
            Dim strLastName As String = String.Empty
            Dim strErrorMessage As String = String.Empty
            Dim intRecordID As Integer = 0
            If blnUseSystemID = True And blnUseSystemPin Then
                If row.Cells.Count = 3 Then
                    'Continue and check if the right format is entered
                    If row.Cells(0).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("First Name is Required" & vbCrLf & "First Name missing from Column 1, Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strFirstName = CStr(row.Cells(0).Value).ToUpper
                    End If

                    If row.Cells(1).Value IsNot DBNull.Value Then
                        strOtherName = CStr(row.Cells(1).Value).ToUpper
                    End If

                    If row.Cells(2).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("Last Name is Required" & vbCrLf & "Last Name missing from Coulmn 3 Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strLastName = CStr(row.Cells(2).Value).ToUpper
                    End If

                    'MessageBox.Show(strFirstName & " " & strOtherName & " " & strLastName)
                    Dim strInsert As String = "INSERT INTO tblVoters(VoterID,PinNumber,FirstName,OtherName,LastName) " &
                        "VALUES(@VoterID,@PinNumber, @FirstName,@OtherName,@LastName)"
                    Dim mySqlInsert As New SqlCommand(strInsert, myConnectionString)
                    Dim mySqlDataReader As SqlDataReader = Nothing

                    Try
                        generateID()
                        generatePIN()
                        If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                        strVoterID = CStr(txtVoterID.Text.Trim)
                        strVoterPin = CStr(txtPin1.Text.Trim & txtPin2.Text.Trim & txtPin3.Text.Trim & txtPin4.Text.Trim)
                        mySqlInsert.Parameters.AddWithValue("@VoterID", strVoterID)
                        mySqlInsert.Parameters.AddWithValue("@PinNumber", strVoterPin)
                        mySqlInsert.Parameters.AddWithValue("@FirstName", strFirstName)
                        mySqlInsert.Parameters.AddWithValue("@OtherName", strOtherName)
                        mySqlInsert.Parameters.AddWithValue("@LastName", strLastName)
                        mySqlInsert.ExecuteNonQuery()

                    Catch ex As Exception
                        Try
                            SplashScreenManager.CloseForm()
                        Catch ex1 As Exception

                        End Try
                        strErrorMessage = ex.Message
                    Finally
                        myConnectionString.Close()
                    End Try

                Else
                    SplashScreenManager.CloseForm()
                    MessageBox.Show("Data Columns must be three :FirstName,OtherName,LastName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                'A situation where system voterID is used but not system pin

            ElseIf blnUseSystemID = True And blnUseSystemPin = False Then
                'We are not using the system Data So the format must be four

                If row.Cells.Count = 4 Then
                    'Validate and insert data
                    '**********************************
                    If row.Cells(0).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("Voter PIN is Required" & vbCrLf & "Pin Number missing from Column 1 Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strVoterPin = CStr(row.Cells(0).Value).ToUpper
                    End If

                    If row.Cells(1).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("First Name is Required" & vbCrLf & "First Name missing from Column 2 Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strFirstName = CStr(row.Cells(1).Value).ToUpper
                    End If

                    If row.Cells(2).Value IsNot DBNull.Value Then
                        strOtherName = CStr(row.Cells(2).Value).ToUpper
                    End If

                    If row.Cells(3).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("Last Name is Required" & vbCrLf & "Last Name missing from Column 4 Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strLastName = CStr(row.Cells(3).Value).ToUpper
                    End If
                    'Generate A systemID
                    generateID()
                    strVoterID = CStr(txtVoterID.Text.Trim)

                    'MessageBox.Show(strFirstName & " " & strOtherName & " " & strLastName)
                    Dim strInsert As String = "INSERT INTO tblVoters(VoterID, PinNumber, FirstName,OtherName,LastName) " &
                        "VALUES(@VoterID, @PinNumber, @FirstName,@OtherName,@LastName)"
                    Dim mySqlInsert As New SqlCommand(strInsert, myConnectionString)
                    Dim mySqlDataReader As SqlDataReader = Nothing
                    Try

                        If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                        mySqlInsert.Parameters.AddWithValue("@VoterID", strVoterID)
                        mySqlInsert.Parameters.AddWithValue("@PinNumber", strVoterPin)
                        mySqlInsert.Parameters.AddWithValue("@FirstName", strFirstName)
                        mySqlInsert.Parameters.AddWithValue("@OtherName", strOtherName)
                        mySqlInsert.Parameters.AddWithValue("@LastName", strLastName)
                        mySqlInsert.ExecuteNonQuery()

                    Catch ex As Exception
                        Try
                            SplashScreenManager.CloseForm()
                        Catch ex1 As Exception

                        End Try
                        strErrorMessage = ex.Message
                    Finally
                        myConnectionString.Close()
                    End Try
                    '***********************************

                Else
                    SplashScreenManager.CloseForm()
                    MessageBox.Show("Data Columns must be Four :Voter PIN,FirstName,OtherName,LastName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                '######################
            ElseIf blnUseSystemID = False And blnUseSystemPin = True Then
                'We are not using the system Data So the format must be four

                If row.Cells.Count = 4 Then
                    'Validate and insert data

                    If row.Cells(0).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("VoterID is Required" & vbCrLf & "VoterID missing from Column 1 Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strVoterID = CStr(row.Cells(0).Value).ToUpper
                    End If

                    If row.Cells(1).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("First Name is Required" & vbCrLf & "First Name missing from Column 2 Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strFirstName = CStr(row.Cells(1).Value).ToUpper
                    End If

                    If row.Cells(2).Value IsNot DBNull.Value Then
                        strOtherName = CStr(row.Cells(2).Value).ToUpper
                    End If

                    If row.Cells(3).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("Last Name is Required" & vbCrLf & "Last Name missing from Column 4 Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strLastName = CStr(row.Cells(3).Value).ToUpper
                    End If
                    'Generate A system pin
                    generatePIN()
                    strVoterPin = CStr(txtPin1.Text.Trim & txtPin2.Text.Trim & txtPin3.Text.Trim & txtPin4.Text.Trim)

                    'MessageBox.Show(strFirstName & " " & strOtherName & " " & strLastName)
                    Dim strInsert As String = "INSERT INTO tblVoters(VoterID, PinNumber, FirstName,OtherName,LastName) " &
                        "VALUES(@VoterID, @PinNumber, @FirstName,@OtherName,@LastName)"
                    Dim mySqlInsert As New SqlCommand(strInsert, myConnectionString)
                    Dim mySqlDataReader As SqlDataReader = Nothing
                    Try

                        If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                        mySqlInsert.Parameters.AddWithValue("@VoterID", strVoterID)
                        mySqlInsert.Parameters.AddWithValue("@PinNumber", strVoterPin)
                        mySqlInsert.Parameters.AddWithValue("@FirstName", strFirstName)
                        mySqlInsert.Parameters.AddWithValue("@OtherName", strOtherName)
                        mySqlInsert.Parameters.AddWithValue("@LastName", strLastName)
                        mySqlInsert.ExecuteNonQuery()

                    Catch ex As Exception
                        Try
                            SplashScreenManager.CloseForm()
                        Catch ex1 As Exception

                        End Try
                        strErrorMessage = ex.Message
                    Finally
                        myConnectionString.Close()
                    End Try


                Else
                    SplashScreenManager.CloseForm()
                    MessageBox.Show("Data Columns must be Four :VoterID,FirstName,OtherName,LastName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            Else
                'No System generated PIN and Voter ID is used so the format must be 5

                If row.Cells.Count = 5 Then
                    'Validate and insert data

                    If row.Cells(0).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("VoterID is Required" & vbCrLf & "VoterID missing from Column 1 Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strVoterID = CStr(row.Cells(0).Value).ToUpper
                    End If

                    If row.Cells(1).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("Voter PIN is Required" & vbCrLf & "Voter PIN missing from Column 1 Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strVoterPin = CStr(row.Cells(1).Value).ToUpper
                    End If

                    If row.Cells(2).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("First Name is Required" & vbCrLf & "First Name missing from Column 2 Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strFirstName = CStr(row.Cells(2).Value).ToUpper
                    End If

                    If row.Cells(3).Value IsNot DBNull.Value Then
                        strOtherName = CStr(row.Cells(3).Value).ToUpper
                    End If

                    If row.Cells(4).Value Is DBNull.Value Then
                        SplashScreenManager.CloseForm()
                        MessageBox.Show("Last Name is Required" & vbCrLf & "Last Name missing from Column 4 Row " & count.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        strLastName = CStr(row.Cells(4).Value).ToUpper
                    End If


                    'MessageBox.Show(strFirstName & " " & strOtherName & " " & strLastName)
                    Dim strInsert As String = "INSERT INTO tblVoters(VoterID, PinNumber, FirstName,OtherName,LastName) " &
                        "VALUES(@VoterID, @PinNumber, @FirstName,@OtherName,@LastName)"
                    Dim mySqlInsert As New SqlCommand(strInsert, myConnectionString)
                    Dim mySqlDataReader As SqlDataReader = Nothing
                    Try

                        If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                        mySqlInsert.Parameters.AddWithValue("@VoterID", strVoterID)
                        mySqlInsert.Parameters.AddWithValue("@PinNumber", strVoterPin)
                        mySqlInsert.Parameters.AddWithValue("@FirstName", strFirstName)
                        mySqlInsert.Parameters.AddWithValue("@OtherName", strOtherName)
                        mySqlInsert.Parameters.AddWithValue("@LastName", strLastName)
                        mySqlInsert.ExecuteNonQuery()

                    Catch ex As Exception
                        Try
                            SplashScreenManager.CloseForm()
                        Catch ex1 As Exception

                        End Try
                        strErrorMessage = ex.Message
                    Finally
                        myConnectionString.Close()
                    End Try


                Else
                    SplashScreenManager.CloseForm()
                    MessageBox.Show("Data Columns must be Five :VoterID, VoterPIN, FirstName,OtherName,LastName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                '
            End If

            'Increase counter variable
            count = count + 1
        Next
        generateID()
        generatePIN()
        Try
            SplashScreenManager.CloseForm()
        Catch ex As Exception

        End Try

        MessageBox.Show(count & " Records Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    '*************************************************************************



    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 0 Then
            enableButtons()
        Else

            btnUpdate.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub


    Private Sub frmCreateVoters_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        isValidCode = False
    End Sub



    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If strVoterLID <> String.Empty Then
            txtVoterID.Text = strVoterLID
            txtFirstName.Text = strVoterFirstName
            txtOtherName.Text = strVoterOtherName
            txtLastName.Text = strVoterLastName
            blnHadData = True
            enableButtons()
            TabControl1.SelectedIndex = 0
            Timer2.Enabled = False
        End If
    End Sub

    Sub enableButtons()
        If blnHadData Then
            btnDelete.Enabled = True
            btnUpdate.Enabled = True
            btnSave.Enabled = False
        Else
            btnDelete.Enabled = False
            btnUpdate.Enabled = False
            btnSave.Enabled = True


        End If
    End Sub

    Private Sub txtPin1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPin4.KeyPress, txtPin3.KeyPress, txtPin2.KeyPress, txtPin1.KeyPress
        If Char.IsControl(e.KeyChar) Then Exit Sub
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPin1_TextChanged(sender As Object, e As EventArgs) Handles txtPin1.TextChanged
        If txtPin1.Text.Length = 1 Then txtPin2.Focus()


    End Sub

    Private Sub txtPin2_TextChanged(sender As Object, e As EventArgs) Handles txtPin2.TextChanged
        If txtPin2.Text.Length = 1 Then txtPin3.Focus()
    End Sub

    Private Sub txtPin3_TextChanged(sender As Object, e As EventArgs) Handles txtPin3.TextChanged
        If txtPin3.Text.Length = 1 Then txtPin4.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        clearProvider()
        If isValid() Then

            If TabControl1.SelectedIndex = 0 Then
                'Insert an entry data
                Dim strVotersID As String = CStr(txtVoterID.Text.Trim).ToUpper
                Dim strFirstName As String = CStr(txtFirstName.Text.Trim).ToUpper
                Dim strPinNo As String = CStr(txtPin1.Text) & CStr(txtPin2.Text) & CStr(txtPin3.Text) & CStr(txtPin4.Text)
                Dim strOtherName As String = String.Empty
                If txtOtherName.Text.Trim <> String.Empty Then
                    strOtherName = CStr(txtOtherName.Text.Trim).ToUpper
                End If
                Dim strLastName As String = CStr(txtLastName.Text.Trim).ToUpper
                Dim strInsert As String = "INSERT INTO tblVoters(VoterID,PinNumber,FirstName,OtherName,LastName) " &
                       "VALUES(@VoterID,@PinNumber,@FirstName,@OtherName,@LastName)"
                Dim mySqlInsert As New SqlCommand(strInsert, myConnectionString)
                Dim strSearch As String = "SELECT * FROM tblVoters WHERE VoterID=@VoterID"
                Dim mySqlDataReader As SqlDataReader = Nothing
                Dim mySqlSearch As New SqlCommand(strSearch, myConnectionString)
                Try

                    If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                    mySqlSearch.Parameters.AddWithValue("@VoterID", txtVoterID.Text)
                    mySqlDataReader = mySqlSearch.ExecuteReader
                    If mySqlDataReader.Read Then
                        mySqlDataReader.Close()
                        If blnUseSystemID Then
                            generateID()
                            MessageBox.Show("Please Reset Controls.. Voter ID already assigned")
                            Exit Sub
                        End If

                        MessageBox.Show("Voter ID has been already assigned " & vbCrLf & "Enter a new Voters ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtVoterID.Focus()
                        Exit Sub
                    Else
                        mySqlDataReader.Close()
                        mySqlInsert.Parameters.AddWithValue("@VoterID", strVotersID)
                        mySqlInsert.Parameters.AddWithValue("@FirstName", strFirstName)
                        mySqlInsert.Parameters.AddWithValue("@OtherName", strOtherName)
                        mySqlInsert.Parameters.AddWithValue("@LastName", strLastName)
                        mySqlInsert.Parameters.AddWithValue("@PinNumber", strPinNo)
                        Dim nRecord As Integer = mySqlInsert.ExecuteNonQuery()
                        MessageBox.Show(nRecord & " Voter successfully registered", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        reSet()
                    End If

                Catch ex As Exception
                    MessageBox.Show("Sorry An Error Occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Finally
                    myConnectionString.Close()
                End Try

            ElseIf TabControl1.SelectedIndex = 1 Then

                insertGridViewData()
                reSet()
            End If
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        clearProvider()
        If isValid() Then
            If TabControl1.SelectedIndex = 0 Then
                'Insert an entry data
                Dim strVotersID As String = CStr(txtVoterID.Text.Trim).ToUpper
                Dim strFirstName As String = CStr(txtFirstName.Text.Trim).ToUpper
                Dim strOtherName As String = String.Empty
                If txtOtherName.Text.Trim <> String.Empty Then
                    strOtherName = CStr(txtOtherName.Text.Trim).ToUpper
                End If
                Dim strLastName As String = CStr(txtLastName.Text.Trim).ToUpper
                Dim strUpdate As String = "UPDATE tblVoters SET " &
                       "FirstName=@FirstName,OtherName=@OtherName,LastName=@LastName WHERE VoterID='" & strVotersID & "'"
                Dim mySqlUpdate As New SqlCommand(strUpdate, myConnectionString)

                Try

                    If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                    mySqlUpdate.Parameters.AddWithValue("@FirstName", strFirstName)
                    mySqlUpdate.Parameters.AddWithValue("@OtherName", strOtherName)
                    mySqlUpdate.Parameters.AddWithValue("@LastName", strLastName)
                    Dim nRecord As Integer = mySqlUpdate.ExecuteNonQuery()
                    MessageBox.Show(nRecord & " Record Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    reSet()
                Catch ex As Exception
                    MessageBox.Show("Sorry An Error Occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Finally
                    myConnectionString.Close()
                End Try



            End If
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If TabControl1.SelectedIndex = 0 Then
            Dim strVoterID As String = txtVoterID.Text.Trim
            Dim myData As New GetData
            If myData.checkVoterStatus(strVoterID) Then
                'Voter has cast a vote
                If MessageBox.Show("The voter you want to delete has already cast a vote." & vbCrLf & "Deleting such record will affect election results" & vbCrLf & "Do you want to Delete anyway?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                Else
                    myData.DeleteVoter(strVoterID)
                    reSet()
                    btnDelete.Enabled = False
                    btnUpdate.Enabled = False
                End If

            Else
                'No voting has been cast
                If MessageBox.Show("Are you sure you want to delete Voter with ID " + strVoterID, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    myData.DeleteVoter(strVoterID)
                    reSet()
                    btnDelete.Enabled = False
                    btnUpdate.Enabled = False
                End If



            End If
        End If
    End Sub

    Private Sub btnGetData_Click(sender As Object, e As EventArgs) Handles btnGetData.Click


        Using frm As frmVotersGet = New frmVotersGet()
            If frm.ShowDialog = DialogResult.OK Then
                txtVoterID.Text = frm.VoterID
                Dim strPin As String = frm.PinNumber
                txtPin1.Text = strPin.Substring(0, 1)
                txtPin2.Text = strPin.Substring(1, 1)
                txtPin3.Text = strPin.Substring(2, 1)
                txtPin4.Text = strPin.Substring(3, 1)
                txtFirstName.Text = frm.FirstName
                txtLastName.Text = frm.LastName
                If frm.OtherName IsNot Nothing Then
                    txtOtherName.Text = frm.OtherName
                End If
                lblStatus.Text = ""
                btnUpdate.Enabled = True
                btnDelete.Enabled = True
                txtVoterID.ReadOnly = True
            End If
        End Using
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reSet()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'close the form
        Me.Close()
    End Sub

    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        Dim openDialog As New OpenFileDialog
        With openDialog
            .Filter = "Excel|*.xlsx"

            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                txtExcelFile.Text = .FileName
                'Get all worksheet names from the Excel file selected using GetSchema of an OleDbConnection
                Dim sourceConnectionString As String = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & txtExcelFile.Text & "';Extended Properties='Excel 12.0 Xml;HDR=YES'", txtExcelFile.Text)
                Dim connection As OleDbConnection = New OleDbConnection(sourceConnectionString)

                Try
                    connection.Open()
                    Dim table As DataTable = connection.GetSchema("Tables")
                    connection.Dispose()

                    'Add each table Name to the Combo box
                    If (table IsNot Nothing And table.Rows.Count > 0) Then
                        Dim row As DataRow
                        cboWorkSheet.Items.Clear()
                        For Each row In table.Rows
                            cboWorkSheet.Items.Add(row("TABLE_NAME").ToString)
                        Next
                    End If

                    cboWorkSheet.SelectedItem = cboWorkSheet.Items(0)
                    DataGridView1.DataSource = String.Empty
                    btnLoad.Enabled = True
                Catch ex As Exception
                    MessageBox.Show("Unable to Load Data." & vbCrLf & ex.Message, "Load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cboWorkSheet.Items.Clear()
                    txtExcelFile.Clear()
                    cboWorkSheet.Text = String.Empty
                    DataGridView1.DataSource = String.Empty
                    btnLoad.Enabled = False
                End Try

            End If
        End With
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        If DataGridView1.Rows.Count > 0 Then
            'For i = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.DataSource = String.Empty
            'Next
        End If

        If txtExcelFile.Text <> String.Empty And cboWorkSheet.Text <> String.Empty Then
            Timer1.Enabled = True
            btnReset.Enabled = False
        End If
    End Sub
End Class