using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirAmbe
{
    /// <summary>
    /// Classe Adresse
    /// </summary>
    public class Adresse
    {
        //Déclaration des attributs de la classe Adresse
        public int IdAdresse { get; set; }
        public int Numero { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        public string Province { get; set; }
        public string CodePostal{get; set;}

        /// <summary>
        /// Constructeur par défaut de la classe Adresse
        /// </summary>
        public Adresse()
        {

        }

        /// <summary>
        /// Constructeur de la classe Adresse
        /// </summary>
        /// <param name="uneAdresse">Indique les données a récupérer dans une rangée</param>
        public Adresse(DataRow uneAdresse)
        {
            IdAdresse = (int)uneAdresse["IdAdresse"];
            Numero = (int)uneAdresse["Numero"];
            Rue = (string)uneAdresse["Rue"];
            Ville = (string)uneAdresse["Ville"];
            Province = (string)uneAdresse["Province"];
            CodePostal = (string)uneAdresse["CodePostal"];
        }

    }
}
