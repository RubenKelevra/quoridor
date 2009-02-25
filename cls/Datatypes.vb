Public Structure Position
    Public X As Byte
    Public Y As Byte
End Structure

Public Structure AstarData
    'for faster search for min value we use a tree structure
    Dim usNextMinF_ValueIndex As UShort

    'if node is on close list
    Dim bClosed As Boolean
    'f-score
    Dim usF_Score As UInteger
    'costs which we think that we need to reach target (normally the minimal length)
    Dim usHeuristic As UShort
    'costs to get to this position
    Dim uiCosts As UInteger
    'parent-field index from list
    Dim usParentFieldI As UShort
    'position
    Dim B_X As Byte
    Dim B_Y As Byte
End Structure

Public Structure clsBrick
    Public Placed As Boolean
    Public Position As Position
    Public Horizontal As Boolean
End Structure

Public Structure clsMove
    Public ErrorCode As Byte
    Public FigureMove As Boolean
    Public MoveDirection As Byte
    Public Position As Point
    Public Horizontal As Boolean
End Structure
