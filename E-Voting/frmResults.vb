Imports System.Data
Imports System.Data.SqlClient

Public Class frmResults
    Private blnIsLoaded As Boolean = False
    Private intNumPanel As Integer = 0
    Private intLoopThrough As Integer = 0 'Used to Step Through the Positions one after the other
    ' Retrieve the working rectangle from the Screen class using the        PrimaryScreen and the WorkingArea properties.  
    Dim workingRectangle As System.Drawing.Rectangle = Screen.PrimaryScreen.WorkingArea


    Sub view()
        frmProgress.ShowDialog()
        Dim PositionID As Integer = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key
        Dim strPositionName As String = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Value
        enablePanCandidates(PositionID, strPositionName) 'Show the Positions for the various candidates

        LoadTotalVotesCast() 'Determine the total Vote cast and display it
        LoadTotalVotesForCandidate()
        lblSkip.Text = countTotalVotesSkiped(CInt(DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key))
    End Sub
    Sub loadFirstTotal()
        If cboSelect.Items.Count > 0 Then
            Dim PositionID As Integer = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key
            Dim strPositionName As String = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Value
            enablePanCandidates(PositionID, strPositionName) 'Show the Positions for the various candidates

            LoadTotalVotesCast() 'Determine the total Vote cast and display it
            LoadTotalVotesForCandidate()
            blnIsLoaded = True
        End If

    End Sub
    Private Function isValid() As Boolean
        If cboSelect.Items.Count = 0 Then
            MessageBox.Show("Please Register Candidates", "NOTE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        If cboSelect.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(cboSelect, "Please Select Position")
            Return False
        End If
        Return True
    End Function

    Private Sub frmResults_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the size of the form slightly less than size of working rectangle. 
        Me.Size = New System.Drawing.Size(workingRectangle.Width - 5, workingRectangle.Height - 5)
        ' Set the location so the entire form is visible. 
        Me.Location = New System.Drawing.Point(3, 3)
        LoadPosition() 'Load the various positions into the combo box
        LoadFirstPosition()
        ResetControls()
        determinePositions()
        'Timer1.Enabled = True
        'Determine the total vote cast for each candidate and their various percentages
    End Sub

    Sub determinePositions()
        If cboSelect.Items.Count = 0 Then
            lblPosition.Text = "No Candidate Registered!! Please Register Candidate"
        End If
    End Sub
    Sub resetControlPanel2()
        panCandidate1.Visible = False
        lblCan2vote.Text = String.Empty
        lblCan2per.Text = String.Empty
    End Sub

    Sub ResetControls()
        lblCan1vote.Text = String.Empty
        lblCan1per.Text = String.Empty
        lblTotal.Text = String.Empty

        lblCan2vote.Text = String.Empty
        lblCan2per.Text = String.Empty

        lblCan3vote.Text = String.Empty
        lblCan3per.Text = String.Empty

        lblCan4vote.Text = String.Empty
        lblCan4per.Text = String.Empty

        lblCan5vote.Text = String.Empty
        lblCan5per.Text = String.Empty

        lblCan6vote.Text = String.Empty
        lblCan6per.Text = String.Empty


    End Sub
    Sub LoadFirstPosition()
        If cboSelect.Items.Count > 0 Then
            Dim PositionID As Integer = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key
            Dim strPositiionName As String = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Value
            enablePanCandidates(PositionID, strPositiionName) 'Show the Positions for the various candidates


        End If
    End Sub

    'Count the total Number of votes on each portfolio as the form loads
    Sub LoadTotalVotesCast()
        Try
            If cboSelect.Items.Count > 0 Then
                Dim PositionID As Integer = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key
                Dim intTotalVotesCast As Integer = countTotalVotesCast(PositionID)
                lblTotal.Text = intTotalVotesCast.ToString("d")
            End If
        Catch ex As Exception

        End Try

    End Sub

    'A sub procedure to Determine the total votes received for each candidate
    Sub LoadTotalVotesForCandidate()
        If cboSelect.Items.Count > 0 Then
            Dim strPosition As String = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Value
            'Load The total Votes only when the panels are visible
            If panCandidate1.Visible = True Then
                Dim strCandidateName As String = CStr(lblCan1.Text)
                Dim intCandidateID As Integer = CInt(lblCanID1.Text)
                Dim PositionID As Integer = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key
                Dim intCountVotes As Integer = countTotalVotesForCandidate(PositionID, intCandidateID)

                lblCan1vote.Text = intCountVotes & " Vote(s)" 'Deteriming the total votes gained
                Dim intTotalVotesCast As Integer = CInt(lblTotal.Text) 'Holds the total votes Cast
                Dim dblPercent As Double = 0
                If intTotalVotesCast = 0 Then
                    lblCan1per.Text = "0.0%"

                Else
                    dblPercent = CDbl(intCountVotes / intTotalVotesCast)
                    lblCan1per.Text = CStr(dblPercent.ToString("p2"))
                End If
            End If

            If panCandidate2.Visible = True Then
                Dim strCandidateName As String = CStr(lblCan2.Text)
                Dim intCandidateID As Integer = CInt(lblCanID2.Text)
                Dim PositionID As Integer = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key
                Dim intCountVotes As Integer = countTotalVotesForCandidate(PositionID, intCandidateID)
                lblCan2vote.Text = intCountVotes & " Vote(s)" 'Deteriming the total votes gained
                Dim intTotalVotesCast As Integer = CInt(lblTotal.Text) 'Holds the total votes Cast
                Dim dblPercent As Double = 0
                If intTotalVotesCast = 0 Then
                    lblCan2per.Text = "0.0%"
                Else
                    dblPercent = CDbl(intCountVotes / intTotalVotesCast)
                    lblCan2per.Text = CStr(dblPercent.ToString("p2"))
                End If
            End If

            If panCandidate3.Visible = True Then
                Dim strCandidateName As String = CStr(lblCan3.Text)
                Dim intCandidateID As Integer = CInt(lblCanID3.Text)
                Dim PositionID As Integer = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key
                Dim intCountVotes As Integer = countTotalVotesForCandidate(PositionID, intCandidateID)
                lblCan3vote.Text = intCountVotes & " Vote(s)" 'Deteriming the total votes gained
                Dim intTotalVotesCast As Integer = CInt(lblTotal.Text) 'Holds the total votes Cast
                Dim dblPercent As Double = 0
                If intTotalVotesCast = 0 Then
                    lblCan3per.Text = "0.0%"
                Else
                    dblPercent = CDbl(intCountVotes / intTotalVotesCast)
                    lblCan3per.Text = CStr(dblPercent.ToString("p2"))
                End If
            End If

            If panCandidate4.Visible = True Then
                Dim strCandidateName As String = CStr(lblCan4.Text)
                Dim intCandidateID As Integer = CInt(lblCanID4.Text)
                Dim PositionID As Integer = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key
                Dim intCountVotes As Integer = countTotalVotesForCandidate(PositionID, intCandidateID)
                lblCan4vote.Text = intCountVotes & " Vote(s)" 'Deteriming the total votes gained
                Dim intTotalVotesCast As Integer = CInt(lblTotal.Text) 'Holds the total votes Cast
                Dim dblPercent As Double = 0
                If intTotalVotesCast = 0 Then
                    lblCan4per.Text = "0.0%"
                Else
                    dblPercent = CDbl(intCountVotes / intTotalVotesCast)
                    lblCan4per.Text = CStr(dblPercent.ToString("p2"))
                End If
            End If

            If panCandidate5.Visible = True Then
                Dim strCandidateName As String = CStr(lblCan5.Text)
                Dim intCandidateID As Integer = CInt(lblCanID5.Text)
                Dim PositionID As Integer = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key
                Dim intCountVotes As Integer = countTotalVotesForCandidate(PositionID, intCandidateID)
                lblCan5vote.Text = intCountVotes & " Vote(s)" 'Deteriming the total votes gained
                Dim intTotalVotesCast As Integer = CInt(lblTotal.Text) 'Holds the total votes Cast
                Dim dblPercent As Double = 0
                If intTotalVotesCast = 0 Then
                    lblCan5per.Text = "0.0%"
                Else
                    dblPercent = CDbl(intCountVotes / intTotalVotesCast)
                    lblCan5per.Text = CStr(dblPercent.ToString("p2"))
                End If
            End If

            If panCandidate6.Visible = True Then
                Dim strCandidateName As String = CStr(lblCan6.Text)
                Dim intCandidateID As Integer = CInt(lblCanID6.Text)
                Dim PositionID As Integer = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key
                Dim intCountVotes As Integer = countTotalVotesForCandidate(PositionID, intCandidateID)
                lblCan6vote.Text = intCountVotes & " Vote(s)" 'Deteriming the total votes gained
                Dim intTotalVotesCast As Integer = CInt(lblTotal.Text) 'Holds the total votes Cast
                Dim dblPercent As Double = 0
                If intTotalVotesCast = 0 Then
                    lblCan6per.Text = "0.0%"
                Else
                    dblPercent = CDbl(intCountVotes / intTotalVotesCast)
                    lblCan6per.Text = CStr(dblPercent.ToString("p2"))
                End If
            End If


        End If

    End Sub

    Sub LoadPosition()
        cboSelect.DataSource = Nothing
        Dim intCount As Integer = 0
        Dim strSelect As String = "SELECT * FROM tblPositions ORDER BY PositionID"
        Dim strCount As String = "SELECT COUNT(PositionID) FROM [tblPositions] "
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)
        Dim mySqlCount As New SqlCommand(strCount, myConnectionString)
        Dim mySqlDataReader As SqlDataReader = Nothing
        Dim myPositionItems As New Dictionary(Of Int32, String)
        Dim blnRead As Boolean = False

        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            mySqlDataReader = mySqlSelect.ExecuteReader
            While mySqlDataReader.Read
                myPositionItems.Add(CInt(mySqlDataReader.Item("PositionID")), CStr(mySqlDataReader.Item("Name")))
                blnRead = True
            End While
            If blnRead Then
                cboSelect.DataSource = New BindingSource(myPositionItems, Nothing)
                cboSelect.DisplayMember = "value"
                cboSelect.ValueMember = "key"
            End If
            mySqlDataReader.Close()
        Catch ex As Exception
        Finally
            myConnectionString.Close()

        End Try
    End Sub

    'Reset the panels
    Sub resetPanel()
        panCandidate1.Visible = False
        panCandidate2.Visible = False
        panCandidate3.Visible = False
        panCandidate4.Visible = False
        panCandidate5.Visible = False
        panCandidate6.Visible = False
    End Sub

    '************************************************
    Sub enablePanCandidates(ByVal PositionID As Integer, strPositionName As String)
        'Set the number of panel, Use to determine a yes/no voting type
        Dim intNumberofCan As Integer = 0 'Use to determine the number of candidate in each portfolio
        resetPanel() 'Reset the panel
        lblPosition.Text = strPositionName.Trim.ToUpper
        Dim strFirstName As String = String.Empty
        Dim strOtherName As String = String.Empty
        Dim strLastName As String = String.Empty
        Dim strProfile As String = String.Empty
        Dim strTitle As String = String.Empty
        Dim intCandidateID As Integer

        Dim strSelect As String = "SELECT * FROM tblCandidate WHERE PositionID=" & PositionID & " ORDER BY CandidateID"
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)
        Dim mySqlDataReader As SqlDataReader = Nothing
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            mySqlDataReader = mySqlSelect.ExecuteReader
            While mySqlDataReader.Read
                intNumberofCan += 1 ' A candidate has been identified
                strFirstName = CStr(mySqlDataReader.Item("FirstName"))
                If mySqlDataReader.Item("Title") IsNot DBNull.Value Then
                    strTitle = CStr(mySqlDataReader.Item("Title") + " ").TrimStart
                End If

                If mySqlDataReader.Item("OtherName") IsNot DBNull.Value Then
                    strOtherName = CStr(mySqlDataReader.Item("OtherName"))
                Else
                    strOtherName = String.Empty
                End If
                strLastName = CStr(mySqlDataReader.Item("LastName"))
                strProfile = CStr(mySqlDataReader.Item("Profile"))
                intCandidateID = CInt(mySqlDataReader.Item("CandidateID"))

                Select Case intNumberofCan
                    Case 1
                        panCandidate1.Visible = True
                        lblCan1.Text = strTitle & strLastName & " " & strOtherName & " " & strFirstName
                        lblCanID1.Text = intCandidateID.ToString
                        Dim BMP As Bitmap
                        BMP = Bitmap.FromFile(strPath & strProfile)
                        can1Pic = strPath & strProfile
                        picCan1.Image = New Bitmap(BMP)
                        BMP.Dispose()
                        GC.Collect()
                        intNumPanel = 1

                    Case 2
                        panCandidate1.Visible = True
                        panCandidate2.Visible = True
                        lblCan2.Text = strTitle & strLastName & " " & strOtherName & " " & strFirstName
                        lblCanID2.Text = intCandidateID.ToString
                        Dim BMP As Bitmap
                        BMP = Bitmap.FromFile(strPath & strProfile)
                        can2Pic = strPath & strProfile
                        picCan2.Image = New Bitmap(BMP)
                        BMP.Dispose()
                        GC.Collect()
                        intNumPanel = 2
                    Case 3
                        panCandidate1.Visible = True
                        panCandidate2.Visible = True
                        panCandidate3.Visible = True
                        lblCan3.Text = strTitle & strLastName & " " & strOtherName & " " & strFirstName
                        lblCanID3.Text = intCandidateID.ToString
                        Dim BMP As Bitmap
                        BMP = Bitmap.FromFile(strPath & strProfile)
                        can3Pic = strPath & strProfile
                        picCan3.Image = New Bitmap(BMP)
                        BMP.Dispose()
                        GC.Collect()
                        intNumPanel = 3
                    Case 4
                        panCandidate1.Visible = True
                        panCandidate2.Visible = True
                        panCandidate3.Visible = True
                        panCandidate4.Visible = True
                        lblCan4.Text = strTitle & strLastName & " " & strOtherName & " " & strFirstName
                        lblCanID4.Text = intCandidateID.ToString
                        Dim BMP As Bitmap
                        BMP = Bitmap.FromFile(strPath & strProfile)
                        can4Pic = strPath & strProfile
                        picCan4.Image = New Bitmap(BMP)
                        BMP.Dispose()
                        GC.Collect()
                        intNumPanel = 4
                    Case 5
                        panCandidate1.Visible = True
                        panCandidate2.Visible = True
                        panCandidate3.Visible = True
                        panCandidate4.Visible = True
                        panCandidate5.Visible = True
                        lblCanID5.Text = intCandidateID.ToString
                        lblCan5.Text = strTitle & strLastName & " " & strOtherName & " " & strFirstName
                        Dim BMP As Bitmap
                        BMP = Bitmap.FromFile(strPath & strProfile)
                        can5Pic = strPath & strProfile
                        picCan5.Image = New Bitmap(BMP)
                        BMP.Dispose()
                        GC.Collect()
                        intNumPanel = 5
                    Case 6
                        panCandidate1.Visible = True
                        panCandidate2.Visible = True
                        panCandidate3.Visible = True
                        panCandidate4.Visible = True
                        panCandidate5.Visible = True
                        panCandidate6.Visible = True
                        lblCanID6.Text = intCandidateID.ToString
                        lblCan6.Text = strTitle & strLastName & " " & strOtherName & " " & strFirstName
                        Dim BMP As Bitmap
                        BMP = Bitmap.FromFile(strPath & strProfile)
                        picCan6.Image = New Bitmap(BMP)
                        can6Pic = strPath & strProfile
                        BMP.Dispose()
                        GC.Collect()
                        intNumPanel = 6

                End Select
            End While

            If intNumPanel = 1 Then
                resetPanel()
                panCandidate2.Visible = True
                lblCan2.Text = strTitle & strLastName & " " & strOtherName & " " & strFirstName

                lblCanID2.Text = intCandidateID.ToString
                Dim BMP As Bitmap
                BMP = Bitmap.FromFile(strPath & strProfile)
                can2Pic = strPath & strProfile
                can1Pic = String.Empty
                can3Pic = String.Empty
                can4Pic = String.Empty
                can5Pic = String.Empty
                can6Pic = String.Empty
                picCan2.Image = New Bitmap(BMP)
                BMP.Dispose()
                GC.Collect()
                intNumPanel = 1

            End If
        Catch ex As Exception
            MessageBox.Show("Sorry an Error Occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            myConnectionString.Close()
        End Try

    End Sub

    'Count the total Votes Cast In the Selected Potfolio
    Private Function countTotalVotesCast(ByVal strPositionID As Integer) As Integer
        Dim intVotes As Integer = 0
        Dim strSelect As String = "SELECT COUNT(ElectionID) FROM tblElections WHERE PositionID=" & strPositionID & " AND CandidateID IS NOT NULL"
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)

        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()

            If mySqlSelect.ExecuteScalar IsNot DBNull.Value Then
                intVotes = CInt(mySqlSelect.ExecuteScalar)

            End If

        Catch ex As Exception
            MessageBox.Show("Sorry an Error Occred." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            myConnectionString.Close()
        End Try

        Return intVotes
    End Function

    '*********************************
    'Count the total Votes Skipped In the Selected Potfolio
    Private Function countTotalVotesSkiped(ByVal intPositionID As Integer) As Integer
        Dim intVotes As Integer = 0
        Dim strSelect As String = "SELECT COUNT(*) FROM tblElections WHERE PositionID=" & intPositionID & " AND CandidateID IS NULL"
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            If mySqlSelect.ExecuteScalar IsNot DBNull.Value Then
                intVotes = CInt(mySqlSelect.ExecuteScalar)
            End If

        Catch ex As Exception
            MessageBox.Show("Sorry an Error Occred." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            myConnectionString.Close()
        End Try

        Return intVotes
    End Function


    'Count the total Votes Received for each candidate
    Private Function countTotalVotesForCandidate(ByVal intPositionID As Integer, ByVal intCandidateID As Integer) As Integer
        Dim intVotes As Integer = 0
        Dim strSelect As String = "SELECT COUNT(ElectionID)  FROM tblElections WHERE PositionID=" & intPositionID & " AND CandidateID=" & intCandidateID & ""
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)

        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            If mySqlSelect.ExecuteScalar IsNot DBNull.Value Then
                intVotes = CInt(mySqlSelect.ExecuteScalar)
            End If



        Catch ex As Exception
            MessageBox.Show("Sorry an Error Occred." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            myConnectionString.Close()
        End Try

        Return intVotes
    End Function

    'In the case of yes or no vote
    Private Function countTotalYesVote(ByVal intPositionID As Integer, ByVal intCandidateID As Integer) As Integer
        Dim intVotes As Integer = 0
        Dim strSelect As String = "SELECT COUNT(ElectionID)  FROM tblElections WHERE CandidateID=" & intCandidateID & " AND YesNo='YES' AND PositionID=" & intPositionID & ""
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)

        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()

            If mySqlSelect.ExecuteScalar IsNot DBNull.Value Then
                intVotes = CInt(mySqlSelect.ExecuteScalar)
            End If

        Catch ex As Exception
            MessageBox.Show("Sorry an Error Occred." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            myConnectionString.Close()
        End Try

        Return intVotes
    End Function

    Private Function countTotalNoVote(ByVal intPositionID As Integer, ByVal intCandidateID As Integer) As Integer
        Dim intVotes As Integer = 0
        Dim strSelect As String = "SELECT COUNT(ElectionID)  FROM tblElections WHERE  CandidateID=" & intCandidateID & " AND YesNo='NO' AND PositionID=" & intPositionID & ""
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)

        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()

            If mySqlSelect.ExecuteScalar IsNot DBNull.Value Then
                intVotes = CInt(mySqlSelect.ExecuteScalar)
            Else

            End If

        Catch ex As Exception
            MessageBox.Show("Sorry an Error Occred." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            myConnectionString.Close()
        End Try

        Return intVotes
    End Function
    '************************************************


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'LoadFirstPosition()
        'Timer1.Enabled = False
    End Sub


    Private Sub mnuFileGet_Click(sender As Object, e As EventArgs) Handles mnuFileGet.Click
        If cboSelect.DataSource IsNot Nothing Then
            intPrintPositionID = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key
            portfololioName = lblPosition.Text.Trim
        Else
            intPrintPositionID = 0
        End If
        frmResultsGet.ShowDialog()
    End Sub

    Private Sub frmResults_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        isValidCode = False
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        ErrorProvider1.Clear()

        If isValid() Then
            view()
            If intNumPanel = 1 Then
                'Count all yes or no votes
                resetControlPanel2()
                Dim strPosition As String = CStr(DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Value)
                Dim intPositionID As Integer = DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Integer, String)).Key
                Dim strCandidateName As String = CStr(lblCan2.Text)
                Dim intCandidateID As Integer = CInt(lblCanID1.Text.Trim)
                Dim intCountVotes As Integer = countTotalYesVote(intPositionID, intCandidateID)
                Dim intCountNoVotes As Integer = countTotalNoVote(intPositionID, intCandidateID)
                lblCan2vote.Text = intCountVotes & " Vote(s)-YES" & vbCrLf & intCountNoVotes & " Vote(s)-NO" 'Deteriming the total votes gained
                Dim intTotalVotesCast As Integer = CInt(lblTotal.Text) 'Holds the total votes Cast
                Dim dblPercent As Double = 0
                If intTotalVotesCast = 0 Then
                    lblCan2per.Text = "0.0% " & " -YES" & vbCrLf & " 0.0% -NO"
                    lblSkip.Text = ""
                Else
                    dblPercent = CDbl(intCountVotes / intTotalVotesCast)
                    Dim dblNo = CDbl(intCountNoVotes / intTotalVotesCast)
                    Dim strNo As String = CStr(dblNo.ToString("p2"))
                    lblCan2per.Text = CStr(dblPercent.ToString("p2")) & " -YES" & vbCrLf & strNo & " -NO"
                    lblSkip.Text = countTotalVotesSkiped(CInt(DirectCast(cboSelect.SelectedItem, KeyValuePair(Of Int32, String)).Key))

                End If


            End If



        End If
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

    Private Sub mnuPrint_Click(sender As Object, e As EventArgs) Handles mnuPrint.Click
        If lblPosition.Text.Trim <> String.Empty Then
            portfololioName = lblPosition.Text.Trim

        End If

        If panCandidate2.Visible Then
            candidate2Name = lblCan2.Text.Trim
            can2NoOfVote = lblCan2vote.Text.Trim
            can2Percent = lblCan2per.Text.Trim
            totalValidVotesCast = lblTotal.Text.Trim
            totalVotesSkip = lblSkip.Text.Trim

            candidate1Name = String.Empty
            can1NoOfVote = String.Empty
            can1Percent = String.Empty


            candidate3Name = String.Empty
            can3NoOfVote = String.Empty
            can3Percent = String.Empty

            candidate4Name = String.Empty
            can4NoOfVote = String.Empty
            can4Percent = String.Empty

            candidate5Name = String.Empty
            can5NoOfVote = String.Empty
            can5Percent = String.Empty

            candidate6Name = String.Empty
            can6NoOfVote = String.Empty
            can6Percent = String.Empty

        End If

        If panCandidate1.Visible Then
            candidate1Name = lblCan1.Text.Trim
            can1NoOfVote = lblCan1vote.Text.Trim
            can1Percent = lblCan1per.Text.Trim
            totalValidVotesCast = lblTotal.Text.Trim
            totalVotesSkip = lblSkip.Text.Trim
            'Reset the rest

            candidate3Name = String.Empty
            can3NoOfVote = String.Empty
            can3Percent = String.Empty


            candidate4Name = String.Empty
            can4NoOfVote = String.Empty
            can4Percent = String.Empty


            candidate5Name = String.Empty
            can5NoOfVote = String.Empty
            can5Percent = String.Empty


            candidate6Name = String.Empty
            can6NoOfVote = String.Empty
            can6Percent = String.Empty

        End If

        If panCandidate3.Visible Then
            candidate3Name = lblCan3.Text.Trim
            can3NoOfVote = lblCan3vote.Text.Trim
            can3Percent = lblCan3per.Text.Trim

            candidate4Name = String.Empty
            can4NoOfVote = String.Empty
            can4Percent = String.Empty

            candidate5Name = String.Empty
            can5NoOfVote = String.Empty
            can5Percent = String.Empty

            candidate6Name = String.Empty
            can6NoOfVote = String.Empty
            can6Percent = String.Empty


        End If
        If panCandidate4.Visible Then
            candidate4Name = lblCan4.Text.Trim
            can4NoOfVote = lblCan4vote.Text.Trim
            can4Percent = lblCan4per.Text.Trim

            candidate5Name = String.Empty
            can5NoOfVote = String.Empty
            can5Percent = String.Empty

            candidate6Name = String.Empty
            can6NoOfVote = String.Empty
            can6Percent = String.Empty
        End If

        If panCandidate5.Visible Then
            candidate5Name = lblCan5.Text.Trim
            can5NoOfVote = lblCan5vote.Text.Trim
            can5Percent = lblCan5per.Text.Trim

            candidate6Name = String.Empty
            can6NoOfVote = String.Empty
            can6Percent = String.Empty
        End If

        If panCandidate6.Visible Then
            candidate6Name = lblCan6.Text.Trim
            can6NoOfVote = lblCan6vote.Text.Trim
            can6Percent = lblCan6per.Text.Trim

        End If

        If panCandidate2.Visible And panCandidate1.Visible = False Then
            can1Pic = can2Pic
            can2Pic = String.Empty
            candidate1Name = candidate2Name
            candidate2Name = String.Empty
            can1Percent = can2Percent
            can2Percent = String.Empty
            can1NoOfVote = can2NoOfVote
            can2NoOfVote = String.Empty
        End If
        Try
            Using frm As frmPrintResultsCan = New frmPrintResultsCan

                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try

    End Sub

    Private Sub mnuFileExit_Click_1(sender As Object, e As EventArgs) Handles mnuFileExit.Click
        Me.Close()
    End Sub
End Class