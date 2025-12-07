Public Class Form2
  Private Sub Form2_Load(sender As Object, e As EventArgs) Handles Me.Load
    TextBox2.Enabled = Form1._idx_edit <> Form1._idx_current
    RoundedButton2.Enabled = TextBox2.Enabled
    If Form1._idx_edit < 0 Then
      TextBox1.Text = ""
      TextBox2.Text = ""
      Text = "New Desktop Definition"
    Else
      TextBox1.Text = Form1.Panel1.Controls("d_name_" & Form1._idx_edit.ToString("000")).Text
      TextBox2.Text = Form1._desktop_path(Form1._idx_edit)
      Text = "Edit: " & TextBox1.Text
    End If
  End Sub

  Private Sub RoundedButton1_Click(sender As Object, e As EventArgs) Handles RoundedButton1.Click
    If Form1.edit_save(TextBox1.Text, TextBox2.Text) Then Close()
  End Sub

  Private Sub RoundedButton2_Click(sender As Object, e As EventArgs) Handles RoundedButton2.Click
    With New FolderBrowserDialog()
      Dim path$ = TextBox2.Text.Trim
      If path.Length = 0 Then path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
      .SelectedPath = path
      .ShowNewFolderButton = True
      .Description = "select the desktop folder:"
      If .ShowDialog() = DialogResult.OK Then TextBox2.Text = .SelectedPath
    End With
  End Sub

End Class