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
        public int IdScenarioModif { get; set; }

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

            lstVolScen = ChargerListeVols(s);
            txtDesc.Text = s.Description;

            btnAjouterScenario.Click += BtnAjouterScenario_ClickModifier;
            btnAjouterScenario.Click -= btnAjouterScenario_ClickAjouter;

            IdScenarioModif = s.IdScenario;

            dgVols.ItemsSource = lstVols;
            dgVolsScen.ItemsSource = lstVolScen;
        }

        private void BtnAjouterScenario_ClickModifier(object sender, RoutedEventArgs e)
        {
            if (lstVolScen.Count <= 0)
            {
                MessageBox.Show("Il doit avoir au moins un vol dans le scénario.");
            }
            else
            {
                Scenario s = CreerScenario();
                s.IdScenario = IdScenarioModif;
                ScenarioAS sAS = new ScenarioAS();

                sAS.Modifier(s);

                MessageBox.Show("Scénario #" + s.IdScenario + " modifié.");
                EcranScenario ES = new EcranScenario();
                this.Close();
                ES.Show();
            }
        }

        private List<Vol> ChargerListeVols(Scenario s)
        {
            List<Vol> lstVolTemp = new List<Vol>();
            List<String> lstTemp = s.lstVols;

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
                scen.lstVols.Add(lstVolScen[i].NumeroVol);
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

        //Ne fonctionne pas
        private bool VerifNbVols()
        {
            int compteur = 0;
            bool estAtterrissage = true;

            for (int i = 0; i < lstVolScen.Count; i++)
            {
                if (estAtterrissage = lstVolScen[i].EstAtterrissage)
                {
                    compteur++;
                    if (compteur >= 12)
                    {
                        return true;
                    }
                }
                else
                {
                    compteur = 0;
                    compteur++;
                }
            }
            return false;
        }

        private void btnAjouterScenario_ClickAjouter(object sender, RoutedEventArgs e)
        {
            if (lstVolScen.Count <= 0)
            {
                MessageBox.Show("Il doit avoir au moins un vol dans le scénario.");
            }
            else
            {
                if (VerifNbVols())
                {
                    MessageBox.Show("Il doit y avoir moins de 12 vols du même type (Atterrissage ou Décollage) de suite.");
                }
                else
                {
                    Scenario s = CreerScenario();
                    ScenarioAS sAS = new ScenarioAS();

                    sAS.Inserer(s);

                    MessageBox.Show("Scénario créé.");

                    EcranScenario ES = new EcranScenario();
                    this.Close();
                    ES.Show();
                }
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
