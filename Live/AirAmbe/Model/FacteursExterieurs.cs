using AirAmbe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace AirAmbe
{
    public static class FacteursExterieurs
    {
        private static bool pluie = false;
        public static Vent vent { get; set; }

        private static void FacteurAleatoire(EcranControleur ec)
        {
            Random r = new Random();

            int nombre = r.Next(1, 101);
        
            //if (nombre < 20)
            {
                VolRetarde(ec);
            }
            /*else if (nombre < 35)
            {
                PluieTorrentiel(ec);
            }
            else if (nombre < 45)
            {
                PisteIndisponible(ec.LstPistes);
            }*/
        }

        private static void ChangerDirectionVent()
        {

        }

        private static void PisteIndisponible(List<Piste> lstPistes)
        {
            double nbTryMax = lstPistes.Count * 1.5;
            bool tryMax = false;
            int compteur = 0;
            int pisteIndisponible = new Random().Next(0, lstPistes.Count);
            while (!lstPistes[pisteIndisponible].estDisponible)
            {
                pisteIndisponible = new Random().Next(0, lstPistes.Count);
                compteur++;
                if (compteur >= nbTryMax-1)
                {
                    tryMax = true;
                    break;
                }
            }

            if (!tryMax)
            {
                lstPistes[pisteIndisponible].estDisponible = false;

                StartTimerPiste(pisteIndisponible, lstPistes);
                MessageBox.Show("La piste #" + (pisteIndisponible+1)+" est indisponible.");
            }
        }

        private static void StartTimerPiste(int numPiste, List<Piste> lstPistes)
        {
            DispatcherTimer dtPiste = new DispatcherTimer();
            dtPiste.Tick += new EventHandler((sender, e) => tPiste_tick(sender, e, numPiste, lstPistes,dtPiste));
            dtPiste.Interval = new TimeSpan(0, 0, 30);
            dtPiste.Start();

            /*Timer tPiste = new Timer();
            tPiste.AutoReset = false;
            tPiste.Elapsed += new ElapsedEventHandler((sender, e) => tPiste_Elapsed(sender, e, numPiste, lstPistes));
            tPiste.Interval = 30000;
            tPiste.Start();*/
        }

        private static void tPiste_tick(object sender, EventArgs e, int numPiste, List<Piste> lstPistes, DispatcherTimer dtPiste)
        {
            lstPistes[numPiste].estDisponible = true;
            MessageBox.Show("La piste #" + (numPiste + 1) + " est est disponible.");
            dtPiste.Stop();
        }

        private static void PluieTorrentiel(EcranControleur ec)
        {
            if (!pluie)
            {
                Random r = new Random();

                MessageBox.Show("Pluie!!!!");

                DispatcherTimer dtPluie = new DispatcherTimer();
                dtPluie.Tick += new EventHandler((sender, e) => dtPluie_tick(sender, e, dtPluie, ec));
                dtPluie.Interval = new TimeSpan(0, 0, r.Next(30,301));
                dtPluie.Start();
            }
        }

        private static void dtPluie_tick(object sender, EventArgs e, DispatcherTimer dtPluie, EcranControleur ec)
        {
            MessageBox.Show("Pluie stop");
            dtPluie.Stop();
        }

        public static void StartTimer(EcranControleur ec)
        {
            DispatcherTimer dtFacteur = new DispatcherTimer();
            dtFacteur.Tick += new EventHandler((sender, e) => tFacteur_tick(sender, e, ec));
            dtFacteur.Interval = new TimeSpan(0, 0, 10);
            dtFacteur.Start();

            /*Timer tFacteur = new Timer();
            tFacteur.AutoReset = false;
            tFacteur.Elapsed += new ElapsedEventHandler((sender, e) => tFacteur_Elapsed(sender, e, tFacteur, ec));
            tFacteur.Interval = 10000;
            tFacteur.Start();*/
        }

        private static void tFacteur_tick(object sender, EventArgs e, EcranControleur ec)
        {
            FacteursExterieurs.FacteurAleatoire(ec);
        }

        private static void VolRetarde(EcranControleur ec)
        {        
            bool retarde = false;
            for (int i = 0; i < ec.LstVols.Count; i++)
            {
                if (!(ec.LstVols[i].EtatVol == Etat.Atterrissage || ec.LstVols[i].EtatVol == Etat.Decollage || ec.LstVols[i].EtatVol == Etat.Echoue))
                {
                    retarde = true;
                    break;
                }
            }

            if (retarde)
            {
                int volRetarde = new Random().Next(0, ec.LstVols.Count);
                while (ec.LstVols[volRetarde].EtatVol == Etat.Atterrissage || ec.LstVols[volRetarde].EtatVol == Etat.Decollage || ec.LstVols[volRetarde].EtatVol == Etat.Echoue || ec.LstVols[volRetarde].Delais.TotalMilliseconds <= 120000)
                {
                    volRetarde = new Random().Next(0, ec.LstVols.Count);
                }

                int volMagnitude = new Random().Next(0, 101);

                if (volMagnitude > 75)
                {
                    MessageBox.Show("Vol#" + ec.LstVols[volRetarde].NumeroVol + " retardé de 1 heure.");
                    ec.LstVols[volRetarde].DateVol = ec.LstVols[volRetarde].DateVol.AddHours(1);
                    //ec.TesterPiste(ec.LstVols[volRetarde]);                    
                }
                else if (volMagnitude > 50)
                {
                    MessageBox.Show("Vol#" + ec.LstVols[volRetarde].NumeroVol + " retardé de 30 minutes.");
                    ec.LstVols[volRetarde].DateVol = ec.LstVols[volRetarde].DateVol.AddMinutes(30);
                    //ec.TesterPiste(ec.LstVols[volRetarde]);
                }
                else
                {
                    MessageBox.Show("Vol#" + ec.LstVols[volRetarde].NumeroVol + " retardé de 5 minutes.");
                    ec.LstVols[volRetarde].DateVol = ec.LstVols[volRetarde].DateVol.AddMinutes(5);
                    //ec.TesterPiste(ec.LstVols[volRetarde]);
                }
                /*
                ec.LstVols[volRetarde].EtatVol = Etat.Attente;
                ec.LstVols[volRetarde].PisteAssigne = null;*/

                ec.RafraichirVols();
            }

        }
    }
}
