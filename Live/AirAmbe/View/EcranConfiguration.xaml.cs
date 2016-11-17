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
        public EcranControleur EC { get; set; }
        public int Secondes { get; set; } = 30;
        public EcranConfiguration(EcranControleur ec)
        {
            InitializeComponent();

            EC = ec;

            dgVols.ItemsSource = EC.LstVols;
            dgPistes.ItemsSource = EC.LstPistes;
        }

        private void btnRetarder_Click(object sender, RoutedEventArgs e)
        {
            /*DataGridRow row = dgVols.ItemContainerGenerator.ContainerFromIndex(dgVols.SelectedIndex) as DataGridRow;
            var i = 5;
            var ele = ((ContentPresenter)(dgVols.Columns[i].GetCellContent(row))).Content;
            MessageBox.Show(ele.GetType().ToString());*/

            FacteursExterieurs.VolRetarde((Vol)(dgVols.SelectedItem),Secondes, EC);

            Secondes = 30;
            
            dgVols.Items.Refresh();
        }

        private void btnChangerEtatPiste_Click(object sender, RoutedEventArgs e)
        {
            FacteursExterieurs.ChangerEtatPiste((Piste)dgPistes.SelectedItem);

            dgPistes.Items.Refresh();
            EC.dgPistes.Items.Refresh();
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

        private void txtSecondes_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtSecs = sender as TextBox;

            Secondes = Int32.Parse(txtSecs.Text);
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            EC.AnnulerVol(((Vol)(dgVols.SelectedItem)).IdVol);
        }
    }
}
