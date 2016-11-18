using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirAmbe
{
    public enum Etat : int
    {
        Attente = 0,
        Assigne = 1,
        Decollage = 2,
        Atterrissage = 3,
        Critique = 4,
        Retarde = 5,
        Cancelle = 6
    }
}
