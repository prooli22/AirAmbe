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

namespace ApplicationScenario
{
    /// <summary>
    /// Logique d'interaction pour EcranAjoutScenario.xaml
    /// </summary>
    public partial class EcranAjoutScenario : Window
    {
        public int nbVol { get; set; }

        public EcranAjoutScenario()
        {
            InitializeComponent();

            nbVol = 0;
            AjouterVol();
        }

        private void AjouterVol()
        {
            RowDefinition gridRow = new RowDefinition();
            gridVols.RowDefinitions.Add(gridRow);

            Label lblVol = new Label();
            lblVol.Content = "Vol" + nbVol;
            lblVol.Width = 75;
            lblVol.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(lblVol, nbVol);
            Grid.SetColumn(lblVol, 0);

            ComboBox cboVols = new ComboBox();





            gridVols.Children.Add(lblVol);

            nbVol++;
        }

        private void btnAjouterVol_Click(object sender, RoutedEventArgs e)
        {
            AjouterVol();
        }
    }
}
