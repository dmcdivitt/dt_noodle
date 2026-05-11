Public Module HotkeyRoutines

  Private Declare Function RegisterHotKey Lib "user32.dll" (hWnd As IntPtr, id As Integer, fsModifiers As Integer, vk As Integer) As Boolean
  Private Declare Function UnregisterHotKey Lib "user32.dll" (hWnd As IntPtr, id As Integer) As Boolean
  Private Const character_list$ = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
  Private _last_identifier_used As Integer = 0
  Public Const WM_HOTKEY As Integer = &H312
  Public Const MOD_CONTROL As Integer = &H2
  Public Const MOD_SHIFT As Integer = &H4

  Public Function get_available_hotkeys$(source_form As Form)
    Dim characters_available$ = ""
    Dim ch$
    For f1 = 0 To (character_list.Length - 1)
      ch = character_list.Substring(f1, 1)
      If RegisterHotKey(source_form.Handle, 0, MOD_CONTROL Or MOD_SHIFT, CType(Asc(ch), Keys)) Then
        characters_available &= ch
        UnregisterHotKey(source_form.Handle, 0)
      End If
    Next f1
    Return characters_available
  End Function

  Public Function register_hotkey(source_form As Form, character$) As Integer
    Dim identifier As Integer = _last_identifier_used + 1
    If RegisterHotKey(source_form.Handle, identifier, MOD_CONTROL Or MOD_SHIFT, CType(Asc(character), Keys)) Then
      _last_identifier_used = identifier
      Return identifier
    Else
      Return -1
    End If
  End Function

  Public Sub unregister_hotkey(source_form As Form, identifier As Integer)
    Dim success As Boolean = UnregisterHotKey(source_form.Handle, identifier)
    If success AndAlso identifier = _last_identifier_used Then _last_identifier_used -= 1
  End Sub

End Module