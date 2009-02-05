<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmJump
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
        Me.cmdUp = New System.Windows.Forms.Button
        Me.cmdDown = New System.Windows.Forms.Button
        Me.cmdLeft = New System.Windows.Forms.Button
        Me.cmdRight = New System.Windows.Forms.Button
        Me.lblJump = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdUp
        '
        Me.cmdUp.Location = New System.Drawing.Point(112, 34)
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.Size = New System.Drawing.Size(33, 33)
        Me.cmdUp.TabIndex = 0
        Me.cmdUp.Text = "/|\"
        Me.cmdUp.UseVisualStyleBackColor = True
        '
        'cmdDown
        '
        Me.cmdDown.Location = New System.Drawing.Point(112, 93)
        Me.cmdDown.Name = "cmdDown"
        Me.cmdDown.Size = New System.Drawing.Size(33, 33)
        Me.cmdDown.TabIndex = 1
        Me.cmdDown.Text = "\|/"
        Me.cmdDown.UseVisualStyleBackColor = True
        '
        'cmdLeft
        '
        Me.cmdLeft.Location = New System.Drawing.Point(73, 62)
        Me.cmdLeft.Name = "cmdLeft"
        Me.cmdLeft.Size = New System.Drawing.Size(33, 33)
        Me.cmdLeft.TabIndex = 2
        Me.cmdLeft.Text = "<-"
        Me.cmdLeft.UseVisualStyleBackColor = True
        '
        'cmdRight
        '
        Me.cmdRight.Location = New System.Drawing.Point(151, 62)
        Me.cmdRight.Name = "cmdRight"
        Me.cmdRight.Size = New System.Drawing.Size(33, 33)
        Me.cmdRight.TabIndex = 3
        Me.cmdRight.Text = "->"
        Me.cmdRight.UseVisualStyleBackColor = True
        '
        'lblJump
        '
        Me.lblJump.AutoSize = True
        Me.lblJump.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJump.Location = New System.Drawing.Point(12, 9)
        Me.lblJump.Name = "lblJump"
        Me.lblJump.Size = New System.Drawing.Size(228, 20)
        Me.lblJump.TabIndex = 4
        Me.lblJump.Text = "Where do you want to jump to?"
        '
        'jump
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(254, 139)
        Me.Controls.Add(Me.lblJump)
        Me.Controls.Add(Me.cmdRight)
        Me.Controls.Add(Me.cmdLeft)
        Me.Controls.Add(Me.cmdDown)
        Me.Controls.Add(Me.cmdUp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "jump"
        Me.Text = "Jump"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmdDown As System.Windows.Forms.Button
    Friend WithEvents cmdLeft As System.Windows.Forms.Button
    Friend WithEvents cmdRight As System.Windows.Forms.Button
    Friend WithEvents lblJump As System.Windows.Forms.Label
End Class
