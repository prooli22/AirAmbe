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
        public EcranConfiguration(EcranControleur ec)
        {
            InitializeComponent();

            EC = ec;

            dgVols.ItemsSource = EC.LstVols;
            dgPistes.ItemsSource = EC.LstPistes;
        }

        private void btnRetarder_Click(object sender, RoutedEventArgs e)
        {
            FacteursExterieurs.VolRetarde((Vol)(dgVols.SelectedItem),30, EC);

            dgVols.Items.Refresh();
            EC.refreshDG();
        }

        private void btnChangerEtatPiste_Click(object sender, RoutedEventArgs e)
        {
            FacteursExterieurs.ChangerEtatPiste((Piste)dgPistes.SelectedItem);

            dgPistes.Items.Refresh();
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
    }
}
