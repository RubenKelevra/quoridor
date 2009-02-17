Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.PowerPacks
Friend Class frmMainForm

    Inherits System.Windows.Forms.Form

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

    'gamedata

    Private Playground As clsBoard

    'formdata
    Private bAIPlayers() As Boolean
    Private bRunGame As Boolean
    Private bGameEnabled As Boolean
    Private bKeyUp As Boolean
    Private BBoardDimension As Byte
    Private BNumOfPlayers As Byte
    Private BDirection As Byte
    Private tTempBrick As clsBrick
    'Private GBoard As Graphics
    Private PiStartCoords As Point
    Private PfFieldsize As PointF
    Private PfBricksize As PointF
    Private sPlayerNames() As String

    Public Sub setDirection(ByVal B As Byte)
        BDirection = B
    End Sub

    Public Sub setRunGame(ByVal b As Boolean)

        bRunGame = b

    End Sub

    Public Function getNumOfPlayers() As Boolean

        getNumOfPlayers = BNumOfPlayers

    End Function

    Public Sub setDimOfPlayers(ByVal B As Byte)

        ReDim bAIPlayers(B)

    End Sub

    Public Sub setPlayers(ByVal b() As Boolean)

        bAIPlayers = b

    End Sub

    Public Sub setNumOfPlayers(ByVal B As Byte)

        BNumOfPlayers = B

    End Sub

    Public Sub setBoardDimension(ByVal B As Byte)

        BBoardDimension = B

    End Sub

    Public Sub setPlayerNames(ByVal s() As String)

        sPlayerNames = s

    End Sub

    Public Sub setDimOfNames(ByVal B As Byte)

        ReDim sPlayerNames(B)

    End Sub

    Private Sub cmdCancelBrick_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancelBrick.Click

        ' reset bricks
        Call resetBrickMode()

        ' set focus to frmMainForm for keyboard control
        Call Me.Focus()

        ' repaint form
        Call paintForm()

    End Sub

    Private Sub cmdCancelBrick_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdCancelBrick.MouseUp

        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        ' set focus to frmMainForm for keyboard control
        Call Me.Focus()

    End Sub

    Private Sub cmdMove_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMove.Click
        Dim Index As Short = cmdMove.GetIndex(eventSender)

        ' dec
        Static bGotError As Boolean
        Dim changed As Boolean
        Dim BDirection As Byte
        changed = False
        bGotError = False
        'If bGotError Then
        '    'cmdMove_Click(New System.Object, New System.EventArgs)
        '    changed = Playground.movePlayer(Playground.getActivePlayer(), CByte(Index))
        'End If

        If Not tTempBrick.Placed Then 'move figure

            On Error GoTo jump
            changed = Playground.movePlayer(Playground.getActivePlayer, CByte(Index))
            GoTo doNext

jump:
            On Error GoTo 0
            Call frmJump.ShowDialog(Me)
            changed = Playground.movePlayer(Playground.getActivePlayer, BDirection)

doNext:

        Else

            Select Case Index

                ' move down
                Case 0
                    If tTempBrick.Position.Y < Playground.getDimension - 1 Then
                        tTempBrick.Position.Y = tTempBrick.Position.Y + 1
                        changed = True
                    End If

                    ' move right
                Case 1
                    If tTempBrick.Position.X < Playground.getDimension - 1 Then
                        tTempBrick.Position.X = tTempBrick.Position.X + 1
                        changed = True
                    End If

                    ' move up
                Case 2
                    If tTempBrick.Position.Y > 0 Then
                        tTempBrick.Position.Y = tTempBrick.Position.Y - 1
                        changed = True
                    End If

                    ' move left
                Case 3
                    If tTempBrick.Position.X > 0 Then
                        tTempBrick.Position.X = tTempBrick.Position.X - 1
                        changed = True
                    End If

            End Select

        End If

        ' set focus to picFocus for keyboard control
        Call Me.Focus()

        If changed Then
            If Not tTempBrick.Placed Then
                If Playground.NextTurn Then
                    'repaint form after change to next player
                    'Call frmMainForm_Paint(Me, New System.Windows.Forms.PaintEventArgs(Nothing, Nothing))
                    Call paintForm()
                    'next move
                    Playground.doPlayerMove()
                End If
            End If
            'repaint after AI/network move OR after change to next player
            'Call frmMainForm_Paint(Me, New System.Windows.Forms.PaintEventArgs(Nothing, Nothing))
            Call paintForm()

        End If

    End Sub

    Private Sub cmdMove_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdMove.MouseUp
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        Dim Index As Short = cmdMove.GetIndex(eventSender)

        ' set focus to picFocus for keyboard control
        Call Me.Focus()

    End Sub

    Private Sub cmdRotateBrick_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRotateBrick.Click

        If Me.cmdRotateBrick.Enabled Then

            ' switches rotation variable
            tTempBrick.Horizontal = Not tTempBrick.Horizontal

            ' set focus to picFocus for keyboard control
            Call Me.Focus()

            ' repaint form
            Call paintForm()

        End If

    End Sub

    Private Sub cmdRotateBrick_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdRotateBrick.MouseUp
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        ' set focus to picFocus for keyboard control
        Call Me.Focus()

    End Sub

    Private Sub cmdSetBrick_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSetBrick.Click
        Dim bStoneSaved As Boolean
        bStoneSaved = False
        If Playground.getRemainingPlayerBricks(Playground.getActivePlayer) <= 0 Then
            Exit Sub
        End If

        If tTempBrick.Placed Then
            ' save brick
            Select Case Playground.saveWall(tTempBrick, Playground.getActivePlayer)

                Case 0
                    bStoneSaved = True

                Case 1
                    ' should not happen anyway
                    MsgBox("You haven't got any stones left, so you can't place one.", MsgBoxStyle.OkOnly, "No Stones Left")

                Case 2
                    ' just exit the sub without popping up a msg window
                    ' MsgBox "On this position you can't place a stone.", vbOKOnly, "Stone Not Placeable"
                    Exit Sub

                Case 3
                    MsgBox("Internal Application Error" & vbCrLf & "Error No. 15" & vbCrLf & "Press OK to continue", MsgBoxStyle.Critical, "Internal Application Error")

            End Select

            ' reset bricks
            Call resetBrickMode()

        Else

            ' set new caption
            Me.cmdSetBrick.Text = "OK?"

            ' enable brick options
            Me.cmdRotateBrick.Enabled = True
            Me.cmdCancelBrick.Enabled = True
            tTempBrick.Placed = True

        End If

        ' set focus to picFocus for keyboard control
        Call Me.Focus()

        If bStoneSaved Then
            If Playground.NextTurn Then
                'repaint form after stone is placed
                Call paintForm()
                'next move
                Playground.doPlayerMove()
            End If
        End If

        'repaint after AI/network move or draw stone after change to set stone mode
        Call paintForm()

    End Sub

    Private Sub cmdSetBrick_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdSetBrick.MouseUp
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        ' set focus to picFocus for keyboard control
        Call Me.Focus()

    End Sub

    Public Sub ddmExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ddmExit.Click

        ' exit programm
        End

    End Sub

    Public Sub ddmNewGame_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ddmNewGame.Click

        Dim i As Integer

        ' open setting window for new games
        Call frmNewGame.ShowDialog()
        If Not bRunGame Then

            Exit Sub

        End If

        ' init board
        Playground = New clsBoard
        Call Playground.create(BNumOfPlayers, BBoardDimension) ' player, number of fields (x=y)
        Me.shpCurrentPlayer.FillColor = Playground.getPlayerColor(0) 'init current player marker

        Call sizeBoard()

        ' init jumping buttons
        'For i = cmdChooseDir.LBound() To cmdChooseDir.UBound()
        'Next

        ' starting positions
        PiStartCoords.X = Me.fraBoard.Left
        PiStartCoords.Y = Me.fraBoard.Top

        ' init graphic
        'GBoard = Me.CreateGraphics
        'GBoard.Clear(Me.BackColor)

        ' init brick
        Call resetBrickMode()

        ' init other vars
        Me.lblLoading.Visible = False
        bKeyUp = True

        ' enable GUI
        If Not bGameEnabled Then

            Me.lblBricksLeftNumber.Visible = True
            Me.shpCurrentPlayer.Visible = True
            Me.cmdMove(0).Enabled = True
            Me.cmdMove(1).Enabled = True
            Me.cmdMove(2).Enabled = True
            Me.cmdMove(3).Enabled = True
            Me.lblLoading.Visible = False

        End If

        ' draw
        bGameEnabled = True
        Call paintForm()

    End Sub

    Private Sub frmMainForm_ClientSizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ClientSizeChanged

        Me.fraInfo.Left = Me.Width - Me.fraInfo.Width - 18

        Me.fraMovement.Left = Me.Width - Me.fraMovement.Width - 18
        Me.fraMovement.Top = Me.fraInfo.Top + Me.fraInfo.Height + 10

        Me.fraBoard.Width = Me.fraInfo.Left - Me.fraBoard.Left - 6
        Me.fraBoard.Height = Me.Height - Me.fraBoard.Top - 45

        'GBoard.Clear(Me.BackColor)

        Call sizeBoard()
        Call paintForm()

    End Sub

    Private Sub frmMainForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        ' select movement keys

        If bKeyUp Then

            Select Case e.KeyValue

                ' move down
                Case System.Windows.Forms.Keys.Down
                    Call cmdMove_Click(cmdMove.Item(0), New System.EventArgs())

                    ' move right
                Case System.Windows.Forms.Keys.Right
                    Call cmdMove_Click(cmdMove.Item(1), New System.EventArgs())

                    ' move up
                Case System.Windows.Forms.Keys.Up
                    Call cmdMove_Click(cmdMove.Item(2), New System.EventArgs())

                    ' move left
                Case System.Windows.Forms.Keys.Left
                    Call cmdMove_Click(cmdMove.Item(3), New System.EventArgs())

                    ' set brick
                Case System.Windows.Forms.Keys.Return
                    Call cmdSetBrick_Click(cmdSetBrick, New System.EventArgs())

                    ' rotate brick
                Case System.Windows.Forms.Keys.Space
                    Call cmdRotateBrick_Click(cmdRotateBrick, New System.EventArgs())

                    ' cancel brick mode
                Case System.Windows.Forms.Keys.C
                    Call cmdCancelBrick_Click(cmdCancelBrick, New System.EventArgs())

            End Select

            bKeyUp = False

        End If

    End Sub

    Private Sub frmMainForm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        ' on keyUp, set var to true

        bKeyUp = True

    End Sub

    Private Sub frmMainForm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'GBoard = Me.CreateGraphics
        tTempBrick = New clsBrick

    End Sub

    Private Sub frmMainForm_Paint(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint

        ' sets current color of the active figure
        'Call paintForm()
        If bGameEnabled Then

            Call drawBoard()
            Call drawBricks()

        End If

    End Sub

    Private Sub deactSetBrick()
        ' deactivate placing button

        If tTempBrick.Placed Then

            cmdSetBrick.Enabled = Playground.checkPlaceWall(tTempBrick.Position.X, tTempBrick.Position.Y, (tTempBrick.Horizontal))

        End If

    End Sub

    Private Sub setCurFigureColor()

        ' set current player marker
        Me.shpCurrentPlayer.FillColor = Playground.getPlayerColor(Playground.getActivePlayer)

    End Sub

    Private Sub setBricksLeft()

        Dim b As Byte

        ' set current ammount of brick
        b = Playground.getRemainingPlayerBricks(Playground.getActivePlayer)

        ' update player bricks
        Me.lblBricksLeftNumber.Text = CStr(b)

        If b = 0 Then
            ' deactivate brick button
            Me.cmdSetBrick.Enabled = False
        Else
            ' activate brick button
            Me.cmdSetBrick.Enabled = True
        End If

    End Sub

    Private Sub deactMoveButtons()

        ' dec
        Dim BActPlayer As Byte
        Dim i As Short

        If tTempBrick.Placed Then

            ' deactivates buttons which indicates not possible directions
            For i = 0 To 3

                Me.cmdMove(i).Enabled = True

                Select Case i

                    ' down
                    Case 0
                        If tTempBrick.Position.Y >= Playground.getDimension - 1 Then
                            Me.cmdMove(i).Enabled = False
                        End If

                        ' right
                    Case 1
                        If tTempBrick.Position.X >= Playground.getDimension - 1 Then
                            Me.cmdMove(i).Enabled = False
                        End If

                        ' up
                    Case 2
                        If tTempBrick.Position.Y = 0 Then
                            Me.cmdMove(i).Enabled = False
                        End If

                        ' left
                    Case 3
                        If tTempBrick.Position.X = 0 Then
                            Me.cmdMove(i).Enabled = False
                        End If

                End Select

                cmdMove(i).Font = VB6.FontChangeBold(cmdMove(i).Font, False)

            Next i

        Else

            ' init
            BActPlayer = Playground.getActivePlayer

            ' deactivate buttons which indicates not posssible directions
            For i = 0 To 3
                cmdMove(i).Enabled = Playground.checkMove(Playground.getPlayerLocation(BActPlayer), i)
                cmdMove(i).Font = VB6.FontChangeBold(cmdMove(i).Font, Playground.getPlayerTarget(BActPlayer) = i)
            Next i

        End If

    End Sub

    Private Sub drawBoard()
        'Private drawBoard
        'draws board

        ' declaration
        Static BDimension As Byte
        Static i As Byte
        Static x As Byte
        Static y As Byte
        Static GBoard As Graphics
        Static colorPen As System.Drawing.Color = Color.Black ' default board color
        Static playerLocation() As Position
        Static PfCurPosition As PointF
        Static rect(,) As RectangleF

        Static BlastDimensions As Byte = Byte.MaxValue
        Static BlastPlayerNo As Byte = Byte.MaxValue

        ' init

        'check if we need to change dimension-values from field
        BDimension = Playground.getDimension
        If Not BlastDimensions = BDimension Then
            ReDim rect(BDimension, BDimension)
            BlastDimensions = BDimension
        End If

        x = Playground.getNoOfPlayer
        If Not BlastPlayerNo = x Then
            ReDim playerLocation(x)
            BlastPlayerNo = x
        End If

        GBoard = Me.CreateGraphics()
        For i = LBound(playerLocation) To UBound(playerLocation) - LBound(playerLocation)
            playerLocation(i) = Playground.getPlayerLocation(i)
        Next i

        For x = 0 To BDimension
            PfCurPosition.X = PiStartCoords.X + x * PfFieldsize.X + x * PfBricksize.X

            For y = 0 To BDimension
                PfCurPosition.Y = PiStartCoords.Y + y * PfFieldsize.Y + y * PfBricksize.Y

                rect(x, y) = New RectangleF(PfCurPosition.X, PfCurPosition.Y, PfFieldsize.X, PfFieldsize.Y)

                ' check current position with the position of all players
                For i = LBound(playerLocation) To UBound(playerLocation) - LBound(playerLocation)
                    If comparePos(xy2position(x, y), playerLocation(i)) Then ' set playercolor
                        colorPen = Playground.getPlayerColor(i)
                        Exit For
                    Else
                        colorPen = Color.Black ' default board color
                    End If
                Next i
                Call GBoard.FillRectangle(New SolidBrush(colorPen), rect(x, y))
            Next y
        Next x
    End Sub

    Public Sub drawBricks()
        ' draws the bricks between the board

        Static BDimension As Byte
        Static i As Byte
        Static x As Byte
        Static y As Byte
        Static cCurColor As System.Drawing.Color
        Static GBoard As Graphics
        Static PfCurPosition As PointF
        Static rect(,) As RectangleF
        Static mySavedBricks() As clsBrick
        Static horz As Byte

        Static BlastDimension As Byte

        ' init
        BDimension = Playground.getDimension
        If Not BlastDimension = BDimension Then
            ReDim rect(BDimension, BDimension - 1)
        End If
        mySavedBricks = Playground.getWalls
        GBoard = Me.CreateGraphics()

        ' horizontal
        For horz = 0 To 1
            For x = 0 To BDimension
                For y = 0 To BDimension
                    If (horz And x = BDimension) Or (Not horz And y = BDimension) Then
                        Exit For
                    End If
                    ' --- calc current coords ---

                    ' set x frame position
                    PfCurPosition.X = PiStartCoords.X
                    ' set next position
                    'If horz Then
                    'PfCurPosition.X = PfCurPosition.X + x * PfFieldsize.X
                    'Else
                    PfCurPosition.X = PfCurPosition.X + (x + 1) * PfFieldsize.X
                    'End If
                    ' set empty space
                    PfCurPosition.X = PfCurPosition.X + x * PfBricksize.X

                    ' set y frame position
                    PfCurPosition.Y = PiStartCoords.Y
                    ' set next position
                    'If horz Then
                    PfCurPosition.Y = PfCurPosition.Y + (y + 1) * PfFieldsize.Y
                    'Else
                    'PfCurPosition.Y = PfCurPosition.Y + y * PfFieldsize.Y
                    'End If
                    ' set empty space
                    PfCurPosition.Y = PfCurPosition.Y + y * PfBricksize.Y

                    cCurColor = Me.BackColor

                    For i = LBound(mySavedBricks) To UBound(mySavedBricks) - LBound(mySavedBricks)
                        ' if brick is not set
                        If mySavedBricks(i).Placed = False Or comparePos(xy2position(255, 255), mySavedBricks(i).Position) Then
                            Exit For
                        End If
                        ' saved brick
                        If horz = mySavedBricks(i).Horizontal And (comparePos(xy2position(x, y), mySavedBricks(i).Position) Or comparePos(xy2position(x, y), xy2position(mySavedBricks(i).Position.X, mySavedBricks(i).Position.Y + 1))) Then
                            cCurColor = Color.DarkOrange
                        End If
                    Next i

                    If horz = tTempBrick.Horizontal And tTempBrick.Placed And (comparePos(xy2position(x, y), tTempBrick.Position) Or (comparePos(xy2position(x, y), xy2position(tTempBrick.Position.X, tTempBrick.Position.Y + 1)))) Then
                        cCurColor = Color.CornflowerBlue
                    End If

                    rect(x, y) = New RectangleF(PfCurPosition.X, PfCurPosition.Y, PfBricksize.X, PfFieldsize.Y)
                    'draw brick
                    Call GBoard.FillRectangle(New SolidBrush(cCurColor), rect(x, y))
                Next y
            Next x
        Next horz
    End Sub

    Private Sub resetBrickMode()

        ' reset caption
        Me.cmdSetBrick.Text = "set brick"

        ' reset brick options
        tTempBrick.Horizontal = True
        tTempBrick.Placed = False
        tTempBrick.Position.X = 0
        tTempBrick.Position.Y = 0

        ' reset buttons
        Me.cmdSetBrick.Enabled = True
        Me.cmdRotateBrick.Enabled = False
        Me.cmdCancelBrick.Enabled = False

    End Sub

    Public Sub setLoadingLabel()

        ' (re-)set loading label
        If Playground.getPlayerType(Playground.getActivePlayer()) = 1 Then

            Me.lblLoading.Visible = True

        Else

            Me.lblLoading.Visible = False

        End If

    End Sub

    Private Sub paintForm()
        ' paints all variable graphics on the form
        ' return values:
        ' none

        If bGameEnabled Then

            Call setCurFigureColor()
            Call setBricksLeft()
            Call deactMoveButtons()
            Call deactSetBrick()
            Call setLoadingLabel()
            Call drawBoard()
            Call drawBricks()

        End If

    End Sub

    Public Sub sizeBoard()
        Static GBoard As Graphics = Me.CreateGraphics()
        'clean old board
        Call GBoard.FillRectangle(New SolidBrush(Color.FromArgb(226, 226, 226)), New Rectangle(0, 0, Me.Width, Me.Height))

        ' max. fieldsize
        PfFieldsize.X = Me.fraBoard.Width / (BBoardDimension + 1)
        PfFieldsize.Y = Me.fraBoard.Height / (BBoardDimension + 1)

        ' max. bricksize
        PfBricksize.X = PfFieldsize.X / BBoardDimension
        PfBricksize.Y = PfFieldsize.Y / BBoardDimension

        ' real fieldsize ( minus bricks )
        PfFieldsize.X = PfFieldsize.X - PfBricksize.X
        PfFieldsize.Y = PfFieldsize.Y - PfBricksize.Y

        ' real bricksize ( fit to "screen" )
        PfBricksize.X = PfBricksize.X * (1 + 1 / BBoardDimension)
        PfBricksize.Y = PfBricksize.Y * (1 + 1 / BBoardDimension)
    End Sub

End Class