Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Class RoundedButton
  Inherits Button

#Region "Constructor"

  Public Sub New()
    Me.FlatStyle = FlatStyle.Flat
    Me.FlatAppearance.BorderSize = 0
    Me.BackColor = Color.RoyalBlue
    Me.ForeColor = Color.White
    Me.Size = New Size(150, 40)

    ' Set reasonable defaults
    _back_color_click = Color.Navy
    _back_color_hover = Color.CornflowerBlue
    _fore_color_click = Color.White
    _fore_color_hover = Color.White
  End Sub

#End Region

#Region "Fields"

  Private _back_color_click As Color = Color.Navy
  Private _back_color_hover As Color = Color.CornflowerBlue
  Private _border_radius As Integer = 20
  Private _fore_color_click As Color = Color.White
  Private _fore_color_hover As Color = Color.White
  Private _is_hovered As Boolean = False
  Private _is_pressed As Boolean = False

#End Region

#Region "Methods & Events"

  Private Function GetFigurePath(rect As Rectangle, radius As Integer) As GraphicsPath
    Dim path As New GraphicsPath()
    Dim curve_size As Single = radius * 2.0F

    path.StartFigure()
    path.AddArc(rect.X, rect.Y, curve_size, curve_size, 180, 90)
    path.AddArc(rect.Right - curve_size, rect.Y, curve_size, curve_size, 270, 90)
    path.AddArc(rect.Right - curve_size, rect.Bottom - curve_size, curve_size, curve_size, 0, 90)
    path.AddArc(rect.X, rect.Bottom - curve_size, curve_size, curve_size, 90, 90)
    path.CloseFigure()

    Return path
  End Function

  Protected Overrides Sub OnMouseDown(mevent As MouseEventArgs)
    MyBase.OnMouseDown(mevent)
    _is_pressed = True : Me.Invalidate()
  End Sub

  Protected Overrides Sub OnMouseEnter(e As EventArgs)
    MyBase.OnMouseEnter(e)
    _is_hovered = True : Me.Invalidate()
  End Sub

  Protected Overrides Sub OnMouseLeave(e As EventArgs)
    MyBase.OnMouseLeave(e)
    _is_hovered = False : Me.Invalidate()
  End Sub

  Protected Overrides Sub OnMouseUp(mevent As MouseEventArgs)
    MyBase.OnMouseUp(mevent)
    _is_pressed = False : Me.Invalidate()
  End Sub

  Protected Overrides Sub OnPaint(ByVal pevent As PaintEventArgs)
    Dim graphics As Graphics = pevent.Graphics
    graphics.SmoothingMode = SmoothingMode.AntiAlias

    ' 1. Determine current colors based on state
    Dim current_back_color As Color = Me.BackColor
    Dim current_fore_color As Color = Me.ForeColor

    If Not Me.Enabled Then
      current_back_color = Color.FromArgb(64, Me.BackColor)
      current_fore_color = Color.Black
    ElseIf _is_pressed Then
      current_back_color = _back_color_click
      current_fore_color = _fore_color_click
    ElseIf _is_hovered Then
      current_back_color = _back_color_hover
      current_fore_color = _fore_color_hover
    End If

    ' 2. Define Container Color (for anti-aliasing fix)
    Dim container_color As Color = Me.BackColor
    If Me.Parent IsNot Nothing Then container_color = Me.Parent.BackColor

    ' 3. Clean Canvas
    Using brush_container As New SolidBrush(container_color)
      graphics.FillRectangle(brush_container, Me.ClientRectangle)
    End Using

    ' 4. Draw Button
    Dim rect_surface As Rectangle = Me.ClientRectangle
    Dim rect_border As Rectangle = Rectangle.Inflate(rect_surface, -1, -1)
    Dim smooth_size As Integer = 2

    If _border_radius > 2 Then
      Using path_surface As GraphicsPath = GetFigurePath(rect_surface, _border_radius)
        Using path_border As GraphicsPath = GetFigurePath(rect_border, _border_radius - 1)
          Using pen_surface As New Pen(container_color, smooth_size)
            Using pen_border As New Pen(Me.FlatAppearance.BorderColor, 1)
              Me.Region = New Region(path_surface)
              Using brush_btn As New SolidBrush(current_back_color)
                graphics.FillPath(brush_btn, path_surface)
              End Using
              graphics.DrawPath(pen_surface, path_surface)
              If _border_radius >= 1 Then graphics.DrawPath(pen_border, path_border)
            End Using
          End Using
        End Using
      End Using
    Else
      ' Square Button Fallback
      Me.Region = New Region(rect_surface)
      Using brush_btn As New SolidBrush(current_back_color)
        graphics.FillRectangle(brush_btn, rect_surface)
      End Using
    End If

    TextRenderer.DrawText(graphics, Me.Text, Me.Font, rect_surface, current_fore_color, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
  End Sub

#End Region

#Region "Properties"

  <Category("Appearance")>
  <Description("The background color when the mouse is pressed down on the button.")>
  <DefaultValue(GetType(Color), "Navy")>
  Public Property BackColorClick() As Color
    Get
      Return _back_color_click
    End Get
    Set(ByVal value As Color)
      _back_color_click = value : Me.Invalidate()
    End Set
  End Property

  <Category("Appearance")>
  <Description("The background color when the mouse is hovering over the button.")>
  <DefaultValue(GetType(Color), "CornflowerBlue")>
  Public Property BackColorHover() As Color
    Get
      Return _back_color_hover
    End Get
    Set(ByVal value As Color)
      _back_color_hover = value : Me.Invalidate()
    End Set
  End Property

  <Category("Appearance")>
  <Description("The radius of the button corners.")>
  <DefaultValue(20)>
  Public Property BorderRadius() As Integer
    Get
      Return _border_radius
    End Get
    Set(ByVal value As Integer)
      _border_radius = value : Me.Invalidate()
    End Set
  End Property

  <Category("Appearance")>
  <Description("The text color when the mouse is pressed down on the button.")>
  <DefaultValue(GetType(Color), "White")>
  Public Property ForeColorClick() As Color
    Get
      Return _fore_color_click
    End Get
    Set(ByVal value As Color)
      _fore_color_click = value : Me.Invalidate()
    End Set
  End Property

  <Category("Appearance")>
  <Description("The text color when the mouse is hovering over the button.")>
  <DefaultValue(GetType(Color), "White")>
  Public Property ForeColorHover() As Color
    Get
      Return _fore_color_hover
    End Get
    Set(ByVal value As Color)
      _fore_color_hover = value : Me.Invalidate()
    End Set
  End Property

#End Region

End Class
