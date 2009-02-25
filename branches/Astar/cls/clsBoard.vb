Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class clsBoard
	
	' cls/clsBoard.cls - Part of Quoridor http://code.google.com/p/quoridor/
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
	
	'figures
    Protected Friend NoOfPlayer As Byte
    Protected Friend NoOfBricks As Byte 'per player
    Protected Friend Players() As clsPlayer
    Protected Friend Blocker() As clsBrick
    Protected Friend Dimensions As Byte
    Protected Friend activePlayer As Byte

    Private B As Byte

    ' --- Cache for faster search for free ways and free stone positions used heaviely in AI ---
    Private BlockedVerticalBricks(,) As Boolean 'true is blocked
    Private BlockedHorizontalBricks(,) As Boolean 'true if blocked
    Private BlockedBricksLastUpdate As Byte 'last added stone +1

    Private Sub refreshBrickCache()
        Static newBrickIndex As Byte
        'fixme 
    End Sub

    Public Function getPlayerType(ByRef Player As Byte) As Byte
        ' getPlayerType As Byte
        ' get the type of player
        ' - [IN] Player As Byte: player number which should be asked
        ' - returns the type of player, see clsPlayer.setType()
        getPlayerType = Players(Player).getPlayerType()
    End Function
	
    Public Function getPlayerForLocation(ByRef pos As Position) As Byte
        ' getPlayerForLocation As Byte
        ' - [IN] pos as Position: defines the position which should be checked
        ' - returns the player number if a player is on the position, else 255
        For B = CByte(LBound(Players)) To CByte(UBound(Players))
            If comparePos(pos, Players(B).getLocation) Then
                Return B
                Exit Function
            End If
        Next B
        Return 255 'if no player found return 255
    End Function
	
    Public Function checkMove(ByRef pos As Position, ByVal direction As Byte) As Boolean
        ' checkMove As Boolean
        ' checks for possibility to move to given direction
        ' - [IN] ByRef pos As Position: defines position
        ' - [IN] ByVal dir As Byte: defines direction
        ' - returns True if move is possible
        If Not checkFrontWall(pos, direction) Then 'there is no wall
            'check for borders
            Select Case direction
                Case 0 'bottom
                    If pos.Y = Dimensions Then
                        Return False
                    End If
                Case 1 'right
                    If pos.X = Dimensions Then
                        Return False
                    End If
                Case 2 'top
                    If pos.Y = 0 Then
                        Return False
                    End If
                Case 3 'left
                    If pos.X = 0 Then
                        Return False
                    End If
            End Select

            'field seems to be free and with no wall between us and it
            Return True
        End If
    End Function

    Public Function setPlayername(ByRef BPlayerNo As Byte, ByRef txtField As TextBox) As Byte
        'Public setPlayername As Boolean
        'sets the playername
        ' - [IN] ByRef BPlayerNo As Byte: definites the player number
        ' - :
        '0 = successfull
        '1 = player index out of range
        '2 = no player is definied

        'check if Players() was redimed
        If Players Is Nothing Then
            Return 2
        End If

        'check for valid player index
        If BPlayerNo > UBound(Players) - LBound(Players) Then
            Return 1
        End If
        Players(BPlayerNo).setPlayerName(txtField.Text)
    End Function

    Public Function getPlayername(ByRef BPlayerNo As Byte, ByRef lblField As Label) As Byte
        'Public getPlayername As Boolean
        'gets the playername
        ' - [IN] ByRef BPlayerNo As Byte: definites the player number
        ' - :
        '0 = successfull
        '1 = player index out of range
        '2 = no player is definied

        'check if Players() was redimed
        If Players Is Nothing Then
            Return 2
        End If

        'check for valid player index
        If BPlayerNo > UBound(Players) - LBound(Players) Then
            Return 1
        End If
        lblField.Text = Players(BPlayerNo).getPlayerName
    End Function

    Public Function movePlayer(ByRef i As Byte, ByRef direction As Byte) As Boolean
        If checkMove(Players(i).getLocation, direction) Then
            movePlayer = Players(i).Move(direction)
            For B = 0 To UBound(Players) - LBound(Players)
                If Not B = i Then
                    If comparePos(Players(i).getLocation, Players(B).getLocation) Then
                        Call Err.Raise(vbObjectError, "clsBoard.movePlayer", "Player-position already used")
                    End If
                End If
            Next B
        Else
            movePlayer = False
        End If
    End Function

    Public Function saveWall(ByRef Br As clsBrick, ByRef Player As Byte) As Byte
        'returnvalues
        '0 = ok
        '1 = player have no blockers left
        '2 = cannot be used cause you would envelop a figure or position is used by another blocker
        '3 = player index out of range
        '4 = no space in Bricks() left
        Static foundEmptyPosition As Boolean

        'check for valid player index
        If Player > UBound(Players) - LBound(Players) Then
            Return 3
        End If

        If Players(Player).getBricks = 0 Then
            Return 1
        End If

        If Not checkPlaceWall(Br.Position.X, Br.Position.Y, (Br.Horizontal)) Then
            Return 2
        End If

        foundEmptyPosition = False

        'running thru setted Blockers to get last index of the placed ones
        For B = CByte(LBound(Blocker)) To CByte(UBound(Blocker))
            If Not Blocker(B).Placed Then
                foundEmptyPosition = True
                Exit For
            End If
        Next B
        If Not foundEmptyPosition Then
            Return 4
        End If
        Blocker(B).Placed = True
        Blocker(B).Horizontal = Br.Horizontal
        Blocker(B).Position.X = Br.Position.X
        Blocker(B).Position.Y = Br.Position.Y
        Players(Player).subtractStone()
        Return 0
    End Function

    Public Function getPlayerColor(ByRef i As Byte) As System.Drawing.Color
        Select Case i
            Case 0
                Return System.Drawing.Color.Blue
            Case 1
                If NoOfPlayer = 3 Then '4 player game
                    Return System.Drawing.Color.Lime
                Else
                    Return System.Drawing.Color.Red
                End If
            Case 2
                If NoOfPlayer = 3 Then '4 player game
                    Return System.Drawing.Color.Red
                Else
                    Return System.Drawing.Color.Lime
                End If
            Case 3
                Return System.Drawing.Color.Yellow
        End Select
    End Function

    Public Function getDimension() As Byte
        getDimension = Dimensions
    End Function

    Public Function getPlayerLocation(ByRef i As Byte) As Position
        If i >= NoOfPlayer Then
            getPlayerLocation = Players(i).getLocation
        Else
            getPlayerLocation = xy2position(255, 255)
        End If
    End Function

    Public Function getActivePlayer() As Byte
        getActivePlayer = activePlayer
    End Function

    Public Sub NextTurn()
        activePlayer = getNextPlayer(activePlayer, NoOfPlayer)
    End Sub

    Public Function checkFrontWall(ByRef pos As Position, ByRef direction As Byte) As Boolean
        'if true there are a wall in front of the position and with this direction
        Static stone As clsBrick

        For B = CByte(LBound(Blocker)) To CByte(UBound(Blocker) - LBound(Blocker))
            stone = Blocker(B)
            'if we found the first stone which is not placed or which have a default value as position we exiting
            If stone.Placed = False Then
                Exit For
            End If
            Select Case direction
                Case 0 'bottom
                    If stone.Horizontal = True And stone.Position.Y = pos.Y Then
                        'stone is bottom on right
                        If stone.Position.X = pos.X Then GoTo returnTrue
                        'stone is bottom on left
                        If stone.Position.X + 1 = pos.X Then GoTo returnTrue
                    End If
                Case 1 'right
                    If stone.Horizontal = False And stone.Position.X = pos.X Then
                        'stone is upper on right
                        If stone.Position.Y + 1 = pos.Y Then GoTo returnTrue
                        'stone is lower on right
                        If stone.Position.Y = pos.Y Then GoTo returnTrue
                    End If
                Case 2 'top
                    If stone.Horizontal = True And stone.Position.Y + 1 = pos.Y Then
                        'stone is top on right
                        If stone.Position.X = pos.X Then GoTo returnTrue
                        'stone is top on left
                        If stone.Position.X + 1 = pos.X Then GoTo returnTrue
                    End If
                Case 3 'left
                    If stone.Horizontal = False And stone.Position.X + 1 = pos.X Then
                        'stone is upper on left
                        If stone.Position.Y + 1 = pos.Y Then GoTo returnTrue
                        'stone is lower on left
                        If stone.Position.Y = pos.Y Then GoTo returnTrue
                    End If
            End Select
        Next B
        'no stone have been found which blocks our way
        Return False
returnTrue:
        Return True
    End Function

    Private Function NoToDir(ByRef i As Byte) As Byte
        'part of checkPlaceWall
        NoToDir = CByte(VB.Switch(0 < i - 3, i - 3, True, i))
    End Function

    Private Function NoToField(ByRef i As Byte) As Byte
        'part of checkPlaceWall
        NoToField = CByte(VB.Switch(0 < i - 3, 0, True, 1))
    End Function

    Public Function checkPlaceWall(ByRef x As Byte, ByRef y As Byte, ByRef Horizontal As Boolean) As Boolean
        'if true a wall can be placed on this position
        Static stone As clsBrick
        Static i As Byte

        For i = CByte(LBound(Blocker)) To CByte(UBound(Blocker))
            stone = Blocker(i)
            'if we found the first stone which is not placed or which have a default value as position we exiting
            If stone.Placed = False Or stone.Position.X = 255 Or stone.Position.Y = 255 Then
                Exit For
            End If
            'stone is on the same position
            If stone.Position.X = x And stone.Position.Y = y Then
                GoTo returnFalse_checkPlaceWall
                'same level but left and Horizontal
            ElseIf stone.Position.X = x + 1 And stone.Position.Y = y And Horizontal And stone.Horizontal Then
                GoTo returnFalse_checkPlaceWall
                'same level but right and Horizontal
            ElseIf stone.Position.X + 1 = x And stone.Position.Y = y And Horizontal And stone.Horizontal Then
                GoTo returnFalse_checkPlaceWall
                'up but same row and not Horizontal
            ElseIf stone.Position.X = x And stone.Position.Y = y + 1 And Not Horizontal And Not stone.Horizontal Then
                GoTo returnFalse_checkPlaceWall
                'down but same row and not Horizontal
            ElseIf stone.Position.X = x And stone.Position.Y + 1 = y And Not Horizontal And Not stone.Horizontal Then
                GoTo returnFalse_checkPlaceWall
            End If

        Next i
        'no stone have been found which blocks our new stone
        checkPlaceWall = True
        Exit Function
returnFalse_checkPlaceWall:
        checkPlaceWall = False
        Exit Function
    End Function

    Public Function getNameOfPlayer(ByRef i As Byte) As String
        Try
            Return Players(i).getPlayerName
        Catch
            Return String.Empty
        End Try
    End Function

    Public Function getNoOfPlayer() As Byte
        getNoOfPlayer = NoOfPlayer
    End Function

    Public Function getPlayerTarget(ByRef i As Byte) As Byte
        getPlayerTarget = Players(i).getTarget
    End Function

    Public Function getRemainingPlayerBricks(ByRef i As Byte) As Byte
        getRemainingPlayerBricks = Players(i).getBricks
    End Function

    Public Function getWalls() As clsBrick()
        getWalls = CType(VB6.CopyArray(Blocker), clsBrick())
    End Function

    Private Function getPlayerPositions() As Position()
        Dim i As Byte
        Dim returnvalue() As Position
        ReDim returnvalue(NoOfPlayer)
        For i = 0 To NoOfPlayer
            returnvalue(i).X = getPlayerLocation(i).X
            returnvalue(i).Y = getPlayerLocation(i).Y
        Next i
        getPlayerPositions = returnvalue
    End Function

    Public Sub create(ByRef NoOfPl As Byte, ByRef Dimension As Byte)
        Dim pStarts(3) As Position
        Dim B As Byte
        Dim B2 As Byte

        'check input value of NoOfPl if not 4 set to 2 - started with 0
        NoOfPlayer = CByte(VB.Switch(NoOfPl = 3, 3, True, 1))
        NoOfBricks = getNoOfWalls(NoOfPlayer)
        'if the dimension is smaller than the NoOfPlayer the field will be made bigger
        'if dimension = 255 it'll set to 254 otherwise we may get unexpected results of some functions
        ' because we use 255 as defaultvalue in some functions
        Dimensions = CByte(VB.Switch(Dimension < NoOfPlayer, NoOfPlayer, Dimension < 255, Dimension, Dimension = 255, 254))

        ReDim Players(NoOfPlayer)
        For B = 0 To NoOfPlayer
            Players(B) = New clsPlayer
        Next B

        'For B = 0 To 3
        '    pStarts(B) = New Position
        'Next B

        'calc player starts - players counted ccw
        pStarts(0).X = CByte(Dimensions / 2)
        pStarts(1).Y = pStarts(0).X
        pStarts(0).Y = 0
        pStarts(3).X = pStarts(0).Y
        pStarts(1).X = Dimensions
        pStarts(2).Y = pStarts(1).X
        'subtract to get the other line near the center for the mirrored player
        pStarts(2).X = Dimensions - CByte(Dimensions / 2)
        pStarts(3).Y = pStarts(2).X

        'set player starts
        For B = CByte(LBound(Players)) To CByte(UBound(Players))
            If NoOfPlayer = 1 And B = 1 Then
                B2 = CByte(B + 1)
                'change 2th player's startposition to 3rd players's
                'if two players playing
            Else
                B2 = B
            End If
            'running create routine
            Players(B).create(pStarts(B2).X, pStarts(B2).Y, NoOfBricks, Player2Target(B, NoOfPlayer))
        Next B

        'reserve space for blockers
        '+1-1 cause we need to move one right and left because of 0 starting values
        ReDim Blocker((NoOfPlayer + 1) * (NoOfBricks + 1) - 1)

        'set blockers start values
        For B = 0 To CByte(UBound(Blocker))
            Blocker(B) = New clsBrick
            Blocker(B).Horizontal = False
            Blocker(B).Position.X = 255
            Blocker(B).Position.Y = 255
        Next B
        'Players(1).startAI Blocker, pStarts, 1, Dimensions
    End Sub
End Class
