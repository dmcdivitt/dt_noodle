<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItemEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemMoveUp = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemMoveDown = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItemDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItemNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItemSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemRestore = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemShow = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemUnload = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RotatedLabel1 = New dt_noodle.RotatedLabel()
        Me.RoundedButton2 = New dt_noodle.RoundedButton()
        Me.RoundedButton1 = New dt_noodle.RoundedButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "current desktop:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 15)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(191, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(194, 0)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "icon position"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Location = New System.Drawing.Point(0, 43)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(300, 211)
        Me.Panel1.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemEdit, Me.ToolStripMenuItemMoveUp, Me.ToolStripMenuItemMoveDown, Me.ToolStripSeparator1, Me.ToolStripMenuItemDelete, Me.ToolStripSeparator2, Me.ToolStripMenuItemNew})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.ShowImageMargin = False
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(113, 126)
        '
        'ToolStripMenuItemEdit
        '
        Me.ToolStripMenuItemEdit.Name = "ToolStripMenuItemEdit"
        Me.ToolStripMenuItemEdit.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripMenuItemEdit.Text = "edit"
        '
        'ToolStripMenuItemMoveUp
        '
        Me.ToolStripMenuItemMoveUp.Name = "ToolStripMenuItemMoveUp"
        Me.ToolStripMenuItemMoveUp.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripMenuItemMoveUp.Text = "move up"
        '
        'ToolStripMenuItemMoveDown
        '
        Me.ToolStripMenuItemMoveDown.Name = "ToolStripMenuItemMoveDown"
        Me.ToolStripMenuItemMoveDown.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripMenuItemMoveDown.Text = "move down"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(109, 6)
        '
        'ToolStripMenuItemDelete
        '
        Me.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete"
        Me.ToolStripMenuItemDelete.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripMenuItemDelete.Text = "delete"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(109, 6)
        '
        'ToolStripMenuItemNew
        '
        Me.ToolStripMenuItemNew.Name = "ToolStripMenuItemNew"
        Me.ToolStripMenuItemNew.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripMenuItemNew.Text = "new"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip2
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "desktop noodle"
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemSave, Me.ToolStripMenuItemRestore, Me.ToolStripMenuItemShow, Me.ToolStripMenuItemUnload})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.ShowImageMargin = False
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(172, 92)
        '
        'ToolStripMenuItemSave
        '
        Me.ToolStripMenuItemSave.Name = "ToolStripMenuItemSave"
        Me.ToolStripMenuItemSave.Size = New System.Drawing.Size(171, 22)
        Me.ToolStripMenuItemSave.Text = "save current desktop"
        '
        'ToolStripMenuItemRestore
        '
        Me.ToolStripMenuItemRestore.Name = "ToolStripMenuItemRestore"
        Me.ToolStripMenuItemRestore.Size = New System.Drawing.Size(171, 22)
        Me.ToolStripMenuItemRestore.Text = "restore current desktop"
        '
        'ToolStripMenuItemShow
        '
        Me.ToolStripMenuItemShow.Name = "ToolStripMenuItemShow"
        Me.ToolStripMenuItemShow.Size = New System.Drawing.Size(171, 22)
        Me.ToolStripMenuItemShow.Text = "show"
        '
        'ToolStripMenuItemUnload
        '
        Me.ToolStripMenuItemUnload.Name = "ToolStripMenuItemUnload"
        Me.ToolStripMenuItemUnload.Size = New System.Drawing.Size(171, 22)
        Me.ToolStripMenuItemUnload.Text = "unload"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(174, 0)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(18, 21)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Label4"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.Controls.Add(Me.RotatedLabel1)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.RoundedButton2)
        Me.Panel2.Controls.Add(Me.RoundedButton1)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(15, 15)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(300, 254)
        Me.Panel2.TabIndex = 0
        '
        'RotatedLabel1
        '
        Me.RotatedLabel1.angle_of_rotation_degrees = 330
        Me.RotatedLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RotatedLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RotatedLabel1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.RotatedLabel1.Location = New System.Drawing.Point(166, 28)
        Me.RotatedLabel1.Margin = New System.Windows.Forms.Padding(0)
        Me.RotatedLabel1.Name = "RotatedLabel1"
        Me.RotatedLabel1.Size = New System.Drawing.Size(24, 16)
        Me.RotatedLabel1.TabIndex = 14
        Me.RotatedLabel1.Text = "key"
        '
        'RoundedButton2
        '
        Me.RoundedButton2.BackColor = System.Drawing.Color.Firebrick
        Me.RoundedButton2.BackColorClick = System.Drawing.Color.Maroon
        Me.RoundedButton2.BackColorHover = System.Drawing.Color.Crimson
        Me.RoundedButton2.BorderRadius = 7
        Me.RoundedButton2.FlatAppearance.BorderSize = 0
        Me.RoundedButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RoundedButton2.ForeColor = System.Drawing.Color.White
        Me.RoundedButton2.Location = New System.Drawing.Point(245, 14)
        Me.RoundedButton2.Margin = New System.Windows.Forms.Padding(0)
        Me.RoundedButton2.Name = "RoundedButton2"
        Me.RoundedButton2.Size = New System.Drawing.Size(51, 20)
        Me.RoundedButton2.TabIndex = 8
        Me.RoundedButton2.Text = "restore"
        Me.RoundedButton2.UseVisualStyleBackColor = False
        '
        'RoundedButton1
        '
        Me.RoundedButton1.BackColor = System.Drawing.Color.Firebrick
        Me.RoundedButton1.BackColorClick = System.Drawing.Color.Maroon
        Me.RoundedButton1.BackColorHover = System.Drawing.Color.Crimson
        Me.RoundedButton1.BorderRadius = 7
        Me.RoundedButton1.FlatAppearance.BorderSize = 0
        Me.RoundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RoundedButton1.ForeColor = System.Drawing.Color.White
        Me.RoundedButton1.Location = New System.Drawing.Point(191, 14)
        Me.RoundedButton1.Margin = New System.Windows.Forms.Padding(0)
        Me.RoundedButton1.Name = "RoundedButton1"
        Me.RoundedButton1.Size = New System.Drawing.Size(51, 20)
        Me.RoundedButton1.TabIndex = 7
        Me.RoundedButton1.Text = "save"
        Me.RoundedButton1.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Black
        Me.PictureBox1.Image = Global.dt_noodle.My.Resources.Resources.floating_image
        Me.PictureBox1.Location = New System.Drawing.Point(8, 253)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(25, 26)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(315, 0)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(15, 15)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "X"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button2
        '
        Me.Button2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button2.Location = New System.Drawing.Point(165, 0)
        Me.Button2.Margin = New System.Windows.Forms.Padding(0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(26, 20)
        Me.Button2.TabIndex = 12
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button1.Location = New System.Drawing.Point(139, 0)
        Me.Button1.Margin = New System.Windows.Forms.Padding(0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(26, 20)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(330, 284)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(-500, 0)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "desktop noodle"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents RoundedButton1 As RoundedButton
    Friend WithEvents RoundedButton2 As RoundedButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItemEdit As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemMoveUp As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemMoveDown As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItemDelete As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItemNew As ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItemShow As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemUnload As ToolStripMenuItem
    Friend WithEvents Label4 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ToolStripMenuItemSave As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemRestore As ToolStripMenuItem
    Friend WithEvents RotatedLabel1 As RotatedLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
End Class