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

Private Location As Position
Private Name As String
Private Playtime As Long 'per round
Private GlobalPlaytime As Long 'per round playtime added at the end of a round
Private RemainingStones As Byte
Private TargetWall As Byte
Private PlayedGamesPositions(3) As Byte

Sub GameEnded(Position As Byte)
    playedgamesPosition(Position) = playedgamesPosition(Position) + 1 'very basic does not recognize how many players had played
    GlobalPlaytime = GlobalPlaytime + Playtime
End Sub

Sub newRound(x As Byte, y As Byte, Stones As Byte, Target As Byte)
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

    RemainingStones = Stones
    Playtime = 0
    TargetWall = Target
End Sub
