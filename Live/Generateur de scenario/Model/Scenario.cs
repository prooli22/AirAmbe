//Nom: Vincent Désilets
//Date: 2016-12-12
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
        public string Description { get; set; }
        public List<String> lstVolsAtt { get; set; } = new List<string>();
        public List<String> lstVolsDec { get; set; } = new List<string>();
        public List<String> lstVols { get; set; } = new List<string>();

        public int Intervalle { get; set; }

        public string[] NumVol { get; set; }

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
            Description = (string)unScenario["Description"];
        }
            

    }
}
