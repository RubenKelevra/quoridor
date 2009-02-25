Option Strict On
Option Explicit On
Imports VB = Microsoft.VisualBasic

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

    Protected Friend mBoard As clsBoard
    Private mBSelfPlayerNo As Byte
    Private mbFullPlayer As Boolean
    Private mNodeList() As clsSimpleHeap
    Private mBrickMatrix(,,) As Byte
    Private mBNoOfMatrices As Byte
    Private tempBrickRating(3, 3) As Byte 'used in move
    Private usNodeLocationCache(,) As UShort 'save idexes of nodes in a array which represents the structure of the board

    Enum Rating As Byte
        'rating for fields around a brick
        'if plane
        ' y x | x y
        ' y x | x y
        'where y is second field and x is first field

        'if direction to bottom
        ' w y | y w
        ' x z | z y
        'where:
        ' w is second near
        ' y is first near
        ' x is second far
        ' z is first far

        firstNearField = 16
        firstField = 20
        firstFarField = 24

        secondNearField = 6
        secondField = 10
        secondFarField = 14
    End Enum

    Private B As Byte
    Private B1 As Byte
    Private B2 As Byte

    Private Sub resetTempBrickRating()
        'Private resetTempBrickRating
        'resets the brick-blocking-p osition-cache
        For B = 0 To 3
            For B1 = 0 To 3
                tempBrickRating(B, B1) = 0
            Next B1
        Next B
    End Sub

    Private Function calcPoints(ByRef X As Byte, ByRef Y As Byte, ByRef direction As Byte, ByRef player As Byte) As UInteger
        Dim targetX As Short
        Dim targetY As Short
        targetX = X + dirXshift(direction)
        targetY = Y + dirYshift(direction)

        'invalid position or invalid player index
        If (Not checkPos(targetX, targetY, mBoard.getDimension)) Or player > mBoard.getNoOfPlayer Then
            Return UInteger.MaxValue
        End If

        Return CUInt((mBrickMatrix(player, X, Y) + mBrickMatrix(player, targetX, targetY)) / 2)
    End Function

    Private Function astar(ByVal BPlayerNo As Byte, ByVal bBrickTestRun As Boolean) As Integer
        'note: this fuction is build to be threadsave
        Dim BIndexOfNodes As Byte
        Dim B As Byte
        Dim SNeighborX As Short
        Dim SNeighborY As Short
        Dim usNeighborValue As UShort
        Dim pPlayerLocation As Position
        Dim usTentativeNode As UShort
        'If PlayerNo >= mBNoOfMatrices And mbFullPlayer Then 'there are invalid runparametres
        '    Return 2 'FIXME: There should be a general errorcode
        'End If
        If Not bBrickTestRun Then
            BIndexOfNodes = BPlayerNo
        Else
            BIndexOfNodes = 0
        End If

        With mNodeList(BIndexOfNodes)
            'init open/closed node list
            .init()
            'get startpoint
            pPlayerLocation = mBoard.getPlayerLocation(BPlayerNo)
            'get heuristic way to target
            astar = calcHeuristic(pPlayerLocation.X, pPlayerLocation.Y, BPlayerNo)
            If astar > 0 Then
                Err.Raise(vbObjectError, "Internal AI Error", "Heuristic failed, minus values are not allowed")
                Exit Function
            End If
            'add startpoint
            usTentativeNode = .addOpen(pPlayerLocation.X, pPlayerLocation.Y, 0, 0, CUInt(astar), CUShort(astar))
            If usTentativeNode = UShort.MaxValue Then
                'reset astar var
                astar = 0
                Err.Raise(vbObjectError, "Internal AI Error", "Can't add startnode to open list")
                Exit Function
            End If
            'reset astar var
            astar = 0
            'add startnode to location cache
            usNodeLocationCache(pPlayerLocation.X, pPlayerLocation.Y) = usTentativeNode

            While .OpenNodesRemaning
                usTentativeNode = .getMin
                If isTarget(.Nodes(usTentativeNode)) Then
                    'fixme
                End If
                'walk thru all directions
                For B = 0 To 3
                    'get neighbor position
                    SNeighborX = .Nodes(usTentativeNode).B_X + dirXshift(B)
                    SNeighborY = .Nodes(usTentativeNode).B_Y + dirYshift(B)
                    'check neigbor position
                    If checkPos(SNeighborX, SNeighborY, mBoard.getDimension) Then
                        'get node-index from location cache
                        usNeighborValue = usNodeLocationCache(SNeighborX, SNeighborY)
                        'if there is a valid index
                        If Not usNeighborValue = UShort.MaxValue Then
                            If .Nodes(usNeighborValue).bClosed Then
                                Continue For
                            End If

                        Else
                            'if it's not in cache, we add it to open and to cache
                            usNodeLocationCache(SNeighborX, SNeighborY) = .addOpen(CByte(SNeighborX), CByte(SNeighborY), usTentativeNode, 0, 0, 0) 'fixme wrong values
                        End If
                    End If
                Next B

                .setClosed(usTentativeNode)
            End While
        End With
    End Function

    Private Function isTarget(ByRef node As AstarData) As Boolean
        'fixme
        Return False
    End Function

    Public Function move() As Integer
        'Public move As Integer
        'Do all decitions as fast as possible for a turn
        ' - Returns a errorcode
        Static BlastDimension As Byte = Byte.MaxValue

        'update intern brick information if some bricks were setted
        updateBrickInformation()

        'init space for locationcache, clean it
        B = mBoard.getDimension
        If Not B = BlastDimension Then
            ReDim usNodeLocationCache(B, B)
            BlastDimension = B
        Else
            For B = 0 To BlastDimension
                For B1 = 0 To BlastDimension
                    usNodeLocationCache(B, B1) = UShort.MaxValue
                Next B1
            Next B
        End If

        ' --- we need to ran astar to get the best way to target for all players ---



        'running thru all fields of board - ignoring the fields with a true in BlockedPositions
        For B = 0 To CByte(mBoard.getDimension - 1) 'X
            For B1 = 0 To CByte(mBoard.getDimension - 1) 'Y
                For B2 = 0 To 1 '1 = horizontal
                    If B2 = 1 Then
                        'FIXME: do stuff
                    End If
                Next B2
            Next B1
        Next B
    End Function

    Private Function checkBlocked(ByVal x As Byte, ByVal y As Byte, ByVal horizontal As Boolean) As Boolean
        If x < mBoard.getDimension And y < mBoard.getDimension Then

        End If
    End Function

    Private Sub addBrickRating(ByRef Field() As Byte)
        'fixme: add a cut out from updatebrickinformation
    End Sub

    Private Function calcHeuristic(ByVal startX As Byte, ByVal startY As Byte, ByVal playerNo As Byte) As Integer
        'Private calcHeuristic As Integer
        'calculates the shortest possible path to target
        ' - [IN] ByVal startX As Byte: Startposition
        ' - [IN] ByVal startY As Byte: Startposition
        ' - [IN] ByVal playerNo As Byte: Index of player in playerarray
        ' - shortest possible path
        Select Case mBoard.Players(playerNo).getTarget
            Case 0 'to bottom
                Return betrag(CShort(startY - (mBoard.getDimension + 1)))
            Case 1 'to right
                Return betrag(CShort(startX - (mBoard.getDimension + 1)))
            Case 2 'to top
                Return mBoard.getDimension + 1 - betrag(CShort(startY - mBoard.getDimension))
            Case 3 'to left
                Return mBoard.getDimension + 1 - betrag(CShort(startX - mBoard.getDimension))
        End Select
    End Function

    Private Sub updateBrickInformation(Optional ByVal bFirstRun As Boolean = False)
        'Private updateBrickInformation
        'calc the brick ratings to the matrices to be used as database from astar, only adding the
        'new bricks to speed up this stuff
        ' - [IN] Optional ByVal bFirstRun As Boolean = False: Do a complete reset of all matrices _and_ redim if true 
        Static BNextBrickIndex As Byte = 0 'next index which may be used, on last run

        If mNodeList Is Nothing Then
            Exit Sub
        End If

        If bFirstRun Then

            'allocate memory
            If mbFullPlayer Then
                mBNoOfMatrices = mBoard.getNoOfPlayer
            Else
                mBNoOfMatrices = 0
            End If
            ReDim mBrickMatrix(mBNoOfMatrices, mBoard.getDimension, mBoard.getDimension)

            'plain 10 points values to all fields
            For B = 0 To mBNoOfMatrices
                For B1 = 0 To mBoard.getDimension
                    For B2 = 0 To mBoard.getDimension
                        mBrickMatrix(B, B1, B2) = 10
                    Next B2
                Next B1
            Next B

            If Not mbFullPlayer Then
                'this KI is only for testing if any path is possible to reach target
                'so we don't need to add a complex rating to our matrix
                Exit Sub
            Else
                'we need to add outlining walls to our rating matrices
                For B = 0 To mBNoOfMatrices
                    For B1 = 0 To mBoard.getDimension
                        For B2 = 0 To mBoard.getDimension
                            If B1 = 0 Or B1 = mBoard.getDimension Then
                                mBrickMatrix(B, B1, B2) += Rating.firstField
                                mBrickMatrix(B, B2, B1) += Rating.firstField
                            ElseIf B1 = 1 Or B1 = mBoard.getDimension - 1 Then
                                mBrickMatrix(B, B1, B2) += Rating.secondField
                                mBrickMatrix(B, B2, B1) += Rating.secondField
                            End If
                        Next B2
                    Next B1
                Next B
            End If
        End If

        For B = BNextBrickIndex To CByte(UBound(mBoard.Blocker) - LBound(mBoard.Blocker))
            With mBoard.Blocker(B)
                BNextBrickIndex = CByte(B + 1)
                If Not .Placed Then 'this first unused item
                    Exit For
                End If
                'write ratings in the matrices
                For B1 = 0 To mBNoOfMatrices
                    B2 = Player2Target(B1, mBoard.getNoOfPlayer)

                    If .Horizontal Then
                        If B2 Mod 2 = 0 Then 'to bottom or top
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstField
                            End If
                            If checkPos(CShort(.Position.X), CShort(.Position.Y + 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y + 1) += Rating.secondField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y + 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y + 1) += Rating.secondField
                            End If
                            If checkPos(CShort(.Position.X), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstField
                            End If
                            If checkPos(CShort(.Position.X), CShort(.Position.Y - 2), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 2) += Rating.secondField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y - 2), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 2) += Rating.secondField
                            End If
                        ElseIf B2 = 1 Then 'to right
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstNearField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstFarField
                            End If
                            If checkPos(CShort(.Position.X), CShort(.Position.Y + 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y + 1) += Rating.secondNearField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y + 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y + 1) += Rating.secondFarField
                            End If
                            If checkPos(CShort(.Position.X), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstNearField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstFarField
                            End If
                            If checkPos(CShort(.Position.X), CShort(.Position.Y - 2), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 2) += Rating.secondNearField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y - 2), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 2) += Rating.secondFarField
                            End If
                        Else 'b2 = 3 to left
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstFarField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstNearField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y + 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y + 1) += Rating.secondNearField
                            End If
                            If checkPos(CShort(.Position.X), CShort(.Position.Y + 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y + 1) += Rating.secondFarField
                            End If
                            If checkPos(CShort(.Position.X), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstFarField
                            End If
                            If checkPos(CShort(.Position.X), CShort(.Position.Y - 2), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 2) += Rating.secondFarField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstNearField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y - 2), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 2) += Rating.secondNearField
                            End If
                        End If
                    Else 'not .Horizontal
                        If B2 Mod 2 = 1 Then 'to right or left
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstField
                            End If
                            If checkPos(CShort(.Position.X), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstField
                            End If
                            If checkPos(CShort(.Position.X - 1), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y) += Rating.secondField
                            End If
                            If checkPos(CShort(.Position.X - 1), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y - 1) += Rating.secondField
                            End If
                            If checkPos(CShort(.Position.X + 2), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y) += Rating.secondField
                            End If
                            If checkPos(CShort(.Position.X + 2), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y - 1) += Rating.secondField
                            End If
                        ElseIf B2 = 0 Then 'to bottom
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstFarField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(CShort(.Position.X), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstNearField
                            End If
                            If checkPos(CShort(.Position.X - 1), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y) += Rating.secondFarField
                            End If
                            If checkPos(CShort(.Position.X - 1), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y - 1) += Rating.secondNearField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstFarField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstNearField
                            End If
                            If checkPos(CShort(.Position.X + 2), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y) += Rating.secondFarField
                            End If
                            If checkPos(CShort(.Position.X + 2), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y - 1) += Rating.secondNearField
                            End If
                        Else 'to top
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstNearField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(CShort(.Position.X), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstFarField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstNearField
                            End If
                            If checkPos(CShort(.Position.X + 1), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstFarField
                            End If
                            If checkPos(CShort(.Position.X - 1), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y) += Rating.secondNearField
                            End If
                            If checkPos(CShort(.Position.X - 1), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y - 1) += Rating.secondFarField
                            End If
                            If checkPos(CShort(.Position.X + 2), CShort(.Position.Y), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y) += Rating.secondNearField
                            End If
                            If checkPos(CShort(.Position.X + 2), CShort(.Position.Y - 1), mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y - 1) += Rating.secondFarField
                            End If
                        End If
                    End If
                Next B1
            End With
        Next B
    End Sub

    Public Sub New(ByRef Board As clsBoard, ByVal bFullPlayer As Boolean)
        'Public New
        'This function allocate the memory for the AI
        ' - [IN] ByRef Board As clsBoard: Reference of the complete gameboard, to be able to access its' this functions
        ' - [IN] ByVal bFullPlayer As Boolean: Switch, if false this KI is ready to do very fast checks with low memory
        '                                   footprint if it's still possible for every player to reach its' target, used to
        '                                   implement the rule 'never sourround a player as such that he isn't be able to
        '                                   reach(its) ' target
        mBoard = Board
        mbFullPlayer = bFullPlayer

        'do init space for our open and close list
        If bFullPlayer Then
            ReDim mNodeList(Board.getNoOfPlayer)
        Else
            ReDim mNodeList(0)
        End If
        'init nodelist
        For B = 0 To CByte(UBound(mNodeList) - LBound(mNodeList))
            mNodeList(B) = New clsSimpleHeap
            mNodeList(B).init()
        Next B

        updateBrickInformation(True)
    End Sub
End Class