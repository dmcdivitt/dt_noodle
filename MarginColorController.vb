Public Class MarginColorController
  Private Const friction_multiplier As Double = 0.96
  Private Const max_velocity_kick_strength As Double = 20.0
  Private Const min_ticks_between_kicks As Integer = 10
  Private Const max_ticks_between_kicks As Integer = 50
  Private _floating_position_x As Double = 0.0, _floating_position_y As Double = 0.0
  Private _movement_velocity_x As Double = 0.0, _movement_velocity_y As Double = 0.0
  Private _ticks_remaining_until_next_kick As Integer = 0
  Private _min_allowed_horizontal_position As Integer = 0, _max_allowed_horizontal_position As Integer = 0
  Private _min_allowed_vertical_position As Integer = 0, _max_allowed_vertical_position As Integer = 0
  Private _label_form_x As Integer = 0, _label_form_y As Integer = 0
  Private WithEvents _animate_timer As New Timer()
  Private _working_form As Form, _working_picture_box As PictureBox, _working_close_label As Label, _rng As New Random()

  Public Sub New(working_form As Form, picture_box As PictureBox, close_label As Label, timer_milliseconds As Integer)
    _working_form = working_form
    _working_picture_box = picture_box
    _working_close_label = close_label
    _working_picture_box.Size = _working_picture_box.Image.Size
    _floating_position_x = _working_picture_box.Left
    _floating_position_y = _working_picture_box.Top
    _min_allowed_horizontal_position = _working_form.Size.Width - _working_picture_box.Size.Width
    _min_allowed_vertical_position = _working_form.Size.Height - _working_picture_box.Size.Height
    _label_form_x = _working_close_label.Left
    _label_form_y = _working_close_label.Top
    _working_form.Controls.Remove(_working_close_label)
    _working_picture_box.Controls.Add(_working_close_label)
    _working_close_label.BackColor = Color.Transparent
    _working_close_label.ForeColor = Color.White
    _animate_timer.Interval = timer_milliseconds
  End Sub

  Public Sub run()
    _ticks_remaining_until_next_kick = 0
    apply_new_velocity_kick()
    _animate_timer.Start()
  End Sub

  Public Sub pause()
    _animate_timer.Stop()
  End Sub

  Private Sub animation_timer_Tick(sender As Object, e As EventArgs) Handles _animate_timer.Tick
    _ticks_remaining_until_next_kick -= 1
    If _ticks_remaining_until_next_kick <= 0 Then apply_new_velocity_kick()
    _movement_velocity_x *= friction_multiplier
    _movement_velocity_y *= friction_multiplier
    _floating_position_x += _movement_velocity_x
    _floating_position_y += _movement_velocity_y
    If _floating_position_x < _min_allowed_horizontal_position Then
      _floating_position_x = _min_allowed_horizontal_position
      _movement_velocity_x *= -0.5
    End If
    If _floating_position_x > _max_allowed_horizontal_position Then
      _floating_position_x = _max_allowed_horizontal_position
      _movement_velocity_x *= -0.5
    End If
    If _floating_position_y < _min_allowed_vertical_position Then
      _floating_position_y = _min_allowed_vertical_position
      _movement_velocity_y *= -0.5
    End If
    If _floating_position_y > _max_allowed_vertical_position Then
      _floating_position_y = _max_allowed_vertical_position
      _movement_velocity_y *= -0.5
    End If
    _working_picture_box.Left = _floating_position_x
    _working_picture_box.Top = _floating_position_y
    _working_close_label.Left = _label_form_x - _floating_position_x
    _working_close_label.Top = _label_form_y - _floating_position_y
  End Sub

  Private Sub apply_new_velocity_kick()
    _movement_velocity_x += (_rng.NextDouble() - 0.5) * max_velocity_kick_strength
    _movement_velocity_y += (_rng.NextDouble() - 0.5) * max_velocity_kick_strength
    _ticks_remaining_until_next_kick = _rng.Next(min_ticks_between_kicks, max_ticks_between_kicks)
  End Sub

End Class
