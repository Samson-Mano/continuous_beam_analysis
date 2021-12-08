Public Class Ends_Editor
    Dim t As New ToolTip
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    '---Check Option Free - Free
    Private Sub Ends_Editor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If mem.Count < 3 Then
            RadioButton4.Enabled = False
        End If
        If mem.Count < 2 Then
            RadioButton6.Enabled = False
        End If
    End Sub

    Private Sub Button_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ok.Click

        Me.Close()

        If RadioButton1.Checked = True Then
            ends = 1         'Fixed-Fixed
        ElseIf RadioButton2.Checked = True Then
            ends = 2         'Fixed-Free
        ElseIf RadioButton3.Checked = True Then
            ends = 3         'Pinned-Pinned
        ElseIf RadioButton4.Checked = True Then
            If mem.Count < 3 Then
                Exit Sub
            End If
            ends = 4         'Free-Free
        ElseIf RadioButton5.Checked = True Then
            ends = 5         'Fixed-Pinned
        ElseIf RadioButton6.Checked = True Then 'pinned-free
            If mem.Count < 2 Then
                Exit Sub
            End If
            ends = 6
        End If
        MDIMain.Focus()
        beamcreate.mainpic.Refresh()
    End Sub

    Private Sub Button_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_cancel.Click
        Me.Close()
        MDIMain.Focus()
        beamcreate.mainpic.Refresh()
    End Sub
End Class