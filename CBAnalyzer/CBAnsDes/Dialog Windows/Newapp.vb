Public Class Newapp
    Dim t As New ToolTip
    Private _F As Boolean

    Public Property F() As Boolean
        Get
            Return _F
        End Get
        Set(ByVal value As Boolean)
            _F = value
        End Set
    End Property


    Public Sub New(ByVal FL As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _F = FL
        ' Add any initialization after the InitializeComponent() call.

    End Sub
   
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If Val(TextBox2.Text) = 0 Then
            TextBox2.Text = 10
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If Val(TextBox3.Text) = 0 Then
            TextBox3.Text = 1
        End If
        If TextBox3.Text.Contains(".") Then
            TextBox3.Text = 1
        End If
    End Sub

    Private Sub Newapp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    '---Check Option Free - Free
    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        If Val(TextBox3.Text) < 3 Then
            RadioButton1.Checked = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            TextBox1.Text = "Beam1"
        End If
    End Sub

    '---Check Option Pinned - Free
    Private Sub RadioButton6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton6.CheckedChanged
        If Val(TextBox3.Text) < 2 Then
            RadioButton1.Checked = True
        End If
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        If IsNumeric(TextBox4.Text) Then

        Else
            TextBox4.Text = ""
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If IsNumeric(TextBox5.Text) Then

        Else
            TextBox5.Text = ""
        End If
    End Sub

    Private Sub Button_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Ok.Click
        If Val(TextBox2.Text) < 0.1 Then
            TextBox2.Text = 10
            MsgBox("Sorry Length limited to 0.1 units")
            Exit Sub
        End If

        Dim nos As Integer
        nos = Val(TextBox3.Text)
        For i = 0 To (nos - 1)
            Dim TEMPmem As New Member
            TEMPmem.spanlength = Val(TextBox2.Text) / Val(TextBox3.Text)
            TEMPmem.Emodulus = Val(TextBox4.Text)
            TEMPmem.Inertia = Val(TextBox5.Text)
            TEMPmem.g = Val(TextBox6.Text)
            mem.Add(TEMPmem)
        Next
        If RadioButton1.Checked = True Then
            ends = 1         'Fixed-Fixed
        ElseIf RadioButton2.Checked = True Then
            ends = 2         'Fixed-Free
        ElseIf RadioButton3.Checked = True Then
            ends = 3         'Pinned-Pinned
        ElseIf RadioButton4.Checked = True Then
            ends = 4         'Free-Free
        ElseIf RadioButton5.Checked = True Then
            ends = 5         'Fixed-Pinned
        ElseIf RadioButton6.Checked = True Then
            ends = 6         'pinned-pinned
        End If

        _F = True
        Me.Close()
        MDIMain.Focus()
        beamcreate.mainpic.Refresh()
    End Sub

    Private Sub Button_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_cancel.Click
        _F = False
        Me.Close()
        beamcreate.mainpic.Refresh()
    End Sub
End Class