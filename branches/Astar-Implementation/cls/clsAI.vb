Option Strict Off
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

    Private B As Byte
    Private B1 As Byte
    Private ui As UInteger
    Private ui1 As UInteger

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
            For ui = 0 To mBoard.getDimension
                For ui1 = 0 To mBoard.getDimension
                    mBrickMatrix(0, ui, ui1) = 10
                Next
            Next
            Exit Sub
        End If

        For B = BNextBrickIndex To UBound(mBoard.Blocker) - LBound(mBoard.Blocker)
            With mBoard.Blocker(B)
                If Not .Placed Then
                    BNextBrickIndex = B
                    Exit For
                End If
                'write raitings in the matrices
                For B1 = 0 To mBNoOfMatrices

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
        For B = 0 To UBound(mNodeList) - LBound(mNodeList)
            mNodeList(B) = New clsSimpleHeap
            mNodeList(B).init()
        Next

        updateBrickInformation(True)
    End Sub
End Class