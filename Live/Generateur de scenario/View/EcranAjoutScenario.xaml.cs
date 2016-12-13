//Nom: Vincent Désilets
//Date: 2016-12-12
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
        public int numeroChk { get; set; } = -1;

        /// <summary>
        /// Constructeur de base de la fenetre d'ajout
        /// </summary>
        public EcranAjoutScenario()
        {
            InitializeComponent();

            InitialiserListeVols();

            dgVols.ItemsSource = lstVols;
            dgVolsScen.ItemsSource = lstVolScen;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="numChk">Le numéro du checkbox à cocher</param>
        public EcranAjoutScenario(int numChk)
        {
            InitializeComponent();

            InitialiserListeVols();

            dgVols.ItemsSource = lstVols;
            dgVolsScen.ItemsSource = lstVolScen;

            numeroChk = numChk;
        }

        /// <summary>
        /// Constructeur en modification
        /// </summary>
        /// <param name="s">Le scénario à modifier</param>
        /// <param name="numChk">Le numéro du checkbox à cocher</param>
        public EcranAjoutScenario(Scenario s, int numChk)
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

            numeroChk = numChk;
        }

        /// <summary>
        /// Quand on clique sur le bouton modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouterScenario_ClickModifier(object sender, RoutedEventArgs e)
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
                    s.IdScenario = IdScenarioModif;
                    ScenarioAS sAS = new ScenarioAS();

                    sAS.Modifier(s);

                    MessageBox.Show("Scénario #" + s.IdScenario + " modifié.");
                    EcranScenario ES = new EcranScenario(numeroChk);
                    this.Close();
                    ES.Show();
                }                
            }
        }

        /// <summary>
        /// On charge la liste des vols
        /// </summary>
        /// <param name="s">Le scénario contenant les vols</param>
        /// <returns>Liste de vols</returns>
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

        /// <summary>
        /// Va chercher tous les vols en bd
        /// </summary>
        private void InitialiserListeVols()
        {
            VolAS vAs = new VolAS();
            
            lstVols =  vAs.RecupererTous();
        }

        /// <summary>
        /// Fonction non-utilisé
        /// </summary>
        /// <param name="cbo"></param>
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

        /// <summary>
        /// On crée le scénario avec les données entrées par l'utilisateur
        /// </summary>
        /// <returns>Un scénario</returns>
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

        /// <summary>
        /// Ajoute un vol dans la liste du scénario
        /// </summary>
        /// <param name="v"></param>
        private void AjouterVol(Vol v)
        {
            lstVolScen.Add(v);

            dgVolsScen.Items.Refresh();
        }

        /// <summary>
        /// Enleve un vol de la liste du scénario
        /// </summary>
        /// <param name="v"></param>
        private void EnleverVol(Vol v)
        {
            lstVolScen.Remove(v);

            dgVolsScen.Items.Refresh();
        }

        /// <summary>
        /// Vérifie si le nombre de vols est correct
        /// </summary>
        /// <returns>Vrai ou faux</returns>
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

        /// <summary>
        /// Quand on clique sur le bouton ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                    EcranScenario ES = new EcranScenario(numeroChk);
                    this.Close();
                    ES.Show();
                }
            }                      
        }

        /// <summary>
        /// Quand on clique sur le bouton annuler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {            
            if (numeroChk >= 0)
            {
                EcranScenario es = new EcranScenario(numeroChk);
                this.Close();
                es.Show();
            }
            else
            {
                EcranScenario eS = new EcranScenario();
                this.Close();
                eS.Show();
            }
            
        }

        /// <summary>
        /// Quand on clique sur le bouton enlever
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnlever_Click(object sender, RoutedEventArgs e)
        {
            EnleverVol((Vol)dgVolsScen.SelectedItem);
        }

        /// <summary>
        /// Quand on clique sur le bouton ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterVol((Vol)(dgVols.SelectedItem));
        }


        private void dgVolsScen_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        /// <summary>
        /// Quand la souris entre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Height += 2;
            btn.Width += 2;
        }

        /// <summary>
        /// Quand la souris sort
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Height -= 2;
            btn.Width -= 2;
        }

        private void btnAide_Click(object sender, RoutedEventArgs e)
        {
            EcranGuide eg;
            eg = new EcranGuide(7);
            eg.Show();
        }
    }
}
