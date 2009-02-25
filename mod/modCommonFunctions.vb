Option Strict On
Option Explicit On
Imports VB = Microsoft.VisualBasic
Module modCommonFunctions
	
	' mod/modCommonFunctions.bas - Part of Quoridor http://code.google.com/p/quoridor/
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
	
	Function getNoOfWalls(ByRef NoOfPlayer As Byte) As Byte
        getNoOfWalls = CByte(VB.Switch(NoOfPlayer > 0, 20 / (NoOfPlayer + 1) - 1, True, 0))
    End Function

    'fixme: translation

    Function betrag(ByRef input As Short) As Byte
        If input < 0 Then
            Return CByte(input * -1)
        End If
        Return CByte(input)
    End Function

    Function betrag(ByRef input As Integer) As Integer
        If input < 0 Then
            Return input * -1
        End If
        Return input
    End Function

    Function checkPos(ByRef newX As Short, ByRef newY As Short, ByRef maxXY As Byte) As Boolean
        If newX < 0 Or newY < 0 Or newX > maxXY Or newY > maxXY Then
            checkPos = False
        Else
            checkPos = True
        End If
    End Function

    Function getNextPlayer(ByVal activePlayer As Byte, ByVal PlayerNo As Byte) As Byte
        getNextPlayer = CByte(VB.Switch(activePlayer < PlayerNo, activePlayer + 1, True, 0))
    End Function

    Function flipDir(ByRef i As Byte) As Byte
        flipDir = CByte(VB.Switch(i = 0, 2, i = 1, 3, i = 2, 0, i = 3, 1, True, 255))
    End Function

    Function xy2position(ByVal x As Byte, ByVal y As Byte) As Position
        'convert two values to position-type, saves usualy one line
        xy2position.X = x
        xy2position.Y = y
    End Function

    Function comparePos(ByRef pos1 As Position, ByRef pos2 As Position) As Boolean
        If pos1.X = pos2.X Then
            If pos1.Y = pos2.Y Then
                Return True
            End If
        End If
        Return False
    End Function

    Function shift2dir(ByRef Xshift As Short, ByRef Yshift As Short) As Byte
        If Xshift = 0 And Yshift = 1 Then 'to bottom
            shift2dir = 0
        ElseIf Xshift = 1 And Yshift = 0 Then  'to right
            shift2dir = 1
        ElseIf Xshift = 0 And Yshift = -1 Then  'to top
            shift2dir = 2
        ElseIf Xshift = -1 And Yshift = 0 Then  'to left
            shift2dir = 3
        Else
            shift2dir = 255
        End If
    End Function

    Function dirXshift(ByRef direction As Byte) As Short
        Select Case direction
            Case 0 'to bottom
                dirXshift = 0
            Case 1 'to right
                dirXshift = 1
            Case 2 'to top
                dirXshift = 0
            Case 3 'to left
                dirXshift = -1
        End Select
        If Not (0 <= direction And direction <= 3) Then
            dirXshift = 255
        End If
    End Function



    Function Player2Target(ByRef i As Byte, ByRef NoOfPlayer As Byte) As Byte
        If NoOfPlayer = 3 Then
            Player2Target = CByte(VB.Switch(i = 0, 0, i = 1, 3, i = 2, 2, i = 3, 1, True, 255))
        Else
            Player2Target = CByte(VB.Switch(i = 0, 0, i = 1, 2, True, 255))
        End If
    End Function

    Function dirYshift(ByRef direction As Byte) As Short
        Select Case direction
            Case 0 'to bottom
                dirYshift = 1
            Case 1 'to right
                dirYshift = 0
            Case 2 'to top
                dirYshift = -1
            Case 3 'to left
                dirYshift = 0
        End Select

        If Not (0 <= direction And direction <= 3) Then
            dirYshift = 255
        End If

    End Function

End Module