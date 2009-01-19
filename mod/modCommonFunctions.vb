Option Strict Off
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
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		getNoOfWalls = VB.Switch(NoOfPlayer > 0, 20 / (NoOfPlayer + 1) - 1, True, 0)
	End Function
	
	Function checkPos(ByRef newX As Short, ByRef newY As Short, ByRef maxXY As Byte) As Boolean
		If newX < 0 Or newY < 0 Or newX > maxXY Or newY > maxXY Then
			checkPos = False
		Else
			checkPos = True
		End If
	End Function
	
	
	Function getNextPlayer(ByRef activePlayer As Object, ByRef PlayerNo As Object) As Byte
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts activePlayer konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts PlayerNo konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		getNextPlayer = VB.Switch(activePlayer < PlayerNo, activePlayer + 1, True, 0)
	End Function
	
	Function flipDir(ByRef i As Byte) As Byte
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		flipDir = VB.Switch(i = 0, 2, i = 1, 3, i = 2, 0, i = 3, 1, True, 255)
	End Function
	
    Function xy2pos(ByVal x As Byte, ByVal y As Byte) As Point
        'convert two values to position-type, saves usualy one line
        Dim returnvalue As Point
        returnvalue.X = x
        returnvalue.Y = y

        xy2pos = returnvalue
    End Function
	
    Function comparePos(ByRef pos1 As Point, ByRef pos2 As Point) As Boolean
        If (pos1.X = pos2.X And pos1.Y = pos2.Y) Then
            comparePos = True
        Else
            comparePos = False
        End If
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
	
	'UPGRADE_NOTE: dir wurde aktualisiert auf dir_Renamed. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Function dirXshift(ByRef dir_Renamed As Object) As Short
		Select Case dir_Renamed
			Case 0 'to bottom
				dirXshift = 0
			Case 1 'to right
				dirXshift = 1
			Case 2 'to top
				dirXshift = 0
			Case 3 'to left
				dirXshift = -1
		End Select
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts dir_Renamed konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If Not (0 <= dir_Renamed And dir_Renamed <= 3) Then
			dirXshift = 255
		End If
	End Function
	
	Function Player2Target(ByRef i As Byte, ByRef NoOfPlayer As Byte) As Byte
		If NoOfPlayer = 3 Then
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Player2Target = VB.Switch(i = 0, 0, i = 1, 3, i = 2, 2, i = 3, 1, True, 255)
		Else
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Player2Target = VB.Switch(i = 0, 0, i = 1, 2, True, 255)
		End If
	End Function
	
	'UPGRADE_NOTE: dir wurde aktualisiert auf dir_Renamed. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Function dirYshift(ByRef dir_Renamed As Object) As Short
		Select Case dir_Renamed
			Case 0 'to bottom
				dirYshift = 1
			Case 1 'to right
				dirYshift = 0
			Case 2 'to top
				dirYshift = -1
			Case 3 'to left
				dirYshift = 0
		End Select
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts dir_Renamed konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If Not (0 <= dir_Renamed And dir_Renamed <= 3) Then
			dirYshift = 255
		End If
	End Function
End Module