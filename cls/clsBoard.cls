VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsBoard"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

'figures
Private NoOfPlayer As Byte
Private NoOfStones As Byte 'per player
Private Player() As clsPlayer
Private Blocker() As Stone
Private Dimensions As Byte

'for constructs
Dim i As Integer

Function checkFrontWall(pos As Position, direction As Byte) As Boolean
    'if true there are a wall in front of the position and with this direction
    Dim st As Stone
    
    For Each st In Blocker()
        If (st.Position(0) = 255 Or st.Position(0) = 255) Then
            Exit For
        End If
        'delete this
        'If (st.Position = pos.Position And ((st.Landscape And direction = 2) Or (Not st.Landscape And direction = 1))) Then
        if
        
        
    Next st
    
    
End Function

Function checkPlaceWall(Walls() As Stone, Player() As Position, Dimension, newPos As Stone) As Boolean
    'for-counter
    Dim i As Byte
    Dim i2 As Integer
    Dim b As Boolean
    
    'state variables
    Dim ended As Boolean
    
    'use with care! theoretical max 8192 Byte memspace if field dimension is set to 256 don't know if VB can handle this big
    Dim RunnedFields(7, Dimension * 2 - 1) As Position
    '0 = aborted - touched neighbor field
    '1 = field touched were we had started - ccw around
    '2 = field touched were we had started - cw around
    '3 = target wall reached
    Dim RunningResults(7) As Byte
    'saves the right and left turns while running, so we can determine if we run ccw or cw
    Dim rightLeftCount As Byte
    'saves the actual running direction
    Dim direction As Byte
    
    For i = 0 To 7
        For i2 = 0 To Dimension * 2 - 1
            For b = 0 To 1
                RunnedFields(i, i2).Position(b) = 255
            Next b
        Next i2
    Next i
            
    
    For i = 0 To 3 'position around the new wall
        'setting startfield
        For b = 0 To 1
            'running ccw
            'x
            RunnedFields(i + Switch(Not b, 0, b, 4), 0).Position(0) = Switch( _
                                                                            i = 0 Or i = 1, newPos.Position(0), _
                                                                            i = 2 Or i = 3, newPos.Position(0) + 1 _
                                                                            )
            'y
            RunnedFields(i + Switch(Not b, 0, b, 4), 0).Position(1) = Switch( _
                                                                            i = 0 Or i = 3, newPos.Position(0), _
                                                                            i = 1 Or i = 2, newPos.Position(0) + 1 _
                                                                            )
        Next b
        For b = 0 To 1 'directions per position
            ended = False
            
            Do
                      
            While ended
        Next b
    Next i
End Function

Function getNameOfPlayer(i As Byte) As String
    On Error GoTo OutOfIndex
    getNameOfPlayer = Player(i).getPlayerName
OutOfIndex:
    getNameOfPlayer = ""
End Function
Function getNoOfPlayer() As Byte
    getNoOfPlayer = NoOfPlayer
End Function

Function getNoOfStones() As Byte
    getNoOfStones = NoOfStones
End Function

Sub create(NoOfPl As Byte, NoOfSt As Byte, Dimension As Byte)
    Dim playerstarts(3) As Position
    Dim i As Byte
    
    'check input value of NoOfPl if not between 2 and 4 set to 2
    NoOfPlayer = Switch(NoOfPl <= 4 And NoOfPl > 1, NoOfPl, True, 2)
    NoOfStones = NoOfSt
    Dimensions = Dimension
    
    ReDim Player(NoOfPlayer - 1)
    
    'calc player starts - players counted ccw
    playerstarts(0).Position(0) = playerstarts(1).Position(1) = Dimension / 2
    playerstarts(0).Position(1) = playerstarts(3).Position(0) = 0
    playerstarts(1).Position(0) = playerstarts(2).Position(1) = Dimension
    playerstarts(2).Position(0) = playerstarts(3).Position(1) = Dimension - (Dimension / 2) 'subtract to get the other line near the center for the mirrored player
    
    'set player starts
    For i = 0 To i < NoOfPlayer
        If NoOfPlayer = 2 And i = 1 Then
            i = i + 1 'change 2th player's startposition to 3rd players's
                      'if two players playing
        End If
        'running create routine
        Call Player.create(playerstarts(i).Position(0), playerstarts(i).Position(1), NoOfStones, _
            Switch( _
                i = 0, 2, _
                i = 1, 3, _
                i = 2, 0, _
                i = 3, 1))
        Next i
    
    'reserve space for blockers
    ReDim Blocker(NoOfPlayer * NoOfStones - 1)
    
    'set blockers start values
    For i = 0 To UBound(Blocker)
        Blocker(i).Landscape = False
        Blocker(i).Position(0) = 255
        Blocker(i).Position(1) = 255
    Next i
End Sub
