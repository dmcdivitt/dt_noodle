' If Visual Studio fails to load a form in designer mode upon
' loading the project or after closing a form, do the following:
'
' 1. Close the tab in Visual Studio for the form that does not load. Do not save if prompted.
' 2. Close Visual Studio completely.
' 3. With Windows Explorer delete the bin and obj folders within the project folder.
' 4. Open the project again.
' 5. From the menu choose build -> rebuild solution.
' 6. Correct any errors to insure build is successful.
' 7. Open the form in designer.
'
' This insures any previous compile is deleted so custom controls can be read from the current compile.
' Once compiled the custom controls will appear in the toolbox.
' This is only necessary if a forn fails to load in design mode.
'
Public Class RotatedLabel
  Inherits Label

  Public Property angle_of_rotation_degrees As Integer = 90

  Protected Overrides Sub OnPaint(e As PaintEventArgs)
    Dim text_drawing_format As New StringFormat() : text_drawing_format.Alignment = StringAlignment.Center : text_drawing_format.LineAlignment = StringAlignment.Center : e.Graphics.TranslateTransform(Me.Width / 2.0F, Me.Height / 2.0F) : e.Graphics.RotateTransform(angle_of_rotation_degrees) : e.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), 0, 0, text_drawing_format)
  End Sub

  Public Overrides Function GetPreferredSize(proposedSize As Size) As Size
    Dim original_unrotated_size As Size = MyBase.GetPreferredSize(proposedSize)
    If angle_of_rotation_degrees = 90 OrElse angle_of_rotation_degrees = 270 Then Return New Size(original_unrotated_size.Height, original_unrotated_size.Width)
    Return original_unrotated_size
  End Function

End Class