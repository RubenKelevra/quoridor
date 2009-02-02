Option Strict On
Option Explicit On
Friend Class clsAI
	
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

    Private board As clsBoard
    Private Blocker() As clsBrick
    Private PlayerPos() As Point
	Private SelfPlayerNo As Byte
	Private Dimensions As Byte
	Private PlayerMaxIndex As Byte
	Private genWaysIndex As Integer
    Private bTouchedFieldsBitMap(,,) As Boolean
    Private Paths(,,) As Byte
	Private lMaxPathIndex As Integer
	'absolutMaxPL is the limit for the maximal length of a path, if this limit
	'   is exceeded the path will remain.
	'   this limit is used to limit the range of fields which the AI can 'see'
	Private absolutMaxPL As Byte
	'cutLevel is a percentage value to cut off ways which are longer than the shortest founded path till now
	'   will be applied again after all runs are finished
	'   0 means find only paths wich are exact the same till +1 % length
	'   255 means no limit
	'   cutted paths are deleted if they exceed this limit
	Private cutLevel As Byte
	Private bMinPathFoundToTarget As Boolean
	Private lMinPathToTarget As Integer
	
    Function addWall(ByRef Br As clsBrick) As Boolean
        Dim i As Byte
        'running thru setted Blockers to get last index of the placed ones
        For i = LBound(Blocker) To UBound(Blocker)
            If Not Blocker(i).Placed Then
                Exit For
            End If
        Next i
        Blocker(i).Placed = True
        Blocker(i).Horizontal = Br.Horizontal
        Blocker(i).Position.X = Br.Position.x
        Blocker(i).Position.Y = Br.Position.y
    End Function

    Function updatePlayerPos(ByRef PlayerNo As Byte, ByRef direction As Byte) As Byte
        '0 means okay
        '1 means dir is going out of field
        Dim newX As Short
        Dim newY As Short
        newX = dirXshift(direction) + PlayerPos(PlayerNo).X
        newY = dirYshift(direction) + PlayerPos(PlayerNo).Y
        If Not checkPos(newX, newY, Dimensions) Then
            updatePlayerPos = 1
        Else
            PlayerPos(PlayerNo).X = newX
            PlayerPos(PlayerNo).Y = newY
        End If
    End Function

    Function genMove() As clsMove
        Dim newDir As Byte
        genMoveWays(PlayerPos(SelfPlayerNo), 5)

        genMove.ErrorCode = 0
        genMove.FigureMove = True
        newDir = shift2dir(Paths(0, 0, 0) - PlayerPos(SelfPlayerNo).X, Paths(0, 0, 1) - PlayerPos(SelfPlayerNo).Y)
        genMove.MoveDirection = newDir
        updatePlayerPos(SelfPlayerNo, newDir)
    End Function

    Private Function genMoveWays(ByRef Position As Point, ByRef maxPathCount As Short) As Byte()
        'maxPathCount is the limit of paths which are going to be returned by this function, it's limit only
        '   the returnvalue, not the searchdeep
        Dim bTargetReached As Boolean
        bTargetReached = False

        resetTouchedFields()
        bMinPathFoundToTarget = False

        bTargetReached = genWays(PlayerPos(SelfPlayerNo).X, PlayerPos(SelfPlayerNo).Y, 255, 0, 0)
    End Function

    Function checkFrontWall(ByRef pos As Point, ByRef direction As Byte) As Boolean
        'copied in rev 86
        'if true there are a wall in front of the position and with this direction
        Dim stone As clsBrick
        Dim i As Byte

        For i = LBound(Blocker) To UBound(Blocker)
            stone = Blocker(i)
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
        Next i
        'no stone have been found which blocks our way
        checkFrontWall = False
        Exit Function
returnFalse:
        checkFrontWall = True
        Exit Function
    End Function

    Private Function checkMove(ByRef pos As Point, ByRef direction As Byte) As Boolean
        'copied in rev 86
        If Not checkFrontWall(pos, direction) Then
            'there is a border
            Select Case direction
                Case 0 'bottom
                    If pos.Y = Dimensions Then GoTo fEnd
                Case 1 'right
                    If pos.X = Dimensions Then GoTo fEnd
                Case 2 'top
                    If pos.Y = 0 Then GoTo fEnd
                Case 3 'left
                    If pos.X = 0 Then GoTo fEnd
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

    Private Function getDirs(ByRef mainDir As Byte) As Byte()
        Dim i As Byte
        Dim i2 As Byte
        Dim BReturn(3) As Byte
        Dim i3 As Byte
        Dim b As Boolean
        BReturn(0) = mainDir
        BReturn(1) = 255
        BReturn(2) = 255
        BReturn(3) = 255
        i3 = 1
        For i = 0 To 3
            b = True
            For i2 = 1 To 3
                If Not mainDir = i And BReturn(i2) = i Then
                    b = False
                End If
            Next i2
            If b And i3 <= 3 Then
                BReturn(i3) = i
                i3 = i3 + 1
            End If
        Next i
        getDirs = VB6.CopyArray(BReturn)
    End Function

    'UPGRADE_NOTE: dir wurde aktualisiert auf dir_Renamed. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Function genWays(ByRef x As Byte, ByRef y As Byte, ByRef dir_Renamed As Byte, ByRef lastPathIndex As Integer, ByVal deep As Integer) As Object
        'dir > 3 means every way, else every direction except dir
        'double check input values if you use this function directly!
        'or you might get unexpected results
        Dim vVar As Object
        Dim i As Byte
        Dim i2 As Byte
        Dim newX As Short
        Dim newY As Short
        Dim PathIndex As Integer

        'saves the index
        PathIndex = genWaysIndex

        'if target wall was reached with one path we going to check if we had exceed the cut level
        If bMinPathFoundToTarget Then
            If deep > lMinPathToTarget * (cutLevel / 100 + 1) Then
                Exit Function
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
            For i = 0 To deep
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
            For Each vVar In getDirs(Player2Target(SelfPlayerNo, PlayerMaxIndex))
                'filter direction were we come from - if setted
                'UPGRADE_WARNING: Die Standardeigenschaft des Objekts vVar konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If vVar = flipDir(dir_Renamed) Then
                    GoTo Forend_genWays
                End If
                'calc new position
                newX = x + dirXshift(vVar)
                newY = y + dirYshift(vVar)
                'double check if out of array
                ' (normally checkMove() done this already)
                If Not checkPos(newX, newY, Dimensions) Then
                    GoTo Forend_genWays
                End If
                'check if we had touched the field before
                If bTouchedFieldsBitMap(PathIndex, newX, newY) Then
                    GoTo Forend_genWays
                End If

                'increase index when creating another path
                'UPGRADE_WARNING: Die Standardeigenschaft des Objekts vVar konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If Not vVar = 0 Then
                    genWaysIndex = genWaysIndex + 1
                    'if there is no space left we reserver more for 1000 more paths
                    If genWaysIndex >= lMaxPathIndex Then
                        lMaxPathIndex = lMaxPathIndex + 1 '000 'testing
                        ReDim bTouchedFieldsBitMap(lMaxPathIndex, Dimensions, Dimensions)
                        ReDim Paths(lMaxPathIndex, absolutMaxPL, 1)
                    End If
                End If
                'we call ourself
                'UPGRADE_WARNING: Die Standardeigenschaft des Objekts vVar konnte nicht aufgel�st werden. Klicken Sie hier f�r weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                genWays(CByte(newX), CByte(newY), CByte(vVar), PathIndex, deep + 1)
Forend_genWays:
            Next vVar
        End If
    End Function

    Sub delPath()
        Dim i As Byte
        Dim i2 As Byte
        For i = 0 To absolutMaxPL
            Paths(genWaysIndex, i, 0) = 255
            Paths(genWaysIndex, i, 1) = 255
        Next i
        For i = 0 To Dimensions
            For i2 = 0 To Dimensions
                bTouchedFieldsBitMap(genWaysIndex, i, i2) = False
            Next i2
        Next i
        genWaysIndex = genWaysIndex - 1
    End Sub

    Private Sub resetTouchedFields()
        Dim l As Integer
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

    Sub create(ByRef Bricks() As clsBrick, ByRef PlayerPositions() As Point, ByRef ControlledPlayer As Byte, ByRef Dimension As Byte, ByRef MaxPL As Byte)
        Dim iSumBlocker As Byte
        Dim i As Byte

        absolutMaxPL = MaxPL

        iSumBlocker = UBound(Bricks) - LBound(Bricks)
        SelfPlayerNo = ControlledPlayer
        Dimensions = Dimension
        PlayerMaxIndex = UBound(PlayerPositions) - LBound(PlayerPositions)

        'reserve space for BitMap for touched fields
        lMaxPathIndex = 0

        ReDim bTouchedFieldsBitMap(lMaxPathIndex, Dimensions, Dimensions) 'bitmaps for the first 10000 ways
        resetTouchedFields()

        'reserve space for Paths
        ReDim Paths(lMaxPathIndex, absolutMaxPL, 1)

        'reserve space for playerpositions
        ReDim PlayerPos(PlayerMaxIndex)
        'reserver space for blocker
        ReDim Blocker(iSumBlocker)

        For i = 0 To PlayerMaxIndex
            PlayerPos(i).X = PlayerPositions(i).X
            PlayerPos(i).Y = PlayerPositions(i).Y
        Next i
        For i = 0 To iSumBlocker
            With Blocker(i)
                .Horizontal = Bricks(i).Horizontal
                .Placed = Bricks(i).Placed
                .Position.X = Bricks(i).Position.x
                .Position.Y = Bricks(i).Position.x
            End With
        Next i

        'generate

    End Sub
End Class