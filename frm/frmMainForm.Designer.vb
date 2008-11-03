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
	Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
	Public WithEvents fraBoard As System.Windows.Forms.Panel
	Public WithEvents cmdCancelBrick As System.Windows.Forms.Button
	Public WithEvents _cmdMove_3 As System.Windows.Forms.Button
	Public WithEvents _cmdMove_0 As System.Windows.Forms.Button
	Public WithEvents cmdSetBrick As System.Windows.Forms.Button
	Public WithEvents cmdRotateBrick As System.Windows.Forms.Button
	Public WithEvents _cmdMove_1 As System.Windows.Forms.Button
	Public WithEvents _cmdMove_2 As System.Windows.Forms.Button
	Public WithEvents fraMovement As System.Windows.Forms.GroupBox
	Public WithEvents picFocus As System.Windows.Forms.PictureBox
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
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMainForm))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer
		Me.MainMenu1 = New System.Windows.Forms.MenuStrip
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
		Me.picFocus = New System.Windows.Forms.PictureBox
		Me.lblLoading = New System.Windows.Forms.Label
		Me.lblBricksLeftNumber = New System.Windows.Forms.Label
		Me.lblBricksLeftTxt = New System.Windows.Forms.Label
		Me.lblCurPlayer = New System.Windows.Forms.Label
		Me.shpCurrentPlayer = New Microsoft.VisualBasic.PowerPacks.OvalShape
		Me.cmdMove = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components)
		Me.MainMenu1.SuspendLayout()
		Me.fraMovement.SuspendLayout()
		Me.fraInfo.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.cmdMove, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "Quoridor"
		Me.ClientSize = New System.Drawing.Size(549, 296)
		Me.Location = New System.Drawing.Point(10, 56)
		Me.MaximizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmMainForm"
		Me.ddmMenuGame.Name = "ddmMenuGame"
		Me.ddmMenuGame.Text = "Game"
		Me.ddmMenuGame.Checked = False
		Me.ddmMenuGame.Enabled = True
		Me.ddmMenuGame.Visible = True
		Me.ddmNewGame.Name = "ddmNewGame"
		Me.ddmNewGame.Text = "&New Game"
		Me.ddmNewGame.ShortcutKeys = CType(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys)
		Me.ddmNewGame.Checked = False
		Me.ddmNewGame.Enabled = True
		Me.ddmNewGame.Visible = True
		Me.ddmLoadGame.Name = "ddmLoadGame"
		Me.ddmLoadGame.Text = "L&oad Game"
		Me.ddmLoadGame.Enabled = False
		Me.ddmLoadGame.ShortcutKeys = CType(System.Windows.Forms.Keys.Control or System.Windows.Forms.Keys.O, System.Windows.Forms.Keys)
		Me.ddmLoadGame.Checked = False
		Me.ddmLoadGame.Visible = True
		Me.ddmSaveGame.Name = "ddmSaveGame"
		Me.ddmSaveGame.Text = "&Save Game"
		Me.ddmSaveGame.Enabled = False
		Me.ddmSaveGame.ShortcutKeys = CType(System.Windows.Forms.Keys.Control or System.Windows.Forms.Keys.S, System.Windows.Forms.Keys)
		Me.ddmSaveGame.Checked = False
		Me.ddmSaveGame.Visible = True
		Me.ddmLine1.Enabled = True
		Me.ddmLine1.Visible = True
		Me.ddmLine1.Name = "ddmLine1"
		Me.ddmExit.Name = "ddmExit"
		Me.ddmExit.Text = "&Exit"
		Me.ddmExit.Checked = False
		Me.ddmExit.Enabled = True
		Me.ddmExit.Visible = True
		Me.fraBoard.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.fraBoard.Text = "Gameboard"
		Me.fraBoard.Size = New System.Drawing.Size(249, 249)
		Me.fraBoard.Location = New System.Drawing.Point(12, 36)
		Me.fraBoard.TabIndex = 12
		Me.fraBoard.Visible = False
		Me.fraBoard.BackColor = System.Drawing.SystemColors.Control
		Me.fraBoard.Enabled = True
		Me.fraBoard.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraBoard.Cursor = System.Windows.Forms.Cursors.Default
		Me.fraBoard.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraBoard.Name = "fraBoard"
		Me.fraMovement.Text = "Movement"
		Me.fraMovement.Size = New System.Drawing.Size(257, 137)
		Me.fraMovement.Location = New System.Drawing.Point(280, 147)
		Me.fraMovement.TabIndex = 5
		Me.fraMovement.BackColor = System.Drawing.SystemColors.Control
		Me.fraMovement.Enabled = True
		Me.fraMovement.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraMovement.Visible = True
		Me.fraMovement.Padding = New System.Windows.Forms.Padding(0)
		Me.fraMovement.Name = "fraMovement"
		Me.cmdCancelBrick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdCancelBrick.Text = "cancel brick"
		Me.cmdCancelBrick.Enabled = False
		Me.cmdCancelBrick.Size = New System.Drawing.Size(97, 33)
		Me.cmdCancelBrick.Location = New System.Drawing.Point(8, 96)
		Me.cmdCancelBrick.TabIndex = 13
		Me.cmdCancelBrick.TabStop = False
		Me.cmdCancelBrick.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancelBrick.CausesValidation = True
		Me.cmdCancelBrick.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancelBrick.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancelBrick.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancelBrick.Name = "cmdCancelBrick"
		Me._cmdMove_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._cmdMove_3.Text = "< -"
		Me._cmdMove_3.Enabled = False
		Me._cmdMove_3.Size = New System.Drawing.Size(33, 33)
		Me._cmdMove_3.Location = New System.Drawing.Point(120, 52)
		Me._cmdMove_3.TabIndex = 11
		Me._cmdMove_3.TabStop = False
		Me._cmdMove_3.BackColor = System.Drawing.SystemColors.Control
		Me._cmdMove_3.CausesValidation = True
		Me._cmdMove_3.ForeColor = System.Drawing.SystemColors.ControlText
		Me._cmdMove_3.Cursor = System.Windows.Forms.Cursors.Default
		Me._cmdMove_3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cmdMove_3.Name = "_cmdMove_3"
		Me._cmdMove_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._cmdMove_0.Text = "\l/"
		Me._cmdMove_0.Enabled = False
		Me._cmdMove_0.Size = New System.Drawing.Size(33, 33)
		Me._cmdMove_0.Location = New System.Drawing.Point(160, 72)
		Me._cmdMove_0.TabIndex = 10
		Me._cmdMove_0.TabStop = False
		Me._cmdMove_0.BackColor = System.Drawing.SystemColors.Control
		Me._cmdMove_0.CausesValidation = True
		Me._cmdMove_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._cmdMove_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._cmdMove_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cmdMove_0.Name = "_cmdMove_0"
		Me.cmdSetBrick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdSetBrick.Text = "set brick"
		Me.cmdSetBrick.Enabled = False
		Me.cmdSetBrick.Size = New System.Drawing.Size(97, 33)
		Me.cmdSetBrick.Location = New System.Drawing.Point(8, 32)
		Me.cmdSetBrick.TabIndex = 9
		Me.cmdSetBrick.TabStop = False
		Me.cmdSetBrick.BackColor = System.Drawing.SystemColors.Control
		Me.cmdSetBrick.CausesValidation = True
		Me.cmdSetBrick.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSetBrick.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSetBrick.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSetBrick.Name = "cmdSetBrick"
		Me.cmdRotateBrick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdRotateBrick.Text = "rotate brick"
		Me.cmdRotateBrick.Enabled = False
		Me.cmdRotateBrick.Size = New System.Drawing.Size(97, 33)
		Me.cmdRotateBrick.Location = New System.Drawing.Point(8, 64)
		Me.cmdRotateBrick.TabIndex = 8
		Me.cmdRotateBrick.TabStop = False
		Me.cmdRotateBrick.BackColor = System.Drawing.SystemColors.Control
		Me.cmdRotateBrick.CausesValidation = True
		Me.cmdRotateBrick.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdRotateBrick.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdRotateBrick.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdRotateBrick.Name = "cmdRotateBrick"
		Me._cmdMove_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._cmdMove_1.Text = "- >"
		Me._cmdMove_1.Enabled = False
		Me._cmdMove_1.Size = New System.Drawing.Size(33, 33)
		Me._cmdMove_1.Location = New System.Drawing.Point(200, 52)
		Me._cmdMove_1.TabIndex = 7
		Me._cmdMove_1.TabStop = False
		Me._cmdMove_1.BackColor = System.Drawing.SystemColors.Control
		Me._cmdMove_1.CausesValidation = True
		Me._cmdMove_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._cmdMove_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._cmdMove_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cmdMove_1.Name = "_cmdMove_1"
		Me._cmdMove_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._cmdMove_2.Text = "/l\"
		Me._cmdMove_2.Enabled = False
		Me._cmdMove_2.Size = New System.Drawing.Size(33, 33)
		Me._cmdMove_2.Location = New System.Drawing.Point(160, 32)
		Me._cmdMove_2.TabIndex = 6
		Me._cmdMove_2.TabStop = False
		Me._cmdMove_2.BackColor = System.Drawing.SystemColors.Control
		Me._cmdMove_2.CausesValidation = True
		Me._cmdMove_2.ForeColor = System.Drawing.SystemColors.ControlText
		Me._cmdMove_2.Cursor = System.Windows.Forms.Cursors.Default
		Me._cmdMove_2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cmdMove_2.Name = "_cmdMove_2"
		Me.fraInfo.Text = "Information"
		Me.fraInfo.Size = New System.Drawing.Size(257, 105)
		Me.fraInfo.Location = New System.Drawing.Point(280, 32)
		Me.fraInfo.TabIndex = 1
		Me.fraInfo.BackColor = System.Drawing.SystemColors.Control
		Me.fraInfo.Enabled = True
		Me.fraInfo.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraInfo.Visible = True
		Me.fraInfo.Padding = New System.Windows.Forms.Padding(0)
		Me.fraInfo.Name = "fraInfo"
		Me.picFocus.BackColor = System.Drawing.Color.Black
		Me.picFocus.Size = New System.Drawing.Size(25, 25)
		Me.picFocus.Location = New System.Drawing.Point(224, 72)
		Me.picFocus.TabIndex = 0
		Me.picFocus.TabStop = False
		Me.picFocus.Dock = System.Windows.Forms.DockStyle.None
		Me.picFocus.CausesValidation = True
		Me.picFocus.Enabled = True
		Me.picFocus.ForeColor = System.Drawing.SystemColors.ControlText
		Me.picFocus.Cursor = System.Windows.Forms.Cursors.Default
		Me.picFocus.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picFocus.Visible = True
		Me.picFocus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.picFocus.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.picFocus.Name = "picFocus"
		Me.lblLoading.Text = "loading..."
		Me.lblLoading.Size = New System.Drawing.Size(65, 17)
		Me.lblLoading.Location = New System.Drawing.Point(184, 32)
		Me.lblLoading.TabIndex = 14
		Me.lblLoading.Visible = False
		Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblLoading.BackColor = System.Drawing.SystemColors.Control
		Me.lblLoading.Enabled = True
		Me.lblLoading.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblLoading.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblLoading.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblLoading.UseMnemonic = True
		Me.lblLoading.AutoSize = False
		Me.lblLoading.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblLoading.Name = "lblLoading"
		Me.lblBricksLeftNumber.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblBricksLeftNumber.Text = "88"
		Me.lblBricksLeftNumber.Size = New System.Drawing.Size(25, 25)
		Me.lblBricksLeftNumber.Location = New System.Drawing.Point(152, 64)
		Me.lblBricksLeftNumber.TabIndex = 4
		Me.lblBricksLeftNumber.Visible = False
		Me.lblBricksLeftNumber.BackColor = System.Drawing.SystemColors.Control
		Me.lblBricksLeftNumber.Enabled = True
		Me.lblBricksLeftNumber.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblBricksLeftNumber.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblBricksLeftNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblBricksLeftNumber.UseMnemonic = True
		Me.lblBricksLeftNumber.AutoSize = False
		Me.lblBricksLeftNumber.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblBricksLeftNumber.Name = "lblBricksLeftNumber"
		Me.lblBricksLeftTxt.Text = "Bricks Left:"
		Me.lblBricksLeftTxt.Size = New System.Drawing.Size(129, 25)
		Me.lblBricksLeftTxt.Location = New System.Drawing.Point(8, 64)
		Me.lblBricksLeftTxt.TabIndex = 3
		Me.lblBricksLeftTxt.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblBricksLeftTxt.BackColor = System.Drawing.SystemColors.Control
		Me.lblBricksLeftTxt.Enabled = True
		Me.lblBricksLeftTxt.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblBricksLeftTxt.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblBricksLeftTxt.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblBricksLeftTxt.UseMnemonic = True
		Me.lblBricksLeftTxt.Visible = True
		Me.lblBricksLeftTxt.AutoSize = False
		Me.lblBricksLeftTxt.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblBricksLeftTxt.Name = "lblBricksLeftTxt"
		Me.lblCurPlayer.Text = "Current Player:"
		Me.lblCurPlayer.Size = New System.Drawing.Size(129, 25)
		Me.lblCurPlayer.Location = New System.Drawing.Point(8, 32)
		Me.lblCurPlayer.TabIndex = 2
		Me.lblCurPlayer.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblCurPlayer.BackColor = System.Drawing.SystemColors.Control
		Me.lblCurPlayer.Enabled = True
		Me.lblCurPlayer.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblCurPlayer.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblCurPlayer.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblCurPlayer.UseMnemonic = True
		Me.lblCurPlayer.Visible = True
		Me.lblCurPlayer.AutoSize = False
		Me.lblCurPlayer.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblCurPlayer.Name = "lblCurPlayer"
		Me.shpCurrentPlayer.BackColor = System.Drawing.Color.White
		Me.shpCurrentPlayer.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
		Me.shpCurrentPlayer.Size = New System.Drawing.Size(25, 25)
		Me.shpCurrentPlayer.Location = New System.Drawing.Point(152, 19)
		Me.shpCurrentPlayer.Visible = False
		Me.shpCurrentPlayer.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent
		Me.shpCurrentPlayer.BorderColor = System.Drawing.SystemColors.WindowText
		Me.shpCurrentPlayer.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid
		Me.shpCurrentPlayer.BorderWidth = 1
		Me.shpCurrentPlayer.FillColor = System.Drawing.Color.Black
		Me.shpCurrentPlayer.Name = "shpCurrentPlayer"
		Me.shpCurrentPlayer.Bounds = New Rectangle(152, 19, 25, 25)
		Me.Controls.Add(fraBoard)
		Me.Controls.Add(fraMovement)
		Me.Controls.Add(fraInfo)
		Me.fraMovement.Controls.Add(cmdCancelBrick)
		Me.fraMovement.Controls.Add(_cmdMove_3)
		Me.fraMovement.Controls.Add(_cmdMove_0)
		Me.fraMovement.Controls.Add(cmdSetBrick)
		Me.fraMovement.Controls.Add(cmdRotateBrick)
		Me.fraMovement.Controls.Add(_cmdMove_1)
		Me.fraMovement.Controls.Add(_cmdMove_2)
		Me.fraInfo.Controls.Add(picFocus)
		Me.fraInfo.Controls.Add(lblLoading)
		Me.fraInfo.Controls.Add(lblBricksLeftNumber)
		Me.fraInfo.Controls.Add(lblBricksLeftTxt)
		Me.fraInfo.Controls.Add(lblCurPlayer)
		Me.ShapeContainer1.Shapes.Add(shpCurrentPlayer)
		Me.fraInfo.Controls.Add(ShapeContainer1)
		Me.cmdMove.SetIndex(_cmdMove_3, CType(3, Short))
		Me.cmdMove.SetIndex(_cmdMove_0, CType(0, Short))
		Me.cmdMove.SetIndex(_cmdMove_1, CType(1, Short))
		Me.cmdMove.SetIndex(_cmdMove_2, CType(2, Short))
		CType(Me.cmdMove, System.ComponentModel.ISupportInitialize).EndInit()
		MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me.ddmMenuGame})
		ddmMenuGame.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.ddmNewGame, Me.ddmLoadGame, Me.ddmSaveGame, Me.ddmLine1, Me.ddmExit})
		Me.Controls.Add(MainMenu1)
		Me.MainMenu1.ResumeLayout(False)
		Me.fraMovement.ResumeLayout(False)
		Me.fraInfo.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class