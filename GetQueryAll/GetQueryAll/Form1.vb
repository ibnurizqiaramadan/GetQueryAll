Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If INSERT_DATA("t_user", GetQueryAllInput(Me)) = True Then
            TextBox4.Text = QUERY
            'msgBoxInfo("Berhasil Memasukan Data !")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If UPDATE_DATA("t_user", GetQueryAllUpdate(Me), "user_id", "1") = True Then
            TextBox4.Text = QUERY
            'msgBoxInfo("Berhasil Mengupdate Data !")
        End If
    End Sub
End Class




'parameter pertama nama table, parameter kedua hasil return dari fucntion GetQueryAllInput, parameter ketiga untuk field id data, parameter ke empat untuk value id data yg akan di update
'parameter pertama nama table, parameter kedua hasil return dari fucntion GetQueryAllInput
