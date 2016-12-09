// Nom : Olivier Provost
// Date : 2016-12-09


using AirAmbe.Model;
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

namespace AirAmbe
{
    /// <summary>
    /// Écran de confirmation d'une action jugée dangeureuse par l'application.
    /// </summary>
    public partial class EcranConfirmation : Window
    {
        // Variable.
        private Utilisateur Controleur;


        /// <summary>
        /// Constructeur de l'écran de Confirmation.
        /// </summary>
        /// <param name="U"> L'utilisateur qui accède à cet écran. </param>
        public EcranConfirmation(Utilisateur U)
        {
            InitializeComponent();

            Controleur = U;
        }


        /// <summary>
        /// Appelé lorsqu'on clique sur le bouton Confirmer.
        /// </summary>
        private void btnConfirmation_Click(object sender, RoutedEventArgs e)
        {
            // Si le mot de passe est correct.
            if(MD5.Hash(txtPassword.Password.ToString()) == Controleur.MotPasse)
            {
                this.DialogResult = true;
                this.Close();
            }

            else
            {
                lblMDP.Content = "Erreur : Mauvais mot de passe, veuillez recommencer";
                lblMDP.Foreground = Brushes.Red;
            }
            
        }


        /// <summary>
        /// Appelé lorsqu'on appuie sur la touche Entrée dans le champs du mot de passe. 
        /// </summary>
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnConfirmation_Click(sender, e);
        }


        /// <summary>
        /// Appelé lorsqu'on clique sur le bouton Annuler.
        /// </summary>
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
