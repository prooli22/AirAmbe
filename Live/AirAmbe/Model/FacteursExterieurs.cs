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
        public static void ChangerEtatPiste(Piste piste)
        {
            piste.estDisponible = !piste.estDisponible;
        }

        public static void VolRetarde(Vol v, int secondes, EcranControleur ec)
        {
            MessageBox.Show("Vol#" + v.NumeroVol + " retardé de " + secondes + " secondes.");

            ec.RetarderVol(v.IdVol, secondes * 1000);
        }
    }
}
