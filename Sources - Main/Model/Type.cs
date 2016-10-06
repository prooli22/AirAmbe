using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirAmbe
{
    /// <summary>
    /// Constructeur de la classe Type
    /// </summary>
    public class Type
    {
        //Déclaration des attributs de la classe Type
        public int IdType { get; set; }
        public string Nom { get; set; }

        /// <summary>
        /// Constructeur par défaut de la classe Type
        /// </summary>
        public Type()
        {


        }

        /// <summary>
        /// Constructeur de la classe Type
        /// </summary>
        /// <param name="unType">Indique les données a récupérer dans une rangée</param>
        public Type(DataRow unType)
        {
            IdType = (int)unType["IdType"];
            Nom = (string)unType["Nom"];
        }


    }
}
