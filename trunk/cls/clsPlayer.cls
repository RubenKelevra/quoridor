VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsPlayer"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

' cls/clsPlayer.cls - Part of Quoridor http://code.google.com/p/quoridor/
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


Private Location As Position
Private Name As String
Private Playtime As Long 'per round
Private GlobalPlaytime As Long 'per round playtime added at the end of a round
Private RemainingStones As Byte
Private TargetWall As Byte
'defines see setType()
Private TypeOfPlayer As Byte

Function getBricks() As Byte
    getBricks = RemainingStones
End Function

Sub setType(i As Byte)
    '0 = user controlled local player
    '1 = AI controlled local player
    '2... reserved for networkgames
    TypeOfPlayer = Switch(i <= 1, i, True, 0)
End Sub

Function getType() As Byte
    getType = TypeOfPlayer
End Function


Function Move(dir As Byte) As Boolean
    Select Case dir
        Case 0: 'to bottom
            Location.Position(1) = Switch( _
                                        Location.Position(1) + 1 <= 255, Location.Position(1) + 1, _
                                        True, 255)
        Case 1: 'to right
            Location.Position(0) = Switch( _
                                        Location.Position(0) + 1 <= 255, Location.Position(0) + 1, _
                                        True, 255)
        Case 2:
            Location.Position(1) = Switch( _
                                        Location.Position(1) - 1 >= 0, Location.Position(1) - 1, _
                                        True, 0)
        Case 3:
            Location.Position(0) = Switch( _
                                        Location.Position(0) - 1 >= 0, Location.Position(0) - 1, _
                                        True, 0)
    End Select
    Move = Switch(0 <= dir And dir <= 3, True, True, False)
End Function

Function getLocation() As Position
    getLocation = Location
End Function

Sub newRound(x As Byte, y As Byte, Stones As Byte, Target As Byte)
    GlobalPlaytime = GlobalPlaytime + Playtime
    
    Location.Position(0) = x
    Location.Position(1) = y
    
    RemainingStones = Stones
    Playtime = 0
    TargetWall = Target
End Sub

Function getPlayerName() As String
    getPlayerName = Name
End Function

Sub setPlayerName(s As String)
    Name = s
End Sub

Function subtractStone() As Boolean
    If RemainingStones > 0 Then
        RemainingStones = RemainingStones - 1
        subtractStone = True
    Else
        subtractStone = False
    End If
End Function

Sub create(x As Byte, y As Byte, Stones As Byte, Target As Byte)

    'set the first position of the player
    Location.Position(0) = x
    Location.Position(1) = y

    TypeOfPlayer = 0
    RemainingStones = Stones
    Playtime = 0
    TargetWall = Target
End Sub
