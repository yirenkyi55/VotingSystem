Imports System.Data
Imports System.Data.SqlClient
Imports System.Object
Imports System.Speech.Synthesis
Public Class frmVote
    Private mySynthesizer As New SpeechSynthesizer
    ' Retrieve the working rectangle from the Screen class using the        PrimaryScreen and the WorkingArea properties.  
    Dim workingRectangle As System.Drawing.Rectangle = Screen.PrimaryScreen.WorkingArea
    Private strArray As String()
    Private strArraySplitID As String()
    Dim strPositions As String = String.Empty
    Dim intSelectPositionID As Integer = 0
    Dim strPositionID As String = String.Empty
    Dim intLoopThrough As Integer = 0 'Used to Step Through the Positions one after the other
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '' Set the size of the form slightly less than size of working rectangle. 
        'Me.Size = New System.Drawing.Size(workingRectangle.Width - 5, workingRectangle.Height - 5)
        '' Set the location so the entire form is visible. 
        'Me.Location = New System.Drawing.Point(3, 3)

        LoadPosition()
        enablePanCandidates()
        getUserDetails()
        deterMineSkip()
        mySynthesizer.SelectVoiceByHints(VoiceGender.Female)
        mySynthesizer.SpeakAsync("Welcome to the voting section. For more than one candidate, click on vote to vote for your favorite candidate. For only one candidate click on yes or no to vote for your favorite candidate. Click on skip to skip a porfolio if available. Thanks for your attention.")

    End Sub

    Private Sub mnuFileExit_Click(sender As Object, e As EventArgs) Handles mnuFileExit.Click
        If MessageBox.Show("Are you sure you want to Exit this section?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            frmInterface.Show()
            Me.Close()
        End If

    End Sub


    '*********************Sub Procedures and Functions ***********************
    Sub getUserDetails()
        lblLoginName.Text = "Login Name:" & mvLastName & " " & mvOtherName & " " & mvFirstName & vbCrLf & " ID: " & mvVoterID

    End Sub
    Sub LoadPosition()
        Dim intCount As Integer = 0
        Dim strSelect As String = "SELECT * FROM tblPositions ORDER BY PositionID"
        Dim strCount As String = "SELECT COUNT(*) FROM [tblPositions] "
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)
        Dim mySqlCount As New SqlCommand(strCount, myConnectionString)
        Dim mySqlDataReader As SqlDataReader = Nothing
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            mySqlDataReader = mySqlSelect.ExecuteReader
            strPositions = String.Empty
            strPositionID = String.Empty
            While mySqlDataReader.Read

                strPositions &= mySqlDataReader.Item("Name") & ", "
                strPositionID &= mySqlDataReader.Item("PositionID") & ", "
            End While

            If strPositions <> String.Empty Then
                strPositions = strPositions.Remove(strPositions.Length - 2)
                strPositionID = strPositionID.Remove(strPositionID.Length - 2)
            End If
            mySqlDataReader.Close()
        Catch ex As Exception
        Finally
            myConnectionString.Close()

        End Try


    End Sub

    Sub resetPanel()
        panCandidate1.Visible = False
        panCandidate2.Visible = False
        panCandidate3.Visible = False
        panCandidate4.Visible = False
        panCandidate5.Visible = False
        panCandidate6.Visible = False


    End Sub

    ' A sub procedure to display candidates on the form..
    Sub enablePanCandidates()
        ' MessageBox.Show(strPositions)
        Dim intCount As Integer = 0
        Dim strSelectPosition As String = String.Empty

        Dim intNumPanel As Integer = 0 'Set the number of panel, Use to determine a yes/no voting type
        Dim intNumberofCan As Integer = 0 'Use to determine the number of candidate in each portfolio
        resetPanel() 'Reset the panel
        LoadPosition() 'Load the Various positions and store in a private variable of string(strposition)
        If strPositions <> String.Empty Then
            lblPosition.Text = String.Empty
            btnSkip.Enabled = True
            If (InStr(strPositions, ",")) > 0 Then 'Means the there is more than one position
                strArray = Split(strPositions, ", ") 'Split the private string variable into arrays
                strArraySplitID = Split(strPositionID, ",")
                strPositions = String.Empty
                If intLoopThrough < strArray.Length Then
                    strSelectPosition = strArray(intLoopThrough).Trim
                    intSelectPositionID = CInt(strArraySplitID(intLoopThrough).Trim)
                End If

                intCount = strArray.Length
            Else
                strSelectPosition = strPositions.Trim
                intSelectPositionID = CInt(strPositionID.Trim)
                intCount = 1
            End If

            If intLoopThrough < intCount Then 'Using an integer variable to loop through
                'This means, available portfolios exist and must be displayed for election
                lblPosition.Text = strSelectPosition.Trim.ToUpper 'Display the position
                Dim strFirstName As String = String.Empty
                Dim strOtherName As String = String.Empty
                Dim strLastName As String = String.Empty
                Dim strTitle As String = String.Empty
                Dim strProfile As String = String.Empty
                Dim intCandidateID As Integer
                'Search for candidates under this position
                Dim strSelect As String = "SELECT * FROM tblCandidate WHERE PositionID='" & intSelectPositionID & "' ORDER BY CandidateID"
                Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)
                Dim mySqlDataReader As SqlDataReader = Nothing
                Try
                    If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                    mySqlDataReader = mySqlSelect.ExecuteReader
                    While mySqlDataReader.Read
                        If mySqlDataReader.Item("Title") IsNot DBNull.Value Then
                            strTitle = CStr(mySqlDataReader.Item("Title") & " ").TrimStart
                        Else
                            strTitle = String.Empty
                        End If

                        strFirstName = CStr(mySqlDataReader.Item("FirstName"))
                        If mySqlDataReader.Item("OtherName") IsNot DBNull.Value Then
                            strOtherName = CStr(mySqlDataReader.Item("OtherName"))
                        Else
                            strOtherName = String.Empty
                        End If
                        intCandidateID = mySqlDataReader.Item("CandidateID")
                        strLastName = CStr(mySqlDataReader.Item("LastName"))
                        strProfile = CStr(mySqlDataReader.Item("Profile"))
                        ' A candidate information has been identified
                        intNumberofCan += 1
                        If intNumberofCan > 0 Then
                            Select Case intNumberofCan
                                Case 1
                                    'Show the first candidate
                                    panCandidate1.Visible = True
                                    lblCan1.Text = strTitle & strLastName & " " & strOtherName & " " & strFirstName
                                    lblCanID.Text = intCandidateID.ToString
                                    Dim BMP As Bitmap
                                    BMP = Bitmap.FromFile(strPath & strProfile)
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
                                    lblCan5.Text = strTitle & strLastName & " " & strOtherName & " " & strFirstName
                                    lblCanID5.Text = intCandidateID.ToString
                                    Dim BMP As Bitmap
                                    BMP = Bitmap.FromFile(strPath & strProfile)
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
                                    lblCan6.Text = strTitle & strLastName & " " & strOtherName & " " & strFirstName
                                    lblCanID6.Text = intCandidateID.ToString
                                    Dim BMP As Bitmap
                                    BMP = Bitmap.FromFile(strPath & strProfile)
                                    picCan6.Image = New Bitmap(BMP)
                                    BMP.Dispose()
                                    GC.Collect()
                                    intNumPanel = 6

                            End Select
                        End If

                    End While
                    mySqlDataReader.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                Finally
                    myConnectionString.Close()
                End Try
                'Increase the Loop Through by 1..Meaning we are done with one complete position
                intLoopThrough += 1
                If intNumPanel = 1 Then
                    resetPanel()
                    panCandidate2.Visible = True
                    lblCan2.Text = strTitle & strLastName & " " & strOtherName & " " & strFirstName
                    lblCanID2.Text = intCandidateID.ToString
                    Dim BMP As Bitmap
                    BMP = Bitmap.FromFile(strPath & strProfile)
                    picCan2.Image = New Bitmap(BMP)
                    BMP.Dispose()
                    GC.Collect()
                    intNumPanel = 1



                    btnNo.Visible = True
                    btnNo.Text = "NO"
                    btnCan2.Text = "YES"
                    Me.btnCan2.Size = New System.Drawing.Size(150, 39)
                Else
                    btnNo.Visible = False

                    btnNo.Visible = False
                    btnCan2.Text = "VOTE"
                    Me.btnCan2.Size = New System.Drawing.Size(321, 39)

                End If
            Else
                'close the form
                frmThanks.Show()
                Me.Close()
            End If

        Else
            lblPosition.Text = "No Candidate Registered!!"
            btnSkip.Enabled = False

        End If

    End Sub

    'A sub procedure to Insert Records into the database based a voter choice
    Sub insertVoter(ByVal strCandidateID As String, ByVal strCandidateName As String, ByVal strPositionID As Integer)

        Dim strString As String = "INSERT INTO tblElections(CandidateID,VoterID,VoteTime,PositionID) VALUES(@CandidateID,@VoterID,@VoteTime,@PositionID)"
        Dim mySqlInsert As New SqlCommand(strString, myConnectionString)
        Dim strVoterID As String = mvVoterID.Trim
        Dim dtmDate As String = Now.ToString("hh:mm:ss tt")
        Dim strPositionSection As String = lblPosition.Text.Trim
        If MessageBox.Show("Are you sure you want to Vote for " & strCandidateName & "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()

                mySqlInsert.Parameters.AddWithValue("@CandidateID", CLng(strCandidateID))
                mySqlInsert.Parameters.AddWithValue("@VoterID", strVoterID)
                mySqlInsert.Parameters.AddWithValue("@VoteTime", dtmDate)
                mySqlInsert.Parameters.AddWithValue("@PositionID", CInt(strPositionID))
                mySqlInsert.ExecuteNonQuery()
                MessageBox.Show("Your vote has been successfully casted for " & strPositionSection & " Section", "Vote", MessageBoxButtons.OK, MessageBoxIcon.Information)
                frmProgress.ShowDialog()
                enablePanCandidates()
            Catch ex As Exception
                MessageBox.Show("Sorry an Error Occured " & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Finally
                myConnectionString.Close()
            End Try
        End If

    End Sub

    Sub insertYesNoVote(ByVal strCandidateID As String, ByVal strCandidateName As String, ByVal strPositionID As Integer, ByVal strYesNo As String)

        Dim strString As String = "INSERT INTO tblElections(CandidateID,YesNo,VoterID,VoteTime,PositionID) VALUES(@CandidateID,@YesNo,@VoterID,@VoteTime,@PositionID)"
        Dim mySqlInsert As New SqlCommand(strString, myConnectionString)
        Dim strVoterID As String = mvVoterID.Trim
        Dim dtmDate As String = Now.ToString("hh:mm:ss tt")
        Dim strPositionSection As String = lblPosition.Text.Trim
        If MessageBox.Show("Are you sure you want to Vote " & strYesNo & " for " & strCandidateName & "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                mySqlInsert.Parameters.AddWithValue("@CandidateID", CInt(strCandidateID))
                mySqlInsert.Parameters.AddWithValue("@YesNo", strYesNo)
                mySqlInsert.Parameters.AddWithValue("@VoterID", strVoterID)
                mySqlInsert.Parameters.AddWithValue("@VoteTime", dtmDate)
                mySqlInsert.Parameters.AddWithValue("@PositionID", CInt(strPositionID))
                mySqlInsert.ExecuteNonQuery()
                MessageBox.Show("Your vote has been successfully casted for " & strPositionSection & " Section", "Vote", MessageBoxButtons.OK, MessageBoxIcon.Information)
                frmProgress.ShowDialog()
                enablePanCandidates()
            Catch ex As Exception
                MessageBox.Show("Sorry an Error Occured " & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Finally
                myConnectionString.Close()
            End Try
        End If

    End Sub



    'Insert record into Skip
    Sub insertSkipVote(ByVal strCandidatePosition As String)

        Dim strString As String = "INSERT INTO tblElections(VoterID,VoteTime,PositionID) VALUES(@VoterID,@VoteTime,@PositionID)"
        Dim mySqlInsert As New SqlCommand(strString, myConnectionString)
        Dim strVoterID As String = mvVoterID.Trim
        Dim dtmDate As String = Now.ToString("hh:mm:ss tt")
        If MessageBox.Show("Are you sure you want to Skip Vote for " & strCandidatePosition & " Section ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                mySqlInsert.Parameters.AddWithValue("@VoterID", strVoterID)
                mySqlInsert.Parameters.AddWithValue("@VoteTime", dtmDate)
                mySqlInsert.Parameters.AddWithValue("@PositionID", CInt(intSelectPositionID))
                mySqlInsert.ExecuteNonQuery()
                MessageBox.Show("Voting Successfully Skipped ", "Vote", MessageBoxButtons.OK, MessageBoxIcon.Information)
                frmProgress.ShowDialog()
                enablePanCandidates()
            Catch ex As Exception
                MessageBox.Show("Sorry an Error Occured " & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Finally
                myConnectionString.Close()
            End Try
        End If

    End Sub

    Sub deterMineSkip()
        Dim strSearch As String = "SELECT SkipPositions FROM tblElections"
        Dim mySqlSearch As New SqlCommand(strSearch, myConnectionString)
        Dim mySqlDataReader As SqlDataReader = Nothing
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            mySqlDataReader = mySqlSearch.ExecuteReader
            If mySqlDataReader.Read Then
                Dim strAnswer As String = CStr(mySqlDataReader.Item("SkipPositions"))
                If strAnswer = "YES" Then
                    btnSkip.Visible = True
                Else
                    btnSkip.Visible = False
                End If
                mySqlDataReader.Close()
            Else
                mySqlDataReader.Close()
            End If
        Catch ex As Exception

        Finally
            myConnectionString.Close()
        End Try
    End Sub
    '*********************************************

    Private Sub btnCan1_Click(sender As Object, e As EventArgs) Handles btnCan1.Click
        Dim strCandidateName As String = lblCan1.Text.Trim
        Dim strCanPosition As String = lblPosition.Text.Trim
        Dim strCandidateID As String = CStr(lblCanID.Text.Trim)
        insertVoter(strCandidateID, strCandidateName, intSelectPositionID)
    End Sub


    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        'Insert a no record
        Dim strCandidateName As String = lblCan2.Text.Trim
        Dim strCanPosition As String = lblPosition.Text.Trim
        Dim strCandidateID As String = lblCanID2.Text.Trim


        insertYesNoVote(strCandidateID, strCandidateName, intSelectPositionID, "NO")
    End Sub

    Private Sub btnCan3_Click(sender As Object, e As EventArgs) Handles btnCan3.Click
        Dim strCandidateName As String = lblCan3.Text.Trim
        Dim strCanPosition As String = lblPosition.Text.Trim
        Dim strCandidateID As String = CStr(lblCanID3.Text.Trim)
        insertVoter(strCandidateID, strCandidateName, intSelectPositionID)
    End Sub

    Private Sub btnCan4_Click(sender As Object, e As EventArgs) Handles btnCan4.Click
        Dim strCandidateName As String = lblCan4.Text.Trim
        Dim strCanPosition As String = lblPosition.Text.Trim
        Dim strCandidateID As String = CStr(lblCanID4.Text.Trim)
        insertVoter(strCandidateID, strCandidateName, intSelectPositionID)
    End Sub

    Private Sub btnCan5_Click(sender As Object, e As EventArgs) Handles btnCan5.Click
        Dim strCandidateName As String = lblCan5.Text.Trim
        Dim strCanPosition As String = lblPosition.Text.Trim
        Dim strCandidateID As String = CStr(lblCanID5.Text.Trim)
        insertVoter(strCandidateID, strCandidateName, intSelectPositionID)
    End Sub

    Private Sub btnCan6_Click(sender As Object, e As EventArgs) Handles btnCan6.Click
        Dim strCandidateName As String = lblCan6.Text.Trim
        Dim strCanPosition As String = lblPosition.Text.Trim
        Dim strCandidateID As String = CStr(lblCanID6.Text.Trim)
        insertVoter(strCandidateID, strCandidateName, intSelectPositionID)
    End Sub

    Private Sub btnSkip_Click(sender As Object, e As EventArgs) Handles btnSkip.Click
        Dim strCanPosition As String = lblPosition.Text.Trim
        insertSkipVote(strCanPosition)
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub



    Private Sub btnCan2_Click(sender As Object, e As EventArgs) Handles btnCan2.Click
        'Insert a yes or Vote Record
        If btnCan2.Text = "VOTE" Then
            Dim strCandidateName As String = lblCan2.Text.Trim
            Dim strCanPosition As String = lblPosition.Text.Trim
            Dim strCandidateID As String = lblCanID2.Text.Trim
            insertVoter(strCandidateID, strCandidateName, intSelectPositionID)
        ElseIf btnCan2.Text = "YES" Then
            Dim strCandidateName As String = lblCan2.Text.Trim
            Dim strCanPosition As String = lblPosition.Text.Trim
            Dim strCandidateID As String = lblCanID2.Text.Trim
            insertYesNoVote(strCandidateID, strCandidateName, intSelectPositionID, "YES")
        End If
    End Sub

    Private Sub frmVote_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        mySynthesizer.Dispose()
        Dim data As New GetData
        data.DeleteVoterID()
    End Sub
End Class
