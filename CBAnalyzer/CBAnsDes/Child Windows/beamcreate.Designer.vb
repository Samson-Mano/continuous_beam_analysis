<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class beamcreate
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.coverpic = New System.Windows.Forms.Panel()
        Me.respic = New System.Windows.Forms.Panel()
        Me.mainpic = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
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
        Me.res_coverpic = New System.Windows.Forms.PictureBox()
        Me.coverpic.SuspendLayout()
        Me.respic.SuspendLayout()
        Me.mainpic.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ContextMenuStrip3.SuspendLayout()
        CType(Me.res_coverpic, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.coverpic.Margin = New System.Windows.Forms.Padding(4)
        Me.coverpic.Name = "coverpic"
        Me.coverpic.Size = New System.Drawing.Size(988, 535)
        Me.coverpic.TabIndex = 0
        '
        'respic
        '
        Me.respic.BackColor = System.Drawing.Color.White
        Me.respic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.respic.Controls.Add(Me.res_coverpic)
        Me.respic.Location = New System.Drawing.Point(104, 373)
        Me.respic.Margin = New System.Windows.Forms.Padding(4)
        Me.respic.Name = "respic"
        Me.respic.Size = New System.Drawing.Size(263, 112)
        Me.respic.TabIndex = 1
        '
        'mainpic
        '
        Me.mainpic.BackColor = System.Drawing.Color.White
        Me.mainpic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.mainpic.Controls.Add(Me.PictureBox1)
        Me.mainpic.Location = New System.Drawing.Point(12, 14)
        Me.mainpic.Margin = New System.Windows.Forms.Padding(4)
        Me.mainpic.Name = "mainpic"
        Me.mainpic.Size = New System.Drawing.Size(831, 317)
        Me.mainpic.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Enabled = False
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(829, 315)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddMemberToolStripMenuItem1, Me.EditEndsToolStripMenuItem, Me.ToolStripSeparator9, Me.EditMemebrToolStripMenuItem, Me.RemoveMemberToolStripMenuItem, Me.AddLoadToolStripMenuItem1, Me.RemoveLoadsToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(197, 166)
        '
        'AddMemberToolStripMenuItem1
        '
        Me.AddMemberToolStripMenuItem1.Image = Global.CBAnsDes.My.Resources.Resources.addmember
        Me.AddMemberToolStripMenuItem1.Name = "AddMemberToolStripMenuItem1"
        Me.AddMemberToolStripMenuItem1.Size = New System.Drawing.Size(196, 26)
        Me.AddMemberToolStripMenuItem1.Text = "Add Member"
        '
        'EditEndsToolStripMenuItem
        '
        Me.EditEndsToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.editends
        Me.EditEndsToolStripMenuItem.Name = "EditEndsToolStripMenuItem"
        Me.EditEndsToolStripMenuItem.Size = New System.Drawing.Size(196, 26)
        Me.EditEndsToolStripMenuItem.Text = "Edit Ends"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(193, 6)
        '
        'EditMemebrToolStripMenuItem
        '
        Me.EditMemebrToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.editmember
        Me.EditMemebrToolStripMenuItem.Name = "EditMemebrToolStripMenuItem"
        Me.EditMemebrToolStripMenuItem.Size = New System.Drawing.Size(196, 26)
        Me.EditMemebrToolStripMenuItem.Text = "Edit Memebr"
        '
        'RemoveMemberToolStripMenuItem
        '
        Me.RemoveMemberToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.deletemember
        Me.RemoveMemberToolStripMenuItem.Name = "RemoveMemberToolStripMenuItem"
        Me.RemoveMemberToolStripMenuItem.Size = New System.Drawing.Size(196, 26)
        Me.RemoveMemberToolStripMenuItem.Text = "Remove Member"
        '
        'AddLoadToolStripMenuItem1
        '
        Me.AddLoadToolStripMenuItem1.Image = Global.CBAnsDes.My.Resources.Resources.addload
        Me.AddLoadToolStripMenuItem1.Name = "AddLoadToolStripMenuItem1"
        Me.AddLoadToolStripMenuItem1.Size = New System.Drawing.Size(196, 26)
        Me.AddLoadToolStripMenuItem1.Text = "Add Load"
        '
        'RemoveLoadsToolStripMenuItem
        '
        Me.RemoveLoadsToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.removeloads
        Me.RemoveLoadsToolStripMenuItem.Name = "RemoveLoadsToolStripMenuItem"
        Me.RemoveLoadsToolStripMenuItem.Size = New System.Drawing.Size(196, 26)
        Me.RemoveLoadsToolStripMenuItem.Text = "Remove Loads"
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddMemberToolStripMenuItem, Me.EditEndsToolStripMenuItem1, Me.ToolStripSeparator1, Me.EditLoadToolStripMenuItem, Me.RemoveLoadToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(174, 114)
        '
        'AddMemberToolStripMenuItem
        '
        Me.AddMemberToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.addmember
        Me.AddMemberToolStripMenuItem.Name = "AddMemberToolStripMenuItem"
        Me.AddMemberToolStripMenuItem.Size = New System.Drawing.Size(173, 26)
        Me.AddMemberToolStripMenuItem.Text = "Add Member"
        '
        'EditEndsToolStripMenuItem1
        '
        Me.EditEndsToolStripMenuItem1.Image = Global.CBAnsDes.My.Resources.Resources.editends
        Me.EditEndsToolStripMenuItem1.Name = "EditEndsToolStripMenuItem1"
        Me.EditEndsToolStripMenuItem1.Size = New System.Drawing.Size(173, 26)
        Me.EditEndsToolStripMenuItem1.Text = "Edit Ends"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(170, 6)
        '
        'EditLoadToolStripMenuItem
        '
        Me.EditLoadToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.modifyload
        Me.EditLoadToolStripMenuItem.Name = "EditLoadToolStripMenuItem"
        Me.EditLoadToolStripMenuItem.Size = New System.Drawing.Size(173, 26)
        Me.EditLoadToolStripMenuItem.Text = "Edit Load"
        '
        'RemoveLoadToolStripMenuItem
        '
        Me.RemoveLoadToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.removeload
        Me.RemoveLoadToolStripMenuItem.Name = "RemoveLoadToolStripMenuItem"
        Me.RemoveLoadToolStripMenuItem.Size = New System.Drawing.Size(173, 26)
        Me.RemoveLoadToolStripMenuItem.Text = "Remove Load"
        '
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BendingMomentToolStripMenuItem, Me.ShearForceToolStripMenuItem, Me.DeflectionToolStripMenuItem, Me.SlopeToolStripMenuItem})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip3"
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(198, 108)
        '
        'BendingMomentToolStripMenuItem
        '
        Me.BendingMomentToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.bm
        Me.BendingMomentToolStripMenuItem.Name = "BendingMomentToolStripMenuItem"
        Me.BendingMomentToolStripMenuItem.Size = New System.Drawing.Size(197, 26)
        Me.BendingMomentToolStripMenuItem.Text = "Bending Moment"
        '
        'ShearForceToolStripMenuItem
        '
        Me.ShearForceToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.shear
        Me.ShearForceToolStripMenuItem.Name = "ShearForceToolStripMenuItem"
        Me.ShearForceToolStripMenuItem.Size = New System.Drawing.Size(197, 26)
        Me.ShearForceToolStripMenuItem.Text = "Shear Force"
        '
        'DeflectionToolStripMenuItem
        '
        Me.DeflectionToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.bend
        Me.DeflectionToolStripMenuItem.Name = "DeflectionToolStripMenuItem"
        Me.DeflectionToolStripMenuItem.Size = New System.Drawing.Size(197, 26)
        Me.DeflectionToolStripMenuItem.Text = "Deflection"
        '
        'SlopeToolStripMenuItem
        '
        Me.SlopeToolStripMenuItem.Image = Global.CBAnsDes.My.Resources.Resources.Slope
        Me.SlopeToolStripMenuItem.Name = "SlopeToolStripMenuItem"
        Me.SlopeToolStripMenuItem.Size = New System.Drawing.Size(197, 26)
        Me.SlopeToolStripMenuItem.Text = "Slope"
        '
        'res_coverpic
        '
        Me.res_coverpic.BackColor = System.Drawing.Color.Transparent
        Me.res_coverpic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.res_coverpic.Enabled = False
        Me.res_coverpic.Location = New System.Drawing.Point(0, 0)
        Me.res_coverpic.Name = "res_coverpic"
        Me.res_coverpic.Size = New System.Drawing.Size(261, 110)
        Me.res_coverpic.TabIndex = 0
        Me.res_coverpic.TabStop = False
        '
        'beamcreate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(988, 535)
        Me.Controls.Add(Me.coverpic)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(533, 369)
        Me.Name = "beamcreate"
        Me.Opacity = 0.9R
        Me.Text = "beamcreate"
        Me.coverpic.ResumeLayout(False)
        Me.respic.ResumeLayout(False)
        Me.mainpic.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ContextMenuStrip3.ResumeLayout(False)
        CType(Me.res_coverpic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents coverpic As System.Windows.Forms.Panel
    Friend WithEvents mainpic As System.Windows.Forms.Panel
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
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents res_coverpic As PictureBox
End Class
