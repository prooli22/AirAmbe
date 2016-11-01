using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace AirAmbe
{
    public static class FacteursExterieurs
    {
        private static void FacteurAleatoire(EcranControleur ec)
        {
            Random r = new Random();

            int nombre = r.Next(1, 101);
        
            /*if (nombre < 20)
            {
                VolRetarde(ec);
            }
            else if (nombre < 35)
            {
                PluieTorrentiel(ec);
            }
            else if (nombre < 45)*/
            {
                PisteIndisponible(ec.LstPistes);
            }
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
            }
        }

        private static void StartTimerPiste(int numPiste, List<Piste> lstPistes)
        {
            Timer tPiste = new Timer();
            tPiste.AutoReset = false;
            tPiste.Elapsed += new ElapsedEventHandler((sender, e) => tPiste_Elapsed(sender, e, numPiste, lstPistes));
            tPiste.Interval = 30000;
            tPiste.Start();
        }

        private static void tPiste_Elapsed(object sender, ElapsedEventArgs e, int numPiste, List<Piste> lstPistes)
        {
            lstPistes[numPiste].estDisponible = true;
        }

        private static void PluieTorrentiel(EcranControleur ec)
        {
            MessageBox.Show("Pluie!!!!");
            //ec.StartPluie();
        }

        public static void StartTimer(EcranControleur ec)
        {
            Timer tFacteur = new Timer();
            tFacteur.AutoReset = false;
            tFacteur.Elapsed += new ElapsedEventHandler((sender, e) => tFacteur_Elapsed(sender, e, tFacteur, ec));
            tFacteur.Interval = 10000;
            tFacteur.Start();
        }

        private static void tFacteur_Elapsed(object sender, ElapsedEventArgs e, Timer tFacteur, EcranControleur ec)
        {
            FacteursExterieurs.FacteurAleatoire(ec);

            tFacteur.Start();
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
                while (ec.LstVols[volRetarde].EtatVol == Etat.Atterrissage || ec.LstVols[volRetarde].EtatVol == Etat.Decollage || ec.LstVols[volRetarde].EtatVol == Etat.Echoue || ec.LstVols[volRetarde].Delais <= 2)
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
            }

        }
    }
}
