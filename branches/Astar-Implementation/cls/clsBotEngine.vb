'Option Strict Off
'Option Explicit On
'Friend Class clsBotEngine

'    'Function Astar(ByRef startPoint() As Byte, ByRef targetDir As Byte) As Byte()
'    '	Dim openlist As clsFibonacciHeap
'    '	Dim closedlist As clsFibonacciHeap
'    '	'UPGRADE_ISSUE: HeapData Objekt wurde nicht aktualisiert. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
'    '	Dim currentNode As HeapData
'    '	' Initialisierung der Open List, die Closed List ist noch leer
'    '	' (die Priorität bzw. der f Wert des Startknotens ist unerheblich)
'    '	openlist.insert(startPoint, 0)
'    '	' diese Schleife wird durchlaufen bis entweder
'    '	' - die optimale Lösung gefunden wurde oder
'    '	' - feststeht, dass keine Lösung existiert
'    '	Do 
'    '		' Knoten mit dem geringsten f Wert aus der Open List entfernen
'    '		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts currentNode konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
'    '		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts openlist.removeMin konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
'    '		currentNode = openlist.removeMin()
'    '		' wurde das Ziel gefunden?
'    '		if currentNode == zielknoten then
'    '		'UPGRADE_ISSUE: Die vorherige Zeile konnte nicht analysiert werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="82EBB1AE-1FCB-4FEF-9E6C-8736A316F8A7"'
'    '		'UPGRADE_WARNING: Return hat ein neues Verhalten. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
'    '		Return 
'    '		return PathFound
'    '		'UPGRADE_ISSUE: Die vorherige Zeile konnte nicht analysiert werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="82EBB1AE-1FCB-4FEF-9E6C-8736A316F8A7"'
'    '		' Nachfolgeknoten auf die Open List setzen
'    '		expandNode(currentNode)
'    '		' der aktuelle Knoten ist nun abschließend untersucht
'    '		'UPGRADE_WARNING: Die Standardeigenschaft des Objekts closedlist.Add konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
'    '		closedlist.Add(currentNode)
'    '		While Not openlist.IsEmpty()
'    '			' die Open List ist leer, es existiert kein Pfad zum Ziel
'    '			MsgBox("AI has occoured an unexpected error", MsgBoxStyle.Critical, "AI unexpected error")
'    '		End While
'    '	Loop 
'    'End Function

'	' überprüft alle Nachfolgeknoten und fügt sie der Open List hinzu, wenn entweder
'	' - der Nachfolgeknoten zum ersten Mal gefunden wird oder
'	' - ein besserer Weg zu diesem Knoten gefunden wird
'	Function expandNode(ByRef currentNode As Object) As Object
'		Dim closedlist As Object
'		Dim f As Object
'		Dim openlist As Object
'		'UPGRADE_NOTE: continue wurde aktualisiert auf continue_Renamed. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
'		Dim continue_Renamed As Object
'		Dim successor As Object
'		For	Each successor In currentNode
'			' wenn der Nachfolgeknoten bereits auf der Closed List ist - tue nichts
'			'UPGRADE_WARNING: Die Standardeigenschaft des Objekts closedlist.contains konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
'			If closedlist.contains(successor) Then
'				continue_Renamed()
'				' f Wert für den neuen Weg berechnen: g Wert des Vorgängers plus die Kosten der
'				' gerade benutzten Kante plus die geschätzten Kosten von Nachfolger bis Ziel
'				f := g(currentNode) + c(currentNode, successor) + h(successor)
'				'UPGRADE_ISSUE: Die vorherige Zeile konnte nicht analysiert werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="82EBB1AE-1FCB-4FEF-9E6C-8736A316F8A7"'
'				' wenn der Nachfolgeknoten bereits auf der Open List ist,
'				' aber der neue Weg länger ist als der alte - tue nichts
'				'UPGRADE_WARNING: Die Standardeigenschaft des Objekts openlist.find konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
'				'UPGRADE_WARNING: Die Standardeigenschaft des Objekts openlist.contains konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
'				If openlist.contains(successor) And f > openlist.find(successor).f Then
'					continue_Renamed()
'					' Vorgängerzeiger setzen
'					successor.predecessor := currentNode
'					'UPGRADE_ISSUE: Die vorherige Zeile konnte nicht analysiert werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="82EBB1AE-1FCB-4FEF-9E6C-8736A316F8A7"'
'					' f Wert des Knotens in der Open List aktualisieren
'					' bzw. Knoten mit f Wert in die Open List einfügen
'					'UPGRADE_WARNING: Die Standardeigenschaft des Objekts openlist.contains konnte nicht aufgelöst werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
'					If openlist.contains(successor) Then
'						openlist.decreaseKey(successor, f)
'						'UPGRADE_ISSUE: Die vorherige Zeile konnte nicht analysiert werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="82EBB1AE-1FCB-4FEF-9E6C-8736A316F8A7"'
'					Else
'						openlist.enqueue(successor, f)
'						'UPGRADE_ISSUE: Die vorherige Zeile konnte nicht analysiert werden. Klicken Sie hier für weitere Informationen: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="82EBB1AE-1FCB-4FEF-9E6C-8736A316F8A7"'
'						End
'						End
'					End If
'				End If
'			End If
'		Next successor
'	End Function
'End Class