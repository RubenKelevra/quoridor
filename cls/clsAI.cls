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
Private bTouchedFieldsBitMap() As Boolean
Private Paths() As Byte
Private lMaxPathIndex As Long
'absolutMaxPL is the limit for the maximal length of a path, if this limit
'   is exceeded the path will remain.
'   this limit is used to limit the range of fields which the AI can 'see'
Private absolutMaxPL As Byte
'cutLevel is a percentage*10 value to cut off ways which are longer than the shortest founded path till now
'   will be applied again after all runs are finished
'   0 means find only paths wich are exact the same till 10-1 % length
'   255 means no limit
'   cutted paths are deleted if they exceed this limit
Private cutLevel As Byte
Private bMinPathFoundToTarget As Boolean
Private lMinPathToTarget As Long

Function genMove() As Move
    
End Function

Private Function genMoveWays(Position As Position, maxPathCount As Integer) As Byte()
    'maxPathCount is the limit of paths which are going to be returned by this function, it's limit only
    '   the returnvalue, not the searchdeep
    Dim bTargetReached As Boolean
    bTargetReached = False
    
    resetTouchedFields
    bMinPathFoundToTarget = False
    
    bTargetReached = genWays(PlayerPos(SelfPlayerNo).Position(0), PlayerPos(SelfPlayerNo).Position(1), Player2Target(SelfPlayerNo), 0, 0)
End Function

Function checkFrontWall(pos As Position, direction As Byte) As Boolean
    'copied in rev 86
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


Private Function checkMove(ByRef pos As Position, ByVal dir As Byte) As Boolean
    'copied in rev 86
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

Private Function getDirs(mainDir As Byte) As Byte()
    Dim i As Integer
    Dim i2 As Integer
    Dim bReturn(3) As Byte
    bReturn(0) = mainDir
    For i = 1 To 3
        For i2 = 0 To 3
            If Not mainDir = i2 Then
                bReturn(i) = i2
            End If
        Next i2
    Next i
    getDirs = bReturn
End Function

Private Sub genWays(ByRef x As Byte, ByRef y As Byte, ByRef dir As Byte, ByRef lastPathIndex As Long, ByVal deep As Long)
    'dir > 3 means every way, else every direction except dir
    'double check input values if you use this function directly!
    'or you might get unexpected results
    Dim vVar As Variant
    Dim i As Byte
    Dim i2 As Byte
    Dim newX As Integer
    Dim newY As Integer
    Dim PathIndex As Long
    
    'saves the index
    PathIndex = genWaysIndex
    
    'if target wall was reached with one path we going to check if we had exceed the cut level
    If bMinPathFoundToTarget Then
        If deep > lMinPathToTarget * (cutLevel / 10 + 1) Then
            Exit Sub
        End If
    End If
    
    'if pathindex has changed
    If Not lastPathIndex = PathIndex Then
        'we have to copy bitmap to our new pathindex
        For i = 0 To Dimensions
            For i2 = 0 To Dimensions
                 bTouchedFieldsBitMap(PathIndex, i, i2) = bTouchedFieldsBitMap(lastPathIndex, i, i2)
            Next i2
        Next i
        'we have also to copy our path to Paths()
        For i = 0 To deep - 1
            For i2 = 0 To 1
                Paths(PathIndex, i, i2) = Paths(lastPathIndex, i, i2)
            Next i2
        Next i
    End If
    'save that we touched the field
    bTouchedFieldsBitMap(PathIndex, x, y) = True
    'save position
    Paths(PathIndex, deep, 0) = x
    Paths(PathIndex, deep, 1) = y
    
    'when max deep isn't exceeded
    If deep < absolutMaxPL Then
        'running thru all directions
        For Each vVar In getDirs(Player2Target(SelfPlayerNo))
            'filter direction were we come from - if setted
            If i = dir Then
                GoTo Forend_genWays
            End If
            'check if move is possible
            If Not checkMove(xy2pos(x, y), i) Then
                GoTo Forend_genWays
            End If
            'calc new position
            newX = x + dirXshift(i)
            newY = y + dirYshift(i)
            'double check if out of array
            ' (normally checkMove() done this already)
            If newX < 0 Or newY < 0 Or newX > Dimensions Or newY > Dimensions Then
                GoTo Forend_genWays
            End If
            'check if we had touched the field before
            If bTouchedFieldsBitMap(PathIndex, newX, newY) Then
                GoTo Forend_genWays
            End If
            
            'increase index when creating another path
            If Not i = 0 Then
                genWaysIndex = genWaysIndex + 1
                'if there is no space left we reserver more for 1000 more paths
                If genWaysIndex >= lMaxPathIndex Then
                    lMaxPathIndex = lMaxPathIndex + 1000
                    ReDim bTouchedFieldsBitMap(lMaxPathIndex, Dimensions, Dimensions)
                    ReDim Paths(lMaxPathIndex, absolutMaxPL - 1, 1)
                End If
            End If
            'we call ourself
            genWays CByte(newX), CByte(newY), i, genWaysIndex, deep + 1
Forend_genWays:
        Next vVar
    End If
End Sub

Private Sub resetTouchedFields()
    Dim l As Long
    Dim i As Byte
    Dim i2 As Byte
    For l = 0 To lMaxPathIndex
        For i = 0 To Dimensions
            For i2 = 0 To Dimensions
                bTouchedFieldsBitMap(l, i, i2) = False
            Next i2
        Next i
    Next l
End Sub
    
Sub create(Bricks() As Brick, PlayerPositions() As Position, ControlledPlayer As Byte, Dimension As Byte, MaxPL As Byte)
    Dim iSumBlocker As Byte
    Dim i As Byte
    
    absolutMaxPL = MaxPL
    
    iSumBlocker = UBound(Bricks) - LBound(Bricks)
    SelfPlayerNo = ControlledPlayer
    Dimensions = Dimension
    PlayerMaxIndex = UBound(PlayerPositions) - LBound(PlayerPositions)
    
    'reserve space for BitMap for touched fields
    lMaxPathIndex = 10000
    
    ReDim bTouchedFieldsBitMap(lMaxPathIndex, Dimensions, Dimensions) 'bitmaps for the first 10000 ways
    resetTouchedFields
    
    'reserve space for Paths
    ReDim Paths(lMaxPathIndex, absolutMaxPL - 1, 1)
    
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