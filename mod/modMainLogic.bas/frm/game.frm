VERSION 5.00
Begin VB.Form frmMainForm 
   BorderStyle     =   1  'Fest Einfach
   Caption         =   "Quoridor"
   ClientHeight    =   4095
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   8175
   ClipControls    =   0   'False
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   4095
   ScaleWidth      =   8175
   StartUpPosition =   3  'Windows-Standard
   Begin VB.Frame fraBoard 
      BorderStyle     =   0  'Kein
      Caption         =   "Gameboard"
      Height          =   3735
      Left            =   180
      TabIndex        =   11
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
      TabIndex        =   7
      Top             =   120
      Width           =   3855
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
         TabIndex        =   10
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
         TabIndex        =   9
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
         TabIndex        =   8
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
      TabIndex        =   0
      Top             =   2040
      Width           =   3855
      Begin VB.CommandButton cmdRotateBrick 
         Caption         =   "rotate Brick"
         Height          =   495
         Left            =   120
         TabIndex        =   6
         Top             =   1080
         Width           =   1455
      End
      Begin VB.CommandButton cmdSetBrick 
         Caption         =   "set Brick"
         Height          =   495
         Left            =   120
         TabIndex        =   5
         Top             =   480
         Width           =   1455
      End
      Begin VB.CommandButton cmdMoveUp 
         Caption         =   "/|\"
         Height          =   495
         Left            =   2400
         TabIndex        =   4
         Top             =   480
         Width           =   495
      End
      Begin VB.CommandButton cmdMoveDown 
         Caption         =   "\|/"
         Height          =   495
         Left            =   2400
         TabIndex        =   3
         Top             =   1080
         Width           =   495
      End
      Begin VB.CommandButton cmdMoveLeft 
         Caption         =   "<-"
         Height          =   495
         Left            =   1800
         TabIndex        =   2
         Top             =   1080
         Width           =   495
      End
      Begin VB.CommandButton cmdMoveRight 
         Caption         =   "->"
         Height          =   495
         Left            =   3000
         TabIndex        =   1
         Top             =   1080
         Width           =   495
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

Private bSetBrick As Boolean
Private iFieldsize As Integer
Private clsBoard As clsBoard

Private Sub cmdMoveDown_Click()

    If clsBoard.getTempBrickActivasion Then
        
        If clsBoard.getTempBrickY < 7 Then
        
            ' move right
            clsBoard.setTempBrickY (clsBoard.getTempBrickY + 1)
        
        End If
    
    End If
    
    ' repaint form
    Call Form_Paint

End Sub

Private Sub cmdMoveLeft_Click()

    If clsBoard.getTempBrickActivasion Then
        
        If clsBoard.getTempBrickX > 0 Then
        
            ' move left
            clsBoard.setTempBrickX (clsBoard.getTempBrickX - 1)
        
        End If
    
    End If
    
    ' repaint form
    Call Form_Paint

End Sub

Private Sub cmdMoveRight_Click()

    If clsBoard.getTempBrickActivasion Then
        
        If clsBoard.getTempBrickX < 7 Then
        
            ' move right
            clsBoard.setTempBrickX (clsBoard.getTempBrickX + 1)
        
        End If
    
    End If
    
    ' repaint form
    Call Form_Paint

End Sub

Private Sub cmdMoveUp_Click()

    If clsBoard.getTempBrickActivasion Then
        
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
    clsBoard.setTempBrickValign (Switch(clsBoard.getTempBrickValign))
    
    ' repaint form
    Call Form_Paint

End Sub

Private Sub cmdSetBrick_Click()

    ' switches temp brick
    clsBoard.setTempBrickActivasion (Switch(clsBoard.getTempBrickActivasion))
    
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
    

    ' init brick option
    bSetBrick = False
    
    ' init current player marker
    Me.shpCurrentPlayer.FillColor = vbBlue
    
    ' init gameboard
    Set clsBoard = New clsBoard
    clsBoard.setDrawStartX (Me.fraBoard.Left)
    clsBoard.setDrawStartY (Me.fraBoard.Top)
    Call clsBoard.calcSize(Me.fraBoard.Height, Me.fraBoard.Width)

End Sub

Private Sub Form_Paint()

    clsBoard.drawBoard
    clsBoard.drawBricks
    
End Sub

