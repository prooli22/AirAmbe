﻿// Nom : Olivier Provost.
// Date : 2016-12-09.


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AirAmbe
{
    /// <summary>
    /// Classe Vol
    /// </summary>
    public class Vol
    {
        //Déclaration des attributs de la classe
        public int IdVol { get; set; }
        public string ModeleAvion { get; set; } //Attribut IdAvion
        public string Aeroport { get; set; } //Attribut IdAeroport
        public string NumeroVol { get; set; }
        public bool EstAtterrissage { get; set; }

        // Propriétés.
        public DateTime DateVol { get; set; }
        public DateTime DatePrevu { get; set; }
        public int Intervalle { get; set; }
        public int NumScenario { get; set; }
        public Etat EtatVol { get; set; }
        public TimeSpan Delais { get; set; }
        public Piste PisteAssigne { get; set; }
        public Piste PisteHistorique { get; set; }
        public int TempsCritque { get; set; }
        public int TempsFinal { get; set; }
        public int TempsRetard { get; set; } = 1;
        public string TempsUnite { get; set; } = "Secondes";
        public bool EstLance { get; set; } = false;

        /// <summary>
        /// Constructeur de la classe Vol.
        /// </summary>
        public Vol()
        {

        }

        /// <summary>
        /// Constructeur de la classe Vol.
        /// </summary>
        /// <param name="unVol"> Indique les données a récupérer dans une rangée .</param>
        public Vol(DataRow unVol)
        {
            IdVol = (int)unVol["IdVol"];
            ModeleAvion = (string)unVol["ModeleAvion"];
            Aeroport = (string)unVol["Aeroport"];
            NumeroVol = (string)unVol["NumeroVol"];
            EstAtterrissage = (bool)unVol["EstAtterrissage"];
            DateVol = DateTime.Now;
            EtatVol = Etat.Attente;
        }

    }
}
