<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class beamcreate
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.coverpic = New System.Windows.Forms.Panel()
        Me.respic = New System.Windows.Forms.Panel()
        Me.mainpic = New System.Windows.Forms.Panel()
        Me.HScrollBar1 = New System.Windows.Forms.HScrollBar()
        Me.VScrollBar1 = New System.Windows.Forms.VScrollBar()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddMemberToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditEndsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditMemebrToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveMemberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddLoadToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveLoadsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddMemberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditEndsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditLoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveLoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BendingMomentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShearForceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeflectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SlopeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.coverpic.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ContextMenuStrip3.SuspendLayout()
        Me.SuspendLayout()
        '
        'coverpic
        '
        Me.coverpic.BackColor = System.Drawing.Color.White
        Me.coverpic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.coverpic.Controls.Add(Me.respic)
        Me.coverpic.Controls.Add(Me.mainpic)
        Me.coverpic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.coverpic.Location = New System.Drawing.Point(0, 0)
        Me.coverpic.Name = "coverpic"
        Me.coverpic.Size = New System.Drawing.Size(741, 435)
        Me.coverpic.TabIndex = 0
        '
        'respic
        '
        Me.respic.BackColor = System.Drawing.Color.White
        Me.respic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.respic.Location = New System.Drawing.Point(101, 269)
        Me.respic.Name = "respic"
        Me.respic.Size = New System.Drawing.Size(187, 122)
        Me.respic.TabIndex = 1
        '
        'mainpic
        '
        Me.mainpic.BackColor = System.Drawing.Color.White
        Me.mainpic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.mainpic.Location = New System.Drawing.Point(101, 86)
        Me.mainpic.Name = "mainpic"
        Me.mainpic.Size = New System.Drawing.Size(187, 137)
        Me.mainpic.TabIndex = 0
        '
        'HScrollBar1
        '
        Me.HScrollBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HScrollBar1.Location = New System.Drawing.Point(1, 413)
        Me.HScrollBar1.Name = "HScrollBar1"
        Me.HScrollBar1.Size = New System.Drawing.Size(719, 20)
        Me.HScrollBar1.TabIndex = 1
        '
        'VScrollBar1
        '
        Me.VScrollBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VScrollBar1.Location = New System.Drawing.Point(718, 0)
        Me.VScrollBar1.Name = "VScrollBar1"
        Me.VScrollBar1.Size = New System.Drawing.Size(20, 413)
        Me.VScrollBar1.TabIndex = 2
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddMemberToolStripMenuItem1, Me.EditEndsToolStripMenuItem, Me.ToolStripSeparator9, Me.EditMemebrToolStripMenuItem, Me.RemoveMemberToolStripMenuItem, Me.AddLoadToolStripMenuItem1, Me.RemoveLoadsToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(166, 142)
        '
        'AddMemberToolStripMenuItem1
        '
        Me.AddMemberToolStripMenuItem1.Image = Global.CBAnsDes.My.Resources.Resources.addmember
        Me.AddMemberToolStripMenuItem1.Name = "AddMemberToolStripMenuItem1"
        Me.AddMemberToolStripMenuItem1.Size = New System.Drawing.Size(165, 22)
        Me.AddMemberToolStripMenuItem1.Text = "Add Member"
        '
        'EditEndsToolStripMenuItem
        '
        Me.EditEndsToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.editends
        Me.EditEndsToolStripMenuItem.Name = "EditEndsToolStripMenuItem"
        Me.EditEndsToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.EditEndsToolStripMenuItem.Text = "Edit Ends"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(162, 6)
        '
        'EditMemebrToolStripMenuItem
        '
        Me.EditMemebrToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.editmember
        Me.EditMemebrToolStripMenuItem.Name = "EditMemebrToolStripMenuItem"
        Me.EditMemebrToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.EditMemebrToolStripMenuItem.Text = "Edit Memebr"
        '
        'RemoveMemberToolStripMenuItem
        '
        Me.RemoveMemberToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.deletemember
        Me.RemoveMemberToolStripMenuItem.Name = "RemoveMemberToolStripMenuItem"
        Me.RemoveMemberToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.RemoveMemberToolStripMenuItem.Text = "Remove Member"
        '
        'AddLoadToolStripMenuItem1
        '
        Me.AddLoadToolStripMenuItem1.Image = Global.CBAnsDes.My.Resources.Resources.addload
        Me.AddLoadToolStripMenuItem1.Name = "AddLoadToolStripMenuItem1"
        Me.AddLoadToolStripMenuItem1.Size = New System.Drawing.Size(165, 22)
        Me.AddLoadToolStripMenuItem1.Text = "Add Load"
        '
        'RemoveLoadsToolStripMenuItem
        '
        Me.RemoveLoadsToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.removeloads
        Me.RemoveLoadsToolStripMenuItem.Name = "RemoveLoadsToolStripMenuItem"
        Me.RemoveLoadsToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.RemoveLoadsToolStripMenuItem.Text = "Remove Loads"
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddMemberToolStripMenuItem, Me.EditEndsToolStripMenuItem1, Me.ToolStripSeparator1, Me.EditLoadToolStripMenuItem, Me.RemoveLoadToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(147, 98)
        '
        'AddMemberToolStripMenuItem
        '
        Me.AddMemberToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.addmember
        Me.AddMemberToolStripMenuItem.Name = "AddMemberToolStripMenuItem"
        Me.AddMemberToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.AddMemberToolStripMenuItem.Text = "Add Member"
        '
        'EditEndsToolStripMenuItem1
        '
        Me.EditEndsToolStripMenuItem1.Image = Global.CBAnsDes.My.Resources.Resources.editends
        Me.EditEndsToolStripMenuItem1.Name = "EditEndsToolStripMenuItem1"
        Me.EditEndsToolStripMenuItem1.Size = New System.Drawing.Size(146, 22)
        Me.EditEndsToolStripMenuItem1.Text = "Edit Ends"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(143, 6)
        '
        'EditLoadToolStripMenuItem
        '
        Me.EditLoadToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.modifyload
        Me.EditLoadToolStripMenuItem.Name = "EditLoadToolStripMenuItem"
        Me.EditLoadToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.EditLoadToolStripMenuItem.Text = "Edit Load"
        '
        'RemoveLoadToolStripMenuItem
        '
        Me.RemoveLoadToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.removeload
        Me.RemoveLoadToolStripMenuItem.Name = "RemoveLoadToolStripMenuItem"
        Me.RemoveLoadToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.RemoveLoadToolStripMenuItem.Text = "Remove Load"
        '
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BendingMomentToolStripMenuItem, Me.ShearForceToolStripMenuItem, Me.DeflectionToolStripMenuItem, Me.SlopeToolStripMenuItem})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip3"
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(168, 92)
        '
        'BendingMomentToolStripMenuItem
        '
        Me.BendingMomentToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.bm
        Me.BendingMomentToolStripMenuItem.Name = "BendingMomentToolStripMenuItem"
        Me.BendingMomentToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.BendingMomentToolStripMenuItem.Text = "Bending Moment"
        '
        'ShearForceToolStripMenuItem
        '
        Me.ShearForceToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.shear
        Me.ShearForceToolStripMenuItem.Name = "ShearForceToolStripMenuItem"
        Me.ShearForceToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.ShearForceToolStripMenuItem.Text = "Shear Force"
        '
        'DeflectionToolStripMenuItem
        '
        Me.DeflectionToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.bend
        Me.DeflectionToolStripMenuItem.Name = "DeflectionToolStripMenuItem"
        Me.DeflectionToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.DeflectionToolStripMenuItem.Text = "Deflection"
        '
        'SlopeToolStripMenuItem
        '
        Me.SlopeToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.Slope
        Me.SlopeToolStripMenuItem.Name = "SlopeToolStripMenuItem"
        Me.SlopeToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.SlopeToolStripMenuItem.Text = "Slope"
        '
        'beamcreate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(741, 435)
        Me.Controls.Add(Me.VScrollBar1)
        Me.Controls.Add(Me.HScrollBar1)
        Me.Controls.Add(Me.coverpic)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MinimumSize = New System.Drawing.Size(400, 300)
        Me.Name = "beamcreate"
        Me.Opacity = 0.9R
        Me.Text = "beamcreate"
        Me.coverpic.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ContextMenuStrip3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents coverpic As System.Windows.Forms.Panel
    Friend WithEvents HScrollBar1 As System.Windows.Forms.HScrollBar
    Friend WithEvents mainpic As System.Windows.Forms.Panel
    Friend WithEvents VScrollBar1 As System.Windows.Forms.VScrollBar
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddMemberToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditEndsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditMemebrToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveMemberToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddLoadToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveLoadsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditLoadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveLoadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents respic As System.Windows.Forms.Panel
    Friend WithEvents AddMemberToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditEndsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ContextMenuStrip3 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BendingMomentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShearForceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeflectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SlopeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
