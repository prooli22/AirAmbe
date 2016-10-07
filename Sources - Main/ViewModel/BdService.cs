using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AirAmbe.ViewModel
{
    /// <summary>
    /// Classe BdService qui permet de faire les accès en Bd
    /// </summary>
    public class BdService
    {
        //Déclaration des attributs de la classe BdService
        private MySqlConnection BdInterne;
        //private string Serveur = "420.cstj.qc.ca/mysql";
        //private string BaseDonnee = "420.5a5.a16_aeroplus";
        //private string Utilisateur = "aeroplus";
        //private string MotPasse = "AP228855";

        /// <summary>
        /// Constructeur par défaut de la classe BdService
        /// </summary>
        public BdService()
        {
            //Un try/catch qui établi la connection avec la Base de données
            try
            {
                string connexionString = ConfigurationManager.ConnectionStrings["MySqlConnexion"].ConnectionString;

                BdInterne = new MySqlConnection(connexionString);

                //MessageBox.Show("Connexion OK");

                
            }
            catch (Exception e)
            {
                MessageBox.Show("Connexion defectueuse : " + e.Message);
            }
        }

        /// <summary>
        /// Méthode qui permet d'intéragir avec une requête
        /// </summary>
        /// <param name="requete"></param>
        /// <returns></returns>
        public long Commande(string requete)
        {
            long retVal = 0;
            MessageBox.Show(requete.ToString());
            try
            {
                if (OuvrirConnexion())
                {
                    MySqlCommand cmd = new MySqlCommand(requete, BdInterne);

                    cmd.ExecuteNonQuery();
                    FermerConnexion();
                    retVal = cmd.LastInsertedId;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur de requete SQL :" + e.Message);
            }

            return retVal;
        }


        public DataSet Selection(string requete)
        {
            DataSet ds = new DataSet();

            try
            {
                if (OuvrirConnexion())
                {
                    MySqlDataAdapter adapteur = new MySqlDataAdapter();
                    adapteur.SelectCommand = new MySqlCommand(requete, BdInterne);

                  
                    adapteur.Fill(ds);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur de récuperation : {0}", e.Message);
                ds = null;
            }
            finally
            {
                FermerConnexion();
            }
          
            return ds;
        }

        /// <summary>
        /// Une méthode qui permet d'ouvrir la connexion avec la base de données
        /// </summary>
        /// <returns></returns>
        private bool OuvrirConnexion()
        {
            //Un try/catch qui essai d'ouvrir la base de données
            try
            {
                BdInterne.Open();   
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Une méthode qui permet de fermer la connexion avec la base de données
        /// </summary>
        /// <returns></returns>
        private bool FermerConnexion()
        {
            //Un try/catch qui essai de fermer la base de données
            try
            {
                BdInterne.Close();  
                return true;
            }
            catch
            {
                return false;
            }
        }





    }
}
