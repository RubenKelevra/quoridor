VERSION 5.00
Begin VB.Form frmMainForm 
   BorderStyle     =   1  'Fest Einfach
   Caption         =   "Quoridor"
   ClientHeight    =   4080
   ClientLeft      =   150
   ClientTop       =   840
   ClientWidth     =   8235
   ClipControls    =   0   'False
   LinkTopic       =   "frmMainForm"
   MaxButton       =   0   'False
   ScaleHeight     =   4080
   ScaleWidth      =   8235
   StartUpPosition =   3  'Windows-Standard
   Begin VB.Frame fraBoard 
      BorderStyle     =   0  'Kein
      Caption         =   "Gameboard"
      Height          =   3735
      Left            =   180
      TabIndex        =   12
      Top             =   180
      Visible         =   0   'False
      Width           =   3735
   End
   Begin VB.Frame fraMovement 
      Caption         =   "Movement"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   18
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2055
      Left            =   4200
      TabIndex        =   5
      Top             =   1850
      Width           =   3855
      Begin VB.CommandButton cmdMove 
         Caption         =   "< -"
         Enabled         =   0   'False
         Height          =   495
         Index           =   3
         Left            =   1800
         TabIndex        =   11
         TabStop         =   0   'False
         Top             =   780
         Width           =   495
      End
      Begin VB.CommandButton cmdMove 
         Caption         =   "\l/"
         Enabled         =   0   'False
         Height          =   495
         Index           =   0
         Left            =   2400
         TabIndex        =   10
         TabStop         =   0   'False
         Top             =   1080
         Width           =   495
      End
      Begin VB.CommandButton cmdSetBrick 
         Caption         =   "set brick"
         Enabled         =   0   'False
         Height          =   495
         Left            =   120
         TabIndex        =   9
         TabStop         =   0   'False
         Top             =   480
         Width           =   1455
      End
      Begin VB.CommandButton cmdRotateBrick 
         Caption         =   "rotate brick"
         Enabled         =   0   'False
         Height          =   495
         Left            =   120
         TabIndex        =   8
         TabStop         =   0   'False
         Top             =   1080
         Width           =   1455
      End
      Begin VB.CommandButton cmdMove 
         Caption         =   "- >"
         Enabled         =   0   'False
         Height          =   495
         Index           =   1
         Left            =   3000
         TabIndex        =   7
         TabStop         =   0   'False
         Top             =   780
         Width           =   495
      End
      Begin VB.CommandButton cmdMove 
         Caption         =   "/l\"
         Enabled         =   0   'False
         Height          =   495
         Index           =   2
         Left            =   2400
         TabIndex        =   6
         TabStop         =   0   'False
         Top             =   480
         Width           =   495
      End
   End
   Begin VB.Frame fraInfo 
      Caption         =   "Information"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   18
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1575
      Left            =   4200
      TabIndex        =   1
      Top             =   120
      Width           =   3855
      Begin VB.PictureBox picFocus 
         BackColor       =   &H00000000&
         BorderStyle     =   0  'Kein
         Height          =   375
         Left            =   3360
         ScaleHeight     =   375
         ScaleWidth      =   375
         TabIndex        =   0
         TabStop         =   0   'False
         Top             =   360
         Width           =   375
      End
      Begin VB.Label lblBricksLeftNumber 
         Alignment       =   2  'Zentriert
         Caption         =   "88"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   13.5
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   2280
         TabIndex        =   4
         Top             =   960
         Visible         =   0   'False
         Width           =   375
      End
      Begin VB.Label lblBricksLeftTxt 
         Caption         =   "Bricks Left:"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   13.5
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   120
         TabIndex        =   3
         Top             =   960
         Width           =   1935
      End
      Begin VB.Label lblCurPlayer 
         Caption         =   "Current Player:"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   13.5
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   120
         TabIndex        =   2
         Top             =   480
         Width           =   1935
      End
      Begin VB.Shape shpCurrentPlayer 
         BackColor       =   &H00FFFFFF&
         FillStyle       =   0  'Ausgefüllt
         Height          =   375
         Left            =   2280
         Shape           =   3  'Kreis
         Top             =   480
         Visible         =   0   'False
         Width           =   375
      End
   End
   Begin VB.Menu ddmMenuGame 
      Caption         =   "Spiel"
      Begin VB.Menu ddmNewGame 
         Caption         =   "&Neues Spiel"
         Shortcut        =   {F2}
      End
      Begin VB.Menu ddmSaveGame 
         Caption         =   "Spiel &speichern"
         Shortcut        =   ^S
      End
      Begin VB.Menu ddmLoadGame 
         Caption         =   "Spiel &laden"
         Shortcut        =   ^O
      End
      Begin VB.Menu ddmLine1 
         Caption         =   "-"
      End
      Begin VB.Menu ddmExit 
         Caption         =   "&Beenden"
      End
   End
End
Attribute VB_Name = "frmMainForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

' frm/frmMainForm.frm - Part of Quoridor http://code.google.com/p/quoridor/
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

'gamedata
Private Playground As clsBoard

'formdata
Private bKeyUp As Boolean
Private bGameEnabled As Boolean
Private iFieldsize As Integer
Private iBricksize As Integer
Private iDrawStartX As Integer
Private iDrawStartY As Integer
Private lBoardcolor As Long
Private tTempBrick As Brick

Private Sub cmdMove_Click(Index As Integer)
    
    ' dec
    Dim changed As Boolean
    changed = False
    
    If Not tTempBrick.Placed Then 'move figure
        
        changed = Playground.movePlayer(Playground.getActivePlayer, CByte(Index))
    
    Else
    
        Select Case Index
            
            ' move down
            Case 0:
                If tTempBrick.Position(1) < Playground.getDimension - 1 Then
                    tTempBrick.Position(1) = tTempBrick.Position(1) + 1
                    changed = True
                End If
            
            ' move right
            Case 1:
                If tTempBrick.Position(0) < Playground.getDimension - 1 Then
                    tTempBrick.Position(0) = tTempBrick.Position(0) + 1
                    changed = True
                End If
            
            ' move up
            Case 2:
                If tTempBrick.Position(1) > 0 Then
                    tTempBrick.Position(1) = tTempBrick.Position(1) - 1
                    changed = True
                End If
            
            ' move left
            Case 3:
                If tTempBrick.Position(0) > 0 Then
                    tTempBrick.Position(0) = tTempBrick.Position(0) - 1
                    changed = True
                End If
        
        End Select
        
    End If
    
    If changed Then
    
        ' repaint form
        Call Form_Paint
    
    End If
    
End Sub

Private Sub cmdMove_MouseUp(Index As Integer, Button As Integer, Shift As Integer, x As Single, y As Single)

    ' set focus to picFocus for arrow-movement
    Me.picFocus.SetFocus

End Sub

Private Sub cmdRotateBrick_Click()

    ' switches rotation variable
    tTempBrick.Landscape = Not tTempBrick.Landscape
    
    ' set focus to picFocus for arrow-movement
    Me.picFocus.SetFocus
    
    ' repaint form
    Call Form_Paint

End Sub

Private Sub cmdRotateBrick_MouseUp(Button As Integer, Shift As Integer, x As Single, y As Single)
    
    ' set focus to picFocus for arrow-movement
    Me.picFocus.SetFocus

End Sub

Private Sub cmdSetBrick_Click()
    
    If tTempBrick.Placed Then
        If Playground.checkPlaceWall(tTempBrick.Position(0), tTempBrick.Position(1), tTempBrick.Landscape) Then
        
            ' save brick
            Select Case Playground.saveWall(tTempBrick, Playground.getActivePlayer)
                Case 0:
                    Playground.NextTurn
                Case 1:
                    MsgBox "You don't have enough bricks.", vbOKOnly, "No Bricks Left"
                Case 2:
                    MsgBox "You can't place a brick on this position.", vbOKOnly, "Brick Cannot Be Placed"
                Case 3:
                    MsgBox "Internal Application Error" + vbCrLf + "Error No. 15" + vbCrLf + "Press OK to continue", vbCritical, "Internal Application Error"
            End Select
            
            ' reset caption
            Me.cmdSetBrick.Caption = "set brick"

            ' reset brick options
            tTempBrick.Landscape = False
            tTempBrick.Position(0) = 0
            tTempBrick.Position(1) = 0
            tTempBrick.Placed = False
            Me.cmdRotateBrick.Enabled = False
            
        Else
            
            ' placing on this position is not possible
            MsgBox "You can't place a brick on this position.", vbOKOnly, "Brick Cannot Be Placed"
        
        End If
        
    Else
        
        ' set new caption
        Me.cmdSetBrick.Caption = "OK?"
            
        ' enable brick options
        Me.cmdRotateBrick.Enabled = True
        tTempBrick.Placed = True
    
    End If
    
    ' set focus to picFocus for arrow-movement
    Me.picFocus.SetFocus
            
    ' repaint form
    Call Form_Paint
    
End Sub

Private Sub cmdSetBrick_MouseUp(Button As Integer, Shift As Integer, x As Single, y As Single)

    ' set focus to picFocus for arrow-movement
    Me.picFocus.SetFocus

End Sub

Private Sub ddmExit_Click()

    ' exit programm
    End

End Sub

Private Sub ddmNewGame_Click()

    ' init board
    Set Playground = New clsBoard
    Call Playground.create(3, 4, 8) 'player, bricks, fields dimension (x=y)
    Me.shpCurrentPlayer.FillColor = Playground.getPlayerColor(0) 'init current player marker
    
    ' init drawing coords
    iDrawStartX = Me.fraBoard.Left
    iDrawStartY = Me.fraBoard.Top
    
    iFieldsize = (Me.fraBoard.Height + Me.fraBoard.Width) / (9 + 9)
    iBricksize = iFieldsize / 9
    
    ' init colors
    lBoardcolor = RGB(0, 0, 0)
    
    ' init other vars
    bKeyUp = True
    
    ' enable GUI
    If Not bGameEnabled Then
        Me.lblBricksLeftNumber.Visible = True
        Me.shpCurrentPlayer.Visible = True
        Me.cmdMove(0).Enabled = True
        Me.cmdMove(1).Enabled = True
        Me.cmdMove(2).Enabled = True
        Me.cmdMove(3).Enabled = True
        Me.cmdSetBrick.Enabled = True
    End If
    
    ' draw
    bGameEnabled = True
    Call Form_Paint

End Sub

Private Sub Form_Load()

    ' hide picture box
    Me.picFocus.BackColor = Me.BackColor

End Sub

Private Sub Form_Paint()

    ' sets current color of the active figure
    If bGameEnabled Then
        Call setCurFigureColor
        Call setBricksLeft
        Call deactMoveButtons
        
        Call drawBoard
        Call drawBricks
    End If
    
End Sub

Private Sub setCurFigureColor()

    ' set current player marker
    Me.shpCurrentPlayer.FillColor = Playground.getPlayerColor(Playground.getActivePlayer)

End Sub

Private Sub setBricksLeft()

    Dim B As Byte
    
    ' set current ammount of brick
    B = Playground.getRemainingPlayerBricks(Playground.getActivePlayer)
    
    ' update player bricks
    Me.lblBricksLeftNumber.Caption = CStr(B)
    
    If B = 0 Then
        ' deactivate brick button
        Me.cmdSetBrick.Enabled = False
    End If

End Sub

Private Sub deactMoveButtons()

    ' dec
    Dim BActPlayer As Byte
    Dim i As Integer
    Dim t As Position
    
    If tTempBrick.Placed Then
    
        ' deactivates buttons which indicates not possible directions
        For i = 0 To 3
            
            Me.cmdMove(i).Enabled = True
        
            Select Case i
                
                ' down
                Case 0:
                    If tTempBrick.Position(1) >= Playground.getDimension - 1 Then
                        Me.cmdMove(i).Enabled = False
                    End If
                
                ' right
                Case 1:
                    If tTempBrick.Position(0) >= Playground.getDimension - 1 Then
                        Me.cmdMove(i).Enabled = False
                    End If
                
                ' up
                Case 2:
                    If tTempBrick.Position(1) <= 0 Then
                        Me.cmdMove(i).Enabled = False
                    End If
                
                ' left
                Case 3:
                    If tTempBrick.Position(0) <= 0 Then
                        Me.cmdMove(i).Enabled = False
                    End If
                    
            End Select
                    
            cmdMove(i).FontBold = False
            
        Next i
    
    Else
    
        ' init
        BActPlayer = Playground.getActivePlayer
    
        ' deactivate buttons which indicates not posssible directions
        For i = 0 To 3
            cmdMove(i).Enabled = Playground.checkMove(Playground.getPlayerLocation(BActPlayer), i, False)
            cmdMove(i).FontBold = Playground.getPlayerTarget(BActPlayer) = i
        Next i
    
    End If

End Sub


Private Sub drawBoard()
' draws the fields on which a player can move

    ' dec
    Dim BDimension As Byte
    Dim i As Byte
    Dim x As Byte
    Dim y As Byte
    Dim iCurX As Integer
    Dim iCurY As Integer
    Dim lCurColor As Long
    Dim tDrawPos As Position
    Dim tPlayerPos As Position
    
    ' save dimension
    BDimension = Playground.getDimension
    
    For x = 0 To BDimension
        For y = 0 To BDimension
        
            ' init color
            lCurColor = lBoardcolor
        
            ' calc current coords
            iCurX = iDrawStartX + x * iFieldsize
            iCurY = iDrawStartY + y * iFieldsize
            
            ' check current position with the position of all players
            For i = 0 To Playground.getNoOfPlayer
                
                tDrawPos = xy2pos(x, y)
                tPlayerPos = Playground.getPlayerLocation(i)
                
                If Not comparePos(xy2pos(255, 255), tPlayerPos) And comparePos(tDrawPos, tPlayerPos) Then
                        
                    ' set playercolor
                    lCurColor = Playground.getPlayerColor(i)
                
                End If
            
            Next i

            ' draw lines
            Me.Line (iCurX, iCurY)- _
                         (iCurX + iFieldsize - iBricksize, iCurY + iFieldsize - iBricksize), _
                          lCurColor, _
                          BF
            
        Next y
    Next x

End Sub

Public Sub drawBricks()
' draws the bricks between the board

    Dim i As Integer
    Dim x As Integer
    Dim y As Integer
    Dim iCurX As Integer
    Dim iCurY As Integer
    Dim lCurColor As Long
    Dim tSavedBrick() As Brick
    
    ReDim tSavedBrick(UBound(Playground.getWalls))
    tSavedBrick = Playground.getWalls
    
    ' horizontal
    For x = 0 To 8
        For y = 0 To 7
        
            ' init color
            lCurColor = Me.BackColor
        
            ' calc current coords
            iCurX = iDrawStartX + x * iFieldsize
            iCurY = iDrawStartY + (y + 1) * iFieldsize - iBricksize
            
            For i = LBound(tSavedBrick) To UBound(tSavedBrick)
                
                ' if brick is not set
                If tSavedBrick(i).Placed = False Or tSavedBrick(i).Position(0) = 255 Or tSavedBrick(i).Position(1) = 255 Then
                    Exit For
                End If
                
                ' saved brick
                If Not tSavedBrick(i).Landscape And _
                   ((x = tSavedBrick(i).Position(0) And y = tSavedBrick(i).Position(1)) Or _
                   (x = tSavedBrick(i).Position(0) + 1 And y = tSavedBrick(i).Position(1))) _
                Then
                
                    lCurColor = RGB(0, 192, 192)
                    
                End If
                
            Next i
            
            ' temp brick
            If tTempBrick.Placed And Not tTempBrick.Landscape And _
               ((x = tTempBrick.Position(0) And y = tTempBrick.Position(1)) Or _
               (x = tTempBrick.Position(0) + 1 And y = tTempBrick.Position(1))) _
            Then
            
                lCurColor = RGB(0, 192, 0)
                
            End If
            
            ' draw bricks
            Me.Line (iCurX, iCurY)- _
                         (iCurX + iFieldsize - iBricksize, iCurY + iBricksize), _
                          lCurColor, _
                          BF
        
        Next y
    Next x
    
    ' vertical
    For x = 0 To 7
        For y = 0 To 8
        
            ' init color
            lCurColor = Me.BackColor
        
            ' calc current coords
            iCurX = iDrawStartX + (x + 1) * iFieldsize - iBricksize
            iCurY = iDrawStartY + y * iFieldsize
            
            For i = LBound(tSavedBrick) To UBound(tSavedBrick)
                
                ' if brick is not set
                If tSavedBrick(i).Placed = False Or tSavedBrick(i).Position(0) = 255 Or tSavedBrick(i).Position(1) = 255 Then
                    Exit For
                End If
                
                ' saved brick
                If tSavedBrick(i).Landscape And _
                   ((x = tSavedBrick(i).Position(0) And y = tSavedBrick(i).Position(1)) Or _
                   (x = tSavedBrick(i).Position(0) And y = tSavedBrick(i).Position(1) + 1)) _
                Then
                
                    lCurColor = RGB(0, 192, 192)
                    
                End If
                
            Next i
            
            ' temp brick
            If tTempBrick.Placed And tTempBrick.Landscape And _
               ((x = tTempBrick.Position(0) And y = tTempBrick.Position(1)) Or _
               (x = tTempBrick.Position(0) And y = tTempBrick.Position(1) + 1)) _
            Then
            
                lCurColor = RGB(0, 192, 0)
                
            End If
            
            ' draw bricks
            Me.Line (iCurX, iCurY)- _
                         (iCurX + iBricksize, iCurY + iFieldsize - iBricksize), _
                          lCurColor, _
                          BF
        
        Next y
    Next x

End Sub

Private Sub picFocus_KeyDown(KeyCode As Integer, Shift As Integer)
' select movement keys

    If bKeyUp Then
    
        Select Case KeyCode
        
            Case vbKeyDown:
                Call cmdMove_Click(0)
                
            Case vbKeyRight:
                Call cmdMove_Click(1)
                
            Case vbKeyUp:
                Call cmdMove_Click(2)
                
            Case vbKeyLeft:
                Call cmdMove_Click(3)
        
        End Select
        
        bKeyUp = False
        
    End If
    
End Sub

Private Sub picFocus_KeyUp(KeyCode As Integer, Shift As Integer)
' on keyUp, set var to true

    bKeyUp = True

End Sub
