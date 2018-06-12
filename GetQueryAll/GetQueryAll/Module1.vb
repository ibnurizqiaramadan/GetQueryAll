'Imports MySql.Data.MySqlClient

Module Module1
    ''' DATABASE SETTING !!!
    Dim SERVER = "Localhost"
    Dim PORT = "3306"
    Dim UID = "root"
    Dim PASSWORD = ""
    Dim DATABASE = "db_module_test"
    Dim ZERO_DATETIME = True

    'Dim CONNECTIONSTRING As String = "SERVER=" & SERVER & ";PORT=" & PORT & ";UID=" & UID & ";PASSWORD=" & PASSWORD & ";DATABASE=" & DATABASE & ";Convert Zero Datetime=" & ZERO_DATETIME & ""
    'Public MYSQLCONNECTION As New MySqlConnection(CONNECTIONSTRING)
    'Public MYSQLCMD As MySqlCommand
    Public QUERY

    Sub buka_koneksi()
        'MYSQLCONNECTION.Open()
    End Sub

    Sub tutup_koneksi()
        'MYSQLCONNECTION.Dispose()
    End Sub

    Function GetQueryAllInput(control As Control, Optional prefix As String = "input_")
        Dim field, value, resultfield, resultvalue, namecontrol, data 'Deklarasi Variable
        For Each inputcontrol In control.Controls   'Mengambil semua controls Array
            namecontrol = Strings.Left(inputcontrol.name, prefix.Length)    'Mengecek prefix
            If namecontrol = prefix Then
                If TypeOf inputcontrol Is DateTimePicker Then
                    field += "`" & Strings.Right(inputcontrol.name, inputcontrol.name.length - prefix.Length) & "`,"   'mengambil dan mengumpulkan nama field berdasarkan nama controls
                    value += "'" & Format(inputcontrol.value, "yyyy-MM-dd") & "',"   'mengambil dan mengumpulkan value tanggal di controls
                Else
                    field += "`" & Strings.Right(inputcontrol.name, inputcontrol.name.length - prefix.Length) & "`,"   'mengambil dan mengumpulkan nama field berdasarkan nama controls
                    value += "'" & EscapeString(inputcontrol.text) & "',"   'mengambil dan mengumpulkan value text di controls
                End If
            End If
        Next
        resultfield = Strings.Left(field, field.Length - 1) 'mengumpulkan hasil field
        resultvalue = Strings.Left(value, value.Length - 1) 'mengumpulkan hasil value
        data = "(" & resultfield & ") VALUES (" & resultvalue & ")" 'hasilnya
        Return data
    End Function

    Function GetQueryAllUpdate(control As Control, Optional prefix As String = "input_")
        Dim field, resultfield, resultvalue, value, result, namecontrol, data
        For Each inputcontrol In control.Controls
            namecontrol = Strings.Left(inputcontrol.name, prefix.Length)
            If namecontrol = prefix Then
                field = "`" & Strings.Right(inputcontrol.name, inputcontrol.name.length - prefix.Length) & "`,"
                If TypeOf inputcontrol Is DateTimePicker Then
                    value = "'" & Format(inputcontrol.value, "yyyy-MM-dd") & "',"
                Else
                    value = "'" & EscapeString(inputcontrol.text) & "',"
                End If
                resultfield = Strings.Left(field, field.Length - 1)
                resultvalue = Strings.Left(value, value.Length - 1)
                result += resultfield & " = " & resultvalue & ", "
            End If
        Next
        data = Strings.Left(result, result.Length - 2)
        Return data
    End Function

    Function INSERT_DATA(table As String, data As String) As Boolean
        Try
            'buka_koneksi()

            QUERY = "INSERT INTO " & table & " " & data
            'MYSQLCMD = New MySqlCommand(QUERY, MYSQLCONNECTION)
            'MYSQLCMD.ExecuteNonQuery()

            'tutup_koneksi()
            Return True
        Catch ex As Exception
            ERRORDB(ex)
            Return False
        End Try
    End Function

    Function UPDATE_DATA(table As String, data As String, where As String, value As String)
        Try
            'buka_koneksi()

            QUERY = "UPDATE " & table & " SET " & data & " WHERE " & where & " = " & value
            'MYSQLCMD = New MySqlCommand(QUERY, MYSQLCONNECTION)
            'MYSQLCMD.ExecuteNonQuery()

            'tutup_koneksi()
            Return True
        Catch ex As Exception
            ERRORDB(ex)
            Return False
        End Try
    End Function

    Sub msgBoxInfo(Pesan As String, Optional Judul As String = "Info")
        MsgBox(Pesan, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Judul)
    End Sub

    Sub msgBoxPeringatan(Pesan As String, Optional Judul As String = "Peringatan")
        MsgBox(Pesan, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, Judul)
    End Sub

    Sub msgBoxError(Pesan As String, Optional Judul As String = "Error")
        MsgBox(Pesan, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Judul)
    End Sub

    Function msgBoxKonfir(pesan As String, Optional Judul As String = "Konfirmasi") As MsgBoxResult
        Return MsgBox(pesan, MsgBoxStyle.Question + MsgBoxStyle.YesNo, Judul)
    End Function

    Sub ERRORDB(ex As Exception)
        msgBoxError("GAGAL, karena " + ex.Message + " | QUERY :  ( " + QUERY + " ) ", "Gagal !")
        'If MYSQLCONNECTION.State = ConnectionState.Open Then
        '    tutup_koneksi()
        'End If
    End Sub

    'Function EscapeString1(text As String) As String
    '    Try
    '        Return MySqlHelper.EscapeString(text)
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function

    Function EscapeString(text As String) As String
        Dim result As String
        result = text.Replace("\", "\\")
        result = result.Replace("'", "\'")
        result = result.Replace("""", "\""")
        result = result.Replace("`", "\`")
        Return result
    End Function

End Module
