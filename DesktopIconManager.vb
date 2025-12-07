Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms

Public Class DesktopIconManager

#Region "Fields"

  Private Shared _found_shell_view As IntPtr = IntPtr.Zero

#End Region

#Region "Methods"

  Private Shared Function FindWorkerW(hwnd As IntPtr, lParam As IntPtr) As Boolean
    Dim shell_view As IntPtr = FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", Nothing)
    If shell_view <> IntPtr.Zero Then
      _found_shell_view = shell_view
      Return False
    End If
    Return True
  End Function

  ''' <summary>
  ''' Re-sets the clipboard data object to fix issues with file drops.
  ''' </summary>
  Public Shared Sub FixCopiedFileList()
    Dim original_data As IDataObject = Clipboard.GetDataObject()
    Dim file_list As System.Collections.Specialized.StringCollection = Clipboard.GetFileDropList()

    If file_list.Count > 0 Then
      Dim new_data As New DataObject()
      new_data.SetFileDropList(file_list)
      If original_data.GetDataPresent("Preferred DropEffect") Then new_data.SetData("Preferred DropEffect", original_data.GetData("Preferred DropEffect"))

      For attempt_count As Integer = 1 To 5
        Try
          Clipboard.SetDataObject(new_data, True) : Exit For
        Catch clipboard_exception As ExternalException
          Thread.Sleep(100)
        End Try
      Next
    End If
  End Sub

  Private Shared Function GetDesktopListViewHandle() As IntPtr
    Dim hProgman As IntPtr = FindWindow("Progman", Nothing)
    Dim hShellViewWin As IntPtr = FindWindowEx(hProgman, IntPtr.Zero, "SHELLDLL_DefView", Nothing)

    If hShellViewWin = IntPtr.Zero Then
      _found_shell_view = IntPtr.Zero
      EnumWindows(AddressOf FindWorkerW, IntPtr.Zero)
      hShellViewWin = _found_shell_view
    End If

    Return FindWindowEx(hShellViewWin, IntPtr.Zero, "SysListView32", Nothing)
  End Function

  Private Shared Function GetItemText(hwnd As IntPtr, hProcess As IntPtr, sharedMem As IntPtr, index As Integer) As String
    Dim bytes_moved As Integer = 0
    Dim item As New LVITEM
    item.mask = LVIF_TEXT
    item.iItem = index
    item.iSubItem = 0
    item.cchTextMax = 260
    item.pszText = IntPtr.Add(sharedMem, Marshal.SizeOf(GetType(LVITEM)))

    WriteProcessMemory(hProcess, sharedMem, item, Marshal.SizeOf(item), bytes_moved)
    SendMessage(hwnd, LVM_GETITEMTEXT, New IntPtr(index), sharedMem)

    Dim local_buffer(260) As Byte
    Dim text_offset As IntPtr = IntPtr.Add(sharedMem, Marshal.SizeOf(GetType(LVITEM)))
    ReadProcessMemory(hProcess, text_offset, local_buffer, 260, bytes_moved)

    Dim raw_text As String = Encoding.Default.GetString(local_buffer)
    Dim null_index As Integer = raw_text.IndexOf(Chr(0))
    If null_index >= 0 Then raw_text = raw_text.Substring(0, null_index)

    Return raw_text
  End Function

  ''' <summary>
  ''' Reads a text file (format "Name*X*Y") and restores the icon positions.
  ''' </summary>
  Public Shared Sub LayoutRestore(file_path As String)
    If file_path.Length > 0 Then
      Dim icons As New List(Of DesktopIcon)
      Dim desktop_icon As DesktopIcon
      Dim line_parts As String()

      With New IO.StreamReader(file_path)
        While Not .EndOfStream
          line_parts = .ReadLine.Trim.Split("*"c)
          If line_parts.Length = 3 Then
            desktop_icon = New DesktopIcon
            desktop_icon.Name = line_parts(0)
            Try
              desktop_icon.Position = New Point(CInt(line_parts(1)), CInt(line_parts(2)))
              icons.Add(desktop_icon)
            Catch
              ' Ignore malformed lines
            End Try
          End If
        End While
        .Close()
      End With

      RestoreIcons(icons)
    End If
  End Sub

  ''' <summary>
  ''' Saves the current icon layout to a text file in the format "Name*X*Y".
  ''' </summary>
  Public Shared Sub LayoutSave(file_path As String)
    Dim icons = SaveIcons()
    With IO.File.CreateText(file_path)
      For Each desktop_icon As DesktopIcon In icons : .WriteLine(desktop_icon.Name & "*" & desktop_icon.Position.X & "*" & desktop_icon.Position.Y) : Next
      .Close()
    End With
  End Sub

  Private Shared Function MakeLParam(lo As Integer, hi As Integer) As Integer
    Return (lo And &HFFFF) Or ((hi And &HFFFF) << 16)
  End Function

  ''' <summary>
  ''' Restores icons to their saved positions.
  ''' Note: "Auto Arrange" must be OFF on the desktop for this to work.
  ''' </summary>
  Public Shared Sub RestoreIcons(saved_icons As List(Of DesktopIcon))
    Dim hwndDesktop As IntPtr = GetDesktopListViewHandle()
    If hwndDesktop = IntPtr.Zero Then Exit Sub

    Dim current_icons = SaveIcons()

    For Each saved In saved_icons
      ' Case Insensitive Match
      Dim match = current_icons.FindIndex(Function(x) x.Name.Equals(saved.Name, StringComparison.OrdinalIgnoreCase))

      If match <> -1 Then
        Dim lparam As Integer = MakeLParam(saved.Position.X, saved.Position.Y)
        SendMessage(hwndDesktop, LVM_SETITEMPOSITION, New IntPtr(match), New IntPtr(lparam))
      End If
    Next
  End Sub

  ''' <summary>
  ''' Scans the desktop and returns a list of Icon Names and their current X,Y coordinates.
  ''' </summary>
  Public Shared Function SaveIcons() As List(Of DesktopIcon)
    Dim icons As New List(Of DesktopIcon)
    Dim hwndDesktop As IntPtr = GetDesktopListViewHandle()
    If hwndDesktop = IntPtr.Zero Then Return icons

    Dim count As Integer = SendMessage(hwndDesktop, LVM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero)
    Dim vProcessId As Integer = 0
    GetWindowThreadProcessId(hwndDesktop, vProcessId)

    Dim hProcess As IntPtr = OpenProcess(PROCESS_VM_OPERATION Or PROCESS_VM_READ Or PROCESS_VM_WRITE, False, vProcessId)
    Dim sharedMem As IntPtr = VirtualAllocEx(hProcess, IntPtr.Zero, 4096, MEM_COMMIT, PAGE_READWRITE)

    Try
      For i As Integer = 0 To count - 1
        Dim pt As New POINTAPI()
        WriteProcessMemory(hProcess, sharedMem, pt, Marshal.SizeOf(pt), IntPtr.Zero)
        SendMessage(hwndDesktop, LVM_GETITEMPOSITION, New IntPtr(i), sharedMem)
        ReadProcessMemory(hProcess, sharedMem, pt, Marshal.SizeOf(pt), IntPtr.Zero)

        Dim text As String = GetItemText(hwndDesktop, hProcess, sharedMem, i)
        icons.Add(New DesktopIcon With {.Name = text, .Position = New Point(pt.X, pt.Y)})
      Next
    Finally
      VirtualFreeEx(hProcess, sharedMem, 0, MEM_RELEASE)
      CloseHandle(hProcess)
    End Try

    Return icons
  End Function

  Public Shared Sub ToggleDesktopPainting(enable_painting As Boolean)
    Dim hwnd_desktop As IntPtr = GetDesktopListViewHandle()
    If hwnd_desktop = IntPtr.Zero Then Exit Sub

    If enable_painting Then
      ' Unlock painting and force an immediate full redraw
      SendMessage(hwnd_desktop, WM_SETREDRAW, New IntPtr(1), IntPtr.Zero)
      RedrawWindow(hwnd_desktop, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE Or RDW_ALLCHILDREN Or RDW_UPDATENOW)
    Else
      ' Freeze the window so no updates are drawn
      SendMessage(hwnd_desktop, WM_SETREDRAW, New IntPtr(0), IntPtr.Zero)
    End If
  End Sub

  ' -----------------------------------------------------------------------------------------
  ' TIMING RATIONALE:
  ' WaitForDesktopRender is hardcoded to a 500ms sleep cycle with a 3-frame stability check.
  '
  ' Windows Explorer populates the SysListView32 in "batches" (e.g., 500 icons, pause, 500 icons).
  ' A faster check (e.g., 100ms) risks catching the counter during a "pause" between batches,
  ' causing the code to proceed prematurely and leaving half the icons stranded in the default grid.
  '
  ' The 500ms delay ensures we bridge those processing gaps. This sacrifice in visual speed
  ' (approx 1.5s delay) guarantees data integrity on slower CPUs or mechanical HDDs.
  ' -----------------------------------------------------------------------------------------
  ''' <summary>
  ''' Waits until the Desktop icon count is greater than 0 and stabilizes.
  ''' </summary>
  Public Shared Function WaitForDesktopRender(Optional ByVal timeout_seconds As Integer = 10) As Boolean
    Dim hwnd As IntPtr = GetDesktopListViewHandle()
    If hwnd <> IntPtr.Zero Then
      Dim last_count As Integer = -1
      Dim current_count As Integer = 0
      Dim stable_frames As Integer = 0
      Dim stop_watch As Stopwatch = Stopwatch.StartNew()

      While stop_watch.Elapsed.TotalSeconds < timeout_seconds
        current_count = SendMessage(hwnd, LVM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero)

        If current_count > 0 AndAlso current_count = last_count Then
          stable_frames += 1
        Else
          stable_frames = 0
        End If

        If stable_frames >= 3 Then Return True

        last_count = current_count
        Thread.Sleep(350)
      End While
    End If
    Return False
  End Function

#End Region

#Region "Native Interop - Constants"

  Private Const LVIF_TEXT As Integer = &H1
  Private Const LVM_FIRST As Integer = &H1000
  Private Const LVM_GETITEMCOUNT As Integer = LVM_FIRST + 4
  Private Const LVM_GETITEMPOSITION As Integer = LVM_FIRST + 16
  Private Const LVM_GETITEMTEXT As Integer = LVM_FIRST + 45
  Private Const LVM_SETITEMPOSITION As Integer = LVM_FIRST + 15
  Private Const MEM_COMMIT As Integer = &H1000
  Private Const MEM_RELEASE As Integer = &H8000
  Private Const PAGE_READWRITE As Integer = &H4
  Private Const PROCESS_VM_OPERATION As Integer = &H8
  Private Const PROCESS_VM_READ As Integer = &H10
  Private Const PROCESS_VM_WRITE As Integer = &H20
  Private Const RDW_ALLCHILDREN As Integer = &H80
  Private Const RDW_INVALIDATE As Integer = &H1
  Private Const RDW_UPDATENOW As Integer = &H100
  Private Const WM_SETREDRAW As Integer = 11

#End Region

#Region "Native Interop - Delegates & Structures"

  Public Delegate Function EnumWindowsProc(hwnd As IntPtr, lParam As IntPtr) As Boolean

  <StructLayout(LayoutKind.Sequential)>
  Private Structure LVITEM
    Public mask As UInteger
    Public iItem As Integer
    Public iSubItem As Integer
    Public state As UInteger
    Public stateMask As UInteger
    Public pszText As IntPtr
    Public cchTextMax As Integer
    Public iImage As Integer
    Public lParam As IntPtr
    Public iIndent As Integer
    Public iGroupId As Integer
    Public cColumns As UInteger
    Public puColumns As IntPtr
    Public piColFmt As IntPtr
    Public iGroup As Integer
  End Structure

  <StructLayout(LayoutKind.Sequential)>
  Private Structure POINTAPI
    Public X As Integer
    Public Y As Integer
  End Structure

#End Region

#Region "Native Interop - External Functions"

  <DllImport("kernel32.dll")>
  Private Shared Function CloseHandle(hObject As IntPtr) As Boolean
  End Function

  <DllImport("user32.dll")>
  Private Shared Function EnumWindows(lpEnumFunc As EnumWindowsProc, lParam As IntPtr) As Boolean
  End Function

  <DllImport("user32.dll")>
  Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
  End Function

  <DllImport("user32.dll")>
  Private Shared Function FindWindowEx(parent As IntPtr, child As IntPtr, className As String, windowName As String) As IntPtr
  End Function

  <DllImport("user32.dll")>
  Private Shared Function GetWindowThreadProcessId(hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
  End Function

  <DllImport("kernel32.dll")>
  Private Shared Function OpenProcess(dwDesiredAccess As Integer, bInheritHandle As Boolean, dwProcessId As Integer) As IntPtr
  End Function

  <DllImport("kernel32.dll", SetLastError:=True)>
  Private Shared Function ReadProcessMemory(hProcess As IntPtr, lpBaseAddress As IntPtr, ByRef lpBuffer As POINTAPI, nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Boolean
  End Function

  <DllImport("kernel32.dll", SetLastError:=True)>
  Private Shared Function ReadProcessMemory(hProcess As IntPtr, lpBaseAddress As IntPtr, <Out> lpBuffer() As Byte, nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Boolean
  End Function

  <DllImport("user32.dll")>
  Private Shared Function RedrawWindow(hWnd As IntPtr, lprcUpdate As IntPtr, hrgnUpdate As IntPtr, flags As Integer) As Boolean
  End Function

  <DllImport("user32.dll")>
  Private Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
  End Function

  <DllImport("kernel32.dll")>
  Private Shared Function VirtualAllocEx(hProcess As IntPtr, lpAddress As IntPtr, dwSize As Integer, flAllocationType As Integer, flProtect As Integer) As IntPtr
  End Function

  <DllImport("kernel32.dll")>
  Private Shared Function VirtualFreeEx(hProcess As IntPtr, lpAddress As IntPtr, dwSize As Integer, dwFreeType As Integer) As Boolean
  End Function

  <DllImport("kernel32.dll", SetLastError:=True)>
  Private Shared Function WriteProcessMemory(hProcess As IntPtr, lpBaseAddress As IntPtr, ByRef lpBuffer As POINTAPI, nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Boolean
  End Function

  <DllImport("kernel32.dll", SetLastError:=True)>
  Private Shared Function WriteProcessMemory(hProcess As IntPtr, lpBaseAddress As IntPtr, ByRef lpBuffer As LVITEM, nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Boolean
  End Function

#End Region

#Region "Structures"

  ''' <summary>
  ''' Data structure to hold icon info
  ''' </summary>
  Public Structure DesktopIcon
    Public Name As String
    Public Position As Point
  End Structure

#End Region

End Class
