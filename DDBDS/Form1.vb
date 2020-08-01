Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim cmd As MySqlCommand
    '' Dim da As MySqlDataAdapter
    '' Dim dataadp As MySqlDataAdapter
    '' Dim ds As DataSet
    Dim connectionstring = "server=localhost;user id=root;password=ddbds;persistsecurityinfo=True;database=ddbds"
    Dim stringquery As String
    Dim selection As Integer = 0

    Public Function report_client_personalinfo()


        stringquery = "select upper(client_name) as NAME,upper(client_father) as FATHER_NAME,upper(client_mob) as MOBILE_NO,
                        upper(client_aadhar) as AADHAR_NO , upper(client_address) as CLIENT_ADDRESS 
                        from client WHERE file_no=@file_no"
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)

            cmd.Parameters.AddWithValue("file_no", TextBox22.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            If (rs.HasRows) Then
                While (rs.Read)

                    TextBox21.Text = rs("NAME").ToString
                    TextBox20.Text = rs("FATHER_NAME").ToString
                    MaskedTextBox14.Text = rs("MOBILE_NO").ToString
                    MaskedTextBox15.Text = rs("AADHAR_NO").ToString
                    RichTextBox6.Text = rs("CLIENT_ADDRESS").ToString

                End While
            Else
                MsgBox("NO RECORD FOUND")
            End If

            ''  Dim dt As DataTable = New DataTable
            ''  dt.Load(rs)

            '' DataGridView2.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try

    End Function
    Public Function report_guarantor_personalinfo()
        stringquery = "select upper(gua_name) as NAME,upper(gua_father) as FATHER_NAME,upper(gua_mob) as MOBILE_NO,
                        upper(gua_aadhar) as AADHAR_NO , upper(gua_address) as CLIENT_ADDRESS 
                        from guarantor WHERE file_no=@file_no "
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            cmd.Parameters.AddWithValue("file_no", TextBox22.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            If (rs.HasRows) Then
                While (rs.Read)
                    TextBox24.Text = rs("NAME").ToString
                    TextBox23.Text = rs("FATHER_NAME").ToString
                    MaskedTextBox17.Text = rs("MOBILE_NO").ToString
                    MaskedTextBox16.Text = rs("AADHAR_NO").ToString
                    RichTextBox7.Text = rs("CLIENT_ADDRESS").ToString

                End While
            Else
                MsgBox("NO RECORD FOR GUARANTOR AVAILABLE")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try

    End Function
    Public Function report_sanction()
        Try
            ''  Dim connection As SqlConnection
            ''  Dim command As New SqlCommand
            ''   Dim ds As New DataSet
            ''   Dim sAvailable As String = "N"
            ''    Dim ConnectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("GWPemcoConnectionString").ToString()
            ''   Dim param As MySqlParameter
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            '' cmd = New MySqlCommand("collection_data", conn)
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "collection_data"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("case_n", TextBox19.Text).Direction = ParameterDirection.Input
            cmd.Parameters.Add("@clients", MySqlDbType.VarChar, 20).Direction = ParameterDirection.Output
            cmd.Parameters.Add("@recover", MySqlDbType.VarChar, 20).Direction = ParameterDirection.Output
            cmd.Parameters.Add("@collected", MySqlDbType.VarChar, 20).Direction = ParameterDirection.Output
            cmd.Parameters.Add("@balance", MySqlDbType.VarChar, 20).Direction = ParameterDirection.Output
            ''   cmd.Parameters("@client").Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()
            ''      TextBox11.Text = cmd.Parameters("@clients").Value.ToString
            TextBox27.Text = cmd.Parameters("@recover").Value.ToString
            TextBox32.Text = cmd.Parameters("@collected").Value.ToString
            TextBox33.Text = cmd.Parameters("@balance").Value.ToString
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Function
    Public Function report_sanction_data()
        stringquery = "select file_no,sanction_date,sanction_lastdate,sanction_amount,upper(employee) as employee
                        from sanction WHERE case_no=@file_no "
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            cmd.Parameters.AddWithValue("file_no", TextBox19.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            If (rs.HasRows) Then
                While (rs.Read)
                    TextBox22.Text = rs("file_no").ToString
                    TextBox25.Text = rs("sanction_date").ToString
                    TextBox29.Text = rs("sanction_lastdate").ToString
                    TextBox28.Text = rs("sanction_amount").ToString
                    TextBox26.Text = rs("employee").ToString


                End While
                Panel3.Show()
                DataGridView5.Show()

                report_sanction()
                report_client_personalinfo()
                report_guarantor_personalinfo()
                load_collection()
            Else
                MsgBox("NO RECORD  FOUND/CASE NOT STARTED")
                Panel3.Hide()
                DataGridView5.Hide()

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try

    End Function

    Public Function visibility()
        TabControl1.Hide()
        Panel8.Hide()
        TabControl2.Hide()
        DataGridView4.Hide()

    End Function
    Public Function setlocation()
        TabControl1.Size = New Size(1430, 860)
        TabControl1.Location = New Point(5, 40)
        DataGridView1.Size = New Size(620, 860)
        DataGridView1.Location = New Point(800, 5)
        DataGridView3.Size = New Size(400, 750)
        DataGridView3.Location = New Point(1030, 5)
        TabControl2.Size = New Size(1430, 860)
        TabControl2.Location = New Point(5, 40)

        Panel11.Location = New Point(0, 110)
        DataGridView6.Size = New Size(400, 700)
        DataGridView6.Location = New Point(900, 60)
    End Function
    Public Function client_personalinfo()


        stringquery = "select upper(client_name) as NAME,upper(client_father) as FATHER_NAME,upper(client_mob) as MOBILE_NO,
                        upper(client_aadhar) as AADHAR_NO , upper(client_address) as CLIENT_ADDRESS 
                        from client WHERE file_no=@file_no "
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)

            cmd.Parameters.AddWithValue("file_no", TextBox16.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            While (rs.Read)
                TextBox18.Text = rs("NAME").ToString
                TextBox17.Text = rs("FATHER_NAME").ToString
                MaskedTextBox12.Text = rs("MOBILE_NO").ToString
                MaskedTextBox13.Text = rs("AADHAR_NO").ToString
                RichTextBox5.Text = rs("CLIENT_ADDRESS").ToString

            End While
            ''  Dim dt As DataTable = New DataTable
            ''  dt.Load(rs)

            '' DataGridView2.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try

    End Function
    Public Function guarantor_personalinfo()
        stringquery = "select upper(gua_name) as NAME,upper(gua_father) as FATHER_NAME,upper(gua_mob) as MOBILE_NO,
                        upper(gua_aadhar) as AADHAR_NO , upper(gua_address) as CLIENT_ADDRESS 
                        from guarantor WHERE file_no=@file_no "
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            cmd.Parameters.AddWithValue("file_no", TextBox16.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            While (rs.Read)
                TextBox15.Text = rs("NAME").ToString
                TextBox14.Text = rs("FATHER_NAME").ToString
                MaskedTextBox11.Text = rs("MOBILE_NO").ToString
                MaskedTextBox10.Text = rs("AADHAR_NO").ToString
                RichTextBox4.Text = rs("CLIENT_ADDRESS").ToString

            End While
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try

    End Function
    Public Function client_data()
        Try
            ''  Dim connection As SqlConnection
            ''  Dim command As New SqlCommand
            ''   Dim ds As New DataSet
            ''   Dim sAvailable As String = "N"
            ''    Dim ConnectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("GWPemcoConnectionString").ToString()
            ''   Dim param As MySqlParameter
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            '' cmd = New MySqlCommand("collection_data", conn)
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "collection_data"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("case_n", TextBox6.Text).Direction = ParameterDirection.Input
            cmd.Parameters.Add("@clients", MySqlDbType.VarChar, 20).Direction = ParameterDirection.Output
            cmd.Parameters.Add("@recover", MySqlDbType.VarChar, 20).Direction = ParameterDirection.Output
            cmd.Parameters.Add("@collected", MySqlDbType.VarChar, 20).Direction = ParameterDirection.Output
            cmd.Parameters.Add("@balance", MySqlDbType.VarChar, 20).Direction = ParameterDirection.Output
            ''   cmd.Parameters("@client").Direction = ParameterDirection.Output


            cmd.ExecuteNonQuery()



            TextBox11.Text = cmd.Parameters("@clients").Value.ToString
                TextBox10.Text = cmd.Parameters("@recover").Value.ToString
                TextBox9.Text = cmd.Parameters("@collected").Value.ToString
                TextBox8.Text = cmd.Parameters("@balance").Value.ToString
            load_employee_sanction()


        Catch ex As Exception
            MsgBox(ex.Message)

            conn.Close()
        End Try
    End Function
    Public Function load_employee()
        stringquery = "select distinct emp_name from employee "
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            Dim row As DataRow = dt.NewRow()
            row(0) = "PLEASE SELECT"
            dt.Rows.InsertAt(row, 0)
            ComboBox1.ValueMember = "emp_name"
            ComboBox1.DisplayMember = "emp_name"
            ComboBox1.DataSource = dt
            ComboBox2.ValueMember = "emp_name"
            ComboBox2.DisplayMember = "emp_name"
            ComboBox2.DataSource = dt
            ComboBox3.ValueMember = "emp_name"
            ComboBox3.DisplayMember = "emp_name"
            ComboBox3.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Function
    Public Function load_employee_sanction()
        stringquery = "select distinct employee from sanction where case_no=@case_no  "
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            cmd.Parameters.AddWithValue("case_no", TextBox6.Text)
            TextBox5.Text = cmd.ExecuteScalar.ToString

        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Function
    Public Function get_fileno()
        Dim str As String
        stringquery = "select last_insert_id() From client"
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            str = cmd.ExecuteScalar.ToString
            MsgBox("ADDED SUCESSFULLY.FILE NO. IS " & str)
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Function
    Public Function get_caseno()
        Dim str As String
        stringquery = "select last_insert_id() From sanction"
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            str = cmd.ExecuteScalar.ToString
            MsgBox("ADDED SUCESSFULLY.CASE NO. IS " & str)
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Function
    Public Function load_pending()
        DataGridView3.DataSource = Nothing
        stringquery = "select file_no, upper(client_name) as NAME,upper(client_father) as FATHER_NAME
                        from client WHERE status='PENDING' "
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView3.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Function
    Public Function load_filed()
        DataGridView1.DataSource = Nothing
        stringquery = "select file_no, upper(client_name) as NAME,upper(client_father) as FATHER_NAME,upper(client_mob) as MOBILE_NO,
                        upper(client_aadhar) as AADHAR_NO , upper(client_address) as CLIENT_ADDRESS 
                        from client WHERE status='FILED' "
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Function
    Public Function load_collection()
        DataGridView5.DataSource = Nothing
        stringquery = "select txn_no, coll_date as COLLECTION_DATE, amount as AMOUNT,upper(employee) as EMPLOYEE_NAME 
                        from collection WHERE case_no=@case_no "
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            cmd.Parameters.AddWithValue("case_no", TextBox19.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView5.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()



        End Try
    End Function
    Public Function EMP_COLLECTION_TODAY()
        DataGridView8.DataSource = Nothing
        stringquery = "select coll_date as DATE , upper(employee) as EMPLOYEE, sum(amount) AS AMOUNT
                        from collection where coll_date=curdate()  group by employee"
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            ''   cmd.Parameters.AddWithValue("case_no", TextBox19.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView8.DataSource = dt
            Dim max As Integer = DataGridView8.Rows.Count - 1
            Dim total As Integer = 0
            For Each row As DataGridViewRow In DataGridView8.Rows
                total = total + row.Cells(2).Value


            Next
            TextBox31.Text = total.ToString
            '   DataGridView8.Rows(max).Cells(2).Value += total
            '    DataGridView8.Rows(max).Cells(1).Value = "TOTAL"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()






        End Try
    End Function
    Public Function EMP_COLLECTION_YESTERDAY()
        DataGridView8.DataSource = Nothing
        stringquery = "select coll_date as DATE , upper(employee) as EMPLOYEE, sum(amount) AS AMOUNT
                        from collection where coll_date=DATE_ADD(curdate(),INTERVAL -1 DAY)  group by employee"
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            ''   cmd.Parameters.AddWithValue("case_no", TextBox19.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView8.DataSource = dt
            Dim max As Integer = DataGridView8.Rows.Count - 1
            Dim total As Integer = 0
            For Each row As DataGridViewRow In DataGridView8.Rows
                total = total + row.Cells(2).Value


            Next
            TextBox31.Text = total.ToString
            '   DataGridView8.Rows(max).Cells(2).Value += total
            '    DataGridView8.Rows(max).Cells(1).Value = "TOTAL"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()






        End Try
    End Function
    Public Function EMP_COLLECTION_THISMONTH()
        DataGridView8.DataSource = Nothing
        stringquery = "select MONTH(curdate()) AS MONTH , upper(employee) as EMPLOYEE, sum(amount) AS AMOUNT
                        from collection where MONTH(coll_date)=MONTH(curdate())  group by employee"
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            ''   cmd.Parameters.AddWithValue("case_no", TextBox19.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView8.DataSource = dt
            Dim max As Integer = DataGridView8.Rows.Count - 1
            Dim total As Integer = 0
            For Each row As DataGridViewRow In DataGridView8.Rows
                total = total + row.Cells(2).Value


            Next
            TextBox31.Text = total.ToString
            '   DataGridView8.Rows(max).Cells(2).Value += total
            '    DataGridView8.Rows(max).Cells(1).Value = "TOTAL"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()






        End Try
    End Function
    Public Function EMP_COLLECTION_PREVIOUSMONTH()
        DataGridView8.DataSource = Nothing
        stringquery = "select MONTH(DATE_SUB(curdate(),INTERVAL 1 MONTH)) AS MONTH , upper(employee) as EMPLOYEE, sum(amount) AS AMOUNT
                        from collection where MONTH(coll_date)=MONTH(DATE_SUB(curdate(),INTERVAL 1 MONTH))  group by employee"
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            ''   cmd.Parameters.AddWithValue("case_no", TextBox19.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView8.DataSource = dt
            Dim max As Integer = DataGridView8.Rows.Count - 1
            Dim total As Integer = 0
            For Each row As DataGridViewRow In DataGridView8.Rows
                total = total + row.Cells(2).Value


            Next
            TextBox31.Text = total.ToString
            '   DataGridView8.Rows(max).Cells(2).Value += total
            '    DataGridView8.Rows(max).Cells(1).Value = "TOTAL"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()






        End Try
    End Function
    Public Function EMP_SANCTION_TODAY()
        DataGridView9.DataSource = Nothing
        stringquery = "select sanction_date as DATE , upper(employee) as EMPLOYEE, sum(sanction_amount) AS AMOUNT
                        from sanction where sanction_date=curdate()  group by employee"
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            ''   cmd.Parameters.AddWithValue("case_no", TextBox19.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView9.DataSource = dt
            Dim max As Integer = DataGridView8.Rows.Count - 1
            Dim total As Integer = 0
            For Each row As DataGridViewRow In DataGridView9.Rows
                total = total + row.Cells(2).Value


            Next
            TextBox34.Text = total.ToString
            '   DataGridView8.Rows(max).Cells(2).Value += total
            '    DataGridView8.Rows(max).Cells(1).Value = "TOTAL"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()






        End Try
    End Function
    Public Function EMP_sanction_YESTERDAY()
        DataGridView9.DataSource = Nothing
        stringquery = "select sanction_date as DATE , upper(employee) as EMPLOYEE, sum(sanction_amount) AS AMOUNT
                        from sanction where sanction_date=DATE_ADD(curdate(),INTERVAL -1 DAY)  group by employee"
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            ''   cmd.Parameters.AddWithValue("case_no", TextBox19.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView9.DataSource = dt
            Dim max As Integer = DataGridView8.Rows.Count - 1
            Dim total As Integer = 0
            For Each row As DataGridViewRow In DataGridView9.Rows
                total = total + row.Cells(2).Value


            Next
            TextBox34.Text = total.ToString
            '   DataGridView8.Rows(max).Cells(2).Value += total
            '    DataGridView8.Rows(max).Cells(1).Value = "TOTAL"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()






        End Try
    End Function
    Public Function EMP_sanctioN_THISMONTH()
        DataGridView9.DataSource = Nothing
        stringquery = "select MONTH(curdate()) AS MONTH , upper(employee) as EMPLOYEE, sum(sanction_amount) AS AMOUNT
                        from sanction where MONTH(sanction_date)=MONTH(curdate())  group by employee"
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            ''   cmd.Parameters.AddWithValue("case_no", TextBox19.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView9.DataSource = dt
            Dim max As Integer = DataGridView8.Rows.Count - 1
            Dim total As Integer = 0
            For Each row As DataGridViewRow In DataGridView9.Rows
                total = total + row.Cells(2).Value


            Next
            TextBox34.Text = total.ToString
            '   DataGridView8.Rows(max).Cells(2).Value += total
            '    DataGridView8.Rows(max).Cells(1).Value = "TOTAL"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()






        End Try
    End Function
    Public Function EMP_sanction_PREVIOUSMONTH()
        DataGridView9.DataSource = Nothing
        stringquery = "select MONTH(DATE_SUB(curdate(),INTERVAL 1 MONTH)) AS MONTH , upper(employee) as EMPLOYEE, sum(sanction_amount) AS AMOUNT
                        from sanction where MONTH(sanction_date)=MONTH(DATE_SUB(curdate(),INTERVAL 1 MONTH))  group by employee"
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)
            ''   cmd.Parameters.AddWithValue("case_no", TextBox19.Text)

            Dim rs As MySqlDataReader = cmd.ExecuteReader
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView9.DataSource = dt
            Dim max As Integer = DataGridView8.Rows.Count - 1
            Dim total As Integer = 0
            For Each row As DataGridViewRow In DataGridView9.Rows
                total = total + row.Cells(2).Value


            Next
            TextBox34.Text = total.ToString
            '   DataGridView8.Rows(max).Cells(2).Value += total
            '    DataGridView8.Rows(max).Cells(1).Value = "TOTAL"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()






        End Try
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (
                TextBox1.Text = "" Or
               TextBox2.Text = "" Or
               MaskedTextBox1.Text = "" Or
                 MaskedTextBox2.Text = "" Or
                 RichTextBox1.Text = ""
                ) Then
            MsgBox("PLEASE FILL ALL THE OPTIONS")
        Else
            Try
                conn = New MySqlConnection(connectionstring)
                ''  cmd.Connection = conn
                stringquery = "insert into client(client_name,client_father,client_mob,client_aadhar,client_address)values(@client_name,@client_father,@client_mob,@client_aadhar,@client_address)"
                conn.Open()
                ' stringquery = "select * from mandi_name"
                cmd = New MySqlCommand(stringquery)
                cmd.Connection = conn
                cmd.Parameters.AddWithValue("@client_name", TextBox1.Text)
                cmd.Parameters.AddWithValue("@client_father", TextBox2.Text)
                cmd.Parameters.AddWithValue("@client_mob", MaskedTextBox1.Text)
                cmd.Parameters.AddWithValue("@client_aadhar", MaskedTextBox2.Text)
                cmd.Parameters.AddWithValue("@client_address", RichTextBox1.Text)
                cmd.ExecuteNonQuery()
                conn.Close()

                get_fileno()
                TextBox1.Text = ""
                TextBox2.Text = ""
                MaskedTextBox1.Text = ""
                MaskedTextBox2.Text = ""
                RichTextBox1.Text = ""
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If (
                TextBox4.Text = "" Or
               TextBox3.Text = "" Or
               MaskedTextBox4.Text = "" Or
                 MaskedTextBox3.Text = "" Or
                 RichTextBox2.Text = "" Or
                 TextBox7.Text = ""
                ) Then
            MsgBox("PLEASE FILL ALL THE OPTIONS")
        Else



            Try
                conn = New MySqlConnection(connectionstring)
                ''  cmd.Connection = conn
                stringquery = "insert into guarantor(file_no,gua_name,gua_father,gua_mob,gua_aadhar,gua_address)values(@file_no,@gua_name,@gua_father,@gua_mob,@gua_aadhar,@gua_address)"
                conn.Open()
                ' stringquery = "select * from mandi_name"
                cmd = New MySqlCommand(stringquery)
                cmd.Connection = conn
                cmd.Parameters.AddWithValue("@gua_name", TextBox4.Text)
                cmd.Parameters.AddWithValue("@gua_father", TextBox3.Text)
                cmd.Parameters.AddWithValue("@gua_mob", MaskedTextBox4.Text)
                cmd.Parameters.AddWithValue("@gua_aadhar", MaskedTextBox3.Text)
                cmd.Parameters.AddWithValue("@gua_address", RichTextBox2.Text)
                cmd.Parameters.AddWithValue("@file_no", TextBox7.Text)
                cmd.ExecuteNonQuery()
                conn.Close()
                MsgBox("ADDED SUCESSFULLY")
                load_filed()
                TextBox4.Text = ""
                TextBox3.Text = ""
                MaskedTextBox4.Text = ""
                MaskedTextBox3.Text = ""
                RichTextBox2.Text = ""
                TextBox7.Text = ""
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If (
             MaskedTextBox6.Text = "" Or
               MaskedTextBox5.Text = "" Or
            ComboBox1.Text = "PLEASE SELECT" Or
            TextBox16.Text = ""
              ) Then
            MsgBox("PLEASE FILL ALL THE OPTIONS")
        Else
            Try
                conn = New MySqlConnection(connectionstring)
                ''  cmd.Connection = conn
                stringquery = "insert into sanction(file_no,sanction_date,sanction_lastdate,sanction_amount,recoverable,employee)values(@file_no,curdate(),DATE_ADD(curdate(), INTERVAL +100 DAY),@sanction_amount,@recoverable,@employee)"
                conn.Open()
                ' stringquery = "select * from mandi_name"
                cmd = New MySqlCommand(stringquery)
                cmd.Connection = conn
                '  cmd.Parameters.Add("sanction_date", Now())

                ''  cmd.Parameters.AddWithValue("@sanction_lastdate", DateTimePicker2.Value.ToShortDateString)
                cmd.Parameters.AddWithValue("@sanction_amount", MaskedTextBox6.Text)
                cmd.Parameters.AddWithValue("@recoverable", MaskedTextBox5.Text)
                cmd.Parameters.AddWithValue("@employee", ComboBox1.Text)
                cmd.Parameters.AddWithValue("@file_no", TextBox16.Text)
                cmd.ExecuteNonQuery()
                conn.Close()
                get_caseno()
                load_pending()
                TextBox16.Text = ""
                MaskedTextBox6.Text = ""
                MaskedTextBox5.Text = ""
                ComboBox1.Text = "PLEASE SELECT"
                TextBox18.Text = ""
                TextBox17.Text = ""
                TextBox15.Text = ""
                TextBox14.Text = ""
                MaskedTextBox10.Text = ""
                MaskedTextBox11.Text = ""
                MaskedTextBox12.Text = ""
                MaskedTextBox13.Text = ""
                RichTextBox4.Text = ""
                RichTextBox5.Text = ""
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If (
             MaskedTextBox7.Text = "" Or
              TextBox5.Text = "" Or
              TextBox6.Text = ""
              ) Then
            MsgBox("PLEASE FILL ALL THE OPTIONS")
        Else


            Try
                conn = New MySqlConnection(connectionstring)
                ''  cmd.Connection = conn
                stringquery = "insert into collection(case_no,coll_date,amount,employee)values(@case_no,curdate(),@amount,@employee)"
                conn.Open()
                ' stringquery = "select * from mandi_name"
                cmd = New MySqlCommand(stringquery)
                cmd.Connection = conn
                ''    cmd.Parameters.AddWithValue("@coll_date", DateTimePicker3.Value.ToShortDateString)
                cmd.Parameters.AddWithValue("@amount", MaskedTextBox7.Text)

                cmd.Parameters.AddWithValue("@employee", TextBox5.Text)
                cmd.Parameters.AddWithValue("@case_no", TextBox6.Text)
                cmd.ExecuteNonQuery()
                conn.Close()
                MsgBox("ADDED SUCESSFULLY")
                client_data()
                MaskedTextBox7.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs)
        selection = 1
        client_personalinfo()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        selection = 2
        client_personalinfo()
        guarantor_personalinfo()

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        client_data()

    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        If (
              TextBox13.Text = "" Or
             TextBox12.Text = "" Or
             MaskedTextBox9.Text = "" Or
               MaskedTextBox8.Text = "" Or
               RichTextBox3.Text = ""
              ) Then
            MsgBox("PLEASE FILL ALL THE OPTIONS")
        Else
            Try
                conn = New MySqlConnection(connectionstring)
                ''  cmd.Connection = conn
                stringquery = "insert into employee(emp_name,emp_father,emp_mob,emp_aadhar,emp_address)values(@client_name,@client_father,@client_mob,@client_aadhar,@client_address)"
                conn.Open()
                ' stringquery = "select * from mandi_name"
                cmd = New MySqlCommand(stringquery)
                cmd.Connection = conn
                cmd.Parameters.AddWithValue("@client_name", TextBox13.Text)
                cmd.Parameters.AddWithValue("@client_father", TextBox12.Text)
                cmd.Parameters.AddWithValue("@client_mob", MaskedTextBox9.Text)
                cmd.Parameters.AddWithValue("@client_aadhar", MaskedTextBox8.Text)
                cmd.Parameters.AddWithValue("@client_address", RichTextBox3.Text)
                cmd.ExecuteNonQuery()
                conn.Close()
                MsgBox("ADDED SUCESSFULLY")
                TextBox13.Text = ""
                TextBox12.Text = ""
                MaskedTextBox9.Text = ""
                MaskedTextBox8.Text = ""
                RichTextBox3.Text = ""
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim report As New reports

        load_employee()
        visibility()
        setlocation()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        visibility()
        TabControl1.Show()
        TabControl1.SelectedIndex = 0

        TextBox1.Text = ""
        TextBox2.Text = ""
        MaskedTextBox1.Text = ""
        MaskedTextBox2.Text = ""
        RichTextBox1.Text = ""
    End Sub

    Private Sub TabPage2_Enter(sender As Object, e As EventArgs) Handles TabPage2.Enter
        load_filed()
        TextBox7.Text = ""
        TextBox4.Text = ""
        TextBox3.Text = ""
        MaskedTextBox4.Text = ""
        MaskedTextBox3.Text = ""
        RichTextBox2.Text = ""

    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBox7.Text = row.Cells(0).Value.ToString
            ''   txtName.Text = row.Cells(1).Value.ToString
            ''    txtCountry.Text = row.Cells(2).Value.ToString
        End If
    End Sub

    Private Sub TabPage3_Enter(sender As Object, e As EventArgs) Handles TabPage3.Enter
        load_pending()
        Dim futuredate As Date = Now
        futuredate = futuredate.AddDays(100)
        DateTimePicker2.Value = futuredate.ToShortDateString
        TextBox14.Text = ""
        TextBox15.Text = ""
        TextBox16.Text = ""
        TextBox17.Text = ""
        TextBox18.Text = ""

        MaskedTextBox5.Text = ""
        MaskedTextBox6.Text = ""
        MaskedTextBox10.Text = ""
        MaskedTextBox11.Text = ""
        MaskedTextBox12.Text = ""
        MaskedTextBox13.Text = ""
        RichTextBox4.Text = ""
        RichTextBox5.Text = ""
        DataGridView3.DataSource = Nothing
        ComboBox1.Text = ""
    End Sub

    Private Sub DataGridView3_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView3.CellMouseDoubleClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
            TextBox16.Text = row.Cells(0).Value.ToString
            client_personalinfo()
            guarantor_personalinfo()
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        report_sanction_data()



    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs)
        Try

            conn = New MySqlConnection(connectionstring)
            conn.Open()
            '' cmd = New MySqlCommand("collection_data", conn)
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "client_info"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("case_n", TextBox6.Text).Direction = ParameterDirection.Input
            Dim rs As MySqlDataReader = cmd.ExecuteReader
            ''  cmd.ExecuteNonQuery()
            Dim dt As DataTable = New DataTable
            dt.Load(rs)
            DataGridView3.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()

        End Try

    End Sub

    Private Sub TabPage7_Click(sender As Object, e As EventArgs) Handles TabPage7.Click
        DateTimePicker7.Enabled = False
        EMP_COLLECTION_TODAY()
    End Sub

    Private Sub Panel12_Paint(sender As Object, e As PaintEventArgs) Handles Panel12.Paint

    End Sub

    Private Sub TabPage5_Click(sender As Object, e As EventArgs) Handles TabPage5.Click

    End Sub

    Private Sub TabControl2_Enter(sender As Object, e As EventArgs) Handles TabControl2.Enter
        Panel3.Hide()
        DataGridView5.Hide()

    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        visibility()


        TabControl2.Show()
        TabControl2.SelectedIndex = 0
    End Sub

    Private Sub TabPage1_Enter(sender As Object, e As EventArgs) Handles TabPage1.Enter
        TextBox1.Text = ""
        TextBox2.Text = ""
        MaskedTextBox1.Text = ""
        MaskedTextBox2.Text = ""
        RichTextBox1.Text = ""
    End Sub

    Private Sub TabControl1_Enter(sender As Object, e As EventArgs) Handles TabControl1.Enter

    End Sub

    Private Sub TabPage4_Enter(sender As Object, e As EventArgs) Handles TabPage4.Enter
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        TextBox5.Text = ""

        MaskedTextBox7.Text = ""
    End Sub



    Private Sub Button17_Click_1(sender As Object, e As EventArgs) Handles Button17.Click
        If (ComboBox6.Text = "TODAY") Then
            EMP_COLLECTION_TODAY()
        ElseIf COMBOBOX6.Text = "YESTERDAY" Then
            EMP_COLLECTION_YESTERDAY()
        ElseIf ComboBox6.Text = "THIS MONTH" Then
            EMP_COLLECTION_THISMONTH()
        ElseIf ComboBox6.Text = "PREVIOUS MONTH" Then
            EMP_COLLECTION_PREVIOUSMONTH()
        End If


    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.Text = "SPECIFIC DATE" Then
            DateTimePicker6.Enabled = True
        Else
            DateTimePicker6.Enabled = False

        End If

    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If (ComboBox7.Text = "TODAY") Then
            EMP_SANCTION_TODAY()
        ElseIf ComboBox7.Text = "YESTERDAY" Then
            EMP_sanction_YESTERDAY()
        ElseIf ComboBox7.Text = "THIS MONTH" Then
            EMP_sanctioN_THISMONTH()
        ElseIf ComboBox7.Text = "LAST MONTH" Then
            EMP_sanction_PREVIOUSMONTH()
        End If
    End Sub
End Class
Public Class reports
    Dim conn As MySqlConnection
    Dim cmd As MySqlCommand
    '' Dim da As MySqlDataAdapter
    '' Dim dataadp As MySqlDataAdapter
    '' Dim ds As DataSet
    Dim connectionstring = "server=localhost;user id=root;password=ddbds;persistsecurityinfo=True;database=ddbds"
    Dim stringquery As String
    Dim selection As Integer = 0
    Public Function client_personadata(file_n As Integer)
        Dim obj As New Form1

        stringquery = "select upper(client_name) as NAME,upper(client_father) as FATHER_NAME,upper(client_mob) as MOBILE_NO,
                        upper(client_aadhar) as AADHAR_NO , upper(client_address) as CLIENT_ADDRESS 
                        from client "
        Try
            conn = New MySqlConnection(connectionstring)
            conn.Open()
            cmd = New MySqlCommand(stringquery, conn)

            ''  cmd.Parameters.AddWithValue("file_no", file_n)

            Dim rs As MySqlDataReader = cmd.ExecuteReader

            While (rs.Read)
                MsgBox("done")
                obj.TextBox21.Text = rs("NAME").ToString
                ''  TextBox17.Text = rs("FATHER_NAME").ToString
                ''  MaskedTextBox12.Text = rs("MOBILE_NO").ToString
                ''  MaskedTextBox13.Text = rs("AADHAR_NO").ToString
                ''  RichTextBox5.Text = rs("CLIENT_ADDRESS").ToString

            End While
            ''  Dim dt As DataTable = New DataTable
            ''  dt.Load(rs)

            '' DataGridView2.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Function

End Class

