﻿using AirAmbe.Model;

using AirAmbe.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        public List<UserControlVol> LstUserControlVols { get; set; }

        public Animation Anim { get; set; }



        /// <summary>
        /// Constructeur de la fênetre du contrôleur.
        /// </summary>
        /// <param name="U"> Un contrôleur </param>
        public EcranControleur(Utilisateur U)
        {
            InitializeComponent();
            Controleur = U;
            LstUserControlVols = new List<UserControlVol>();
            
            

            if (Controleur == null)
                btnProfil.Visibility = Visibility.Hidden;


            if (ChargerScenarios())
            {
                if(LstScenarios.Count > 0)
                {
                    ChargerVols();
                    ModifierHeures();
                    ChargerDataGrid();
                    ChargerProchainsVols();

                    Anim = new Animation(this);
                    Anim.DemarreAtterrissage(1);

                    Anim = new Animation(this);
                    Anim.DemarreAtterrissage(2);

                    Anim = new Animation(this);
                    Anim.DemarreAtterrissage(3);

                    Anim = new Animation(this);
                    Anim.DemarreAtterrissage(4);

                    Anim = new Animation(this);
                    Anim.DemarreAtterrissage(5);

                    Anim = new Animation(this);
                    Anim.DemarreDecollage(1);


                    //FacteursExterieurs.StartTimer(this);
                    //Anim = new Animation(this);
                    //Anim.DemarreAtterrissage(1);
                    //Anim = new Animation(this);
                    //Anim.DemarreAtterrissage(2);
                }

                Anim = new Animation(this);
                Anim.DessinerHangar();
                Anim.GererDessinPiste(LstPistes.Count);
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

            if (scenarios.Length == 1)
                return true;

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
                LstVols[i].DateVol = LstVols[i].DateVol.AddMilliseconds(LstVols[i].TEMPSFINAL + (LstVols[i].Intervalle * 1000));

                // Met les secondes à 00
                if (LstVols[i].DateVol.Second != 0)
                    //LstVols[i].DateVol = LstVols[i].DateVol.AddSeconds(-LstVols[i].DateVol.Second);

                // Ajoute 2 minutes entre chaque scénario
                if (i > 0 && (LstVols[i].NumScenario != LstVols[i - 1].NumScenario))
                {
                    LstVols[i].DateVol = LstVols[i - 1].DateVol.AddMinutes(2);
                    nbVols = 1;
                }

                // Ajoute le temps de l'intervalle pour un scénario
                LstVols[i].DateVol = LstVols[i].DateVol.AddSeconds(LstVols[i].Intervalle * nbVols);
                nbVols++;

                LstVols[i].DatePrevu = LstVols[i].DateVol;
                LstVols[i].Delais = LstVols[i].DateVol - DateTime.Now;
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
                LstVols[i].IdVol = i + 1;

                if (i < 10)
                {
                    RowDefinition gridRow = new RowDefinition();
                    gridRow.Height = GridLength.Auto;
                    grdProchainsVols.RowDefinitions.Add(gridRow);

                    LstUserControlVols.Add(new UserControlVol(this, LstVols[i], i));
                } 
            }
        }


        public void RafraichirVols()
        {
            ViderListes();

            // On tri la liste des vols.
            LstVols.Sort((a, b) => DateTime.Compare(a.DateVol, b.DateVol));

            // Clear les listes.
            LstAtterrissages.Clear();
            LstDecollages.Clear();

            for (int i = 0; i < LstVols.Count; i++)
            {
                if (LstVols[i].EstAtterrissage)
                    LstAtterrissages.Add(LstVols[i]);

                else
                    LstDecollages.Add(LstVols[i]);

                LstUserControlVols.Add(new UserControlVol(this, LstVols[i], i));
            }

            dgAtterissages.Items.Refresh();
            dgDecollages.Items.Refresh();
        }


        private void FlushVols()
        {
            ViderListes();

            int compteur = 0;

            for (int i = 0; i < LstVols.Count; i++)
            {
                if(LstVols[i].EtatVol != Etat.Atterrissage && LstVols[i].EtatVol != Etat.Decollage)
                {
                    LstUserControlVols.Add(new UserControlVol(this, LstVols[i], compteur));

                    compteur++;
                }

                if (compteur == 10)
                    break;
            }
        }


        private void ViderListes()
        {
            // On vide la grille des prochains vols.
            grdProchainsVols.Children.Clear();

            // Stop les timers.
            for (int i = 0; i < LstUserControlVols.Count; i++)

                LstUserControlVols[i].Dispose();

            // On vide la liste des UserControlVols.
            LstUserControlVols.Clear();
        }


        public void RetarderVol(int idVol, int millisecondes)
        {
            for (int i = 0; i < LstUserControlVols.Count; i++)
            {
                if(LstUserControlVols[i].vol.IdVol == idVol)
                {
                    LstUserControlVols[i].RetarderVol(millisecondes);
                }
            }

            RafraichirVols();
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
            RafraichirVols();
            FlushVols();
        }


        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            EcranConnexion eCon = new EcranConnexion();
            this.Close();
            eCon.Show();
        }


        public void btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Height += 2;
            btn.Width += 2;
        }


        public void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Height -= 2;
            btn.Width -= 2;
        }


        private void cnvCarte_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            const double GROSSISSEMENTEXTERIEUR = 4.1;
            const double GROSSISSEMENTINTERIEUR = 1;

            zoom.ScaleX = 1;
            zoom.ScaleY = 1;


            if (e.Delta > 0)
            {

                zoom.ScaleX *= GROSSISSEMENTINTERIEUR;
                zoom.ScaleY *= GROSSISSEMENTINTERIEUR;
            }
            else
            {

                zoom.ScaleX /= GROSSISSEMENTEXTERIEUR;
                zoom.ScaleY /= GROSSISSEMENTEXTERIEUR;
                cnvCarte.Height = 477;
                cnvCarte.Width = 927;
            }

            cnvCarte.LayoutTransform = new ScaleTransform(zoom.ScaleX, zoom.ScaleY);

        }


    }
}