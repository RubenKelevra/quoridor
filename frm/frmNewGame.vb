Public Class frmNewGame

    ' frm/frmMainForm.frm - Part of Quoridor http://code.google.com/p/quoridor/
    '
    ' Copyright (C) 2008 Quoridor VB-Project Team
    '
    ' This program is free software; you can redistribute it and/or modify it
    ' under the terms of the GNU General Public License as published by the
    ' Free Software Foundation; either version 3 of the License, or (at your
    ' option) any later version.
    '
    ' This program is distributed in the hope that it will be useful, but
    ' WITHOUT ANY WARRANTY; without even the impliefd warranty of
    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General
    ' Public License for more details.
    '
    ' You should have received a copy of the GNU General Public License along
    ' with this program; if not, see <http://www.gnu.org/licenses/>.

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        ' cancel new game
        frmMainForm.setRunGame(False)
        Me.Hide()

    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click

        ' dec
        Dim bAIPlayers() As Boolean
        Dim i As Byte
        Dim BNumOfPlayers As Byte
        Dim sPlayerNames() As String

        ' set number of players
        If Me.optNumOfPlayers(0).Checked Then
            BNumOfPlayers = 1
        Else
            BNumOfPlayers = 3
        End If

        ' set arrays and number of players in the main formular
        Call frmMainForm.setNumOfPlayers(BNumOfPlayers)
        Call frmMainForm.setDimOfPlayers(BNumOfPlayers)
        Call frmMainForm.setDimOfNames(BNumOfPlayers)
        ReDim bAIPlayers(BNumOfPlayers)
        ReDim sPlayerNames(BNumOfPlayers)

        ' set AI players
        For i = Me.chkAIPlayers.LBound To BNumOfPlayers
            bAIPlayers(i) = Me.chkAIPlayers(i).Checked
        Next i
        Call frmMainForm.setPlayers(bAIPlayers)

        ' set player names
        For i = Me.txtPlayerNames.LBound To BNumOfPlayers
            sPlayerNames(i) = Me.txtPlayerNames(i).Text
        Next
        Call frmMainForm.setPlayerNames(sPlayerNames)

        ' set board dimension (x=y)
        Call frmMainForm.setBoardDimension(Me.numBoardDimension.Value - 1)

        ' close formular
        frmMainForm.setRunGame(True)
        Me.Hide()

    End Sub

    Private Sub optNumOfPlayers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNumOfPlayers.CheckedChanged

        ' dec
        Dim i As Integer
        Dim j As Integer
        Dim iPlayers As Byte

        ' disable / enable checkBox for setting human / AI players
        For i = Me.optNumOfPlayers.LBound To Me.optNumOfPlayers.UBound
            If Me.optNumOfPlayers(i).Checked Then
                iPlayers = CInt(Me.optNumOfPlayers(i).Text)
                For j = Me.chkAIPlayers.UBound To iPlayers Step -1
                    Me.chkAIPlayers(j).Enabled = False
                    Me.txtPlayerNames(j).Enabled = False
                Next
                For j = j To Me.chkAIPlayers.LBound Step -1
                    Me.chkAIPlayers(j).Enabled = True
                Next
            End If
        Next

    End Sub

    Private Sub chkAIPlayers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAIPlayers.CheckedChanged

        ' dec
        Dim i As Integer
        Dim sName(3) As String

        ' init
        sName(0) = "Martin (AI)"
        sName(1) = "Dennis (AI)"
        sName(2) = "Ruben (AI)"
        sName(3) = "Sebastian (AI)"

        ' disable / enable setting player names (and 
        For i = Me.chkAIPlayers.LBound To Me.chkAIPlayers.UBound
            If Me.chkAIPlayers(i).Checked Then
                Me.txtPlayerNames(i).Text = sName(i)
                Me.txtPlayerNames(i).Enabled = False
            Else
                Me.txtPlayerNames(i).Enabled = True
                Me.txtPlayerNames(i).Text = "Player " & i + 1
            End If
        Next

    End Sub

End Class