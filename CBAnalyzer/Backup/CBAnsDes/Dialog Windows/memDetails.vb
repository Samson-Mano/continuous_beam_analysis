Public Class memDetails
    Public text1 As String
    Private Sub memDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TextBox1.Text = text1
        TextBox1.SelectedText = ""
    End Sub
End Class