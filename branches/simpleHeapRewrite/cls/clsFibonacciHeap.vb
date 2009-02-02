Option Strict On
Option Explicit On
Friend Class clsFibonacciHeap
	
	Dim HeapArray As clsSimpleHeap
	
	Public Sub create(ByRef Dimensions As Byte)
		Dim Heap As Object
		' create
		' - [IN] Dimensions as Byte: describe the max index in each direction
		
		'we going to allocate enouth memoryspace for our List
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap.setHeapSize konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Call Heap.setHeapSize((Dimensions + 1) * 2)
	End Sub
	
	Public Function getNoOfItems() As Short
		Dim Heap As Object
		' getNoOfItems As Integer
		' - returns the Number of Items which are currently saved in the heap
		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts Heap.getNoOfItems konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		getNoOfItems = Heap.getNoOfItems
	End Function
	
	Public Function isEmpty() As Boolean
		' isEmpty As Boolean
		' - returns true if no element is saved in the heap
		If getNoOfItems = 0 Then
			isEmpty = True
		End If
	End Function
	
	Public Function insert(ByRef pos() As Byte, ByRef value As Byte) As Short
		
	End Function
End Class