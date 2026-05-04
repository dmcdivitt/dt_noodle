Imports System.IO
Imports System.Runtime.InteropServices

Public Class Form1

  Private Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As IntPtr) As Boolean
  Private Declare Function SetWindowText Lib "user32.dll" Alias "SetWindowTextA" (ByVal hWnd As Long, ByVal lpString As String) As Long
  Private Const SHCNE_ASSOCCHANGED As Integer = &H8000000
  Private Const SHCNF_IDLIST As Integer = &H0
  Private _in_edit As Boolean = False, _in_option As Boolean = False, _system_tray_exit As Boolean = False, _panel_control_top As Integer, _config_file$, _cache_folder$
  Private _msgbox_title$ = My.Application.Info.Title, _quick_key$ = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ"
  Private _mouse_down As Boolean = False, _mouse_initial_x As Integer, _mouse_initial_y As Integer, label5background As Color, _mcc As MarginColorController
  Public _idx_current As Integer, _idx_edit As Integer, _desktop_path$()
  Public _option_login As Boolean, _option_tray As Boolean, _option_save As Boolean, _option_GUI As Boolean, _option_center As Boolean, _hotkey_indicator As Integer = -1, _hotkey_char$ = ""

  Private Sub add_desktop_row(idx As Integer, name$, path$)
    Const c_height As Integer = 20
    Panel1.Controls.Add(New Label With {
                   .AutoSize = False,
                   .ContextMenuStrip = ContextMenuStrip1,
                   .Location = New Point(0, _panel_control_top),
                   .Margin = New Padding(0),
                   .Name = "d_name_" & idx.ToString("000"),
                   .Size = New Drawing.Size(170, c_height),
                   .Tag = idx,
                   .Text = name,
                   .TextAlign = ContentAlignment.MiddleLeft
                   })
    AddHandler Panel1.Controls(Panel1.Controls.Count - 1).MouseDown, AddressOf d_name_mousedown
    Dim qk$ = ""
    If idx < _quick_key.Length Then qk = _quick_key.Substring(idx, 1)
    Panel1.Controls.Add(New Label With {
                   .Text = qk,
                   .TextAlign = ContentAlignment.MiddleLeft,
                   .Location = New Point(174, _panel_control_top + 4),
                   .Size = New Drawing.Size(12, c_height - 4)
                   })
    Panel1.Controls.Add(New RoundedButton With {
                   .BorderRadius = 7,
                   .Location = New Point(182, _panel_control_top),
                   .Margin = New Padding(0),
                   .Name = "d_select_" & idx.ToString("000"),
                   .Size = New Drawing.Size(51, c_height),
                   .Tag = idx,
                   .Text = "select"
                   })
    AddHandler Panel1.Controls(Panel1.Controls.Count - 1).Click, AddressOf d_select_click
    Panel1.Controls.Add(New RoundedButton With {
                   .BorderRadius = 7,
                   .Location = New Point(238, _panel_control_top),
                   .Margin = New Padding(0),
                   .Name = "d_open_" & idx.ToString("000"),
                   .Size = New Drawing.Size(51, c_height),
                   .Tag = idx,
                   .Text = "open"
                   })
    AddHandler Panel1.Controls(Panel1.Controls.Count - 1).Click, AddressOf d_open_click
    If idx = 0 Then ReDim _desktop_path(idx) Else ReDim Preserve _desktop_path(idx)
    _desktop_path(idx) = path
    _panel_control_top += c_height + 2
  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    If _in_edit OrElse _in_option Then
      Threading.Thread.Sleep(100)
      SetForegroundWindow(Handle)
      If _in_edit Then SetForegroundWindow(Form2.Handle)
      If _in_option Then SetForegroundWindow(Form3.Handle)
    Else
      form_position()
      Threading.Thread.Sleep(100)
      SetForegroundWindow(Handle)
    End If
  End Sub

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    form_position()
    populate_panel()
    Threading.Thread.Sleep(100)
    SetForegroundWindow(Handle)
  End Sub

  Private Function cache_file_name$(for_writing As Boolean)
    Dim screen_width As Integer = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, base_name$ = "." & _desktop_path(_idx_current).Replace("\", "_").Replace(":", "_") & ".$"
    Dim default_path$ = _cache_folder & "\" & screen_width & base_name
    If for_writing OrElse IO.File.Exists(default_path) Then Return default_path
    Dim file_list = IO.Directory.GetFiles(_cache_folder), wide As Integer, w$(), file_name$
    Dim highest_below_idx As Integer = -1, highest_below_val As Integer = Integer.MinValue, lowest_above_idx As Integer = -1, lowest_above_val As Integer = Integer.MaxValue
    For f1 As Integer = 0 To (file_list.Length - 1)
      file_name = file_list(f1).Substring(file_list(f1).LastIndexOf("\") + 1)
      w = file_name.Split(".")
      If w.Length > 2 AndAlso file_name.IndexOf(base_name, StringComparison.OrdinalIgnoreCase) <> -1 Then
        If Integer.TryParse(w(0), wide) Then
          If wide > highest_below_val AndAlso wide < screen_width Then
            highest_below_val = wide
            highest_below_idx = f1
          End If
          If wide < lowest_above_val AndAlso wide > screen_width Then
            lowest_above_val = wide
            lowest_above_idx = f1
          End If
        End If
      End If
    Next f1
    If highest_below_idx <> -1 Then Return file_list(highest_below_idx)
    If lowest_above_idx <> -1 Then Return file_list(lowest_above_idx)
    Return ""
  End Function

  Private Sub config_save(Optional skip_for_delete As Integer = -1)
    Static backup_was_done As Boolean = False
    If Panel1.Controls.Count > 0 Then
      If Not backup_was_done Then
        backup_was_done = True
        Dim backup_file$ = _config_file.Substring(0, _config_file.Length - 3) & "bak"
        If IO.File.Exists(backup_file) Then IO.File.Delete(backup_file)
        IO.File.Move(_config_file, backup_file)
      End If
      With New StreamWriter(_config_file, False)
        If Not _option_login Then .WriteLine("login=no")
        If Not _option_tray Then .WriteLine("tray=no")
        If Not _option_save Then .WriteLine("save=no")
        If Not _option_GUI Then .WriteLine("gui=no")
        If Not _option_center Then .WriteLine("center=no")
        .WriteLine("hotkey=" & _hotkey_char)
        For f1 As Integer = 0 To (_desktop_path.Length - 1)
          If f1 <> skip_for_delete Then .WriteLine("path=" & Panel1.Controls("d_name_" & f1.ToString("000")).Text & "*" & _desktop_path(f1))
        Next f1
        .Close()
      End With
    End If
  End Sub

  Private Sub d_name_mousedown(sender As Object, e As EventArgs)
    _idx_edit = DirectCast(sender, Label).Tag
    ToolStripMenuItemDelete.Visible = _idx_edit <> _idx_current
    ToolStripSeparator1.Visible = _idx_edit <> _idx_current
    ToolStripMenuItemMoveUp.Visible = _idx_edit > 0
    ToolStripMenuItemMoveDown.Visible = _idx_edit < (_desktop_path.Length - 1)
  End Sub

  Private Sub d_open_click(sender As Object, e As EventArgs)
    Dim idx As Integer = DirectCast(sender, Button).Tag
    Dim path$ = get_desktop_path$(idx)
    If path.Length > 0 Then Shell("explorer.exe """ & _desktop_path(idx) & """", AppWinStyle.NormalFocus)
  End Sub

  Private Sub d_select_click(sender As Object, e As EventArgs)
    Dim idx As Integer = DirectCast(sender, Button).Tag
    select_desktop(idx)
  End Sub

  Public Function edit_save(ByRef name$, ByRef path$) As Boolean
    name = name.Trim
    If name.Length = 0 Then
      MsgBox("name must be given", vbOK Or vbSystemModal, _msgbox_title)
      Return False
    End If
    path = path.Trim
    If path.Length < 3 OrElse ((Not path.StartsWith("\\")) AndAlso path.Substring(1, 1) <> ":") Then
      MsgBox("path must be UNC or begin with drive letter", vbOK Or vbSystemModal, _msgbox_title)
      Return False
    End If
    If path.EndsWith("\") Then path = path.Substring(0, path.Length - 1)
    If path.Length < 3 OrElse path.EndsWith(":") Then
      MsgBox("invalid path for this application", vbOK Or vbSystemModal, _msgbox_title)
      Return False
    End If
    Dim found As Boolean
    Try
      found = IO.Directory.Exists(path)
    Catch
      found = False
    End Try
    If Not found Then
      MsgBox("path not found", vbOK Or vbSystemModal, _msgbox_title)
      Return False
    End If
    Dim found_name As Boolean = False, found_path As Boolean = False
    For f1 As Integer = 0 To (_desktop_path.Length - 1)
      If f1 <> _idx_edit Then
        found_name = found_name Or name.Equals(Panel1.Controls("d_name_" & f1.ToString("000")).Text, StringComparison.OrdinalIgnoreCase)
        found_path = found_path Or path.Equals(_desktop_path(f1), StringComparison.OrdinalIgnoreCase)
      End If
    Next f1
    Dim msg$ = If(found_name, "desktop name already defined", "")
    If found_path Then msg &= If(found_name, vbCrLf, "") & "path already defined"
    If msg.Length > 0 Then
      MsgBox(msg, vbOK Or vbSystemModal, _msgbox_title)
      Return False
    End If
    If _idx_edit < 0 Then
      Panel1.SuspendLayout()
      Dim c As Control = Panel1.Controls("d_information")
      Panel1.Controls.Remove(c)
      c.Dispose()
      add_desktop_row(_desktop_path.Length, name, path)
      populate_panel_info()
      Panel1.ResumeLayout()
    Else
      Panel1.Controls("d_name_" & _idx_edit.ToString("000")).Text = name
      _desktop_path(_idx_edit) = path
    End If
    config_save()
    If _idx_edit = _idx_current Then show_current()
    Return True
  End Function

  Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    If (Not _system_tray_exit) AndAlso _option_tray AndAlso e.CloseReason = CloseReason.UserClosing Then
      e.Cancel = True
      toggle_visible()
    Else
      setup_login_load()
      If _hotkey_indicator > -1 Then unregister_hotkey(Me, _hotkey_indicator)
      NotifyIcon1.Visible = False
      NotifyIcon1 = Nothing
    End If
  End Sub

  Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
    If Visible AndAlso (Not _in_edit) AndAlso (Not _in_option) Then
      Dim idx = _quick_key.IndexOf(e.KeyChar.ToString)
      If idx >= 0 AndAlso idx < _desktop_path.Length Then
        If _option_GUI Then Hide()
        select_desktop(idx)
        If _option_GUI Then wipe_panel(True)
      End If
    End If
  End Sub

  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
    Text = _main_window_title
    SetWindowText(Handle, _main_window_title)
    Button1.Top = 0 - Button1.Height
    Button2.Top = 0 - Button2.Height
    Label4.Text = ChrW(&H2630)
    ContextMenuStrip2.Items.Insert(0, New ToolStripLabel With {.Font = New Font("Segoe UI", 9, FontStyle.Bold),
                                                              .ForeColor = Color.Black,
                                                              .Text = _msgbox_title
                                                              })
    _mcc = New MarginColorController(Me, PictureBox1, Label5, 30)
    _cache_folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
    If Not _cache_folder.EndsWith("\") Then _cache_folder &= "\"
    _cache_folder &= My.Application.Info.AssemblyName & "$$"
    If Not IO.Directory.Exists(_cache_folder) Then IO.Directory.CreateDirectory(_cache_folder)
    _config_file = _cache_folder & "\_config.txt"
    If Not IO.File.Exists(_config_file) Then IO.File.Create(_config_file).Close()
    populate_panel()
    If _option_tray AndAlso Command.IndexOf("/hide", StringComparison.OrdinalIgnoreCase) <> -1 Then
      Timer1.Enabled = True ' form position defined off the screen, this will hide as well
    Else
      form_position()
    End If
    KeyPreview = True
    label5background = Label5.BackColor
  End Sub

  Private Sub form_position()
    Dim desktop_rect As Rectangle = Screen.FromPoint(Cursor.Position).WorkingArea
    Dim form_left As Integer, form_top As Integer
    If _option_center Then
      form_left = (desktop_rect.Right - desktop_rect.Left - Width) \ 2
      form_top = (desktop_rect.Bottom - desktop_rect.Top - Height) \ 2
    Else
      form_left = Cursor.Position.X
      form_top = Cursor.Position.Y - (Height \ 2)
      If (form_left + Width) > desktop_rect.Right Then form_left = desktop_rect.Right - Width
      If (form_top + Height) > desktop_rect.Bottom Then form_top = desktop_rect.Bottom - Height
      If form_left < desktop_rect.Left Then form_left = desktop_rect.Left
      If form_top < desktop_rect.Top Then form_top = desktop_rect.Top
    End If
    Location = New Point(form_left, form_top)
    _mcc.run()
  End Sub

  Private Function get_desktop_path$(idx As Integer)
    If Not IO.Directory.Exists(_desktop_path(idx)) Then
      MsgBox("the path:" & vbCrLf &
             _desktop_path(idx) & vbCrLf &
             "does not exist for desktop:" & vbCrLf &
             Panel1.Controls("d_name_" & _idx_current.ToString("000")).Text, vbOK Or vbSystemModal, _msgbox_title)
      Return ""
    End If
    Return _desktop_path(idx)
  End Function

  Public Sub hotkey_reset()
    If _hotkey_indicator > -1 Then
      unregister_hotkey(Me, _hotkey_indicator)
      _hotkey_indicator = -1
      _hotkey_char = ""
    End If
  End Sub

  Public Function hotkey_set(ch$) As Boolean
    hotkey_reset()
    Dim proposed_indicator As Integer = register_hotkey(Me, ch)
    If proposed_indicator < 0 Then Return False
    _hotkey_indicator = proposed_indicator
    _hotkey_char = ch
    Return True
  End Function

  Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
    _in_option = True
    Form3.ShowDialog()
    _in_option = False
    If (Not IsNothing(NotifyIcon1)) AndAlso NotifyIcon1.Visible <> _option_tray Then
      NotifyIcon1.Visible = _option_tray
      Dim c As Control = Panel1.Controls("d_information")
      Panel1.Controls.Remove(c)
      c.Dispose()
      populate_panel_info()
    End If
    config_save()
  End Sub

  Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
    Close()
  End Sub

  Private Sub Label5_MouseEnter(sender As Object, e As EventArgs) Handles Label5.MouseEnter
    Label5.BackColor = Color.Red
  End Sub

  Private Sub Label5_MouseLeave(sender As Object, e As EventArgs) Handles Label5.MouseLeave
    Label5.BackColor = label5background
  End Sub

  Private Sub move_row(direction)
    Dim target As Integer = _idx_edit + direction
    Dim save$ = Panel1.Controls("d_name_" & _idx_edit.ToString("000")).Text
    Panel1.Controls("d_name_" & _idx_edit.ToString("000")).Text = Panel1.Controls("d_name_" & target.ToString("000")).Text
    Panel1.Controls("d_name_" & target.ToString("000")).Text = save
    save = _desktop_path(_idx_edit)
    _desktop_path(_idx_edit) = _desktop_path(target)
    _desktop_path(target) = save
    Dim save_enable As Boolean = Panel1.Controls("d_select_" & _idx_edit.ToString("000")).Enabled
    Panel1.Controls("d_select_" & _idx_edit.ToString("000")).Enabled = Panel1.Controls("d_select_" & target.ToString("000")).Enabled
    Panel1.Controls("d_select_" & target.ToString("000")).Enabled = save_enable
    If _idx_edit = _idx_current Then
      _idx_current = target
    ElseIf target = _idx_current Then
      _idx_current = _idx_edit
    End If
    config_save()
  End Sub

  Private Sub NotifyIcon1_MouseDown(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDown
    If e.Button = MouseButtons.Right Then ToolStripMenuItemShow.Text = If(Visible, "hide", "show")
  End Sub

  Private Sub NotifyIcon1_MouseUp(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseUp
    If e.Button = MouseButtons.Left Then show_form()
  End Sub

  Private Sub populate_panel()
    Panel1.SuspendLayout()
    wipe_panel(False)
    Dim current_desktop_folder$ = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
    If current_desktop_folder.EndsWith("\") Then current_desktop_folder = current_desktop_folder.Substring(0, current_desktop_folder.Length - 1)
    Dim p As Integer, idx As Integer = 0, idx_default_name As Integer = -1, turn_off As Boolean, a$, b$, d_name$, d_path$
    _idx_current = -1
    _panel_control_top = 0
    _option_login = True
    _option_tray = True
    _option_save = True
    _option_GUI = True
    _option_center = True
    Dim off_noun$() = {"no", "off", "0"}
    With New IO.StreamReader(_config_file)
      While Not .EndOfStream
        a = .ReadLine.Trim
        If a.Length > 0 Then
          p = a.IndexOf("=")
          If p > 2 AndAlso p < (a.Length - 1) Then
            b = a.Substring(p + 1)
            turn_off = off_noun.Contains(b, StringComparer.OrdinalIgnoreCase)
            Select Case a.Substring(0, p).Trim.ToLower
              Case "path"
                p = b.LastIndexOf("*")
                If p > 1 AndAlso p < (b.Length - 3) Then
                  d_name = b.Substring(0, p).Trim
                  d_path = b.Substring(p + 1).Trim
                  If d_path.EndsWith("\") Then d_path = d_path.Substring(0, d_path.Length - 1)
                  If d_name.Length > 1 AndAlso (Not d_path.EndsWith(":")) Then
                    add_desktop_row(idx, d_name, d_path)
                    If d_name.Equals("default", StringComparison.OrdinalIgnoreCase) Then idx_default_name = idx
                    If d_path.Equals(current_desktop_folder, StringComparison.OrdinalIgnoreCase) Then _idx_current = idx
                    idx += 1
                  End If
                End If
              Case "center"
                _option_center = Not turn_off
              Case "gui"
                _option_GUI = Not turn_off
              Case "hotkey"
                b = b.Trim.ToUpper
                If b.Length = 1 Then hotkey_set(b)
              Case "login"
                _option_login = Not turn_off
              Case "save"
                _option_save = Not turn_off
              Case "tray"
                _option_tray = Not turn_off
            End Select
          End If
        End If
      End While
      .Close()
    End With
    If _idx_current < 0 Then
      If idx_default_name < 0 Then
        add_desktop_row(idx, "default", current_desktop_folder)
        _idx_current = idx
        With New StreamWriter(_config_file, True)
          .WriteLine("default=" & current_desktop_folder)
          .Close()
        End With
      Else
        MsgBox("the current desktop path is not configured and" & vbCrLf &
               "a desktop named ""default"" is already present", vbOK Or vbSystemModal, _msgbox_title)
        _system_tray_exit = True
        Close()
        Exit Sub
      End If
    End If
    Panel1.Controls("d_select_" & _idx_current.ToString("000")).Enabled = False
    populate_panel_info()
    Panel1.ResumeLayout()
    show_current()
    If NotifyIcon1.Visible <> _option_tray Then NotifyIcon1.Visible = _option_tray
  End Sub

  Private Sub populate_panel_info()
    Panel1.Controls.Add(New Label With {
                   .AutoSize = True,
                   .ForeColor = Color.Green,
                   .Location = New Point(0, _panel_control_top),
                   .Margin = New Padding(0),
                   .Name = "d_information",
                   .Text = "right click name for actions" & IIf(_option_tray, vbCrLf & "right click tray icon to exit", ""),
                   .TextAlign = ContentAlignment.MiddleCenter
                   })
  End Sub

  Private Sub RoundedButton1_Click(sender As Object, e As EventArgs) Handles RoundedButton1.Click, ToolStripMenuItemSave.Click
    DesktopIconManager.LayoutSave(cache_file_name(True))
  End Sub

  Private Sub RoundedButton2_Click(sender As Object, e As EventArgs) Handles RoundedButton2.Click, ToolStripMenuItemRestore.Click
    Panel1.SuspendLayout()
    DesktopIconManager.LayoutRestore(cache_file_name(False))
    Panel1.ResumeLayout()
  End Sub

  Private Sub select_desktop(idx As Integer)
    Dim path$ = get_desktop_path$(idx)
    If path.Length > 0 Then
      If _option_save Then DesktopIconManager.LayoutSave(cache_file_name(True))
      DesktopIconManager.FixCopiedFileList()
      If SHSetKnownFolderPath(New Guid("B4BFCC3A-DB2C-424C-B029-7FE99A87C641"), 0, IntPtr.Zero, path) = 0 Then
        Panel1.Controls("d_select_" & _idx_current.ToString("000")).Enabled = True
        _idx_current = idx
        SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero)
        If Not DesktopIconManager.WaitForDesktopRender Then MsgBox("a problem occurred waiting for desctop icons to render" & vbCrLf &
                                                                   "and positions may not be fully restored", vbOK Or vbSystemModal, _msgbox_title)
        DesktopIconManager.ToggleDesktopPainting(False)
        DesktopIconManager.LayoutRestore(cache_file_name(False))
        DesktopIconManager.ToggleDesktopPainting(True)
        Label2.Text = Panel1.Controls("d_name_" & _idx_current.ToString("000")).Text
        Panel1.Controls("d_select_" & _idx_current.ToString("000")).Enabled = False
      End If
    End If
  End Sub

  Private Sub setup_login_load()
    Dim wsh = CreateObject("WScript.Shell"), shortcut As Object, good As Boolean = False, exe$ = System.Reflection.Assembly.GetExecutingAssembly().Location, target$, arguments$
    Dim exe_file$ = exe.Substring(exe.LastIndexOf("\") + 1)
    Dim startup_folder$ = Environment.GetFolderPath(Environment.SpecialFolder.Startup)
    Try
      Dim file_list = IO.Directory.GetFiles(startup_folder)
      For Each file_name In file_list
        If file_name.EndsWith(".lnk", StringComparison.OrdinalIgnoreCase) Then
          shortcut = wsh.CreateShortcut(file_name)
          target = shortcut.TargetPath
          arguments = shortcut.Arguments
          If target.IndexOf(exe_file, StringComparison.OrdinalIgnoreCase) <> -1 Then
            If _option_login AndAlso target.Equals(exe, StringComparison.OrdinalIgnoreCase) AndAlso arguments.Equals("/hide", StringComparison.OrdinalIgnoreCase) Then
              good = True
            Else
              Try : IO.File.Delete(file_name) : Catch : End Try
            End If
          End If
        End If
      Next file_name
      If _option_login And (Not good) Then
        With wsh.CreateShortcut(startup_folder & "\" & exe_file.Substring(0, exe_file.LastIndexOf(".")) & ".lnk")
          .Arguments = "/hide"
          .TargetPath = exe
          .Description = _msgbox_title
          .Save
        End With
      End If
    Catch ex As Exception
      MsgBox("the following error occurred setting up app load at login:" & vbCrLf &
             ex.Message, vbYesNo Or vbSystemModal, _msgbox_title)
    End Try
  End Sub

  Private Declare Function SHSetKnownFolderPath Lib "shell32.dll" (
        <MarshalAs(UnmanagedType.LPStruct)> ByVal rfid As Guid,
        ByVal dwFlags As UInteger,
        ByVal hToken As IntPtr,
        <MarshalAs(UnmanagedType.LPWStr)> ByVal pszPath As String
    ) As Integer

  Private Declare Sub SHChangeNotify Lib "shell32.dll" (
        ByVal wEventId As Integer,
        ByVal uFlags As Integer,
        ByVal dwItem1 As IntPtr,
        ByVal dwItem2 As IntPtr)

  Private Sub show_current()
    Label2.Text = Panel1.Controls("d_name_" & _idx_current.ToString("000")).Text
  End Sub

  Private Sub show_form()
    If Not Visible Then Show()
    If _in_edit Then
      _in_edit = False
      Form2.Close()
    End If
    If _in_option Then
      _in_option = False
      Form3.Close()
    End If
    form_position()
    populate_panel()
    SetForegroundWindow(Handle)
  End Sub

  Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    Timer1.Enabled = False
    wipe_panel(True)
    Hide()
  End Sub

  Private Sub toggle_visible()
    If Visible Then
      If _in_edit Then
        _in_edit = False
        Form2.Close()
      End If
      If _in_option Then
        _in_option = False
        Form3.Close()
      End If
      wipe_panel(True)
      Hide()
    Else
      Show()
      form_position()
      populate_panel()
      Threading.Thread.Sleep(100)
      SetForegroundWindow(Handle)
    End If
  End Sub

  Private Sub ToolStripMenuItemDelete_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemDelete.Click
    If MsgBox("the desktop:" & vbCrLf &
              Panel1.Controls("d_name_" & _idx_edit.ToString("000")).Text & vbCrLf &
              "with path:" & vbCrLf &
              _desktop_path(_idx_edit) & vbCrLf &
              "will be deleted" & vbCrLf & vbCrLf &
              "continue?", vbYesNo Or vbSystemModal, _msgbox_title) = vbYes Then
      config_save(_idx_edit)
      populate_panel()
    End If
  End Sub

  Private Sub ToolStripMenuItemEdit_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemEdit.Click
    _in_edit = True
    Form2.ShowDialog()
    _in_edit = False
  End Sub

  Private Sub ToolStripMenuItemMoveDown_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemMoveDown.Click
    move_row(1)
  End Sub

  Private Sub ToolStripMenuItemMoveUp_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemMoveUp.Click
    move_row(-1)
  End Sub

  Private Sub ToolStripMenuItemNew_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemNew.Click
    _idx_edit = -1
    _in_edit = True
    Form2.ShowDialog()
    _in_edit = False
  End Sub

  Private Sub ToolStripMenuItemShow_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemShow.Click
    toggle_visible()
  End Sub

  Private Sub ToolStripMenuItemUnload_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemUnload.Click
    If Visible Then
      If _in_edit Then Form2.Close()
      If _in_option Then Form3.Close()
      wipe_panel(True)
    End If
    _system_tray_exit = True
    Close()
  End Sub

  Private Sub wipe_panel(halt_timer As Boolean)
    While Panel1.Controls.Count > 0 : Dim c As Control = Panel1.Controls(0) : Panel1.Controls.Remove(c) : c.Dispose() : End While
    If halt_timer Then _mcc.pause()
  End Sub

  Protected Overrides Sub WndProc(ByRef windows_message As Message)
    If windows_message.Msg = WM_HOTKEY AndAlso windows_message.WParam.ToInt32() = _hotkey_indicator Then show_form()
    MyBase.WndProc(windows_message)
  End Sub

  Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
    If e.Button = MouseButtons.Left Then
      _mouse_down = True
      _mouse_initial_x = e.X + PictureBox1.Left
      _mouse_initial_y = e.Y + PictureBox1.Top
    End If
  End Sub

  Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
    If _mouse_down Then Me.Location = New Point(Me.Left + (e.X + PictureBox1.Left) - _mouse_initial_x, Me.Top + (e.Y + PictureBox1.Top) - _mouse_initial_y)
  End Sub

  Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
    If e.Button = MouseButtons.Left Then _mouse_down = False
  End Sub

End Class
