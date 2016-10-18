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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirAmbe
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class EcranScenario : Window
    {
        public EcranScenario()
        {
            InitializeComponent();

            AfficherScenarios(7);
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

                AfficherScenarioDetails(i+1, "Test", 3, 5, i);
            }
        }

        //TODO: à effacer
        /// <summary>
        /// Désuet à effacer
        /// </summary>
        /// <param name="compteurRow"></param>
        private void GenererBoutons(int compteurRow)
        {
            Grid.SetRow(lblPistes, compteurRow);
            Grid.SetRow(cboPistes, compteurRow);
            Grid.SetRow(btnCreer, compteurRow);
            Grid.SetRow(btnGenerer, compteurRow);
            Grid.SetRow(btnQuitter, compteurRow);
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
            lblDesc.Width = 100;
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
            cboMin.Items.Add(5);
            cboMin.Items.Add(10);
            cboMin.Width = 50;
            cboMin.Height = 25;
            cboMin.HorizontalAlignment = HorizontalAlignment.Left;
            cboMin.VerticalAlignment = VerticalAlignment.Top;
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

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            EcranAjoutScenario eAjoutScenario = new EcranAjoutScenario();
            this.Close();
            eAjoutScenario.Show();
        }

        private void btnGenerer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
