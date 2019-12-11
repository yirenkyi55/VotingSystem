Imports System.Data
Imports System.Data.SqlClient

Public Class frmVotersGet
    Dim myData As New GetData
    Private blnButtonCreated As Boolean = False
    Public Property VoterID
    Public Property PinNumber
    Public Property FirstName
    Public Property OtherName
    Public Property LastName
    Private Sub refreshGridView()
        Try
            grdVoters.Columns.Remove("btnDelete")
        Catch ex As Exception

        End Try
        Dim myData As GetData = New GetData()
        Dim myDataSet As New DataSet


        myDataSet = myData.GetAllRecords("tblVoters")
        grdVoters.DataSource = myDataSet.Tables(0)
        Dim Delete As DataGridViewButtonColumn = New DataGridViewButtonColumn

        grdVoters.Columns.Add(Delete)
        Delete.HeaderText = "Delete A Voter"
        Delete.Name = "btnDelete"
        Delete.Text = "Delete"
        Delete.UseColumnTextForButtonValue = True
        ' grdVoters.Columns("RecordID").Visible = False
        Me.grdVoters.DefaultCellStyle.ForeColor = Color.Black
        Me.grdVoters.DefaultCellStyle.BackColor = Color.AliceBlue
        Me.grdVoters.DefaultCellStyle.SelectionForeColor = Color.White
        Me.grdVoters.DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 31, 69)
        Me.grdVoters.GridColor = Color.FromArgb(3, 31, 69)


    End Sub

    Sub clearColumns()
        grdVoters.Columns.RemoveAt(0)
        grdVoters.Columns.RemoveAt(1)
        grdVoters.Columns.RemoveAt(2)
        grdVoters.Columns.RemoveAt(3)
        grdVoters.Columns.RemoveAt(4)
        grdVoters.Columns.RemoveAt(5)
    End Sub
    Private Sub refreshSearchGrid(searchBy As String, textSearch As String)
        grdVoters.DataSource = Nothing

        Try
            clearColumns()
        Catch ex As Exception

        End Try
        Dim myData As GetData = New GetData()
        Dim myDataSet As New DataSet

        Try
            Dim strsearch As String = "SELECT * FROM tblVoters WHERE " & searchBy & " LIKE '" & textSearch & "%'"

            Dim da As SqlDataAdapter = New SqlDataAdapter(strsearch, myConnectionString)
            da.Fill(myDataSet)
            grdVoters.DataSource = myDataSet.Tables(0)
            Dim Delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
            grdVoters.Columns.Add(Delete)

            Delete.HeaderText = "Delete A Voter"
            Delete.Name = "btnDelete"
            Delete.Text = "Delete"
            Delete.UseColumnTextForButtonValue = True
            ' grdVoters.Columns("RecordID").Visible = False
            Me.grdVoters.DefaultCellStyle.ForeColor = Color.Black
            Me.grdVoters.DefaultCellStyle.BackColor = Color.AliceBlue
            Me.grdVoters.DefaultCellStyle.SelectionForeColor = Color.White
            Me.grdVoters.DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 31, 69)
            Me.grdVoters.GridColor = Color.FromArgb(3, 31, 69)



        Catch ex As Exception


        End Try


    End Sub
    Private Sub DisableControls()
        If grdVoters.Rows.Count < 1 Then
            btnTransfer.Enabled = False
            txtSearch.Enabled = False
            cboSearch.Enabled = False
        End If
    End Sub
    Private Sub frmVotersGet_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cboSearch.SelectedIndex = 0
        refreshGridView()
        DisableControls()
    End Sub


    Private Sub grdVoters_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdVoters.CellContentClick
        If grdVoters.Columns(e.ColumnIndex).Name = "btnDelete" Then
            Dim strVoterID As String = grdVoters.Rows(e.RowIndex).Cells(0).Value
            If myData.checkVoterStatus(strVoterID) Then
                'Voter has cast a vote
                If MessageBox.Show("The voter you want to delete has already cast a vote." & vbCrLf & "Deleting such record will affect election results" & vbCrLf & "Do you want to Delete anyway?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                Else
                    myData.DeleteVoter(strVoterID)
                    'Refresh the datagridview
                    refreshGridView()
                End If

            Else
                'No voting has been cast
                If MessageBox.Show("Are you sure you want to delete Voter with ID:" + strVoterID, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    myData.DeleteVoter(strVoterID)
                    refreshGridView()
                End If

            End If
        End If
    End Sub



    Private Sub cboSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearch.SelectedIndexChanged
        txtSearch.Focus()
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        VoterID = grdVoters.SelectedRows(0).Cells(0).Value
        PinNumber = grdVoters.SelectedRows(0).Cells(1).Value
        FirstName = grdVoters.SelectedRows(0).Cells(2).Value
        If grdVoters.SelectedRows(0).Cells(3).Value IsNot Nothing Then
            OtherName = grdVoters.SelectedRows(0).Cells(3).Value
        End If
        LastName = grdVoters.SelectedRows(0).Cells(4).Value
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Using frm As frmPrintVoters = New frmPrintVoters
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show("Sorry an Error occured " & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub btnDeleteAll_Click_1(sender As Object, e As EventArgs) Handles btnDeleteAll.Click
        Dim myData As New GetData
        If MessageBox.Show("Deleting voters records might affect voting process" & vbCrLf & "Are you sure you wan to delete all records?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            myData.DeleteAllRecords("tblVoters")
            DisableControls()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text.Trim = String.Empty Then
            Try
                'clearColumns()
            Catch ex As Exception

            End Try
            refreshGridView()

        Else
            refreshSearchGrid(cboSearch.Text, txtSearch.Text.Trim)
        End If

    End Sub


End Class