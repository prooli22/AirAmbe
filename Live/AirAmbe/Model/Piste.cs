//Nom: Vincent Désilets
//Date: 2016-12-09
using AirAmbe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirAmbe
{
    /// <summary>
    /// Une piste est l'endroit où les avions atterrissent et décollent
    /// </summary>
    public class Piste
    {
        public int NumPiste { get; set; }
        public bool estDisponible { get; set; } = true;

        public Piste()
        {

        }
    }
}
