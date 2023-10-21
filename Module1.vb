Imports System.Windows.Markup
Imports Google.Protobuf.WellKnownTypes
Imports MySql.Data.MySqlClient
Module Module1
    Dim con As New MySqlConnection
    Dim reader As MySqlDataReader
    Dim mysqlcmd As New MySqlCommand
    Dim host, uname, pwd, dbname As String
    Dim sqlquery As String
    Public Sub ConnectDbase()
        host = "127.0.01"
        dbname = "it2boop"
        uname = "root"
        pwd = "password"
        'check if connection is open
        If Not con Is Nothing Then
            con.Close() 'close the dbaseconnection
            'connection string signature
            con.ConnectionString = "server =" & host & "; user id= " & uname & "; password =" & pwd & "; database=" & dbname & ""
            Try
                'open the connection
                con.Open()
                MessageBox.Show("Connected!")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
    Public Sub SaveRecord()
        Dim fname, lname, course As String
        fname = Form1.txtFname.Text 'get value from form1
        lname = Form1.txtLname.Text
        course = Form1.txtCourse.Text

        sqlquery = "INSERT INTO student(studName, studLName, course) VALUES(@fname, @lname, @course)"
        'pass the query and connection to mysqlcommand
        mysqlcmd = New MySqlCommand(sqlquery, con)
        'add the parameter value
        mysqlcmd.Parameters.AddWithValue("@fname", fname)
        mysqlcmd.Parameters.AddWithValue("@lname", lname)
        mysqlcmd.Parameters.AddWithValue("@course", course)
        Try
            'execute the sql query command
            mysqlcmd.ExecuteNonQuery()
            MsgBox("Record save successsfully!")
        Catch ex As Exception
            MessageBox.Show("Error" & ex.Message)
        Finally
            TextClear()
        End Try
    End Sub
    Sub TextClear()
        Form1.txtFname.Clear()
        Form1.txtLname.Clear()
        Form1.txtCourse.Clear()
    End Sub

    Public Sub SearchData()
        Dim uid As String
        uid = Form1.txtuserid.Text
        sqlquery = "SELECT * FROM student where studID = @uid "
        mysqlcmd = New MySqlCommand(sqlquery, con)
        mysqlcmd.Parameters.AddWithValue("@uid", uid)
        Try
            reader = mysqlcmd.ExecuteReader()
            If reader.Read Then
                Form1.txtfirst.text = reader("studName").ToString()
                Form1.txtlast.text = reader("studLName").ToString()
                Form1.txtstudcourse.text = reader("course").ToString()
            Else
                MsgBox("No Record!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            reader.Close()

        End Try
    End Sub
End Module
