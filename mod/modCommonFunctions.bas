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


Function xy2pos(x As Byte, y As Byte) As Position
    'convert two values to position-type, saves usualy one line

    xy2pos.Position(0) = x
    xy2pos.Position(1) = y
End Function

