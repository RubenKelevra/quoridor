Option Strict On
Option Explicit On
Imports VB = Microsoft.VisualBasic

Friend Class clsSimpleHeap
	
    'this is the very basic heap implementation with dynamical allocation for 
    'Star-Algorithm
	
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
	
    Public Structure AstarData
        Dim bClosed As Boolean
        'costs which we think that we need to reach target (normally the minimal length)
        Dim usHeuristic As UShort
        'costs to get to thiss position
        Dim uiCosts As UInteger
        'parent-field index from list
        Dim uiParentFieldI As UInteger
        'position
        Dim B_X As Byte
        Dim B_Y As Byte
    End Structure

	' our heap of dataentries
    Private Heap() As AstarData

    ' the steps we increase our Heap()
    Private iAllocationStep As Integer
    'defines the minimal Heap() size, so if we clear it this size will be reached
    Private iMinListSize As Integer
    ' the last index of used elements in list
    Private iFirstEmptyIndex As Integer
    ' Number of Elements which are on the Open List
    Private iRemainingOpenElements As Integer

    Private iTemp As Integer

    Public Sub init(Optional ByVal AllocationStep As Integer = 0, Optional ByVal MinListSize As Integer = 0)
        If Not AllocationStep = 0 Then
            iAllocationStep = AllocationStep
        Else
            If iAllocationStep < 20 Then
                'we going to set default values for allocationStep
                iAllocationStep = 20
            End If
        End If
        If Not MinListSize = 0 Then
            iMinListSize = AllocationStep
        Else
            If iMinListSize < 40 Then
                'we going to set default values for allocationStep
                iMinListSize = 40
            End If
        End If

        ReDim Heap(iMinListSize)
        iFirstEmptyIndex = 0
        iRemainingOpenElements = 0
    End Sub

    Public Function getMin(ByVal id As Integer) As AstarData
        Static i As Integer
        Static minCosts As Integer
        Static index As Integer

        minCosts = Integer.MaxValue
        index = Integer.MaxValue


        For i = 0 To iFirstEmptyIndex - 1
            With Heap(i)
                If Not .bClosed Then
                    If .uiCosts < minCosts Then
                        index = i
                    End If
                End If
            End With
        Next
        If Not index = UInteger.MaxValue Then
            getMin = Heap(index)
        End If
    End Function

    Public Function setClosed(ByVal id As Integer) As Boolean
        If id < iFirstEmptyIndex Then
            Heap(id).bClosed = True
            Return True
        Else
            Return False
        End If
    End Function

    Public Function addOpen(ByVal X As Byte, ByVal Y As Byte, ByVal ParentFieldIndex As UInteger, _
                            ByVal Costs As UInteger, ByVal Heuristic As UShort) As Boolean

        ' --- memory allocation ---

        'if we going to ran out of arrayspace we going to allocate new memory
        If iFirstEmptyIndex > UBound(Heap) - LBound(Heap) Then
            'FIXME: this function really should get known how much memory is the limit to limit this to a
            'real world limit to ran against memoryleaks
            If iFirstEmptyIndex - 1 + iAllocationStep > 64515 Then 'if we would exceed this limit
                If Not iFirstEmptyIndex - 1 = 64515 Then 'we use this ultimate limit of memory which is valid, its a fieldsize of 254 x 254 - the internal limit
                    ReDim Preserve Heap(64515)
                Else 'if this is already set we going to raise an error 
                    Call Err.Raise(vbObjectError, "GC::Alloc", "Heap overflow")
                    Return False
                End If
            Else
                ReDim Preserve Heap(iFirstEmptyIndex - 1 + iAllocationStep)
            End If
        End If

        ' --- add value to array ---

        iFirstEmptyIndex = iFirstEmptyIndex + 1

        With Heap(iFirstEmptyIndex - 1)
            .B_X = X
            .B_Y = Y
            .bClosed = False
            .usHeuristic = Heuristic
            .uiCosts = Costs
            .uiParentFieldI = ParentFieldIndex
        End With

        Return True

    End Function

    Public Sub New()
        ReDim Heap(0)
    End Sub
End Class