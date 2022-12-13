Imports System.IO

Module Module1

    Structure Joueur
        Dim nom As String
        Dim score As Integer
        Dim temps As Integer
    End Structure

    Function comparer(ByVal j As Joueur, ByVal j1 As Joueur) As Integer
        Return j.score - j1.score
    End Function

    Private TJoueurs As New ArrayList()
    Function getTJoueurs() As ArrayList
        Return TJoueurs
    End Function

    Sub AjouterJoueur(j As Joueur)
        TJoueurs.Add(j)
    End Sub

    Function getMeilleurScoreJoueur(ByVal nom As String) As Joueur
        Dim temp As Joueur = Nothing
        For Each j As Joueur In TJoueurs
            If j.nom = nom Then
                temp = j
                For Each j1 As Joueur In TJoueurs
                    If temp.score < j1.score And j1.nom = nom Then
                        temp = j1
                    End If
                Next
            End If
        Next
        Return temp
    End Function

    Function getNBPartie(ByVal nom As String) As Integer
        Dim n As Integer = 0
        For Each j As Joueur In TJoueurs
            If j.nom = nom Then
                n += 1
            End If
        Next
        Return n
    End Function

    Function getTempsTotal(ByVal nom As String) As Integer
        Dim n As Integer = 0
        For Each j As Joueur In TJoueurs
            If j.nom = nom Then
                n += j.temps
            End If
        Next
        Return n
    End Function

    Sub Main()
        Application.Run(Form1) 'appel du formulaire d'acceuil
    End Sub

    Sub chagerDonnees()
        Try
            ' Création d'une instance de StreamReader pour permettre la lecture de notre fichier
            Dim monStreamReader As StreamReader = New StreamReader(Environment.CurrentDirectory + "/Data.txt")
            Dim ligne As String
            'Lecture de toutes les lignes et ajout de chacune sur la liste des joueurs
            While (Not monStreamReader.EndOfStream)
                ligne = monStreamReader.ReadLine()
                Dim donnees As String() = ligne.Split(";")
                Dim j As Joueur
                Dim nom As String = donnees(0)
                j.nom = nom.ToUpper
                j.score = CInt(donnees(1))
                j.temps = CInt(donnees(2))
                AjouterJoueur(j)
            End While
            'Fermeture du StreamReader 
            monStreamReader.Close()
        Catch ex As Exception
            'Code exécuté en cas d'exception
            MsgBox("Une erreur est survenue au cours de la lecture !", MsgBoxStyle.Critical)
        End Try
    End Sub

    Sub enregistrerDonnees()
        Try
            'Instanciation du StreamWriter avec passage du nom du fichier 
            Dim monStreamWriter As StreamWriter = New StreamWriter(Environment.CurrentDirectory + "/Data.txt")
            'Ecriture du texte dans votre fichier
            For Each j As Joueur In TJoueurs
                monStreamWriter.WriteLine(j.nom.ToUpper & ";" & j.score & ";" & j.temps)
            Next
            'Fermeture du StreamWriter (Très important)
            monStreamWriter.Close()
        Catch ex As Exception
            'Code exécuté en cas d'exception
            MsgBox(ex.Message)
        End Try
    End Sub



End Module
