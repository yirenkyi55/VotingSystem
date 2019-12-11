Imports System.Data.SqlClient
Imports System.Data

Public Class frmRegisterCanGet
    Dim myData As New GetData

    Public Property candidateID
    Public Property Title
    Public Property FirstName
    Public Property OtherName
    Public Property LastName
    Public Property Gender
    Public Property DOB
    Public Property Position
    Public Property Profile
    Private Sub refreshGridView()
        Dim myData As GetData = New GetData()
        Dim myDataSet As New DataSet


        myDataSet = myData.GetAllRecords("tblCandidate")
        dgvCandidate.DataSource = myDataSet.Tables(0)
        Dim Delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        dgvCandidate.Columns.Add(Delete)
        Delete.HeaderText = "Delete"
        Delete.Name = "btnDelete"
        Delete.Text = "Delete"
        Delete.UseColumnTextForButtonValue = True
        dgvCandidate.Columns("Profile").Visible = False
        Me.dgvCandidate.DefaultCellStyle.ForeColor = Color.Black
        Me.dgvCandidate.DefaultCellStyle.BackColor = Color.AliceBlue
        Me.dgvCandidate.DefaultCellStyle.SelectionForeColor = Color.White
        Me.dgvCandidate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 31, 69)
        Me.dgvCandidate.GridColor = Color.FromArgb(3, 31, 69)
    End Sub
    Private Sub DisableControls()
        If dgvCandidate.Rows.Count < 1 Then
            btnTransfer.Enabled = False

        End If
    End Sub

    Private Sub frmRegisterCanGet_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        refreshGridView()
        DisableControls()
    End Sub

    Private Sub dgvCandidate_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCandidate.CellContentClick
        Dim myData As New GetData
        If dgvCandidate.Columns(e.ColumnIndex).Name = "btnDelete" Then
            Dim intCandidateID As Integer = CInt(dgvCandidate.Rows(e.RowIndex).Cells(0).Value)
            If myData.DeleteCandidate(intCandidateID) Then
                refreshGridView()
            End If
        End If
    End Sub







    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        candidateID = CInt(dgvCandidate.SelectedRows(0).Cells(0).Value)
        Title = dgvCandidate.SelectedRows(0).Cells(1).Value
        FirstName = dgvCandidate.SelectedRows(0).Cells(2).Value
        OtherName = dgvCandidate.SelectedRows(0).Cells(3).Value
        LastName = dgvCandidate.SelectedRows(0).Cells(4).Value
        Gender = dgvCandidate.SelectedRows(0).Cells(5).Value
        DOB = dgvCandidate.SelectedRows(0).Cells(6).Value
        Position = dgvCandidate.SelectedRows(0).Cells(7).Value
        Profile = dgvCandidate.SelectedRows(0).Cells(8).Value
    End Sub

    Private Sub btnDeleteAll_Click(sender As Object, e As EventArgs) Handles btnDeleteAll.Click
        Dim myData As New GetData
        If myData.DeleteTableRecords("tblCandidate") Then
            refreshGridView()
            DisableControls()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class