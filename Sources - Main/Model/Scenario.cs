using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirAmbe
{
    /// <summary>
    /// Classe Scénario
    /// </summary>
    public class Scenario
    {
        //Déclaration des attributs de la classe Scénario
        public int IdScenario { get; set; }
        
        /// <summary>
        /// Constructeur par défaut de la classe Scenario
        /// </summary>
        public Scenario()
        {

        }

        /// <summary>
        /// Constructeur de la classe Scenario
        /// </summary>
        /// <param name="unScenario">Indique les données a récupérer dans une rangée</param>
        public Scenario(DataRow unScenario)
        {
            IdScenario = (int)unScenario["IdScenario"];
        }
            

    }
}
