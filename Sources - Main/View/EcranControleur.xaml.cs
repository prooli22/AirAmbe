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
        public Utilisateur Controleur { get; set; }


        public EcranControleur(Utilisateur U)
        {
            InitializeComponent();
            ChargerDataGrid();
            Controleur = U;
        }


        public EcranControleur()
        {
            InitializeComponent();
            ChargerDataGrid();

            btnProfil.Visibility = Visibility.Hidden;
        }


        private void ChargerDataGrid()
        {
            
        }


        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            EcranConnexion eCon = new EcranConnexion();
            this.Close();
            eCon.Show();
        }


        private void btnProfil_Click(object sender, RoutedEventArgs e)
        {
            EcranUtilisateur eUser = new EcranUtilisateur(Controleur, false);
            this.Close();
            eUser.Show();
        }
    }
}
