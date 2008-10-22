VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsAI"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

' cls/clsAI.cls - Part of Quoridor http://code.google.com/p/quoridor/
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

Private Blocker() As Brick
Private PlayerPos() As Position
Private SelfPlayerNo As Byte
Private Dimensions As Byte
Private PlayerMaxIndex As Byte
    
Sub create(Bricks() As Brick, PlayerPositions() As Position, ControlledPlayer As Byte, Dimension As Byte)
    Dim iSumBlocker As Byte
    Dim i As Byte
    
    iSumBlocker = UBound(Bricks) - LBound(Bricks)
    SelfPlayerNo = ControlledPlayer
    Dimensions = Dimension
    PlayerMaxIndex = UBound(PlayerPositions) - LBound(PlayerPositions)
    
    'reserve space for playerpositions
    ReDim PlayerPos(PlayerMaxIndex)
    'reserver space for blocker
    ReDim Blocker(iSumBlocker)
    
    For i = 0 To PlayerMaxIndex
        PlayerPos(i).Position(0) = PlayerPositions(i).Position(0)
    Next i
    For i = 0 To iSumBlocker
        With Blocker(i)
            .Landscape = Bricks(i).Landscape
            .Placed = Bricks(i).Placed
            .Position(0) = Bricks(i).Position(0)
            .Position(1) = Bricks(i).Position(0)
        End With
    Next i
    
    'generate
    
End Sub
