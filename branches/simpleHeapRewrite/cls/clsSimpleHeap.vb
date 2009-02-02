Option Strict On
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class clsSimpleHeap
	
	'this is the very basic heap implementation with dynamical allocation and
    'emulated pointer for using in clsFibonacciHeap with Datatypes for AStar-Algorithm
	
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
	
	Private Structure ListData
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
		Dim iHeuristic As Short
		'costs to get to this position
		Dim BValue As Byte
		'position
        Dim BPosX As Byte
        Dim BPosY As Byte
	End Structure
	
	Private Structure List
		Dim Used As Boolean
		Dim Handle As Boolean
		Dim Data As ListData
	End Structure
	
	' our heap of dataentries
	Private Heap() As List
	
	' holds our current size of the list
	Private ListSize As Short
	' the steps we increase or decrease our HeapSize - if activated
	Private AllocationStep As Short
	' the number of used entries
	Private NoOfUsed As Short
	' the number of open handles to entries (used for defrag() each entry which haven't got an open handle
	' is moveable in the Heap, so we can defrag this entries
	Private NoOfHandles As Short
	'defines the minimal heap size, so if we clear it this size will be reach if decrease is activated
	Private iMinListSize As Short
	'use with care, no upper limit
	Private bAutoIncrease As Boolean
	'only works probably with defrag() but this require an unmapping of all handles to defrag all entries
	Private bAutoDecrease As Boolean
	'temporary value to save the last entry of used entries while defraging
    Private DefragTempIndex As Short
    'sets 
    Private ElementsInList As Short
	Private i As Short
	
	Public Sub clear()
        ' clear
        ' set all used values to false
        For i = 0 To ListSize
            Heap(i).Used = False
            Heap(i).Handle = False
        Next i
    End Sub
	
	Public Function getNoOfItems() As Short
		Dim ElementsInList As Object
		' getNoOfItems
		' - returns the number of currently inserted items
		getNoOfItems = ElementsInList
	End Function
	
	Public Function getNoOfHandles() As Short
		
	End Function
	
	Public Sub setHeapSize(ByRef Size As Short)
		Dim OpenListSize As Object
		' SetHeapSize
		' define the logical size of heap
		' - [IN] Size As Integer: Count of segments in heap
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts OpenListSize konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		OpenListSize = Size
		'UPGRADE_WARNING: Die untere Begrenzung des Arrays Heap wurde von 1 in 0 geändert. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		Dim Heap(Size) As Object
	End Sub
	
	Private Function getNextUnused(Optional ByRef Start As Short = 1) As Short
        Dim Heap As Object
        Dim ListSize As Object
        ' getNextUnused As Integer
        ' - [IN] Optional Start As Integer: allows you to overwrite the startpoint
        ' - returns the first as unused declaired field in List or if nothing is free
        '   last index + 1
        'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ListSize konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        For getNextUnused = Start To ListSize
            'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap(getNextUnused).Used konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            If Not Heap(getNextUnused).Used Then
                Exit Function
            End If
        Next getNextUnused
    End Function
	
	Public Sub ReSizeHeap(ByRef newSize As Short)
		Dim ListSize As Object
		' ReSizeHeap
		' - [IN] newSize As Integer: defines the new logical size of List
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ListSize konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		ListSize = newSize
		'UPGRADE_WARNING: Die untere Begrenzung des Arrays Heap wurde von 1 in 0 geändert. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		Dim Heap(newSize) As Object
	End Sub
	
	Public Function Alloc() As Short
		Dim ListDynamically As Object
		Dim AllocationStep As Object
		Dim ListSize As Object
		Dim ElementsInList As Object
		Dim Heap As Object
		' Alloc As Integer
		' reserve space on the heap
		' - returns a pointer of the heap position
		Alloc = getNextUnused(1)
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ListSize konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If Alloc <= ListSize Then
			On Error Resume Next
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap().Used konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Heap(Alloc).Used = True
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ElementsInList konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ElementsInList = ElementsInList + 1
			Exit Function
		End If
		'if no space left, we get to this position, now we allocate more space
		'if it's dynamical and adding at the next free point that we created right now
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ListDynamically konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If ListDynamically Then
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts AllocationStep konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ListSize konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Call ReSizeHeap(ListSize + AllocationStep)
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap().Used konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Heap(Alloc).Used = True
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ElementsInList konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ElementsInList = ElementsInList + 1
		Else
			Call Err.Raise(vbObjectError, "GC::Alloc", "Heap overflow")
		End If
	End Function
	
	Public Sub FreeListElement(ByRef Pointer As Short)
		Dim AutoReduce As Object
		Dim ListSize As Object
		Dim AllocationStep As Object
		Dim DefragTempIndex As Object
		Dim ElementsInList As Object
		Dim Heap As Object
		' FreeListElement
		' set the position Pointer to unused and may start a run of defragmentation
		' - [IN] Pointer As Integer: position in List which is going to set to unused
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap(Pointer).Used konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If Heap(Pointer).Used Then
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap().Used konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Heap(Pointer).Used = False
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ElementsInList konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ElementsInList = ElementsInList - 1
			
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts AutoReduce konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			If AutoReduce Then
				'if we exceed the threshold we going to defrag and ReDim to save memoryspace
				'UPGRADE_WARNING: Die Standardeigenschaft des Objekts AllocationStep konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ElementsInList konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ListSize konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If ListSize - ElementsInList >= AllocationStep * 2 Then
					'UPGRADE_WARNING: Die Standardeigenschaft des Objekts DefragTempIndex konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					DefragTempIndex = Defrag
					'if true we can save a full allocationstep of memory, so we going to
					' ReSize to save memory
					'UPGRADE_WARNING: Die Standardeigenschaft des Objekts AllocationStep konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					'UPGRADE_WARNING: Die Standardeigenschaft des Objekts DefragTempIndex konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ListSize konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					If ListSize - DefragTempIndex > AllocationStep * 2 Then
						'redim to save memoryspace
						'UPGRADE_WARNING: Die Standardeigenschaft des Objekts AllocationStep konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						'UPGRADE_WARNING: Die Standardeigenschaft des Objekts DefragTempIndex konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						Call ReSizeHeap(DefragTempIndex + AllocationStep)
					End If
				End If
			End If
		End If
	End Sub
	
	Public Sub SetDynamical(ByRef Enabled As Boolean, ByRef AllocStep As Short)
		Dim AllocationStep As Object
		Dim ListDynamically As Object
		' SetDynamical
		' defines if List is dynamically or not
		' - [IN] Enabled As Boolean: Dynamic on/off
		' - [IN] AllocStep As Integer: Step of increasing or if defrag is on decreasing
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ListDynamically konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		ListDynamically = Enabled
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts AllocationStep konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		AllocationStep = AllocStep
	End Sub
	
	Public Function getMinValEntry() As Short
        For i = 0 To ListSize
            'UPGRADE_ISSUE: Die vorherige Zeile konnte nicht analysiert werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="82EBB1AE-1FCB-4FEF-9E6C-8736A316F8A7"'
        Next i
	End Function
	
	Public Sub SetDefrag(ByRef Enabled As Boolean)
		Dim AutoDefrag As Object
		' SetDefrag
		' - [IN] Enabled As Boolean: turn AutoDefragmentation on/off
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts AutoDefrag konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		AutoDefrag = Enabled
	End Sub
	
	Private Function getLastUsed(Optional ByRef Start As Short = 0) As Short
		Dim Heap As Object
		Dim ListSize As Object
		' getLastUsed As Integer
		' get the last used element out of the array
		' - [IN] Optional Start As Integer: allows you to set a different start then the end
		'                                   of the list
		' - returns the index of the last as used marked element or 0 if List is empty
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Switch() konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Start = VB.Switch(Start = 0, ListSize, True, Start)
		
		For getLastUsed = Start To 1 Step -1
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap(getLastUsed).Used konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			If Heap(getLastUsed).Used Then
				Exit Function
			End If
		Next getLastUsed
	End Function
	
	Public Function Defrag() As Short
		Dim Heap As Object
		Dim ListSize As Object
		' Defrag
		' This is reodering to fill holes after this the
		' Keep in Mind that this defrag function does NOT keep the old pointeraddresses
		' if there is a old value stored somewhere it may NOT map to the same entry
		' its slow, so you shouldn't use this often, use it only if you have a large list which
		' is heavily fragmented so you may save a LOT of memory
		' - returns the last used entry or 0 if no entry is marked as used
		Dim lastUsedFound As Short
		Dim lastFirstFree As Short
		
		lastFirstFree = 1
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts ListSize konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
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
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap(lastFirstFree).Data konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap().Data konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Heap(lastFirstFree).Data = Heap(lastUsedFound).Data
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap().Used konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Heap(lastFirstFree).Used = True
			'now set this last used to unused and go ahead
			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap().Used konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Heap(lastUsedFound).Used = False
			While True
				
				Defrag = lastUsedFound
			End While
		Loop 
	End Function
End Class