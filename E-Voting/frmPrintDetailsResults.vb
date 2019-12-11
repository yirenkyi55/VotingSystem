Imports System.Data.SqlClient
Public Class frmPrintDetailsResults
    Private Sub frmPrintDetailsResults_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reportInfo As New ReportInformation
        reportInfo.LoadAssocInfo()
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            Dim ds As New DataSet

            Dim commandSearch As String = "SELECT tblCandidate.CandidateID AS [CandidateID],LastName, OtherName,FirstName,ISNULL(YesNo,'') AS [YES / NO],VoteTime,tblCandidate.CandidateID AS [CandidateID] FROM tblCandidate inner join tblElections on tblCandidate.CandidateID = tblElections.CandidateID WHERE tblElections.PositionID = " & intPrintPositionID & " ORDER BY LastName,FirstName,YesNo DESC"
            Dim da As New SqlDataAdapter(commandSearch, myConnectionString)
            da.Fill(ds)
            myConnectionString.Close()
            Dim myReportInfo As New cryptDetailsResults
            myReportInfo.SetDataSource(ds.Tables(0))

            myReportInfo.SetParameterValue("AssociationName", reportInfo.AssociationName & " ELECTIONS")
            myReportInfo.SetParameterValue("sectionName", portfololioName)
            Me.CrystalReportViewer1.ReportSource = Nothing
            Me.CrystalReportViewer1.ReportSource = myReportInfo
            Me.CrystalReportViewer1.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error" & vbCrLf & ex.Message)
        Finally
            myConnectionString.Close()
        End Try
    End Sub
End Class