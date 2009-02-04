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

    Enum BrickRating
        firstNearField = 12
        firstField = 10
        firstFarField = 8

        secondNearField = 6
        secondField = 5
        secondFarField = 4
    End Enum

    Private B As Byte
    Private B1 As Byte
    Private B2 As Byte
    Private i As Integer
    Private i1 As Integer

    Private Sub updateBrickInformation(Optional ByVal bFirstRun As Boolean = False)
        Static BNextBrickIndex As Byte 'next index which may be used, on last run

        If Not isDim(mNodeList) Then
            Exit Sub
        End If

        If bFirstRun Then
            If mbFullPlayer Then
                mBNoOfMatrices = mBoard.getNoOfPlayer
            Else
                mBNoOfMatrices = 0
            End If
            ReDim mBrickMatrix(mBNoOfMatrices, mBoard.getDimension, mBoard.getDimension)
        Else

        End If

        If Not mbFullPlayer Then
            'this KI is only for testing if any path is possible to reach target
            'so we don't need to add a complex raiting to our matrix
            For i = 0 To mBoard.getDimension
                For i1 = 0 To mBoard.getDimension
                    mBrickMatrix(0, i, i1) = 10
                Next
            Next
            Exit Sub
        End If

        For B = BNextBrickIndex To CByte(UBound(mBoard.Blocker) - LBound(mBoard.Blocker))
            With mBoard.Blocker(B)
                If Not .Placed Then
                    BNextBrickIndex = B
                    Exit For
                End If
                'write ratings in the matrices
                For B1 = 0 To mBNoOfMatrices
                    B2 = Player2Target(B1, mBoard.getNoOfPlayer)

                    If .Horizontal Then
                        If B2 Mod 2 = 0 Then 'to bottom or top

                        ElseIf B2 = 1 Then 'to right

                        Else 'b2 = 3 to left

                        End If
                    Else
                        If B2 Mod 2 = 1 Then 'to right or left

                        ElseIf B2 = 0 Then 'to bottom

                        Else 'to top

                        End If
                    End If
                Next
            End With
        Next

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