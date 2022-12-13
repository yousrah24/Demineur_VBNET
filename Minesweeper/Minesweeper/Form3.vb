Public Class Form3

    Private joueurs As New ArrayList()
    Sub trier()
        Dim temp As Joueur
        For i As Integer = 0 To joueurs.Count - 1
            For j As Integer = 0 To joueurs.Count - 1
                If comparer(joueurs(i), joueurs(j)) > 0 Then
                    temp = joueurs(i)
                    joueurs(i) = joueurs(j)
                    joueurs(j) = temp
                ElseIf comparer(joueurs(i), joueurs(j)) = 0 Then
                    If String.Compare(joueurs(i).nom, (joueurs(j).nom)) < 0 Then
                        temp = joueurs(i)
                        joueurs(i) = joueurs(j)
                        joueurs(j) = temp
                    End If
                End If
            Next
        Next
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each j As Joueur In getTJoueurs()
            Dim nom As String = j.nom
            If Not ListBox1.Items.Contains(nom) Then
                Dim temp As Joueur = getMeilleurScoreJoueur(nom)
                joueurs.Add(temp)
                ListBox1.Items.Add(temp.nom)
            End If
        Next
        ListBox1.Items.Clear()
        trier() 'on trie les joueurs selon leur score
        For Each j As Joueur In joueurs
            ListBox1.Items.Add(j.nom)
            ListBox3.Items.Add(j.temps)
            ListBox2.Items.Add(j.score)
        Next
        TextBox1.Text = System.IO.Path.GetFullPath("Data.txt")
        ComboBox1.Sorted = True
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex > -1 Then
            ListBox2.SelectedIndex = ListBox1.SelectedIndex
            ListBox3.SelectedIndex = ListBox1.SelectedIndex
        End If
        If Not ComboBox1.Items.Contains(ListBox1.Items.Item(ListBox1.SelectedIndex)) Then
            ComboBox1.Items.Add(ListBox1.Items.Item(ListBox1.SelectedIndex))
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedIndex > -1 Then
            ListBox1.SelectedIndex = ListBox2.SelectedIndex
            ListBox3.SelectedIndex = ListBox2.SelectedIndex
        End If
        If Not ComboBox1.Items.Contains(ListBox1.Items.Item(ListBox1.SelectedIndex)) Then
            ComboBox1.Items.Add(ListBox1.Items.Item(ListBox1.SelectedIndex))
        End If
    End Sub

    Private Sub ListBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox3.SelectedIndexChanged
        If ListBox3.SelectedIndex > -1 Then
            ListBox1.SelectedIndex = ListBox3.SelectedIndex
            ListBox2.SelectedIndex = ListBox3.SelectedIndex
        End If
        If Not ComboBox1.Items.Contains(ListBox1.Items.Item(ListBox1.SelectedIndex)) Then
            ComboBox1.Items.Add(ListBox1.Items.Item(ListBox1.SelectedIndex))
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim j As Joueur = getMeilleurScoreJoueur(ComboBox1.Text)
        Dim nb As Integer = getNBPartie(j.nom)
        Dim t As Integer = getTempsTotal(j.nom)
        MsgBox("Nom du Joueur : " & ComboBox1.Text & vbNewLine & "Meilleur score (nombre de cases révélée) : " & j.score & vbNewLine & "Temps pris (en secondes) : " & j.temps & vbNewLine & "Nombre de parties jouées : " & nb & vbNewLine & "Temps cumulé (en secondes) : " & t)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        joueurs.Reverse() 'on inverse l'ordre de la liste
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        For Each j As Joueur In joueurs
            ListBox1.Items.Add(j.nom)
            ListBox2.Items.Add(j.score)
            ListBox3.Items.Add(j.temps)
        Next
    End Sub

    Private Sub Form3_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Form1.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            If My.Computer.FileSystem.FileExists(FolderBrowserDialog1.SelectedPath + "\Data.txt") Then
                My.Computer.FileSystem.DeleteFile(FolderBrowserDialog1.SelectedPath + "\Data.txt")
            End If
            My.Computer.FileSystem.MoveFile(TextBox1.Text, FolderBrowserDialog1.SelectedPath + "\Data.txt")
            MsgBox("Le fichier de sauveagarde à ete déplacé sur le dossier " & FolderBrowserDialog1.SelectedPath)
            TextBox1.Text = FolderBrowserDialog1.SelectedPath + "\Data.txt"
        End If

    End Sub


End Class