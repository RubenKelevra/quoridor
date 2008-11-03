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
	Private bKeyUp As Boolean
	Private bGameEnabled As Boolean
	Private iFieldsize As Short
	Private iBricksize As Short
	Private iDrawStartX As Short
	Private iDrawStartY As Short
	Private lBoardcolor As Integer
	'UPGRADE_WARNING: Arrays in Struktur tTempBrick m�ssen m�glicherweise initialisiert werden, bevor sie verwendet werden k�nnen. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
	Private tTempBrick As CustomTypes.Brick
	
	Private Sub cmdCancelBrick_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancelBrick.Click
		
		' reset bricks
		Call resetBrickMode()
		
		' set focus to picFocus for keyboard control
		Me.picFocus.Focus()
		
		' repaint form
		Call frmMainForm_Paint(Me, New System.Windows.Forms.PaintEventArgs(Nothing, Nothing))
		
	End Sub
	
	Private Sub cmdCancelBrick_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdCancelBrick.MouseUp
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		' set focus to picFocus for keyboard control
		Me.picFocus.Focus()
		
	End Sub
	
	Private Sub cmdMove_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMove.Click
		Dim Index As Short = cmdMove.GetIndex(eventSender)
		
		' dec
		Dim changed As Boolean
		changed = False
		
		If Not tTempBrick.Placed Then 'move figure
			changed = Playground.movePlayer(Playground.getActivePlayer, CByte(Index))
			
		Else
			
			Select Case Index
				
				' move down
				Case 0
					If tTempBrick.Position(1) < Playground.getDimension - 1 Then
						tTempBrick.Position(1) = tTempBrick.Position(1) + 1
						changed = True
					End If
					
					' move right
				Case 1
					If tTempBrick.Position(0) < Playground.getDimension - 1 Then
						tTempBrick.Position(0) = tTempBrick.Position(0) + 1
						changed = True
					End If
					
					' move up
				Case 2
					If tTempBrick.Position(1) > 0 Then
						tTempBrick.Position(1) = tTempBrick.Position(1) - 1
						changed = True
					End If
					
					' move left
				Case 3
					If tTempBrick.Position(0) > 0 Then
						tTempBrick.Position(0) = tTempBrick.Position(0) - 1
						changed = True
					End If
					
			End Select
			
		End If
		
		' set focus to picFocus for keyboard control
		Me.picFocus.Focus()
		
		If changed Then
			If Not tTempBrick.Placed Then
				If Playground.NextTurn Then
					'repaint form after change to next player
					Call frmMainForm_Paint(Me, New System.Windows.Forms.PaintEventArgs(Nothing, Nothing))
					'next move
					Playground.doPlayerMove()
				End If
			End If
			'repaint after AI/network move OR after change to next player
			Call frmMainForm_Paint(Me, New System.Windows.Forms.PaintEventArgs(Nothing, Nothing))
		End If
		
	End Sub
	
	Private Sub cmdMove_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdMove.MouseUp
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		Dim Index As Short = cmdMove.GetIndex(eventSender)
		
		' set focus to picFocus for keyboard control
		Me.picFocus.Focus()
		
	End Sub
	
	Private Sub cmdRotateBrick_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRotateBrick.Click
		
		If Me.cmdRotateBrick.Enabled Then
			
			' switches rotation variable
			tTempBrick.Landscape = Not tTempBrick.Landscape
			
			' set focus to picFocus for keyboard control
			Me.picFocus.Focus()
			
			' repaint form
			Call frmMainForm_Paint(Me, New System.Windows.Forms.PaintEventArgs(Nothing, Nothing))
			
		End If
		
	End Sub
	
	Private Sub cmdRotateBrick_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdRotateBrick.MouseUp
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		' set focus to picFocus for keyboard control
		Me.picFocus.Focus()
		
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
					MsgBox("You haven't got any stones left, so you can't place one.", MsgBoxStyle.OKOnly, "No Stones Left")
					
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
		Me.picFocus.Focus()
		
		If bStoneSaved Then
			If Playground.NextTurn Then
				'repaint form after stone is placed
				Call frmMainForm_Paint(Me, New System.Windows.Forms.PaintEventArgs(Nothing, Nothing))
				'next move
				Playground.doPlayerMove()
			End If
		End If
		'repaint after AI/network move or draw stone after change to set stone mode
		Call frmMainForm_Paint(Me, New System.Windows.Forms.PaintEventArgs(Nothing, Nothing))
	End Sub
	
	Private Sub cmdSetBrick_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdSetBrick.MouseUp
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		' set focus to picFocus for keyboard control
		Me.picFocus.Focus()
		
	End Sub
	
	Public Sub ddmExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ddmExit.Click
		
		' exit programm
		End
		
	End Sub
	
	Public Sub ddmNewGame_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ddmNewGame.Click
		
		' init board
		Playground = New clsBoard
		Call Playground.create(1, 8) 'player, fields dimension (x=y)
		Me.shpCurrentPlayer.FillColor = System.Drawing.ColorTranslator.FromOle(Playground.getPlayerColor(0)) 'init current player marker
		
		' init drawing coords
		iDrawStartX = VB6.PixelsToTwipsX(Me.fraBoard.Left)
		iDrawStartY = VB6.PixelsToTwipsY(Me.fraBoard.Top)
		
		iFieldsize = (VB6.PixelsToTwipsY(Me.fraBoard.Height) + VB6.PixelsToTwipsX(Me.fraBoard.Width)) / (9 + 9)
		iBricksize = iFieldsize / 9
		
		' init colors
		lBoardcolor = RGB(0, 0, 0)
		
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
		Call frmMainForm_Paint(Me, New System.Windows.Forms.PaintEventArgs(Nothing, Nothing))
		
	End Sub
	
	Private Sub frmMainForm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		' hide picture box
		Me.picFocus.BackColor = Me.BackColor
		
	End Sub
	
	Private Sub frmMainForm_Paint(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
		
		' sets current color of the active figure
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
	
	Private Sub deactSetBrick()
		' deactivate placing button
		
		If tTempBrick.Placed Then
			
			cmdSetBrick.Enabled = Playground.checkPlaceWall(tTempBrick.Position(0), tTempBrick.Position(1), (tTempBrick.Landscape))
			
		End If
		
	End Sub
	
	Private Sub setCurFigureColor()
		
		' set current player marker
		Me.shpCurrentPlayer.FillColor = System.Drawing.ColorTranslator.FromOle(Playground.getPlayerColor(Playground.getActivePlayer))
		
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
						If tTempBrick.Position(1) >= Playground.getDimension - 1 Then
							Me.cmdMove(i).Enabled = False
						End If
						
						' right
					Case 1
						If tTempBrick.Position(0) >= Playground.getDimension - 1 Then
							Me.cmdMove(i).Enabled = False
						End If
						
						' up
					Case 2
						If tTempBrick.Position(1) = 0 Then
							Me.cmdMove(i).Enabled = False
						End If
						
						' left
					Case 3
						If tTempBrick.Position(0) = 0 Then
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
		' draws the fields on which a player can move
		
		' dec
		Dim BDimension As Byte
		Dim i As Byte
		Dim x As Byte
		Dim y As Byte
		Dim iCurX As Short
		Dim iCurY As Short
		Dim lCurColor As Integer
		'UPGRADE_WARNING: Arrays in Struktur tDrawPos m�ssen m�glicherweise initialisiert werden, bevor sie verwendet werden k�nnen. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim tDrawPos As CustomTypes.Position
		'UPGRADE_WARNING: Arrays in Struktur tPlayerPos m�ssen m�glicherweise initialisiert werden, bevor sie verwendet werden k�nnen. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim tPlayerPos As CustomTypes.Position
		
		' save dimension
		BDimension = Playground.getDimension
		
		For x = 0 To BDimension
			For y = 0 To BDimension
				
				' init color
				lCurColor = lBoardcolor
				
				' calc current coords
				iCurX = iDrawStartX + x * iFieldsize
				iCurY = iDrawStartY + y * iFieldsize
				
				' check current position with the position of all players
				For i = 0 To Playground.getNoOfPlayer
					
					'UPGRADE_WARNING: Die Standardeigenschaft des Objekts tDrawPos konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tDrawPos = xy2pos(x, y)
					'UPGRADE_WARNING: Die Standardeigenschaft des Objekts tPlayerPos konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tPlayerPos = Playground.getPlayerLocation(i)
					
					If Not comparePos(xy2pos(255, 255), tPlayerPos) And comparePos(tDrawPos, tPlayerPos) Then
						
						' set playercolor
						lCurColor = Playground.getPlayerColor(i)
						
					End If
					
				Next i
				
				' draw lines
				'UPGRADE_ISSUE: Form Methode frmMainForm.Line wurde nicht aktualisiert. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
                'Me.Line (iCurX, iCurY) - (iCurX + iFieldsize - iBricksize, iCurY + iFieldsize - iBricksize), lCurColor, BF
				
			Next y
		Next x
		
	End Sub
	
	Public Sub drawBricks()
		' draws the bricks between the board
		
		Dim i As Short
		Dim x As Short
		Dim y As Short
		Dim iCurX As Short
		Dim iCurY As Short
		Dim lCurColor As Integer
		Dim tSavedBrick() As CustomTypes.Brick
		
		ReDim tSavedBrick(UBound(Playground.getWalls) - LBound(Playground.getWalls))
		tSavedBrick = VB6.CopyArray(Playground.getWalls)
		
		' horizontal
		For x = 0 To 8
			For y = 0 To 7
				
				' init color
				lCurColor = System.Drawing.ColorTranslator.ToOle(Me.BackColor)
				
				' calc current coords
				iCurX = iDrawStartX + x * iFieldsize
				iCurY = iDrawStartY + (y + 1) * iFieldsize - iBricksize
				
				For i = LBound(tSavedBrick) To UBound(tSavedBrick)
					
					' if brick is not set
					If tSavedBrick(i).Placed = False Or tSavedBrick(i).Position(0) = 255 Or tSavedBrick(i).Position(1) = 255 Then
						Exit For
					End If
					
					' saved brick
					If tSavedBrick(i).Landscape And ((x = tSavedBrick(i).Position(0) And y = tSavedBrick(i).Position(1)) Or (x = tSavedBrick(i).Position(0) + 1 And y = tSavedBrick(i).Position(1))) Then
						
						lCurColor = RGB(255, 255, 0)
						
					End If
					
				Next i
				
				' temp brick
				If tTempBrick.Placed And tTempBrick.Landscape And ((x = tTempBrick.Position(0) And y = tTempBrick.Position(1)) Or (x = tTempBrick.Position(0) + 1 And y = tTempBrick.Position(1))) Then
					
					lCurColor = RGB(0, 192, 0)
					
				End If
				
				' draw bricks
				'UPGRADE_ISSUE: Form Methode frmMainForm.Line wurde nicht aktualisiert. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
                'Me.Line (iCurX, iCurY) - (iCurX + iFieldsize - iBricksize, iCurY + iBricksize), lCurColor, BF
				
			Next y
		Next x
		
		' vertical
		For x = 0 To 7
			For y = 0 To 8
				
				' init color
				lCurColor = System.Drawing.ColorTranslator.ToOle(Me.BackColor)
				
				' calc current coords
				iCurX = iDrawStartX + (x + 1) * iFieldsize - iBricksize
				iCurY = iDrawStartY + y * iFieldsize
				
				For i = LBound(tSavedBrick) To UBound(tSavedBrick)
					
					' if brick is not set
					If tSavedBrick(i).Placed = False Or tSavedBrick(i).Position(0) = 255 Or tSavedBrick(i).Position(1) = 255 Then
						Exit For
					End If
					
					' saved brick
					If Not tSavedBrick(i).Landscape And ((x = tSavedBrick(i).Position(0) And y = tSavedBrick(i).Position(1)) Or (x = tSavedBrick(i).Position(0) And y = tSavedBrick(i).Position(1) + 1)) Then
						
						lCurColor = RGB(255, 255, 0)
						
					End If
					
				Next i
				
				' temp brick
				If tTempBrick.Placed And Not tTempBrick.Landscape And ((x = tTempBrick.Position(0) And y = tTempBrick.Position(1)) Or (x = tTempBrick.Position(0) And y = tTempBrick.Position(1) + 1)) Then
					
					lCurColor = RGB(0, 192, 0)
					
				End If
				
				' draw bricks
				'UPGRADE_ISSUE: Form Methode frmMainForm.Line wurde nicht aktualisiert. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
                'Me.Line (iCurX, iCurY) - (iCurX + iBricksize, iCurY + iFieldsize - iBricksize), lCurColor, BF
				
			Next y
		Next x
		
	End Sub
	
	Private Sub resetBrickMode()
		
		' reset caption
		Me.cmdSetBrick.Text = "set brick"
		
		' reset brick options
		tTempBrick.Landscape = True
		tTempBrick.Placed = False
		tTempBrick.Position(0) = 0
		tTempBrick.Position(1) = 0
		
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
	
	'UPGRADE_ISSUE: Das PictureBox-Ereignis "picFocus.KeyDown" wurde nicht aktualisiert. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
	Private Sub picFocus_KeyDown(ByRef KeyCode As Short, ByRef Shift As Short)
		' select movement keys
		
		If bKeyUp Then
			
			Select Case KeyCode
				
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
	
	'UPGRADE_ISSUE: Das PictureBox-Ereignis "picFocus.KeyUp" wurde nicht aktualisiert. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
	Private Sub picFocus_KeyUp(ByRef KeyCode As Short, ByRef Shift As Short)
		' on keyUp, set var to true
		
		bKeyUp = True
		
	End Sub
End Class