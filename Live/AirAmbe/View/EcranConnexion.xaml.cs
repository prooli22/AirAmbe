// Nom : Olivier Provost.
// Date : 2016-12-09.


using AirAmbe.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AirAmbe.ViewModel;
using System.Diagnostics;

namespace AirAmbe
{
    /// <summary>
    /// Logique d'interaction pour EcranConnexion.xaml
    /// </summary>
    public partial class EcranConnexion : Window
    {
        /// <summary>
        /// Constructeur de l'écran de Connexion.
        /// </summary>
        public EcranConnexion()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Appelé lorsqu'on appuie sur la touche Connexion.
        /// </summary>
        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            TrouverUtilisateur(txtUser.Text.ToString(), MD5.Hash(txtMDP.Password.ToString()));
        }


        /// <summary>
        /// Appelé lorsqu'on appuie sur la touche Entrée dans le champs du mot de passe. 
        /// </summary>
        private void txtMDP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                TrouverUtilisateur(txtUser.Text.ToString(), MD5.Hash(txtMDP.Password.ToString()));
        }

        
        /// <summary>
        /// Appelé lorsqu'on clique sur bouton Observateur.
        /// </summary>
        private void btnObservateur_Click(object sender, RoutedEventArgs e)
        {
            EcranControleur C = new EcranControleur(null);
            C.Show();
            this.Close();
        }


        /// <summary>
        /// Appelé lorsqu'on clique sur le bouton Quitter.
        /// </summary>
        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            // Demande une confirmation à l'utilisateur avant de quitter.
            MessageBoxResult resultat = MessageBox.Show("Voulez-vous vraiment quitter l'application ?", "Quitter", MessageBoxButton.YesNo, MessageBoxImage.Stop);

            if (resultat == MessageBoxResult.Yes)
                this.Close();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomUtilisateur"> Nom d'utilisateur entré dans le champs. </param>
        /// <param name="motPasse"> Mot de passe entré dans le champs. </param>
        private void TrouverUtilisateur(string nomUtilisateur, string motPasse)
        {
            Utilisateur U = new Utilisateur();
            UtilisateurAS uAs = new UtilisateurAS();

            U = uAs.Recuperer(nomUtilisateur, motPasse);

            // Si l'utilisateur retourné est null, son nom d'utilisateur ou son mot de passe est incorrect.
            // On affiche un erreur à l'écran.
            if (U == null)
            {

                lblErreur.Visibility = Visibility.Visible;
                lblUser.Foreground = Brushes.Red;
                lblMDP.Foreground = Brushes.Red;
                return;
            }

            // Si le type de l'utilisateur est admin, on affiche l'écran admin, sinon on affiche l'écran controleur.
            if (U.TypeUtilisateur == Type.Administrateur.ToString())
                AfficherEcranAdministrateur();
            else
                AfficherEcranControleur(U);

        }


        /// <summary>
        /// Affiche l'écran du Contrôleur.
        /// </summary>
        /// <param name="U"></param>
        private void AfficherEcranControleur(Utilisateur U)
        {
            EcranControleur C = new EcranControleur(U);      
            C.Show();
            this.Close();
        }


        /// <summary>
        /// Affiche l'écran de l'Administrateur.
        /// </summary>
        private void AfficherEcranAdministrateur()
        {
            EcranAdministrateur A = new EcranAdministrateur(null, false);
            A.Show();
            this.Close();
        }


        /// <summary>
        /// Appelé lorsqu'on clique sur le bouton Guide et ouvre le guide en format PDF.
        /// </summary>
        private void btnGuide_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "guide.pdf";
            process.Start();
            process.WaitForExit();
        }
    }
}
