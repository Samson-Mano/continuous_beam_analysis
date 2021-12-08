Public Class addmember
    Dim t As New ToolTip

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Me.Text = "Modify Member" Then
        
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If Val(TextBox1.Text) = 0 Or Val(TextBox1.Text) < 0 Then
            TextBox1.Text = 4
        End If
    End Sub


    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If IsNumeric(TextBox2.Text) Then

        Else
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If IsNumeric(TextBox3.Text) Then

        Else
            TextBox3.Text = ""
        End If
    End Sub

    Private Sub Button_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ok.Click
        If Val(TextBox1.Text) < 0.1 Then
            TextBox1.Text = 10
            MsgBox("Sorry Length limited to 0.1 units")
            Exit Sub
        End If

        If Me.Text = "Add Member" Then
            Dim TEMPmem As New Member
            TEMPmem.spanlength = Val(TextBox1.Text)
            TEMPmem.Emodulus = Val(TextBox2.Text)
            TEMPmem.Inertia = Val(TextBox3.Text)
            TEMPmem.g = Val(TextBox6.Text)
            mem.Add(TEMPmem)
            Me.Close()
            beamcreate.mainpic.Refresh()
        ElseIf Me.Text = "Modify Member" Then
            mem(selline).spanlength = Val(TextBox1.Text)
            mem(selline).Emodulus = Val(TextBox2.Text)
            mem(selline).Inertia = Val(TextBox3.Text)
            mem(selline).g = Val(TextBox6.Text)
            Dim rp As New List(Of Integer)
            '---point load out of span
            For Each pitm In mem(selline).Pload
                If pitm.pdist > mem(selline).spanlength Then
                    rp.Add(mem(selline).Pload.IndexOf(pitm))
                End If
            Next
            rp.Reverse()
            If rp.Count <> 0 Then
                For Each i In rp
                    mem(selline).Pload.RemoveAt(i)
                Next
            End If
            rp.Clear()
            '---UVL out of span
            For Each uitm In mem(selline).Uload
                If uitm.udist2 > mem(selline).spanlength Then
                    rp.Add(mem(selline).Uload.IndexOf(uitm))
                End If
            Next
            rp.Reverse()
            If rp.Count <> 0 Then
                For Each i In rp
                    mem(selline).Uload.RemoveAt(i)
                Next
            End If
            rp.Clear()
            '---Moment out of span
            For Each mitm In mem(selline).Mload
                If mitm.mdist > mem(selline).spanlength Then
                    rp.Add(mem(selline).Mload.IndexOf(mitm))
                End If
            Next
            rp.Reverse()
            If rp.Count <> 0 Then
                For Each i In rp
                    mem(selline).Mload.RemoveAt(i)
                Next
            End If
            rp.Clear()
            Me.Close()
            beamcreate.mainpic.Refresh()
        End If
    End Sub

    Private Sub Button_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_cancel.Click
        Me.Close()
        beamcreate.mainpic.Refresh()
    End Sub
End Class