Public Class frmResetSystem
    Private mGetData As New GetData
    Private Sub frmResetSystem_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        isValidCode = False
    End Sub
    Private Sub btnVoters_Click(sender As Object, e As EventArgs) Handles btnVoters.Click
        If MessageBox.Show("You are about to reset all voters records. Are you sure you want to delete ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            'Delete all records from voters table
            If mGetData.DeleteRecords("tblVoters") Then
                MessageBox.Show("Voters records has been successfully reset")
            End If

        End If
    End Sub

    Private Sub btnCandidate_Click(sender As Object, e As EventArgs) Handles btnCandidate.Click
        If MessageBox.Show("You are about to reset all candidate records. Are you sure you want to delete ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            'Delete all records from candidate table
            If mGetData.DeleteRecords("tblCandidate") Then
                mGetData.DeleteAllCandidateProfile()
                MessageBox.Show("Candidate records has been successfully reset")
            End If
        End If
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        If MessageBox.Show("You are about to reset election settings. Are you sure you want to delete ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            'Delete all records from Association information.
            If mGetData.DeleteRecords("tblAssociationInfo") Then
                MessageBox.Show("Association information has been successfully reset")
            End If
        End If
    End Sub

    Private Sub btnPositions_Click(sender As Object, e As EventArgs) Handles btnPositions.Click
        If MessageBox.Show("You are about to reset position records. Are you sure you want to delete ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            'Delete all records from tblVoters
            If mGetData.DeleteRecords("tblPositions") Then
                MessageBox.Show("Position records has been successfully reset")
            End If
        End If
    End Sub

    Private Sub btnBallots_Click(sender As Object, e As EventArgs) Handles btnBallots.Click
        If MessageBox.Show("You are about to delete all voting records. Are you sure you want to delete ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            'Delete all records from tblVoters
            If mGetData.DeleteRecords("tblElections") Then
                If mGetData.DeleteRecords("tblVoterLogin") Then

                End If
                MessageBox.Show("Ballot records has been successfully reset")

            End If
        End If
    End Sub

    Private Sub btnResetAll_Click(sender As Object, e As EventArgs) Handles btnResetAll.Click
        Try
            If MessageBox.Show("You are about to reset the voting system. Are you sure you want to delete records ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                'Delete all records from System
                If mGetData.DeleteRecords("tblAssociationInfo") Then
                    mGetData.DeleteAllCandidateProfile()

                    If mGetData.DeleteRecords("tblCandidate") Then

                        If mGetData.DeleteRecords("tblElections") Then
                            If mGetData.DeleteRecords("tblPositions") Then
                                If mGetData.DeleteRecords("tblVoters") Then
                                    If mGetData.DeleteRecords("tblVoterLogin") Then

                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                MessageBox.Show("All system components has been successfully reset")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Me.Close()
    End Sub

    Private Sub frmResetSystem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class