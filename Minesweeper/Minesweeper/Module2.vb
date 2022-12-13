Module Module2
    'variable pour le nombre de boutons
    Private n As Integer = 8
    Function getLg() As Integer
        Return n * n 'taille de la grille de jeu
    End Function

    Function getNbBtn() As Integer
        Return n
    End Function

    Sub setNbBtn(ByVal nb As Integer)
        n = nb
    End Sub
    'variable nombre de mines
    Private maxMine As Integer = 10
    Function getNbMines() As Integer
        Return maxMine
    End Function

    Sub setNbMines(ByVal nb As Integer)
        maxMine = nb
    End Sub

    'la liste des numeros des cases mine
    Private CaseMines As New ArrayList()
    Sub GenererMines()
        CaseMines.Clear()
        Dim num As Integer = CInt(Int((getLg() * Rnd()) + 1)) 'numero de la case choisie aléatoirement
        Dim nb As Integer
        For i As Integer = 1 To maxMine
            While estMine(num)
                num = CInt(Int((nb * Rnd()) + 1)) 'numero de la case choisie aléatoirement
            End While
            CaseMines.Add(num) 'ajout du numero de la case
            nb = n * i
        Next
    End Sub

    Private peutGenererGrille As Boolean

    Sub IsPeutGenererGrille(ByVal b As Boolean)
        peutGenererGrille = b
    End Sub

    'tableau contenant les numeros des cases de la grille
    Private Cases(,) As Integer

    Sub GenererBouttons(n As Integer)
        'variable  pour enregistrer la hauteur et largeur de chaque bouton
        Dim width As Integer = 75
        Dim height As Integer = 50
        ReDim Cases(n, n)
        Dim num As Integer = 1
        'boucle pour creer pour les n boutons
        For i As Integer = 0 To n - 1
            For j As Integer = 0 To n - 1
                Dim btn As New Button With {
                    .Text = "",
                    .Location = New Point(i * width, j * height),
                    .Size = New Size(70, 50),
                    .Visible = True,
                    .Name = num
                }
                'Ajout des boutons dans le panel
                Form2.Panel1.Controls.Add(btn)
                Cases(i, j) = num
                num += 1
            Next
        Next
    End Sub


    Private Function GetColonne(num As Integer) As Integer
        For i As Integer = 0 To getNbBtn() - 1
            For j As Integer = 0 To getNbBtn() - 1
                If Cases(i, j) = num Then
                    Return j
                End If
            Next j
        Next i
        Return -1
    End Function

    Private Function GetLigne(num As Integer) As Integer
        For i As Integer = 0 To getNbBtn() - 1
            For j As Integer = 0 To getNbBtn() - 1
                If Cases(i, j) = num Then
                    Return i
                End If
            Next j
        Next i
        Return -1
    End Function

    Function estMine(num As Integer) As Boolean
        Return CaseMines.Contains(num) 'si la case est dans la liste des mines
    End Function


    Private Function estMineVoisin(i As Integer, j As Integer) As Boolean
        If (i >= 0 And i < getNbBtn() And (j >= 0 And j < getNbBtn())) Then
            'si la case voisine est dans la liste des mines
            If estMine(Cases(i, j)) Then
                Return True
            End If
            Return False
        End If
        Return False
    End Function


    Function DonnerNBMinesVoisins(num As Integer) As Integer
        Dim i As Integer = GetLigne(num)
        Dim j As Integer = GetColonne(num)
        Dim n As Integer = 0
        If estMineVoisin(i - 1, j) Then 'voisin du haut
            n += 1
        End If
        If estMineVoisin(i - 1, j + 1) Then 'voisin du haut à droite
            n += 1
        End If
        If estMineVoisin(i - 1, j - 1) Then 'voisin du haut à gauche
            n += 1
        End If
        If estMineVoisin(i + 1, j) Then 'voisin du bas
            n += 1
        End If
        If estMineVoisin(i + 1, j - 1) Then 'voisin du bas à gauche
            n += 1
        End If
        If estMineVoisin(i + 1, j + 1) Then 'voisin du bas à droite
            n += 1
        End If
        If estMineVoisin(i, j + 1) Then 'voisin de droite
            n += 1
        End If
        If estMineVoisin(i, j - 1) Then 'voisin de gauche
            n += 1
        End If
        Return n
    End Function

    Private Function donnerIndexBtn(num As Integer) As Integer
        For i As Integer = 0 To Form2.Panel1.Controls.Count - 1
            'si le numero de case correspond au nom du bouton
            If num = Form2.Panel1.Controls.Item(i).Name Then
                Return i
            End If
        Next i
        Return -1
    End Function

    Private Sub devoilerBtn(ByVal j As Integer, ByRef n As Integer)
        If j <> -1 Then
            If n = 0 Then
                Form2.Panel1.Controls.Item(j).Text = ""
                Form2.Panel1.Controls.Item(j).BackColor = Color.LightGreen
            Else
                Form2.Panel1.Controls.Item(j).Text = n.ToString
                Form2.Panel1.Controls.Item(j).BackColor = Color.GhostWhite
                Form2.Panel1.Controls.Item(j).ForeColor = Color.Blue

            End If
            Form2.Panel1.Controls.Item(j).Enabled = False
        End If
    End Sub

    Private Function estVoisin(i As Integer, j As Integer) As Boolean
        If (i >= 0 And i < getNbBtn() And (j >= 0 And j < getNbBtn())) Then
            'si la case voisine est dans la liste des mines
            If estMine(Cases(i, j)) Then
                Return False
            End If
            Return True
        End If
        Return False
    End Function

    Private Voisins As New ArrayList()
    Private Sub getVoisins(ByVal num As Integer)
        Dim i As Integer = GetLigne(num)
        Dim j As Integer = GetColonne(num)
        If estVoisin(i - 1, j) Then 'voisin du haut
            Voisins.Add(Cases(i - 1, j))
        End If
        If estVoisin(i - 1, j + 1) Then 'voisin du haut à droite
            Voisins.Add(Cases(i - 1, j + 1))
        End If
        If estVoisin(i - 1, j - 1) Then 'voisin du haut à gauche
            Voisins.Add(Cases(i - 1, j - 1))
        End If
        If estVoisin(i + 1, j) Then 'voisin du bas
            Voisins.Add(Cases(i + 1, j))
        End If
        If estVoisin(i + 1, j - 1) Then 'voisin du bas à gauche
            Voisins.Add(Cases(i + 1, j - 1))
        End If
        If estVoisin(i + 1, j + 1) Then 'voisin du bas à droite
            Voisins.Add(Cases(i + 1, j + 1))
        End If
        If estVoisin(i, j + 1) Then 'voisin de droite
            Voisins.Add(Cases(i, j + 1))
        End If
        If estVoisin(i, j - 1) Then 'voisin de gauche
            Voisins.Add(Cases(i, j - 1))
        End If
    End Sub

    Sub DevoilerVoisins(ByVal n As Integer)
        getVoisins(n) 'on ajoute les voisins libre
        For i As Integer = 0 To Voisins.Count - 1
            Dim num = Voisins.Item(i)
            Dim j As Integer = donnerIndexBtn(num) 'on cherche l'index du bouton de la case num
            Dim nb = DonnerNBMinesVoisins(num) 'on calcule son nombre de mines avoisinantes
            devoilerBtn(j, nb) 'on devoile le bouton
        Next i
        Voisins.Clear() 'on vide la liste des voisins
    End Sub

    Function estPartieTermine() As Boolean
        For Each btn As Button In Form2.Panel1.Controls
            'si un bouton n'est pas deactivé et qu'il n'est pas une mine
            If btn.Enabled And Not estMine(btn.Name) Then
                Return False
            End If
        Next btn
        Return True
    End Function

    Sub produireCoup(n As Integer)
        Dim num As Integer = CInt(Int((n * Rnd()) + 1))
        Dim i As Integer = donnerIndexBtn(num)
        'choisie aleatoirement la case a devoiler
        Do While estMine(num) Or Not Form2.Panel1.Controls.Item(i).Enabled
            num = CInt(Int((n * Rnd()) + 1)) 'numero de la case choisie aléatoirement
            i = donnerIndexBtn(num) 'on cherche l'index de la case num
        Loop
        Dim nb As Integer = DonnerNBMinesVoisins(num) 'on calcule son nombre de mines avoisinante
        If nb = 0 Then
            Form2.Panel1.Controls.Item(i).Text = ""
            Form2.Panel1.Controls.Item(i).BackColor = Color.LightGreen
            DevoilerVoisins(num) 'on devoile ses voisins
        Else
            Form2.Panel1.Controls.Item(i).Text = nb.ToString
            Form2.Panel1.Controls.Item(i).BackColor = Color.GhostWhite
            Form2.Panel1.Controls.Item(i).ForeColor = Color.Blue
        End If
        Form2.Panel1.Controls.Item(i).Enabled = False
        MsgBox("La case " & num & " a été dévoilée" & vbNewLine & "Sens de lecture de la grille  : De haut en bas en partant de la gauche")
    End Sub

    Sub GenererGrille()
        If peutGenererGrille Then
            Dim nb As Integer = 4 'nombre de cases a  devoiler
            Dim n As Integer = CInt(Int((getLg() * Rnd()) + 1)) 'numero de la case choisie aléatoirement
            Dim l As New ArrayList
            'on devoile nb cases libre
            For i As Integer = 1 To nb
                Do While estMine(n) Or l.Contains(n)
                    n = CInt(Int((getLg() * Rnd()) + 1)) 'numero de la case choisie aléatoirement
                Loop
                l.Add(n)
            Next
            For Each num As Integer In l
                Dim k As Integer = DonnerNBMinesVoisins(num) 'on calcule son nombre de mines avoisinante
                Dim i As Integer = donnerIndexBtn(num) 'index du bouton
                If k = 0 Then
                    Form2.Panel1.Controls.Item(i).Text = ""
                    Form2.Panel1.Controls.Item(i).BackColor = Color.LightGreen
                    DevoilerVoisins(num) 'on devoile ses voisins
                Else
                    Form2.Panel1.Controls.Item(i).Text = k.ToString
                    Form2.Panel1.Controls.Item(i).BackColor = Color.GhostWhite
                    Form2.Panel1.Controls.Item(i).ForeColor = Color.Blue
                End If
                Form2.Panel1.Controls.Item(i).Enabled = False
            Next
        End If
    End Sub

End Module
