Option Strict Off
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
	
    Private Location As Point
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
	
    Function getMove() As clsMove
        getMove = AI.genMove
    End Function
	
    Function startAI(ByRef Bricks() As clsBrick, ByRef PlayerPos() As Point, ByRef PlayerIndex As Byte, ByRef Dimensions As Byte) As Byte
        '0 = ok
        '1 = is already under AI control
        '2 = is a network player which may have open network
        '    connections, stop networking  first
        If getType_Renamed() = 0 Then
            setType(1)
            startAI = 0
        ElseIf getType_Renamed() = 1 Then
            startAI = 1
        ElseIf getType_Renamed() >= 2 Then
            startAI = 2
        End If

        'start AI
        AI = New clsAI
        AI.create(Bricks, PlayerPos, PlayerIndex, Dimensions, 6)

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
	
	'UPGRADE_NOTE: getType wurde aktualisiert auf getType_Renamed. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Function getType_Renamed() As Byte
		getType_Renamed = TypeOfPlayer
	End Function
	
	
	'UPGRADE_NOTE: dir wurde aktualisiert auf dir_Renamed. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Function Move(ByRef dir_Renamed As Byte) As Boolean
		Select Case dir_Renamed
			Case 0 'to bottom
				'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Location.Y = VB.Switch(Location.Y + 1 <= 255, Location.Y + 1, True, 255)
			Case 1 'to right
				'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Location.X = VB.Switch(Location.X + 1 <= 255, Location.X + 1, True, 255)
			Case 2
				'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Location.Y = VB.Switch(Location.Y - 1 >= 0, Location.Y - 1, True, 0)
			Case 3
				'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Location.X = VB.Switch(Location.X - 1 >= 0, Location.X - 1, True, 0)
		End Select
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Move = VB.Switch(0 <= dir_Renamed And dir_Renamed <= 3, True, True, False)
	End Function
	
    Function getLocation() As Point
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
			RemainingStones = RemainingStones - 1
			subtractStone = True
		Else
			subtractStone = False
		End If
	End Function
	
	Sub create(ByRef x As Byte, ByRef y As Byte, ByRef Stones As Byte, ByRef target As Byte)
		
		'set the first position of the player
        Location.X = x
        Location.Y = y
		
		TypeOfPlayer = 0
		RemainingStones = Stones + 1
		Playtime = 0
		TargetWall = target
	End Sub
End Class