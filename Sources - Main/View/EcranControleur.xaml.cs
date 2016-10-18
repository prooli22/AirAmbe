using AirAmbe.Model;
using AirAmbe.ViewModel;
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
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class EcranControleur : Window
    {
        public Utilisateur Controleur { get; set; }

        public List<Vol> lstVols { get; set; }

        public List<Vol> lstDecollages { get; set; }

        public List<Vol> lstAtterrissages { get; set; }

        public List<Scenario> lstScenarios { get; set; }

        public int NbPistes { get; set; }


        /// <summary>
        /// Constructeur de la fênetre du contrôleur.
        /// </summary>
        /// <param name="U"> Un contrôleur </param>
        public EcranControleur(Utilisateur U)
        {
            InitializeComponent();
            Controleur = U;

            ChargerScenarios();
            ChargerVols();
            ModifierHeures();
            ChargerDataGrid();
        }


        /// <summary>
        /// Constructeur de la fenêtre de l'observateur.
        /// </summary>
        public EcranControleur()
        {
            InitializeComponent();
            btnProfil.Visibility = Visibility.Hidden;


            ChargerScenarios();
            ChargerVols();
            ModifierHeures();
            ChargerDataGrid();
        }


        private void ChargerScenarios()
        {
            lstScenarios = new List<Scenario>();

            string[] scenarios = System.IO.File.ReadAllLines(@".\scenarios.txt");

            // On charge le nombre de pistes.
            NbPistes = Int32.Parse(scenarios[0]);

            for (int i = 1; i < scenarios.Length; i++)
            {
                Scenario S = new Scenario();

                // Get l'intervalle de temps et la supprime de la chaine.
                int position = scenarios[i].IndexOf(';');
                S.Intervalle = Int32.Parse(scenarios[i].Substring(0, position));
                scenarios[i] = scenarios[i].Remove(0, position + 1);

                // Split la chaine en tableau de string;
                S.NumVol = scenarios[i].Split(';');

                // On ajoute le scénario à la liste.
                lstScenarios.Add(S);
            }
        }


        private void ChargerVols()
        {
            lstVols = new List<Vol>();
            VolAS VA = new VolAS();

            for (int i = 0; i < lstScenarios.Count; i++)
            {
                for (int j = 0; j < lstScenarios[i].NumVol.Count(); j++)
                {
                    Vol V = VA.Recuperer(lstScenarios[i].NumVol[j]);
                    V.Intervalle = lstScenarios[i].Intervalle;
                    lstVols.Add(V);
                }
            }
        }


        private void ModifierHeures()
        {
            for (int i = 0; i < lstVols.Count; i++)
            {
                if (i > 0 && (lstVols[i].Intervalle != lstVols[i - 1].Intervalle))
                    lstVols[i].Intervalle += (lstVols[i - 1].Intervalle * i) + 5;
                

                MessageBox.Show((60 - lstVols[i].DateVol.Second).ToString());
                MessageBox.Show((lstVols[i].Intervalle * i).ToString());

                lstVols[i].DateVol.AddSeconds(System.Convert.ToDouble(60 - lstVols[i].DateVol.Second));
                lstVols[i].DateVol.AddMinutes(System.Convert.ToDouble(lstVols[i].Intervalle * i));
            }

        }


        private void ChargerDataGrid()
        {
            lstDecollages = new List<Vol>();
            lstAtterrissages = new List<Vol>();

            for (int i = 0; i < lstVols.Count; i++)
            {
                if (lstVols[i].EstAtterrissage)
                    lstAtterrissages.Add(lstVols[i]);

                else
                    lstDecollages.Add(lstVols[i]);
            }

            dgAtterissages.ItemsSource = lstAtterrissages;
            dgDecollages.ItemsSource = lstDecollages;
        }


        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            EcranConnexion eCon = new EcranConnexion();
            this.Close();
            eCon.Show();
        }


        private void btnProfil_Click(object sender, RoutedEventArgs e)
        {
            EcranUtilisateur eUser = new EcranUtilisateur(Controleur, false);
            this.Close();
            eUser.Show();
        }
    }
}
