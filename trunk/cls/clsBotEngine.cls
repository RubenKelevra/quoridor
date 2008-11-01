VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsBotEngine"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Function Astar(startPoint() As Byte, targetDir As Byte) As Byte()
    Dim openlist As clsFibonacciHeap
    Dim closedlist As clsFibonacciHeap
    Dim currentNode As HeapData
    ' Initialisierung der Open List, die Closed List ist noch leer
    ' (die Priorit�t bzw. der f Wert des Startknotens ist unerheblich)
    openlist.insert startPoint, 0
    ' diese Schleife wird durchlaufen bis entweder
    ' - die optimale L�sung gefunden wurde oder
    ' - feststeht, dass keine L�sung existiert
    Do
        ' Knoten mit dem geringsten f Wert aus der Open List entfernen
        currentNode = openlist.removeMin()
        ' wurde das Ziel gefunden?
        if currentNode == zielknoten then
            return PathFound
        ' Nachfolgeknoten auf die Open List setzen
        expandNode (currentNode)
        ' der aktuelle Knoten ist nun abschlie�end untersucht
        closedlist.Add (currentNode)
    While Not openlist.IsEmpty()
    ' die Open List ist leer, es existiert kein Pfad zum Ziel
    MsgBox "AI has occoured an unexpected error", vbCritical, "AI unexpected error"
End Function

' �berpr�ft alle Nachfolgeknoten und f�gt sie der Open List hinzu, wenn entweder
' - der Nachfolgeknoten zum ersten Mal gefunden wird oder
' - ein besserer Weg zu diesem Knoten gefunden wird
Function expandNode(currentNode)
    For Each successor In currentNode
        ' wenn der Nachfolgeknoten bereits auf der Closed List ist - tue nichts
        If closedlist.contains(successor) Then
            continue
        ' f Wert f�r den neuen Weg berechnen: g Wert des Vorg�ngers plus die Kosten der
        ' gerade benutzten Kante plus die gesch�tzten Kosten von Nachfolger bis Ziel
        f := g(currentNode) + c(currentNode, successor) + h(successor)
        ' wenn der Nachfolgeknoten bereits auf der Open List ist,
        ' aber der neue Weg l�nger ist als der alte - tue nichts
        If openlist.contains(successor) And f > openlist.find(successor).f Then
            continue
        ' Vorg�ngerzeiger setzen
        successor.predecessor := currentNode
        ' f Wert des Knotens in der Open List aktualisieren
        ' bzw. Knoten mit f Wert in die Open List einf�gen
        If openlist.contains(successor) Then
            openlist.decreaseKey(successor, f)
        Else
            openlist.enqueue(successor, f)
    End
End