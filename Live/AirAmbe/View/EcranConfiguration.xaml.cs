//Nom: Vincent Désilets
//Date: 2016-12-09
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;

namespace AirAmbe
{
    /// <summary>
    /// Logique d'interaction pour EcranConfiguration.xaml
    /// </summary>
    public partial class EcranConfiguration : Window
    {
        private EcranControleur EC;
        private DispatcherTimer dt = new DispatcherTimer();

        /// <summary>
        /// L'écran de configuration permet de configurer les vols
        /// </summary>
        /// <param name="ec">L'écran controleur</param>
        public EcranConfiguration(EcranControleur ec)
        {
            InitializeComponent();
            
            EC = ec;

            dgVols.ItemsSource = EC.LstVols;
            dgPistes.ItemsSource = EC.LstPistes;

            dt.Interval = TimeSpan.FromMilliseconds(250);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        /// <summary>
        /// À toutes les 0.25 secondes la fonction DeleteRows est appellé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dt_Tick(object sender, EventArgs e)
        {
            DeleteRows();
        }

        /// <summary>
        /// Les vols qui sont cancellés ou terminé ne s'afficheront plus
        /// </summary>
        private void DeleteRows()
        {
            for (int i = 0; i < EC.LstVols.Count; i++)
            {
                if (EC.LstVols[i].EstLance || EC.LstVols[i].EtatVol == Etat.Cancelle || EC.LstVols[i].EtatVol == Etat.Decollage || EC.LstVols[i].EtatVol == Etat.Atterrissage)
                {
                    DataGridRow row = (DataGridRow)dgVols.ItemContainerGenerator.ContainerFromIndex(i);

                    row.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Le bouton retarder permet de retarder un vol selon le temps choisi dans les combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRetarder_Click(object sender, RoutedEventArgs e)
        {
            Vol v = (Vol)(dgVols.SelectedItem);

            switch (v.TempsUnite)
            {
                case "Secondes":
                    FacteursExterieurs.VolRetarde((Vol)(dgVols.SelectedItem), v.TempsRetard * 1, EC);
                    break;
                case "Minutes":
                    FacteursExterieurs.VolRetarde((Vol)(dgVols.SelectedItem), v.TempsRetard * 60, EC);
                    break;
                case "Heures":
                    FacteursExterieurs.VolRetarde((Vol)(dgVols.SelectedItem), v.TempsRetard * 60 * 60, EC);
                    break;
            }
            
            dgVols.Items.Refresh();
        }

        /// <summary>
        /// En cliquant sur ce bouton, la piste choisi change d'état
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangerEtatPiste_Click(object sender, RoutedEventArgs e)
        {
            FacteursExterieurs.ChangerEtatPiste((Piste)dgPistes.SelectedItem, EC);

            dgPistes.Items.Refresh();
            EC.dgPistes.Items.Refresh();

            EC.ChangerEtatPiste((Piste)dgPistes.SelectedItem);
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

        /// <summary>
        /// Ce bouton annule le vol choisi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            EcranConfirmation eConf = new EcranConfirmation(EC.Controleur);

            if(eConf.ShowDialog() == true)
                EC.AnnulerVol(((Vol)(dgVols.SelectedItem)).IdVol);
        }

        /// <summary>
        /// Quand on change la valeur du combobox, cette donnée est sauvegardé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTemps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Vol v = ((Vol)dgVols.SelectedItem);
            if (v != null)
            {
                ComboBox cboTemps = (ComboBox)sender;
                ComboBoxItem cboiTemps = (ComboBoxItem)cboTemps.Items[cboTemps.SelectedIndex];           

                v.TempsRetard = int.Parse(cboiTemps.Content.ToString());
            }
            
        }

        /// <summary>
        /// Quand on change la valeur du combobox, cette donnée est sauvegardé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboValeur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Vol v = ((Vol)dgVols.SelectedItem);
            if (v != null)
            {
                ComboBox cboUnite = (ComboBox)sender;
                ComboBoxItem cboiUnite = (ComboBoxItem)cboUnite.Items[cboUnite.SelectedIndex];

                v.TempsUnite = cboiUnite.Content.ToString();
            }            
        }

        /// <summary>
        /// Quand on change la valeur du combobox, cette donnée est sauvegardé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboAccel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            float accel = 1;

            switch (cbo.SelectedIndex)
            {
                case 0: accel = 0.1F; break;
                case 1: accel = 0.2F; break;
                case 2: accel = 0.5F; break;
                case 3: accel = 1; break;
                case 4: accel = 2; break;
                case 5: accel = 5; break;
                case 6: accel = 10; break;
            }

            if(EC != null)
                EC.AccelererTemps(accel);
        }

        private void btnAide_Click(object sender, RoutedEventArgs e)
        {
            EcranGuide eg;
            eg = new EcranGuide(10);
            eg.Show();
        }
    }
}
