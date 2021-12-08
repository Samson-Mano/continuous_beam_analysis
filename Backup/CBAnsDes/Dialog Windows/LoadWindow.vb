Public Class LoadWindow
    Private _Clength As Single
    Private _maxload As Single
    Private _Curline As Integer
    Private _CurP As Member.P
    Private _CurU As Member.U
    Private _CurM As Member.M
    Dim H As New List(Of History)
    Dim t As System.EventArgs
    Dim ob As System.Object

    Private Class History
        Public Sub New(ByVal p As Member.P)
            pl = p
        End Sub
        Public Sub New(ByVal u As Member.U)
            ul = u
        End Sub
        Public Sub New(ByVal m As Member.M)
            ml = m
        End Sub
        Private _pl As Member.P
        Private _ul As Member.U
        Private _ml As Member.M

        Public Property pl() As Member.P
            Get
                Return _pl
            End Get
            Set(ByVal value As Member.P)
                _pl = value
            End Set
        End Property
        Public Property ul() As Member.U
            Get
                Return _ul
            End Get
            Set(ByVal value As Member.U)
                _ul = value
            End Set
        End Property
        Public Property ml() As Member.M
            Get
                Return _ml
            End Get
            Set(ByVal value As Member.M)
                _ml = value
            End Set
        End Property
    End Class

    Public Property maxload() As Single
        Get
            Return _maxload
        End Get
        Set(ByVal value As Single)
            _maxload = value
        End Set
    End Property

    Public Property Clength() As Single
        Get
            Return _Clength
        End Get
        Set(ByVal value As Single)
            _Clength = value
        End Set
    End Property

    Public Property Curline() As Integer
        Get
            Return _Curline
        End Get
        Set(ByVal value As Integer)
            _Curline = value
        End Set
    End Property

    Public Sub New(ByVal L As Single, ByVal C As Integer, ByVal CL As Member.P)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _Clength = L
        _Curline = C
        _CurP = CL
        Me.Text = "Modify Point Load Window"
        Dim GGload As Single = 0
        '----Point Load
        For Each Pitm In mem(C).Pload
            If GGload < Pitm.pload Then
                GGload = Pitm.pload
            End If
        Next
        '----UVL
        For Each Uitm In mem(C).Uload
            If GGload < Uitm.uload1 Then
                GGload = Uitm.uload1
            End If
            If GGload < Uitm.uload2 Then
                GGload = Uitm.uload2
            End If
        Next
        If GGload < 10 Then
            GGload = 10
        End If
        maxload = GGload

        TabControl1.TabPages.Remove(TabPage2)
        TabControl1.TabPages.Remove(TabPage3)
        Button8.Enabled = False

        TrackBar1.Maximum = mem(Curline).spanlength * 100
        TrackBar1.Value = CL.pdist * 100
        TrackBar1_Scroll(ob, t)
        TextBox1.Text = CL.pload
        ' Add any initialization after the InitializeComponent() call.
        Panel1.Refresh()
    End Sub

    Public Sub New(ByVal L As Single, ByVal C As Integer, ByVal CL As Member.U)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _Clength = L
        _Curline = C
        _CurU = CL
        Me.Text = "Modify UVL Window"
        Dim GGload As Single = 0
        '----Point Load
        For Each Pitm In mem(C).Pload
            If GGload < Pitm.pload Then
                GGload = Pitm.pload
            End If
        Next
        '----UVL
        For Each Uitm In mem(C).Uload
            If GGload < Uitm.uload1 Then
                GGload = Uitm.uload1
            End If
            If GGload < Uitm.uload2 Then
                GGload = Uitm.uload2
            End If
        Next
        If GGload < 10 Then
            GGload = 10
        End If
        maxload = GGload

        TabControl1.TabPages.Remove(TabPage1)
        TabControl1.TabPages.Remove(TabPage3)
        Button2.Enabled = False

        TrackBar3.Maximum = mem(Curline).spanlength * 100
        TrackBar4.Maximum = mem(Curline).spanlength * 100
        TrackBar3.Value = CL.udist1 * 100
        TrackBar4.Value = CL.udist2 * 100
        TrackBar3_Scroll(ob, t)
        TrackBar4_Scroll(ob, t)
        TextBox3.Text = CL.uload1
        TextBox4.Text = CL.uload2
        ' Add any initialization after the InitializeComponent() call.
        Panel1.Refresh()
    End Sub

    Public Sub New(ByVal L As Single, ByVal C As Integer, ByVal CL As Member.M)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _Clength = L
        _Curline = C
        _CurM = CL
        Me.Text = "Modify Moment Window"
        Dim GGload As Single = 0
        '----Point Load
        For Each Pitm In mem(C).Pload
            If GGload < Pitm.pload Then
                GGload = Pitm.pload
            End If
        Next
        '----UVL
        For Each Uitm In mem(C).Uload
            If GGload < Uitm.uload1 Then
                GGload = Uitm.uload1
            End If
            If GGload < Uitm.uload2 Then
                GGload = Uitm.uload2
            End If
        Next
        If GGload < 10 Then
            GGload = 10
        End If
        maxload = GGload

        TabControl1.TabPages.Remove(TabPage1)
        TabControl1.TabPages.Remove(TabPage2)
        Button9.Enabled = False

        TrackBar2.Maximum = mem(Curline).spanlength * 100
        TrackBar2.Value = CL.mdist * 100
        TrackBar2_Scroll(ob, t)
        TextBox2.Text = CL.mload
        ' Add any initialization after the InitializeComponent() call.
        Panel1.Refresh()
    End Sub

    Public Sub New(ByVal L As Single, ByVal C As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _Clength = L
        _Curline = C
        Me.Text = "Add Load Window"
        Dim GGload As Single = 0
        '----Point Load
        For Each Pitm In mem(C).Pload
            If GGload < Pitm.pload Then
                GGload = Pitm.pload
            End If
        Next
        '----UVL
        For Each Uitm In mem(C).Uload
            If GGload < Uitm.uload1 Then
                GGload = Uitm.uload1
            End If
            If GGload < Uitm.uload2 Then
                GGload = Uitm.uload2
            End If
        Next
        If GGload < 10 Then
            GGload = 10
        End If

        Button3.Enabled = False
        Button4.Enabled = False
        Button7.Enabled = False
        maxload = GGload
        ' Add any initialization after the InitializeComponent() call.
        TrackBar1.Maximum = Clength * 100
        TrackBar2.Maximum = Clength * 100
        TrackBar3.Maximum = Clength * 100
        TrackBar4.Maximum = Clength * 100
        TrackBar4.Value = Clength * 100
        TextBox1.Text = 10
        TextBox2.Text = 10
        TextBox3.Text = 10
        TextBox4.Text = 10
    End Sub

#Region "Track Bar Zone"

    Private Sub LoadWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Label14.Text = TrackBar4.Value / 100
        AddHandler TabControl1.SelectedIndexChanged, AddressOf MAXl
        AddHandler TabControl1.SelectedIndexChanged, AddressOf Panel1.Refresh
        AddHandler Me.Button3.Click, AddressOf beamcreate.mainpic.Refresh
        AddHandler Me.Button4.Click, AddressOf beamcreate.mainpic.Refresh
        AddHandler Me.Button7.Click, AddressOf beamcreate.mainpic.Refresh
    End Sub

    Private Sub MAXl()
        Dim GGload As Single = 0
        '----Point Load
        For Each Pitm In mem(Curline).Pload
            If GGload < Pitm.pload Then
                GGload = Pitm.pload
            End If
        Next
        '----UVL
        For Each Uitm In mem(Curline).Uload
            If GGload < Uitm.uload1 Then
                GGload = Uitm.uload1
            End If
            If GGload < Uitm.uload2 Then
                GGload = Uitm.uload2
            End If
        Next
        If TabControl1.SelectedTab.Contains(Label1) Then
            If GGload < Val(TextBox1.Text) Then
                GGload = Val(TextBox1.Text)
            End If
        ElseIf TabControl1.SelectedTab.Contains(Label2) Then
            If GGload < Val(TextBox3.Text) Then
                GGload = Val(TextBox3.Text)
            End If
            If GGload < Val(TextBox4.Text) Then
                GGload = Val(TextBox4.Text)
            End If
        End If
        maxload = GGload
        Panel1.Refresh()
    End Sub

#End Region

#Region "Panel1 Events"
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        paintBeam(e)
        paintTl(e)
        paintOl(e)
    End Sub

    Private Sub paintBeam(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim BeamPen As New System.Drawing.Pen(Color.Blue, 2)
        Dim dimpen As New System.Drawing.Pen(Color.CadetBlue, 1.5)
        Dim adcap As New System.Drawing.Drawing2D.AdjustableArrowCap(3, 5)

        dimpen.CustomStartCap = adcap
        dimpen.CustomEndCap = adcap
        e.Graphics.DrawLine(BeamPen, 30, 130, 430, 130)
        e.Graphics.DrawLine(dimpen, 30, 150, 430, 150)
        e.Graphics.DrawString(_Clength, Font, dimpen.Brush, 230, 148)
    End Sub

    Private Sub paintTl(ByVal e As System.Windows.Forms.PaintEventArgs)
        If TabControl1.SelectedTab.Contains(Label1) Then
            paintPointload(e)
        ElseIf TabControl1.SelectedTab.Contains(Label2) Then
            paintUVL(e)
        ElseIf TabControl1.SelectedTab.Contains(Label3) Then
            paintMoment(e)
        End If
    End Sub

    Private Sub paintOl(ByVal e As System.Windows.Forms.PaintEventArgs)
        puload(e)
        pmload(e)
        ppload(e)
    End Sub

#Region "Paint Point load"
    Private Sub paintPointload(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim loadpen As New System.Drawing.Pen(Color.Green, 2)
        Dim adcap As New System.Drawing.Drawing2D.AdjustableArrowCap(3, 5)
        Dim toSX As Single
        Dim toSY As Single
        loadpen.DashStyle = Drawing2D.DashStyle.Dash
        loadpen.CustomStartCap = adcap
        toSX = 30 + (Val(Label12.Text) * (400 / Clength))
        toSY = Val(TextBox1.Text) * (90 / maxload)

        e.Graphics.DrawLine(loadpen, toSX, 130, toSX, 130 - toSY)
        e.Graphics.DrawString(Val(TextBox1.Text), Font, loadpen.Brush, toSX, 110 - toSY)

    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        Label12.Text = TrackBar1.Value / 100
        Panel1.Refresh()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If Val(TextBox1.Text) <= 0 Then
            TextBox1.Text = 10
        End If
        MAXl()
        Panel1.Refresh()
    End Sub

    Private Sub ppload(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim loadpen As New System.Drawing.Pen(Color.Green, 2)
        Dim adcap As New System.Drawing.Drawing2D.AdjustableArrowCap(3, 5)
        Dim toSX As Single
        Dim toSY As Single

        loadpen.CustomStartCap = adcap
        For Each itm In mem(Curline).Pload
            toSX = 30 + (itm.pdist * (400 / Clength))
            toSY = itm.pload * (90 / maxload)

            e.Graphics.DrawLine(loadpen, toSX, 130, toSX, 130 - toSY)
            e.Graphics.DrawString(itm.pload, Font, loadpen.Brush, toSX, 110 - toSY)
        Next
    End Sub
#End Region

#Region "Paint UVL"

    Private Sub TrackBar3_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar3.Scroll
        If TrackBar3.Value >= TrackBar4.Value Then
            TrackBar3.Value = TrackBar4.Value - 1
        End If
        Label13.Text = (TrackBar3.Value / 100)
        Panel1.Refresh()
    End Sub

    Private Sub TrackBar4_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar4.Scroll
        If TrackBar4.Value <= TrackBar3.Value Then
            TrackBar4.Value = TrackBar3.Value + 1
        End If
        Label14.Text = TrackBar4.Value / 100
        Panel1.Refresh()
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If Val(TextBox3.Text) <= 0 Then
            TextBox3.Text = 10
        End If
        MAXl()
        Panel1.Refresh()
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        If Val(TextBox4.Text) <= 0 Then
            TextBox4.Text = 10
        End If
        MAXl()
        Panel1.Refresh()
    End Sub

    Private Sub paintUVL(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim loadpen As New System.Drawing.Pen(Color.DeepPink, 1)
        Dim adcap As New System.Drawing.Drawing2D.AdjustableArrowCap(2, 4)
        Dim toSX1, toSX2 As Single
        Dim toSY1, toSY2 As Single
        loadpen.DashStyle = Drawing2D.DashStyle.Dash
        loadpen.CustomStartCap = adcap
        toSX1 = 30 + (Val(Label13.Text) * (400 / Clength))
        toSX2 = 30 + (Val(Label14.Text) * (400 / Clength))
        toSY1 = Val(TextBox3.Text) * (90 / maxload)
        toSY2 = Val(TextBox4.Text) * (90 / maxload)

        e.Graphics.DrawLine(loadpen, toSX1, 130, toSX1, 130 - toSY1)
        e.Graphics.DrawString(Val(TextBox3.Text), Font, loadpen.Brush, toSX1, 110 - toSY1)

        If toSY2 = toSY1 Then
            For i = toSX1 To toSX2 Step 10
                e.Graphics.DrawLine(loadpen, i, 130, i, 130 - (toSY1))
            Next
        ElseIf toSY2 > toSY1 Then
            For i = toSX1 To toSX2 Step 10
                e.Graphics.DrawLine(loadpen, i, 130, i, 130 - (toSY1 + (((toSY2 - toSY1) / (toSX2 - toSX1)) * (i - toSX1))))
            Next
        Else
            For i = toSX1 To toSX2 Step 10
                e.Graphics.DrawLine(loadpen, i, 130, i, 130 - (toSY2 + (((toSY1 - toSY2) / (toSX2 - toSX1)) * (toSX2 - i))))
            Next
        End If
        e.Graphics.DrawLine(loadpen, toSX2, 130, toSX2, 130 - toSY2)
        e.Graphics.DrawString(Val(TextBox4.Text), Font, loadpen.Brush, toSX2, 110 - toSY2)
    End Sub

    Private Sub puload(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim loadpen As New System.Drawing.Pen(Color.DeepPink, 1)
        Dim adcap As New System.Drawing.Drawing2D.AdjustableArrowCap(2, 4)
        Dim toSX1, toSX2 As Single
        Dim toSY1, toSY2 As Single

        loadpen.CustomStartCap = adcap
        For Each itm In mem(Curline).Uload
            toSX1 = 30 + (itm.udist1 * (400 / Clength))
            toSX2 = 30 + (itm.udist2 * (400 / Clength))
            toSY1 = itm.uload1 * (90 / maxload)
            toSY2 = itm.uload2 * (90 / maxload)

            e.Graphics.DrawLine(loadpen, toSX1, 130, toSX1, 130 - toSY1)
            e.Graphics.DrawString(itm.uload1, Font, loadpen.Brush, toSX1, 110 - toSY1)

            If toSY2 = toSY1 Then
                For i = toSX1 To toSX2 Step 10
                    e.Graphics.DrawLine(loadpen, i, 130, i, 130 - (toSY1))
                Next
            ElseIf toSY2 > toSY1 Then
                For i = toSX1 To toSX2 Step 10
                    e.Graphics.DrawLine(loadpen, i, 130, i, 130 - (toSY1 + (((toSY2 - toSY1) / (toSX2 - toSX1)) * (i - toSX1))))
                Next
            Else
                For i = toSX1 To toSX2 Step 10
                    e.Graphics.DrawLine(loadpen, i, 130, i, 130 - (toSY2 + (((toSY1 - toSY2) / (toSX2 - toSX1)) * (toSX2 - i))))
                Next
            End If
            e.Graphics.DrawLine(loadpen, toSX2, 130, toSX2, 130 - toSY2)
            e.Graphics.DrawString(itm.uload2, Font, loadpen.Brush, toSX2, 110 - toSY2)
        Next
    End Sub
#End Region

#Region "Paint Moment"
    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        Label15.Text = TrackBar2.Value / 100
        Panel1.Refresh()
    End Sub

    Private Sub paintMoment(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim loadpen As New System.Drawing.Pen(Color.Orange, 2)
        Dim adcap As New System.Drawing.Drawing2D.AdjustableArrowCap(3, 5)
        Dim toSX As Single
        loadpen.DashStyle = Drawing2D.DashStyle.Dash
        If RadioButton1.Checked = True Then
            loadpen.CustomStartCap = adcap
        Else
            loadpen.CustomEndCap = adcap
        End If

        toSX = 30 + (Val(Label15.Text) * (400 / Clength))
        e.Graphics.DrawArc(loadpen, toSX - 15, 115, 30, 30, 270, 180)
        e.Graphics.DrawString(Val(TextBox2.Text), Font, loadpen.Brush, toSX, 80)
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Panel1.Refresh()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Panel1.Refresh()
    End Sub

    Private Sub pmload(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim loadpen As New System.Drawing.Pen(Color.Orange, 2)
        Dim adcap As New System.Drawing.Drawing2D.AdjustableArrowCap(3, 5)
        Dim toSX As Single

        For Each itm In mem(Curline).Mload
            If itm.mload > 0 Then
                loadpen.CustomStartCap = adcap
            Else
                loadpen.CustomEndCap = adcap
            End If
            toSX = 30 + (itm.mdist * (400 / Clength))
            e.Graphics.DrawArc(loadpen, toSX - 15, 115, 30, 30, 270, 180)
            e.Graphics.DrawString(Math.Abs(itm.mload), Font, loadpen.Brush, toSX, 80)
        Next
    End Sub
#End Region
#End Region

#Region "ADD Events"
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        '--Point Load ADD
        Dim temp As Member.P
        temp.pload = Val(TextBox1.Text)
        temp.pdist = Val(Label12.Text)

        '----chk for conflict
        For Each itm In mem(Curline).Pload
            If itm.pdist = temp.pdist Then
                Exit Sub
            End If
        Next
        mem(Curline).Pload.Add(temp)
        Panel1.Refresh()
        beamcreate.mainpic.Refresh()
        Dim tempH As New History(mem(Curline).Pload(mem(Curline).Pload.Count - 1))
        H.Add(tempH)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        '--UVL ADD
        Dim temp As Member.U
        temp.uload1 = Val(TextBox3.Text)
        temp.uload2 = Val(TextBox4.Text)
        temp.udist1 = Val(Label13.Text)
        temp.udist2 = Val(Label14.Text)
        '----chk for conflict
        For Each itm In mem(Curline).Uload
            If itm.udist1 = temp.udist1 And itm.udist2 = temp.udist2 Then
                Exit Sub
            End If
        Next
        mem(Curline).Uload.Add(temp)
        Panel1.Refresh()
        beamcreate.mainpic.Refresh()
        Dim tempH As New History(mem(Curline).Uload(mem(Curline).Uload.Count - 1))
        H.Add(tempH)
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        '--Moment ADD
        Dim temp As Member.M
        If RadioButton1.Checked = True Then '---- Clockwise
            temp.mload = Val(TextBox2.Text)
        Else '---- Anti clockwise
            temp.mload = -1 * Val(TextBox2.Text)
        End If

        temp.mdist = Val(Label15.Text)
        '----chk for conflict
        For Each itm In mem(Curline).Mload
            If itm.mdist = temp.mdist Then
                Exit Sub
            End If
        Next
        mem(Curline).Mload.Add(temp)
        Panel1.Refresh()
        beamcreate.mainpic.Refresh()
        Dim tempH As New History(mem(Curline).Mload(mem(Curline).Mload.Count - 1))
        H.Add(tempH)
    End Sub
#End Region

#Region "Modify Events"
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        '--Point Load MODIFY
        Dim temp As Member.P
        temp.pload = Val(TextBox1.Text)
        temp.pdist = Val(Label12.Text)
        mem(Curline).Pload.Insert(mem(Curline).Pload.IndexOf(_CurP), temp)
        mem(Curline).Pload.Remove(_CurP)
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        '--UVL MODIFY
        Dim temp As Member.U
        temp.uload1 = Val(TextBox3.Text)
        temp.uload2 = Val(TextBox4.Text)
        temp.udist1 = Val(Label13.Text)
        temp.udist2 = Val(Label14.Text)
        mem(Curline).Uload.Insert(mem(Curline).Uload.IndexOf(_CurU), temp)
        mem(Curline).Uload.Remove(_CurU)
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        '--Moment MODIFY
        Dim temp As Member.M
        If RadioButton1.Checked = True Then '---- Clockwise
            temp.mload = Val(TextBox2.Text)
        Else '---- Anti clockwise
            temp.mload = -1 * Val(TextBox2.Text)
        End If
        temp.mdist = Val(Label15.Text)
        mem(Curline).Mload.Insert(mem(Curline).Mload.IndexOf(_CurM), temp)
        mem(Curline).Mload.Remove(_CurM)
        Me.Close()
    End Sub

#End Region

    Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        If H.Count <> 0 Then
            mem(Curline).Pload.Remove(H(H.Count - 1).pl)
            mem(Curline).Uload.Remove(H(H.Count - 1).ul)
            mem(Curline).Mload.Remove(H(H.Count - 1).ml)

            H.RemoveAt(H.Count - 1)
            Panel1.Refresh()
            beamcreate.mainpic.Refresh()
        End If
        Panel1.Refresh()
    End Sub
End Class