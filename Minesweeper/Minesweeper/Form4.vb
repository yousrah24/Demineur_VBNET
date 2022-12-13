Public Class Form4

    Private Sub numerique(e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        setNbBtn(Label11.Text)
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBox5.Checked = True
        HScrollBar1.Minimum = 20
        HScrollBar1.Maximum = 40
        Label5.Text = HScrollBar1.Value
        Label12.Text = Form1.ComboBox1.Text.Substring(0, 1) & Form1.ComboBox1.Text.Substring(1).ToLower & " " & Label12.Text & vbNewLine & vbNewLine & "Vous devez valider pour que l'option s'applique au jeu."
    End Sub


    Private nbOptions As Integer = 0
    Private Sub OptionsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged, CheckBox2.CheckedChanged, CheckBox3.CheckedChanged, CheckBox5.CheckedChanged, CheckBox4.CheckedChanged, CheckBox6.CheckedChanged, CheckBox7.CheckedChanged
        If sender.Checked Then
            nbOptions += 1
            If nbOptions > 3 Then
                MsgBox("Impossible de selectionner plus de 3 options", MsgBoxStyle.Critical)
                sender.Checked = False
                nbOptions -= 1
            End If
        Else
            nbOptions -= 1
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        numerique(e)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox2.Text <= 60 Then
            MsgBox("Vous devez mettre un chrono superieure à 60 sencondes", MsgBoxStyle.Information)
        Else
            Form2.lblTimer.Text = TextBox2.Text
            Panel3.Enabled = False
        End If
    End Sub

    Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar1.Scroll
        Label5.Text = HScrollBar1.Value
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        setNbMines(Label5.Text)
        Panel4.Enabled = False
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            Form2.Button2.Visible = True
            Label9.Visible = True
        Else
            Form2.Button2.Visible = False
            Label9.Visible = False
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            Form2.Button3.Visible = True
            Label10.Visible = True
        Else
            Form2.Button3.Visible = False
            Label10.Visible = False
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            Panel4.Visible = True
        Else
            Panel4.Visible = False
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked Then
            Panel2.Visible = True
        Else
            Panel2.Visible = False
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Panel2.Enabled = False
        setNbBtn(Label11.Text)
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked Then
            CheckBox1.Enabled = False
            CheckBox1.Checked = False
            CheckBox3.Enabled = False
            CheckBox3.Checked = False
            Form2.Panel2.Visible = False
        Else
            CheckBox1.Enabled = True
            CheckBox3.Enabled = True
            Form2.Panel2.Visible = True
        End If
    End Sub

    Private Sub Form4_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Form1.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub HScrollBar2_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar2.Scroll
        Label11.Text = HScrollBar2.Value
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked Then
            IsPeutGenererGrille(True)
        Else
            IsPeutGenererGrille(False)
        End If
    End Sub
End Class