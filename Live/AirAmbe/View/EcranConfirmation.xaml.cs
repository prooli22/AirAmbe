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
    /// Interaction logic for EcranConfirmation.xaml
    /// </summary>
    public partial class EcranConfirmation : Window
    {
        public Utilisateur Controleur { get; set; }


        public EcranConfirmation(Utilisateur U)
        {
            InitializeComponent();

            Controleur = U;
        }

        protected override void OnClosed(EventArgs e)
        {
            this.DialogResult = false;
            base.OnClosed(e);
            //this.Close();
        }


        private void btnConfirmation_Click(object sender, RoutedEventArgs e)
        {
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

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnConfirmation_Click(sender, e);
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
