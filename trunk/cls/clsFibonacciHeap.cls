VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsFibonacciHeap"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Dim Heap As clsSimpleHeap

Public Sub create(Dimensions As Byte)
    ' create
    ' - [IN] Dimensions as Byte: describe the max index in each direction
    
    'we going to allocate enouth memoryspace for our List
    Call Heap.setHeapSize((Dimensions + 1) * 2)
End Sub

Public Function getNoOfItems() As Integer
    ' getNoOfItems As Integer
    ' - returns the Number of Items which are currently saved in the heap
    getNoOfItems = Heap.getNoOfItems
End Function

Public Function isEmpty() As Boolean
    ' isEmpty As Boolean
    ' - returns true if no element is saved in the heap
    If getNoOfItems = 0 Then
        isEmpty = True
    End If
End Function

Public Function insert(pos() As Byte, value As Byte) As Integer
    Dim l As Long
    l = UBound(Paths) - LBound(Paths) + 1
    Paths(l).pos(0) = pos(0)
    Paths(l).pos(1) = pos(1)
    Paths(l).value = value
End Function



