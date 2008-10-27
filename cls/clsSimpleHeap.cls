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
    'parent's heap-pointer
    parent As Integer
    'costs which we think that we need to reach target
    heuristic As Integer
    'costs to get to this position
    value As Byte
    'position
    pos(1) As Byte
End Type

Private Type List
    Used As Boolean
    Data As ListData
End Type

Private List() As List
Private ListSize As Integer
Private ListDynamically As Boolean
Private AllocationStep As Integer
Private ElementsInList As Integer
Private AutoDefrag As Boolean
Private DefragTempIndex As Integer

Public Sub setHeapSize(Size As Integer)
    ' SetHeapSize
    ' define the logical size of heap
    ' - [IN] Size As Integer: Count of segments in heap
    OpenListSize = Size
    ReDim openlist(1 To Size)
End Sub

Private Function getNextUnused(Optional Start As Integer = 1) As Integer
    ' getNextUnused As Integer
    ' - [IN] Optional Start As Integer: allows you to overwrite the startpoint
    ' - returns the first as unused declaired field in List or if nothing is free
    '   last index + 1
    For getNextUnused = Start To ListSize
        If Not List(getNextUnused).Used Then
            Exit Function
        End If
    Next NextUnused
End Function

Public Sub ReSizeHeap(newSize As Integer)
    ' ReSizeHeap
    ' - [IN] newSize As Integer: defines the new logical size of List
    ListSize = newSize
    ReDim Preserve List(1 To newSize)
End Sub

Public Function Alloc() As Integer
    ' Alloc As Integer
    ' reserve space on the heap
    ' - returns a pointer of the heap position
    Alloc = getNextUnused(1)
    If Alloc <= ListSize Then
        List(Alloc).Used = True
        ElementsInList = ElementsInList + 1
        Exit Function
    End If
    'if no space left, we get to this position, now we allocate more space
    'if it's dynamical and adding at the next free point that we created right now
    If ListDynamically Then
        Call ReSizeHeap(ListSize + AllocationStep)
        List(Alloc).Used = True
        ElementsInList = ElementsInList + 1
    Else
        Call Err.Raise(vbObjectError, "GC::Alloc", "Heap overflow")
    End If
End Function

Public Sub FreeListElement(Pointer As Integer)
    ' FreeListElement
    ' set the position Pointer to unused and may start a run of defragmentation
    If openlist(Pointer).Used Then
        openlist(Pointer).Used = False
    
        If AutoDefrag Then
            ElementsInList = ElementsInList - 1
            'if we exceed the threshold we going to defrag and ReDim to save memoryspace
            If ListSize - ElementsInList >= AllocationStep Then
                DefragTempIndex = Defrag
                'if true we can save a full allocationstep of memory, so we going to
                ' ReSize to save memory
                If ListSize - DefragTempIndex > AllocationStep * 2 Then
                    Call ReSizeHeap(Defrag + AllocationStep)
                End If
                FreedCounter = 0
                'redim to save memoryspace
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
        If List(getLastUsed).Used Then
            Exit Function
        End If
    Next getLastUsed
End Function

Public Function Defrag() As Integer
    ' Defrag
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
        List(lastFirstFree).Data = List(lastUsedFound).Data
        List(lastFirstFree).Used = True
        'now set this last used to unused and go ahead
        Call FreeListElement(lastUsedFound)
    While True
    
    Defrag = lastUsedFound
End Function
