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
using System.Windows.Threading;

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

        public List<Piste> LstPistes { get; set; }

        //public Animation Anim { get; set; }
       
       
        /// <summary>
        /// Constructeur de la fênetre du contrôleur.
        /// </summary>
        /// <param name="U"> Un contrôleur </param>
        public EcranControleur(Utilisateur U)
        {
            InitializeComponent();
            Controleur = U;
            //Anim = new Animation(this);
            

            if (Controleur == null)
                btnProfil.Visibility = Visibility.Hidden;


            if (ChargerScenarios())
            {
                ChargerVols();
                ModifierHeures();
                ChargerDataGrid();
                ChargerProchainsVols();

                //FacteursExterieurs.StartTimer(this);

                //Anim.DessinerHangar();
                //Anim.GererDessinPiste(LstPistes.Count);
                //Anim.DessinerVoieService();
            }

            else
            {
                MessageBox.Show("Le fichier de scénarios n'est pas présent ou encore il est vide. Veuillez contacter l'administrateur de l'application pour remédier au problème.", "Air-Ambe", MessageBoxButton.OK, MessageBoxImage.Error);
                btnProfil.Visibility = Visibility.Hidden;
                btnRefresh.Visibility = Visibility.Hidden;
            }
        }


        // ---------------------------------------------------------------------------------- \\


        private bool ChargerScenarios()
        {
            LstScenarios = new List<Scenario>();
            LstPistes = new List<Piste>();
            string[] scenarios;

            try { scenarios = System.IO.File.ReadAllLines(@".\scenarios.txt"); }

            catch (Exception e)
            {
                return false;
            }

            if (scenarios.Length == 0)
                return false;

            // On charge le nombre de pistes dans la liste.
            for (int i = 0; i < Int32.Parse(scenarios[0]); i++)
            {
                LstPistes.Add(new Piste());
                LstPistes[i].NumPiste = i + 1;
            }

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

            return true;
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
                // Met les secondes à 00
                if (LstVols[i].DateVol.Second != 0)
                    LstVols[i].DateVol = LstVols[i].DateVol.AddSeconds(-LstVols[i].DateVol.Second);

                // Ajoute 5 minutes entre chaque scénario
                if (i > 0 && (LstVols[i].NumScenario != LstVols[i - 1].NumScenario))
                {
                    LstVols[i].DateVol = LstVols[i - 1].DateVol.AddMinutes(5);
                    nbVols = 1;
                }

                // Ajoute le temps de l'intervalle pour un scénario
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

                LstVols[i].IdVol = i + 1;

                if (i < 10)
                {
                    AfficherDetailsVols(LstVols[i], i);
                    TesterEtat(LstVols[i]);
                } 
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
            grdVol.Name = "grdVol" + vol.IdVol.ToString();
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
            Ellipse E = new Ellipse();
            E.Height = 40;
            E.Width = 40;
            E.HorizontalAlignment = HorizontalAlignment.Left;
            E.VerticalAlignment = VerticalAlignment.Center;
            E.Margin = new Thickness(10, 10, 0, 10);
            E.Stroke = Brushes.Black;
            E.StrokeThickness = 2;
            E.Fill = Brushes.Transparent;
            Grid.SetRow(E, 0);


            Image imgEtat = new Image();
            imgEtat.Name = "imgEtat" + vol.IdVol.ToString();
            imgEtat.Source = TrouverEtat(Etat.Attente);
            imgEtat.Width = 40;
            imgEtat.Height = 40;
            imgEtat.HorizontalAlignment = HorizontalAlignment.Left;
            imgEtat.Margin = new Thickness(10, 0, 0, 0);
            Grid.SetRow(imgEtat, 0);


            Label lblNumVol = new Label();
            lblNumVol.Content = "Vol " + vol.NumeroVol;
            lblNumVol.Height = 30;
            lblNumVol.Width = 110;
            lblNumVol.HorizontalAlignment = HorizontalAlignment.Center;
            lblNumVol.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(lblNumVol, 0);


            Timer t = new Timer();
            t.AutoReset = false;
            t.Elapsed += new ElapsedEventHandler((sender, e) => t_Elapsed(sender, e, vol, t));
            t.Interval = (60 - DateTime.Now.Second) * 1000 - DateTime.Now.Millisecond;
            t.Start();


            Label lblDelais = new Label();
            lblDelais.Name = "lblDelais" + vol.IdVol.ToString();
            lblDelais.Content = "Dans " + vol.Delais.ToString() + " minutes";
            lblDelais.Height = 30;
            lblDelais.Width = 110;
            lblDelais.HorizontalAlignment = HorizontalAlignment.Center;
            lblDelais.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(lblDelais, 0);


            Image imgHamburger = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/hamburger.png");
            b.EndInit();
            imgHamburger.Source = b;


            Button btnDetailsVols = new Button();
            btnDetailsVols.Name = "btnDetails" + vol.IdVol.ToString();
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

            if (vol.PisteAssigne == null)
                cboPistes.SelectedIndex = 0;

            else
                cboPistes.SelectedIndex = vol.PisteAssigne.NumPiste;

            //cboPistes.SelectedItem = cmbItem;
            cboPistes.SelectionChanged += new SelectionChangedEventHandler((sender, e) => cboPistes_Selection(sender, e, vol));

            if (vol.EstAtterrissage)
                cboPistes.Name = "cbo" + Etat.Atterrissage.ToString() + vol.IdVol.ToString();

            else
                cboPistes.Name = "cbo" + Etat.Decollage.ToString() + vol.IdVol.ToString();

            for (int i = 0; i < LstPistes.Count; i++)
                cboPistes.Items.Add(new ComboBoxItem() { Content = "Piste #" + (i + 1) });

            if (Controleur == null)
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


        private BitmapImage TrouverEtat(Etat A)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();

            switch (A)
            {
                case Etat.Attente:
                    b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/x.png");
                    break;

                case Etat.Assigne:
                    b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/crochet.png");
                    break;

                case Etat.Critique:
                    b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/exclamation.png");
                    break;

                case Etat.Atterrissage:
                    b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/atterrissage.png");
                    break;

                case Etat.Decollage:
                    b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/decollage.png");
                    break;

                case Etat.Echoue:
                    b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/interrogation.png");
                    break;

                default:
                    break;
            }

            b.EndInit();

            return b;
        }


        private void TesterEtat(Vol vol)
        {
            Grid grd = new Grid();

            foreach (Image img in TrouverEnfant<Image>(grdProchainsVols))
            {
                if (img.Name.Contains(vol.IdVol.ToString()))
                {
                    grd = img.Parent as Grid;

                    if (vol.Delais <= 5 && (vol.EtatVol == Etat.Attente || vol.EtatVol == Etat.Critique))
                    {
                        vol.EtatVol = Etat.Critique;
                        grd.Background = Brushes.Firebrick;
                    }

                    else if (vol.EtatVol == Etat.Atterrissage || vol.EtatVol == Etat.Decollage)
                    {
                        img.Height = 37;
                        img.Margin = new Thickness(12, 0, 0, 0);
                    }

                    img.Source = TrouverEtat(vol.EtatVol);

                    break;
                }
            }
        }


        // À TERMINER.
        public void TesterPiste(Vol vol)
        {
            for (int i = 0; LstVols.Count > 20 ? i < 20 : i < LstVols.Count; i++)
            {
                if (LstVols[i] != vol)
                {
                    TimeSpan t = LstVols[i].DateVol - vol.DateVol;

                    if (t.TotalSeconds <= 180 && t.TotalSeconds >= -180)
                    {
                        foreach (ComboBox cboT in TrouverEnfant<ComboBox>(grdProchainsVols))
                        {
                            if (cboT.Name.Contains(LstVols[i].IdVol.ToString()))
                            {
                                ComboBoxItem cboI = cboT.Items[vol.PisteAssigne.NumPiste] as ComboBoxItem;

                                if (vol.EtatVol == Etat.Assigne)
                                    cboI.Foreground = Brushes.Red;

                                else
                                    cboI.Foreground = Brushes.Black;
                            }
                        }
                    }
                }
            }
        }


        // ---------------------------------------------------------------------------------- \\


        private void btnProfil_Click(object sender, RoutedEventArgs e)
        {
            EcranUtilisateur eUser = new EcranUtilisateur(Controleur, false, null);
            this.Close();
            eUser.Show();
        }


        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            grdProchainsVols.Children.Clear();

            for (int i = 0; LstVols.Count > 10 ? i < 10 : i < LstVols.Count; i++)
            {
                if (LstVols[i].EtatVol != Etat.Atterrissage || LstVols[i].EtatVol != Etat.Decollage || LstVols[i].EtatVol != Etat.Echoue)
                {
                    AfficherDetailsVols(LstVols[i], i);
                    TesterEtat(LstVols[i]);
                }
            }

            LstVols.Sort((a, b) => DateTime.Compare(a.DateVol, b.DateVol));
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


        private void cboPistes_Selection(object sender, SelectionChangedEventArgs e, Vol vol)
        {
            ComboBox cbo = sender as ComboBox;
            ComboBoxItem cboi = cbo.Items[cbo.SelectedIndex] as ComboBoxItem;
            Grid grd = new Grid();
            Image imgVol = new Image();

            foreach (Image img in TrouverEnfant<Image>(grdProchainsVols))
            {
                if (img.Name.Contains(vol.IdVol.ToString()))
                {
                    imgVol = img;
                    grd = img.Parent as Grid;
                    break;
                }
            }

            // Si la piste n'est pas disponible et qu'on veut la forcer.
            if (cboi.Foreground == Brushes.Red)
            {
                EcranConfirmation eC = new EcranConfirmation(Controleur);
                eC.ShowDialog();
            }

            // Si l'utilisateur choisi une piste. On change l'état a ASSIGNE.
            if (cbo.SelectedIndex > 0)
            {
                imgVol.Source = TrouverEtat(Etat.Assigne);
                imgVol.Margin = new Thickness(9, 0, 0, 0);

                if (vol.EstAtterrissage)
                    grd.Background = Brushes.LightBlue;

                else
                    grd.Background = Brushes.LightGreen;

                vol.EtatVol = Etat.Assigne;
                vol.PisteAssigne = LstPistes[cbo.SelectedIndex - 1];
                TesterPiste(vol);
            }

            // Sinon on remet l'état a ATTENTE.
            else
            {
                imgVol.Source = TrouverEtat(Etat.Attente);
                imgVol.Margin = new Thickness(10, 0, 0, 0);

                vol.EtatVol = Etat.Attente;
                TesterPiste(vol);
                vol.PisteAssigne = null;
                TesterEtat(vol);
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
            Grid grd = null;
            Label lblVol = null;

            vol.TrouverDelais();


            foreach (Label lbl in TrouverEnfant<Label>(grdProchainsVols))
            {
                if (lbl.Name.Contains(vol.IdVol.ToString()))
                {
                    grd = lbl.Parent as Grid;
                    lblVol = lbl;
                    break;
                }
            }
           

            // Changer quand le vol est terminée.
            if (vol.Delais <= 0)
            {
                foreach (Button btn in TrouverEnfant<Button>(grdProchainsVols))
                    if (btn.Name.Contains(vol.IdVol.ToString()))
                        grd.Children.Remove(btn);

                if (vol.EtatVol == Etat.Critique)
                {
                    lblVol.Content = "Le vol a échoué!";
                    vol.EtatVol = Etat.Echoue;
                }

                else if (vol.EstAtterrissage)
                {
                    //Anim.DemarreAtterrissage(vol.PisteAssigne.NumPiste);
                    lblVol.Content = "Attérit sur la piste " + vol.PisteAssigne.NumPiste + " à " + vol.DateVol.ToString("HH:mm");
                    lblVol.Width = 170;
                    lblVol.Margin = new Thickness(58, 0, 0, 0);
                    vol.EtatVol = Etat.Atterrissage;
                }

                else
                {
                    //Anim.DemarreDecollage(vol.PisteAssigne.NumPiste);
                    lblVol.Content = "Décolle sur la piste " + vol.PisteAssigne.NumPiste + " à " + vol.DateVol.ToString("HH:mm");
                    lblVol.Width = 170;
                    lblVol.Margin = new Thickness(58, 0, 0, 0);
                    vol.EtatVol = Etat.Decollage;
                }

                grd.Height = 60;
                grd.Background = Brushes.LightGray;
                TesterEtat(vol);
                TesterPiste(vol);

                vol.PisteAssigne = null;
            }

            // Changer les secondes.
            else if (vol.Delais <= 1)
            {
                lblVol.Content = "Dans " + (60 - DateTime.Now.Second).ToString() + " secondes";

                t.Interval = 1000;
                t.Start();
            }

            // Changer les minutes.
            else
            {
                //this.Dispatcher.Invoke(() => {  });

                lblVol.Content = "Dans " + vol.Delais.ToString() + " minutes";

                t.Interval = (60 - DateTime.Now.Second) * 1000 - DateTime.Now.Millisecond;
                t.Start();
            }

            TesterEtat(vol);
            
        }


        public IEnumerable<T> TrouverEnfant<T>(DependencyObject depObj) where T : DependencyObject
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

                    foreach (T childOfChild in TrouverEnfant<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}