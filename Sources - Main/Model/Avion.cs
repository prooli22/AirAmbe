using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirAmbe
{
    /// <summary>
    /// Classe Avion
    /// </summary>
    public class Avion
    {
        //Déclaration des attributs de la classe Avion
        public int IdAvion { get; set; }
        public string Marque { get; set;}
        public string Modele { get; set; }

        /// <summary>
        /// Constructeur par défaut de la classe Avion
        /// </summary>
        public Avion()
        {

        }

        /// <summary>
        /// Constructeur de la classe Avion
        /// </summary>
        /// <param name="unAvion">Indique les données a récupérer dans une rangée</param>
        public Avion(DataRow unAvion)
        {
            IdAvion = (int)unAvion["IdAvion"];
            Marque = (string)unAvion["Avion"];
            Modele = (string)unAvion["Modele"];
        }


    }
}
