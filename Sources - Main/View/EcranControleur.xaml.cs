using AirAmbe.Model;
using AirAmbe.ViewModel;
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
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class EcranControleur : Window
    {
        public EcranControleur(Utilisateur U)
        {
            InitializeComponent();

            Label lblUser = new Label();
            lblUser.Content = U.NomUtilisateur;
            lblUser.Width = 300;
            lblUser.Height = 30;
            lblUser.VerticalAlignment = VerticalAlignment.Top;
            grdPrincipale.Children.Add(lblUser);


            Label lblMDP = new Label();
            lblMDP.Content = U.MotPasse;
            lblMDP.Width = 300;
            lblMDP.Height = 30;
            grdPrincipale.Children.Add(lblMDP);
        }
    }
}
