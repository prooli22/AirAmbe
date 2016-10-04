using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirAmbe
{
    /// <summary>
    /// Classe Aeroport
    /// </summary>
    public class Aeroport
    {
        //Déclaration des attributs de la classe
        public int IdAeroport { get; set; }
        public string Ville { get; set; }
        public string CodeAITA { get; set; }

        /// <summary>
        /// Constructeur par défaut de la classe Aeroport
        /// </summary>
        public Aeroport()
        {

        }

        /// <summary>
        /// Constructeur de la classe Aeroport
        /// </summary>
        /// <param name="unAeroport">Indique les données a récupérer dans une rangée</param>
        public Aeroport(DataRow unAeroport)
        {
            IdAeroport = (int)unAeroport["IdAeroport"];
            Ville = (string)unAeroport["Ville"];
            CodeAITA = (string)unAeroport["CodeAITA"];
        }
    }
}
