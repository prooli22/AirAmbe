using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirAmbe.ViewModel;
using System.Data;
using System.Collections.ObjectModel;

namespace AirAmbe.Model
{
    /// <summary>
    /// Classe VolAS qui permet de définir les requêtes pour cette classe
    /// </summary>
    public class VolAS
    {
        //Déclaration des attributs de la classe VolAS
        private BdService MaBd;

        /// <summary>
        /// Constructeur par défaut de la classe VolAS
        /// </summary>
        public VolAS()
        {
            MaBd = new BdService();
        }

        /// <summary>
        /// Une méthode pour l'insertion d'un vol en BD
        /// </summary>
        /// <param name="VolInsertion">Un vol</param>
        public void Inserer(Vol VolInsertion)
        {
            //Manque la requete de select pour les 2 id

            string ins = "INSERT INTO vols VALUE(NULL,'" + VolInsertion.ModeleAvion + "','" +
                                                                     VolInsertion.Aeroport + "','" +
                                                                     VolInsertion.NumeroVol + "','" +
                                                                     VolInsertion.EstAtterrissage + "')";
            MaBd.Commande(ins);
        }

        /// <summary>
        /// Une méthode pour la suppression d'un vol en BD
        /// </summary>
        /// <param name="VolSuppression">Un vol</param>
        public void Supprimer(int VolSuppression)
        {
            string asup = "DELETE from vols WHERE idVol=" + VolSuppression;
            MaBd.Commande(asup);
        }

        /// <summary>
        /// Une méthode pour la modification d'un vol en BD
        /// </summary>
        /// <param name="UtilisateurModification">Un vol</param>
        public void Modifier(Vol VolModification)
        {
            //Manque la requete de select pour les 2 id

            string amod = "UPDATE vols SET idAvion = '" + VolModification.ModeleAvion +
                                                "',idAeroport = '" + VolModification.Aeroport +
                                                "',numeroVol = '" + VolModification.NumeroVol +
                                                "',estAtterissage = '" + VolModification.EstAtterrissage +
                                          
                                                "' WHERE idVol = " +
                                                VolModification.IdVol;
            MaBd.Commande(amod);
        }

        /// <summary>
        /// Une méthode pour sélectionner un seul vol en BD
        /// </summary>
        /// <param name="VolSelection">Un vol</param>
        /// <returns></returns>
        public Vol Recuperer(string NumVol)
        {
            string sel = "SELECT v.idVol" +
                    ", v.numeroVol" +
                    ", CONCAT(av.marque, ' ', av.modele) AS modeleAvion" +
                    ", CONCAT (ae.codeAITA, ' - ', ae.ville) AS aeroport" +
                    ", v.estAtterrissage " +
             "FROM vols v " +
                    "INNER JOIN aeroports ae ON v.idAeroport = ae.idAeroport " +
                    "INNER JOIN avions av ON v.idAvion = av.idAvion " +
                     "WHERE numeroVol like '" + NumVol + "'";

            DataSet dsVols = MaBd.Selection(sel);

            DataTable dtVols = dsVols.Tables[0];

            return new Vol(dtVols.Rows[0]);
        }

        /// <summary>
        /// Une méthode pour sélectionner tous les vols
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Vol> RecupererTous()
        {

            string sel = "SELECT v.idVol" +
                                ", v.numeroVol" +
                                ", CONCAT(av.marque, ' ', av.modele) AS modeleAvion" +
                                ", CONCAT (ae.codeAITA, ' - ', ae.ville) AS aeroport" +
                                ", v.estAtterrissage " +
                         "FROM vols v "+
                                "INNER JOIN aeroports ae ON v.idAeroport = ae.idAeroport " + 
                                "INNER JOIN avions av ON v.idAvion = av.idAvion";
            ObservableCollection<Vol> ObservableDesVols = new ObservableCollection<Vol>();

            DataSet dsVols = MaBd.Selection(sel);

            DataTable dtVols = dsVols.Tables[0];

            foreach (DataRow RowVol in dtVols.Rows)
            {
                ObservableDesVols.Add(new Vol(RowVol));
            }



            return ObservableDesVols;
        }



    }




}
