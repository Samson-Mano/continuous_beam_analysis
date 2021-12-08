Imports System.Windows.Forms
Imports System.IO
Imports System.Collections.Specialized
Imports System.Runtime.Serialization.Formatters.Binary
Public Class MDIMain

#Region "New Menu"

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripMenuItem.Click, NewToolStripButton.Click
        Dim msgrslt As String
        If mem.Count <> 0 Then
            CreateToolStripMenuItem_Click(sender, e)
            msgrslt = MessageBox.Show("Do you want to save your Job ?", "Samson Mano", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If msgrslt = Windows.Forms.DialogResult.Yes Then
                SaveAsToolStripMenuItem_Click(sender, e)
            ElseIf msgrslt = Windows.Forms.DialogResult.No Then
                GoTo x1
            Else
                beamcreate.mainpic.Refresh()
                Exit Sub
            End If

        End If
x1:
        mem.Clear()
        Dim Frm As New Newapp(False)
        Frm.ShowInTaskbar = False
        Frm.ShowDialog()

        If Frm.F <> True Then
            ToolStrip1.Visible = False
            EditMenu.Enabled = False
            ComputeToolStripMenuItem.Enabled = False
            ViewMenu.Enabled = False
            ToolsMenu.Enabled = False
            beamcreate.Visible = False
            beamcreate.mainpic.Refresh()
            Exit Sub
        End If

        ' Make it a child of this MDI form before showing it.
        beamcreate.MdiParent = Me
        beamcreate.Text = Frm.TextBox1.Text
        beamcreate.Dock = DockStyle.Fill
        beamcreate.Show()

        ToolStrip1.Visible = True
        ToolStripButton1.Enabled = True
        AddMemberToolStripMenuItem.Enabled = True
        ToolStripButton2.Enabled = True
        BeamEndsToolStripMenuItem.Enabled = True
        ToolStripButton3.Enabled = False
        editMemberToolStripMenuItem.Enabled = False
        ToolStripButton4.Enabled = False
        RemoveMemberToolStripMenuItem.Enabled = False
        ToolStripButton5.Enabled = False
        AddLoadToolStripMenuItem.Enabled = False
        ToolStripButton6.Enabled = False
        RemoveLoadsToolStripMenuItem.Enabled = False
        ToolStripButton7.Enabled = False
        ModifyLoadToolStripMenuItem.Enabled = False
        ToolStripButton8.Enabled = False
        RemoveLoadToolStripMenuItem.Enabled = False

        EditMenu.Enabled = True
        ComputeToolStripMenuItem.Enabled = True
        ViewMenu.Enabled = True
        ToolsMenu.Enabled = True
        CreateToolStripMenuItem_Click(sender, e)
        CreateToolStripMenuItem.Checked = True
        beamcreate.mainpic.Refresh()
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click, OpenToolStripButton.Click
        Dim msgrslt As String
        CreateToolStripMenuItem_Click(sender, e)
        CreateToolStripMenuItem.Checked = True
        If mem.Count <> 0 Then
            msgrslt = MessageBox.Show("Do you want to save your Job ?", "Samson Mano", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If msgrslt = Windows.Forms.DialogResult.Yes Then
                SaveAsToolStripMenuItem_Click(sender, e)
            ElseIf msgrslt = Windows.Forms.DialogResult.No Then
                GoTo x1
            Else
                beamcreate.mainpic.Refresh()
                Exit Sub
            End If
        Else
x1:
            mem.Clear()
            beamcreate.mainpic.Refresh()

            beamcreate.Visible = False
            ToolStrip1.Visible = False
            ToolStrip2.Visible = False
            EditMenu.Enabled = False
            ComputeToolStripMenuItem.Enabled = False
            ViewMenu.Enabled = False
            ToolsMenu.Enabled = False

            Dim ofd = New OpenFileDialog()
            ofd.DefaultExt = ".cbf"
            ofd.Filter = "Samson Mano's continuous beam Object Files (*.cbf)|*.cbf"
            ofd.ShowDialog()
            If File.Exists(ofd.FileName) Then
                Dim cbobject As New List(Of Object)
                Dim gsf As Stream = File.OpenRead(ofd.FileName)
                Dim deserializer As New BinaryFormatter
                Try
                    cbobject = CType(deserializer.Deserialize(gsf), Object)
                Catch ex As Exception
                    MessageBox.Show("Unable to Open !!!", "Samson Mano", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                Dim instance1 As List(Of Member) = cbobject(0)
                mem = instance1
                Dim instance2 As Integer = cbobject(1)
                ends = instance2
                '----Call For ALL
                beamcreate.MdiParent = Me
                beamcreate.Text = ofd.FileName
                beamcreate.Dock = DockStyle.Fill
                beamcreate.Show()

                ToolStrip1.Visible = True
                ToolStripButton1.Enabled = True
                AddMemberToolStripMenuItem.Enabled = True
                ToolStripButton2.Enabled = True
                BeamEndsToolStripMenuItem.Enabled = True
                ToolStripButton3.Enabled = False
                editMemberToolStripMenuItem.Enabled = False
                ToolStripButton4.Enabled = False
                RemoveMemberToolStripMenuItem.Enabled = False
                ToolStripButton5.Enabled = False
                AddLoadToolStripMenuItem.Enabled = False
                ToolStripButton6.Enabled = False
                RemoveLoadsToolStripMenuItem.Enabled = False
                ToolStripButton7.Enabled = False
                ModifyLoadToolStripMenuItem.Enabled = False
                ToolStripButton8.Enabled = False
                RemoveLoadToolStripMenuItem.Enabled = False

                EditMenu.Enabled = True
                ComputeToolStripMenuItem.Enabled = True
                ViewMenu.Enabled = True
                ToolsMenu.Enabled = True
                CreateToolStripMenuItem_Click(sender, e)
                CreateToolStripMenuItem.Checked = True
                beamcreate.mainpic.Refresh()
                Me.WindowState = FormWindowState.Maximized
                gsf.Close()
            Else
                'MessageBox.Show("File does not exist!")
            End If
            beamcreate.mainpic.Refresh()
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripButton.Click

        If mem.Count <> 0 Then
            CreateToolStripMenuItem_Click(sender, e)
            CreateToolStripMenuItem.Checked = True
            Dim sfd = New SaveFileDialog
            sfd.DefaultExt = ".cbf"
            sfd.Filter = "Samson Mano's continuous beam Object Files (*.cbf)|*.cbf"
            sfd.ShowDialog()

            If sfd.FileName <> "" Then
                Dim cbobject As New List(Of Object)

                cbobject.Add(mem)
                cbobject.Add(ends)
                Dim psf As Stream = File.Create(sfd.FileName)
                Dim serializer As New BinaryFormatter
                serializer.Serialize(psf, cbobject)
                psf.Close()
            End If
        End If
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

#End Region

#Region "Edit Menu"

#End Region

#Region "View Menu"
    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub
#End Region

#Region "Toolstrip1"
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddMemberToolStripMenuItem.Click, ToolStripButton1.Click
        '--Add Member
        Dim a As New addmember
        a.Text = "Add Member"
        a.ShowDialog()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeamEndsToolStripMenuItem.Click, ToolStripButton2.Click
        '--Edit Ends
        Dim a As New Ends_Editor
        a.ShowDialog()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles editMemberToolStripMenuItem.Click, ToolStripButton3.Click
        '--Edit Member
        Dim a As New addmember
        a.Text = "Modify Member"
        a.TextBox1.Text = mem(selline).spanlength
        a.TextBox2.Text = mem(selline).Emodulus
        a.TextBox3.Text = mem(selline).Inertia
        a.TextBox6.Text = mem(selline).g
        a.ShowDialog()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveMemberToolStripMenuItem.Click, ToolStripButton4.Click
        '--Remove Member
        If ends = 4 And mem.Count = 3 Then
            Exit Sub
        ElseIf ends = 6 And mem.Count = 2 Then
            Exit Sub
        End If
        If mem.Count <> 1 Then
            mem.RemoveAt(selline)
            selline = -1
            beamcreate.toolstrip1Mod()
            beamcreate.mainpic.Refresh()
        End If
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddLoadToolStripMenuItem.Click, ToolStripButton5.Click
        '--Add Loads
        Dim a As New LoadWindow(mem(selline).spanlength, selline)
        a.ShowDialog()
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveLoadsToolStripMenuItem.Click, ToolStripButton6.Click
        '--Remove Loads
        mem(selline).Pload.Clear()
        mem(selline).Uload.Clear()
        mem(selline).Mload.Clear()
        beamcreate.mainpic.Refresh()
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModifyLoadToolStripMenuItem.Click, ToolStripButton7.Click
        '--Edit Load
        If beamcreate.tipe = 1 Then
            Dim a As New LoadWindow(mem(Lselline).spanlength, Lselline, beamcreate.selPL)
            a.ShowDialog()
        ElseIf beamcreate.tipe = 2 Then
            Dim a As New LoadWindow(mem(Lselline).spanlength, Lselline, beamcreate.selUL)
            a.ShowDialog()
        ElseIf beamcreate.tipe = 3 Then
            Dim a As New LoadWindow(mem(Lselline).spanlength, Lselline, beamcreate.selML)
            a.ShowDialog()
        End If
        beamcreate.mainpic.Refresh()
    End Sub

    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveLoadToolStripMenuItem.Click, ToolStripButton8.Click
        '--Single Load is removed
        If beamcreate.tipe = 1 Then
            mem(Lselline).Pload.Remove(beamcreate.selPL)
        ElseIf beamcreate.tipe = 2 Then
            mem(Lselline).Uload.Remove(beamcreate.selUL)
        ElseIf beamcreate.tipe = 3 Then
            mem(Lselline).Mload.Remove(beamcreate.selML)
        End If
        '--Null selected load
        Dim ptm As New Member.P
        Dim utm As New Member.U
        Dim mtm As New Member.M
        beamcreate.selPL = ptm
        beamcreate.selUL = utm
        beamcreate.selML = mtm
        selline = -1
        Lselline = -1
        beamcreate.toolstrip1Mod()
        beamcreate.mainpic.Refresh()
    End Sub
#End Region

#Region "Compute Menu"
    Private Sub CreateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateToolStripMenuItem.Click
        AnalyzeToolStripMenuItem.Checked = False
        CreateToolStripMenuItem.Checked = True
        DesignToolStripMenuItem.Checked = False
        beamcreate.mainpic.Visible = True
        beamcreate.respic.Visible = False
        DesignToolStripMenuItem.Visible = False
        ResultToolStripMenuItem.Visible = False
        EditMenu.Enabled = True
        ToolStrip1.Visible = True
        ToolStrip2.Visible = False
        beamcreate.mainpic.Refresh()
        beamcreate.respic.Refresh()
        ToolStripLabel1.Text = "100%"
        ToolStripLabel2.Text = "100%"
    End Sub

    Private Sub AnalyzeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnalyzeToolStripMenuItem.Click
        For Each m In mem
            If m.Pload.Count <> 0 Or m.Uload.Count <> 0 Or m.Mload.Count <> 0 Then
                '------Analysis Part
                'Try
                Call TESLA()
                'Catch ex As Exception
                '    MsgBox("Error Analysis .....                   " & vbNewLine & "Please Report Bug" & vbNewLine & "..................................................", MsgBoxStyle.OkOnly, "Bug")
                '    CreateToolStripMenuItem_Click(sender, e)
                '    Exit Sub
                'End Try

                DesignToolStripMenuItem.Checked = False
                AnalyzeToolStripMenuItem.Checked = True
                CreateToolStripMenuItem.Checked = False
                beamcreate.mainpic.Visible = False
                beamcreate.respic.Visible = True
                DesignToolStripMenuItem.Visible = True
                ResultToolStripMenuItem.Visible = True
                ToolStrip1.Visible = False
                ToolStrip2.Visible = True
                EditMenu.Enabled = False
                beamcreate.mainpic.Refresh()
                beamcreate.respic.Refresh()
                ToolStripLabel1.Text = 100%
                ToolStripLabel2.Text = 100%

                BendingMomentDiagramToolStripMenuItem_Click(sender, e)
                Exit Sub
            Else
                Continue For
            End If
        Next
        MsgBox("No valid Load type is found !!!!!!!!", MsgBoxStyle.OkOnly, "No Load")
        CreateToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub DesignToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DesignToolStripMenuItem.Click
        CreateToolStripMenuItem.Checked = False
        AnalyzeToolStripMenuItem.Checked = False
    End Sub

    Private Sub ShearForceDiagramToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShearForceDiagramToolStripMenuItem.Click, ToolStripButton9.Click
        If BendingMomentDiagramToolStripMenuItem.Checked = True Or DeflectionToolStripMenuItem.Checked = True Or SlopeToolStripMenuItem.Checked = True Then
            BendingMomentDiagramToolStripMenuItem.Checked = False
            DeflectionToolStripMenuItem.Checked = False
            SlopeToolStripMenuItem.Checked = False
            ShearForceDiagramToolStripMenuItem.Checked = True
            beamcreate.respic.Refresh()
        End If
    End Sub

    Private Sub BendingMomentDiagramToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BendingMomentDiagramToolStripMenuItem.Click, ToolStripButton10.Click
        If ShearForceDiagramToolStripMenuItem.Checked = True Or DeflectionToolStripMenuItem.Checked = True Or SlopeToolStripMenuItem.Checked = True Then
            ShearForceDiagramToolStripMenuItem.Checked = False
            DeflectionToolStripMenuItem.Checked = False
            BendingMomentDiagramToolStripMenuItem.Checked = True
            SlopeToolStripMenuItem.Checked = False
            beamcreate.respic.Refresh()
        End If
    End Sub

    Private Sub DeflectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeflectionToolStripMenuItem.Click, ToolStripButton11.Click
        If BendingMomentDiagramToolStripMenuItem.Checked = True Or ShearForceDiagramToolStripMenuItem.Checked = True Or SlopeToolStripMenuItem.Checked = True Then
            BendingMomentDiagramToolStripMenuItem.Checked = False
            ShearForceDiagramToolStripMenuItem.Checked = False
            DeflectionToolStripMenuItem.Checked = True
            SlopeToolStripMenuItem.Checked = False
            beamcreate.respic.Refresh()
        End If
    End Sub
    Private Sub SlopeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SlopeToolStripMenuItem.Click, ToolStripButton12.Click
        If BendingMomentDiagramToolStripMenuItem.Checked = True Or ShearForceDiagramToolStripMenuItem.Checked = True Or DeflectionToolStripMenuItem.Checked = True Then
            BendingMomentDiagramToolStripMenuItem.Checked = False
            ShearForceDiagramToolStripMenuItem.Checked = False
            DeflectionToolStripMenuItem.Checked = False
            SlopeToolStripMenuItem.Checked = True
            beamcreate.respic.Refresh()
        End If
    End Sub
#End Region


    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ToolStrip1.Visible = False
        ToolStrip2.Visible = False
        EditMenu.Enabled = False
        ComputeToolStripMenuItem.Enabled = False
        ViewMenu.Enabled = False
        ToolsMenu.Enabled = False
        '---Scene
        'Me.Visible = False
        'logo.ShowDialog()
        'Me.Visible = True
    End Sub

    Private Sub GeneralInstructionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GeneralInstructionToolStripMenuItem.Click
        GeneralInstruction.ShowDialog()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Aboutus.ShowDialog()
    End Sub

    Private Sub memdetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles memdetailsToolStripMenuItem.Click
        TESLA(True)
    End Sub

    Private Sub CalculatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculatorToolStripMenuItem.Click
        Dim p As New Process()
        p.StartInfo.FileName = "calc.exe"
        p.Start()
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'PrintDocument1.DefaultPageSettings.PaperSource.Kind = "Custom"



        'PrintDocument1.DefaultPageSettings.PaperSize.Height = 21 'PrintDocument1.DefaultPageSettings.PaperSize.Width = 10 PrintPreviewDialog1.Document = PrintDocument1



        PrintPreviewDialog1.ShowDialog()


    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

      
    End Sub

End Class
