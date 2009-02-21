Public Class frmJump

    Private Sub cmdLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLeft.Click
        Call frmMainForm.setDirection(3)
        Me.Close()
    End Sub

    Private Sub cmdUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUp.Click
        Call frmMainForm.setDirection(2)
        Me.Close()
    End Sub

    Private Sub cmdRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRight.Click
        Call frmMainForm.setDirection(1)
        Me.Close()
    End Sub

    Private Sub cmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDown.Click
        Call frmMainForm.setDirection(0)
        Me.Close()
    End Sub

End Class