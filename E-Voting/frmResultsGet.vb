Imports System.Data
Imports System.Data.SqlClient
Public Class frmResultsGet

    Private Sub btnExit_Click_1(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            frmPrintDetailsResults.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "error")
        End Try

    End Sub

    Private Sub frmResultsGet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refreshGridView(intPrintPositionID)
    End Sub
    Private Sub refreshGridView(ByVal intPositionID As Integer)

        Dim myDataSet As New DataSet
        Dim commandSearch As String = "SELECT LastName, OtherName,FirstName,ISNULL(YesNo,'') AS [YES / NO],VoteTime FROM tblCandidate inner join tblElections on tblCandidate.CandidateID = tblElections.CandidateID WHERE tblElections.PositionID = " & intPositionID & " ORDER BY LastName,FirstName,YesNo DESC"
        Dim da As New SqlDataAdapter(commandSearch, myConnectionString)
        da.Fill(myDataSet)
        DataGridView1.DataSource = myDataSet.Tables(0)

    End Sub
End Class