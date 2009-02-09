Option Strict Off 'FIXME: can't be switched on cause of a bug in isDim()
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

    Enum Rating
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

    Public Function move() As Integer



    End Function
    Private Sub updateBrickInformation(Optional ByVal bFirstRun As Boolean = False)
        Static BNextBrickIndex As Byte = 0 'next index which may be used, on last run

        If Not isDim(mNodeList) Then
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
                'so we don't need to add a complex raiting to our matrix
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
                If Not .Placed Then 'this first unused item
                    BNextBrickIndex = B
                    Exit For
                End If
                'write ratings in the matrices
                For B1 = 0 To mBNoOfMatrices
                    B2 = Player2Target(B1, mBoard.getNoOfPlayer)

                    If .Horizontal Then
                        If B2 Mod 2 = 0 Then 'to bottom or top
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(.Position.X + 1, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstField
                            End If
                            If checkPos(.Position.X, .Position.Y + 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y + 1) += Rating.secondField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y + 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y + 1) += Rating.secondField
                            End If
                            If checkPos(.Position.X, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstField
                            End If
                            If checkPos(.Position.X, .Position.Y - 2, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 2) += Rating.secondField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y - 2, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 2) += Rating.secondField
                            End If
                        ElseIf B2 = 1 Then 'to right
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstNearField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(.Position.X + 1, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstFarField
                            End If
                            If checkPos(.Position.X, .Position.Y + 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y + 1) += Rating.secondNearField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y + 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y + 1) += Rating.secondFarField
                            End If
                            If checkPos(.Position.X, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstNearField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstFarField
                            End If
                            If checkPos(.Position.X, .Position.Y - 2, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 2) += Rating.secondNearField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y - 2, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 2) += Rating.secondFarField
                            End If
                        Else 'b2 = 3 to left
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstFarField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(.Position.X + 1, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstNearField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y + 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y + 1) += Rating.secondNearField
                            End If
                            If checkPos(.Position.X, .Position.Y + 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y + 1) += Rating.secondFarField
                            End If
                            If checkPos(.Position.X, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstFarField
                            End If
                            If checkPos(.Position.X, .Position.Y - 2, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 2) += Rating.secondFarField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstNearField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y - 2, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 2) += Rating.secondNearField
                            End If
                        End If
                    Else 'not .Horizontal
                        If B2 Mod 2 = 1 Then 'to right or left
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(.Position.X + 1, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstField
                            End If
                            If checkPos(.Position.X, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstField
                            End If
                            If checkPos(.Position.X - 1, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y) += Rating.secondField
                            End If
                            If checkPos(.Position.X - 1, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y - 1) += Rating.secondField
                            End If
                            If checkPos(.Position.X + 2, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y) += Rating.secondField
                            End If
                            If checkPos(.Position.X + 2, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y - 1) += Rating.secondField
                            End If
                        ElseIf B2 = 0 Then 'to bottom
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstFarField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(.Position.X, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstNearField
                            End If
                            If checkPos(.Position.X - 1, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y) += Rating.secondFarField
                            End If
                            If checkPos(.Position.X - 1, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y - 1) += Rating.secondNearField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstFarField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstNearField
                            End If
                            If checkPos(.Position.X + 2, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y) += Rating.secondFarField
                            End If
                            If checkPos(.Position.X + 2, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y - 1) += Rating.secondNearField
                            End If
                        Else 'to top
                            mBrickMatrix(B1, .Position.X, .Position.Y) += Rating.firstNearField
                            'FIXME: correct if left top field is 0,0
                            If checkPos(.Position.X, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X, .Position.Y - 1) += Rating.firstFarField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y) += Rating.firstNearField
                            End If
                            If checkPos(.Position.X + 1, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 1, .Position.Y - 1) += Rating.firstFarField
                            End If
                            If checkPos(.Position.X - 1, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y) += Rating.secondNearField
                            End If
                            If checkPos(.Position.X - 1, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X - 1, .Position.Y - 1) += Rating.secondFarField
                            End If
                            If checkPos(.Position.X + 2, .Position.Y, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y) += Rating.secondNearField
                            End If
                            If checkPos(.Position.X + 2, .Position.Y - 1, mBoard.getDimension) Then
                                mBrickMatrix(B1, .Position.X + 2, .Position.Y - 1) += Rating.secondFarField
                            End If
                        End If
                    End If
                Next B1
            End With
        Next B
    End Sub
    Public Sub New(ByRef Board As clsBoard, ByVal bFullPlayer As Boolean)
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
        Next

        updateBrickInformation(True)
    End Sub
End Class