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
    /// Logique d'interaction pour EcranAjoutScenario.xaml
    /// </summary>
    public partial class EcranAjoutScenario : Window
    {
        public int nbVol { get; set; }
        public List<Vol> lstVols { get; set; } = new List<Vol>();

        public EcranAjoutScenario()
        {
            InitializeComponent();

            InitialiserListeVols();
            nbVol = 0;
            AjouterVol();
        }

        private void InitialiserListeVols()
        {
            Vol v = new Vol();
            v.Aeroport = "Mirabel";
            v.EstAtterrissage = true;
            v.ModeleAvion = "Airbus";
            v.NumeroVol = "CAN45";
            lstVols.Add(v);
            Vol v1 = new Vol();
            v1.Aeroport = "Dorval";
            v1.EstAtterrissage = false;
            v1.ModeleAvion = "Boeing";
            v1.NumeroVol = "CANCAN";
            lstVols.Add(v1);
            Vol v2 = new Vol();
            v2.Aeroport = "AUSTRALIA";
            v2.EstAtterrissage = false;
            v2.ModeleAvion = "Airbus";
            v2.NumeroVol = "GGGG";
            lstVols.Add(v2);
            Vol v3 = new Vol();
            v3.Aeroport = "Mirabel";
            v3.EstAtterrissage = false;
            v3.ModeleAvion = "Airbus";
            v3.NumeroVol = "CAN5";
            lstVols.Add(v3);
        }

        private void AjouterVol()
        {
            RowDefinition gridRow = new RowDefinition();
            gridVols.RowDefinitions.Add(gridRow);

            Label lblVol = new Label();

            //REGISTER NAME IMPORTANT
            lblVol.Name = "nomTest";
            gridVols.RegisterName(lblVol.Name, lblVol);
            Label lblTest = (Label)gridVols.FindName("nomTest");

            lblVol.Content = "Vol" + nbVol;
            lblVol.Width = 50;
            lblVol.Height = 25;
            lblVol.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(lblVol, nbVol);
            Grid.SetColumn(lblVol, 0);

            ComboBox cboVols = new ComboBox();
            for (int i = 0; i < lstVols.Count; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = lstVols[i].NumeroVol;
                cboVols.Items.Add(cbi);
                cboVols.SelectedItem = cbi;
            }
            cboVols.Height = 25;
            cboVols.Width = 75;
            cboVols.Margin = new Thickness(10, 0, 0, 0);
            Grid.SetRow(cboVols, nbVol);
            Grid.SetColumn(cboVols, 0);

            /*Label lblAttDec = new Label();
            int compteur = 0;
            Vol vCourant = new Vol();
            vCourant = lstVols[compteur];
            while (lstVols[compteur].NumeroVol != ((ComboBoxItem)cboVols.SelectedItem).Content.ToString())
            {
                if (lstVols[compteur].NumeroVol == ((ComboBoxItem)cboVols.SelectedItem).Content.ToString())
                {
                    vCourant = lstVols[compteur];
                }
                else
                {
                    compteur++;
                    vCourant = lstVols[compteur];
                }
            }
            if (vCourant.EstAtterrissage)
            {
                lblAttDec.Content = "Atterrissage";
            }
            else
            {
                lblAttDec.Content = "Décollage";
            }
            lblAttDec.Width = 75;
            lblAttDec.Height = 25;
            lblAttDec.Margin = new Thickness(75, 0, 0, 0);
            lblAttDec.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(lblAttDec, nbVol);
            Grid.SetColumn(lblAttDec, 0);*/

            cboVols.SelectionChanged += new SelectionChangedEventHandler(CboSelectionChange);

            gridVols.Children.Add(lblVol);
            //gridVols.Children.Add(lblAttDec);
            gridVols.Children.Add(cboVols);

            




            nbVol++;
        }

        private void CboSelectionChange(object sender, RoutedEventArgs e)
        {

        }

        private void btnAjouterVol_Click(object sender, RoutedEventArgs e)
        {
            AjouterVol();
        }
    }
}
