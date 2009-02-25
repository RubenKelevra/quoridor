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

	' our heap of dataentries
    Private mHeap() As AstarData
    Public ReadOnly Nodes() As AstarData = mHeap
    ' smallest value is saved on this position
    Private musMinF_ValueIndex As UShort

    ' the steps we increase our Heap()
    Private musAllocationStep As UShort
    'defines the minimal Heap() size, so if we clear it this size will be reached
    Private musMinListSize As UShort
    ' the last index of used elements in list
    Private musFirstEmptyIndex As UShort
    ' Number of Elements which are on the Open List
    Private musRemainingOpenElements As UShort

    Private mi As Integer

    Private mB As Byte
    Private mB2 As Byte

    Public Function changeParent(ByRef usFieldID As UShort, ByRef usNewParent As UShort) As Boolean
        'Public changeParent As Boolean
        'Change the parent field index of the given node id
        ' - [IN] ByRef usFieldID As UShort: The given node id
        ' - [IN] ByRef usNewParent As UShort: The new parent field id
        ' - returns true if change has been done
        If (Not usFieldID < musFirstEmptyIndex) Or (Not usNewParent < musFirstEmptyIndex) Then
            Return False
        End If
        mHeap(usFieldID).usParentFieldI = usNewParent
        Return True
    End Function

    Public Function FindNode(ByVal x As Byte, ByVal y As Byte) As Integer
        'very slow, use cache instead
        For mi = 0 To musFirstEmptyIndex - 1
            If mHeap(mi).B_X = x Then
                If mHeap(mi).B_Y = y Then
                    Return mi
                End If
            End If
        Next
        Return -1
    End Function

    Public Function OpenNodesRemaning() As Boolean
        If musRemainingOpenElements = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Sub init(Optional ByVal AllocationStep As Integer = 0, Optional ByVal MinListSize As Integer = 0)
        If Not AllocationStep = 0 Then
            musAllocationStep = AllocationStep
        Else
            If musAllocationStep < 20 Then
                'we going to set default values for allocationStep
                musAllocationStep = 20
            End If
        End If
        If Not MinListSize = 0 Then
            musMinListSize = AllocationStep
        Else
            If musMinListSize < 40 Then
                'we going to set default values for allocationStep
                musMinListSize = 40
            End If
        End If

        musFirstEmptyIndex = 0
        musRemainingOpenElements = 0

        If Not UBound(mHeap) - LBound(mHeap) = musMinListSize Then
            ReDim mHeap(musMinListSize)
        End If
    End Sub

    Public Function getMin() As UShort
        Static minF_Score As UInteger

        minF_Score = UInteger.MaxValue
        getMin = UShort.MaxValue

        For mi = 0 To musFirstEmptyIndex - 1
            With mHeap(mi)
                If Not .bClosed Then
                    If .usF_Score < minF_Score Then
                        getMin = mi
                    End If
                End If
            End With
        Next mi
    End Function

    Public Function setClosed(ByVal id As Integer) As Boolean
        If id < musFirstEmptyIndex Then
            mHeap(id).bClosed = True
            Return True
        Else
            Return False
        End If
        musRemainingOpenElements -= 1
    End Function

    Public Function addOpen(ByVal X As Byte, ByVal Y As Byte, ByVal ParentFieldIndex As UInteger, _
                            ByVal Costs As UInteger, ByVal F_Value As UInteger, ByVal Heuristic As UShort) As UShort

        ' --- memory allocation ---

        'if we going to ran out of arrayspace we going to allocate new memory
        If musFirstEmptyIndex > UBound(mHeap) - LBound(mHeap) Then
            'FIXME: this function really should get known how much memory is the limit to limit this to a
            'real world limit to ran against memoryleaks
            If musFirstEmptyIndex - 1 + musAllocationStep > 64515 Then 'if we would exceed this limit
                If Not musFirstEmptyIndex - 1 = 64515 Then 'we use this ultimate limit of memory which is valid, its a fieldsize of 254 x 254 - the internal limit
                    ReDim Preserve mHeap(64515)
                Else 'if this is already set we going to raise an error 
                    Call Err.Raise(vbObjectError, "GC::Alloc", "Heap overflow")
                    Return UShort.MaxValue
                End If
            Else
                ReDim Preserve mHeap(musFirstEmptyIndex - 1 + musAllocationStep)
            End If
        End If

        ' --- add value to array ---

        musFirstEmptyIndex += 1

        With mHeap(musFirstEmptyIndex - 1) 'added +1 before
            .B_X = X
            .B_Y = Y
            .bClosed = False
            .usHeuristic = Heuristic
            .uiCosts = Costs
            .usParentFieldI = ParentFieldIndex
            .usF_Score = F_Value
        End With

        musRemainingOpenElements += 1

        Return musFirstEmptyIndex - 1
    End Function

    Public Sub New()
        ReDim mHeap(0)
    End Sub
End Class