// Nom: Anthony Massé
// Date: 9 Décembre 2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace AirAmbe
{
    /// <summary>
    /// Classe permettant d'accéder à une avion
    /// </summary>
    public class Avion
    {
        //Attributs de la classe Avion
        public int NumAvion { get; set; }

        public bool EstDisponibleAtterrissage { get; set; }

        public bool EstDisponibleDecollage { get; set; }

        public bool EstInitialisee { get; set; }

        public Rectangle ImageAvion { get; set; }

        public int HangarAssigne { get; set; }

        public float CoordX { get; set; }

        public float CoordY { get; set; }

        /// <summary>
        /// Constructeur de la classe Avion
        /// </summary>
        public Avion()
        {
            this.ImageAvion = new Rectangle();
        }
    }
}
