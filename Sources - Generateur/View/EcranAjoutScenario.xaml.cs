using AirAmbe.Model;
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
    /// Logique d'interaction pour EcranAjoutScenario.xaml
    /// </summary>
    public partial class EcranAjoutScenario : Window
    {
        public int nbVol { get; set; }
        public ObservableCollection<Vol> lstVols { get; set; } = new ObservableCollection<Vol>();
        public ObservableCollection<Vol> lstVolScen { get; set; } = new ObservableCollection<Vol>();

        public EcranAjoutScenario()
        {
            InitializeComponent();

            InitialiserListeVols();
            nbVol = 0;
            AjouterVol();
        }

        private void InitialiserListeVols()
        {
            VolAS vAs = new VolAS();
            
            lstVols =  vAs.RecupererTous();
        }

        private void RemplirCombobox(ComboBox cbo)
        {
            for (int i = 0; i < lstVols.Count; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = lstVols[i].NumeroVol;
                cbo.Items.Add(cbi);
                cbo.SelectedItem = cbi;
            }
        }

        private void AjouterVol()
        {
            RowDefinition gridRow = new RowDefinition();
            gridVols.RowDefinitions.Add(gridRow);

            Label lblVol = new Label();
            lblVol.Name = "lblVol"+nbVol;
            gridVols.RegisterName(lblVol.Name, lblVol);
            lblVol.Content = "Vol" + nbVol;
            lblVol.Width = 50;
            lblVol.Height = 30;
            lblVol.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(lblVol, nbVol);
            Grid.SetColumn(lblVol, 0);

            ComboBox cboVols = new ComboBox();
            cboVols.Name = "cboVols" + nbVol;
            gridVols.RegisterName(cboVols.Name, cboVols);
            RemplirCombobox(cboVols);
            cboVols.Height = 25;
            cboVols.Width = 75;
            cboVols.Margin = new Thickness(10, 0, 0, 0);
            Grid.SetRow(cboVols, nbVol);
            Grid.SetColumn(cboVols, 0);                        

            cboVols.SelectionChanged += new SelectionChangedEventHandler(CboSelectionChange);

            Label lblType = new Label();
            lblType.Name = "lblType" + nbVol;
            gridVols.RegisterName(lblType.Name, lblType);

            Vol v = ChercherVol(nbVol);
            if (v.EstAtterrissage)
            {
                lblType.Content = "Atterrissage";
            }
            else
            {
                lblType.Content = "Décollage";
            }

            lblType.Width = 75;
            lblType.Height = 30;
            lblType.Margin = new Thickness(60, 0, 0, 0);
            lblType.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(lblType, nbVol);
            Grid.SetColumn(lblType, 0);

            gridVols.Children.Add(lblType);
            gridVols.Children.Add(lblVol);
            gridVols.Children.Add(cboVols);

            nbVol++;
        }

        private void CboSelectionChange(object sender, RoutedEventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            ComboBoxItem cbi = cbo.SelectedItem as ComboBoxItem;
            int nb = -1;
            nb = Int32.Parse(cbo.Name.ToString().Substring(7));

            Vol v = ChercherVol(nb);
            Label lblType = (Label)gridVols.FindName("lblType" + nb);

            if (v.EstAtterrissage)
            {
                lblType.Content = "Atterrissage";
            }
            else
            {
                lblType.Content = "Décollage";
            }
        }

        private void btnAjouterVol_Click(object sender, RoutedEventArgs e)
        {
            AjouterVol();
        }

        private void btnAjouterScenario_Click(object sender, RoutedEventArgs e)
        {
            ChargerListVols();
            Scenario s =  CreerScenario();
            ScenarioAS sAS = new ScenarioAS();

            sAS.Inserer(s);

            EcranScenario ES = new EcranScenario();
            this.Close();
            ES.Show();
        }

        private Vol ChercherVol(int nb)
        {
            ComboBox cbo = (ComboBox)gridVols.FindName("cboVols" + nb);
            ComboBoxItem cbi = cbo.SelectedItem as ComboBoxItem;
            string num = cbi.Content as string;

            int compt = 0;
            
            Vol v = lstVols[compt];
            while (num != lstVols[compt].NumeroVol)
            {
                compt++;
                v = lstVols[compt];
            }

            return v;
        }

        private void ChargerListVols()
        {
            for (int i = 0; i < nbVol; i++)
            {
                ComboBox cbo = (ComboBox)gridVols.FindName("cboVols" + i);

                int compt = 0;
                string num = cbo.Text;
                Vol v = lstVols[compt];
                while (num != lstVols[compt].NumeroVol)
                {
                    compt++;
                }
                v = lstVols[compt];
                lstVolScen.Add(v);                
            }
        }

        private Scenario CreerScenario()
        {
            Scenario scen = new Scenario();

            scen.Description = txtDesc.Text;

            for (int i = 0; i < lstVolScen.Count; i++)
            {
                if (lstVolScen[i].EstAtterrissage == true)
                {
                    scen.lstVolsAtt.Add(lstVolScen[i].NumeroVol);
                }
                else
                {
                    scen.lstVolsDec.Add(lstVolScen[i].NumeroVol);
                }
            }

            return scen;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            EcranScenario eS = new EcranScenario();
            this.Close();
            eS.Show();
        }
    }
}
