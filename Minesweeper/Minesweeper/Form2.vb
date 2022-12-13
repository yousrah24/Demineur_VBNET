Public Class Form2
    Private j As Joueur
    Private nb As Integer = 0
    Private t As Integer = 0
    Private nbCoups As Integer = 0

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Label2.Text & " " & Form1.ComboBox1.Text.Substring(0, 1) & Form1.ComboBox1.Text.Substring(1).ToLower
        monTimer.Interval = 2000
        j.nom = Form1.ComboBox1.Text
        t = lblTimer.Text
        GenererBouttons(getNbBtn())
        For Each btn As Button In Panel1.Controls
            'Ajout de l'evenement click du bouton, à chaque clic du bouton la methode OnButton_Click est appelé
            AddHandler btn.Click, AddressOf OnButton_Click
        Next
        GenererMines()
        GenererGrille()

    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim reponse As Integer = MsgBox("Voulez vous vraiment abandonner la partie ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question, "Confirmation")
        If reponse = MsgBoxResult.Yes Then
            Me.Close()
            Form1.Show()
        End if
    End Sub

    Private Sub monTimer_Tick(sender As Object, e As EventArgs) Handles monTimer.Tick
        lblTimer.Text = lblTimer.Text - 1
        If lblTimer.Text = "0" Then
            monTimer.Stop()
            j.score = nb
            j.temps = t - lblTimer.Text
            AjouterJoueur(j)
            MsgBox("Temps ecoulé !" & vbNewLine & "Vous avez révélé " & nb & " Cases")
            Me.Close()
            Form1.Show()
        End If
        If lblTimer.Text = "4" Then
            lblTimer.ForeColor = Color.Red
        End If
    End Sub

    Private Sub OnButton_Click(sender As Button, e As MouseEventArgs)
        If Panel2.Visible Then
            monTimer.Enabled = True
            monTimer.Start()
        End If

        If Panel1.Enabled Then
            If estMine(sender.Name) Then
                sender.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + "/Bomb.png")
                sender.BackgroundImageLayout = ImageLayout.Center
                monTimer.Stop()
                j.score = nb
                j.temps = t - lblTimer.Text
                MsgBox("BOOM ! EXPLOSION !" & vbNewLine & "Vous avez perdue :(" & vbNewLine & "Vous avez révélé " & nb & " cases en " & t - lblTimer.Text & " secondes")
                AjouterJoueur(j) 'on ajoute le joueur
                Me.Close()
                Form1.Show()
            Else
                nb += 1
                Dim n = DonnerNBMinesVoisins(sender.Name)
                If n = 0 Then
                    sender.Text = ""
                    sender.BackColor = Color.LightGreen
                    DevoilerVoisins(sender.Name)
                Else
                    sender.Text = n.ToString
                    sender.BackColor = Color.GhostWhite
                    sender.ForeColor = Color.Blue
                End If
                sender.Enabled = False
                If estPartieTermine() Then
                    j.score = nb
                    j.temps = 60 - lblTimer.Text
                    MsgBox("Partie terminée ! " & vbNewLine & "Vous avez révélé " & nb & " cases en " & t - lblTimer.Text & " secondes")
                    AjouterJoueur(j) 'on ajoute le joueur
                    Me.Close()
                    Form1.Show()
                End If
            End If
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Panel1.Enabled Then
            If nbCoups < 3 Then
                produireCoup(getLg())
            Else
                MsgBox("Vous avez déjà utilisé toute vos chances", MsgBoxStyle.Information)
            End If
            nbCoups += 1
        Else
            MsgBox("Le jeu est mis en pause, impossible de dévoiler une case", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Panel1.Enabled Then
            Panel1.Enabled = False
            monTimer.Enabled = False
        Else
            Panel1.Enabled = True
            monTimer.Enabled = True
        End If
    End Sub

    Private Sub Form2_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Form1.Show()
    End Sub


End Class