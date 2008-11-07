<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewGame
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.lblNumOfPlayers = New System.Windows.Forms.Label
        Me._optNumOfPlayers_0 = New System.Windows.Forms.RadioButton
        Me._optNumOfPlayers_1 = New System.Windows.Forms.RadioButton
        Me.optNumOfPlayers = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.lblAIPlayers = New System.Windows.Forms.Label
        Me._chkAIPlayers_0 = New System.Windows.Forms.CheckBox
        Me._chkAIPlayers_1 = New System.Windows.Forms.CheckBox
        Me._chkAIPlayers_2 = New System.Windows.Forms.CheckBox
        Me._chkAIPlayers_3 = New System.Windows.Forms.CheckBox
        Me.cmdStart = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.chkAIPlayers = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.lblBoadDimension = New System.Windows.Forms.Label
        Me.numBoardDimension = New System.Windows.Forms.NumericUpDown
        Me.lblPlayerNames = New System.Windows.Forms.Label
        Me._txtPlayerNames_0 = New System.Windows.Forms.TextBox
        Me._txtPlayerNames_1 = New System.Windows.Forms.TextBox
        Me._txtPlayerNames_2 = New System.Windows.Forms.TextBox
        Me._txtPlayerNames_3 = New System.Windows.Forms.TextBox
        Me.txtPlayerNames = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        CType(Me.optNumOfPlayers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAIPlayers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numBoardDimension, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPlayerNames, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblNumOfPlayers
        '
        Me.lblNumOfPlayers.AutoSize = True
        Me.lblNumOfPlayers.Location = New System.Drawing.Point(12, 9)
        Me.lblNumOfPlayers.Name = "lblNumOfPlayers"
        Me.lblNumOfPlayers.Size = New System.Drawing.Size(95, 13)
        Me.lblNumOfPlayers.TabIndex = 0
        Me.lblNumOfPlayers.Text = "Number of players:"
        '
        '_optNumOfPlayers_0
        '
        Me._optNumOfPlayers_0.AutoSize = True
        Me._optNumOfPlayers_0.Checked = True
        Me.optNumOfPlayers.SetIndex(Me._optNumOfPlayers_0, CType(0, Short))
        Me._optNumOfPlayers_0.Location = New System.Drawing.Point(113, 7)
        Me._optNumOfPlayers_0.Name = "_optNumOfPlayers_0"
        Me._optNumOfPlayers_0.Size = New System.Drawing.Size(31, 17)
        Me._optNumOfPlayers_0.TabIndex = 1
        Me._optNumOfPlayers_0.TabStop = True
        Me._optNumOfPlayers_0.Text = "2"
        Me._optNumOfPlayers_0.UseVisualStyleBackColor = True
        '
        '_optNumOfPlayers_1
        '
        Me._optNumOfPlayers_1.AutoSize = True
        Me.optNumOfPlayers.SetIndex(Me._optNumOfPlayers_1, CType(1, Short))
        Me._optNumOfPlayers_1.Location = New System.Drawing.Point(150, 7)
        Me._optNumOfPlayers_1.Name = "_optNumOfPlayers_1"
        Me._optNumOfPlayers_1.Size = New System.Drawing.Size(31, 17)
        Me._optNumOfPlayers_1.TabIndex = 2
        Me._optNumOfPlayers_1.TabStop = True
        Me._optNumOfPlayers_1.Text = "4"
        Me._optNumOfPlayers_1.UseVisualStyleBackColor = True
        '
        'lblAIPlayers
        '
        Me.lblAIPlayers.AutoSize = True
        Me.lblAIPlayers.Location = New System.Drawing.Point(12, 40)
        Me.lblAIPlayers.Name = "lblAIPlayers"
        Me.lblAIPlayers.Size = New System.Drawing.Size(57, 13)
        Me.lblAIPlayers.TabIndex = 3
        Me.lblAIPlayers.Text = "AI Players:"
        '
        '_chkAIPlayers_0
        '
        Me._chkAIPlayers_0.AutoSize = True
        Me.chkAIPlayers.SetIndex(Me._chkAIPlayers_0, CType(0, Short))
        Me._chkAIPlayers_0.Location = New System.Drawing.Point(150, 40)
        Me._chkAIPlayers_0.Name = "_chkAIPlayers_0"
        Me._chkAIPlayers_0.Size = New System.Drawing.Size(32, 17)
        Me._chkAIPlayers_0.TabIndex = 4
        Me._chkAIPlayers_0.Text = "1"
        Me._chkAIPlayers_0.UseVisualStyleBackColor = True
        '
        '_chkAIPlayers_1
        '
        Me._chkAIPlayers_1.AutoSize = True
        Me._chkAIPlayers_1.Checked = True
        Me._chkAIPlayers_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAIPlayers.SetIndex(Me._chkAIPlayers_1, CType(1, Short))
        Me._chkAIPlayers_1.Location = New System.Drawing.Point(187, 63)
        Me._chkAIPlayers_1.Name = "_chkAIPlayers_1"
        Me._chkAIPlayers_1.Size = New System.Drawing.Size(32, 17)
        Me._chkAIPlayers_1.TabIndex = 5
        Me._chkAIPlayers_1.Text = "2"
        Me._chkAIPlayers_1.UseVisualStyleBackColor = True
        '
        '_chkAIPlayers_2
        '
        Me._chkAIPlayers_2.AutoSize = True
        Me._chkAIPlayers_2.Checked = True
        Me._chkAIPlayers_2.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkAIPlayers_2.Enabled = False
        Me.chkAIPlayers.SetIndex(Me._chkAIPlayers_2, CType(2, Short))
        Me._chkAIPlayers_2.Location = New System.Drawing.Point(150, 86)
        Me._chkAIPlayers_2.Name = "_chkAIPlayers_2"
        Me._chkAIPlayers_2.Size = New System.Drawing.Size(32, 17)
        Me._chkAIPlayers_2.TabIndex = 6
        Me._chkAIPlayers_2.Text = "3"
        Me._chkAIPlayers_2.UseVisualStyleBackColor = True
        '
        '_chkAIPlayers_3
        '
        Me._chkAIPlayers_3.AutoSize = True
        Me._chkAIPlayers_3.Checked = True
        Me._chkAIPlayers_3.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkAIPlayers_3.Enabled = False
        Me.chkAIPlayers.SetIndex(Me._chkAIPlayers_3, CType(3, Short))
        Me._chkAIPlayers_3.Location = New System.Drawing.Point(113, 63)
        Me._chkAIPlayers_3.Name = "_chkAIPlayers_3"
        Me._chkAIPlayers_3.Size = New System.Drawing.Size(32, 17)
        Me._chkAIPlayers_3.TabIndex = 7
        Me._chkAIPlayers_3.Text = "4"
        Me._chkAIPlayers_3.UseVisualStyleBackColor = True
        '
        'cmdStart
        '
        Me.cmdStart.Location = New System.Drawing.Point(150, 255)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(75, 23)
        Me.cmdStart.TabIndex = 8
        Me.cmdStart.Text = "Start"
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(69, 255)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 9
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'lblBoadDimension
        '
        Me.lblBoadDimension.AutoSize = True
        Me.lblBoadDimension.Location = New System.Drawing.Point(12, 224)
        Me.lblBoadDimension.Name = "lblBoadDimension"
        Me.lblBoadDimension.Size = New System.Drawing.Size(115, 13)
        Me.lblBoadDimension.TabIndex = 10
        Me.lblBoadDimension.Text = "Gameboard dimension:"
        '
        'numBoardDimension
        '
        Me.numBoardDimension.Location = New System.Drawing.Point(150, 222)
        Me.numBoardDimension.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.numBoardDimension.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numBoardDimension.Name = "numBoardDimension"
        Me.numBoardDimension.Size = New System.Drawing.Size(39, 20)
        Me.numBoardDimension.TabIndex = 12
        Me.numBoardDimension.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numBoardDimension.Value = New Decimal(New Integer() {9, 0, 0, 0})
        '
        'lblPlayerNames
        '
        Me.lblPlayerNames.AutoSize = True
        Me.lblPlayerNames.Location = New System.Drawing.Point(12, 116)
        Me.lblPlayerNames.Name = "lblPlayerNames"
        Me.lblPlayerNames.Size = New System.Drawing.Size(73, 13)
        Me.lblPlayerNames.TabIndex = 13
        Me.lblPlayerNames.Text = "Player names:"
        '
        '_txtPlayerNames_0
        '
        Me.txtPlayerNames.SetIndex(Me._txtPlayerNames_0, CType(0, Short))
        Me._txtPlayerNames_0.Location = New System.Drawing.Point(113, 113)
        Me._txtPlayerNames_0.Name = "_txtPlayerNames_0"
        Me._txtPlayerNames_0.Size = New System.Drawing.Size(106, 20)
        Me._txtPlayerNames_0.TabIndex = 14
        Me._txtPlayerNames_0.Text = "Player 1"
        '
        '_txtPlayerNames_1
        '
        Me._txtPlayerNames_1.Enabled = False
        Me.txtPlayerNames.SetIndex(Me._txtPlayerNames_1, CType(1, Short))
        Me._txtPlayerNames_1.Location = New System.Drawing.Point(113, 139)
        Me._txtPlayerNames_1.Name = "_txtPlayerNames_1"
        Me._txtPlayerNames_1.Size = New System.Drawing.Size(106, 20)
        Me._txtPlayerNames_1.TabIndex = 15
        Me._txtPlayerNames_1.Text = "Dennis (AI)"
        '
        '_txtPlayerNames_2
        '
        Me._txtPlayerNames_2.Enabled = False
        Me.txtPlayerNames.SetIndex(Me._txtPlayerNames_2, CType(2, Short))
        Me._txtPlayerNames_2.Location = New System.Drawing.Point(113, 165)
        Me._txtPlayerNames_2.Name = "_txtPlayerNames_2"
        Me._txtPlayerNames_2.Size = New System.Drawing.Size(106, 20)
        Me._txtPlayerNames_2.TabIndex = 16
        Me._txtPlayerNames_2.Text = "Ruben (AI)"
        '
        '_txtPlayerNames_3
        '
        Me._txtPlayerNames_3.Enabled = False
        Me.txtPlayerNames.SetIndex(Me._txtPlayerNames_3, CType(3, Short))
        Me._txtPlayerNames_3.Location = New System.Drawing.Point(113, 191)
        Me._txtPlayerNames_3.Name = "_txtPlayerNames_3"
        Me._txtPlayerNames_3.Size = New System.Drawing.Size(106, 20)
        Me._txtPlayerNames_3.TabIndex = 17
        Me._txtPlayerNames_3.Text = "Sebastian (AI)"
        '
        'frmNewGame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(231, 284)
        Me.Controls.Add(Me._txtPlayerNames_3)
        Me.Controls.Add(Me._txtPlayerNames_2)
        Me.Controls.Add(Me._txtPlayerNames_1)
        Me.Controls.Add(Me._txtPlayerNames_0)
        Me.Controls.Add(Me.lblPlayerNames)
        Me.Controls.Add(Me.numBoardDimension)
        Me.Controls.Add(Me.lblBoadDimension)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdStart)
        Me.Controls.Add(Me._chkAIPlayers_3)
        Me.Controls.Add(Me._chkAIPlayers_2)
        Me.Controls.Add(Me._chkAIPlayers_1)
        Me.Controls.Add(Me._chkAIPlayers_0)
        Me.Controls.Add(Me.lblAIPlayers)
        Me.Controls.Add(Me._optNumOfPlayers_1)
        Me.Controls.Add(Me._optNumOfPlayers_0)
        Me.Controls.Add(Me.lblNumOfPlayers)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNewGame"
        Me.Text = "New Game"
        CType(Me.optNumOfPlayers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAIPlayers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numBoardDimension, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPlayerNames, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblNumOfPlayers As System.Windows.Forms.Label
    Friend WithEvents _optNumOfPlayers_0 As System.Windows.Forms.RadioButton
    Friend WithEvents _optNumOfPlayers_1 As System.Windows.Forms.RadioButton
    Public WithEvents optNumOfPlayers As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Friend WithEvents lblAIPlayers As System.Windows.Forms.Label
    Friend WithEvents _chkAIPlayers_0 As System.Windows.Forms.CheckBox
    Friend WithEvents _chkAIPlayers_1 As System.Windows.Forms.CheckBox
    Friend WithEvents _chkAIPlayers_2 As System.Windows.Forms.CheckBox
    Friend WithEvents _chkAIPlayers_3 As System.Windows.Forms.CheckBox
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents chkAIPlayers As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
    Friend WithEvents lblBoadDimension As System.Windows.Forms.Label
    Friend WithEvents numBoardDimension As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPlayerNames As System.Windows.Forms.Label
    Friend WithEvents _txtPlayerNames_0 As System.Windows.Forms.TextBox
    Friend WithEvents _txtPlayerNames_1 As System.Windows.Forms.TextBox
    Friend WithEvents _txtPlayerNames_2 As System.Windows.Forms.TextBox
    Friend WithEvents _txtPlayerNames_3 As System.Windows.Forms.TextBox
    Public WithEvents txtPlayerNames As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
End Class
