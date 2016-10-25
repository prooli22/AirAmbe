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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirAmbe
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class EcranScenario : Window
    {
        public ObservableCollection<Scenario> lstScenarios { get; set; }
        public List<Scenario> lstScenarioAjouter { get; set; }
        public int NbChk { get; set; } = 0;

        public EcranScenario()
        {
            InitializeComponent();

            ChercherScenarios();

            AfficherScenarios(lstScenarios.Count);
        }

        private void ChercherScenarios()
        {
            ScenarioAS sAS = new ScenarioAS();

            lstScenarios = sAS.RecupererTous();
        }

        /// <summary>
        /// Genere des Grid.Row et ajoute les scénarios dans la gridScen
        /// </summary>
        /// <param name="nbScenarios">Le nombre de scénario</param>
        private void AfficherScenarios(int nbScenarios)
        {
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(0);
            gridScen.RowDefinitions.Add(gridRow1);
            for (int i = 0; i < nbScenarios; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(100);
                gridScen.RowDefinitions.Add(gridRow);

                AfficherScenarioDetails(lstScenarios[i].IdScenario,lstScenarios[i].Description, lstScenarios[i].lstVolsAtt.Count, lstScenarios[i].lstVolsDec.Count, i);
            }
        }

        /// <summary>
        /// Affiche les détails du scénario
        /// </summary>
        /// <param name="numScenario">le numéro du scénario</param>
        /// <param name="desc">La decription du scénario</param>
        /// <param name="volsEntrants">le nombre de vols entrants</param>
        /// <param name="volsSortants">Le nombre de vols sortants</param>
        /// <param name="compteurScenario">Le compteur de scénario, permet de savoir où placer ce scénario</param>
        private void AfficherScenarioDetails(int numScenario, string desc, int volsEntrants, int volsSortants, int compteurScenario)
        {
            //http://www.c-sharpcorner.com/resources/676/how-to-create-a-grid-in-wpf-dynamically.aspx
            Grid gridScenario = new Grid();
            gridScenario.Height = 100;
            gridScenario.Background = new SolidColorBrush(Colors.LightSteelBlue);
            //Création des collonnes
            ColumnDefinition gridCol1 = new ColumnDefinition();
            ColumnDefinition gridCol2 = new ColumnDefinition();
            ColumnDefinition gridCol3 = new ColumnDefinition();
            ColumnDefinition gridCol4 = new ColumnDefinition();
            gridCol1.Width = new GridLength(15);
            gridCol2.Width = new GridLength(175);
            gridCol3.Width = new GridLength(200);
            gridScenario.ColumnDefinitions.Add(gridCol1);
            gridScenario.ColumnDefinitions.Add(gridCol2);
            gridScenario.ColumnDefinitions.Add(gridCol3);
            gridScenario.ColumnDefinitions.Add(gridCol4);
            //Création des rangées
            RowDefinition gridRow1 = new RowDefinition();
            RowDefinition gridRow2 = new RowDefinition();
            gridScenario.RowDefinitions.Add(gridRow1);
            gridScenario.RowDefinitions.Add(gridRow2);

            //affichage des infos du scénario
            CheckBox chkBoxScenario = new CheckBox();
            chkBoxScenario.VerticalAlignment = VerticalAlignment.Center;
            chkBoxScenario.Checked += ChkBoxScenario_Checked;
            chkBoxScenario.Unchecked += ChkBoxScenario_Unchecked;
            chkBoxScenario.Name = "chkBox" + compteurScenario;
            gridScen.RegisterName(chkBoxScenario.Name, chkBoxScenario);
            Grid.SetRow(chkBoxScenario, 0);
            Grid.SetColumn(chkBoxScenario, 0);

            Label lblScenario = new Label();
            lblScenario.Content = "Scénario #" + numScenario;
            lblScenario.Width = 100;
            lblScenario.Height = 25;
            lblScenario.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(lblScenario, 0);
            Grid.SetColumn(lblScenario, 1);

            Label lblDesc = new Label();
            lblDesc.Content = desc;
            lblDesc.Width = 200;
            lblDesc.Height = 50;
            lblDesc.HorizontalAlignment = HorizontalAlignment.Left;
            lblDesc.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(lblDesc, 1);
            Grid.SetColumn(lblDesc, 1);

            Label lblEntrants = new Label();
            lblEntrants.Content = "Nb vols entrants : " + volsEntrants;
            lblEntrants.Width = 150;
            lblEntrants.Height = 25;
            lblEntrants.HorizontalAlignment = HorizontalAlignment.Left;
            lblEntrants.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(lblEntrants, 0);
            Grid.SetColumn(lblEntrants, 2);

            Label lblSortants = new Label();
            lblSortants.Content = "Nb vols sortants : " + volsSortants;
            lblSortants.Width = 150;
            lblSortants.Height = 25;
            lblSortants.HorizontalAlignment = HorizontalAlignment.Left;
            lblSortants.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(lblSortants, 1);
            Grid.SetColumn(lblSortants, 2);

            Label lblInterval = new Label();
            lblInterval.Content = "Interval de temps\nen minutes";
            lblInterval.Width = 105;
            lblInterval.Height = 50;
            lblInterval.HorizontalAlignment = HorizontalAlignment.Left;
            lblInterval.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(lblInterval, 0);
            Grid.SetColumn(lblInterval, 3);

            ComboBox cboMin = new ComboBox();
            cboMin.Items.Add(2);
            cboMin.SelectedIndex = 0;
            cboMin.Items.Add(5);
            cboMin.Items.Add(10);
            cboMin.Width = 50;
            cboMin.Height = 25;
            cboMin.HorizontalAlignment = HorizontalAlignment.Left;
            cboMin.VerticalAlignment = VerticalAlignment.Top;
            cboMin.Name = "cboMin" + compteurScenario;
            gridScen.RegisterName(cboMin.Name, cboMin);
            Grid.SetRow(cboMin, 1);
            Grid.SetColumn(cboMin, 3);

            gridScenario.Children.Add(chkBoxScenario);
            gridScenario.Children.Add(lblScenario);
            gridScenario.Children.Add(lblDesc);
            gridScenario.Children.Add(lblEntrants);
            gridScenario.Children.Add(lblSortants);
            gridScenario.Children.Add(lblInterval);
            gridScenario.Children.Add(cboMin);

            Grid.SetRow(gridScenario, compteurScenario+1);
            gridScen.Children.Add(gridScenario);
        }

        private void ChkBoxScenario_Unchecked(object sender, RoutedEventArgs e)
        {
            NbChk--;
        }

        private void ChkBoxScenario_Checked(object sender, RoutedEventArgs e)
        {
            NbChk++;
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            EcranAjoutScenario eAjoutScenario = new EcranAjoutScenario();
            this.Close();
            eAjoutScenario.Show();
        }

        private void btnGenerer_Click(object sender, RoutedEventArgs e)
        {
            lstScenarioAjouter = new List<Scenario>();
            ChargerListeScen();

            /*if (lstScenarioAjouter.Count > 0)
            {
                EcrireFichier();
            }*/
            EcrireFichier();
        }

        private void EcrireFichier()
        {
            string[] lines = ChargerTabString();

            System.IO.File.WriteAllLines(@".\Scenarios.txt", lines);

            MessageBox.Show("Le fichier Scenarios.txt est prêt pour l'application AirAmbe");
        }

        private string[] ChargerTabString()
        {
            List<string> lstString = new List<string>();
            ComboBox cbo = (ComboBox)gridPrincipal.FindName("cboPistes");            

            lstString.Add(cbo.Text);

            for (int i = 0; i < lstScenarioAjouter.Count; i++)
            {
                string ligne = "";
                ligne = ligne + lstScenarioAjouter[i].Intervalle;
                for (int j = 0; j < lstScenarioAjouter[i].lstVolsAtt.Count; j++)
                {
                    ligne = ligne + ";" + lstScenarioAjouter[i].lstVolsAtt[j];
                }
                for (int j = 0; j < lstScenarioAjouter[i].lstVolsDec.Count; j++)
                {
                    ligne = ligne + ";" + lstScenarioAjouter[i].lstVolsDec[j];
                }
                lstString.Add(ligne);
            }

            string[] lignes = new string[lstString.Count];

            for (int i = 0; i < lstString.Count; i++)
            {
                lignes[i] = lstString[i];
            }

            return lignes;
        }

        private void ChargerListeScen()
        {
            for (int i = 0; i < lstScenarios.Count; i++)
            {
                CheckBox chk = (CheckBox)gridScen.FindName("chkBox" + i);
                ComboBox cbo = (ComboBox)gridScen.FindName("cboMin" + i);


                if (chk.IsChecked == true)
                {
                    lstScenarios[i].Intervalle = Int32.Parse(cbo.Text);
                    lstScenarioAjouter.Add(lstScenarios[i]);
                }
            }
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
