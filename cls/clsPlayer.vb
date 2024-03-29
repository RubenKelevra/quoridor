Option Strict On
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class clsPlayer
	
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
	Private Playtime As Integer 'per round
	Private GlobalPlaytime As Integer 'per round playtime added at the end of a round
	Private RemainingStones As Byte
	'as direction number
	Private TargetWall As Byte
	'defines see setType()
	Private TypeOfPlayer As Byte
	Private AI As clsAI
	
	Function getBricks() As Byte
		getBricks = RemainingStones
	End Function

    'FIXME: was written for old ki implementation, maybe usefull for network handler
    'Function getMove() As clsMove
    '    getMove = AI.genMove
    'End Function

    Function startAI(ByRef Bricks() As clsBrick, ByRef PlayerPos() As Point, ByRef PlayerIndex As Byte, ByRef Dimensions As Byte) As Byte
        '0 = ok
        '1 = is already under AI control
        '2 = is a network player which may have open network
        '    connections, stop networking  first
        If getPlayerType() = 0 Then
            setType(1)
            startAI = 0
        ElseIf getPlayerType() = 1 Then
            startAI = 1
        ElseIf getPlayerType() >= 2 Then
            startAI = 2
        End If

        'start AI
        'FIXME: here we need a reference to board to start the KI
        'AI = New clsAI

    End Function

    Private Function setType(ByRef i As Byte) As Byte
        'i:
        '0 = user controlled local player
        '1 = AI controlled local player
        '2... reserved for networkgames
        'returnvalue
        '0 = ok
        '1 = out of range
        If i <= 1 Then
            TypeOfPlayer = i
            setType = 0
        Else
            setType = 1
        End If
    End Function

    Function getTarget() As Byte
        getTarget = TargetWall
    End Function

    Function getPlayerType() As Byte
        getPlayerType = TypeOfPlayer
    End Function

    Function Move(ByRef direction As Byte) As Boolean
        Select Case direction
            Case 0 'to bottom
                Location.Y = CByte(VB.Switch(Location.Y + 1 <= 255, Location.Y + 1, True, 255))
            Case 1 'to right
                Location.X = CByte(VB.Switch(Location.X + 1 <= 255, Location.X + 1, True, 255))
            Case 2
                Location.Y = CByte(VB.Switch(Location.Y - 1 >= 0, Location.Y - 1, True, 0))
            Case 3
                Location.X = CByte(VB.Switch(Location.X - 1 >= 0, Location.X - 1, True, 0))
        End Select
        Move = CBool(VB.Switch(0 <= direction And direction <= 3, True, True, False))
    End Function

    Function getLocation() As Position
        getLocation = Location
    End Function

    Sub newRound(ByRef x As Byte, ByRef y As Byte, ByRef Stones As Byte, ByRef target As Byte)
        GlobalPlaytime = GlobalPlaytime + Playtime

        Location.X = x
        Location.Y = y

        RemainingStones = Stones
        Playtime = 0
        TargetWall = target
    End Sub

    Function getPlayerName() As String
        getPlayerName = Name
    End Function

    Sub setPlayerName(ByRef s As String)
        Name = s
    End Sub

    Function subtractStone() As Boolean
        If RemainingStones > 0 Then
            RemainingStones = CByte(RemainingStones - 1)
            subtractStone = True
        Else
            subtractStone = False
        End If
    End Function

    Sub create(ByRef x As Byte, ByRef y As Byte, ByRef Stones As Byte, ByRef target As Byte)

        'set the first position of the player
        Location = New Position
        Location.X = x
        Location.Y = y

        TypeOfPlayer = 0
        RemainingStones = CByte(Stones + 1)
        Playtime = 0
        TargetWall = target

    End Sub
End Class