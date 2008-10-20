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

'for constructs
'Dim i As Integer 'FIXME: really used?

Function getPlayerType(i As Byte) As Byte
    'get the type of player, see clsPlayer.setPlayerType()
    getPlayerType = Players(i).getPlayerType
End Function

Function setPlayerType(i As Byte, newType As Byte) As Byte
    'only necessary if not a local user controled player, see clsPlayer.setPlayerType()
    setPlayerType = Players(i).setPlayerType(newType)
End Function

Function checkMove(pos As Position, dir As Byte) As Boolean
    Dim newPos As Position
    Dim i As Byte
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
        For i = LBound(Players) To UBound(Players)
            If Not i = Me.getActivePlayer Then
                If comparePos(Players(i).getLocation, newPos) Then
                    GoTo fEnd
                End If
            End If
        Next i
        
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
    If checkMove(Players(i).getLocation, dir) Then
        movePlayer = Players(i).Move(dir)
        NextTurn
    Else
        movePlayer = False
    End If
End Function
Function saveBlocker(Br As Brick, Player As Byte) As Integer
    'returnvalues
    '0 = ok
    '1 = player have no blocker left
    '2 = position is used by another blocker
    '3 = cannot be used cause you would envelop a figure
    Dim i As Byte
    
    For i = LBound(Blocker) To UBound(Blocker)
        
    Next i
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

Function getPlayerlocation(i As Byte) As Position
    On Error GoTo OutOfIndex
    getPlayerlocation = Players(i).getLocation
    Exit Function
OutOfIndex:
    getPlayerlocation = xy2pos(255, 255)
End Function

Function getActivePlayer() As Byte
    getActivePlayer = activePlayer
End Function

Sub NextTurn()
    activePlayer = Switch(activePlayer < 3, activePlayer + 1, True, 0)
End Sub

Function checkFrontWall(pos As Position, direction As Byte) As Boolean
        'if true there are a wall in front of the position and with this direction
    Dim Stone As Brick
    Dim i As Byte
    
    For i = LBound(Blocker) To UBound(Blocker)
        Stone = Blocker(i)
        'if we found the first stone which is not placed or which have a default value as position we exiting
        If Stone.Placed = False Or Stone.Position(0) = 255 _
                                    Or Stone.Position(0) = 255 Then
            Exit For
        End If
        Select Case direction
            Case 0: 'bottom
                If Stone.Landscape = True And pos.Position(1) + 1 = Stone.Position(1) Then
                    'stone is bottom on right
                    If pos.Position(0) = Stone.Position(0) Then GoTo returnFalse
                    'stone is bottom on left
                    If pos.Position(0) = Stone.Position(0) + 1 Then GoTo returnFalse
                End If
            Case 1: 'right
                If Stone.Landscape = False And pos.Position(0) = Stone.Position(0) Then
                    'stone is upper on right
                    If pos.Position(1) = Stone.Position(1) Then GoTo returnFalse
                    'stone is lower on right
                    If pos.Position(1) + 1 = Stone.Position(1) Then GoTo returnFalse
                End If
            Case 2: 'top
                If Stone.Landscape = True And pos.Position(1) = Stone.Position(1) Then
                    'stone is top on right
                    If pos.Position(0) = Stone.Position(0) Then GoTo returnFalse
                    'stone is top on left
                    If pos.Position(0) = Stone.Position(0) + 1 Then GoTo returnFalse
                End If
            Case 3: 'left
                If Stone.Landscape = False And pos.Position(0) = Stone.Position(0) + 1 Then
                    'stone is upper on left
                    If pos.Position(1) = Stone.Position(1) Then GoTo returnFalse
                    'stone is lower on left
                    If pos.Position(1) + 1 = Stone.Position(1) Then GoTo returnFalse
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

Function checkPlaceWall(newPos As Brick) As Boolean
    'for-counter
    Dim i As Byte
    Dim i2 As Integer
    Dim i3 As Byte
    
    'state variables
    Dim ended As Boolean

    'use with care! theoretical max 8192 Byte memspace if field dimension is set to 256 don't know if VB can handle this big
    Dim RunnedFields() As Position
    ReDim RunnedFields(7, (Dimensions + 1) * (Dimensions + 1) - 1)
    '0 = aborted - touched neighbor field on first move
    '1 = field touched were we had started - ccw around
    '2 = field touched were we had started - cw around
    '3 = target wall reached
    Dim RunningResults(7) As Byte
    'saves the right and left turns while running, so we can determine if we run ccw or cw
    Dim rightLeftCount As Byte
    'saves the actual running direction
    Dim direction As Byte

    For i = 0 To 7 'fields around the new wall
        For i2 = 0 To (Dimensions + 1) * (Dimensions + 1) - 1 '-1 because we start at 0
            For i3 = 0 To 1 'directions, first to the smaller index, then to the higher
                RunnedFields(i, i2).Position(i3) = 255
            Next i3
        Next i2
    Next i

    For i = 0 To 3 'position around the new wall
        'setting startfield
        For i2 = 0 To 1
            'running ccw
            'x
            RunnedFields(i + Switch(Not i2, 0, i2, 4), 0).Position(0) = Switch( _
                                                                            i = 0 Or i = 1, newPos.Position(0), _
                                                                            i = 2 Or i = 3, newPos.Position(0) + 1 _
                                                                            )
            'y
            RunnedFields(i + Switch(Not i2, 0, i2, 4), 0).Position(1) = Switch( _
                                                                            i = 0 Or i = 3, newPos.Position(0), _
                                                                            i = 1 Or i = 2, newPos.Position(0) + 1 _
                                                                            )
        Next i2
        For i2 = 0 To 1 'directions per position
            ended = False

            'Do

            'While ended
        Next i2
    Next i
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

Function getNoOfBricks() As Byte
    getNoOfBricks = NoOfBricks
End Function

Sub create(NoOfPl As Byte, NoOfBr As Byte, Dimension As Byte)
    Dim pStarts(3) As Position
    Dim i As Byte
    
    'check input value of NoOfPl if not 4 set to 2 - started with 0
    NoOfPlayer = Switch(NoOfPl = 3, 3, True, 1)
    NoOfBricks = NoOfBr
    'if the dimension is smaller than the NoOfPlayer the field will be made bigger
    Dimensions = Switch(Dimension < NoOfPlayer, NoOfPlayer, True, Dimension)
    
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
    
    'set player starts
    For i = LBound(Players) To UBound(Players)
        If NoOfPlayer = 2 And i = 1 Then
            i = i + 1 'change 2th player's startposition to 3rd players's
                      'if two players playing
        End If
        'running create routine
        Players(i).create pStarts(i).Position(0), pStarts(i).Position(1), NoOfBricks, _
            Switch( _
                i = 0, 2, _
                i = 1, 3, _
                i = 2, 0, _
                i = 3, 1)
    Next i
    
    'reserve space for blockers
    ReDim Blocker((NoOfPlayer + 1) * (NoOfBricks + 1) - 1) '+1-1 cause we need to move one right and left because of 0 starting values
    
    'set blockers start values
    For i = 0 To UBound(Blocker)
        Blocker(i).Landscape = False
        Blocker(i).Position(0) = 255
        Blocker(i).Position(1) = 255
    Next i
End Sub
