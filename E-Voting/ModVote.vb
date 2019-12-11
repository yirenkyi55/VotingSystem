Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Public Module ModVote

    Public myConnectionString As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("E_VotingCstr").ConnectionString)
    Public intPrintPositionID As Integer
    Public voterLog As String = String.Empty
    Public strPath As String = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 9))
    'A function to Generate Record ID's
    Public Function eGenerateID(ByVal strID As String, ByVal strTableName As String) As Integer
        Dim strSelect As String = "SELECT " & strID & " FROM " & strTableName & " ORDER BY " & strID & " DESC"
        Dim mySqlSelect As New SqlCommand(strSelect, myConnectionString)
        Dim mySqlDataReader As SqlDataReader = Nothing
        Dim intRecordId As Integer = 1
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            mySqlDataReader = mySqlSelect.ExecuteReader
            If mySqlDataReader.Read Then
                intRecordId = CInt(mySqlDataReader(strID)) + 1
                Return intRecordId
                mySqlDataReader.Close()
            Else
                mySqlDataReader.Close()

                Return intRecordId
            End If
        Catch ex As Exception
            MessageBox.Show("An Error Occured " & vbCrLf & ex.Message)

        Finally
            myConnectionString.Close()
        End Try
        Return intRecordId
    End Function

    'Private variables to hold Voter Details records
    Public mvFirstName As String = String.Empty
    Public mvOtherName As String = String.Empty
    Public mvLastName As String = String.Empty
    Public mvVoterID As String = String.Empty

    Public Sub resetMvVariables()
        mvFirstName = String.Empty
        mvOtherName = String.Empty
        mvLastName = String.Empty
        mvVoterID = String.Empty
    End Sub





    Public isValidCode As Boolean = False

    Public strVoterFirstName As String = String.Empty
    Public strVoterOtherName As String = String.Empty
    Public strVoterLastName As String = String.Empty
    Public strVoterLID As String = String.Empty


    Public can1Pic As String = String.Empty
    Public can2Pic As String = String.Empty
    Public can3Pic As String = String.Empty
    Public can4Pic As String = String.Empty
    Public can5Pic As String = String.Empty
    Public can6Pic As String = String.Empty

    Public candidate1Name = String.Empty
    Public candidate2Name = String.Empty
    Public candidate6Name = String.Empty
    Public candidate3Name = String.Empty
    Public candidate4Name = String.Empty
    Public candidate5Name = String.Empty

    Public can1NoOfVote = String.Empty
    Public can2NoOfVote = String.Empty
    Public can3NoOfVote = String.Empty
    Public can4NoOfVote = String.Empty
    Public can5NoOfVote = String.Empty
    Public can6NoOfVote = String.Empty

    Public can1Percent = String.Empty
    Public can2Percent = String.Empty
    Public can3Percent = String.Empty
    Public can4Percent = String.Empty
    Public can5Percent = String.Empty
    Public can6Percent = String.Empty

    Public portfololioName As String = String.Empty
    Public totalValidVotes As String = String.Empty

    Public totalValidVotesCast As String = String.Empty
    Public totalVotesSkip As String = String.Empty

End Module
