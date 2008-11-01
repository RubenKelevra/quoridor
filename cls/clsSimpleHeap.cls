VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsSimpleHeap"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

'this is the very basic heap implementation with dynamical allocation and
'emulated pointer for using in clsFibonacciHeap with Datatypes for DStar-Algorithm

' cls/clsSimpleHeap.cls - Part of Quoridor http://code.google.com/p/quoridor/
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

Private Type ListData
'   Information for FibonacciHeap

'    'number of children
'    iDegree As Integer
'
'    bMark As Boolean
'
'    ' heap-pointer
'    'child's
'    pChild As Integer
'    'left's
'    pLeft As Integer
'    'right's
'    pRight As Integer
'    'parent's heap-pointer
'    pParent As Integer

    'data of node
    'costs which we think that we need to reach target
    iHeuristic As Integer
    'costs to get to this position
    BValue As Byte
    'position
    BPos(1) As Byte
End Type

Private Type List
    Used As Boolean
    Handle As Boolean
    Data As ListData
End Type

' our heap of dataentries
Private Heap() As List

' holds our current size of the list
Private ListSize As Integer
' the steps we increase or decrease our HeapSize - if activated
Private AllocationStep As Integer
' the number of used entries
Private NoOfUsed As Integer
' the number of open handles to entries (used for defrag() each entry which haven't got an open handle
' is moveable in the Heap, so we can defrag this entries
Private NoOfHandles As Integer
'defines the minimal heap size, so if we clear it this size will be reach if decrease is activated
Private iMinListSize As Integer
'use with care, no upper limit
Private bAutoIncrease As Boolean
'only works probably with defrag() but this require an unmapping of all handles to defrag all entries
Private bAutoDecrease As Boolean
'temporary value to save the last entry of used entries while defraging
Private DefragTempIndex As Integer
Private i As Integer

Public Sub clear()
    ' clear
    ' set all used values to false
    For i = 1 To ListSize
        Heap(i).Used = False
        ElementsInList =
        Heap(i).Handle = False
    Next i
End Sub

Public Function getNoOfItems() As Integer
    ' getNoOfItems
    ' - returns the number of currently inserted items
    getNoOfItems = ElementsInList
End Function

Public Function getNoOfHandles() As Integer
    
End Function

Public Sub setHeapSize(Size As Integer)
    ' SetHeapSize
    ' define the logical size of heap
    ' - [IN] Size As Integer: Count of segments in heap
    OpenListSize = Size
    ReDim Heap(1 To Size)
End Sub

Private Function getNextUnused(Optional Start As Integer = 1) As Integer
    ' getNextUnused As Integer
    ' - [IN] Optional Start As Integer: allows you to overwrite the startpoint
    ' - returns the first as unused declaired field in List or if nothing is free
    '   last index + 1
    For getNextUnused = Start To ListSize
        If Not Heap(getNextUnused).Used Then
            Exit Function
        End If
    Next NextUnused
End Function

Public Sub ReSizeHeap(newSize As Integer)
    ' ReSizeHeap
    ' - [IN] newSize As Integer: defines the new logical size of List
    ListSize = newSize
    ReDim Preserve Heap(1 To newSize)
End Sub

Public Function Alloc() As Integer
    ' Alloc As Integer
    ' reserve space on the heap
    ' - returns a pointer of the heap position
    Alloc = getNextUnused(1)
    If Alloc <= ListSize Then
        On Error Resume Next
        Heap(Alloc).Used = True
        ElementsInList = ElementsInList + 1
        Exit Function
    End If
    'if no space left, we get to this position, now we allocate more space
    'if it's dynamical and adding at the next free point that we created right now
    If ListDynamically Then
        Call ReSizeHeap(ListSize + AllocationStep)
        Heap(Alloc).Used = True
        ElementsInList = ElementsInList + 1
    Else
        Call Err.Raise(vbObjectError, "GC::Alloc", "Heap overflow")
    End If
End Function

Public Sub FreeListElement(Pointer As Integer)
    ' FreeListElement
    ' set the position Pointer to unused and may start a run of defragmentation
    ' - [IN] Pointer As Integer: position in List which is going to set to unused
    If Heap(Pointer).Used Then
        Heap(Pointer).Used = False
        ElementsInList = ElementsInList - 1
        
        If AutoReduce Then
            'if we exceed the threshold we going to defrag and ReDim to save memoryspace
            If ListSize - ElementsInList >= AllocationStep * 2 Then
                DefragTempIndex = Defrag
                'if true we can save a full allocationstep of memory, so we going to
                ' ReSize to save memory
                If ListSize - DefragTempIndex > AllocationStep * 2 Then
                    'redim to save memoryspace
                    Call ReSizeHeap(DefragTempIndex + AllocationStep)
                End If
            End If
        End If
    End If
End Sub

Public Sub SetDynamical(Enabled As Boolean, AllocStep As Integer)
    ' SetDynamical
    ' defines if List is dynamically or not
    ' - [IN] Enabled As Boolean: Dynamic on/off
    ' - [IN] AllocStep As Integer: Step of increasing or if defrag is on decreasing
    ListDynamically = Enabled
    AllocationStep = AllocStep
End Sub

Public Function getMinValEntry() As Integer
    for i = 0 to
End Function

Public Sub SetDefrag(Enabled As Boolean)
    ' SetDefrag
    ' - [IN] Enabled As Boolean: turn AutoDefragmentation on/off
    AutoDefrag = Enabled
End Sub

Private Function getLastUsed(Optional Start As Integer) As Integer
    ' getLastUsed As Integer
    ' get the last used element out of the array
    ' - [IN] Optional Start As Integer: allows you to set a different start then the end
    '                                   of the list
    ' - returns the index of the last as used marked element or 0 if List is empty
    Start = Switch(Start = 0, ListSize, True, Start)
    
    For getLastUsed = Start To 1 Step -1
        If Heap(getLastUsed).Used Then
            Exit Function
        End If
    Next getLastUsed
End Function

Public Function Defrag() As Integer
    ' Defrag
    ' This is reodering to fill holes after this the
    ' Keep in Mind that this defrag function does NOT keep the old pointeraddresses
    ' if there is a old value stored somewhere it may NOT map to the same entry
    ' its slow, so you shouldn't use this often, use it only if you have a large list which
    ' is heavily fragmented so you may save a LOT of memory
    ' - returns the last used entry or 0 if no entry is marked as used
    Dim lastUsedFound As Integer
    Dim lastFirstFree As Integer
    
    lastFirstFree = 1
    lastUsedFound = ListSize
    
    Do
        'check for first free and last used entry
        lastFirstFree = getNextUnused(lastFirstFree)
        lastUsedFound = getLastUsed(lastUsedFound)
        'if free entry between used ones is found
        If lastFirstFree > lastUsedFound Then 'if there is no free between the used we exiting
            Exit Do
        End If
        'copy data from last used to this position
        Heap(lastFirstFree).Data = Heap(lastUsedFound).Data
        Heap(lastFirstFree).Used = True
        'now set this last used to unused and go ahead
        Heap(lastUsedFound).Used = False
    While True
    
    Defrag = lastUsedFound
End Function
