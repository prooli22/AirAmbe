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
        public static void ChangerEtatPiste(Piste piste, EcranControleur ec)
        {
            piste.estDisponible = !piste.estDisponible;

            Animation a = new Animation(ec);

            a.ChangerOpacitePiste(piste);

        }

        public static void VolRetarde(Vol v, int secondes, EcranControleur ec)
        {
            MessageBox.Show("Vol#" + v.NumeroVol + " retardé de " + secondes + " secondes.");

            ec.RetarderVol(v.IdVol, secondes * 1000);
        }
    }
}
