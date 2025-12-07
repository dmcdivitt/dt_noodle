<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.RoundedButton2 = New dt_noodle.RoundedButton()
        Me.RoundedButton1 = New dt_noodle.RoundedButton()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Location = New System.Drawing.Point(42, 16)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(305, 20)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.Text = "Zxx"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(2, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "name:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(2, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "path:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Location = New System.Drawing.Point(42, 42)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(305, 20)
        Me.TextBox2.TabIndex = 2
        Me.TextBox2.Text = "Zxx"
        '
        'RoundedButton2
        '
        Me.RoundedButton2.BackColor = System.Drawing.Color.RoyalBlue
        Me.RoundedButton2.BorderRadius = 7
        Me.RoundedButton2.FlatAppearance.BorderSize = 0
        Me.RoundedButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RoundedButton2.ForeColor = System.Drawing.Color.White
        Me.RoundedButton2.Location = New System.Drawing.Point(350, 41)
        Me.RoundedButton2.Margin = New System.Windows.Forms.Padding(0)
        Me.RoundedButton2.Name = "RoundedButton2"
        Me.RoundedButton2.Size = New System.Drawing.Size(55, 20)
        Me.RoundedButton2.TabIndex = 11
        Me.RoundedButton2.Text = "browse"
        Me.RoundedButton2.UseVisualStyleBackColor = False
        '
        'RoundedButton1
        '
        Me.RoundedButton1.BackColor = System.Drawing.Color.RoyalBlue
        Me.RoundedButton1.BorderRadius = 7
        Me.RoundedButton1.FlatAppearance.BorderSize = 0
        Me.RoundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RoundedButton1.ForeColor = System.Drawing.Color.White
        Me.RoundedButton1.Location = New System.Drawing.Point(181, 75)
        Me.RoundedButton1.Margin = New System.Windows.Forms.Padding(0)
        Me.RoundedButton1.Name = "RoundedButton1"
        Me.RoundedButton1.Size = New System.Drawing.Size(51, 20)
        Me.RoundedButton1.TabIndex = 9
        Me.RoundedButton1.Text = "save"
        Me.RoundedButton1.UseVisualStyleBackColor = False
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(409, 106)
        Me.Controls.Add(Me.RoundedButton2)
        Me.Controls.Add(Me.RoundedButton1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form2"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Form2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents RoundedButton1 As RoundedButton
    Friend WithEvents RoundedButton2 As RoundedButton
End Class
