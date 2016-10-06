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
using System.Windows.Shapes;
using AirAmbe.Model;
using AirAmbe.ViewModel;

namespace AirAmbe
{
    /// <summary>
    /// Logique d'interaction pour EcranUtilisateur.xaml
    /// </summary>
    public partial class EcranUtilisateur : Window
    {
        public string Retour { get; set; }
        public Utilisateur UsagerActuel { get; set; }
        public EcranUtilisateur()
        {
            InitializeComponent();

            Retour = "EcranAdministrateur";
            btnAction.Content = "Ajouter";
            lblNouvMdp.Content = "Mot de passe: ";
        }
        public EcranUtilisateur(Utilisateur user, bool peutModifier)
        {
            InitializeComponent();

            UsagerActuel = user;
            lblTitre.Content = lblTitre.Content + ": " + user.NomUtilisateur;
            btnAction.Content = "Modifier";
            if (peutModifier)
            {
                SetTxtWritable();
                InitialiseValeur(user);
                Retour = "EcranAdministrateur";
            }
            else
            {
                Retour = "EcranControleur";
            }
        }

        private void InitialiseValeur(Utilisateur user)
        {
            txtAdresse.Text = user.Adresse;
            txtCour.Text = user.Courriel;
            txtDate.Text = user.DateEmbauche.ToString("d");
            txtNom.Text = user.Nom;
            txtPrenom.Text = user.Prenom;
            txtType.Text = user.TypeUtilisateur;
            txtPoste.Text = user.Poste;
            txtNum.Text = user.Telephone;
        }

        private void SetTxtWritable()
        {
            txtAdresse.IsReadOnly = false;
            txtCour.IsReadOnly = false;
            txtDate.IsReadOnly = false;
            txtNom.IsReadOnly = false;
            txtNum.IsReadOnly = false;
            txtPoste.IsReadOnly = false;
            txtPrenom.IsReadOnly = false;
            txtType.IsReadOnly = false;
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            Window win;
            switch (Retour)
            {
                case "EcranAdministrateur":
                    win = new EcranAdministrateur();
                    win.Show();
                    break;
                case "EcranControleur":
                    win = new EcranControleur(UsagerActuel);
                    win.Show();
                    break;
            }
            this.Close();
        }
    }
}
