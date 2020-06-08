Public Class beamcreate
    Private ht As Single
    Private totL As Single
    Private _selPL As Member.P
    Private _selUL As Member.U
    Private _selML As Member.M
    Private _tipe As Integer

    Public Property tipe() As Integer
        Get
            Return _tipe
        End Get
        Set(ByVal value As Integer)
            _tipe = value
        End Set
    End Property

    Public Property selPL() As Member.P
        Get
            Return _selPL
        End Get
        Set(ByVal value As Member.P)
            _selPL = value
        End Set
    End Property

    Public Property selUL() As Member.U
        Get
            Return _selUL
        End Get
        Set(ByVal value As Member.U)
            _selUL = value
        End Set
    End Property

    Public Property selML() As Member.M
        Get
            Return _selML
        End Get
        Set(ByVal value As Member.M)
            _selML = value
        End Set
    End Property

    Public Property MEheight() As Single
        Get
            Return ht
        End Get
        Set(ByVal value As Single)
            ht = value
        End Set
    End Property

    Public Property Tlength() As Single
        Get
            Return totL
        End Get
        Set(ByVal value As Single)
            totL = value
        End Set
    End Property

    Private Sub beamcreate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            mainpic.Width = 1600
            mainpic.Height = 1600
            respic.Width = 1600
            respic.Height = 1600
            VScrollBar1.Maximum = (1600 - coverpic.Height) / 2
            HScrollBar1.Maximum = (1600 - coverpic.Width) / 2
            'VScrollBar1.Minimum = 100
            'HScrollBar1.Minimum = 50
            VScrollBar1.Value = VScrollBar1.Maximum / 2
            HScrollBar1.Value = HScrollBar1.Maximum / 2
            MDIMain.SFlabel.Visible = False
            MDIMain.BMlabel.Visible = False
            MDIMain.Xlabel.Visible = False
            AddHandler Me.SizeChanged, AddressOf sizemonitor
            sizemonitor()
        Catch ex As Exception
            MsgBox("Samson Mano is an Idiot")
        End Try

    End Sub

#Region "Mainpic Events"
#Region "Mainpic Paint Events"

    Private Sub mainpic_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles mainpic.Paint
        e.Graphics.ScaleTransform(Zm, Zm)
        '---Grids
        For i = 0 To 1600 Step 100
            e.Graphics.DrawLine(Pens.Beige, i, 0, i, ht)
            e.Graphics.DrawLine(Pens.Beige, 0, i, 1600, i)
            'e.Graphics.DrawString(i, Font, Brushes.Brown, i, ht * (8 / 10))
        Next
        paintBeam(e)
        paintEnds(e)
        paintSupport(e)
        paintload(e)
    End Sub

    Private Sub paintBeam(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim BeamPen As New System.Drawing.Pen(Color.Blue, 2)
        Dim dimpen As New System.Drawing.Pen(Color.CadetBlue, 1.5)
        Dim linGrBrush As System.Drawing.Drawing2D.LinearGradientBrush
        Dim StDist As Single
        Dim ScaTotLength As Single
        Dim TotLength As Single = 0

        For Each itm In mem
            TotLength = TotLength + itm.spanlength
        Next
        StDist = (1600 / 2 - coverpic.Width / 2) + 100
        ScaTotLength = coverpic.Width - 200

        e.Graphics.DrawLine(BeamPen, StDist, ht / 2, StDist + ScaTotLength, ht / 2)
        e.Graphics.DrawLine(Pens.CadetBlue, StDist, (ht / 2 + 100), StDist, (ht / 2 + 140))

        'e.Graphics.DrawRectangle(Pens.DarkBlue, StDist - 2, (ht / 2) - 2, 4, 4)
        'e.Graphics.FillRectangle(Brushes.DarkBlue, StDist - 2, (ht / 2) - 2, 4, 4)

        Dim Idist As Single = StDist
        Dim dist As Single = 0
        For Each itm In mem
            dist = dist + itm.spanlength
            e.Graphics.DrawLine(Pens.CadetBlue, StDist + (dist * (ScaTotLength / TotLength)), (ht / 2 + 100), StDist + (dist * (ScaTotLength / TotLength)), (ht / 2 + 140))
            'e.Graphics.DrawRectangle(Pens.DarkBlue, (StDist + (dist * (ScaTotLength / TotLength))) - 2, (ht / 2) - 2, 4, 4)
            'e.Graphics.FillRectangle(Brushes.DarkBlue, (StDist + (dist * (ScaTotLength / TotLength))) - 2, (ht / 2) - 2, 4, 4)

            '---Dimension Line
            Dim adcap As New System.Drawing.Drawing2D.AdjustableArrowCap(3, 5)
            linGrBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(Idist, (ht / 2 + 120)), New Point(StDist + (dist * (ScaTotLength / TotLength)), (ht / 2 + 120)), Color.CadetBlue, Color.Azure)
            linGrBrush.SetSigmaBellShape(0.5, 1)
            dimpen.Brush = linGrBrush
            dimpen.CustomStartCap = adcap
            dimpen.CustomEndCap = adcap
            e.Graphics.DrawLine(dimpen, Idist, (ht / 2 + 120), StDist + (dist * (ScaTotLength / TotLength)), (ht / 2 + 120))
            Dim R As New System.Drawing.Rectangle((Idist) * Zm, ((ht / 2) - 100) * Zm, ((StDist + (dist * (ScaTotLength / TotLength))) - Idist) * Zm, 140 * Zm)
            mem(mem.IndexOf(itm)).rect = R
            e.Graphics.DrawString(itm.spanlength, Font, Brushes.DodgerBlue, ((StDist + (dist * (ScaTotLength / TotLength))) + Idist) / 2, (ht / 2 + 115))
            linGrBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(Idist, (ht / 2 - 3)), New Point(StDist + (dist * (ScaTotLength / TotLength)), (ht / 2 + 3)), Color.LightBlue, Color.Aqua)
            linGrBrush.SetSigmaBellShape(0.5, 0.2)
            If selline = mem.IndexOf(itm) Or Tselline = mem.IndexOf(itm) Then
                e.Graphics.DrawRectangle(Pens.WhiteSmoke, (Idist), ((ht / 2) - 3), ((StDist + (dist * (ScaTotLength / TotLength))) - Idist), 6)
                e.Graphics.FillRectangle(linGrBrush, (Idist), ((ht / 2) - 3), ((StDist + (dist * (ScaTotLength / TotLength))) - Idist), 6)
                e.Graphics.DrawLine(BeamPen, (Idist), ht / 2, StDist + (dist * (ScaTotLength / TotLength)), ht / 2)
            End If
            Idist = StDist + (dist * (ScaTotLength / TotLength))
        Next
    End Sub

    Private Sub paintEnds(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim StDist As Single
        Dim ScaTotLength As Single
        Dim TotLength As Single = 0
        Dim dofpen As New System.Drawing.Pen(Color.Firebrick, 2)
        Dim linGrBrush As System.Drawing.Drawing2D.LinearGradientBrush

        For Each itm In mem
            TotLength = TotLength + itm.spanlength
        Next
        StDist = (1600 / 2 - coverpic.Width / 2) + 100
        ScaTotLength = coverpic.Width - 200

        '----Ends
        If ends = 1 Then 'Fixed-Fixed
            '---end1
            linGrBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(StDist - 15, ht / 2), New Point(StDist - 32, ht / 2), Color.Beige, Color.Firebrick)
            e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15, ht / 2 - 35, 15, 70)
            e.Graphics.FillRectangle(linGrBrush, StDist - 15, ht / 2 - 35, 15, 70)
            '---end2
            linGrBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(StDist + ScaTotLength, ht / 2), New Point(StDist + ScaTotLength + 15, ht / 2), Color.Beige, Color.Firebrick)
            e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist + ScaTotLength, ht / 2 - 35, 15, 70)
            e.Graphics.FillRectangle(linGrBrush, StDist + ScaTotLength, ht / 2 - 35, 15, 70)
        ElseIf ends = 2 Then 'Fixed-Free
            '---end1
            linGrBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(StDist - 15, ht / 2), New Point(StDist - 32, ht / 2), Color.Beige, Color.Firebrick)
            e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15, ht / 2 - 35, 15, 70)
            e.Graphics.FillRectangle(linGrBrush, StDist - 15, ht / 2 - 35, 15, 70)

        ElseIf ends = 3 Then 'Pinned-Pinned
            '---end1
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 3, StDist - 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 3, StDist + 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, StDist + 8, ht / 2 + 19, StDist - 8, ht / 2 + 19)

            e.Graphics.DrawLine(dofpen, StDist - 8, ht / 2 + 19, StDist, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 19, StDist - 8, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist + 8, ht / 2 + 19, StDist, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 19, StDist + 8, ht / 2 + 25)

            e.Graphics.DrawLine(dofpen, StDist - 8, ht / 2 + 25, StDist + 8, ht / 2 + 25)
            '---end2
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 3, (StDist + ScaTotLength) - 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 3, (StDist + ScaTotLength) + 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) + 8, ht / 2 + 19, (StDist + ScaTotLength) - 8, ht / 2 + 19)

            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) - 8, ht / 2 + 19, (StDist + ScaTotLength), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 19, (StDist + ScaTotLength) - 8, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) + 8, ht / 2 + 19, (StDist + ScaTotLength), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 19, (StDist + ScaTotLength) + 8, ht / 2 + 25)

            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) - 8, ht / 2 + 25, (StDist + ScaTotLength) + 8, ht / 2 + 25)
            'ElseIf ends = 4 Then 'Free-Free
            '----
        ElseIf ends = 5 Then 'Fixed-Pinned
            '---end1
            linGrBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(StDist - 15, ht / 2), New Point(StDist - 32, ht / 2), Color.Beige, Color.Firebrick)
            e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15, ht / 2 - 35, 15, 70)
            e.Graphics.FillRectangle(linGrBrush, StDist - 15, ht / 2 - 35, 15, 70)
            '---end2
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 3, (StDist + ScaTotLength) - 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 3, (StDist + ScaTotLength) + 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) + 8, ht / 2 + 19, (StDist + ScaTotLength) - 8, ht / 2 + 19)

            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) - 8, ht / 2 + 19, (StDist + ScaTotLength), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 19, (StDist + ScaTotLength) - 8, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) + 8, ht / 2 + 19, (StDist + ScaTotLength), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 19, (StDist + ScaTotLength) + 8, ht / 2 + 25)

            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) - 8, ht / 2 + 25, (StDist + ScaTotLength) + 8, ht / 2 + 25)
        ElseIf ends = 6 Then
            '---end1
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 3, StDist - 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 3, StDist + 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, StDist + 8, ht / 2 + 19, StDist - 8, ht / 2 + 19)

            e.Graphics.DrawLine(dofpen, StDist - 8, ht / 2 + 19, StDist, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 19, StDist - 8, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist + 8, ht / 2 + 19, StDist, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 19, StDist + 8, ht / 2 + 25)

            e.Graphics.DrawLine(dofpen, StDist - 8, ht / 2 + 25, StDist + 8, ht / 2 + 25)
        End If
    End Sub

    Private Sub paintSupport(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim dofpen As New System.Drawing.Pen(Color.Firebrick, 2)
        Dim StDist As Single
        Dim ScaTotLength As Single
        Dim TotLength As Single = 0

        For Each itm In mem
            TotLength = TotLength + itm.spanlength
        Next
        StDist = (1600 / 2 - coverpic.Width / 2) + 100
        ScaTotLength = coverpic.Width - 200

        Dim dist As Single
        For Each itm In mem
            dist = dist + itm.spanlength
            If mem.IndexOf(itm) = (mem.Count - 1) Then
                Exit Sub
            End If
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 3, (StDist + (dist * (ScaTotLength / TotLength))) - 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 3, (StDist + (dist * (ScaTotLength / TotLength))) + 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))) + 8, ht / 2 + 19, (StDist + (dist * (ScaTotLength / TotLength))) - 8, ht / 2 + 19)

            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))) - 8, ht / 2 + 19, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 19, (StDist + (dist * (ScaTotLength / TotLength))) - 8, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))) + 8, ht / 2 + 19, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 19, (StDist + (dist * (ScaTotLength / TotLength))) + 8, ht / 2 + 25)

            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))) - 8, ht / 2 + 25, (StDist + (dist * (ScaTotLength / TotLength))) + 8, ht / 2 + 25)
        Next
    End Sub

    Private Sub paintload(ByVal e As System.Windows.Forms.PaintEventArgs)
        'Dim linGrBrush As System.Drawing.Drawing2D.LinearGradientBrush
        Dim StDist As Single
        Dim ScaTotLength As Single
        Dim TotLength As Single = 0
        Dim maxload As Single = 0
        '----Finding Maximum Load
        For Each itm In mem
            TotLength = TotLength + itm.spanlength
            '----Point Load
            For Each Pitm In itm.Pload
                If Pitm.pload > maxload Then
                    maxload = Pitm.pload
                End If
            Next
            '----UVL
            For Each Uitm In itm.Uload
                If Uitm.uload1 > maxload Then
                    maxload = Uitm.uload1
                End If
                If Uitm.uload2 > maxload Then
                    maxload = Uitm.uload2
                End If
            Next
        Next
        StDist = (1600 / 2 - coverpic.Width / 2) + 100
        ScaTotLength = coverpic.Width - 200

        Dim intSTdist As Single = StDist
        Dim dist As Single = 0

        Dim loadpen As System.Drawing.Pen
        Dim adcap As System.Drawing.Drawing2D.AdjustableArrowCap

        For Each itm In mem
            dist = dist + itm.spanlength
            '----UVL
            loadpen = New System.Drawing.Pen(Color.DeepPink, 1)
            adcap = New System.Drawing.Drawing2D.AdjustableArrowCap(2, 4)
            loadpen.CustomStartCap = adcap
            Dim j As Integer = itm.Uload.Count - 1
            For i = 0 To j
                Dim toSX1 As Single
                Dim toSX2 As Single
                Dim toSY1 As Single
                Dim toSY2 As Single
                toSX1 = intSTdist + (itm.Uload(i).udist1 * (ScaTotLength / TotLength))
                toSX2 = intSTdist + (itm.Uload(i).udist2 * (ScaTotLength / TotLength))
                toSY1 = itm.Uload(i).uload1 * (100 / maxload)
                toSY2 = itm.Uload(i).uload2 * (100 / maxload)

                If (itm.Uload(i).udist1 = selUL.udist1) And (itm.Uload(i).uload1 = selUL.uload1) And (itm.Uload(i).udist2 = selUL.udist2) And (itm.Uload(i).uload2 = selUL.uload2) Then
                    loadpen = New System.Drawing.Pen(Color.LightPink, 2)

                    e.Graphics.DrawLine(loadpen, toSX1, ht / 2 - 5, toSX1, (ht / 2) - toSY1)
                    e.Graphics.DrawString(itm.Uload(i).uload1, Font, loadpen.Brush, toSX1, ((ht / 2) - 20) - toSY1)
                End If
                loadpen = New System.Drawing.Pen(Color.DeepPink, 1)
                loadpen.CustomStartCap = adcap
                e.Graphics.DrawLine(loadpen, toSX1, (ht / 2), toSX1, (ht / 2) - toSY1)
                e.Graphics.DrawString(itm.Uload(i).uload1, Font, loadpen.Brush, toSX1, ((ht / 2) - 20) - toSY1)

                If toSY1 >= toSY2 Then
                    Dim temp As Member.U
                    temp.uload1 = itm.Uload(i).uload1
                    temp.uload2 = itm.Uload(i).uload2
                    temp.udist1 = itm.Uload(i).udist1
                    temp.udist2 = itm.Uload(i).udist2
                    Dim R As New System.Drawing.Rectangle((toSX1) * Zm, ((ht / 2) - toSY1) * Zm, (toSX2 - toSX1) * Zm, toSY1 * Zm)
                    temp.rect = R
                    Dim ind As Integer = i
                    mem(mem.IndexOf(itm)).Uload.Insert(ind, temp)
                    mem(mem.IndexOf(itm)).Uload.RemoveAt(ind + 1)
                Else
                    Dim temp As Member.U
                    temp.uload1 = itm.Uload(i).uload1
                    temp.uload2 = itm.Uload(i).uload2
                    temp.udist1 = itm.Uload(i).udist1
                    temp.udist2 = itm.Uload(i).udist2
                    Dim R As New System.Drawing.Rectangle((toSX1) * Zm, ((ht / 2) - toSY2) * Zm, (toSX2 - toSX1) * Zm, toSY2 * Zm)
                    temp.rect = R
                    Dim ind As Integer = i
                    mem(mem.IndexOf(itm)).Uload.Insert(ind, temp)
                    mem(mem.IndexOf(itm)).Uload.RemoveAt(ind + 1)
                End If

                If (itm.Uload(i).udist1 = selUL.udist1) And (itm.Uload(i).uload1 = selUL.uload1) And (itm.Uload(i).udist2 = selUL.udist2) And (itm.Uload(i).uload2 = selUL.uload2) Then
                    If toSY2 = toSY1 Then
                        For k = toSX1 To toSX2 Step 10
                            loadpen = New System.Drawing.Pen(Color.LightPink, 3)
                            e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY1))
                            loadpen = New System.Drawing.Pen(Color.DeepPink, 1)
                            loadpen.CustomStartCap = adcap
                            e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY1))
                        Next
                    ElseIf toSY2 > toSY1 Then
                        For k = toSX1 To toSX2 Step 10
                            loadpen = New System.Drawing.Pen(Color.LightPink, 3)
                            e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY1 + (((toSY2 - toSY1) / (toSX2 - toSX1)) * (k - toSX1))))
                            loadpen = New System.Drawing.Pen(Color.DeepPink, 1)
                            loadpen.CustomStartCap = adcap
                            e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY1 + (((toSY2 - toSY1) / (toSX2 - toSX1)) * (k - toSX1))))
                        Next
                    Else
                        For k = toSX1 To toSX2 Step 10
                            loadpen = New System.Drawing.Pen(Color.LightPink, 3)
                            e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY2 + (((toSY1 - toSY2) / (toSX2 - toSX1)) * (toSX2 - k))))
                            loadpen = New System.Drawing.Pen(Color.DeepPink, 1)
                            loadpen.CustomStartCap = adcap
                            e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY2 + (((toSY1 - toSY2) / (toSX2 - toSX1)) * (toSX2 - k))))
                        Next
                    End If
                Else
                    loadpen = New System.Drawing.Pen(Color.DeepPink, 1)
                    loadpen.CustomStartCap = adcap
                    If toSY2 = toSY1 Then
                        For k = toSX1 To toSX2 Step 10
                            e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY1))
                        Next
                    ElseIf toSY2 > toSY1 Then
                        For k = toSX1 To toSX2 Step 10
                            e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY1 + (((toSY2 - toSY1) / (toSX2 - toSX1)) * (k - toSX1))))
                        Next
                    Else
                        For k = toSX1 To toSX2 Step 10
                            e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY2 + (((toSY1 - toSY2) / (toSX2 - toSX1)) * (toSX2 - k))))
                        Next
                    End If
                End If


                If (itm.Uload(i).udist1 = selUL.udist1) And (itm.Uload(i).uload1 = selUL.uload1) And (itm.Uload(i).udist2 = selUL.udist2) And (itm.Uload(i).uload2 = selUL.uload2) Then
                    loadpen = New System.Drawing.Pen(Color.LightPink, 2)
                    e.Graphics.DrawLine(loadpen, toSX2, ht / 2 - 5, toSX2, (ht / 2) - toSY2)
                End If
                loadpen = New System.Drawing.Pen(Color.DeepPink, 1)
                loadpen.CustomStartCap = adcap
                e.Graphics.DrawLine(loadpen, toSX2, (ht / 2), toSX2, (ht / 2) - toSY2)
                e.Graphics.DrawString(itm.Uload(i).uload2, Font, loadpen.Brush, toSX2, ((ht / 2) - 20) - toSY2)
            Next
            '----Point Load
            j = itm.Pload.Count - 1
            For i = 0 To j
                Dim toSX As Single
                Dim toSY As Single
                toSX = intSTdist + (itm.Pload(i).pdist * (ScaTotLength / TotLength))
                toSY = itm.Pload(i).pload * (100 / maxload)

                Dim temp As Member.P
                temp.pload = itm.Pload(i).pload
                temp.pdist = itm.Pload(i).pdist
                Dim R As New System.Drawing.Rectangle((toSX - 4) * Zm, ((ht / 2) - toSY) * Zm, (8) * Zm, toSY * Zm)
                temp.rect = R
                Dim ind As Integer = i
                mem(mem.IndexOf(itm)).Pload.Insert(ind, temp)
                mem(mem.IndexOf(itm)).Pload.RemoveAt(ind + 1)

                If (itm.Pload(i).pdist = selPL.pdist) And (itm.Pload(i).pload = selPL.pload) Then
                    loadpen = New System.Drawing.Pen(Color.LightGreen, 4)

                    e.Graphics.DrawLine(loadpen, toSX, ht / 2 - 5, toSX, (ht / 2) - toSY)
                    e.Graphics.DrawString(itm.Pload(i).pload, Font, loadpen.Brush, toSX, ((ht / 2) - 20) - toSY)
                End If
                loadpen = New System.Drawing.Pen(Color.Green, 2)
                adcap = New System.Drawing.Drawing2D.AdjustableArrowCap(3, 5)
                loadpen.CustomStartCap = adcap

                e.Graphics.DrawLine(loadpen, toSX, ht / 2, toSX, (ht / 2) - toSY)
                e.Graphics.DrawString(itm.Pload(i).pload, Font, loadpen.Brush, toSX, ((ht / 2) - 20) - toSY)

            Next
            '----Moment
            loadpen = New System.Drawing.Pen(Color.Orange, 2)
            adcap = New System.Drawing.Drawing2D.AdjustableArrowCap(3, 5)
            j = itm.Mload.Count - 1
            For i = 0 To j
                Dim toSX As Single
                toSX = intSTdist + (itm.Mload(i).mdist * (ScaTotLength / TotLength))

                If (itm.Mload(i).mdist = selML.mdist) And (itm.Mload(i).mload = selML.mload) Then
                    loadpen = New System.Drawing.Pen(Color.Yellow, 4)
                    e.Graphics.DrawArc(loadpen, toSX - 30, ((ht / 2) - 30), 60, 60, 270, 180)
                End If
                loadpen = New System.Drawing.Pen(Color.Orange, 2)
                If itm.Mload(i).mload > 0 Then
                    loadpen.CustomStartCap = adcap
                Else
                    loadpen.CustomEndCap = adcap
                End If

                Dim temp As Member.M
                temp.mload = itm.Mload(i).mload
                temp.mdist = itm.Mload(i).mdist
                Dim R As New System.Drawing.Rectangle((toSX) * Zm, ((ht / 2) - 30) * Zm, (30) * Zm, (60) * Zm)
                temp.rect = R
                Dim ind As Integer = i
                mem(mem.IndexOf(itm)).Mload.Insert(ind, temp)
                mem(mem.IndexOf(itm)).Mload.RemoveAt(ind + 1)

                e.Graphics.DrawArc(loadpen, toSX - 30, ((ht / 2) - 30), 60, 60, 270, 180)
                e.Graphics.DrawString(Math.Abs(itm.Mload(i).mload), Font, loadpen.Brush, toSX, ((ht / 2) - 50))
            Next
            intSTdist = StDist + (dist * (ScaTotLength / TotLength))
        Next
        '--Check For Load Selection 
        'For Each itm In mem
        '    For Each Pitm In itm.Pload
        '        e.Graphics.DrawRectangle(Pens.DarkRed, Pitm.rect)
        '    Next
        '    For Each uitm In itm.Uload
        '        e.Graphics.DrawRectangle(Pens.DarkRed, uitm.rect)
        '    Next
        '    For Each mitm In itm.Mload
        '        e.Graphics.DrawRectangle(Pens.DarkRed, mitm.rect)
        '    Next
        'Next
    End Sub

#End Region

#Region "Mainpic mouse click events"
    Private Sub mainpic_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mainpic.MouseClick
        '--Null selected load
        Dim ptm As New Member.P
        Dim utm As New Member.U
        Dim mtm As New Member.M
        selPL = ptm
        selUL = utm
        selML = mtm
        '---Line Select
        If e.Y > (ht / 2 - 5) * Zm Then
            Lselline = -1
            If e.Button = Windows.Forms.MouseButtons.Left Then
                For Each itm In mem
                    If itm.rect.Contains(e.X, e.Y) Then
                        selline = (mem.IndexOf(itm))
                        toolstrip1Mod()
                        mainpic.Refresh()
                        Exit Sub
                    End If
                Next
            ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
                For Each itm In mem
                    If itm.rect.Contains(e.X, e.Y) Then
                        selline = (mem.IndexOf(itm))
                    End If
                Next
                If selline = -1 Then
                    AddMemberToolStripMenuItem1.Enabled = True
                    EditEndsToolStripMenuItem.Enabled = True
                    EditMemebrToolStripMenuItem.Enabled = False
                    RemoveMemberToolStripMenuItem.Enabled = False
                    AddLoadToolStripMenuItem1.Enabled = False
                    RemoveLoadsToolStripMenuItem.Enabled = False
                Else
                    AddMemberToolStripMenuItem1.Enabled = False
                    EditEndsToolStripMenuItem.Enabled = False
                    EditMemebrToolStripMenuItem.Enabled = True
                    RemoveMemberToolStripMenuItem.Enabled = True
                    AddLoadToolStripMenuItem1.Enabled = True
                    RemoveLoadsToolStripMenuItem.Enabled = True
                End If
                ContextMenuStrip1.Show(mainpic.PointToScreen(e.Location))
                If selline <> -1 Then
                    mainpic.Refresh()
                    Exit Sub
                End If
            End If
        Else
            selline = -1
            Tselline = -1
            '--Load Selection Procedure
            If e.Button = Windows.Forms.MouseButtons.Left Then
                For Each itm In mem
                    If itm.rect.Contains(e.X, e.Y) Then
                        Lselline = mem.IndexOf(itm)
                        For Each pitm In itm.Pload
                            If pitm.rect.Contains(e.X, e.Y) Then
                                Dim uutm As New Member.U
                                Dim mmtm As New Member.M
                                selUL = uutm
                                selML = mmtm
                                selPL = pitm
                                tipe = 1
                                toolstrip1Mod()
                                mainpic.Refresh()
                                Exit Sub
                            End If
                        Next
                        For Each mitm In itm.Mload
                            If mitm.rect.Contains(e.X, e.Y) Then
                                Dim pptm As New Member.P
                                Dim uutm As New Member.U
                                selPL = pptm
                                selUL = uutm
                                selML = mitm
                                tipe = 3
                                toolstrip1Mod()
                                mainpic.Refresh()
                                Exit Sub
                            End If
                        Next
                        For Each uitm In itm.Uload
                            If uitm.rect.Contains(e.X, e.Y) Then
                                Dim pptm As New Member.P
                                Dim mmtm As New Member.M
                                selPL = pptm
                                selML = mmtm
                                selUL = uitm
                                tipe = 2
                                toolstrip1Mod()
                                mainpic.Refresh()
                                Exit Sub
                            End If
                        Next
                        selline = -1
                        Lselline = -1
                    End If
                Next
            ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
                For Each itm In mem
                    If itm.rect.Contains(e.X, e.Y) Then
                        Lselline = mem.IndexOf(itm)
                        For Each pitm In itm.Pload
                            If pitm.rect.Contains(e.X, e.Y) Then
                                selPL = pitm
                                tipe = 1
                                GoTo r1
                            End If
                        Next
                        For Each uitm In itm.Uload
                            If uitm.rect.Contains(e.X, e.Y) Then
                                selUL = uitm
                                tipe = 2
                                GoTo r1
                            End If
                        Next
                        For Each mitm In itm.Mload
                            If mitm.rect.Contains(e.X, e.Y) Then
                                selML = mitm
                                tipe = 3
                                GoTo r1
                            End If
                        Next
                        Lselline = -1
                        selline = -1
                        Continue For
r1:
                        mainpic.Refresh()
                        AddMemberToolStripMenuItem.Enabled = False
                        EditEndsToolStripMenuItem1.Enabled = False
                        RemoveLoadToolStripMenuItem.Enabled = True
                        EditLoadToolStripMenuItem.Enabled = True
                        ContextMenuStrip2.Show(mainpic.PointToScreen(e.Location))
                        Exit Sub
                    End If
                Next
                mainpic.Refresh()
                AddMemberToolStripMenuItem.Enabled = True
                EditEndsToolStripMenuItem1.Enabled = True
                RemoveLoadToolStripMenuItem.Enabled = False
                EditLoadToolStripMenuItem.Enabled = False
                ContextMenuStrip2.Show(mainpic.PointToScreen(e.Location))
            End If
        End If
        selline = -1
        toolstrip1Mod()
        mainpic.Refresh()
    End Sub

    Private Sub mainpic_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mainpic.MouseDown
        If e.Y > (ht / 2 - 5) * Zm Then
            Tselline = -1
            selline = -1
            If e.Button = Windows.Forms.MouseButtons.Left Then
                For Each itm In mem
                    If itm.rect.Contains(e.X, e.Y) Then
                        Tselline = (mem.IndexOf(itm))
                        mainpic.Refresh()
                        Exit Sub
                    End If
                Next
                Tselline = -1
            End If
        End If

    End Sub

    Private Sub mainpic_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mainpic.MouseUp
        Try
            If e.Y > (ht / 2 - 5) * Zm Then
                If e.Button = Windows.Forms.MouseButtons.Left Then
                    For Each itm In mem
                        If itm.rect.Contains(e.X, e.Y) Then
                            If selline = Tselline Or Tselline = -1 Then
                                Exit Sub
                            End If
                            If Tselline > selline Then
                                mem.Insert(selline, mem(Tselline))
                                mem.RemoveAt(Tselline + 1)
                            Else
                                mem.Insert(Tselline, mem(selline))
                                mem.RemoveAt(selline + 1)
                            End If
                            Tselline = -1
                            mainpic.Refresh()
                            Exit Sub
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MsgBox("Error")
        End Try

    End Sub

    Public Sub toolstrip1Mod()
        If selline = -1 And Lselline = -1 Then
            MDIMain.ToolStripButton1.Enabled = True
            MDIMain.AddMemberToolStripMenuItem.Enabled = True
            MDIMain.ToolStripButton2.Enabled = True
            MDIMain.BeamEndsToolStripMenuItem.Enabled = True
            MDIMain.ToolStripButton3.Enabled = False
            MDIMain.editMemberToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton4.Enabled = False
            MDIMain.RemoveMemberToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton5.Enabled = False
            MDIMain.AddLoadToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton6.Enabled = False
            MDIMain.RemoveLoadsToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton7.Enabled = False
            MDIMain.ModifyLoadToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton8.Enabled = False
            MDIMain.RemoveLoadToolStripMenuItem.Enabled = False
        ElseIf Lselline = -1 Then
            MDIMain.ToolStripButton1.Enabled = False
            MDIMain.AddMemberToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton2.Enabled = False
            MDIMain.BeamEndsToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton3.Enabled = True
            MDIMain.editMemberToolStripMenuItem.Enabled = True
            MDIMain.ToolStripButton4.Enabled = True
            MDIMain.RemoveMemberToolStripMenuItem.Enabled = True
            MDIMain.ToolStripButton5.Enabled = True
            MDIMain.AddLoadToolStripMenuItem.Enabled = True
            MDIMain.ToolStripButton6.Enabled = True
            MDIMain.RemoveLoadsToolStripMenuItem.Enabled = True
            MDIMain.ToolStripButton7.Enabled = False
            MDIMain.ModifyLoadToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton8.Enabled = False
            MDIMain.RemoveLoadToolStripMenuItem.Enabled = False
        ElseIf Lselline <> -1 Then
            MDIMain.ToolStripButton1.Enabled = False
            MDIMain.AddMemberToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton2.Enabled = False
            MDIMain.BeamEndsToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton3.Enabled = False
            MDIMain.editMemberToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton4.Enabled = False
            MDIMain.RemoveMemberToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton5.Enabled = False
            MDIMain.AddLoadToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton6.Enabled = False
            MDIMain.RemoveLoadsToolStripMenuItem.Enabled = False
            MDIMain.ToolStripButton7.Enabled = True
            MDIMain.ModifyLoadToolStripMenuItem.Enabled = True
            MDIMain.ToolStripButton8.Enabled = True
            MDIMain.RemoveLoadToolStripMenuItem.Enabled = True
        End If
    End Sub
#End Region

#Region "Mainpic Zoom events"
    Private Sub mainpic_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles mainpic.MouseEnter
        mainpic.Focus()
    End Sub

    Private Sub mainpic_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles mainpic.MouseLeave
        MDIMain.ToolStrip1.Focus()
    End Sub

    Private Sub HScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar1.Scroll
        mainpic.Left = -(HScrollBar1.Value)
        respic.Left = -(HScrollBar1.Value)
    End Sub

    Private Sub VScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles VScrollBar1.Scroll
        mainpic.Top = -(VScrollBar1.Value)
        respic.Top = -(VScrollBar1.Value)
    End Sub

    Private Sub mainpic_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mainpic.MouseWheel
        mainpic.SuspendLayout()
        Dim xw, yw As Single
        xw = e.X / Zm
        yw = e.Y / Zm
        If e.Delta > 0 Then
            If Zm < 10 Then
                Zm = Zm + 0.25
            End If
        Else
            If Zm > 1 Then
                Zm = Zm - 0.25
            End If
        End If
        mainpic.Refresh()
        If e.X <> xw * Zm Then
            mainpic.Width = 1600 * Zm
            mainpic.Height = ht * Zm
            HScrollBar1.Maximum = (1600 * Zm) - coverpic.Width
            VScrollBar1.Maximum = (1600 * Zm) - coverpic.Height

            xw = -(mainpic.Left - ((xw * Zm) - e.X))
            yw = -(mainpic.Top - ((yw * Zm) - e.Y))

            If xw <= HScrollBar1.Minimum Then
                mainpic.Left = -1
                HScrollBar1.Value = 50
            ElseIf xw >= HScrollBar1.Maximum Then
                mainpic.Left = -HScrollBar1.Maximum
                HScrollBar1.Value = HScrollBar1.Maximum
            Else
                mainpic.Left = -xw
                HScrollBar1.Value = xw
            End If
            mainpic.Refresh()
            If yw <= VScrollBar1.Minimum Then
                mainpic.Top = -1
                VScrollBar1.Value = 100
            ElseIf yw >= VScrollBar1.Maximum Then
                mainpic.Top = -VScrollBar1.Maximum
                VScrollBar1.Value = VScrollBar1.Maximum
            Else
                mainpic.Top = -yw
                VScrollBar1.Value = yw
            End If

            ' mainpic.Top = -(((ht * Zm) - ht) / 2)
            mainpic.Invalidate()

            mainpic.ResumeLayout()
            mainpic.Refresh()
            MDIMain.ToolStripLabel1.Text = (Zm * 100) & "%"
            MDIMain.ToolStripLabel2.Text = (Zm * 100) & "%"
        End If
    End Sub
#End Region

#Region "Context menu strip1 Events"

    Private Sub AddMemberToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddMemberToolStripMenuItem1.Click
        Dim a As New addmember
        a.Text = "Add Member"
        a.ShowDialog()
    End Sub

    Private Sub EditEndsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditEndsToolStripMenuItem.Click
        Dim a As New Ends_Editor
        a.ShowDialog()
    End Sub

    Private Sub EditMemebrToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditMemebrToolStripMenuItem.Click
        Dim a As New addmember
        a.TextBox1.Text = mem(selline).spanlength
        a.TextBox2.Text = mem(selline).Emodulus
        a.TextBox3.Text = mem(selline).Inertia
        a.TextBox6.Text = mem(selline).g
        a.Text = "Modify Member"
        a.ShowDialog()
    End Sub

    Private Sub RemoveMemberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveMemberToolStripMenuItem.Click
        If ends = 4 And mem.Count = 3 Then
            Exit Sub
        ElseIf ends = 6 And mem.Count = 2 Then
            Exit Sub
        End If
        If mem.Count <> 1 Then
            mem.RemoveAt(selline)
            selline = -1
            toolstrip1Mod()
            mainpic.Refresh()
        End If
    End Sub

    Private Sub AddLoadToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddLoadToolStripMenuItem1.Click
        Dim a As New LoadWindow(mem(selline).spanlength, selline)
        a.ShowDialog()
    End Sub

    Private Sub RemoveLoadsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveLoadsToolStripMenuItem.Click
        '--ALL Loads Are Removed
        mem(selline).Pload.Clear()
        mem(selline).Uload.Clear()
        mem(selline).Mload.Clear()

        mainpic.Refresh()
    End Sub

    Private Sub EditLoadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditLoadToolStripMenuItem.Click
        If tipe = 1 Then
            Dim a As New LoadWindow(mem(Lselline).spanlength, Lselline, selPL)
            a.ShowDialog()
        ElseIf tipe = 2 Then
            Dim a As New LoadWindow(mem(Lselline).spanlength, Lselline, selUL)
            a.ShowDialog()
        ElseIf tipe = 3 Then
            Dim a As New LoadWindow(mem(Lselline).spanlength, Lselline, selML)
            a.ShowDialog()
        End If
        mainpic.Refresh()
    End Sub

    Private Sub RemoveLoadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveLoadToolStripMenuItem.Click
        '--Single Load is removed
        If tipe = 1 Then
            mem(Lselline).Pload.Remove(selPL)
        ElseIf tipe = 2 Then
            mem(Lselline).Uload.Remove(selUL)
        ElseIf tipe = 3 Then
            mem(Lselline).Mload.Remove(selML)
        End If
        '--Null selected load
        Dim ptm As New Member.P
        Dim utm As New Member.U
        Dim mtm As New Member.M
        selPL = ptm
        selUL = utm
        selML = mtm
        selline = -1
        Lselline = -1
        toolstrip1Mod()
        mainpic.Refresh()
    End Sub

    Private Sub AddMemberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddMemberToolStripMenuItem.Click
        Dim a As New addmember
        a.Text = "Add Member"
        a.ShowDialog()
    End Sub

    Private Sub EditEndsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditEndsToolStripMenuItem1.Click
        Dim a As New Ends_Editor
        a.ShowDialog()
    End Sub

#End Region
#End Region

#Region "Respic Events"
#Region "Respic Paint Events"
    Private Sub respic_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles respic.Paint
        e.Graphics.ScaleTransform(Zm, Zm)
        '---Grids
        For i = 0 To 1600 Step 100
            e.Graphics.DrawLine(Pens.Beige, i, 0, i, ht)
            e.Graphics.DrawLine(Pens.Beige, 0, i, 1600, i)
            'e.Graphics.DrawString(i, Font, Brushes.Brown, i, ht * (8 / 10))
        Next
        rpaintBeam(e)
        rpaintEnds(e)
        rpaintSupport(e)
        rpaintload(e)

        If MDIMain.ShearForceDiagramToolStripMenuItem.Checked = True Then
            rShearPaint(e)
        ElseIf MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = True Then
            rMomentPaint(e)
        ElseIf MDIMain.DeflectionToolStripMenuItem.Checked = True Then
            rDeflectionPaint(e)
        ElseIf MDIMain.SlopeToolStripMenuItem.Checked = True Then
            rSlopePaint(e)
        End If
    End Sub

    Private Sub rpaintBeam(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim BeamPen As New System.Drawing.Pen(Color.LightGray, 2)
        Dim dimpen As New System.Drawing.Pen(Color.LightGray, 1.5)
        Dim linGrBrush As System.Drawing.Drawing2D.LinearGradientBrush
        Dim StDist As Single
        Dim ScaTotLength As Single
        Dim TotLength As Single = 0

        For Each itm In mem
            TotLength = TotLength + itm.spanlength
        Next
        StDist = (1600 / 2 - coverpic.Width / 2) + 100
        ScaTotLength = coverpic.Width - 200

        e.Graphics.DrawLine(BeamPen, StDist, ht / 2, StDist + ScaTotLength, ht / 2)
        e.Graphics.DrawLine(Pens.LightGray, StDist, (ht / 2 + 100), StDist, (ht / 2 + 140))
        Dim Idist As Single = StDist
        Dim dist As Single = 0
        For Each itm In mem
            dist = dist + itm.spanlength
            '---Dimension Line
            Dim adcap As New System.Drawing.Drawing2D.AdjustableArrowCap(3, 5)
            linGrBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(Idist, (ht / 2 + 120)), New Point(StDist + (dist * (ScaTotLength / TotLength)), (ht / 2 + 120)), Color.LightGray, Color.WhiteSmoke)
            linGrBrush.SetSigmaBellShape(0.5, 1)
            dimpen.Brush = linGrBrush
            dimpen.CustomStartCap = adcap
            dimpen.CustomEndCap = adcap
            e.Graphics.DrawLine(dimpen, Idist, (ht / 2 + 120), StDist + (dist * (ScaTotLength / TotLength)), (ht / 2 + 120))
            e.Graphics.DrawString(itm.spanlength, Font, Brushes.LightGray, ((StDist + (dist * (ScaTotLength / TotLength))) + Idist) / 2, (ht / 2 + 115))
            Idist = StDist + (dist * (ScaTotLength / TotLength))
        Next
    End Sub

    Private Sub rpaintEnds(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim StDist As Single
        Dim ScaTotLength As Single
        Dim TotLength As Single = 0
        Dim dofpen As New System.Drawing.Pen(Color.LightGray, 2)
        Dim linGrBrush As System.Drawing.Drawing2D.LinearGradientBrush

        For Each itm In mem
            TotLength = TotLength + itm.spanlength
        Next
        StDist = (1600 / 2 - coverpic.Width / 2) + 100
        ScaTotLength = coverpic.Width - 200

        '----Ends
        If ends = 1 Then 'Fixed-Fixed
            '---end1
            linGrBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(StDist - 15, ht / 2), New Point(StDist - 32, ht / 2), Color.WhiteSmoke, Color.LightGray)
            e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15, ht / 2 - 35, 15, 70)
            e.Graphics.FillRectangle(linGrBrush, StDist - 15, ht / 2 - 35, 15, 70)
            '---end2
            linGrBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(StDist + ScaTotLength, ht / 2), New Point(StDist + ScaTotLength + 15, ht / 2), Color.WhiteSmoke, Color.LightGray)
            e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist + ScaTotLength, ht / 2 - 35, 15, 70)
            e.Graphics.FillRectangle(linGrBrush, StDist + ScaTotLength, ht / 2 - 35, 15, 70)
        ElseIf ends = 2 Then 'Fixed-Free
            '---end1
            linGrBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(StDist - 15, ht / 2), New Point(StDist - 32, ht / 2), Color.WhiteSmoke, Color.LightGray)
            e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15, ht / 2 - 35, 15, 70)
            e.Graphics.FillRectangle(linGrBrush, StDist - 15, ht / 2 - 35, 15, 70)

        ElseIf ends = 3 Then 'Pinned-Pinned
            '---end1
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 3, StDist - 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 3, StDist + 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, StDist + 8, ht / 2 + 19, StDist - 8, ht / 2 + 19)

            e.Graphics.DrawLine(dofpen, StDist - 8, ht / 2 + 19, StDist, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 19, StDist - 8, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist + 8, ht / 2 + 19, StDist, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 19, StDist + 8, ht / 2 + 25)

            e.Graphics.DrawLine(dofpen, StDist - 8, ht / 2 + 25, StDist + 8, ht / 2 + 25)
            '---end2
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 3, (StDist + ScaTotLength) - 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 3, (StDist + ScaTotLength) + 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) + 8, ht / 2 + 19, (StDist + ScaTotLength) - 8, ht / 2 + 19)

            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) - 8, ht / 2 + 19, (StDist + ScaTotLength), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 19, (StDist + ScaTotLength) - 8, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) + 8, ht / 2 + 19, (StDist + ScaTotLength), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 19, (StDist + ScaTotLength) + 8, ht / 2 + 25)

            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) - 8, ht / 2 + 25, (StDist + ScaTotLength) + 8, ht / 2 + 25)
            'ElseIf ends = 4 Then 'Free-Free
            '----
        ElseIf ends = 5 Then 'Fixed-Pinned
            '---end1
            linGrBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(StDist - 15, ht / 2), New Point(StDist - 32, ht / 2), Color.WhiteSmoke, Color.LightGray)
            e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15, ht / 2 - 35, 15, 70)
            e.Graphics.FillRectangle(linGrBrush, StDist - 15, ht / 2 - 35, 15, 70)
            '---end2
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 3, (StDist + ScaTotLength) - 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 3, (StDist + ScaTotLength) + 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) + 8, ht / 2 + 19, (StDist + ScaTotLength) - 8, ht / 2 + 19)

            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) - 8, ht / 2 + 19, (StDist + ScaTotLength), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 19, (StDist + ScaTotLength) - 8, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) + 8, ht / 2 + 19, (StDist + ScaTotLength), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength), ht / 2 + 19, (StDist + ScaTotLength) + 8, ht / 2 + 25)

            e.Graphics.DrawLine(dofpen, (StDist + ScaTotLength) - 8, ht / 2 + 25, (StDist + ScaTotLength) + 8, ht / 2 + 25)
        ElseIf ends = 6 Then
            '---end1
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 3, StDist - 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 3, StDist + 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, StDist + 8, ht / 2 + 19, StDist - 8, ht / 2 + 19)

            e.Graphics.DrawLine(dofpen, StDist - 8, ht / 2 + 19, StDist, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 19, StDist - 8, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist + 8, ht / 2 + 19, StDist, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, StDist, ht / 2 + 19, StDist + 8, ht / 2 + 25)

            e.Graphics.DrawLine(dofpen, StDist - 8, ht / 2 + 25, StDist + 8, ht / 2 + 25)
        End If
    End Sub

    Private Sub rpaintSupport(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim dofpen As New System.Drawing.Pen(Color.LightGray, 2)
        Dim StDist As Single
        Dim ScaTotLength As Single
        Dim TotLength As Single = 0

        For Each itm In mem
            TotLength = TotLength + itm.spanlength
        Next
        StDist = (1600 / 2 - coverpic.Width / 2) + 100
        ScaTotLength = coverpic.Width - 200

        Dim dist As Single
        For Each itm In mem
            dist = dist + itm.spanlength
            If mem.IndexOf(itm) = (mem.Count - 1) Then
                Exit Sub
            End If
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 3, (StDist + (dist * (ScaTotLength / TotLength))) - 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 3, (StDist + (dist * (ScaTotLength / TotLength))) + 8, ht / 2 + 19)
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))) + 8, ht / 2 + 19, (StDist + (dist * (ScaTotLength / TotLength))) - 8, ht / 2 + 19)

            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))) - 8, ht / 2 + 19, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 19, (StDist + (dist * (ScaTotLength / TotLength))) - 8, ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))) + 8, ht / 2 + 19, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 25)
            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))), ht / 2 + 19, (StDist + (dist * (ScaTotLength / TotLength))) + 8, ht / 2 + 25)

            e.Graphics.DrawLine(dofpen, (StDist + (dist * (ScaTotLength / TotLength))) - 8, ht / 2 + 25, (StDist + (dist * (ScaTotLength / TotLength))) + 8, ht / 2 + 25)
        Next
    End Sub

    Private Sub rpaintload(ByVal e As System.Windows.Forms.PaintEventArgs)
        'Dim linGrBrush As System.Drawing.Drawing2D.LinearGradientBrush
        Dim StDist As Single
        Dim ScaTotLength As Single
        Dim TotLength As Single = 0
        Dim maxload As Single = 0
        '----Finding Maximum Load
        For Each itm In mem
            TotLength = TotLength + itm.spanlength
            '----Point Load
            For Each Pitm In itm.Pload
                If Pitm.pload > maxload Then
                    maxload = Pitm.pload
                End If
            Next
            '----UVL
            For Each Uitm In itm.Uload
                If Uitm.uload1 > maxload Then
                    maxload = Uitm.uload1
                End If
                If Uitm.uload2 > maxload Then
                    maxload = Uitm.uload2
                End If
            Next
        Next
        StDist = (1600 / 2 - coverpic.Width / 2) + 100
        ScaTotLength = coverpic.Width - 200

        Dim intSTdist As Single = StDist
        Dim dist As Single = 0

        Dim loadpen As System.Drawing.Pen
        Dim adcap As System.Drawing.Drawing2D.AdjustableArrowCap

        For Each itm In mem
            dist = dist + itm.spanlength
            '----UVL
            loadpen = New System.Drawing.Pen(Color.LightGray, 1)
            adcap = New System.Drawing.Drawing2D.AdjustableArrowCap(2, 4)
            loadpen.CustomStartCap = adcap
            Dim j As Integer = itm.Uload.Count - 1
            For i = 0 To j
                Dim toSX1 As Single
                Dim toSX2 As Single
                Dim toSY1 As Single
                Dim toSY2 As Single
                toSX1 = intSTdist + (itm.Uload(i).udist1 * (ScaTotLength / TotLength))
                toSX2 = intSTdist + (itm.Uload(i).udist2 * (ScaTotLength / TotLength))
                toSY1 = itm.Uload(i).uload1 * (100 / maxload)
                toSY2 = itm.Uload(i).uload2 * (100 / maxload)

                e.Graphics.DrawLine(loadpen, toSX1, (ht / 2), toSX1, (ht / 2) - toSY1)
                e.Graphics.DrawString(itm.Uload(i).uload1, Font, loadpen.Brush, toSX1, ((ht / 2) - 20) - toSY1)
                If toSY2 = toSY1 Then
                    For k = toSX1 To toSX2 Step 10
                        e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY1))
                    Next
                ElseIf toSY2 > toSY1 Then
                    For k = toSX1 To toSX2 Step 10
                        e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY1 + (((toSY2 - toSY1) / (toSX2 - toSX1)) * (k - toSX1))))
                    Next
                Else
                    For k = toSX1 To toSX2 Step 10
                        e.Graphics.DrawLine(loadpen, k, (ht / 2), k, (ht / 2) - (toSY2 + (((toSY1 - toSY2) / (toSX2 - toSX1)) * (toSX2 - k))))
                    Next
                End If
                e.Graphics.DrawLine(loadpen, toSX2, (ht / 2), toSX2, (ht / 2) - toSY2)
                e.Graphics.DrawString(itm.Uload(i).uload2, Font, loadpen.Brush, toSX2, ((ht / 2) - 20) - toSY2)
            Next
            '----Point Load
            j = itm.Pload.Count - 1
            loadpen = New System.Drawing.Pen(Color.LightGray, 2)
            adcap = New System.Drawing.Drawing2D.AdjustableArrowCap(3, 5)
            loadpen.CustomStartCap = adcap
            For i = 0 To j
                Dim toSX As Single
                Dim toSY As Single
                toSX = intSTdist + (itm.Pload(i).pdist * (ScaTotLength / TotLength))
                toSY = itm.Pload(i).pload * (100 / maxload)

                e.Graphics.DrawLine(loadpen, toSX, ht / 2, toSX, (ht / 2) - toSY)
                e.Graphics.DrawString(itm.Pload(i).pload, Font, loadpen.Brush, toSX, ((ht / 2) - 20) - toSY)

            Next
            '----Moment
            loadpen = New System.Drawing.Pen(Color.LightGray, 2)
            adcap = New System.Drawing.Drawing2D.AdjustableArrowCap(3, 5)
            j = itm.Mload.Count - 1
            For i = 0 To j
                Dim toSX As Single
                toSX = intSTdist + (itm.Mload(i).mdist * (ScaTotLength / TotLength))
                If itm.Mload(i).mload > 0 Then
                    loadpen.CustomStartCap = adcap
                Else
                    loadpen.CustomEndCap = adcap
                End If
                e.Graphics.DrawArc(loadpen, toSX - 30, ((ht / 2) - 30), 60, 60, 270, 180)
                e.Graphics.DrawString(Math.Abs(itm.Mload(i).mload), Font, loadpen.Brush, toSX, ((ht / 2) - 50))
            Next
            intSTdist = StDist + (dist * (ScaTotLength / TotLength))
        Next
        '--Check For Load Selection 
        'For Each itm In mem
        '    For Each Pitm In itm.Pload
        '        e.Graphics.DrawRectangle(Pens.DarkRed, Pitm.rect)
        '    Next
        '    For Each uitm In itm.Uload
        '        e.Graphics.DrawRectangle(Pens.DarkRed, uitm.rect)
        '    Next
        '    For Each mitm In itm.Mload
        '        e.Graphics.DrawRectangle(Pens.DarkRed, mitm.rect)
        '    Next
        'Next
    End Sub

    Private Sub rShearPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim PP As New System.Drawing.Pen(Color.CornflowerBlue, 3)
        Dim fnt As New Font("Verdana", 14)
        e.Graphics.DrawString("Shear Force Diagram", fnt, Brushes.CornflowerBlue, (1600 / 2 - coverpic.Width / 2) + 100, ht / 2 - 260)
        e.Graphics.DrawLine(PP, CInt(SFpts(0).X), CInt(Me.MEheight / 2), SFpts(0).X, SFpts(0).Y)
        e.Graphics.DrawLines(PP, SFpts)
        fnt = New Font("Verdana", 10, FontStyle.Bold)
        Dim i As Integer = 0
        For Each pt In SFmaxs
            e.Graphics.DrawString(Math.Round(SF(SFMc(i)), 3), fnt, Brushes.CornflowerBlue, pt.X, pt.Y)
            i = i + 1
        Next
    End Sub

    Private Sub rMomentPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim PP As New System.Drawing.Pen(Color.Crimson, 3)
        Dim fnt As Font
        fnt = New Font("Verdana", 14)
        e.Graphics.DrawString("Bending Moment Diagram", fnt, Brushes.Crimson, (1600 / 2 - coverpic.Width / 2) + 100, ht / 2 - 260)
        e.Graphics.DrawLine(PP, CInt(BMpts(0).X), CInt(Me.MEheight / 2), BMpts(0).X, BMpts(0).Y)
        e.Graphics.DrawLines(PP, BMpts)
        fnt = New Font("Verdana", 10, FontStyle.Bold)
        Dim i As Integer = 0
        For Each pt In BMmaxs
            e.Graphics.DrawString(Math.Round(BM(BMMc(i)), 3), fnt, Brushes.Crimson, pt.X, pt.Y)
            i = i + 1
        Next
    End Sub

    Private Sub rDeflectionPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim PP As New System.Drawing.Pen(Color.DarkGreen, 3)
        Dim fnt As Font
        fnt = New Font("Verdana", 14)
        e.Graphics.DrawString("Displacement Diagram", fnt, Brushes.DarkGreen, (1600 / 2 - coverpic.Width / 2) + 100, ht / 2 - 260)
        e.Graphics.DrawLines(PP, DEpts)
        fnt = New Font("Verdana", 10, FontStyle.Bold)
        Dim i As Integer = 0
        For Each pt In DEmaxs
            e.Graphics.DrawString(Math.Round(DE(DEMc(i)), 8), fnt, Brushes.DarkGreen, pt.X, pt.Y)
            i = i + 1
        Next
    End Sub

    Private Sub rSlopePaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim PP As New System.Drawing.Pen(Color.DeepPink, 3)
        Dim fnt As Font
        fnt = New Font("Verdana", 14)
        e.Graphics.DrawString("Slope Diagram", fnt, Brushes.DeepPink, (1600 / 2 - coverpic.Width / 2) + 100, ht / 2 - 260)
        e.Graphics.DrawLines(PP, SLpts)
        fnt = New Font("Verdana", 10, FontStyle.Bold)
        Dim i As Integer = 0
        For Each pt In SLmaxs
            e.Graphics.DrawString(Math.Round(SL(SLMc(i)), 8), fnt, Brushes.DeepPink, pt.X, pt.Y)
            i = i + 1
        Next
    End Sub
#End Region

#Region "Respic Zoom events"
    Private Sub respic_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles respic.MouseEnter
        respic.Focus()
    End Sub

    Private Sub respic_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles respic.MouseLeave
        MDIMain.ToolStrip1.Focus()
    End Sub

    Private Sub respic_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles respic.MouseWheel
        Dim xw, yw As Single
        xw = e.X / Zm
        yw = e.Y / Zm
        If e.Delta > 0 Then
            If Zm < 10 Then
                Zm = Zm + 0.25
            End If
        Else
            If Zm > 1 Then
                Zm = Zm - 0.25
            End If
        End If
        If e.X <> xw * Zm Then
            respic.Width = 1600 * Zm
            respic.Height = ht * Zm
            HScrollBar1.Maximum = (1600 * Zm) - coverpic.Width
            VScrollBar1.Maximum = (1600 * Zm) - coverpic.Height

            xw = -(respic.Left - ((xw * Zm) - e.X))
            yw = -(respic.Top - ((yw * Zm) - e.Y))

            If xw <= HScrollBar1.Minimum Then
                respic.Left = -1
                HScrollBar1.Value = 50
            ElseIf xw >= HScrollBar1.Maximum Then
                respic.Left = -HScrollBar1.Maximum
                HScrollBar1.Value = HScrollBar1.Maximum
            Else
                respic.Left = -xw
                HScrollBar1.Value = xw
            End If

            If yw <= VScrollBar1.Minimum Then
                respic.Top = -1
                VScrollBar1.Value = 100
            ElseIf yw >= VScrollBar1.Maximum Then
                respic.Top = -VScrollBar1.Maximum
                VScrollBar1.Value = VScrollBar1.Maximum
            Else
                respic.Top = -yw
                VScrollBar1.Value = yw
            End If

            ' mainpic.Top = -(((ht * Zm) - ht) / 2)
            respic.Refresh()
            MDIMain.ToolStripLabel1.Text = (Zm * 100) & "%"
            MDIMain.ToolStripLabel2.Text = (Zm * 100) & "%"
        End If
    End Sub
#End Region

#Region "Respic Mouse Move Events"

    Private Sub respic_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles respic.MouseMove
        Dim StDist As Single
        Dim ScaTotLength As Single

        StDist = (1600 / 2 - coverpic.Width / 2) + 100
        ScaTotLength = coverpic.Width - 200
        MDIMain.SFlabel.Visible = False
        MDIMain.BMlabel.Visible = False
        MDIMain.DELabel.Visible = False
        MDIMain.SLLabel.Visible = False
        MDIMain.Xlabel.Visible = False
        If MDIMain.ShearForceDiagramToolStripMenuItem.Checked = True Then
            If ((e.X / Zm) > StDist And (e.X / Zm) < StDist + ScaTotLength) And ((e.Y / Zm) > ht / 2 - 40 And (e.Y / Zm) < ht / 2 + 40) Then
                Dim i As Integer = (e.X / Zm) - StDist
                MDIMain.SFlabel.Text = Math.Round(SF(i), 4)
                MDIMain.SFlabel.Visible = True
                MDIMain.Xlabel.Text = Math.Round(((totL / (coverpic.Width - 200)) * i), 3) & "     ---> "
                MDIMain.Xlabel.Visible = True
            End If
        ElseIf MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = True Then
            If ((e.X / Zm) > StDist And (e.X / Zm) < StDist + ScaTotLength) And ((e.Y / Zm) > ht / 2 - 40 And (e.Y / Zm) < ht / 2 + 40) Then
                Dim i As Integer = (e.X / Zm) - StDist
                MDIMain.BMlabel.Text = Math.Round(BM(i), 4)
                MDIMain.BMlabel.Visible = True
                MDIMain.Xlabel.Text = Math.Round(((totL / (coverpic.Width - 200)) * i), 3) & "     ---> "
                MDIMain.Xlabel.Visible = True
            End If
        ElseIf MDIMain.DeflectionToolStripMenuItem.Checked = True Then
            If ((e.X / Zm) > StDist And (e.X / Zm) < StDist + ScaTotLength) And ((e.Y / Zm) > ht / 2 - 40 And (e.Y / Zm) < ht / 2 + 40) Then
                Dim i As Integer = (e.X / Zm) - StDist
                MDIMain.DELabel.Text = Math.Round(DE(i), 8)
                MDIMain.DELabel.Visible = True
                MDIMain.Xlabel.Text = Math.Round(((totL / (coverpic.Width - 200)) * i), 3) & "     ---> "
                MDIMain.Xlabel.Visible = True
            End If
        ElseIf MDIMain.SlopeToolStripMenuItem.Checked = True Then
            If ((e.X / Zm) > StDist And (e.X / Zm) < StDist + ScaTotLength) And ((e.Y / Zm) > ht / 2 - 40 And (e.Y / Zm) < ht / 2 + 40) Then
                Dim i As Integer = (e.X / Zm) - StDist
                MDIMain.SLLabel.Text = Math.Round(SL(i), 8)
                MDIMain.SLLabel.Visible = True
                MDIMain.Xlabel.Text = Math.Round(((totL / (coverpic.Width - 200)) * i), 3) & "     ---> "
                MDIMain.Xlabel.Visible = True
            End If
        End If
    End Sub
#End Region

#Region "Respic Mouse Click Events"
    Private Sub respic_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles respic.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip3.Show(respic.PointToScreen(e.Location))
        End If
    End Sub
#End Region

#Region "Context Menu 3 Events"
    Private Sub BendingMomentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BendingMomentToolStripMenuItem.Click
        ' MDIMain.BendingMomentDiagramToolStripMenuItem_Click(sender, e)
        If MDIMain.ShearForceDiagramToolStripMenuItem.Checked = True Or MDIMain.DeflectionToolStripMenuItem.Checked = True Or MDIMain.SlopeToolStripMenuItem.Checked = True Then
            MDIMain.ShearForceDiagramToolStripMenuItem.Checked = False
            MDIMain.DeflectionToolStripMenuItem.Checked = False
            MDIMain.SlopeToolStripMenuItem.Checked = False
            MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = True
            respic.Refresh()
        End If
    End Sub

    Private Sub ShearForceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShearForceToolStripMenuItem.Click
        ' MDIMain.ShearForceDiagramToolStripMenuItem_Click(sender, e)
        If MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = True Or MDIMain.DeflectionToolStripMenuItem.Checked = True Or MDIMain.SlopeToolStripMenuItem.Checked = True Then
            MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = False
            MDIMain.DeflectionToolStripMenuItem.Checked = False
            MDIMain.SlopeToolStripMenuItem.Checked = False
            MDIMain.ShearForceDiagramToolStripMenuItem.Checked = True
            respic.Refresh()
        End If
    End Sub

    Private Sub DeflectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeflectionToolStripMenuItem.Click
        '  MDIMain.DeflectionToolStripMenuItem_Click(sender, e)
        If MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = True Or MDIMain.ShearForceDiagramToolStripMenuItem.Checked = True Or MDIMain.SlopeToolStripMenuItem.Checked = False Then
            MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = False
            MDIMain.ShearForceDiagramToolStripMenuItem.Checked = False
            MDIMain.SlopeToolStripMenuItem.Checked = False
            MDIMain.DeflectionToolStripMenuItem.Checked = True
            respic.Refresh()
        End If
    End Sub

    Private Sub SlopeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SlopeToolStripMenuItem.Click
        '  MDIMain.SlopeToolStripMenuItem_Click(sender, e)
        If MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = True Or MDIMain.ShearForceDiagramToolStripMenuItem.Checked = True Or MDIMain.DeflectionToolStripMenuItem.Checked = True Then
            MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = False
            MDIMain.ShearForceDiagramToolStripMenuItem.Checked = False
            MDIMain.DeflectionToolStripMenuItem.Checked = False
            MDIMain.SlopeToolStripMenuItem.Checked = True
            respic.Refresh()
        End If
    End Sub
#End Region
#End Region



End Class