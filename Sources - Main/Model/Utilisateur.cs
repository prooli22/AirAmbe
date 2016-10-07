using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AirAmbe.Model
{
    /// <summary>
    /// Classe Utilisateur qui permet d'accéder aux informations d'un utilisateur
    /// </summary>
    public class Utilisateur
    {
        //Déclaration des attributs de la classe 
        public int IdUtilisateur { get; set; }
        public string Adresse { get; set; }
        //public int Type { get; set; }
        //public int TypeUtilisateur { get; set; }
        //public int IdAdresse { get; set; }
        public string TypeUtilisateur { get; set; }
        public string NomUtilisateur { get; set; }
        public string MotPasse { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Poste { get; set; }
        public DateTime DateEmbauche { get; set; }
        public string Telephone { get; set; }
        public string Courriel { get; set; }

        //TODO
        //Rajouter attribut photo

        /// <summary>
        /// Constructeur par défaut de la classe Utilisateur
        /// </summary>
        public Utilisateur()
        {

        }

        /// <summary>
        /// Constructeur de la classe Utilisateur
        /// </summary>
        /// <param name="unUtilisateur">Indique les données a récupérer dans une rangée</param>
        public Utilisateur(DataRow unUtilisateur)
        {
            IdUtilisateur = (int)unUtilisateur["IdUtilisateur"];         
            TypeUtilisateur = (string)unUtilisateur["TypeUtilisateur"];                   
            NomUtilisateur = (string)unUtilisateur["NomUtilisateur"];
            MotPasse = (string)unUtilisateur["MotPasse"];
            Prenom = (string)unUtilisateur["Prenom"];
            Nom = (string)unUtilisateur["Nom"];
            Poste = (string)unUtilisateur["Poste"];
            DateEmbauche = (DateTime)unUtilisateur["DateEmbauche"];
            Telephone = (string)unUtilisateur["Telephone"];
            Courriel = (string)unUtilisateur["Courriel"];
            Adresse = (string)unUtilisateur["Adresse"];
        }

      

    }
}
