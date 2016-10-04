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
using AirAmbe.Model;
using AirAmbe.ViewModel;
using System.Collections.ObjectModel;

namespace AirAmbe
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class EcranAdministrateur : Window
    {
        public EcranAdministrateur()
        {
            InitializeComponent();

            DataContext = new AirAmbe.ViewModel.EcranTestConnexionViewModel();            

            dgUtilisateur.ItemsSource = ((EcranTestConnexionViewModel)DataContext).SommaireUtilisateurs;
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            EcranUtilisateur eUser = new EcranUtilisateur((Utilisateur)(dgUtilisateur.SelectedItem),true);
            this.Close();
            eUser.Show();
        }
    }
}
