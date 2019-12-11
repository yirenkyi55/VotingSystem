Imports System.Data.SqlClient
Public Class frmPrintResultsCan

    Private Sub frmPrintResultsCan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reportInfo As New ReportInformation

        reportInfo.LoadAssocInfo()
        Try

            Dim myReportInfo As New cryptCandidateInfo

            myReportInfo.SetParameterValue("AssociationName", reportInfo.AssociationName & " ELECTIONS")

            myReportInfo.SetParameterValue("sectionName", portfololioName)
            myReportInfo.SetParameterValue("totalVotesCast", "TOTAL VALID VOTES CAST: " & totalValidVotesCast)
            myReportInfo.SetParameterValue("totalVotesSkip", "TOTAL VOTES SKIPPED: " & totalVotesSkip)

            myReportInfo.SetParameterValue("can1Pic", can1Pic)
            myReportInfo.SetParameterValue("can1Name", candidate1Name)
            myReportInfo.SetParameterValue("can1NumberOfVotes", can1NoOfVote)
            myReportInfo.SetParameterValue("can1PercentageOfVotes", can1Percent)

            myReportInfo.SetParameterValue("can2Pic", can2Pic)
            myReportInfo.SetParameterValue("can2Name", candidate2Name)
            myReportInfo.SetParameterValue("can2NumberOfVotes", can2NoOfVote)
            myReportInfo.SetParameterValue("can2PercentageOfVotes", can2Percent)

            myReportInfo.SetParameterValue("can3Pic", can3Pic)
            myReportInfo.SetParameterValue("can3Name", candidate3Name)
            myReportInfo.SetParameterValue("can3NumberOfVotes", can3NoOfVote)
            myReportInfo.SetParameterValue("can3PercentageOfVotes", can3Percent)

            myReportInfo.SetParameterValue("can4Pic", can4Pic)
            myReportInfo.SetParameterValue("can4Name", candidate4Name)
            myReportInfo.SetParameterValue("can4NumberOfVotes", can4NoOfVote)
            myReportInfo.SetParameterValue("can4PercentageOfVotes", can4Percent)

            myReportInfo.SetParameterValue("can5Pic", can5Pic)
            myReportInfo.SetParameterValue("can5Name", candidate5Name)
            myReportInfo.SetParameterValue("can5NumberOfVotes", can5NoOfVote)
            myReportInfo.SetParameterValue("can5PercentageOfVotes", can5Percent)

            myReportInfo.SetParameterValue("can6Pic", can6Pic)
            myReportInfo.SetParameterValue("can6Name", candidate6Name)
            myReportInfo.SetParameterValue("can6NumberOfVotes", can6NoOfVote)
            myReportInfo.SetParameterValue("can6PercentageOfVotes", can6Percent)


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