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
    public partial class MainWindow : Window
    {
        public List<Utilisateur> lstUser = new List<Utilisateur>();
        public MainWindow()
        {
            InitializeComponent();

            DateTime dt = new DateTime(1999, 11, 5);
            Utilisateur user1 = new Utilisateur(1, 1, "Admin", "oprovost", "123", "Olivier", "Provost", "1345",dt, "1234567890", "bl@hoo.com");
            lstUser.Add(user1);
            DateTime dt1 = new DateTime(2000, 11, 5);
            Utilisateur user2 = new Utilisateur(2, 2, "Controleur", "amasse", "baseball", "Anthony", "Masse", "0000", dt1, "1234567890", "bl@hoo.com");
            lstUser.Add(user2);
            DateTime dt3 = new DateTime(2016, 11, 5);
            Utilisateur user3 = new Utilisateur(3, 3, "Admin", "vdesilets", "5555", "Vincent", "Désilets", "3354", dt3, "1234567890", "bl@hoo.com");
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            lstUser.Add(user3);
            
            dgUtilisateur.ItemsSource = lstUser;
        }
    }
}
