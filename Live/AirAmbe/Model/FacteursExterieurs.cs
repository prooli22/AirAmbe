//Nom: Vincent Désilets
//Date: 2016-12-12
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
        /// <summary>
        /// Change l'état d'une piste
        /// </summary>
        /// <param name="piste">La piste à changer</param>
        /// <param name="ec">l'écran du controleur</param>
        public static void ChangerEtatPiste(Piste piste, EcranControleur ec)
        {
            piste.estDisponible = !piste.estDisponible;

            Animation a = new Animation(ec);

            a.ChangerOpacitePiste(piste);

        }

        /// <summary>
        /// Retarde un vol
        /// </summary>
        /// <param name="v">Le vol à retarder</param>
        /// <param name="secondes">Le nombre de secondes</param>
        /// <param name="ec">L'écran du controleur</param>
        public static void VolRetarde(Vol v, int secondes, EcranControleur ec)
        {
            MessageBox.Show("Vol#" + v.NumeroVol + " retardé de " + secondes + " secondes.");

            ec.RetarderVol(v.IdVol, secondes * 1000);
        }
    }
}
