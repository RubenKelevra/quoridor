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
Private genWaysIndex As Long

Private Function genMovesWays(Position As Position, cutLevel As Byte, absolutMaxPL As Byte, maxPathCount As Integer) As Byte()
    'cutLevel is a percentage*10 value to cut off ways which are longer than the shortest founded path till now
    '   will be applied again after all runs are finished
    '   0 means find only paths wich are exact the same till 10-1 % length
    '   255 means no limit
    '   cutted paths are deleted if they exceed this limit
    'absolutMaxPL is the limit for the maximal length of a path, if this limit
    '   is exceeded the path will remain.
    '   this limit is used to limit the range of fields which the AI can 'see'
    'maxPathCount is the limit of paths which are going to be returned by this function, it's limit only
    '   the returnvalue, not the searchdeep
    
End Function
Private Function genWays(x As Byte, y As Byte, Dir As Byte, deep As Long, ByRef cutLevel As Byte, ByRef absolutMaxPL As Byte) As Byte()
    'dir < 3 means every way, else every direction except dir
    'double check input values if you use this function directly!
    'or you might get unexpected results
    Dim aNodePosition As Variant
    Dim aNodesPositions() As Position
    Dim i As Byte
    Dim newX As Integer
    Dim newY As Integer
    
    'running thru all directions
    For i = 0 To 3
        'filter direction which we come from - if set
        If i = Dir Then
            GoTo Forend_genWays
        End If
        'calc new position
        newX = x + dirXshift(i)
        newY = y + dirYshift(i)
        'if out of array
        If newX < 0 Or newY < 0 Or newX > Dimensions Or newY > Dimensions Then
            GoTo Forend_genWays
        End If
        'we call ourself
        If deep < absolutMaxPL Then
            'aNodesPositions = genWays(CByte(newX), CByte(newY), i, deep + 1, cutLevel, absolutMaxPL)
        End If
        'For Each aNodePosition In aNodesPositions
        
        'Next aNodePosition
        
Forend_genWays:
    Next i
End Function
    
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
