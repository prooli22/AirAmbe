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
    /// Logique d'interaction pour EcranConfiguration.xaml
    /// </summary>
    public partial class EcranConfiguration : Window
    {
        private EcranControleur EC;
        
        public EcranConfiguration(EcranControleur ec)
        {
            InitializeComponent();
            
            EC = ec;

            dgVols.ItemsSource = EC.LstVols;
            dgPistes.ItemsSource = EC.LstPistes;
        }

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

        private void btnChangerEtatPiste_Click(object sender, RoutedEventArgs e)
        {
            FacteursExterieurs.ChangerEtatPiste((Piste)dgPistes.SelectedItem, EC);

            dgPistes.Items.Refresh();
            EC.dgPistes.Items.Refresh();

            EC.ChangerEtatPiste((Piste)dgPistes.SelectedItem);
        }

        private void btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Height += 2;
            btn.Width += 2;
        }

        private void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Height -= 2;
            btn.Width -= 2;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            EcranConfirmation eConf = new EcranConfirmation(EC.Controleur);

            if(eConf.ShowDialog() == true)
                EC.AnnulerVol(((Vol)(dgVols.SelectedItem)).IdVol);
        }

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
    }
}
