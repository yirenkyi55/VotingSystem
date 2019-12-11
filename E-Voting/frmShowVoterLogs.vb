Imports System.Data.SqlClient
Public Class frmShowVoterLogs
    Private Sub frmShowVoterLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refreshGridView()
    End Sub
    Private Sub refreshGridView()

        Dim myDataSet As New DataSet
        Dim commandSearch As String = "SELECT LastName,OtherName,FirstName,tblElections.VoterID, VoteTime AS [Voted Time],'Successfully Voted' AS [Success] FROM tblVoters INNER JOIN tblElections ON tblVoters.VoterID= tblElections.VoterID"
        Dim da As New SqlDataAdapter(commandSearch, myConnectionString)
        da.Fill(myDataSet)
        grdVoters.DataSource = myDataSet.Tables(0)

        If grdVoters.Rows.Count > 0 Then
            voterLog = "SELECT LastName,OtherName,FirstName,tblElections.VoterID, VoteTime,'Successfully Voted' AS [Success] FROM tblVoters INNER JOIN tblElections ON tblVoters.VoterID= tblElections.VoterID"
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        refreshGridView()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text = String.Empty Then
            refreshGridView()
        Else
            refreshGrid(txtSearch.Text.Trim)
            voterLog = "SELECT LastName,OtherName,FirstName,tblElections.VoterID, VoteTime,'Successfully Voted' AS [Success] FROM tblVoters INNER JOIN tblElections ON tblVoters.VoterID= tblElections.VoterID WHERE tblElections.VoterID LIKE '" & txtSearch.Text.Trim & "%'"
        End If

    End Sub

    Private Sub refreshGrid(ByVal strVoterID)

        Dim myDataSet As New DataSet
        Dim commandSearch As String = "SELECT LastName,OtherName,FirstName,tblElections.VoterID, VoteTime AS [Voted Time],'Successfully Voted' AS [Success] FROM tblVoters INNER JOIN tblElections ON tblVoters.VoterID= tblElections.VoterID WHERE tblElections.VoterID LIKE '" & strVoterID & "%'"
        Dim da As New SqlDataAdapter(commandSearch, myConnectionString)
        da.Fill(myDataSet)
        grdVoters.DataSource = myDataSet.Tables(0)

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            frmPrintVoterLogs.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try

    End Sub

    Private Sub frmShowVoterLogs_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        isValidCode = False
    End Sub
End Class