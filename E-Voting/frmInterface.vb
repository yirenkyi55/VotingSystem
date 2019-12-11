Public Class frmInterface
    Private Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        If MessageBox.Show("Are you sure you want to Exit ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            frmAdminLogin.Show()
            Me.Close()
        End If

    End Sub

    Private Sub mnuConnect_Click(sender As Object, e As EventArgs) Handles mnuConnect.Click
        frmConn.ShowDialog()

    End Sub

    Private Sub mnuBackupRestore_Click(sender As Object, e As EventArgs) Handles menuBackupRestore.Click

        frmCode.ShowDialog()
        If isValidCode Then
            frmBackupRestore.ShowDialog()
        End If
    End Sub
    Private Sub tleCreateAdmin_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles tleCreateAdmin.ItemClick
        frmCode.ShowDialog()
        If isValidCode Then
            frmCreateAdmin.ShowDialog()
        End If
    End Sub

    Private Sub tleRegisterVoters_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles tleRegisterVoters.ItemClick
        frmCode.ShowDialog()
        If isValidCode Then
            frmCreateVoters.ShowDialog()
        End If
    End Sub

    Private Sub tleSettings_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles tleSettings.ItemClick
        frmCode.ShowDialog()
        If isValidCode Then
            frmSettings.ShowDialog()
        End If
    End Sub

    Private Sub tleVoting_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles tleVoting.ItemClick
        frmCode.ShowDialog()
        If isValidCode Then
            frmVoterLogin.Show()
            Me.Close()
        End If
    End Sub

    Private Sub tlePositions_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles tlePositions.ItemClick
        frmCode.ShowDialog()
        If isValidCode Then
            frmRegisterPosition.ShowDialog()
        End If
    End Sub

    Private Sub tleResults_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles tleResults.ItemClick
        frmCode.ShowDialog()
        If isValidCode Then
            frmResults.ShowDialog()
        End If
    End Sub

    Private Sub tleCandidates_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles tleCandidates.ItemClick
        frmCode.ShowDialog()
        If isValidCode Then
            frmRegisterCan.ShowDialog()
        End If

    End Sub

    Private Sub btnVoterLogs_Click(sender As Object, e As EventArgs) Handles btnVoterLogs.Click
        frmCode.ShowDialog()
        If isValidCode Then
            frmShowVoterLogs.ShowDialog()
        End If
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub navElection_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navElection.LinkClicked
        tleSettings_ItemClick(Me, Nothing)
    End Sub

    Private Sub navDatabase_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navDatabase.LinkClicked
        frmConn.ShowDialog()
    End Sub

    Private Sub navRegister_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navRegister.LinkClicked
        tleCreateAdmin_ItemClick(Me, Nothing)
    End Sub

    Private Sub navVoters_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navVoters.LinkClicked
        tleRegisterVoters_ItemClick(Me, Nothing)
    End Sub

    Private Sub navPositions_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navPositions.LinkClicked
        tlePositions_ItemClick(Me, Nothing)
    End Sub

    Private Sub navCandidates_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navCandidates.LinkClicked
        tleCandidates_ItemClick(Me, Nothing)
    End Sub

    Private Sub navStartVoting_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navStartVoting.LinkClicked
        tleVoting_ItemClick(Me, Nothing)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        frmCode.ShowDialog()
        If isValidCode Then
            frmResetSystem.ShowDialog()
        End If
    End Sub
End Class