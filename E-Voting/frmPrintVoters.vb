Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmPrintVoters
    Private Sub frmPrintVoters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reportInfo As New ReportInformation
        reportInfo.LoadAssocInfo()
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            Dim ds As New DataSet

            Dim strsearch As String = "SELECT FirstName,OtherName,LastName,VoterID,PinNumber FROM tblVoters ORDER BY LastName, FirstName"
            Dim da As New SqlDataAdapter(strsearch, myConnectionString)
            da.Fill(ds)
            myConnectionString.Close()
            Dim myReportInfo As New crptVoters
            myReportInfo.SetDataSource(ds.Tables(0))
            myReportInfo.Database.Tables("dsVoters").SetDataSource(ds.Tables(0))
            myReportInfo.SetParameterValue("AssociationName", reportInfo.AssociationName)
            Me.CrystalReportViewer1.ReportSource = Nothing
            Me.CrystalReportViewer1.ReportSource = myReportInfo
            Me.CrystalReportViewer1.ShowNextPage()
            Me.CrystalReportViewer1.ShowPreviousPage()

            Me.CrystalReportViewer1.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error" & vbCrLf & ex.Message)
        Finally
            myConnectionString.Close()
        End Try



    End Sub
End Class