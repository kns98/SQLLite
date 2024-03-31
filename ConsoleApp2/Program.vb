Imports System
Imports System.Data.SQLite

Module Program
    Sub Main(args As String())
        Dim connectionString As String = "Data Source=mydatabase.db;Version=3;"
        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Console.WriteLine("Successfully connected to the database!")
                'You can perform database operations here.
            End Using
        Catch ex As SQLiteException
            Console.WriteLine("Error: " & ex.Message)
        End Try
        InitializeDb()
        LoadDataIntoArray()
    End Sub

    Sub InitializeDb()
        ' Designate a new database file named "mydatabase.db"
        Dim connectionString As String = "Data Source=mydatabase.db;Version=3;"
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()

            ' Create Users table
            Dim createTableQuery As String = "CREATE TABLE IF NOT EXISTS Users (UserID INTEGER PRIMARY KEY, Name TEXT, Country TEXT)"
            Using cmd As New SQLiteCommand(createTableQuery, conn)
                cmd.ExecuteNonQuery()
            End Using

            ' Insert initial users
            Dim insertUsers As String = "INSERT INTO Users (Name, Country) VALUES (?, ?)"
            Using cmd As New SQLiteCommand(insertUsers, conn)
                cmd.Parameters.AddWithValue("Name", "Alice")
                cmd.Parameters.AddWithValue("Country", "USA")
                cmd.ExecuteNonQuery()

                cmd.Parameters.Clear()

                cmd.Parameters.AddWithValue("Name", "Bob")
                cmd.Parameters.AddWithValue("Country", "UK")
                cmd.ExecuteNonQuery()

                cmd.Parameters.Clear()

                cmd.Parameters.AddWithValue("Name", "Charlie")
                cmd.Parameters.AddWithValue("Country", "Canada")
                cmd.ExecuteNonQuery()

                cmd.Parameters.Clear()
            End Using

            Console.WriteLine("Database and initial data created successfully!")
        End Using
    End Sub
    Sub LoadDataIntoArray()
        ' Set the connection string
        Dim connectionString As String = "Data Source=mydatabase.db;Version=3;"

        ' Set up the SQL query
        Dim query As String = "SELECT Name FROM Users"

        ' Create a list to store names retrieved from the database
        Dim names As New List(Of String)()

        Using conn As New SQLiteConnection(connectionString)
            conn.Open()

            Using cmd As New SQLiteCommand(query, conn)

                Using reader As SQLiteDataReader = cmd.ExecuteReader()

                    ' Add all the names from the database to the list
                    While reader.Read()
                        names.Add(reader("Name").ToString())
                    End While

                End Using

            End Using
        End Using

        ' So far the 'names' list has been populated with usernames from the database.
        ' You can convert this list to an array if you want:
        Dim namesArray() As String = names.ToArray()

        ' Print array content for testing purposes
        For Each name In namesArray
            Console.WriteLine(name)
        Next
    End Sub
End Module
