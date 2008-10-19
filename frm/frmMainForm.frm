VERSION 5.00
Begin VB.Form frmMainForm 
   BorderStyle     =   1  'Fest Einfach
   Caption         =   "Quoridor"
   ClientHeight    =   4095
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   8175
   ClipControls    =   0   'False
   LinkTopic       =   "frmMainForm"
   MaxButton       =   0   'False
   ScaleHeight     =   4095
   ScaleWidth      =   8175
   StartUpPosition =   3  'Windows-Standard
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
      Height          =   1815
      Left            =   4200
      TabIndex        =   5
      Top             =   2040
      Width           =   3855
      Begin VB.CommandButton cmdMove 
         Caption         =   "< -"
         Height          =   495
         Index           =   3
         Left            =   1800
         TabIndex        =   11
         Top             =   1080
         Width           =   495
      End
      Begin VB.CommandButton cmdMove 
         Caption         =   "\l/"
         Height          =   495
         Index           =   0
         Left            =   2400
         TabIndex        =   10
         Top             =   1080
         Width           =   495
      End
      Begin VB.CommandButton cmdSetBrick 
         Caption         =   "set Brick"
         Height          =   495
         Left            =   120
         TabIndex        =   9
         Top             =   480
         Width           =   1455
      End
      Begin VB.CommandButton cmdRotateBrick 
         Caption         =   "rotate Brick"
         Height          =   495
         Left            =   120
         TabIndex        =   8
         Top             =   1080
         Width           =   1455
      End
      Begin VB.CommandButton cmdMove 
         Caption         =   "- >"
         Height          =   495
         Index           =   1
         Left            =   3000
         TabIndex        =   7
         Top             =   1080
         Width           =   495
      End
      Begin VB.CommandButton cmdMove 
         Caption         =   "/l\"
         Height          =   495
         Index           =   2
         Left            =   2400
         TabIndex        =   6
         Top             =   480
         Width           =   495
      End
   End
   Begin VB.Frame fraBoard 
      BorderStyle     =   0  'Kein
      Caption         =   "Gameboard"
      Height          =   3735
      Left            =   180
      TabIndex        =   4
      Top             =   180
      Visible         =   0   'False
      Width           =   3735
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
      Height          =   1815
      Left            =   4200
      TabIndex        =   0
      Top             =   120
      Width           =   3855
      Begin VB.Label lblBricksLeftNumber 
         Alignment       =   2  'Zentriert
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
         TabIndex        =   3
         Top             =   960
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
         TabIndex        =   2
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
         TabIndex        =   1
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
         Width           =   375
      End
   End
   Begin VB.Line lneCutter 
      X1              =   4080
      X2              =   4080
      Y1              =   0
      Y2              =   5040
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
Private bSetBrickMode As Boolean
Private iFieldsize As Integer
Private iBricksize As Integer
Private iDrawStartX As Integer
Private iDrawStartY As Integer
Private lBoardcolor As Long
Private tTempBrick As Brick

Private Sub cmdMove_Click(Index As Integer)

    If bSetBrickMode Then
        
        If tTempBrick.Position(1) > 0 Then
        
            ' move right
            tTempBrick.Position(1) = tTempBrick.Position(1) - 1
        
        End If
    
    End If
    
    ' repaint form
    Call Form_Paint
End Sub

Private Sub cmdRotateBrick_Click()

    ' switches rotation variable
    tTempBrick.Landscape = Not tTempBrick.Landscape
    
    ' repaint form
    Call Form_Paint

End Sub

Private Sub cmdSetBrick_Click()
    If bSetBrickMode And Playground.checkPlaceWall(tTempBrick) Then
        
        
        'FIXME: save the brick
        
        
        Me.cmdSetBrick.Caption = "set |"
        
        ' reset temp brick
        tTempBrick.Landscape = False
        tTempBrick.Position(0) = 0
        tTempBrick.Position(1) = 0
        bSetBrickMode = False
        
        ' repaint form
        Call Form_Paint
    Else
        Me.cmdSetBrick.Caption = "OK?"
        bSetBrickMode = True
    End If
End Sub

Private Sub Form_Load()
    
    Set Playground = New clsBoard
    Call Playground.create(3, 4, 8) 'player, bricks, fields dimension (x=y)
    bSetBrickMode = False 'init brick option
    Me.shpCurrentPlayer.FillColor = Playground.getPlayerColor(0) 'init current player marker

    ' init fieldsize
    iFieldsize = 380
    
    ' init linesize
    iBricksize = 50
    
    ' init drawing coords
    iDrawStartX = Me.fraBoard.Left
    iDrawStartY = Me.fraBoard.Top
    
    iFieldsize = (Me.fraBoard.Height + Me.fraBoard.Width) / (9 + 9)
    iBricksize = iFieldsize / 9
    
    ' init color
    lBoardcolor = RGB(0, 0, 0)
    
End Sub

Private Sub Form_Paint()

    drawBoard
    drawBricks
    
End Sub

Private Sub drawBoard()
' draws the fields on which a player can move

    Dim BDimension As Byte
    Dim x As Byte
    Dim y As Byte
    Dim i As Byte
    Dim iCurX As Integer
    Dim iCurY As Integer
    Dim lCurColor As Long
    Dim tDrawPos As Position
    Dim tPlayerPos As Position
    
    BDimension = Playground.getDimension
    lCurColor = lBoardcolor
    
    For x = 0 To BDimension
        For y = 0 To BDimension
        
        
            ' calc current coords
            iCurX = iDrawStartX + x * iFieldsize
            iCurY = iDrawStartY + y * iFieldsize
            
            ' draw lines
            For i = 0 To Playground.getNoOfPlayer
                tDrawPos = xy2pos(x, y)
                tPlayerPos = Playground.getPlayerlocation(i)
                
                If Not IsNull(tPlayerPos) Then
                    If comparePos(tDrawPos, tPlayerPos) Then
                        lCurColor = Playground.getPlayerColor(i)
                    End If
                End If
            Next i

            Me.Line (iCurX, iCurY)- _
                         (iCurX + iFieldsize - iBricksize, iCurY + iFieldsize - iBricksize), _
                          lCurColor, _
                          BF
            
        Next y
    Next x

End Sub

Public Sub drawBoard_old()
' draws the fields where a player can move

    Dim x As Integer
    Dim y As Integer
    Dim iCurX As Integer
    Dim iCurY As Integer
    
    For x = 0 To 8
        For y = 0 To 8
        
            ' calc current coords
            iCurX = iDrawStartX + x * iFieldsize
            iCurY = iDrawStartY + y * iFieldsize
            
            ' draw lines
            Select Case 1: 'iBoard(x, y) FIXME Dennis sein bier
            
                Case 1:
                    Me.Line (iCurX, iCurY)- _
                                 (iCurX + iFieldsize - iBricksize, iCurY + iFieldsize - iBricksize), _
                                  RGB(0, 0, 255), _
                                  BF
                Case 2:
                    Me.Line (iCurX, iCurY)- _
                                 (iCurX + iFieldsize - iBricksize, iCurY + iFieldsize - iBricksize), _
                                  RGB(255, 0, 0), _
                                  BF
                                  
                Case Else:
                    Me.Line (iCurX, iCurY)- _
                                 (iCurX + iFieldsize - iBricksize, iCurY + iFieldsize - iBricksize), _
                                  RGB(0, 0, 0), _
                                  BF
            End Select
            
        Next y
    Next x

End Sub

Public Sub drawBricks()
' draws the bricks between the board

    Dim x As Integer
    Dim y As Integer
    Dim iCurX As Integer
    Dim iCurY As Integer
    
    ' horizontal
    For x = 0 To 8
        For y = 0 To 7
        
            ' calc current coords
            iCurX = iDrawStartX + x * iFieldsize
            iCurY = iDrawStartY + (y + 1) * iFieldsize - iBricksize
            
            If tTempBrick.Placed And Not tTempBrick.Landscape And _
               ((x = tTempBrick.Position(0) And y = tTempBrick.Position(1)) Or _
               (x = tTempBrick.Position(0) + 1 And y = tTempBrick.Position(1))) _
            Then
                ' draw temp brick
                frmMainForm.Line (iCurX, iCurY)- _
                             (iCurX + iFieldsize - iBricksize, iCurY + iBricksize), _
                              RGB(0, 192, 0), _
                              BF
            Else
                ' draw neutral brick
                frmMainForm.Line (iCurX, iCurY)- _
                             (iCurX + iFieldsize - iBricksize, iCurY + iBricksize), _
                              &H8000000F, _
                              BF
            End If
        
        Next y
    Next x
    
    ' vertical
    For x = 0 To 7
        For y = 0 To 8
        
            ' calc current coords
            iCurX = iDrawStartX + (x + 1) * iFieldsize - iBricksize
            iCurY = iDrawStartY + y * iFieldsize
            
            If tTempBrick.Placed And tTempBrick.Landscape And _
               ((x = tTempBrick.Position(0) And y = tTempBrick.Position(1)) Or _
               (x = tTempBrick.Position(0) And y = tTempBrick.Position(1) + 1)) _
            Then
                ' draw line to set
                frmMainForm.Line (iCurX, iCurY)- _
                             (iCurX + iBricksize, iCurY + iFieldsize - iBricksize), _
                              RGB(0, 192, 0), _
                              BF
            Else
                ' draw neutral lines
                frmMainForm.Line (iCurX, iCurY)- _
                             (iCurX + iBricksize, iCurY + iFieldsize - iBricksize), _
                              &H8000000F, _
                              BF
            End If
        
        Next y
    Next x

End Sub


