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
Private bSetBrick As Boolean
Private iFieldsize As Integer


Private Sub cmdMove_Click(Index As Integer)
    
End Sub

Private Sub cmdMoveDown_Click()

    If clsBoard.getTempBrickActivasion Then
        
        If clsBoard.getTempBrickY < 7 Then
        
            ' move right
            clsBoard_dennis.setTempBrickY (clsBoard_dennis.getTempBrickY + 1)
        
        End If
    
    End If
    
    ' repaint form
    Call Form_Paint

End Sub

Private Sub cmdMoveLeft_Click()

    If clsBoard_dennis.getTempBrickActivasion Then
        
        If clsBoard_dennis.getTempBrickX > 0 Then
        
            ' move left
            clsBoard_dennis.setTempBrickX (clsBoard_dennis.getTempBrickX - 1)
        
        End If
    
    End If
    
    ' repaint form
    Call Form_Paint

End Sub

Private Sub cmdMoveRight_Click()

    If clsBoard_dennis.getTempBrickActivasion Then
        
        If clsBoard_dennis.getTempBrickX < 7 Then
        
            ' move right
            clsBoard_dennis.setTempBrickX (clsBoard_dennis.getTempBrickX + 1)
        
        End If
    
    End If
    
    ' repaint form
    Call Form_Paint

End Sub

Private Sub cmdMoveUp_Click()

    If clsBoard_dennis.getTempBrickActivasion Then
        
        If clsBoard.getTempBrickY > 0 Then
        
            ' move right
            clsBoard.setTempBrickY (clsBoard.getTempBrickY - 1)
        
        End If
    
    End If
    
    ' repaint form
    Call Form_Paint

End Sub



Private Sub cmdRotateBrick_Click()

    ' switches rotation variable
    clsBoard.setTempBrickValign (switch(clsBoard.getTempBrickValign))
    
    ' repaint form
    Call Form_Paint

End Sub

Private Sub cmdSetBrick_Click()

    ' switches temp brick
    clsBoard.setTempBrickActivasion (switch(clsBoard.getTempBrickActivasion))
    
    If clsBoard.getTempBrickActivasion Then
        
        Me.cmdSetBrick.Caption = "OK?"
        
        ' reset temp brick
        clsBoard.setTempBrickValign (False)
        clsBoard.setTempBrickX (0)
        clsBoard.setTempBrickY (0)
        
    Else
        Me.cmdSetBrick.Caption = "set |"
    End If
    
    ' repaint form
    Call Form_Paint

End Sub

Private Sub Form_Load()
    
    Call Playground.create(4, 20, 9)        'player, bricks, fields dimension (x=y)
    bSetBrick = False                       'init brick option
    Me.shpCurrentPlayer.FillColor = vbBlue  'init current player marker
    
    ' init gameboard
    Set clsBoard = New clsBoard_dennis
    clsBoard.setDrawStartX (Me.fraBoard.Left)
    clsBoard.setDrawStartY (Me.fraBoard.Top)
    Call clsBoard.calcSize(Me.fraBoard.Height, Me.fraBoard.Width)

End Sub

Private Sub Form_Paint()

    clsBoard.drawBoard
    clsBoard.drawBricks
    
End Sub

