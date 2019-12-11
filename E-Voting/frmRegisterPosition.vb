Imports System.Data
Imports System.Data.SqlClient
Public Class frmRegisterPosition
    Private blnHasRecord As Boolean = False


    Private Sub refreshGridView()
        Dim myData As GetData = New GetData()
        Dim myDataSet As New DataSet


        myDataSet = myData.GetAllRecords("tblPositions")
        DataGridView1.DataSource = myDataSet.Tables(0)
        Dim Delete As DataGridViewButtonColumn = New DataGridViewButtonColumn

        DataGridView1.Columns("PositionID").Visible = False
        Me.DataGridView1.DefaultCellStyle.ForeColor = Color.Black
        Me.DataGridView1.DefaultCellStyle.BackColor = Color.AliceBlue
        Me.DataGridView1.DefaultCellStyle.SelectionForeColor = Color.White
        Me.DataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 31, 69)
        Me.DataGridView1.GridColor = Color.FromArgb(3, 31, 69)
    End Sub
    Private Sub frmRegisterPosition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refreshGridView()
        generateID()
        enableMenue()
        enableDeleteUpdate()
        reset()

    End Sub

    Sub enableMenue()
        If DataGridView1.Rows.Count > 0 Then
            mnuFileClear.Enabled = True
        Else
            mnuFileClear.Enabled = False
        End If

    End Sub

    Sub enableDeleteUpdate()
        If blnHasRecord Then
            btnDelete.Enabled = True
            btnSave.Enabled = False
            btnUpdate.Enabled = True
        Else
            btnDelete.Enabled = False
            btnSave.Enabled = True
            btnUpdate.Enabled = False
            txtPosition.Focus()
        End If

    End Sub


    Private Function isValid() As Boolean
        If txtPosition.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(txtPosition, "Please Register Position")
            lblStatus.Text = "Please Register Position"
            Return False
        End If

        Return True
    End Function

    Sub generateID()
        Dim strRecordName As String = "PositionID"
        Dim strTableName As String = "tblPositions"
        lblRecordID.Text = CStr(eGenerateID(strRecordName, strTableName))
    End Sub


    Sub reset()
        refreshGridView()
        generateID()
        txtPosition.Clear()
        txtPosition.Focus()
        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
        blnHasRecord = False
        enableDeleteUpdate()
        blnHasRecord = True
    End Sub


    Private Sub mnuFileExit_Click(sender As Object, e As EventArgs) Handles mnuFileExit.Click
        'close the form
        Me.Close()
    End Sub

    Private Sub mnuFileClear_Click(sender As Object, e As EventArgs) Handles mnuFileClear.Click
        Delete()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If blnHasRecord = True Then
            Try
                Dim intRecordID As Integer = CInt(DataGridView1.SelectedRows(0).Cells(0).Value.ToString)
                Dim strPosition As String = CStr(DataGridView1.SelectedRows(0).Cells(1).Value.ToString)
                lblRecordID.Text = intRecordID
                txtPosition.Text = strPosition
                blnHasRecord = True
                enableDeleteUpdate()
            Catch ex As Exception

            End Try
        End If


    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        'Try
        '    Dim mRow As DataGridViewRow = DataGridView1.SelectedRows(e.RowIndex)
        '    lblRecordID.Text = CInt(mRow.Cells(0).Value.ToString)
        '    txtPosition.Text = CStr(mRow.Cells(1).Value.ToString)
        '    blnHasRecord = True
        '    enableDeleteUpdate()
        'Catch ex As Exception

        'End Try

    End Sub





    Private Sub Delete()
        Dim myData As New GetData
        If MessageBox.Show("Deleting Position records might affect voting process" & vbCrLf & "Are you sure you want to delete all records?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            myData.DeleteAllRecords("tblPositions")

        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
        If isValid() Then
            Dim strPosition As String = CStr(txtPosition.Text.Trim).ToUpper
            Dim strInsert As String = "INSERT INTO tblPositions(PositionID,[Name]) VALUES(@PositionID,@Name)"
            Dim intRecordID As Integer = CInt(lblRecordID.Text.Trim)
            Dim mySqlInsert As New SqlCommand(strInsert, myConnectionString)
            Dim strSearch As String = "SELECT * FROM tblPositions WHERE [Name]=@Name"
            Dim mySqlSearch As New SqlCommand(strSearch, myConnectionString)
            mySqlSearch.Parameters.AddWithValue("@Name", strPosition)
            Dim mySqlDataReader As SqlDataReader = Nothing

            Try
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                mySqlDataReader = mySqlSearch.ExecuteReader
                If mySqlDataReader.Read Then
                    mySqlDataReader.Close()
                    MessageBox.Show("Position has been already registered" & vbCrLf & "Please Register New Position", "Register", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                Else
                    mySqlDataReader.Close()
                    'Save into the database
                    mySqlInsert.Parameters.AddWithValue("@PositionID", intRecordID)
                    mySqlInsert.Parameters.AddWithValue("@Name", strPosition)
                    Dim nRecord As Integer = mySqlInsert.ExecuteNonQuery()
                    MessageBox.Show(nRecord & " Record Inserted", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    reset()
                    enableMenue()
                End If
            Catch ex As Exception

            Finally
                myConnectionString.Close()
            End Try
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        ErrorProvider1.Clear()
        lblStatus.Text = String.Empty
        If isValid() Then
            Dim strPosition As String = CStr(txtPosition.Text.Trim).ToUpper
            Dim intRecordID As Integer = CInt(lblRecordID.Text.Trim)
            Dim strUpdate As String = "UPDATE tblPositions SET [Name]=@Name WHERE PositionID=@PositionID"
            Dim mySqlUppdate As New SqlCommand(strUpdate, myConnectionString)
            mySqlUppdate.Parameters.AddWithValue("@Name", strPosition)
            mySqlUppdate.Parameters.AddWithValue("@PositionID", intRecordID)
            Try
                If MessageBox.Show("Are you sure you want to update Position?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                    'Save into the database                
                    Dim nRecord As Integer = mySqlUppdate.ExecuteNonQuery()
                    MessageBox.Show(nRecord & " Record Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Me.PositionsTableAdapter.Fill(Me.EVotingDbDataSet.Positions)
                    reset()
                    enableMenue()

                End If

            Catch ex As Exception
                MessageBox.Show("Sorry an Error occured." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                myConnectionString.Close()
            End Try
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim intRecordID As Integer = CInt(lblRecordID.Text.Trim)

        Dim strDelete As String = "DELETE FROM tblPositions WHERE PositionID=@PositionID"
        Dim mySqlDelete As New SqlCommand(strDelete, myConnectionString)
        mySqlDelete.Parameters.AddWithValue("@PositionID", intRecordID)
        If MessageBox.Show("Deleting a position will delete any candidate record registered under the position." & vbCrLf & "Are you sure you want to Delete anyway?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                'Delete From  the database
                Dim nRecord As Integer = mySqlDelete.ExecuteNonQuery()
                MessageBox.Show(nRecord & " Record Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                reset()
                enableMenue()
            Catch ex As Exception

            Finally
                myConnectionString.Close()
            End Try
        End If

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class