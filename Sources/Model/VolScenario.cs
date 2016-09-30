using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirAmbe
{
    /// <summary>
    /// Classe VolScénario
    /// </summary>
    public class VolScenario
    {
        //Déclaration des attributs 
        public int IdVolScenario { get; set;}
        public int IdVol { get; set; }
        public int IdScenario { get; set; }

        /// <summary>
        /// Constructeur de la classe VolScenario
        /// </summary>
        public VolScenario()
        {

        }

        /// <summary>
        /// Constructeur de la classe VolScenario
        /// </summary>
        /// <param name="unVolScenario">Indique les données a récupérer dans une rangée</param>
        public VolScenario(DataRow unVolScenario)
        {
            IdVolScenario = (int)unVolScenario["IdVolScenario"];
            IdVol = (int)unVolScenario["IdVol"];
            IdScenario = (int)unVolScenario["IdScenario"];
        }
    }
}
