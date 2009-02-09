Public Class Position
    Public X As Byte
    Public Y As Byte
End Class

Public Class clsBrick
    Public Placed As Boolean
    Public Position As Position
    Public Horizontal As Boolean

    Public Sub New()
        Position = New Position
    End Sub
End Class

Public Class clsMove
    Public ErrorCode As Byte
    Public FigureMove As Boolean
    Public MoveDirection As Byte
    Public Position As Point
    Public Horizontal As Boolean
End Class
