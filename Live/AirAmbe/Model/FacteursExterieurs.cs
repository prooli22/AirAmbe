using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AirAmbe
{
    public static class FacteursExterieurs
    {
        public static void FacteurAleatoire(List<Vol> lstVol)
        {
            Random r = new Random();

            int nombre = r.Next(1, 101);

            if (nombre < 20)
            {
                VolRetarde(lstVol);
            }
        }

        private static void VolRetarde(List<Vol> lstVol)
        {
            int volRetarde = new Random().Next(0, lstVol.Count);

            int volMagnitude = new Random().Next(0, lstVol.Count);

            if (volMagnitude > 75)
            {
                lstVol[volRetarde].DateVol.AddHours(1);
            }

        }
    }
}
