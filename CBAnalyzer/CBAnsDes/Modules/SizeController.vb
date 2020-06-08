Module SizeController
    Public Sub sizemonitor()
        Dim ht As Single
        Dim wd As Single
        ht = beamcreate.coverpic.Height
        wd = beamcreate.coverpic.Width
        beamcreate.mainpic.Top = 0
        beamcreate.mainpic.Height = max_width
        beamcreate.respic.Top = 0
        beamcreate.respic.Height = max_width
        '----Hscrollbar1
        beamcreate.HScrollBar1.Maximum = (max_width - beamcreate.coverpic.Width)
        beamcreate.HScrollBar1.Minimum = 50
        beamcreate.HScrollBar1.Value = beamcreate.HScrollBar1.Maximum / 2
        beamcreate.mainpic.Left = -(beamcreate.HScrollBar1.Value)
        beamcreate.respic.Left = -(beamcreate.HScrollBar1.Value)
        '----VscrollBar
        beamcreate.VScrollBar1.Maximum = (max_width - ht)
        beamcreate.VScrollBar1.Minimum = 100
        beamcreate.VScrollBar1.Value = beamcreate.VScrollBar1.Maximum / 2
        beamcreate.mainpic.Top = -(beamcreate.VScrollBar1.Value)
        beamcreate.respic.Top = -(beamcreate.VScrollBar1.Value)
        beamcreate.MEheight = max_width
        Zm = 1
        beamcreate.mainpic.Refresh()
        If MDIMain.CreateToolStripMenuItem.Checked = False Then
            Call CoordinateCalculator()
        End If
    End Sub
End Module
