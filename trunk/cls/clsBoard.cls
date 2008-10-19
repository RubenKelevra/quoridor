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

Function movePlayer(i As Byte, dir As Byte) As Boolean
    movePlayer = Players(i).Move(dir)
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
    activePlayer = Switch(activePlayer < 3, activePlayer, True, 0)
End Sub

Function checkFrontWall(Pos As Position, direction As Byte) As Boolean
        'if true there are a wall in front of the position and with this direction
    Dim Stone As Brick
    
    For Each Stone In Blocker()
        If (Stone.Position(0) = 255 Or Stone.Position(0) = 255) Then
            Exit For
        End If
        'delete this
        'If (st.Position = pos.Position And ((st.Landscape And direction = 2) Or (Not st.Landscape And direction = 1))) Then
        'if


    Next Stone

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
    '0 = aborted - touched neighbor field
    '1 = field touched were we had started - ccw around
    '2 = field touched were we had started - cw around
    '3 = target wall reached
    Dim RunningResults(7) As Byte
    'saves the right and left turns while running, so we can determine if we run ccw or cw
    Dim rightLeftCount As Byte
    'saves the actual running direction
    Dim direction As Byte

    For i = 0 To 7 'fields around the
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
    pStarts(0).Position(0) = Dimension / 2
    pStarts(1).Position(1) = Dimension / 2
    pStarts(0).Position(1) = 0
    pStarts(3).Position(0) = 0
    pStarts(1).Position(0) = Dimension
    pStarts(2).Position(1) = Dimension
    'subtract to get the other line near the center for the mirrored player
    pStarts(2).Position(0) = Dimension - (Dimension / 2)
    pStarts(3).Position(1) = Dimension - (Dimension / 2)
    
    'set player starts
    For i = LBound(Players) To UBound(Players)
        If NoOfPlayer = 2 And i = 1 Then
            i = i + 1 'change 2th player's startposition to 3rd players's
                      'if two players playing
        End If
        'running create routine
        Call Players(i).create(pStarts(i).Position(0), pStarts(i).Position(1), NoOfBricks, _
            Switch( _
                i = 0, 2, _
                i = 1, 3, _
                i = 2, 0, _
                i = 3, 1))
    Next i
    
    'reserve space for blockers
    ReDim Blocker(NoOfPlayer * NoOfBricks)
    
    'set blockers start values
    For i = 0 To UBound(Blocker)
        Blocker(i).Landscape = False
        Blocker(i).Position(0) = 255
        Blocker(i).Position(1) = 255
    Next i
End Sub
