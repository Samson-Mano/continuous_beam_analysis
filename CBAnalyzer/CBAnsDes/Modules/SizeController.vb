﻿Module SizeController
    Public Sub sizemonitor()
        'Dim ht As Single
        'Dim wd As Single
        'ht = beamcreate.coverpic.Height
        'wd = beamcreate.coverpic.Width
        'beamcreate.mainpic.Top = 0
        'beamcreate.mainpic.Height = 1600
        'beamcreate.respic.Top = 0
        'beamcreate.respic.Height = 1600
        '----Hscrollbar1
        'beamcreate.HScrollBar1.Maximum = (1600 - beamcreate.coverpic.Width)
        'beamcreate.HScrollBar1.Minimum = 50
        'beamcreate.HScrollBar1.Value = beamcreate.HScrollBar1.Maximum / 2
        'beamcreate.mainpic.Left = -(beamcreate.HScrollBar1.Value)
        'beamcreate.respic.Left = -(beamcreate.HScrollBar1.Value)
        '----VscrollBar
        'beamcreate.VScrollBar1.Maximum = (1600 - ht)
        'beamcreate.VScrollBar1.Minimum = 100
        'beamcreate.VScrollBar1.Value = beamcreate.VScrollBar1.Maximum / 2
        'beamcreate.mainpic.Top = -(beamcreate.VScrollBar1.Value)
        'beamcreate.respic.Top = -(beamcreate.VScrollBar1.Value)
        'beamcreate.MEheight = 1600
        Zm = 1
        If beamcreate.mainpic.Visible = True Then
            MidPt = New Point(beamcreate.mainpic.Width / 2, beamcreate.mainpic.Height / 2)
        Else
            MidPt = New Point(beamcreate.respic.Width / 2, beamcreate.respic.Height / 2)
        End If



        If MDIMain.CreateToolStripMenuItem.Checked = False Then
            Call CoordinateCalculator()
        End If

        beamcreate.mainpic.Refresh()
        beamcreate.respic.Refresh()
    End Sub
End Module
