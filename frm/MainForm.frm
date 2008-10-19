VERSION 5.00
Begin VB.Form MainForm 
   Caption         =   "Quoridor v0.0.1"
   ClientHeight    =   5010
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   6960
   ClipControls    =   0   'False
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   5010
   ScaleWidth      =   6960
   StartUpPosition =   3  'Windows-Standard
   Begin VB.CommandButton cmd_testStart 
      Caption         =   "Test Start"
      Height          =   375
      Left            =   1080
      TabIndex        =   0
      Top             =   960
      Width           =   1695
   End
End
Attribute VB_Name = "MainForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmd_testStart_Click()
    Dim Board As clsBoard
    Call Board.create(4, 5, 9) '4 player; 5 blocker; 9 fields dimension
    
End Sub

Private Sub Form_Load()
    
End Sub
