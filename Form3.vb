Public Class Form3

  Private _populating As Boolean = False

  Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    Form1._option_login = CheckBox1.Checked
  End Sub

  Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
    Form1._option_tray = CheckBox2.Checked
  End Sub

  Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
    Form1._option_save = CheckBox3.Checked
  End Sub

  Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
    Form1._option_GUI = CheckBox4.Checked
  End Sub

  Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
    Form1._option_center = CheckBox5.Checked
  End Sub

  Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
    If Not _populating Then
      With ComboBox1
        If .Text.Length = 1 Then Form1.hotkey_set(.Text) Else Form1.hotkey_reset()
      End With
      populate_dropdown()
    End If
  End Sub

  Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Text = _main_window_title & " options"
    CheckBox1.Checked = Form1._option_login
    CheckBox2.Checked = Form1._option_tray
    CheckBox3.Checked = Form1._option_save
    CheckBox4.Checked = Form1._option_GUI
    CheckBox5.Checked = Form1._option_center
    populate_dropdown()
  End Sub

  Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
    Process.Start("https://github.com/dmcdivitt/dt_noodle")
  End Sub

  Private Sub populate_dropdown()
    _populating = True
    Dim available$ = get_available_hotkeys(Me), stat$
    With ComboBox1
      .Items.Clear()
      If Form1._hotkey_char.Length = 0 Then
        stat = "There is not one assigned."
      Else
        stat = "The hotkey in use is " & Form1._hotkey_char & "."
        .Items.Add(Form1._hotkey_char)
      End If
      .Items.Add("none")
      .SelectedIndex = 0
      For f1 As Integer = 0 To (available.Length - 1) : .Items.Add(available.Substring(f1, 1)) : Next f1
    End With
    Label2.Text = "The hotkey works with ctrl-shift." & vbCrLf &
                  stat & vbCrLf &
                  "Select from the list."
    _populating = False
  End Sub

End Class