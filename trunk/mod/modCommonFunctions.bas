Attribute VB_Name = "modCommonFunctions"
Option Explicit

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

Function flipDir(i As Byte) As Byte
    flipDir = Switch( _
                i = 0, 2, _
                i = 1, 3, _
                i = 2, 0, _
                i = 3, 1 _
                )
End Function

Function xy2pos(ByVal x As Byte, ByVal y As Byte) As Position
    'convert two values to position-type, saves usualy one line
    Dim returnvalue As Position
    returnvalue.Position(0) = x
    returnvalue.Position(1) = y
    xy2pos = returnvalue
End Function

Function comparePos(pos1 As Position, pos2 As Position) As Boolean
    If (pos1.Position(0) = pos2.Position(0) And pos1.Position(1) = pos2.Position(1)) Then
        comparePos = True
    Else
        comparePos = False
    End If
End Function

Function dirXshift(dir) As Integer
    Select Case dir
        Case 0: 'to bottom
            dirXshift = 0
        Case 1: 'to right
            dirXshift = 1
        Case 2: 'to top
            dirXshift = 0
        Case 3: 'to bottom
            dirXshift = -1
    End Select
    If Not (dir <= 0 And dir <= 3) Then
        dirXshift = 255
    End If
End Function

Function Player2Target(i As Byte) As Byte
    Player2Target = Switch( _
                            i = 0, 0, _
                            i = 1, 3, _
                            i = 2, 2, _
                            i = 3, 1)
End Function

Function dirYshift(dir) As Integer
    Select Case dir
        Case 0: 'to bottom
            dirYshift = 1
        Case 1: 'to right
            dirYshift = 0
        Case 2: 'to top
            dirYshift = -1
        Case 3: 'to bottom
            dirYshift = 0
    End Select
    If Not (dir <= 0 And dir <= 3) Then
        dirYshift = 255
    End If
End Function

