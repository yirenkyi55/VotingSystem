Imports System.Data.SqlClient
Public Class frmPrintVoterLogs
    Private Sub frmPrintVoterLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim reportInfo As New ReportInformation
        reportInfo.LoadAssocInfo()
        If voterLog <> String.Empty Then
            Try
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                Dim ds As New DataSet


                Dim da As New SqlDataAdapter(voterLog, myConnectionString)
                da.Fill(ds)
                myConnectionString.Close()
                Dim myReportInfo As New crptVoterLogs
                myReportInfo.SetDataSource(ds.Tables(0))

                myReportInfo.SetParameterValue("AssociationName", reportInfo.AssociationName & " ELECTIONS")
                ' myReportInfo.Database.Tables("tblVoterLogs").SetDataSource(ds.Tables(0))
                Me.CrystalReportViewer1.ReportSource = Nothing
                Me.CrystalReportViewer1.ReportSource = myReportInfo
                Me.CrystalReportViewer1.Refresh()
            Catch ex As Exception
                MessageBox.Show("Error" & vbCrLf & ex.Message)
            Finally
                myConnectionString.Close()
            End Try

        End If

    End Sub
End Class