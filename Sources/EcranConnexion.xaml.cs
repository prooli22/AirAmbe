using System;
using System.Collections.Generic;
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
            AfficherEcranControleur();
        }


        private void btnObservateur_Click(object sender, RoutedEventArgs e)
        {
            AfficherEcranObservateur();
        }


        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            QuitterApplication();
        }



        private void AfficherEcranControleur()
        {
            Utilisateur U = TrouverUtilisateur(txtUser.Text.ToString(), txtMDP.Password.ToString());

            if (U != null)
            {
                EcranControleur C = new EcranControleur(U);
                this.Close();
                C.ShowDialog();
            }

            else
            {
                lblErreur.Visibility = Visibility.Visible;
                lblUser.Foreground = Brushes.Red;
                lblMDP.Foreground = Brushes.Red;
            }
        }


        private Utilisateur TrouverUtilisateur(string nomUtilisateur, string motPasse)
        {
            Utilisateur U = new Utilisateur();

            if (nomUtilisateur == "oprovost")
            {
                U.NomUtilisateur = nomUtilisateur;
                U.MotPasse = motPasse;
            }

            else
            {
                U = null;
            }

            return U;
        }


        private void AfficherEcranObservateur()
        {
            EcranControleur C = new EcranControleur(new Utilisateur());
            this.Close();
            C.ShowDialog();
        }


        private void QuitterApplication()
        {
            // Demande une confirmation à l'utilisateur avant de quitter.
            MessageBoxResult resultat = MessageBox.Show("Voulez-vous vraiment quitter l'application ?", "Quitter", MessageBoxButton.YesNo, MessageBoxImage.Stop);

            if (resultat == MessageBoxResult.Yes)
                this.Close();
        }
    }
}
