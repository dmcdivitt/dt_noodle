Imports System.Threading

Module Module1
  Private Const SW_HIDE As Integer = 0
  Private Const SW_SHOWNORMAL As Integer = 1
  Private Const SW_SHOW As Integer = 5
  Private Const SW_RESTORE As Integer = 9
  Private Declare Function IsWindowVisible Lib "user32" (ByVal hwnd As IntPtr) As Boolean
  Private Declare Function ShowWindow Lib "user32" (ByVal hwnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
  Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
  Private Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWndParent As IntPtr, ByVal hWndChildAfter As IntPtr, ByVal lpszClass As String, ByVal lpszWindow As String) As IntPtr
  Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
  Private Const BM_CLICK As Integer = &HF5
  Public _ide_mode As Boolean = False, _main_window_title$ = My.Application.Info.Title & " v " & My.Application.Info.Version.ToString

  Sub main()
    _ide_mode = System.Diagnostics.Debugger.IsAttached
    If Not _ide_mode Then
      If Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1 Then
        Dim other_hwnd = FindWindow(vbNullString, _main_window_title)
        If other_hwnd <> IntPtr.Zero Then
          Dim button_text$
          If IsWindowVisible(other_hwnd) Then
            button_text = "Button1"
          Else
            button_text = "Button2"
            ShowWindow(other_hwnd, SW_SHOW)
          End If
          Dim button_hwnd = FindWindowEx(other_hwnd, IntPtr.Zero, vbNullString, button_text)
          If button_hwnd <> IntPtr.Zero Then SendMessage(button_hwnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero)
        End If
        Exit Sub
      End If
      AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf unhandled_other_exceptions
      AddHandler Application.ThreadException, AddressOf unhandled_form_exceptions
      Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
    End If
    Application.Run(Form1)
    If Not _ide_mode Then
      RemoveHandler Application.ThreadException, AddressOf unhandled_form_exceptions
      RemoveHandler AppDomain.CurrentDomain.UnhandledException, AddressOf unhandled_other_exceptions
    End If
  End Sub

  Sub unhandled_form_exceptions(ByVal sender As Object, ByVal t As ThreadExceptionEventArgs)
    Dim e As Exception = t.Exception, a$ = e.Message
    While Not IsNothing(e.InnerException) : e = e.InnerException : a &= vbCrLf & vbCrLf & e.Message : End While
    MsgBox("The following error occurred in the application:" & vbCrLf & vbCrLf & _
           a)
    RemoveHandler Application.ThreadException, AddressOf unhandled_form_exceptions
    RemoveHandler AppDomain.CurrentDomain.UnhandledException, AddressOf unhandled_other_exceptions
    Application.Exit()
  End Sub

  Sub unhandled_other_exceptions(sender As Object, args As UnhandledExceptionEventArgs)
    Dim e As Exception = DirectCast(args.ExceptionObject, Exception), a$ = e.Message
    While Not IsNothing(e.InnerException) : e = e.InnerException : a &= vbCrLf & vbCrLf & e.Message : End While
    MsgBox("The following error occurred in the application:" & vbCrLf & vbCrLf & _
           a)
    RemoveHandler Application.ThreadException, AddressOf unhandled_form_exceptions
    RemoveHandler AppDomain.CurrentDomain.UnhandledException, AddressOf unhandled_other_exceptions
    Application.Exit()
  End Sub

End Module