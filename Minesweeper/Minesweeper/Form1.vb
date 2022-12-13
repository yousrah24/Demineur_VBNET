Public Class Form1
    Private Sub alphabetique(e As KeyPressEventArgs)
        If Not Char.IsLetter(e.KeyChar) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text.Length < 3 Then
            Label2.ForeColor = Color.Red
            ComboBox1.Focus()
        Else
            Label2.ForeColor = Color.Black
            Me.Hide()
            Form4.Show()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Form3.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim reponse As Integer = MsgBox("Voulez vous vraiment quitter le jeu ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question, "Confirmation")
        If reponse = MsgBoxResult.Yes Then
            enregistrerDonnees()
            Me.Close()
        End If
    End Sub
    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        alphabetique(e)
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        chagerDonnees()
        For Each j As Joueur In getTJoueurs()
            If Not ComboBox1.Items.Contains(j.nom) Then
                ComboBox1.Items.Add(j.nom)
            End If
        Next
        ComboBox1.Sorted = True
    End Sub

    Private Sub ComboBox1_LostFocus(sender As Object, e As EventArgs) Handles ComboBox1.LostFocus
        sender.Text = sender.Text.ToUpper
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        enregistrerDonnees()
    End Sub
End Class
