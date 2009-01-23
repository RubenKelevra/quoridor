<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMainForm
#Region "Vom Windows Form-Designer generierter Code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'Dieser Aufruf ist für den Windows Form-Designer erforderlich.
		InitializeComponent()
	End Sub
	'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Wird vom Windows Form-Designer benötigt.
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents ddmNewGame As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents ddmLoadGame As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents ddmSaveGame As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents ddmLine1 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents ddmExit As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents ddmMenuGame As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mmuMain As System.Windows.Forms.MenuStrip
    Public WithEvents fraBoard As System.Windows.Forms.Panel
    Public WithEvents cmdCancelBrick As System.Windows.Forms.Button
    Public WithEvents _cmdMove_3 As System.Windows.Forms.Button
    Public WithEvents _cmdMove_0 As System.Windows.Forms.Button
    Public WithEvents cmdSetBrick As System.Windows.Forms.Button
    Public WithEvents cmdRotateBrick As System.Windows.Forms.Button
    Public WithEvents _cmdMove_1 As System.Windows.Forms.Button
    Public WithEvents _cmdMove_2 As System.Windows.Forms.Button
    Public WithEvents fraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblLoading As System.Windows.Forms.Label
    Public WithEvents lblBricksLeftNumber As System.Windows.Forms.Label
    Public WithEvents lblBricksLeftTxt As System.Windows.Forms.Label
    Public WithEvents lblCurPlayer As System.Windows.Forms.Label
    Public WithEvents shpCurrentPlayer As Microsoft.VisualBasic.PowerPacks.OvalShape
    Public WithEvents fraInfo As System.Windows.Forms.GroupBox
    Public WithEvents cmdMove As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Public WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Verändern mit dem Windows Form-Designer ist nicht möglich.
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer
        Me.shpCurrentPlayer = New Microsoft.VisualBasic.PowerPacks.OvalShape
        Me.mmuMain = New System.Windows.Forms.MenuStrip
        Me.ddmMenuGame = New System.Windows.Forms.ToolStripMenuItem
        Me.ddmNewGame = New System.Windows.Forms.ToolStripMenuItem
        Me.ddmLoadGame = New System.Windows.Forms.ToolStripMenuItem
        Me.ddmSaveGame = New System.Windows.Forms.ToolStripMenuItem
        Me.ddmLine1 = New System.Windows.Forms.ToolStripSeparator
        Me.ddmExit = New System.Windows.Forms.ToolStripMenuItem
        Me.fraBoard = New System.Windows.Forms.Panel
        Me.fraMovement = New System.Windows.Forms.GroupBox
        Me.cmdCancelBrick = New System.Windows.Forms.Button
        Me._cmdMove_3 = New System.Windows.Forms.Button
        Me._cmdMove_0 = New System.Windows.Forms.Button
        Me.cmdSetBrick = New System.Windows.Forms.Button
        Me.cmdRotateBrick = New System.Windows.Forms.Button
        Me._cmdMove_1 = New System.Windows.Forms.Button
        Me._cmdMove_2 = New System.Windows.Forms.Button
        Me.fraInfo = New System.Windows.Forms.GroupBox
        Me.lblLoading = New System.Windows.Forms.Label
        Me.lblBricksLeftNumber = New System.Windows.Forms.Label
        Me.lblBricksLeftTxt = New System.Windows.Forms.Label
        Me.lblCurPlayer = New System.Windows.Forms.Label
        Me.cmdMove = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.mmuMain.SuspendLayout()
        Me.fraMovement.SuspendLayout()
        Me.fraInfo.SuspendLayout()
        CType(Me.cmdMove, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 13)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.shpCurrentPlayer})
        Me.ShapeContainer1.Size = New System.Drawing.Size(257, 92)
        Me.ShapeContainer1.TabIndex = 15
        Me.ShapeContainer1.TabStop = False
        '
        'shpCurrentPlayer
        '
        Me.shpCurrentPlayer.BackColor = System.Drawing.Color.White
        Me.shpCurrentPlayer.BorderColor = System.Drawing.SystemColors.WindowText
        Me.shpCurrentPlayer.FillColor = System.Drawing.Color.Black
        Me.shpCurrentPlayer.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.shpCurrentPlayer.Location = New System.Drawing.Point(152, 19)
        Me.shpCurrentPlayer.Name = "shpCurrentPlayer"
        Me.shpCurrentPlayer.Size = New System.Drawing.Size(25, 25)
        Me.shpCurrentPlayer.Visible = False
        '
        'mmuMain
        '
        Me.mmuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ddmMenuGame})
        Me.mmuMain.Location = New System.Drawing.Point(0, 0)
        Me.mmuMain.Name = "mmuMain"
        Me.mmuMain.Size = New System.Drawing.Size(549, 24)
        Me.mmuMain.TabIndex = 13
        '
        'ddmMenuGame
        '
        Me.ddmMenuGame.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ddmNewGame, Me.ddmLoadGame, Me.ddmSaveGame, Me.ddmLine1, Me.ddmExit})
        Me.ddmMenuGame.Name = "ddmMenuGame"
        Me.ddmMenuGame.Size = New System.Drawing.Size(46, 20)
        Me.ddmMenuGame.Text = "Game"
        '
        'ddmNewGame
        '
        Me.ddmNewGame.Name = "ddmNewGame"
        Me.ddmNewGame.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.ddmNewGame.Size = New System.Drawing.Size(181, 22)
        Me.ddmNewGame.Text = "&New Game"
        '
        'ddmLoadGame
        '
        Me.ddmLoadGame.Enabled = False
        Me.ddmLoadGame.Name = "ddmLoadGame"
        Me.ddmLoadGame.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.ddmLoadGame.Size = New System.Drawing.Size(181, 22)
        Me.ddmLoadGame.Text = "L&oad Game"
        '
        'ddmSaveGame
        '
        Me.ddmSaveGame.Enabled = False
        Me.ddmSaveGame.Name = "ddmSaveGame"
        Me.ddmSaveGame.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.ddmSaveGame.Size = New System.Drawing.Size(181, 22)
        Me.ddmSaveGame.Text = "&Save Game"
        '
        'ddmLine1
        '
        Me.ddmLine1.Name = "ddmLine1"
        Me.ddmLine1.Size = New System.Drawing.Size(178, 6)
        '
        'ddmExit
        '
        Me.ddmExit.Name = "ddmExit"
        Me.ddmExit.Size = New System.Drawing.Size(181, 22)
        Me.ddmExit.Text = "&Exit"
        '
        'fraBoard
        '
        Me.fraBoard.BackColor = System.Drawing.SystemColors.Control
        Me.fraBoard.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraBoard.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBoard.Location = New System.Drawing.Point(12, 36)
        Me.fraBoard.Name = "fraBoard"
        Me.fraBoard.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBoard.Size = New System.Drawing.Size(249, 249)
        Me.fraBoard.TabIndex = 12
        Me.fraBoard.Text = "Gameboard"
        Me.fraBoard.Visible = False
        '
        'fraMovement
        '
        Me.fraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.fraMovement.Controls.Add(Me.cmdCancelBrick)
        Me.fraMovement.Controls.Add(Me._cmdMove_3)
        Me.fraMovement.Controls.Add(Me._cmdMove_0)
        Me.fraMovement.Controls.Add(Me.cmdSetBrick)
        Me.fraMovement.Controls.Add(Me.cmdRotateBrick)
        Me.fraMovement.Controls.Add(Me._cmdMove_1)
        Me.fraMovement.Controls.Add(Me._cmdMove_2)
        Me.fraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraMovement.Location = New System.Drawing.Point(280, 147)
        Me.fraMovement.Name = "fraMovement"
        Me.fraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMovement.Size = New System.Drawing.Size(257, 137)
        Me.fraMovement.TabIndex = 5
        Me.fraMovement.TabStop = False
        Me.fraMovement.Text = "Movement"
        '
        'cmdCancelBrick
        '
        Me.cmdCancelBrick.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancelBrick.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancelBrick.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancelBrick.Location = New System.Drawing.Point(8, 96)
        Me.cmdCancelBrick.Name = "cmdCancelBrick"
        Me.cmdCancelBrick.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancelBrick.Size = New System.Drawing.Size(97, 33)
        Me.cmdCancelBrick.TabIndex = 13
        Me.cmdCancelBrick.TabStop = False
        Me.cmdCancelBrick.Text = "cancel brick"
        Me.cmdCancelBrick.UseVisualStyleBackColor = True
        '
        '_cmdMove_3
        '
        Me._cmdMove_3.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMove_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMove_3.Enabled = False
        Me._cmdMove_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMove.SetIndex(Me._cmdMove_3, CType(3, Short))
        Me._cmdMove_3.Location = New System.Drawing.Point(120, 52)
        Me._cmdMove_3.Name = "_cmdMove_3"
        Me._cmdMove_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMove_3.Size = New System.Drawing.Size(33, 33)
        Me._cmdMove_3.TabIndex = 11
        Me._cmdMove_3.TabStop = False
        Me._cmdMove_3.Text = "< -"
        Me._cmdMove_3.UseVisualStyleBackColor = True
        '
        '_cmdMove_0
        '
        Me._cmdMove_0.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMove_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMove_0.Enabled = False
        Me._cmdMove_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMove.SetIndex(Me._cmdMove_0, CType(0, Short))
        Me._cmdMove_0.Location = New System.Drawing.Point(160, 72)
        Me._cmdMove_0.Name = "_cmdMove_0"
        Me._cmdMove_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMove_0.Size = New System.Drawing.Size(33, 33)
        Me._cmdMove_0.TabIndex = 10
        Me._cmdMove_0.TabStop = False
        Me._cmdMove_0.Text = "\l/"
        Me._cmdMove_0.UseVisualStyleBackColor = True
        '
        'cmdSetBrick
        '
        Me.cmdSetBrick.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetBrick.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetBrick.Enabled = False
        Me.cmdSetBrick.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetBrick.Location = New System.Drawing.Point(8, 32)
        Me.cmdSetBrick.Name = "cmdSetBrick"
        Me.cmdSetBrick.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetBrick.Size = New System.Drawing.Size(97, 33)
        Me.cmdSetBrick.TabIndex = 9
        Me.cmdSetBrick.TabStop = False
        Me.cmdSetBrick.Text = "set brick"
        Me.cmdSetBrick.UseVisualStyleBackColor = True
        '
        'cmdRotateBrick
        '
        Me.cmdRotateBrick.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRotateBrick.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRotateBrick.Enabled = False
        Me.cmdRotateBrick.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRotateBrick.Location = New System.Drawing.Point(8, 64)
        Me.cmdRotateBrick.Name = "cmdRotateBrick"
        Me.cmdRotateBrick.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRotateBrick.Size = New System.Drawing.Size(97, 33)
        Me.cmdRotateBrick.TabIndex = 8
        Me.cmdRotateBrick.TabStop = False
        Me.cmdRotateBrick.Text = "rotate brick"
        Me.cmdRotateBrick.UseVisualStyleBackColor = True
        '
        '_cmdMove_1
        '
        Me._cmdMove_1.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMove_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMove_1.Enabled = False
        Me._cmdMove_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMove.SetIndex(Me._cmdMove_1, CType(1, Short))
        Me._cmdMove_1.Location = New System.Drawing.Point(200, 52)
        Me._cmdMove_1.Name = "_cmdMove_1"
        Me._cmdMove_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMove_1.Size = New System.Drawing.Size(33, 33)
        Me._cmdMove_1.TabIndex = 7
        Me._cmdMove_1.TabStop = False
        Me._cmdMove_1.Text = "- >"
        Me._cmdMove_1.UseVisualStyleBackColor = True
        '
        '_cmdMove_2
        '
        Me._cmdMove_2.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMove_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMove_2.Enabled = False
        Me._cmdMove_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMove.SetIndex(Me._cmdMove_2, CType(2, Short))
        Me._cmdMove_2.Location = New System.Drawing.Point(160, 32)
        Me._cmdMove_2.Name = "_cmdMove_2"
        Me._cmdMove_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMove_2.Size = New System.Drawing.Size(33, 33)
        Me._cmdMove_2.TabIndex = 6
        Me._cmdMove_2.TabStop = False
        Me._cmdMove_2.Text = "/l\"
        Me._cmdMove_2.UseVisualStyleBackColor = True
        '
        'fraInfo
        '
        Me.fraInfo.BackColor = System.Drawing.SystemColors.Control
        Me.fraInfo.Controls.Add(Me.lblLoading)
        Me.fraInfo.Controls.Add(Me.lblBricksLeftNumber)
        Me.fraInfo.Controls.Add(Me.lblBricksLeftTxt)
        Me.fraInfo.Controls.Add(Me.lblCurPlayer)
        Me.fraInfo.Controls.Add(Me.ShapeContainer1)
        Me.fraInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraInfo.Location = New System.Drawing.Point(280, 32)
        Me.fraInfo.Name = "fraInfo"
        Me.fraInfo.Padding = New System.Windows.Forms.Padding(0)
        Me.fraInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraInfo.Size = New System.Drawing.Size(257, 105)
        Me.fraInfo.TabIndex = 1
        Me.fraInfo.TabStop = False
        Me.fraInfo.Text = "Information"
        '
        'lblLoading
        '
        Me.lblLoading.BackColor = System.Drawing.SystemColors.Control
        Me.lblLoading.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLoading.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLoading.Location = New System.Drawing.Point(184, 32)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLoading.Size = New System.Drawing.Size(65, 17)
        Me.lblLoading.TabIndex = 14
        Me.lblLoading.Text = "loading..."
        Me.lblLoading.Visible = False
        '
        'lblBricksLeftNumber
        '
        Me.lblBricksLeftNumber.BackColor = System.Drawing.SystemColors.Control
        Me.lblBricksLeftNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBricksLeftNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBricksLeftNumber.Location = New System.Drawing.Point(152, 64)
        Me.lblBricksLeftNumber.Name = "lblBricksLeftNumber"
        Me.lblBricksLeftNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBricksLeftNumber.Size = New System.Drawing.Size(25, 25)
        Me.lblBricksLeftNumber.TabIndex = 4
        Me.lblBricksLeftNumber.Text = "88"
        Me.lblBricksLeftNumber.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblBricksLeftNumber.Visible = False
        '
        'lblBricksLeftTxt
        '
        Me.lblBricksLeftTxt.BackColor = System.Drawing.SystemColors.Control
        Me.lblBricksLeftTxt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBricksLeftTxt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBricksLeftTxt.Location = New System.Drawing.Point(8, 64)
        Me.lblBricksLeftTxt.Name = "lblBricksLeftTxt"
        Me.lblBricksLeftTxt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBricksLeftTxt.Size = New System.Drawing.Size(129, 25)
        Me.lblBricksLeftTxt.TabIndex = 3
        Me.lblBricksLeftTxt.Text = "Bricks Left:"
        '
        'lblCurPlayer
        '
        Me.lblCurPlayer.BackColor = System.Drawing.SystemColors.Control
        Me.lblCurPlayer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCurPlayer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCurPlayer.Location = New System.Drawing.Point(8, 32)
        Me.lblCurPlayer.Name = "lblCurPlayer"
        Me.lblCurPlayer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCurPlayer.Size = New System.Drawing.Size(129, 25)
        Me.lblCurPlayer.TabIndex = 2
        Me.lblCurPlayer.Text = "Current Player:"
        '
        'cmdMove
        '
        '
        'frmMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(549, 296)
        Me.Controls.Add(Me.fraBoard)
        Me.Controls.Add(Me.fraMovement)
        Me.Controls.Add(Me.fraInfo)
        Me.Controls.Add(Me.mmuMain)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(10, 56)
        Me.MaximizeBox = False
        Me.Name = "frmMainForm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Quoridor"
        Me.mmuMain.ResumeLayout(False)
        Me.mmuMain.PerformLayout()
        Me.fraMovement.ResumeLayout(False)
        Me.fraInfo.ResumeLayout(False)
        CType(Me.cmdMove, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class