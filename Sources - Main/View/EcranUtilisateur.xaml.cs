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
        public Utilisateur UsagerAfficher { get; set; }
        public EcranUtilisateur()
        {
            InitializeComponent();

            InitialiseCbo();
            SetTxtWritable(true);
            Retour = "EcranAdministrateur";
            btnAction.Content = "Ajouter";
            lblNouvMdp.Content = "Mot de passe: ";
            txtCour.Text = "L'adresse courriel est créée toute seule.";
        }
        public EcranUtilisateur(Utilisateur user, bool peutModifier)
        {
            InitializeComponent();

            InitialiseCbo();
            UsagerAfficher = user;
            lblTitre.Content = lblTitre.Content + " : " + user.NomUtilisateur;
            btnAction.Content = "Modifier";
            if (peutModifier)
            {
                SetTxtWritable(false);
                InitialiseValeur(user);
                Retour = "EcranAdministrateur";
            }
            else
            {
                InitialiseValeur(user);
                Retour = "EcranControleur";
            }
        }

        private void InitialiseCbo()
        {
            cboType.Items.Add(Type.Administrateur.ToString());
            cboType.Items.Add(Type.Contrôleur.ToString());
        }

        private void InitialiseValeur(Utilisateur user)
        {
            txtAdresse.Text = user.Adresse;
            txtCour.Text = user.Courriel;
            txtDate.Text = user.DateEmbauche.ToString("d");
            txtNom.Text = user.Nom;
            txtPrenom.Text = user.Prenom;
            cboType.SelectedValue = user.TypeUtilisateur;
            txtPoste.Text = user.Poste;
            txtNum.Text = user.Telephone;
        }

        private void SetTxtWritable(bool ajout)
        {
            txtAdresse.IsReadOnly = false;
            txtDate.IsReadOnly = false;
            txtNom.IsReadOnly = false;
            txtNum.IsReadOnly = false;
            txtPoste.IsReadOnly = false;
            txtPrenom.IsReadOnly = false;
            if (ajout)
            {
                cboType.IsEnabled = true;
            }
        }

        private void QuitterPage()
        {
            Window win;
            switch (Retour)
            {
                case "EcranAdministrateur":
                    win = new EcranAdministrateur();
                    win.Show();
                    break;
                case "EcranControleur":
                    win = new EcranControleur(UsagerAfficher);
                    win.Show();
                    break;
            }
            this.Close();
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            QuitterPage();
        }

        private void Modifier(Utilisateur user)
        {
            user.Adresse = txtAdresse.Text;
            user.DateEmbauche = Convert.ToDateTime(txtDate.Text);
            if (txtNouvMdp.Password != "" && txtNouvMdp.Password == txtConfMdp.Password)
            {
                user.MotPasse = MD5.Hash(txtNouvMdp.Password);
            }
            user.Nom = txtNom.Text;
            user.NomUtilisateur = (txtPrenom.Text.Substring(0, 1) + txtNom.Text).ToLower();
            user.Courriel = user.NomUtilisateur + "@airambe.com";
            
            user.Poste = txtPoste.Text;
            user.Prenom = txtPrenom.Text;
            user.Telephone = txtNum.Text;
            user.TypeUtilisateur = cboType.SelectedValue.ToString();

            UtilisateurAS userAs = new UtilisateurAS();
            userAs.Modifier(user);
        }

        private void Ajouter()
        {
            Utilisateur user = new Utilisateur();

            user.Adresse = txtAdresse.Text;
            user.DateEmbauche = Convert.ToDateTime(txtDate.Text);
            if (txtNouvMdp.Password == txtConfMdp.Password)
            {
                user.MotPasse = MD5.Hash(txtNouvMdp.Password);
            }
            user.Nom = txtNom.Text;
            user.NomUtilisateur = (txtPrenom.Text.Substring(0, 1) + txtNom.Text).ToLower();
            user.Courriel = user.NomUtilisateur + "@airambe.com";
            user.Poste = txtPoste.Text;
            user.Prenom = txtPrenom.Text;
            user.Telephone = txtNum.Text;
            user.TypeUtilisateur = cboType.SelectedValue.ToString();

            UtilisateurAS userAs = new UtilisateurAS();
            userAs.Inserer(user);
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultat = MessageBox.Show("Voulez-vous vraiment " + btnAction.Content.ToString().ToLower() + " l'utilisateur ?", "Quitter", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (resultat == MessageBoxResult.Yes)
            {
                if (btnAction.Content.ToString() == "Modifier")
                {
                    Modifier(UsagerAfficher);
                    QuitterPage();                
                }
                else if (btnAction.Content.ToString() == "Ajouter")
                {
                    Ajouter();
                    QuitterPage();
                }
            }
        }
    }
}
