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
using AirAmbe.Model;
using AirAmbe.ViewModel;
using System.Collections.ObjectModel;

namespace AirAmbe
{
    /// <summary>
    /// L'écran de l'administrateur. D'ici, il peut gérer les utilisateurs
    /// </summary>
    public partial class EcranAdministrateur : Window
    {
        public ObservableCollection<Utilisateur> lstUser { get; set; }
        /// <summary>
        /// Le constructeur met les utilisateurs de la BD dans une datagrid.
        /// </summary>
        public EcranAdministrateur(Utilisateur u,bool estAjouter)
        {
            InitializeComponent();

            AfficherDerniereAction(u, estAjouter);

            DataContext = new AirAmbe.ViewModel.UtilisateurViewModel();            

            lstUser = ((UtilisateurViewModel)DataContext).SommaireUtilisateurs;
            dgUtilisateur.ItemsSource = lstUser;
        }

        private void AfficherDerniereAction(Utilisateur u, bool estAjout)
        {
            if (u != null)
            {
                if (estAjout)
                {
                    MessageBox.Show("L'utilisateur " + u.NomUtilisateur + " a été ajouté.");
                }
                else
                {
                    MessageBox.Show("L'utilisateur " + u.NomUtilisateur + " a été modifié.");
                }
            }
        }

        /// <summary>
        /// Le bouton modifier nous envois vers la fenêtre Utilisateur en mode modifier avec le bon utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            EcranUtilisateur eUser = new EcranUtilisateur((Utilisateur)(dgUtilisateur.SelectedItem),true,lstUser);
            this.Close();
            eUser.Show();
        }

        /// <summary>
        /// Le bouton Deconnexion ferme la fenêtre et ouvre la fenêtre de connexion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            EcranConnexion eCon = new EcranConnexion();
            this.Close();
            eCon.Show();
        }

        /// <summary>
        /// Le bouton Ajouter, nous envoie vers la fenêtre Utilisateur en mode ajout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            EcranUtilisateur eUser = new EcranUtilisateur(lstUser);
            this.Close();
            eUser.Show();
        }
    }
}
