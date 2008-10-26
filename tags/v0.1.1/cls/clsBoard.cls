VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsBoard"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

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
Private Blocker() As Brick
Private Dimensions As Byte
Private activePlayer As Byte

Function getPlayerType(i As Byte) As Byte
    'get the type of player, see clsPlayer.setType()
    getPlayerType = Players(i).getType
End Function

Function setPlayerType(i As Byte, newType As Byte) As Byte
    'only necessary if not a local user controled player, definition of Type see clsPlayer.setType()
    'returnvalue
    '0 = ok
    '1 = out of valid type range
    '2 = out of player index range
    If i <= NoOfPlayer Then
        setPlayerType = Players(i).setType(newType)
    Else
        setPlayerType = 2
    End If
End Function

Function getPlayerForLocation(pos As Position) As Byte
    Dim i As Byte
    getPlayerForLocation = 255 'if no player found return 255
    For i = LBound(Players) To UBound(Players)
        If comparePos(pos, Players(i).getLocation) Then
            getPlayerForLocation = i
            Exit Function
        End If
    Next i
End Function

Function checkMove(ByRef pos As Position, ByVal dir As Byte, DontSetMeOnTrue As Boolean) As Boolean
    Dim newPos As Position
    Dim i As Byte
    Dim i2 As Byte
    Dim i3 As Byte
    Dim bRekursionCheck As Boolean
    Dim int1 As Integer
    Dim int2 As Integer
    If Not checkFrontWall(pos, dir) Then
    
        'there is a border
        Select Case dir
            Case 0: 'bottom
                If pos.Position(1) = Dimensions Then
                    GoTo fEnd
                End If
                newPos.Position(0) = pos.Position(0)
                newPos.Position(1) = pos.Position(1) + 1
            Case 1: 'right
                If pos.Position(0) = Dimensions Then
                    GoTo fEnd
                End If
                newPos.Position(0) = pos.Position(0) + 1
                newPos.Position(1) = pos.Position(1)
            Case 2: 'top
                If pos.Position(1) = 0 Then
                    GoTo fEnd
                End If
                newPos.Position(0) = pos.Position(0)
                newPos.Position(1) = pos.Position(1) - 1
            Case 3: 'left
                If pos.Position(0) = 0 Then
                    GoTo fEnd
                End If
                newPos.Position(0) = pos.Position(0) - 1
                newPos.Position(1) = pos.Position(1)
        End Select
    
        'is there a player
        'AND does every player can do moves after our move?
        If Not (getPlayerForLocation(newPos) = 255) Then
            GoTo fEnd
        End If
        'FIXME switched off
        'DontSetMeOnTrue = True
                
        If Not DontSetMeOnTrue Then 'if we are recursitioning
            bRekursionCheck = True 'if we found no player in move dir which might be blocked
                                   'we do allow this move
            For i2 = LBound(Players) To UBound(Players)
                int1 = 0
                int2 = 0
                int1 = Players(i2).getLocation.Position(0) + dirXshift(flipDir(dir)) * 2
                int2 = Players(i2).getLocation.Position(1) + dirYshift(flipDir(dir)) * 2
                If Dimensions <= int1 Or int1 < 0 Or Dimensions <= int2 Or int2 < 0 Then 'this couldn't be the playerposition, its out of range
                    GoTo endNext_i2
                End If
                If comparePos(xy2pos(int1, int2), Players(getActivePlayer).getLocation) Then
                    bRekursionCheck = False 'if the figure have no other way then the way to the player
                                             'we aren't allowed to make this move
                    'there is a figure that might be blocked, checkout if it haves another way to move
                    For i3 = 0 To 3 'dirs for the figure what might be blocked
                        'now we want to check if the given player has another way to turn on the next move if
                        'the active player had moved to this position (deactivating recursion!)
                        If (Not (i3 = flipDir(dir))) Then 'don't check in player direction,
                                                          'this move isn't anymore possible
                                                          'if active player had moved
                            If bRekursionCheck = False Then 'if we hadn't found another way till now
                                bRekursionCheck = checkMove(Players(i2).getLocation, i3, True)
                            End If
                        End If
                    Next i3
                    'there isn't the possibility that two players are on the same position
                    Exit For
                End If
endNext_i2:
            Next i2
            If Not bRekursionCheck Then
                checkMove = False
                Exit Function
            End If
        End If
                
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
Function movePlayer(i As Byte, dir As Byte) As Boolean
    If checkMove(Players(i).getLocation, dir, False) Then
        movePlayer = Players(i).Move(dir)
        NextTurn
    Else
        movePlayer = False
    End If
End Function

Function getWalls() As Brick()
    getWalls = Blocker
End Function

Function saveWall(Br As Brick, Player As Byte) As Integer
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
    
    If Not checkPlaceWall(Br.Position(0), Br.Position(1), Br.Landscape) Then
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
    Players(Player).subtractStone
End Function
Function getPlayerColor(i As Byte) As Long
    Select Case i
        Case 0:
            getPlayerColor = vbBlue
        Case 1:
            If NoOfPlayer = 4 Then
                getPlayerColor = vbGreen
            Else
                getPlayerColor = vbRed
            End If
        Case 2:
            If NoOfPlayer = 4 Then
                getPlayerColor = vbRed
            Else
                getPlayerColor = vbGreen
            End If
        Case 3:
            getPlayerColor = vbYellow
    End Select
End Function

Function getDimension() As Byte
    getDimension = Dimensions
End Function

Function getPlayerLocation(i As Byte) As Position
    On Error GoTo OutOfIndex
    getPlayerLocation = Players(i).getLocation
    Exit Function
OutOfIndex:
    getPlayerLocation = xy2pos(255, 255)
End Function

Function getActivePlayer() As Byte
    getActivePlayer = activePlayer
End Function

Sub NextTurn()
    activePlayer = Switch(activePlayer < 3, activePlayer + 1, True, 0)
End Sub

Function checkFrontWall(pos As Position, direction As Byte) As Boolean
        'if true there are a wall in front of the position and with this direction
    Dim stone As Brick
    Dim i As Byte
    
    For i = LBound(Blocker) To UBound(Blocker)
        stone = Blocker(i)
        'if we found the first stone which is not placed or which have a default value as position we exiting
        If stone.Placed = False Or stone.Position(0) = 255 _
                                    Or stone.Position(0) = 255 Then
            Exit For
        End If
        Select Case direction
            Case 0: 'bottom
                If stone.Landscape = True And stone.Position(1) = pos.Position(1) Then
                    'stone is bottom on right
                    If stone.Position(0) = pos.Position(0) Then GoTo returnFalse
                    'stone is bottom on left
                    If stone.Position(0) + 1 = pos.Position(0) Then GoTo returnFalse
                End If
            Case 1: 'right
                If stone.Landscape = False And stone.Position(0) = pos.Position(0) Then
                    'stone is upper on right
                    If stone.Position(1) + 1 = pos.Position(1) Then GoTo returnFalse
                    'stone is lower on right
                    If stone.Position(1) = pos.Position(1) Then GoTo returnFalse
                End If
            Case 2: 'top
                If stone.Landscape = True And stone.Position(1) + 1 = pos.Position(1) Then
                    'stone is top on right
                    If stone.Position(0) = pos.Position(0) Then GoTo returnFalse
                    'stone is top on left
                    If stone.Position(0) + 1 = pos.Position(0) Then GoTo returnFalse
                End If
            Case 3: 'left
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

Private Function NoToDir(i As Byte) As Byte
    'part of checkPlaceWall
    NoToDir = Switch(0 < i - 3, i - 3, True, i)
End Function

Private Function NoToField(i As Byte) As Byte
    'part of checkPlaceWall
    NoToField = Switch(0 < i - 3, 0, True, 1)
End Function

Function checkPlaceWall(x As Byte, y As Byte, Landscape As Boolean) As Boolean
    'if true a wall can be placed on this position
    Dim stone As Brick
    Dim i As Byte
    
    For i = LBound(Blocker) To UBound(Blocker)
        stone = Blocker(i)
        'if we found the first stone which is not placed or which have a default value as position we exiting
        If stone.Placed = False Or stone.Position(0) = 255 _
                                    Or stone.Position(1) = 255 Then
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

Function checkPlaceWall_donotuse(newPos As Brick) As Boolean
    'for-counter
    Dim i As Byte
    Dim i2 As Integer
    Dim i3 As Byte
    
    'state variables
    Dim ended As Boolean

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
    Dim turned() As Position
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
            Case 0:
                startPoint.Position(0) = newPos.Position(0)
                startPoint.Position(1) = newPos.Position(1)
            Case 1:
                startPoint.Position(0) = newPos.Position(0)
                startPoint.Position(1) = newPos.Position(1) + 1
            Case 2:
                startPoint.Position(0) = newPos.Position(0) + 1
                startPoint.Position(1) = newPos.Position(1) + 1
            Case 3:
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
    '                                                                        i = 0 Or i = 1, newPos.Position(0), _
    '                                                                        i = 2 Or i = 3, newPos.Position(0) + 1 _
    '                                                                        )
    '        'y
    '        RunnedFields(i + Switch(Not i2, 0, i2, 4), 0).Position(1) = Switch( _
    '                                                                        i = 0 Or i = 3, newPos.Position(0), _
    '                                                                        i = 1 Or i = 2, newPos.Position(0) + 1 _
    '                                                                        )
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

Function getNameOfPlayer(i As Byte) As String
    On Error GoTo OutOfIndex
    getNameOfPlayer = Players(i).getPlayerName
OutOfIndex:
    getNameOfPlayer = ""
End Function
Function getNoOfPlayer() As Byte
    getNoOfPlayer = NoOfPlayer
End Function

Function getPlayerTarget(i As Byte) As Byte
    getPlayerTarget = Players(i).getTarget
End Function

Function getRemainingPlayerBricks(i As Byte) As Byte
    getRemainingPlayerBricks = Players(i).getBricks 'we want a number not an index
End Function

Sub create(NoOfPl As Byte, NoOfBr As Byte, Dimension As Byte)
    Dim pStarts(3) As Position
    Dim i As Byte
    
    'check input value of NoOfPl if not 4 set to 2 - started with 0
    NoOfPlayer = Switch(NoOfPl = 3, 3, True, 1)
    NoOfBricks = NoOfBr
    'if the dimension is smaller than the NoOfPlayer the field will be made bigger
    'if dimension = 255 it'll set to 254 otherwise we may get unexpected results of some functions
    ' because we use 255 as defaultvalue in some functions
    Dimensions = Switch(Dimension < NoOfPlayer, NoOfPlayer, Dimension < 255, Dimension, Dimension = 255, 254)
    
    ReDim Players(NoOfPlayer)
    For i = 0 To NoOfPlayer
        Set Players(i) = New clsPlayer
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
        If NoOfPlayer = 2 And i = 1 Then
            i = i + 1 'change 2th player's startposition to 3rd players's
                      'if two players playing
        End If
        'running create routine
        Players(i).create pStarts(i).Position(0), pStarts(i).Position(1), NoOfBricks, _
            Switch( _
                i = 0, 0, _
                i = 1, 3, _
                i = 2, 2, _
                i = 3, 1)
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
End Sub