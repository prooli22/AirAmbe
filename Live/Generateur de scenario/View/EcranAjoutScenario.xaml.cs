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
        public ObservableCollection<Vol> lstVols { get; set; } = new ObservableCollection<Vol>();
        public List<Vol> lstVolScen { get; set; } = new List<Vol>();

        public EcranAjoutScenario()
        {
            InitializeComponent();

            InitialiserListeVols();

            dgVols.ItemsSource = lstVols;
            dgVolsScen.ItemsSource = lstVolScen;
        }

        public EcranAjoutScenario(Scenario s)
        {
            InitializeComponent();

            InitialiserListeVols();

            lstVolScen = FusionnerListesVols(s);
            txtDesc.Text = s.Description;

            btnAjouterScenario.Content = "Modifier";
            btnAjouterScenario.Click += BtnAjouterScenario_ClickModifier;
            btnAjouterScenario.Click -= btnAjouterScenario_ClickAjouter;

            dgVols.ItemsSource = lstVols;
            dgVolsScen.ItemsSource = lstVolScen;
        }

        private void BtnAjouterScenario_ClickModifier(object sender, RoutedEventArgs e)
        {
            
        }

        private List<Vol> FusionnerListesVols(Scenario s)
        {
            List<Vol> lstVolTemp = new List<Vol>();
            s.lstVolsAtt.AddRange(s.lstVolsDec);
            List<String> lstTemp = s.lstVolsAtt;

            for (int i = 0; i < lstTemp.Count; i++)
            {
                for (int j = 0; j < lstVols.Count; j++)
                {
                    if (lstTemp[i] == lstVols[j].NumeroVol)
                    {
                        lstVolTemp.Add(lstVols[j]);
                    }
                }                
            }

            return lstVolTemp;
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

        private void AjouterVol(Vol v)
        {
            lstVolScen.Add(v);

            dgVolsScen.Items.Refresh();
        }

        private void EnleverVol(Vol v)
        {
            lstVolScen.Remove(v);

            dgVolsScen.Items.Refresh();
        }

        private void btnAjouterScenario_ClickAjouter(object sender, RoutedEventArgs e)
        {
            if (lstVolScen.Count <= 0)
            {
                MessageBox.Show("Il doit avoir au moins un vol dans le scénario.");
            }
            else
            {
                Scenario s = CreerScenario();
                ScenarioAS sAS = new ScenarioAS();

                sAS.Inserer(s);

                EcranScenario ES = new EcranScenario();
                this.Close();
                ES.Show();
            }            
        }        

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            EcranScenario eS = new EcranScenario();
            this.Close();
            eS.Show();
        }

        private void btnEnlever_Click(object sender, RoutedEventArgs e)
        {
            EnleverVol((Vol)dgVolsScen.SelectedItem);
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterVol((Vol)(dgVols.SelectedItem));
        }
    }
}
