Imports System.Data
Imports System.Data.SqlClient
Public Class GetData
    'Fields for AdminGet table
    Private strAdminName As String
    Private strAdminPassword As String
    Private strAdminCode As String

    Public Property AdminName
        Get
            Return strAdminName
        End Get
        Set(value)
            strAdminName = value.ToString

        End Set
    End Property

    Public Property AdminPassword
        Get
            Return strAdminPassword
        End Get
        Set(value)
            strAdminPassword = value.ToString
        End Set
    End Property

    Public Property AdminCode
        Get
            Return strAdminCode
        End Get
        Set(value)
            strAdminCode = value.ToString
        End Set
    End Property



    Dim ds As DataSet
    Dim da As SqlDataAdapter
    Public Function GetAllRecords(tableName As String) As DataSet
        Dim strSearch As String = "SELECT * FROM " & tableName
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            ds = New DataSet
            da = New SqlDataAdapter(strSearch, myConnectionString)
            da.Fill(ds)
            Return ds

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            myConnectionString.Close()
        End Try
    End Function

    Public Function checkVoterStatus(voterID As String) As Boolean
        Dim status As Boolean = False
        Dim strSearch As String = "SELECT * FROM tblElections WHERE VoterID=@VoterID"
        Dim mySqlSearch As New SqlCommand(strSearch, myConnectionString)
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            mySqlSearch.Parameters.AddWithValue("@VoterID", voterID)
            Dim readear As SqlDataReader = mySqlSearch.ExecuteReader
            If readear.Read Then
                'Record exist so return true
                status = True

            Else
                status = False

            End If
        Catch ex As Exception
            MessageBox.Show("Sorry an error occured. " & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            myConnectionString.Close()
        End Try
        Return status
    End Function

    'A sub procedure to Delete A record
    Public Sub DeleteVoter(voterID As String)


        Dim strDelete As String = "DELETE FROM tblVoters " &
                   "WHERE VoterID=@VoterID"

        Dim mySqlDelete As New SqlCommand(strDelete, myConnectionString)
        mySqlDelete.Parameters.AddWithValue("@VoterID", voterID)
        Try

            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()

            Dim nRecord As Integer = mySqlDelete.ExecuteNonQuery()
            MessageBox.Show(nRecord & " Record Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Reset()
        Catch ex As Exception
            MessageBox.Show("Sorry An Error Occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Finally
            myConnectionString.Close()
        End Try
    End Sub

    Public Sub DeleteAllRecords(strTableName As String)


        Dim strDelete As String = "DELETE FROM " + strTableName

        Dim mySqlDelete As New SqlCommand(strDelete, myConnectionString)

        Try

            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()

            Dim nRecord As Integer = mySqlDelete.ExecuteNonQuery()
            MessageBox.Show(nRecord & " Record Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception
            MessageBox.Show("Sorry An Error Occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Finally
            myConnectionString.Close()
        End Try
    End Sub

    Public Function DeleteRecords(strTableName As String) As Boolean


        Dim strDelete As String = "DELETE FROM " + strTableName
        If strTableName = "tblElections" Then
            strDelete &= ";DBCC CHECKIDENT('tblElections',RESEED,0)"
        End If

        If strTableName = "tblVoterLogin" Then
            strDelete &= ";DBCC CHECKIDENT('tblVoterLogin',RESEED,0)"
        End If
        Dim mySqlDelete As New SqlCommand(strDelete, myConnectionString)

        Try

            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()

            If mySqlDelete.ExecuteNonQuery() Then
                Return True

            End If


        Catch ex As Exception
            MessageBox.Show("Sorry An Error Occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Finally
            myConnectionString.Close()
        End Try
        Return False
    End Function


    'Delete all candidate records
    Public Function DeleteCandidate(candidateID As Integer) As Boolean
        Dim result As Boolean = False

        Dim strProfile As String = "\Images\Candidate" & candidateID & ".jpg"
        Dim strDelete As String = "DELETE FROM tblCandidate  " &
            "WHERE CandidateID=@CandidateID"
        Dim mySqlDelete As New SqlCommand(strDelete, myConnectionString)
        mySqlDelete.Parameters.AddWithValue("@CandidateID", candidateID)
        If MessageBox.Show("Are you sure you want to delete candidate record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
                System.IO.File.Delete(strPath & strProfile)
                Dim nRecord As Integer = mySqlDelete.ExecuteNonQuery
                MessageBox.Show(nRecord & " Candidate Information successfully Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                result = True

            Catch ex As Exception
                MessageBox.Show("Sorry An Error Occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Finally
                myConnectionString.Close()
            End Try

        End If
        Return False
    End Function

    Public Sub DeleteAllCandidateProfile()

        Dim strProfile As String = "SELECT Profile FROM tblCandidate"
        Dim command As New SqlCommand(strProfile, myConnectionString)

        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            Dim reader As SqlDataReader = command.ExecuteReader
            While reader.Read
                System.IO.File.Delete(strPath & reader.Item("Profile").ToString)
            End While
            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Sorry An Error Occured.Unable to delete profile" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            myConnectionString.Close()
        End Try

    End Sub
    'A Function to Delete all records from a database
    Public Function DeleteTableRecords(strTableName As String) As Boolean
        Dim blnResult As Boolean = False
        Dim strDelete As String = "DELETE FROM " + strTableName
        Dim command As SqlCommand = New SqlCommand(strDelete, myConnectionString)
        If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
        Try
            If MessageBox.Show("Are you sure you want to delete all records?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Dim nRecords As Integer = command.ExecuteNonQuery
                MessageBox.Show(nRecords & " Records successfully deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                blnResult = True
            End If
        Catch ex As Exception
            MessageBox.Show("Sorry an error occured " & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
        Return blnResult
    End Function

    'Insert record into tblVoterLogin
    Public Sub ActivateVoter()
        Dim strinsert As String = "INSERT INTO tblVoterLogin(VoterID) VALUES(@VoterID)"
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            Dim command As New SqlCommand(strinsert, myConnectionString)
            command.Parameters.AddWithValue("@VoterID", mvVoterID)
            command.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Sorry an error occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    'A function to check if voterID is activated
    Public Function isVoterIDFound()
        Dim strSearch As String = "SELECT * FROM tblVoterLogin WHERE VoterID=@VoterID"
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            Dim command As New SqlCommand(strSearch, myConnectionString)
            command.Parameters.AddWithValue("@VoterID", mvVoterID)
            Dim reader As SqlDataReader = command.ExecuteReader
            If reader.Read Then
                reader.Close()
                Return True
            Else
                reader.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Sorry an error occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return False
    End Function

    Public Sub DeleteVoterID()
        Dim strSearch As String = "DELETE FROM tblVoterLogin WHERE VoterID=@VoterID"
        Try
            If myConnectionString.State = ConnectionState.Closed Then myConnectionString.Open()
            Dim command As New SqlCommand(strSearch, myConnectionString)
            command.Parameters.AddWithValue("@VoterID", mvVoterID)
            command.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Sorry an error occured" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
