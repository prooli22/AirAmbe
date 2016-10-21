using AirAmbe.Model;
using AirAmbe.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

        public List<Vol> LstVols { get; set; }

        public List<Vol> LstDecollages { get; set; }

        public List<Vol> LstAtterrissages { get; set; }

        public List<Scenario> LstScenarios { get; set; }

        public int NbPistes { get; set; }

        public bool EstObservateur { get; set; }

        //static Timer t;


        // ---------------------------------------------------------------------------------- \\


        // Liens utiles.
        //http://stackoverflow.com/questions/12690400/digital-clock-on-a-form-should-show-time-changing-datetime-now-shows-only-still
        //http://stackoverflow.com/questions/1329900/net-event-every-minute-on-the-minute-is-a-timer-the-best-option


        // ---------------------------------------------------------------------------------- \\


        /// <summary>
        /// Constructeur de la fênetre du contrôleur.
        /// </summary>
        /// <param name="U"> Un contrôleur </param>
        public EcranControleur(Utilisateur U)
        {
            InitializeComponent();
            Controleur = U;
            EstObservateur = false;


            ChargerScenarios();
            ChargerVols();
            ModifierHeures();
            ChargerDataGrid();
            ChargerProchainsVols();
        }


        /// <summary>
        /// Constructeur de la fenêtre de l'observateur.
        /// </summary>
        public EcranControleur()
        {
            InitializeComponent();
            btnProfil.Visibility = Visibility.Hidden;
            EstObservateur = true;


            ChargerScenarios();
            ChargerVols();
            ModifierHeures();
            ChargerDataGrid();
            ChargerProchainsVols();
        }


        // ---------------------------------------------------------------------------------- \\


        private void ChargerScenarios()
        {
            LstScenarios = new List<Scenario>();

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
                LstScenarios.Add(S);
            }
        }


        private void ChargerVols()
        {
            LstVols = new List<Vol>();
            VolAS VA = new VolAS();

            for (int i = 0; i < LstScenarios.Count; i++)
            {
                for (int j = 0; j < LstScenarios[i].NumVol.Count(); j++)
                {
                    Vol V = VA.Recuperer(LstScenarios[i].NumVol[j]);
                    V.Intervalle = LstScenarios[i].Intervalle;
                    V.NumScenario = i + 1;
                    LstVols.Add(V);
                }
            }
        }


        private void ModifierHeures()
        {
            int nbVols = 1;

            for (int i = 0; i < LstVols.Count; i++)
            {
                if (LstVols[i].DateVol.Second != 0)
                    LstVols[i].DateVol = LstVols[i].DateVol.AddSeconds(60 - LstVols[i].DateVol.Second);

                if (i > 0 && (LstVols[i].NumScenario != LstVols[i - 1].NumScenario))
                {
                    LstVols[i].DateVol = LstVols[i - 1].DateVol.AddMinutes(5);
                    nbVols = 1;
                }

                LstVols[i].DateVol = LstVols[i].DateVol.AddMinutes(LstVols[i].Intervalle * nbVols);
                nbVols++;

                LstVols[i].TrouverDelais();
            }

        }


        private void ChargerDataGrid()
        {
            LstDecollages = new List<Vol>();
            LstAtterrissages = new List<Vol>();

            for (int i = 0; i < LstVols.Count; i++)
            {
                if (LstVols[i].EstAtterrissage)
                    LstAtterrissages.Add(LstVols[i]);

                else
                    LstDecollages.Add(LstVols[i]);
            }

            dgAtterissages.ItemsSource = LstAtterrissages;
            dgDecollages.ItemsSource = LstDecollages;
        }


        private void ChargerProchainsVols()
        {
            for (int i = 0; i < LstVols.Count; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = GridLength.Auto;
                grdProchainsVols.RowDefinitions.Add(gridRow);

                AfficherDetailsVols(LstVols[i], i);
            }
        }


        // ---------------------------------------------------------------------------------- \\


        private void AfficherDetailsVols(Vol vol, int compteurVols)
        {
            Border brdVol = new Border();
            brdVol.Height = Double.NaN;
            brdVol.BorderBrush = Brushes.Black;
            brdVol.BorderThickness = new Thickness(2);
            brdVol.Margin = new Thickness(10);


            Grid grdVol = new Grid();
            grdVol.Name = "grd" + vol.NumeroVol;
            grdVol.Height = 60;
            RowDefinition grdRow1 = new RowDefinition();
            RowDefinition grdRow2 = new RowDefinition();
            grdRow1.Height = GridLength.Auto;
            grdRow2.Height = GridLength.Auto;
            grdVol.RowDefinitions.Add(grdRow1);
            grdVol.RowDefinitions.Add(grdRow2);

            if (vol.EstAtterrissage)
                grdVol.Background = Brushes.LightBlue;

            else
                grdVol.Background = Brushes.LightGreen;


            grdVol = AfficherDetailsVolsH(grdVol, vol);
            grdVol = AfficherDetailsVolsB(grdVol, vol);

            brdVol.Child = grdVol;

            Grid.SetRow(brdVol, compteurVols);
            grdProchainsVols.Children.Add(brdVol);
        }


        private Grid AfficherDetailsVolsH(Grid grdVol, Vol vol)
        {
            Ellipse E = CreerCercle();

            if (vol.EstAtterrissage)
                E.Fill = Brushes.LightBlue;

            else
                E.Fill = Brushes.LightGreen;

            Grid.SetRow(E, 0);


            Image imgEtat = TrouverEtat(vol.EtatVol, 10);
            imgEtat.Name = "imgEtat" + vol.NumeroVol;
            Grid.SetRow(imgEtat, 0);


            Label lblNumVol = new Label();
            lblNumVol.Content = "Vol " + vol.NumeroVol;
            lblNumVol.Height = 30;
            lblNumVol.Width = 100;
            lblNumVol.HorizontalAlignment = HorizontalAlignment.Center;
            lblNumVol.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(lblNumVol, 0);


            Timer t = new Timer();
            t.AutoReset = false;
            t.Elapsed += new ElapsedEventHandler((sender, e) => t_Elapsed(sender, e, vol, t));
            t.Interval = (60 - DateTime.Now.Second) * 1000 - DateTime.Now.Millisecond;
            t.Start();


            Label lblDelais = new Label();
            lblDelais.Name = "lblDelais" + vol.NumeroVol;
            lblDelais.Content = "Dans " + vol.Delais.ToString() + " minutes";
            lblDelais.Height = 30;
            lblDelais.Width = 100;
            lblDelais.HorizontalAlignment = HorizontalAlignment.Center;
            lblDelais.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(lblDelais, 0);


            Image imgHamburger = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("pack://application:,,,/SolutionTest;component/Images/hamburger.png");
            b.EndInit();
            imgHamburger.Source = b;


            Button btnDetailsVols = new Button();
            btnDetailsVols.Content = imgHamburger;
            btnDetailsVols.Style = this.FindResource("NoChromeButton") as Style;
            btnDetailsVols.Click += new RoutedEventHandler(btnDetailsVols_Click);
            btnDetailsVols.MouseEnter += new MouseEventHandler(btn_MouseEnter);
            btnDetailsVols.MouseLeave += new MouseEventHandler(btn_MouseLeave);
            btnDetailsVols.Height = 30;
            btnDetailsVols.Width = 30;
            btnDetailsVols.HorizontalAlignment = HorizontalAlignment.Right;
            btnDetailsVols.Margin = new Thickness(0, 7, 10, 0);
            Grid.SetRow(btnDetailsVols, 0);


            grdVol.Children.Add(E);
            grdVol.Children.Add(imgEtat);
            grdVol.Children.Add(lblNumVol);
            grdVol.Children.Add(lblDelais);
            grdVol.Children.Add(btnDetailsVols);


            return grdVol;
        }


        private Grid AfficherDetailsVolsB(Grid grdVol, Vol vol)
        {
            string atterrissage = "";
            string estime = "";


            if (vol.EstAtterrissage)
            {
                atterrissage = "Attérit ";
                estime = "ETA : ";
            }
                
            else
            {
                atterrissage = "Décolle ";
                estime = "ETD : ";
            }
                

            Rectangle R = new Rectangle();
            R.Fill = Brushes.Black;
            R.Width = Double.NaN;
            R.Height = 2;
            R.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(R, 1);


            Label lblAtterrissage = new Label();
            lblAtterrissage.Content = atterrissage + "sur la piste :";
            lblAtterrissage.Height = 30;
            lblAtterrissage.VerticalAlignment = VerticalAlignment.Top;
            lblAtterrissage.HorizontalAlignment = HorizontalAlignment.Left;
            lblAtterrissage.Margin = new Thickness(10, 0, 0, 0);
            Grid.SetRow(lblAtterrissage, 1);


            ComboBox cboPistes = new ComboBox();
            ComboBoxItem cmbItem = new ComboBoxItem() { Content = "Aucune piste" };
            cboPistes.Height = 25;
            cboPistes.Width = 100;
            cboPistes.VerticalAlignment = VerticalAlignment.Top;
            cboPistes.HorizontalAlignment = HorizontalAlignment.Left;
            cboPistes.Margin = new Thickness(14, 30, 0, 0);
            cboPistes.Items.Add(cmbItem);
            cboPistes.SelectedItem = cmbItem;
            cboPistes.SelectionChanged += new SelectionChangedEventHandler(cboPistes_Selection);

            if (vol.EstAtterrissage)
                cboPistes.Name = "cbo"+ Etat.Atterrissage.ToString() + vol.NumeroVol;

            else
                cboPistes.Name = "cbo" + Etat.Decollage.ToString() + vol.NumeroVol;

            for (int i = 0; i < NbPistes; i++)
                cboPistes.Items.Add(new ComboBoxItem() { Content = "Piste #" + (i + 1) });

            if (EstObservateur)
                cboPistes.IsEnabled = false;

            Grid.SetRow(cboPistes, 1);


            Label lblETA = new Label();
            lblETA.Content = estime + vol.DateVol.ToString("HH:mm:ss");
            lblETA.Height = 30;
            lblETA.VerticalAlignment = VerticalAlignment.Top;
            lblETA.HorizontalAlignment = HorizontalAlignment.Left;
            lblETA.Margin = new Thickness(10, 60, 0, 0);
            Grid.SetRow(lblETA, 1);


            grdVol.Children.Add(R);
            grdVol.Children.Add(lblAtterrissage);
            grdVol.Children.Add(cboPistes);
            grdVol.Children.Add(lblETA);

            return grdVol;
        }


        private Image TrouverEtat(Etat A, int thickness)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();

            switch(A)
            {
                case Etat.Attente :
                    b.UriSource = new Uri("pack://application:,,,/SolutionTest;component/Images/x.png");
                    break;

                case Etat.Assigne :
                    b.UriSource = new Uri("pack://application:,,,/SolutionTest;component/Images/crochet.png");
                    break;

                case Etat.Critique :
                    b.UriSource = new Uri("pack://application:,,,/SolutionTest;component/Images/exclamation.png");
                    break;

                default:
                    break;
            }

            b.EndInit();

            Image img = new Image();
            img.Source = b;
            img.Width = 40;
            img.Height = 40;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.Margin = new Thickness(thickness, 0, 0, 0);

            return img;
        }


        private Ellipse CreerCercle()
        {
            Ellipse E = new Ellipse();
            E.Height = 40;
            E.Width = 40;
            E.HorizontalAlignment = HorizontalAlignment.Left;
            E.VerticalAlignment = VerticalAlignment.Center;
            E.Margin = new Thickness(10, 10, 0, 10);
            E.Stroke = Brushes.Black;
            E.StrokeThickness = 2;
            return E;
        }


        // ---------------------------------------------------------------------------------- \\


        private void btnProfil_Click(object sender, RoutedEventArgs e)
        {
            EcranUtilisateur eUser = new EcranUtilisateur(Controleur, false);
            this.Close();
            eUser.Show();
        }


        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            EcranConnexion eCon = new EcranConnexion();
            this.Close();
            eCon.Show();
        }


        private void btnDetailsVols_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Grid grd = btn.Parent as Grid;

            if (grd.Height == 60)
                grd.Height = Double.NaN;

            else
                grd.Height = 60;
        }


        private void cboPistes_Selection(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            Grid grd = cbo.Parent as Grid;
            Image imgAssignation = new Image();
            int longueur = 0;
            Etat AssignationTemp;


            Ellipse E = CreerCercle();

            if (cbo.Name.Contains(Etat.Atterrissage.ToString()))
            {
                E.Fill = Brushes.LightBlue;
                longueur = Etat.Atterrissage.ToString().Length;
                
            }
                
            else
            {
                E.Fill = Brushes.LightGreen;
                longueur = Etat.Decollage.ToString().Length;
            }
                

            Grid.SetRow(E, 0);


            string NumVolTemp = cbo.Name.Remove(0, 3 + longueur);


            if (cbo.SelectedIndex > 0)
            {
                imgAssignation = TrouverEtat(Etat.Assigne, 9);
                AssignationTemp = Etat.Assigne;
            }
                
            else
            {
                imgAssignation = TrouverEtat(Etat.Attente, 10);
                AssignationTemp = Etat.Attente;
            }
                
            Grid.SetRow(imgAssignation, 0);


            grd.Children.Add(E);
            grd.Children.Add(imgAssignation);


            for (int i = 0; i < LstVols.Count; i++)
            {
                if (LstVols[i].NumeroVol == NumVolTemp)
                    LstVols[i].EtatVol = AssignationTemp;
            }
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


        private void t_Elapsed(object sender, ElapsedEventArgs e, Vol vol, Timer t)
        {
            vol.TrouverDelais();

            this.Dispatcher.Invoke(() =>
            {
                foreach (Label lbl in FindVisualChildren<Label>(grdProchainsVols))
                {
                    if (lbl.Name == "lblDelais" + vol.NumeroVol)
                        lbl.Content = "Dans " + vol.Delais.ToString() + " minutes";
                }

                if (vol.Delais <= 5 && vol.EtatVol == Etat.Attente)
                {
                    //Grid grd = new Grid();
                    
                    foreach (Image img in FindVisualChildren<Image>(grdProchainsVols))
                    {
                        //Grid grd = img.Parent as Grid;

                        if (img.Name == "imgEtat" + vol.NumeroVol)
                        {
                            img.Visibility = Visibility.Hidden;
                            //grd = img.Parent as Grid;

                            //Image imgEtat = TrouverEtat(vol.EtatVol, 10);
                            //imgEtat.Name = "imgEtat" + vol.NumeroVol;
                            //Grid.SetRow(imgEtat, 0);

                            //grd.Children.Add(imgEtat);
                        }
                    }

                    //Image imgEtat = TrouverEtat(vol.EtatVol, 10);
                    //imgEtat.Name = "imgEtat" + vol.NumeroVol;
                    //Grid.SetRow(imgEtat, 0);

                    //grd.Children.Add(imgEtat);
                }
            });

            t.Interval = (60 - DateTime.Now.Second) * 1000 - DateTime.Now.Millisecond;
            t.Start();
        }


        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
