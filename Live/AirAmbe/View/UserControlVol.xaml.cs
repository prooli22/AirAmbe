﻿using System;
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
    /// Logique d'interaction pour UserControlVol.xaml
    /// </summary>
    public partial class UserControlVol : UserControl, IDisposable
    {
        private EcranControleur EC { get; set; }

        public Vol vol { get; set; }

        private Grid GrdVol { get; set; }

        private int compteurVol;

        private DispatcherTimer dtStart;

        private DispatcherTimer dt500;

        private DispatcherTimer dt1000;

        private Image imgEtat = new Image();

        private Label lblNumVol = new Label();

        private Label lblDelais = new Label();

        private Button btnDetailsVols = new Button();

        private ComboBox cboPistes = new ComboBox();

       


        /// <summary>
        /// Constructeur du UserControlVol
        /// </summary>
        /// <param name="ec"></param>
        /// <param name="volEC"></param>
        public UserControlVol(EcranControleur ec, Vol volEC, int compteur)
        {
            InitializeComponent();


            EC = ec;
            vol = volEC;
            compteurVol = compteur;

            AfficherDetailsVols();
            AfficherDetailsVolsH();
            AfficherDetailsVolsB();
            //TesterEtat();
            //TesterPiste();

            dtStart = new DispatcherTimer();
            dtStart.Tick += dt_Tick;
            dtStart.Interval = TimeSpan.FromMilliseconds(1);
            dtStart.Start();

            dt500 = new DispatcherTimer();
            dt500.Tick += dt_Tick;
            dt500.Interval = TimeSpan.FromMilliseconds(500);

            dt1000 = new DispatcherTimer();
            dt1000.Tick += dt_Tick;
            dt1000.Interval = TimeSpan.FromMilliseconds(1000);
        }


        // ---------------------------------------------------------------------------------- \\


        private void AfficherDetailsVols()
        {
            Border brdVol = new Border();
            brdVol.Height = Double.NaN;
            brdVol.BorderBrush = Brushes.Black;
            brdVol.BorderThickness = new Thickness(2);
            brdVol.Margin = new Thickness(10);


            GrdVol = new Grid();
            GrdVol.Name = "GrdVol" + vol.IdVol.ToString();
            GrdVol.Height = 60;
            RowDefinition grdRow1 = new RowDefinition();
            RowDefinition grdRow2 = new RowDefinition();
            grdRow1.Height = GridLength.Auto;
            grdRow2.Height = GridLength.Auto;
            GrdVol.RowDefinitions.Add(grdRow1);
            GrdVol.RowDefinitions.Add(grdRow2);

            if (vol.EstAtterrissage)
                GrdVol.Background = Brushes.LightBlue;

            else
                GrdVol.Background = Brushes.LightGreen;

            brdVol.Child = GrdVol;

            Grid.SetRow(brdVol, compteurVol);
            EC.grdProchainsVols.Children.Add(brdVol);
        }


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


            lblNumVol.Content = "Vol " + vol.NumeroVol;
            lblNumVol.Height = 30;
            lblNumVol.Width = 110;
            lblNumVol.HorizontalAlignment = HorizontalAlignment.Center;
            lblNumVol.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(lblNumVol, 0);

            lblDelais.Name = "lblDelais" + vol.IdVol.ToString();
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
            Grid.SetRow(btnDetailsVols, 0);


            GrdVol.Children.Add(E);
            GrdVol.Children.Add(imgEtat);
            GrdVol.Children.Add(lblNumVol);
            GrdVol.Children.Add(lblDelais);
            GrdVol.Children.Add(btnDetailsVols);
        }


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
                cboPistes.Items.Add(new ComboBoxItem() { Content = "Piste #" + (i + 1) });

            if (EC.Controleur == null)
                cboPistes.IsEnabled = false;

            Grid.SetRow(cboPistes, 1);

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


        // À TERMINER.
        private void TesterPiste()
        {
            for (int i = 0; i < EC.LstVols.Count; i++)
            {
                if (EC.LstVols[i] != vol)
                {
                    TimeSpan t = EC.LstVols[i].DateVol - vol.DateVol;

                    if (t.TotalMilliseconds <= 30000 && t.TotalMilliseconds >= -30000)
                    {
                        if (vol.PisteAssigne != null)
                        {
                            ComboBoxItem cboI = cboPistes.Items[vol.PisteAssigne.NumPiste] as ComboBoxItem;

                            if (vol.EtatVol == Etat.Assigne)
                                cboI.Foreground = Brushes.Red;

                            else
                                cboI.Foreground = Brushes.Black;
                        }
                    }
                }
            }
        }


        public void ChangerEtatPiste(Piste piste)
        {
            ComboBoxItem cboI = cboPistes.Items[piste.NumPiste] as ComboBoxItem;

            if (piste.estDisponible)
                cboI.IsEnabled = true;
            
            else
            {
                cboI.IsEnabled = false;

                if(vol.PisteAssigne.NumPiste == piste.NumPiste)
                    vol.PisteAssigne = null;
                
            }
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


        private void TesterEtat()
        {
            if (vol.Delais.TotalMilliseconds < vol.TempsCritque + vol.TempsFinal)
            {
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

            imgEtat.Source = TrouverEtat(vol.EtatVol);
        }
         

        public void RetarderVol(int millisecondes)
        {
            // Piste null + combo box null + etat retarder
            vol.PisteAssigne = null;
            cboPistes.SelectedIndex = 0;
            vol.EtatVol = Etat.Retarde;

            // On retarde le vol selon le nombre de millisecondes.
            vol.DateVol = vol.DateVol.AddMilliseconds(millisecondes);
        }


        public void AnnulerVol()
        {
            // Etat annuler
            vol.EtatVol = Etat.Cancelle;

            // On retarde le vol selon le nombre de millisecondes.
            vol.DateVol = DateTime.Now.AddMilliseconds(500);
        }


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


        private void ChangerUserControl()
        {
            //if(vol.PisteAssigne != null)
            //    System.Diagnostics.Trace.TraceInformation("Vol : " + vol.IdVol + "     Piste : " + vol.PisteAssigne.NumPiste.ToString());

            //else
            //    System.Diagnostics.Trace.TraceInformation("Vol : " + vol.IdVol + "     Piste : null");

            

            // On test le delais.
            vol.Delais = vol.DateVol - DateTime.Now;

            // Changer quand le vol est terminée.
            if (vol.Delais.TotalMilliseconds < 1000/* && vol.Delais.TotalMilliseconds >= 0*/)
            {
                GrdVol.Children.Remove(btnDetailsVols);
                GrdVol.Height = 60;
                GrdVol.Background = Brushes.LightGray;
                vol.PisteAssigne = null;

                lblDelais.FontSize = 12;
                lblDelais.FontWeight = FontWeights.Normal;


                if(vol.EtatVol != Etat.Cancelle)
                {
                    if (vol.EstAtterrissage)
                    {
                        EC.Anim = new Animation(EC);
                        EC.Anim.DemarreAtterrissage(vol.PisteHistorique.NumPiste);

                        lblDelais.Content = "Atterrit sur la piste " + vol.PisteHistorique.NumPiste + " à " + vol.DateVol.ToString("HH:mm");
                        lblDelais.Width = 170;
                        lblDelais.Margin = new Thickness(58, 0, 0, 0);

                        vol.EtatVol = Etat.Atterrissage;
                    }

                    else
                    {
                        EC.Anim = new Animation(EC);
                        EC.Anim.DemarreDecollage(vol.PisteHistorique.NumPiste);

                        lblDelais.Content = "Décolle sur la piste " + vol.PisteHistorique.NumPiste + " à " + vol.DateVol.ToString("HH:mm");
                        lblDelais.Width = 170;
                        lblDelais.Margin = new Thickness(58, 0, 0, 0);

                        vol.EtatVol = Etat.Decollage;
                    }
                }

                else
                {
                    lblDelais.Content = "Le vol a été annulé";
                    lblDelais.Width = 170;
                    lblDelais.Margin = new Thickness(58, 0, 0, 0);
                }

                dt500.Stop();
                dt1000.Stop();
                dtStart.Stop();
            }

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
            {
                ChangerLabelDelais();
                dt1000.Stop();
                dtStart.Stop();

                dt500.Start();
            }

            // Changer les minutes.
            else
            {
                lblDelais.Content = "Dans " + (vol.Delais.Minutes + (vol.Delais.Hours * 60) + 1).ToString() + " minutes";
                dtStart.Stop();

                dt1000.Start();
            }
        }


        // ---------------------------------------------------------------------------------- \\


        private void btnDetailsVols_Click(object sender, RoutedEventArgs e)
        {
            if (GrdVol.Height == 60)
                GrdVol.Height = Double.NaN;

            else
                GrdVol.Height = 60;
        }


        // À REVOIR.
        private void cboPistes_Selection(object sender, SelectionChangedEventArgs e)
        {
            // Si la piste n'est pas disponible et qu'on veut la forcer.
            ComboBoxItem cboI = cboPistes.Items[cboPistes.SelectedIndex] as ComboBoxItem;

            if (cboI.Foreground == Brushes.Red)
            {
                EcranConfirmation eC = new EcranConfirmation(EC.Controleur);
                eC.Show();
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
                TesterPiste();
            }

            // Sinon on remet l'état a ATTENTE.
            else
            {
                imgEtat.Source = TrouverEtat(Etat.Attente);
                imgEtat.Margin = new Thickness(10, 0, 0, 0);

                vol.EtatVol = Etat.Attente;
                TesterPiste();
                vol.PisteAssigne = null;
                TesterEtat();
            }
        }


        private void dt_Tick(object sender, EventArgs e)
        {
            ChangerUserControl();
            TesterEtat();
            TesterPiste();
        }


        public void Dispose()
        {
            //throw new NotImplementedException();
            dtStart.Stop();
            dt500.Stop();
            dt1000.Stop();

        }
    }
}
