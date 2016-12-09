// Nom : Olivier Provost
// Date : 2016-12-09


using AirAmbe.Model;
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
    /// Écran du Centre de Contrôle.
    /// </summary>
    public partial class EcranControleur : Window
    {
        // Propriétés.
        public Utilisateur Controleur { get; set; }

        public List<Vol> LstVols { get; set; }

        public List<Vol> LstDecollages { get; set; }

        public List<Vol> LstAtterrissages { get; set; }

        public List<Scenario> LstScenarios { get; set; }

        public List<Piste> LstPistes { get; set; }

        public List<UserControlVol> LstUserControlVols { get; set; }

        public List<Hangar> LstHangar { get; set; }

        public List<Avion> LstAvion { get; set; }

        public Animation Anim { get; set; }

        public float Accelerateur { get; set; } = 1;


        // Variables.
        private DispatcherTimer dtRefresh;

        private EcranConfiguration ec;
        EcranGuide eg;


        /// <summary>
        /// Constructeur de la fênetre du contrôleur.
        /// </summary>
        /// <param name="U"> Un contrôleur </param>
        public EcranControleur(Utilisateur U)
        {
            InitializeComponent();
            Controleur = U;
            

            // Si connextion en tant qu'observateur.
            if (Controleur == null)
            {
                btnConfig.Visibility = Visibility.Hidden;
                btnProfil.Visibility = Visibility.Hidden;
            }


            // Si le fichier de scénarios est présent on charge tout ce qu'on a de besoin.
            if (ChargerScenarios())
            {
                if(LstScenarios.Count > 0)
                {
                    ChargerVols();
                    InitialiserHangar();
                    InitialiserAvion();
                    ModifierHeures();
                    ChargerDataGrid();
                    ChargerProchainsVols();

                    Anim = new Animation(this);
                    Anim.DisperserDecollageHangar();

                    dtRefresh = new DispatcherTimer();
                    dtRefresh.Interval = TimeSpan.FromSeconds(1);
                    dtRefresh.Tick += dtRefresh_Tick;
                    dtRefresh.Start();
                }

                Anim = new Animation(this);
                Anim.DessinerHangar();
                Anim.GererDessinPiste(LstPistes.Count);            
            }

            // Sinon on affiche un message à l'écran.
            else
            {
                MessageBox.Show("Le fichier de scénarios n'est pas présent ou encore il est vide. Veuillez contacter l'administrateur de l'application pour remédier au problème.", "Air-Ambe", MessageBoxButton.OK, MessageBoxImage.Error);
                btnProfil.Visibility = Visibility.Hidden;
                btnRefresh.Visibility = Visibility.Hidden;
                btnConfig.Visibility = Visibility.Hidden;
            }
        }


        /// <summary>
        /// Lorsqu'on ferme l'écran on veut que l'écran de configuration aussi se ferme.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if(ec != null)
                ec.Close();

            if (eg != null)
                eg.Close();
        }

        // ---------------------------------------------------------------------------------- \\
        #region Méthodes Maisons


        /// <summary>
        /// Lis le fichier scenarios.txt et charge les vols en conséquence à partir de la BD.
        /// </summary>
        /// <returns> Retourne vrai si le fichier est présent, sinon retourne faux. </returns>
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
            for (int i = 0; i < 5; i++)
            {
                LstPistes.Add(new Piste());
                LstPistes[i].NumPiste = i + 1;
            }

            if (scenarios.Length == 1)
                return true;

            for (int i = 1; i < scenarios.Length; i++)
            {
                Scenario S = new Scenario();

                // Get le temps de retard.
                S.TempsRetard = Int32.Parse(scenarios[0]);

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


        /// <summary>
        /// Charge les vols présent dans le fichier de scénarios et charge la LstVols.
        /// </summary>
        private void ChargerVols()
        {
            LstVols = new List<Vol>();
            VolAS VA = new VolAS();

            for (int i = 0; i < LstScenarios.Count; i++)
            {
                for (int j = 0; j < LstScenarios[i].NumVol.Count(); j++)
                {
                    // Initialisé le vol et les propriétés de temps.
                    Vol V = VA.Recuperer(LstScenarios[i].NumVol[j]);
                    V.Intervalle = LstScenarios[i].Intervalle;
                    V.NumScenario = i + 1;
                    V.TempsCritque = LstScenarios[i].TempsRetard * 1000;
                    V.TempsFinal = V.TempsCritque * 2;
                    LstVols.Add(V);
                }
            }
        }


        /// <summary>
        /// Modifie les heures prévues de décollages et/ou d'atterrissage de chaque vol.
        /// </summary>
        private void ModifierHeures()
        {
            int nbVols = 1;


            for (int i = 0; i < LstVols.Count; i++)
            {
                LstVols[i].DateVol = LstVols[i].DateVol.AddMilliseconds(LstVols[i].TempsFinal + (LstVols[i].Intervalle * 1000));

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


        /// <summary>
        /// Créer la grille de données des atterrissages et des décollages.
        /// </summary>
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

            // DataGrid des pistes.
            dgPistes.ItemsSource = LstPistes;
        }


        /// <summary>
        /// Créer la grille des prochains vols avec les 10 vols les + récents.
        /// Avec les UserControlVols.
        /// </summary>
        private void ChargerProchainsVols()
        {
            LstUserControlVols = new List<UserControlVol>();

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


        /// <summary>
        /// Créer la liste des hangars disponibles (Anthony).
        /// </summary>
        public void InitialiserHangar()
        {
            LstHangar = new List<Hangar>();

            //Pour chaque hangar on donne un numero et une disponibilité
            for (int i = 0; i < 12; i++)
            {
                LstHangar.Add(new Hangar());
                LstHangar[i].NumHangar = i + 1;
                LstHangar[i].EstDisponible = true;
            }
        }


        /// <summary>
        /// Créer la liste des avions selon le nombre de décollage et d'atterrissage (Anthony).
        /// </summary>
        public void InitialiserAvion()
        {
            LstAvion = new List<Avion>();
            int nbDecollage=0;
     
            for (int i = 0; i < LstVols.Count; i++)
            {
                if (LstVols[i].EstAtterrissage==false)
                {
                    nbDecollage++;
                }
            }

            if (LstVols.Count() > 12)
            {
                nbDecollage = (nbDecollage / 2) + 1;
            }
        
            //Pour chaque avion on donne un numero et une disponibilité        
            for (int i = 0; i < nbDecollage; i++)
            {
                LstAvion.Add(new Avion());
                LstAvion[i].NumAvion = i + 1;
                LstAvion[i].EstDisponibleAtterrissage = false;
                LstAvion[i].EstDisponibleDecollage = true;
            }

          
            for (int i = nbDecollage; i < LstVols.Count(); i++)
            {
                LstAvion.Add(new Avion());
                LstAvion[i].NumAvion = i + 1;
                LstAvion[i].EstDisponibleAtterrissage = true;
                LstAvion[i].EstDisponibleDecollage = false;
            }
        }


        /// <summary>
        /// Met à jour les données de la liste des vols, la remet en ordre du plus récent et affiche les prochains vols.
        /// </summary>
        public void RafraichirVols()
        {
            // On vide la grille des prochains vols.
            grdProchainsVols.Children.Clear();

            // Stop les timers.
            for (int i = 0; i < LstUserControlVols.Count; i++)
                LstUserControlVols[i].Dispose();

            // On vide la liste des UserControlVols.
            LstUserControlVols.Clear();

            // On tri la liste des vols.
            LstVols.Sort((a, b) => DateTime.Compare(a.DateVol, b.DateVol));

            int compteur = 0;

            // On recréer la liste des prochains vols (10).
            for (int i = 0; i < LstVols.Count; i++)
            {
                if (LstVols[i].EtatVol != Etat.Atterrissage && LstVols[i].EtatVol != Etat.Decollage && LstVols[i].EtatVol != Etat.Cancelle)
                {
                    LstUserControlVols.Add(new UserControlVol(this, LstVols[i], compteur));

                    compteur++;
                }

                if (compteur == 10)
                    break;
            }
        }


        /// <summary>
        /// Met à jour la grille de données des atterrissages et des décollages.
        /// </summary>
        public void RafaichirListe()
        {
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
            }

            dgAtterissages.Items.Refresh();
            dgDecollages.Items.Refresh();
        }


        /// <summary>
        /// Retarde le vol selon le nombre de millisecondes.
        /// </summary>
        /// <param name="idVol"> id du vol à retarder. </param>
        /// <param name="millisecondes"> Temps en millisecondes. </param>
        public void RetarderVol(int idVol, int millisecondes)
        {
            for (int i = 0; i < LstUserControlVols.Count; i++)
            {
                // On trouve le UserControl qui contient le bon vol.
                if(LstUserControlVols[i].vol.IdVol == idVol)
                {
                    LstUserControlVols[i].RetarderVol(millisecondes);
                    break;
                }
            }
        }


        /// <summary>
        /// Annule le vol.
        /// </summary>
        /// <param name="idVol"> id du vol à annuler. </param>
        public void AnnulerVol(int idVol)
        {
            for (int i = 0; i < LstUserControlVols.Count; i++)
            {
                // On trouve le UserControl qui contient le bon vol.
                if (LstUserControlVols[i].vol.IdVol == idVol)
                {
                    LstUserControlVols[i].AnnulerVol();
                    break;
                }
            }
        }


        /// <summary>
        /// Change l'état de la piste.
        /// </summary>
        /// <param name="piste"> Piste à changer. </param>
        public void ChangerEtatPiste(Piste piste)
        {
            for (int i = 0; i < LstUserControlVols.Count; i++)
                LstUserControlVols[i].ChangerEtatPiste(piste);
        }


        /// <summary>
        /// Change la vitesse à laquelle les secondes se passent dans le simulateur.
        /// </summary>
        /// <param name="accelerateur"></param>
        public void AccelererTemps(float accelerateur)
        {
            // Pour le délai.
            Accelerateur = accelerateur;

            // Pour les animations.
            for (int i = 0; i < LstUserControlVols.Count; i++)
            {
                LstUserControlVols[i].Accelerateur = Accelerateur;

                LstUserControlVols[i].AnimVol.Vitesse = 4.5F * Accelerateur;
                LstUserControlVols[i].AnimVol.VitesseAeroport = 2 * Accelerateur;
                LstUserControlVols[i].AnimVol.TempsAttentePiste = 4000 / Accelerateur;
            }
        }


#endregion


        // ---------------------------------------------------------------------------------- \\
        #region Méthodes Éléments

        /// <summary>
        /// Appelé lorsqu'on clique sur le bouton Profil et affiche l'écran du profil utilisateur.
        /// </summary>
        private void btnProfil_Click(object sender, RoutedEventArgs e)
        {
            EcranUtilisateur eUser = new EcranUtilisateur(Controleur, false, null);
            this.Close();
            eUser.Show();
        }


        /// <summary>
        /// Appelé lorsqu'on clique sur le bouton Rafraichir et rafraichis la liste des prochains vols.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RafraichirVols();
        }


        /// <summary>
        /// Appelé lorsqu'on clique sur le bouton Déconnexion et quitte l'écran du Contrôleur.
        /// </summary>
        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            EcranConnexion eCon = new EcranConnexion();
            this.Close();
            eCon.Show();
        }


        /// <summary>
        /// Appelé lorsqu'on clique sur le bouton Configuration et ouvre l'écran de Configuration.
        /// </summary>
        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            ec = new EcranConfiguration(this);
            ec.Show();
        }


        /// <summary>
        /// Fonctions qui grossit les boutons on passe notre souris au dessus.
        /// </summary>
        public void btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Height += 2;
            btn.Width += 2;
        }


        /// <summary>
        /// Fonctions qui rappetisse les boutons on passe notre souris au dessus.
        /// </summary>
        public void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Height -= 2;
            btn.Width -= 2;
        }


        /// <summary>
        /// Appelé lorsqu'on utilise la molette de la souris dans le canvas de la carte des pistes (Anthony).
        /// Permet de zoomer et dézoomer dans la carte.
        /// </summary>
        private void cnvCarte_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            const double GROSSISSEMENTEXTERIEUR = 1.6;
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


        /// <summary>
        /// Appelé à chaque seconde pour rafraichir la grille de données des atterrissages et des décollages.
        /// </summary>
        private void dtRefresh_Tick(object sender, EventArgs e)
        {
            RafaichirListe();
        }


        /// <summary>
        /// Lorsqu'on clique sur le bouton Aide, le guide d'utilisateur est ouvert.
        /// </summary>
        private void btnAide_Click(object sender, RoutedEventArgs e)
        {
            //Process process = new Process();
            //process.StartInfo.FileName = "guide.pdf";
            //process.Start();
            //process.WaitForExit();

            eg = new EcranGuide();
            eg.Show();
        }


        /// <summary>
        /// Appelé lorsqu'on clique sur le bouton Recharger, affiche un message de confirmation pour recharger la page.
        /// </summary>
        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            // Demande une confirmation à l'utilisateur avant de quitter.
            MessageBoxResult resultat = MessageBox.Show("Voulez-vous vraiment recharger l'application ?", "Recharger", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (resultat == MessageBoxResult.Yes)
            {
                EcranControleur EC = new EcranControleur(Controleur);
                EC.Show();
                this.Close();
            }
        }

        #endregion

    }
}