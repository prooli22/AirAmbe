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

namespace AirAmbe
{
    /// <summary>
    /// Logique d'interaction pour EcranConnexion.xaml
    /// </summary>
    public partial class EcranConnexion : Window
    {

        public EcranConnexion()
        {
            InitializeComponent();
        }


        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            TrouverUtilisateur(txtUser.Text.ToString(), MD5.Hash(txtMDP.Password.ToString()));
        }


        private void txtMDP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                TrouverUtilisateur(txtUser.Text.ToString(), MD5.Hash(txtMDP.Password.ToString()));
        }


        private void btnObservateur_Click(object sender, RoutedEventArgs e)
        {
            EcranControleur C = new EcranControleur();
            this.Close();
            C.ShowDialog();
        }


        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            // Demande une confirmation à l'utilisateur avant de quitter.
            MessageBoxResult resultat = MessageBox.Show("Voulez-vous vraiment quitter l'application ?", "Quitter", MessageBoxButton.YesNo, MessageBoxImage.Stop);

            if (resultat == MessageBoxResult.Yes)
                this.Close();
        }


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


        private void AfficherEcranControleur(Utilisateur U)
        {
            EcranControleur C = new EcranControleur(U);
            this.Close();
            C.ShowDialog();
        }


        private void AfficherEcranAdministrateur()
        {
            EcranAdministrateur A = new EcranAdministrateur(null, false);
            this.Close();
            A.ShowDialog();
        }


    }
}
