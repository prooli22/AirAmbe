using AirAmbe.Model;
using AirAmbe.ViewModel;
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
using System.Windows.Shapes;

namespace AirAmbe
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class EcranControleur : Window
    {
        public Utilisateur Controleur { get; set; }

        public List<Vol> lstVols { get; set; }

        public List<Vol> lstDecollages { get; set; }

        public List<Vol> lstAtterrissages { get; set; }


        /// <summary>
        /// Constructeur de la fênetre du contrôleur.
        /// </summary>
        /// <param name="U"> Un contrôleur </param>
        public EcranControleur(Utilisateur U)
        {
            InitializeComponent();
            ChargerDataGrid();
            Controleur = U;
        }


        /// <summary>
        /// Constructeur de la fenêtre de l'observateur.
        /// </summary>
        public EcranControleur()
        {
            InitializeComponent();
            ChargerDataGrid();

            btnProfil.Visibility = Visibility.Hidden;
        }


        private void ChargerDataGrid()
        {
            lstVols = new List<Vol>();
            lstDecollages = new List<Vol>();
            lstAtterrissages = new List<Vol>();

            Vol v1 = new Vol();
            v1.IdVol = 1;
            v1.Aeroport = "Mirabel";
            v1.ModeleAvion = "Airbus 203";
            v1.NumeroVol = "CAN69";
            v1.EstAtterissage = false;

            Vol v2 = new Vol();
            v2.IdVol = 2;
            v2.Aeroport = "Montréal";
            v2.ModeleAvion = "Boeing 69";
            v2.NumeroVol = "DICK70";
            v2.EstAtterissage = true;

            for (int i = 0; i < 5; i++)
            {
                lstVols.Add(v1);
                lstVols.Add(v2);
            }

            for (int i = 0; i < lstVols.Count; i++)
            {
                if (lstVols[i].EstAtterissage)
                    lstAtterrissages.Add(lstVols[i]);

                else
                    lstDecollages.Add(lstVols[i]);

            }

            dgAtterissages.ItemsSource = lstAtterrissages;
            dgDecollages.ItemsSource = lstDecollages;
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
