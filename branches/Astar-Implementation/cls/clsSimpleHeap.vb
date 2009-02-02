Option Strict Off
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
    Private uiAllocationStep As UInteger
    'defines the minimal Heap() size, so if we clear it this size will be reached
    Private uiMinListSize As UInteger
    ' the last index of used elements in list
    Private uiFirstEmptyIndex As UInteger
    ' Number of Elements which are on the Open List
    Private uiRemainingOpenElements As UInteger

    Private uiTemp As UInteger
	
    Public Sub init(Optional ByVal AllocationStep As UInteger = 0, Optional ByVal MinListSize As UInteger = 0)
        If Not AllocationStep = 0 Then
            uiAllocationStep = AllocationStep
        Else
            If uiAllocationStep < 20 Then
                'we going to set default values for allocationStep
                uiAllocationStep = 20
            End If
        End If
        If Not MinListSize = 0 Then
            uiMinListSize = AllocationStep
        Else
            If uiMinListSize < 40 Then
                'we going to set default values for allocationStep
                uiMinListSize = 40
            End If
        End If

        ReDim Heap(uiMinListSize)
        uiFirstEmptyIndex = 0
        uiRemainingOpenElements = 0
    End Sub

    Public Function getMin(ByVal id As UInteger) As AstarData
        Static ui As UInteger
        Static minCosts As UInteger
        Static index As UInteger

        minCosts = UInteger.MaxValue
        index = UInteger.MaxValue


        For ui = 0 To uiFirstEmptyIndex - 1
            With Heap(ui)
                If Not .bClosed Then
                    If .uiCosts < minCosts Then
                        index = ui
                    End If
                End If
            End With
        Next
        If Not index = UInteger.MaxValue Then
            getMin = Heap(index)
        End If
    End Function

    Public Function setClosed(ByVal id As UInteger) As Boolean
        If id < uiFirstEmptyIndex Then
            Heap(id).bClosed = True
            Return True
        Else
            Return False
        End If
    End Function

    Public Function addOpen(ByVal X As Byte, ByVal Y As Byte, ByVal ParentFieldIndex As UInteger, _
                            ByVal Costs As UInteger, ByVal Heuristic As Short) As Boolean

        ' --- memory allocation ---

        'if we going to ran out of arrayspace we going to allocate new memory
        If uiFirstEmptyIndex > UBound(Heap) - LBound(Heap) Then
            If uiFirstEmptyIndex - 1 + uiAllocationStep > 64515 Then 'if we would exceed this limit
                If Not uiFirstEmptyIndex - 1 = 64515 Then 'we use this ultimate limit of memory which is valid, its a fieldsize of 254 x 254 - the internal limit
                    ReDim Preserve Heap(64515)
                Else 'if this is already set we going to raise an error 
                    Call Err.Raise(vbObjectError, "GC::Alloc", "Heap overflow")
                    Return False
                End If
            Else
                ReDim Preserve Heap(uiFirstEmptyIndex - 1 + uiAllocationStep)
            End If
        End If

        ' --- add value to array ---

        uiFirstEmptyIndex = uiFirstEmptyIndex + 1

        With Heap(uiFirstEmptyIndex - 1)
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