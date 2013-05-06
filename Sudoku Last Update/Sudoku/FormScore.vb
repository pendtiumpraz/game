Public Class FormScore

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Score_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = HardLevel.score
    End Sub
End Class