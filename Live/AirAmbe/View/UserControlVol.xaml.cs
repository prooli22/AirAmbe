// Nom : Oliver Provost
// Date : 2016-12-09


using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AirAmbe
{
    /// <summary>
    /// Un éléments des prochains vols.
    /// </summary>
    public partial class UserControlVol : UserControl, IDisposable
    {
        // Propriétés.
        public Vol vol { get; set; }

        public Animation AnimVol { get; set; }

        public float Accelerateur { get; set; }


        // Variables.
        private EcranControleur EC;

        private int compteurVol;

        private DispatcherTimer dt500;

        private DispatcherTimer dtDelais;

        private Grid GrdVol;

        private Image imgEtat = new Image();

        private Label lblNumVol = new Label();

        private Label lblPiste = new Label();

        private Label lblDelais = new Label();

        private Button btnDetailsVols = new Button();

        public ComboBox cboPistes = new ComboBox();
        

        /// <summary>
        /// Constructeur du UserControlVol.
        /// </summary>
        /// <param name="ec"> L'écran de controle. </param>
        /// <param name="volEC"> Le vol correspondant. </param>
        /// <param name="compteur"> Le numero du UserControl. </param>
        public UserControlVol(EcranControleur ec, Vol volEC, int compteur)
        {
            InitializeComponent();
            EC = ec;
            vol = volEC;
            compteurVol = compteur;
            Accelerateur = ec.Accelerateur;

            AfficherDetailsVols();
            AfficherDetailsVolsH();
            AfficherDetailsVolsB();
            ChangerUserControl();
            TesterEtat();

            dt500 = new DispatcherTimer();
            dt500.Tick += dt_Tick;
            dt500.Interval = TimeSpan.FromMilliseconds(500);
            dt500.Start();

            dtDelais = new DispatcherTimer();
            dtDelais.Tick += dtDelais_Tick;
            dtDelais.Interval = TimeSpan.FromMilliseconds(1000);
            dtDelais.Start();

            AnimVol = new Animation(EC);
        }


        // ---------------------------------------------------------------------------------- \\
        #region Méthodes Maisons


        /// <summary>
        /// Contenu principal du UserControl.
        /// </summary>
        private void AfficherDetailsVols()
        {
            // Bordure du UserControl.
            Border brdVol = new Border();
            brdVol.Height = Double.NaN;
            brdVol.BorderBrush = Brushes.Black;
            brdVol.BorderThickness = new Thickness(2);
            brdVol.Margin = new Thickness(10);

            // Grid qui contient les éléments.
            GrdVol = new Grid();
            GrdVol.Name = "GrdVol" + vol.IdVol.ToString();
            GrdVol.Height = 60;
            RowDefinition grdRow1 = new RowDefinition();
            RowDefinition grdRow2 = new RowDefinition();
            grdRow1.Height = GridLength.Auto;
            grdRow2.Height = GridLength.Auto;
            GrdVol.RowDefinitions.Add(grdRow1);
            GrdVol.RowDefinitions.Add(grdRow2);

            // Choix de la couleur de fond.
            if (vol.EstAtterrissage)
                GrdVol.Background = Brushes.LightBlue;

            else
                GrdVol.Background = Brushes.LightGreen;

            brdVol.Child = GrdVol;

            // On ajoute le UserControl dans la grille contenant tous les UserControls.
            Grid.SetRow(brdVol, compteurVol);
            EC.grdProchainsVols.Children.Add(brdVol);
        }


        /// <summary>
        /// Partie du haut (toujours affichée).
        /// </summary>
        private void AfficherDetailsVolsH()
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


            imgEtat.Name = "imgEtat" + vol.IdVol.ToString();
            imgEtat.Source = TrouverEtat(Etat.Attente);
            imgEtat.Width = 40;
            imgEtat.Height = 40;
            imgEtat.HorizontalAlignment = HorizontalAlignment.Left;
            imgEtat.Margin = new Thickness(10, 0, 0, 0);
            Grid.SetRow(imgEtat, 0);

             
            lblDelais.Name = "lblDelais" + vol.IdVol.ToString();
            lblDelais.Height = 30;
            lblDelais.Width = 130;
            lblDelais.HorizontalAlignment = HorizontalAlignment.Center;
            lblDelais.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(lblDelais, 0);


            Image imgHamburger = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/hamburger.png");
            b.EndInit();
            imgHamburger.Source = b;


            btnDetailsVols.Name = "btnDetails" + vol.IdVol.ToString();
            btnDetailsVols.Content = imgHamburger;
            btnDetailsVols.Style = EC.FindResource("NoChromeButton") as Style;
            btnDetailsVols.Click += new RoutedEventHandler(btnDetailsVols_Click);
            btnDetailsVols.MouseEnter += new MouseEventHandler(EC.btn_MouseEnter);
            btnDetailsVols.MouseLeave += new MouseEventHandler(EC.btn_MouseLeave);
            btnDetailsVols.Height = 30;
            btnDetailsVols.Width = 30;
            btnDetailsVols.HorizontalAlignment = HorizontalAlignment.Right;
            btnDetailsVols.Margin = new Thickness(0, 7, 10, 0);
            btnDetailsVols.ToolTip = "Détails";
            Grid.SetRow(btnDetailsVols, 0);


            GrdVol.Children.Add(E);
            GrdVol.Children.Add(imgEtat);
            GrdVol.Children.Add(lblNumVol);
            GrdVol.Children.Add(lblPiste);
            GrdVol.Children.Add(lblDelais);
            GrdVol.Children.Add(btnDetailsVols);
        }


        /// <summary>
        /// Partie du bas (affichée avec le bouton de détails).
        /// </summary>
        private void AfficherDetailsVolsB()
        {
            string atterrissage;
            string estime;


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


            ComboBoxItem cmbItem = new ComboBoxItem() { Content = "Aucune piste" };
            cboPistes.Height = 25;
            cboPistes.Width = 100;
            cboPistes.ToolTip = "Choisissez une piste";
            cboPistes.VerticalAlignment = VerticalAlignment.Top;
            cboPistes.HorizontalAlignment = HorizontalAlignment.Left;
            cboPistes.Margin = new Thickness(14, 30, 0, 0);
            cboPistes.Items.Add(cmbItem);

            if (vol.PisteAssigne == null)
                cboPistes.SelectedIndex = 0;

            else
                cboPistes.SelectedIndex = vol.PisteAssigne.NumPiste;

            cboPistes.SelectionChanged += cboPistes_Selection;

            if (vol.EstAtterrissage)
                cboPistes.Name = "cbo" + Etat.Atterrissage.ToString() + vol.IdVol.ToString();

            else
                cboPistes.Name = "cbo" + Etat.Decollage.ToString() + vol.IdVol.ToString();

            for (int i = 0; i < EC.LstPistes.Count; i++)
            {
                ComboBoxItem cmbItemPiste = new ComboBoxItem() { Content = "Piste #" + (i + 1) };
                cboPistes.Items.Add(cmbItemPiste);
                ChangerEtatPiste(EC.LstPistes[i]);
            }
                

            if (EC.Controleur == null)
                cboPistes.IsEnabled = false;

            Grid.SetRow(cboPistes, 1);

            lblNumVol.Content = "Vol " + vol.NumeroVol + (vol.EstAtterrissage ? " ▼ " : " ▲ " ) + (cboPistes.SelectedIndex == 0 ? "" : cboPistes.Text);
            lblNumVol.Height = 30;
            lblNumVol.Width = 130;
            lblNumVol.HorizontalAlignment = HorizontalAlignment.Center;
            lblNumVol.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(lblNumVol, 0);


            Label lblEstime = new Label();
            lblEstime.Content = estime + vol.DateVol.ToString("HH:mm:ss");
            lblEstime.Height = 30;
            lblEstime.VerticalAlignment = VerticalAlignment.Top;
            lblEstime.HorizontalAlignment = HorizontalAlignment.Left;
            lblEstime.Margin = new Thickness(10, 60, 0, 0);
            Grid.SetRow(lblEstime, 1);


            GrdVol.Children.Add(R);
            GrdVol.Children.Add(lblAtterrissage);
            GrdVol.Children.Add(cboPistes);
            GrdVol.Children.Add(lblEstime);
        }


        /// <summary>
        /// Teste si la piste est occupé par une autre avion ou pas.
        /// </summary>
        private void TesterPiste()
        {
            // On parcourt la liste de vol.
            for (int i = 0; i < EC.LstVols.Count; i++)
            {
                // Si le vol dans la liste est différent du vol du UserControl.
                if (EC.LstVols[i] != vol)
                {
                    // On prend la différence entre ces deux vols.
                    TimeSpan t = vol.DateVol - EC.LstVols[i].DateVol;

                    // Si le TimeSpan est négatif on le rend positif.
                    if (t.TotalMilliseconds < 0)
                        t = t.Negate();

                    // Si le TimeSpan est plus petit que 30 secondes.
                    if (t.TotalMilliseconds < 30000)
                    {
                        // Pour chaque UserControl dans la liste.
                        for (int j = 0; j < EC.LstUserControlVols.Count; j++)
                        {
                            // Si le vol du UserControl est égal au vol dans la liste et que la piste du vol est assigné.
                            if (EC.LstUserControlVols[j].vol == EC.LstVols[i] && vol.PisteAssigne != null)
                            {
                                // On va chercher l'item du ComboBox correspondant à la piste et on change sa couleur.
                                ComboBoxItem cboI = EC.LstUserControlVols[j].cboPistes.Items[vol.PisteAssigne.NumPiste] as ComboBoxItem;

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


        /// <summary>
        /// Selon l'état de la piste, on désactive l'item du ComboBox.
        /// </summary>
        /// <param name="piste"> Piste à rendre disponible ou indisponible. </param>
        public void ChangerEtatPiste(Piste piste)
        {
            if (piste.estDisponible)
            {
                ((ComboBoxItem)cboPistes.Items[piste.NumPiste]).IsEnabled = true;
                ((ComboBoxItem)cboPistes.Items[piste.NumPiste]).Focusable = true;
            }

            else
            {
                ((ComboBoxItem)cboPistes.Items[piste.NumPiste]).IsEnabled = false;
                ((ComboBoxItem)cboPistes.Items[piste.NumPiste]).Focusable = false;

                if (vol.PisteAssigne == piste && vol.Delais.TotalMilliseconds > vol.TempsFinal)
                    RemettreVolAttente(true);
            }
        }


        /// <summary>
        /// Trouve l'image correspondant à l'état d'un vol.
        /// </summary>
        /// <param name="A"> État du vol. </param>
        /// <returns> Retourne l'image en question. </returns>
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

                case Etat.Retarde:
                    b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/exclamation.png");
                    break;

                case Etat.Cancelle:
                    b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/x.png");
                    break;

                default:
                    break;
            }

            b.EndInit();

            return b;
        }


        /// <summary>
        /// Teste et change l'état du vol.
        /// </summary>
        private void TesterEtat()
        {
            // Si le vol entre dans la zone critique.
            if (vol.Delais.TotalMilliseconds < (vol.TempsCritque + vol.TempsFinal))
            {
                // S'il est toujours en attente ou qu'il est critique.
                if (vol.EtatVol == Etat.Attente || vol.EtatVol == Etat.Critique)
                {
                    vol.EtatVol = Etat.Critique;

                    GrdVol.Background = new SolidColorBrush(Color.FromRgb(251, 144, 94)); // Orange

                    // Faire clignoter l'image.
                    if (imgEtat.Source == null)
                        imgEtat.Source = TrouverEtat(vol.EtatVol);

                    else
                        imgEtat.Source = null;

                    return;
                }

                // S'il est retardé.
                else if (vol.EtatVol == Etat.Retarde)
                {
                    GrdVol.Background = new SolidColorBrush(Color.FromRgb(255, 66, 66)); // Rouge

                    // Faire clignoter l'image.
                    if (imgEtat.Source == null)
                        imgEtat.Source = TrouverEtat(vol.EtatVol);

                    else
                        imgEtat.Source = null;

                    return;
                }
            }

            if (vol.EtatVol == Etat.Atterrissage || vol.EtatVol == Etat.Decollage)
            {
                imgEtat.Height = 37;
                imgEtat.Margin = new Thickness(12, 0, 0, 0);
            }

            // On change la source de l'image selon l'état.
            imgEtat.Source = TrouverEtat(vol.EtatVol);
        }


        /// <summary>
        /// Teste si tous les hangars au sol sont pris. Si oui il empêche les vols d'atterrir.
        /// </summary>
        private void TesterHangar()
        {
            int compteur = 0;

            foreach(Hangar hangar in EC.LstHangar)
                if (!hangar.EstDisponible)
                    compteur++;

            if (compteur == EC.LstHangar.Count && vol.EstAtterrissage)
            {
                cboPistes.IsEnabled = false;

                if (vol.Delais.TotalMilliseconds > vol.TempsFinal)
                    RemettreVolAttente(true);
            }
        }
         

        /// <summary>
        /// Retarde le vol correspondant.
        /// </summary>
        /// <param name="millisecondes"> Temps en millisecondes du retard. </param>
        public void RetarderVol(int millisecondes)
        {
            // Piste null + combo box null + etat retarder
            vol.PisteAssigne = null;
            cboPistes.SelectedIndex = 0;
            vol.EtatVol = Etat.Retarde;

            // On retarde le vol selon le nombre de millisecondes.
            vol.DateVol = vol.DateVol.AddMilliseconds(millisecondes);
            vol.Delais += TimeSpan.FromMilliseconds(millisecondes);

            EC.RafraichirVols();
        }


        /// <summary>
        /// Annule le vol correspondant.
        /// </summary>
        public void AnnulerVol()
        {
            // Etat annuler
            vol.EtatVol = Etat.Cancelle;

            // On retarde le vol selon le nombre de millisecondes.
            vol.DateVol = DateTime.Now.AddMilliseconds(500);
            vol.Delais = vol.DateVol - DateTime.Now;
        }


        /// <summary>
        /// Modifie le label qui affiche le délai.
        /// </summary>
        private void ChangerLabelDelais()
        {
            string separateur;

            if (vol.Delais.Seconds < 10)
                separateur = " : 0";

            else
                separateur = " : ";

            lblDelais.Content = "Dans 0" + vol.Delais.Minutes.ToString() + separateur + vol.Delais.Seconds.ToString();
            lblDelais.FontSize = 14;
            lblDelais.FontWeight = FontWeights.Bold;
        }


        /// <summary>
        /// Selon le délai on change l'état du vol, le délai, l'image, etc.
        /// </summary>
        private void ChangerUserControl()
        {
            // Changer quand le vol est terminée.
            if (vol.Delais.TotalMilliseconds < 0 && (vol.EtatVol == Etat.Assigne || vol.EtatVol == Etat.Cancelle))
            {
                GrdVol.Children.Remove(btnDetailsVols);
                GrdVol.Height = 60;
                GrdVol.Background = Brushes.LightGray;
                vol.PisteAssigne = null;

                lblDelais.FontSize = 12;
                lblDelais.FontWeight = FontWeights.Normal;
                lblDelais.Width = 170;
                lblDelais.Margin = new Thickness(58, 0, 0, 0);


                if (vol.EtatVol != Etat.Cancelle && vol.PisteHistorique != null)
                {
                    if (vol.EstAtterrissage)
                    {
                        lblDelais.Content = "Atterrit sur la piste " + vol.PisteHistorique.NumPiste + " à " + vol.DateVol.ToString("HH:mm");
                        vol.EtatVol = Etat.Atterrissage;
                    }

                    else
                    {
                        lblDelais.Content = "Décolle sur la piste " + vol.PisteHistorique.NumPiste + " à " + vol.DateVol.ToString("HH:mm");
                        vol.EtatVol = Etat.Decollage;
                    }
                }

                else
                    lblDelais.Content = "Le vol a été annulé";


                if (dt500 != null)
                    dt500.Stop();

                if (dtDelais != null)
                    dtDelais.Stop();
            }

            // Si le vol entre dans la zone finale.
            else if (vol.Delais.TotalMilliseconds < vol.TempsFinal + 500)
            {
                if (vol.EtatVol == Etat.Retarde || vol.EtatVol == Etat.Critique)
                    RetarderVol(vol.TempsCritque);

                else
                {
                    ChangerLabelDelais();
                    cboPistes.IsEnabled = false;

                    // Set piste historique
                    vol.PisteHistorique = vol.PisteAssigne;
                }
            }

            // Changer les secondes.
            else if (vol.Delais.TotalMilliseconds < 5 * 60000 + 1000)
                ChangerLabelDelais();


            // Changer les minutes.
            else
                lblDelais.Content = "Dans " + (vol.Delais.Minutes + (vol.Delais.Hours * 60) + 1).ToString() + " minutes";


            // Démarrer les animations au bon moment.
            if (vol.Delais.TotalMilliseconds < 15000 && vol.EstAtterrissage && !vol.EstLance)
            {
                // Empêche l'application de crash quand la piste est null.
                try
                {
                    AnimVol.DemarreAtterrissage(vol.PisteHistorique.NumPiste);
                    vol.EstLance = true;
                }

                catch(Exception e) { }
            }


            if (vol.Delais.TotalMilliseconds < 30000 && !vol.EstAtterrissage && !vol.EstLance)
            {
                // Empêche l'application de crash quand la piste est null.
                try
                {
                    AnimVol.DemarreDecollage(vol.PisteHistorique.NumPiste);
                    vol.EstLance = true;
                }

                catch (Exception e) { }
            }
        }


        /// <summary>
        /// Remet le vol en attente.
        /// </summary>
        /// <param name="index"> Si vrai, remet l'index du combobox à 0, sinon ne change rien. </param>
        private void RemettreVolAttente(bool index)
        {
            imgEtat.Source = TrouverEtat(Etat.Attente);
            imgEtat.Margin = new Thickness(10, 0, 0, 0);

            if(index)
                cboPistes.SelectedIndex = 0;

            vol.EtatVol = Etat.Attente;

            TesterPiste();

            vol.PisteAssigne = null;

            TesterEtat();
        }


        #endregion


        // ---------------------------------------------------------------------------------- \\
        #region Méthodes Éléments
        
        
        /// <summary>
        /// Appelé lorsqu'on clique sur le bouton détails du UserControl.
        /// </summary>
        private void btnDetailsVols_Click(object sender, RoutedEventArgs e)
        {
            if (GrdVol.Height == 60)
                GrdVol.Height = Double.NaN;

            else
                GrdVol.Height = 60;
        }


        /// <summary>
        /// Appelé lorsqu'on change la sélection d'une piste.
        /// </summary>
        private void cboPistes_Selection(object sender, SelectionChangedEventArgs e)
        {
            RemettreVolAttente(false);


            // Si la piste n'est pas disponible et qu'on veut la forcer.
            ComboBoxItem cboI = cboPistes.Items[cboPistes.SelectedIndex] as ComboBoxItem;

            if (cboI.Foreground == Brushes.Red)
            {
                EcranConfirmation eC = new EcranConfirmation(EC.Controleur);

                if (eC.ShowDialog() != true)
                    cboPistes.SelectedIndex = 0;
            }

            // Si l'utilisateur choisi une piste. On change l'état a ASSIGNE.
            if (cboPistes.SelectedIndex > 0)
            {
                imgEtat.Source = TrouverEtat(Etat.Assigne);
                imgEtat.Margin = new Thickness(9, 0, 0, 0);

                if (vol.EstAtterrissage)
                    GrdVol.Background = Brushes.LightBlue;

                else
                    GrdVol.Background = Brushes.LightGreen;

                vol.EtatVol = Etat.Assigne;
                vol.PisteAssigne = EC.LstPistes[cboPistes.SelectedIndex - 1];
            }

            // Sinon on remet l'état a ATTENTE.
            else
                cboPistes.SelectedIndex = 0;

            TesterPiste();

            lblNumVol.Content = "Vol " + vol.NumeroVol + (vol.EstAtterrissage ? " ▼ " : " ▲ ") + (cboPistes.SelectedIndex == 0 ? "" : cboI.Content);
        }


        /// <summary>
        /// Appelé à chaque demie seconde.
        /// </summary>
        private void dt_Tick(object sender, EventArgs e)
        {
            ChangerUserControl();
            TesterHangar();
            TesterEtat();
            TesterPiste();
        }


        /// <summary>
        /// Appelé a chaque seconde.
        /// </summary>
        private void dtDelais_Tick(object sender, EventArgs e)
        {
            // Diminue le délais en fonction de l'accelerateur (défaut = 1).
            vol.Delais -= TimeSpan.FromMilliseconds(Accelerateur * 1000);
        }


        /// <summary>
        /// Appelé à chaque fois que le UserControl est supprimé.
        /// </summary>
        public void Dispose()
        {
            // Arrête les DispatcherTimer.
            dt500.Stop();
            dtDelais.Stop();
        }


        #endregion

    }
}
