Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class clsBoard
	
	' cls/clsBoard.cls - Part of Quoridor http://code.google.com/p/quoridor/
	'
	' Copyright (C) 2008 Quoridor VB-Project Team
	'
	' This program is free software; you can redistribute it and/or modify it
	' under the terms of the GNU General Public License as published by the
	' Free Software Foundation; either version 3 of the License, or (at your
	' option) any later version.
	'
	' This program is distributed in the hope that it will be useful, but
	' WITHOUT ANY WARRANTY; without even the implied warranty of
	' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General
	' Public License for more details.
	'
	' You should have received a copy of the GNU General Public License along
	' with this program; if not, see <http://www.gnu.org/licenses/>.
	
	'figures
	Private NoOfPlayer As Byte
	Private NoOfBricks As Byte 'per player
	Private Players() As clsPlayer
	Private Blocker() As CustomTypes.Brick
	Private Dimensions As Byte
	Private activePlayer As Byte
	
	Function getPlayerType(ByRef Player As Byte) As Byte
		' getPlayerType As Byte
		' get the type of player
		' - [IN] Player As Byte: player number which should be asked
		' - returns the type of player, see clsPlayer.setType()
		getPlayerType = Players(Player).getType_Renamed()
	End Function
	
	Function getPlayerForLocation(ByRef pos As CustomTypes.Position) As Byte
		' getPlayerForLocation As Byte
		' - [IN] pos as Position: defines the position which should be checked
		' - returns the player number if a player is on the position, else 255
		Dim i As Byte
		getPlayerForLocation = 255 'if no player found return 255
		For i = LBound(Players) To UBound(Players)
			If comparePos(pos, Players(i).getLocation) Then
				getPlayerForLocation = i
				Exit Function
			End If
		Next i
	End Function
	
	'UPGRADE_NOTE: dir wurde aktualisiert auf dir_Renamed. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Function checkMove(ByRef pos As CustomTypes.Position, ByVal dir_Renamed As Byte) As Boolean
		' checkMove As Boolean
		' checks for possibility to move to given direction
		' - [IN] ByRef pos As Position: defines position
		' - [IN] ByVal dir As Byte: defines direction
		' - returns True if move is possible
		'UPGRADE_WARNING: Arrays in Struktur newPos m�ssen m�glicherweise initialisiert werden, bevor sie verwendet werden k�nnen. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim newPos As CustomTypes.Position
		If Not checkFrontWall(pos, dir_Renamed) Then
			
			'there is a border
			Select Case dir_Renamed
				Case 0 'bottom
					If pos.Position(1) = Dimensions Then
						GoTo fEnd
					End If
					newPos.Position(0) = pos.Position(0)
					newPos.Position(1) = pos.Position(1) + 1
				Case 1 'right
					If pos.Position(0) = Dimensions Then
						GoTo fEnd
					End If
					newPos.Position(0) = pos.Position(0) + 1
					newPos.Position(1) = pos.Position(1)
				Case 2 'top
					If pos.Position(1) = 0 Then
						GoTo fEnd
					End If
					newPos.Position(0) = pos.Position(0)
					newPos.Position(1) = pos.Position(1) - 1
				Case 3 'left
					If pos.Position(0) = 0 Then
						GoTo fEnd
					End If
					newPos.Position(0) = pos.Position(0) - 1
					newPos.Position(1) = pos.Position(1)
			End Select
			
			'field seems to be free and with no wall between us and it
			checkMove = True
			Exit Function
			
		Else
			checkMove = False
		End If
fEnd: 
		checkMove = False
		Exit Function
	End Function
	
	'UPGRADE_NOTE: dir wurde aktualisiert auf dir_Renamed. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Function movePlayer(ByRef i As Byte, ByRef dir_Renamed As Byte) As Boolean
		If checkMove(Players(i).getLocation, dir_Renamed) Then
			movePlayer = Players(i).Move(dir_Renamed)
		Else
			movePlayer = False
		End If
	End Function
	
	Function getWalls() As CustomTypes.Brick()
		getWalls = VB6.CopyArray(Blocker)
	End Function
	
	Function saveWall(ByRef Br As CustomTypes.Brick, ByRef Player As Byte) As Short
		'returnvalues
		'0 = ok
		'1 = player have no blockers left
		'2 = cannot be used cause you would envelop a figure or position is used by another blocker
		'3 = player index out of range
		Dim i As Byte
		Dim saved As Boolean
		
		'check for valid player index
		If Player > UBound(Players) - LBound(Players) Then
			saveWall = 3
			Exit Function
		End If
		
		If Players(Player).getBricks = 0 Then
			saveWall = 1
			Exit Function
		End If
		
		If Not checkPlaceWall(Br.Position(0), Br.Position(1), (Br.Landscape)) Then
			saveWall = 2
			Exit Function
		End If
		
		saved = False
		
		'running thru setted Blockers to get last index of the placed ones
		For i = LBound(Blocker) To UBound(Blocker)
			If Not Blocker(i).Placed Then
				Exit For
			End If
		Next i
		Blocker(i).Placed = True
		Blocker(i).Landscape = Br.Landscape
		Blocker(i).Position(0) = Br.Position(0)
		Blocker(i).Position(1) = Br.Position(1)
		Players(Player).subtractStone()
	End Function
	
	Function getPlayerColor(ByRef i As Byte) As Integer
		Select Case i
			Case 0
				getPlayerColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)
			Case 1
				If NoOfPlayer = 3 Then '4 player game
					getPlayerColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Lime)
				Else
					getPlayerColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
				End If
			Case 2
				If NoOfPlayer = 3 Then '4 player game
					getPlayerColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
				Else
					getPlayerColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Lime)
				End If
			Case 3
				getPlayerColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
		End Select
	End Function
	
	Function getDimension() As Byte
		getDimension = Dimensions
	End Function
	
	Function getPlayerLocation(ByRef i As Byte) As CustomTypes.Position
		On Error GoTo OutOfIndex
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts getPlayerLocation konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		getPlayerLocation = Players(i).getLocation
		Exit Function
OutOfIndex: 
		getPlayerLocation = xy2pos(255, 255)
	End Function
	
	Function getActivePlayer() As Byte
		getActivePlayer = activePlayer
	End Function
	
	Private Function getPlayerMove() As CustomTypes.Move
		'function will ask networkhandler or AI for move direction
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts getPlayerMove konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		getPlayerMove = Players(activePlayer).getMove
	End Function
	
	Sub doPlayerMove()
		'UPGRADE_WARNING: Arrays in Struktur PlayerMove m�ssen m�glicherweise initialisiert werden, bevor sie verwendet werden k�nnen. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim PlayerMove As CustomTypes.Move
		If getPlayerType(activePlayer) = 1 Then 'player is AI controlled
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts PlayerMove konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			PlayerMove = getPlayerMove() 'start calculations
			
			If PlayerMove.ErrorCode <> 0 Then
				Exit Sub
			End If
			If PlayerMove.FigureMove = True Then
				movePlayer(activePlayer, (PlayerMove.MoveDirection))
			Else
				'saveWall
			End If
		ElseIf getPlayerType(activePlayer) < 1 Then 
			'networkhandler
		End If
	End Sub
	
	Function NextTurn() As Boolean
		activePlayer = getNextPlayer(activePlayer, NoOfPlayer)
		If getPlayerType(activePlayer) > 0 Then 'player is AI or network controlled
			NextTurn = True
		Else
			NextTurn = False
		End If
	End Function
	
	Function checkFrontWall(ByRef pos As CustomTypes.Position, ByRef direction As Byte) As Boolean
		'rev 86
		'if true there are a wall in front of the position and with this direction
		'UPGRADE_WARNING: Arrays in Struktur stone m�ssen m�glicherweise initialisiert werden, bevor sie verwendet werden k�nnen. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim stone As CustomTypes.Brick
		Dim i As Byte
		
		For i = LBound(Blocker) To UBound(Blocker)
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts stone konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			stone = Blocker(i)
			'if we found the first stone which is not placed or which have a default value as position we exiting
			If stone.Placed = False Or stone.Position(0) = 255 Or stone.Position(0) = 255 Then
				Exit For
			End If
			Select Case direction
				Case 0 'bottom
					If stone.Landscape = True And stone.Position(1) = pos.Position(1) Then
						'stone is bottom on right
						If stone.Position(0) = pos.Position(0) Then GoTo returnFalse
						'stone is bottom on left
						If stone.Position(0) + 1 = pos.Position(0) Then GoTo returnFalse
					End If
				Case 1 'right
					If stone.Landscape = False And stone.Position(0) = pos.Position(0) Then
						'stone is upper on right
						If stone.Position(1) + 1 = pos.Position(1) Then GoTo returnFalse
						'stone is lower on right
						If stone.Position(1) = pos.Position(1) Then GoTo returnFalse
					End If
				Case 2 'top
					If stone.Landscape = True And stone.Position(1) + 1 = pos.Position(1) Then
						'stone is top on right
						If stone.Position(0) = pos.Position(0) Then GoTo returnFalse
						'stone is top on left
						If stone.Position(0) + 1 = pos.Position(0) Then GoTo returnFalse
					End If
				Case 3 'left
					If stone.Landscape = False And stone.Position(0) + 1 = pos.Position(0) Then
						'stone is upper on left
						If stone.Position(1) + 1 = pos.Position(1) Then GoTo returnFalse
						'stone is lower on left
						If stone.Position(1) = pos.Position(1) Then GoTo returnFalse
					End If
			End Select
		Next i
		'no stone have been found which blocks our way
		checkFrontWall = False
		Exit Function
returnFalse: 
		checkFrontWall = True
		Exit Function
	End Function
	
	Private Function NoToDir(ByRef i As Byte) As Byte
		'part of checkPlaceWall
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		NoToDir = VB.Switch(0 < i - 3, i - 3, True, i)
	End Function
	
	Private Function NoToField(ByRef i As Byte) As Byte
		'part of checkPlaceWall
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		NoToField = VB.Switch(0 < i - 3, 0, True, 1)
	End Function
	
	Function checkPlaceWall(ByRef x As Byte, ByRef y As Byte, ByRef Landscape As Boolean) As Boolean
		'if true a wall can be placed on this position
		'UPGRADE_WARNING: Arrays in Struktur stone m�ssen m�glicherweise initialisiert werden, bevor sie verwendet werden k�nnen. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim stone As CustomTypes.Brick
		Dim i As Byte
		
		For i = LBound(Blocker) To UBound(Blocker)
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts stone konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			stone = Blocker(i)
			'if we found the first stone which is not placed or which have a default value as position we exiting
			If stone.Placed = False Or stone.Position(0) = 255 Or stone.Position(1) = 255 Then
				Exit For
			End If
			'stone is on the same position
			If stone.Position(0) = x And stone.Position(1) = y Then
				GoTo returnFalse_checkPlaceWall
				'same level but left and landscape
			ElseIf stone.Position(0) = x + 1 And stone.Position(1) = y And Landscape And stone.Landscape Then 
				GoTo returnFalse_checkPlaceWall
				'same level but right and landscape
			ElseIf stone.Position(0) + 1 = x And stone.Position(1) = y And Landscape And stone.Landscape Then 
				GoTo returnFalse_checkPlaceWall
				'up but same row and not landscape
			ElseIf stone.Position(0) = x And stone.Position(1) = y + 1 And Not Landscape And Not stone.Landscape Then 
				GoTo returnFalse_checkPlaceWall
				'down but same row and not landscape
			ElseIf stone.Position(0) = x And stone.Position(1) + 1 = y And Not Landscape And Not stone.Landscape Then 
				GoTo returnFalse_checkPlaceWall
			End If
			
		Next i
		'no stone have been found which blocks our new stone
		checkPlaceWall = True
		Exit Function
returnFalse_checkPlaceWall: 
		checkPlaceWall = False
		Exit Function
	End Function
	
	Function checkPlaceWall_donotuse(ByRef newPos As CustomTypes.Brick) As Boolean
		'for-counter
		Dim i As Byte
		Dim i2 As Short
		
		'0 = aborted - touched neighbor field on first move
		'1 = field touched were we had started - ccw around
		'2 = field touched were we had started - cw around
		'3 = target wall reached
		'255 = no run have been done jet
		Dim RunningResults(7) As Byte
		'currently startpoint
		'UPGRADE_WARNING: Arrays in Struktur startPoint m�ssen m�glicherweise initialisiert werden, bevor sie verwendet werden k�nnen. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim startPoint As CustomTypes.Position
		'saves the right and left turns while running, so we can determine if we run ccw or cw
		Dim rightLeftCount(7) As Byte
		'holds the actual running direction
		Dim direction As Byte
		
		'saves points were we turned around
        Dim turned(,) As CustomTypes.Position
		ReDim turned(7, 20) '20 to get a default space, will be redim if we need more
		
		For i = 0 To 7
			RunningResults(i) = 255
			rightLeftCount(i) = 127 'to be able to calc minus and plus for left and right
		Next i
		
		direction = 255
		startPoint.Position(0) = 255
		startPoint.Position(1) = 255
		
		For i = 0 To 7
			For i2 = 0 To 20
				turned(i, i2).Position(0) = 255
				turned(i, i2).Position(1) = 255
			Next i2
		Next i
		
		'starting algorithmus
		
		For i = 0 To 7 'startfields/directions
			'setting up startposition
			Select Case NoToField(i)
				Case 0
					startPoint.Position(0) = newPos.Position(0)
					startPoint.Position(1) = newPos.Position(1)
				Case 1
					startPoint.Position(0) = newPos.Position(0)
					startPoint.Position(1) = newPos.Position(1) + 1
				Case 2
					startPoint.Position(0) = newPos.Position(0) + 1
					startPoint.Position(1) = newPos.Position(1) + 1
				Case 3
					startPoint.Position(0) = newPos.Position(0) + 1
					startPoint.Position(1) = newPos.Position(1)
			End Select
		Next i
		
		
		'first try to be deleted
		'use with care! theoretical max 8192 Byte memspace if field dimension is set to 256 don't know if VB can handle this big
		'Dim RunnedFields() As Position
		'ReDim RunnedFields(7, (Dimensions + 1) * (Dimensions + 1) - 1)
		
		'For i = 0 To 7 'fields around the new wall
		'    For i2 = 0 To (Dimensions + 1) * (Dimensions + 1) - 1 '-1 because we start at 0
		'        For i3 = 0 To 1 'directions, first to the smaller index, then to the higher
		'            RunnedFields(i, i2).Position(i3) = 255
		'        Next i3
		'    Next i2
		'Next i
		
		'For i = 0 To 3 'position around the new wall
		'    'setting startfield
		'    For i2 = 0 To 1
		'        'running ccw
		'        'x
		'        RunnedFields(i + Switch(Not i2, 0, i2, 4), 0).Position(0) = Switch( _
		''                                                                        i = 0 Or i = 1, newPos.Position(0), _
		''                                                                        i = 2 Or i = 3, newPos.Position(0) + 1 _
		''                                                                        )
		'        'y
		'        RunnedFields(i + Switch(Not i2, 0, i2, 4), 0).Position(1) = Switch( _
		''                                                                        i = 0 Or i = 3, newPos.Position(0), _
		''                                                                        i = 1 Or i = 2, newPos.Position(0) + 1 _
		''                                                                        )
		'    Next i2
		'    For i2 = 0 To 1 'directions per position
		'        ended = False
		'
		'        'Do
		'
		'        'While ended
		'    Next i2
		'Next i
	End Function
	
	Function activateAI(ByRef PlayerIndex As Byte) As Byte
		'load a AI instance for given Player
		'WARNING: load of four AIs will use 30MB RAM at minimum
		'0 = ok
		'1 = is already under AI control
		'2 = is a network player which may have open network
		'    connections, stop networking  first
		' 255  = out of playerindex range
		If PlayerIndex > UBound(Players) - LBound(Players) Then
			activateAI = 255
		Else
			activateAI = Players(PlayerIndex).startAI(Blocker, getPlayerPositions, PlayerIndex, Dimensions)
		End If
	End Function
	
	Function getNameOfPlayer(ByRef i As Byte) As String
		On Error GoTo OutOfIndex
		getNameOfPlayer = Players(i).getPlayerName
OutOfIndex: 
		getNameOfPlayer = ""
	End Function
	Function getNoOfPlayer() As Byte
		getNoOfPlayer = NoOfPlayer
	End Function
	
	Function getPlayerTarget(ByRef i As Byte) As Byte
		getPlayerTarget = Players(i).getTarget
	End Function
	
	Function getRemainingPlayerBricks(ByRef i As Byte) As Byte
		getRemainingPlayerBricks = Players(i).getBricks 'we want a number not an index
	End Function
	
	Private Function getPlayerPositions() As CustomTypes.Position()
		Dim i As Byte
		Dim returnvalue() As CustomTypes.Position
		ReDim returnvalue(NoOfPlayer)
		For i = 0 To NoOfPlayer
			returnvalue(i).Position(0) = getPlayerLocation(i).Position(0)
			returnvalue(i).Position(1) = getPlayerLocation(i).Position(1)
		Next i
		getPlayerPositions = VB6.CopyArray(returnvalue)
	End Function
	
	Sub create(ByRef NoOfPl As Byte, ByRef Dimension As Byte)
		Dim pStarts(3) As CustomTypes.Position
		Dim i As Byte
		Dim i2 As Byte
		
		'Testing
		
		
		'    Dim Heap As clsSimpleHeap
		'    Dim integ As Integer
		'    Call Heap.setHeapSize(5)
		
		
		'/testing
		
		'check input value of NoOfPl if not 4 set to 2 - started with 0
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		NoOfPlayer = VB.Switch(NoOfPl = 3, 3, True, 1)
		NoOfBricks = getNoOfWalls(NoOfPlayer)
		'if the dimension is smaller than the NoOfPlayer the field will be made bigger
		'if dimension = 255 it'll set to 254 otherwise we may get unexpected results of some functions
		' because we use 255 as defaultvalue in some functions
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Dimensions = VB.Switch(Dimension < NoOfPlayer, NoOfPlayer, Dimension < 255, Dimension, Dimension = 255, 254)
		
		ReDim Players(NoOfPlayer)
		For i = 0 To NoOfPlayer
			Players(i) = New clsPlayer
		Next i
		
		'calc player starts - players counted ccw
		pStarts(0).Position(0) = Dimensions / 2
		pStarts(1).Position(1) = pStarts(0).Position(0)
		pStarts(0).Position(1) = 0
		pStarts(3).Position(0) = pStarts(0).Position(1)
		pStarts(1).Position(0) = Dimensions
		pStarts(2).Position(1) = pStarts(1).Position(0)
		'subtract to get the other line near the center for the mirrored player
		pStarts(2).Position(0) = Dimensions - CByte(Dimensions / 2)
		pStarts(3).Position(1) = pStarts(2).Position(0)
		'player blocked case
		'pStarts(0).Position(0) = 0
		'pStarts(0).Position(1) = 0
		'pStarts(1).Position(0) = 0
		'pStarts(1).Position(1) = 1
		'pStarts(2).Position(0) = 1
		'pStarts(2).Position(1) = 0
		'pStarts(3).Position(0) = 1
		'pStarts(3).Position(1) = 1
		
		'player will be blocked by move
		'pStarts(0).Position(0) = 2
		'pStarts(0).Position(1) = 0
		'pStarts(1).Position(0) = 0
		'pStarts(1).Position(1) = 1
		'pStarts(2).Position(0) = 0
		'pStarts(2).Position(1) = 0
		'pStarts(3).Position(0) = 1
		'pStarts(3).Position(1) = 1
		
		'set player starts
		For i = LBound(Players) To UBound(Players)
			If NoOfPlayer = 1 And i = 1 Then
				i2 = i + 1
				'change 2th player's startposition to 3rd players's
				'if two players playing
			Else
				i2 = i
			End If
			'running create routine
			Players(i).create(pStarts(i2).Position(0), pStarts(i2).Position(1), NoOfBricks, Player2Target(i, NoOfPlayer))
		Next i
		
		'reserve space for blockers
		'+1-1 cause we need to move one right and left because of 0 starting values
		ReDim Blocker((NoOfPlayer + 1) * (NoOfBricks + 1) - 1)
		
		'set blockers start values
		For i = 0 To UBound(Blocker)
			Blocker(i).Landscape = False
			Blocker(i).Position(0) = 255
			Blocker(i).Position(1) = 255
		Next i
		'Players(1).startAI Blocker, pStarts, 1, Dimensions
	End Sub
End Class