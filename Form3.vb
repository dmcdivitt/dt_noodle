Public Class Form3

  Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Text = My.Application.Info.Title & " options"
    CheckBox1.Checked = Form1._option_login
    CheckBox2.Checked = Form1._option_tray
    CheckBox3.Checked = Form1._option_save
  End Sub

  Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    Form1._option_login = CheckBox1.Checked
  End Sub

  Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
    Form1._option_tray = CheckBox2.Checked
  End Sub

  Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
    Form1._option_save = CheckBox3.Checked
  End Sub

End Class