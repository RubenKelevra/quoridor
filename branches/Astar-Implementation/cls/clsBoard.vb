Option Strict On
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
    Protected Friend NoOfPlayer As Byte
    Protected Friend NoOfBricks As Byte 'per player
    Protected Friend Players() As clsPlayer
    Protected Friend Blocker() As clsBrick
    Protected Friend Dimensions As Byte
    Protected Friend activePlayer As Byte
	
    Public Function getPlayerType(ByRef Player As Byte) As Byte
        ' getPlayerType As Byte
        ' get the type of player
        ' - [IN] Player As Byte: player number which should be asked
        ' - returns the type of player, see clsPlayer.setType()
        getPlayerType = Players(Player).getPlayerType()
    End Function
	
    Public Function getPlayerForLocation(ByRef pos As Position) As Byte
        ' getPlayerForLocation As Byte
        ' - [IN] pos as Position: defines the position which should be checked
        ' - returns the player number if a player is on the position, else 255
        Dim i As Byte
        getPlayerForLocation = 255 'if no player found return 255
        For i = CByte(LBound(Players)) To CByte(UBound(Players))
            If comparePos(pos, Players(i).getLocation) Then
                getPlayerForLocation = i
                Exit Function
            End If
        Next i
    End Function
	
    Public Function checkMove(ByRef pos As Position, ByVal direction As Byte) As Boolean
        ' checkMove As Boolean
        ' checks for possibility to move to given direction
        ' - [IN] ByRef pos As Position: defines position
        ' - [IN] ByVal dir As Byte: defines direction
        ' - returns True if move is possible
        Dim newPos As Position
        If Not checkFrontWall(pos, direction) Then

            'there is a border
            Select Case direction
                Case 0 'bottom
                    If pos.Y = Dimensions Then
                        GoTo fEnd
                    End If
                    newPos.X = pos.X
                    newPos.Y = CByte(pos.Y + 1)
                Case 1 'right
                    If pos.X = Dimensions Then
                        GoTo fEnd
                    End If
                    newPos.X = CByte(pos.X + 1)
                    newPos.Y = pos.Y
                Case 2 'top
                    If pos.Y = 0 Then
                        GoTo fEnd
                    End If
                    newPos.X = pos.X
                    newPos.Y = CByte(pos.Y - 1)
                Case 3 'left
                    If pos.X = 0 Then
                        GoTo fEnd
                    End If
                    newPos.X = CByte(pos.X - 1)
                    newPos.Y = pos.Y
            End Select

            'field seems to be free and with no wall between us and it
            checkMove = True
            Exit Function

        Else
            checkMove = False ' NOTICE: Not needed at this place
        End If
fEnd:
        checkMove = False
        Exit Function ' NOTICE: Not needed at this place
    End Function
	
    Public Function movePlayer(ByRef i As Byte, ByRef direction As Byte) As Boolean
        If checkMove(Players(i).getLocation, direction) Then
            movePlayer = Players(i).Move(direction)
        Else
            movePlayer = False
        End If
    End Function
	
    Public Function getWalls() As clsBrick()
        getWalls = CType(VB6.CopyArray(Blocker), clsBrick())
    End Function
	
    Public Function saveWall(ByRef Br As clsBrick, ByRef Player As Byte) As Short
        'returnvalues
        '0 = ok
        '1 = player have no blockers left
        '2 = cannot be used cause you would envelop a figure or position is used by another blocker
        '3 = player index out of range
        Dim B As Byte
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

        If Not checkPlaceWall(Br.Position.X, Br.Position.Y, (Br.Horizontal)) Then
            saveWall = 2
            Exit Function
        End If

        saved = False

        'running thru setted Blockers to get last index of the placed ones
        For B = CByte(LBound(Blocker)) To CByte(UBound(Blocker))
            If Not Blocker(B).Placed Then
                Exit For
            End If
        Next B
        Blocker(B).Placed = True
        Blocker(B).Horizontal = Br.Horizontal
        Blocker(B).Position.X = Br.Position.X
        Blocker(B).Position.Y = Br.Position.Y
        Players(Player).subtractStone()
    End Function
	
    '    Function getPlayerColor(ByRef i As Byte) As Integer
    Public Function getPlayerColor(ByRef i As Byte) As System.Drawing.Color
        Select Case i
            Case 0
                getPlayerColor = System.Drawing.Color.Blue
            Case 1
                If NoOfPlayer = 3 Then '4 player game
                    getPlayerColor = System.Drawing.Color.Lime
                Else
                    getPlayerColor = System.Drawing.Color.Red
                End If
            Case 2
                If NoOfPlayer = 3 Then '4 player game
                    getPlayerColor = System.Drawing.Color.Red
                Else
                    getPlayerColor = System.Drawing.Color.Lime
                End If
            Case 3
                getPlayerColor = System.Drawing.Color.Yellow
        End Select

    End Function

    Public Function getDimension() As Byte
        getDimension = Dimensions
    End Function

    Public Function getPlayerLocation(ByRef i As Byte) As Position
        On Error GoTo OutOfIndex
        getPlayerLocation = Players(i).getLocation
        Exit Function
OutOfIndex:
        getPlayerLocation = xy2position(255, 255)
    End Function

    Public Function getActivePlayer() As Byte
        getActivePlayer = activePlayer
    End Function

    'FIXME: was written for old ki implementation, maybe usefull for network handler
    'Private Function getPlayerMove() As clsMove
    'function will ask networkhandler or AI for move direction
    '    getPlayerMove = Players(activePlayer).getMove
    'End Function

    Public Sub doPlayerMove()
        'Dim PlayerMove As clsMove
        If getPlayerType(activePlayer) = 1 Then 'player is AI controlled
            'PlayerMove = getPlayerMove() 'start calculations

            'If PlayerMove.ErrorCode <> 0 Then
            '    Exit Sub
            'End If
            'If PlayerMove.FigureMove = True Then
            '    movePlayer(activePlayer, (PlayerMove.MoveDirection))
            'Else
            '    'saveWall
            'End If
        ElseIf getPlayerType(activePlayer) < 1 Then
            'networkhandler
        End If
    End Sub

    Public Function NextTurn() As Boolean
        activePlayer = getNextPlayer(activePlayer, NoOfPlayer)
        If getPlayerType(activePlayer) > 0 Then 'player is AI or network controlled
            NextTurn = True
        Else
            NextTurn = False
        End If
    End Function

    Public Function checkFrontWall(ByRef pos As Position, ByRef direction As Byte) As Boolean
        'if true there are a wall in front of the position and with this direction
        Dim stone As clsBrick
        Dim B As Byte

        For B = CByte(LBound(Blocker)) To CByte(UBound(Blocker))
            stone = Blocker(B)
            'if we found the first stone which is not placed or which have a default value as position we exiting
            If stone.Placed = False Or stone.Position.X = 255 Or stone.Position.X = 255 Then
                Exit For
            End If
            Select Case direction
                Case 0 'bottom
                    If stone.Horizontal = True And stone.Position.Y = pos.Y Then
                        'stone is bottom on right
                        If stone.Position.X = pos.X Then GoTo returnFalse
                        'stone is bottom on left
                        If stone.Position.X + 1 = pos.X Then GoTo returnFalse
                    End If
                Case 1 'right
                    If stone.Horizontal = False And stone.Position.X = pos.X Then
                        'stone is upper on right
                        If stone.Position.Y + 1 = pos.Y Then GoTo returnFalse
                        'stone is lower on right
                        If stone.Position.Y = pos.Y Then GoTo returnFalse
                    End If
                Case 2 'top
                    If stone.Horizontal = True And stone.Position.Y + 1 = pos.Y Then
                        'stone is top on right
                        If stone.Position.X = pos.X Then GoTo returnFalse
                        'stone is top on left
                        If stone.Position.X + 1 = pos.X Then GoTo returnFalse
                    End If
                Case 3 'left
                    If stone.Horizontal = False And stone.Position.X + 1 = pos.X Then
                        'stone is upper on left
                        If stone.Position.Y + 1 = pos.Y Then GoTo returnFalse
                        'stone is lower on left
                        If stone.Position.Y = pos.Y Then GoTo returnFalse
                    End If
            End Select
        Next B
        'no stone have been found which blocks our way
        checkFrontWall = False
        Exit Function
returnFalse:
        checkFrontWall = True
        Exit Function
    End Function

    Private Function NoToDir(ByRef i As Byte) As Byte
        'part of checkPlaceWall
        NoToDir = CByte(VB.Switch(0 < i - 3, i - 3, True, i))
    End Function

    Private Function NoToField(ByRef i As Byte) As Byte
        'part of checkPlaceWall
        NoToField = CByte(VB.Switch(0 < i - 3, 0, True, 1))
    End Function

    Function checkPlaceWall(ByRef x As Byte, ByRef y As Byte, ByRef Horizontal As Boolean) As Boolean
        'if true a wall can be placed on this position
        Dim stone As clsBrick
        Dim i As Byte

        For i = CByte(LBound(Blocker)) To CByte(UBound(Blocker))
            stone = Blocker(i)
            'if we found the first stone which is not placed or which have a default value as position we exiting
            If stone.Placed = False Or stone.Position.X = 255 Or stone.Position.Y = 255 Then
                Exit For
            End If
            'stone is on the same position
            If stone.Position.X = x And stone.Position.Y = y Then
                GoTo returnFalse_checkPlaceWall
                'same level but left and Horizontal
            ElseIf stone.Position.X = x + 1 And stone.Position.Y = y And Horizontal And stone.Horizontal Then
                GoTo returnFalse_checkPlaceWall
                'same level but right and Horizontal
            ElseIf stone.Position.X + 1 = x And stone.Position.Y = y And Horizontal And stone.Horizontal Then
                GoTo returnFalse_checkPlaceWall
                'up but same row and not Horizontal
            ElseIf stone.Position.X = x And stone.Position.Y = y + 1 And Not Horizontal And Not stone.Horizontal Then
                GoTo returnFalse_checkPlaceWall
                'down but same row and not Horizontal
            ElseIf stone.Position.X = x And stone.Position.Y + 1 = y And Not Horizontal And Not stone.Horizontal Then
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

    Function checkPlaceWall_donotuse(ByRef newPos As clsBrick) As Boolean
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
        Dim startPoint As Position
        'saves the right and left turns while running, so we can determine if we run ccw or cw
        Dim rightLeftCount(7) As Byte
        'holds the actual running direction
        Dim direction As Byte

        'saves points were we turned around
        Dim turned(,) As Position
        ReDim turned(7, 20) '20 to get a default space, will be redim if we need more

        For i = 0 To 7
            RunningResults(i) = 255
            rightLeftCount(i) = 127 'to be able to calc minus and plus for left and right
        Next i

        direction = 255
        startPoint.X = 255
        startPoint.Y = 255

        For i = 0 To 7
            For i2 = 0 To 20
                turned(i, i2).X = 255
                turned(i, i2).Y = 255
            Next i2
        Next i

        'starting algorithmus

        For i = 0 To 7 'startfields/directions
            'setting up startposition
            Select Case NoToField(i)
                Case 0
                    startPoint.X = newPos.Position.X
                    startPoint.Y = newPos.Position.Y
                Case 1
                    startPoint.X = newPos.Position.X
                    startPoint.Y = CByte(newPos.Position.Y + 1)
                Case 2
                    startPoint.X = CByte(newPos.Position.X + 1)
                    startPoint.Y = CByte(newPos.Position.Y + 1)
                Case 3
                    startPoint.X = CByte(newPos.Position.X + 1)
                    startPoint.Y = newPos.Position.Y
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
        'FIXME: Warning is not up to date
        'WARNING: load of four AIs will use 30MB RAM at minimum
        '0 = ok
        '1 = is already under AI control
        '2 = is a network player which may have open network
        '    connections, stop networking  first
        ' 255  = out of playerindex range
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

    Private Function getPlayerPositions() As Position()
        Dim i As Byte
        Dim returnvalue() As Position
        ReDim returnvalue(NoOfPlayer)
        For i = 0 To NoOfPlayer
            returnvalue(i).X = getPlayerLocation(i).X
            returnvalue(i).Y = getPlayerLocation(i).Y
        Next i
        getPlayerPositions = returnvalue
    End Function

    Sub create(ByRef NoOfPl As Byte, ByRef Dimension As Byte)
        Dim pStarts(3) As Position
        Dim B As Byte
        Dim B2 As Byte

        'check input value of NoOfPl if not 4 set to 2 - started with 0
        NoOfPlayer = CByte(VB.Switch(NoOfPl = 3, 3, True, 1))
        NoOfBricks = getNoOfWalls(NoOfPlayer)
        'if the dimension is smaller than the NoOfPlayer the field will be made bigger
        'if dimension = 255 it'll set to 254 otherwise we may get unexpected results of some functions
        ' because we use 255 as defaultvalue in some functions
        Dimensions = CByte(VB.Switch(Dimension < NoOfPlayer, NoOfPlayer, Dimension < 255, Dimension, Dimension = 255, 254))

        ReDim Players(NoOfPlayer)
        For B = 0 To NoOfPlayer
            Players(B) = New clsPlayer
        Next B

        'calc player starts - players counted ccw
        pStarts(0).X = CByte(Dimensions / 2)
        pStarts(1).Y = pStarts(0).X
        pStarts(0).Y = 0
        pStarts(3).X = pStarts(0).Y
        pStarts(1).X = Dimensions
        pStarts(2).Y = pStarts(1).X
        'subtract to get the other line near the center for the mirrored player
        pStarts(2).X = Dimensions - CByte(Dimensions / 2)
        pStarts(3).Y = pStarts(2).X

        'set player starts
        For B = CByte(LBound(Players)) To CByte(UBound(Players))
            If NoOfPlayer = 1 And B = 1 Then
                B2 = CByte(B + 1)
                'change 2th player's startposition to 3rd players's
                'if two players playing
            Else
                B2 = B
            End If
            'running create routine
            Players(B).create(pStarts(B2).X, pStarts(B2).Y, NoOfBricks, Player2Target(B, NoOfPlayer))
        Next B

        'reserve space for blockers
        '+1-1 cause we need to move one right and left because of 0 starting values
        ReDim Blocker((NoOfPlayer + 1) * (NoOfBricks + 1) - 1)

        'set blockers start values
        For B = 0 To CByte(UBound(Blocker))
            Blocker(B) = New clsBrick
            Blocker(B).Horizontal = False
            Blocker(B).Position.X = 255
            Blocker(B).Position.Y = 255
        Next B
        'Players(1).startAI Blocker, pStarts, 1, Dimensions
    End Sub
End Class