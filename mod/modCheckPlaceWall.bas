Attribute VB_Name = "modMainLogic"
Option Explicit

Function checkPlaceWall(Walls() As Stone, player() As Position, Dimension, newPos As Stone) As Boolean
    Dim i As Byte
    Dim startFieldTouched As Boolean
    Dim b As Boolean
    'use with care! theoretical max 8192 Byte memspace if field dimension is set to 256 don't know if VB can handle this big
    Dim RunnedFields(8, Dimension * 2) As Position
    '0 = aborted touched neighbor field which has
    '1 = start field touched left around
    '2 = start field touched right around
    '3 = target wall reached
    Dim RunningResults As Byte
    
    
    For i = 0 To 4 'position around the new wall
        For b = 0 To 1 'directions per position
            startFieldTouched
            While Not startFieldTouched
                
            Wend
            
End Function
