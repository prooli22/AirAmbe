//Nom: Vincent Désilets
//Date: 2016-12-09
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
using System.Collections.ObjectModel;

namespace AirAmbe
{
    /// <summary>
    /// Cette écran permet d'ajouter, modifier ou voir les infos d'un utilisateur.
    /// </summary>
    public partial class EcranUtilisateur : Window
    {
        public string Retour { get; set; }
        public Utilisateur UsagerAfficher { get; set; }
        public ObservableCollection<Utilisateur> lstUser { get; set; }
        /// <summary>
        /// Si le constructeur est vide, c,est en mode ajout.
        /// </summary>
        public EcranUtilisateur(ObservableCollection<Utilisateur> lst)
        {
            InitializeComponent();

            lstUser = lst;


            InitialiseCbo();
            SetTxtWritable(true);
            Retour = "EcranAdministrateur";
            btnAction.Content = "Ajouter";
            lblNouvMdp.Content = "Mot de passe: ";
            txtCour.Text = "L'adresse courriel est créée toute seule.";
        }
        /// <summary>
        /// Ce constructeur et sois en mode modification ou lecture seule.
        /// </summary>
        /// <param name="user">L'utilisateur à afficher</param>
        /// <param name="peutModifier">True=Modification, False=lecture Seule</param>
        public EcranUtilisateur(Utilisateur user, bool peutModifier, ObservableCollection<Utilisateur> lst)
        {
            InitializeComponent();

            lstUser = lst;
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

        /// <summary>
        /// Met les valeurs des types d'utilisateur dans la combobox de type.
        /// </summary>
        private void InitialiseCbo()
        {
            cboType.Items.Add(Type.Administrateur.ToString());
            cboType.Items.Add(Type.Contrôleur.ToString());
        }

        /// <summary>
        /// Met les valeurs de l'utilisateur dans les champs texte.
        /// </summary>
        /// <param name="user"></param>
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

        /// <summary>
        /// Met les champs en mode écriture.
        /// </summary>
        /// <param name="ajout">True=en mode ajouter</param>
        private void SetTxtWritable(bool ajout)
        {
            txtAdresse.IsReadOnly = false;
            txtDate.IsReadOnly = false;
            txtNum.IsReadOnly = false;
            txtPoste.IsReadOnly = false;
            if (ajout)
            {
                txtNom.IsReadOnly = false;
                txtPrenom.IsReadOnly = false;
                cboType.IsEnabled = true;
            }
        }

        /// <summary>
        /// Quitte la page et retourne à la bonne page source.
        /// </summary>
        private void QuitterPage(Utilisateur u, bool estAjout)
        {
            Window win;
            switch (Retour)
            {
                case "EcranAdministrateur":
                    win = new EcranAdministrateur(u,estAjout);
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
            QuitterPage(null,false);
        }

        /// <summary>
        /// Modifie l'utilisateur et envoie les informations en BD
        /// </summary>
        /// <param name="user">L'utilisateur à modifier</param>
        private Utilisateur Modifier(Utilisateur user)
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
            return user;
        }

        /// <summary>
        /// Vérifie si le nom de l'utilisateur est déjà
        /// </summary>
        /// <param name="nomUser"></param>
        /// <returns></returns>
        private string VerifierNomUtilisateur(string nomUser)
        {
            int compteur = 0;
            string newNomUser = nomUser;

            for (int i = 0; i < lstUser.Count; i++)
            {
                if (lstUser[i].NomUtilisateur.Contains(newNomUser))
                {
                    compteur++;
                }
            }

            if (compteur != 0)
            {
                newNomUser = newNomUser + compteur;
            }

            return newNomUser;
        }

        /// <summary>
        /// Ajoute un utilisateur en BD
        /// </summary>
        private Utilisateur Ajouter()
        {
            Utilisateur user = new Utilisateur();

            user.Adresse = txtAdresse.Text;
            user.DateEmbauche = Convert.ToDateTime(txtDate.Text);
            if (txtNouvMdp.Password == txtConfMdp.Password)
            {
                user.MotPasse = MD5.Hash(txtNouvMdp.Password);
            }
            user.Nom = txtNom.Text;
            user.NomUtilisateur = VerifierNomUtilisateur((txtPrenom.Text.Substring(0, 1) + txtNom.Text).ToLower());
            user.Courriel = user.NomUtilisateur + "@airambe.com";
            user.Poste = txtPoste.Text;
            user.Prenom = txtPrenom.Text;
            user.Telephone = txtNum.Text;
            user.TypeUtilisateur = cboType.SelectedValue.ToString();

            UtilisateurAS userAs = new UtilisateurAS();
            userAs.Inserer(user);
            return user;
        }

        /// <summary>
        /// Ce bouton fait l'action demandé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultat = MessageBox.Show("Voulez-vous vraiment " + btnAction.Content.ToString().ToLower() + " l'utilisateur ?", "Quitter", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (resultat == MessageBoxResult.Yes)
            {
                if (btnAction.Content.ToString() == "Modifier")
                {
                    Utilisateur u = Modifier(UsagerAfficher);
                    QuitterPage(u, false);                
                }
                else if (btnAction.Content.ToString() == "Ajouter")
                {
                    Utilisateur u =  Ajouter();
                    QuitterPage(u, true);
                }
            }
        }
    }
}
