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
    /// Classe AeroportsAS qui permet de définir les requêtes pour cette classe
    /// </summary>
    public class AeroportAS
    {
        //Déclaration des attributs de la classe AeroportAS
        private BdService MaBd;

        /// <summary>
        /// Constructeur par défaut de la classe AeroportAS
        /// </summary>
        public AeroportAS()
        {
            MaBd = new BdService();
        }

        /// <summary>
        /// Une méthode pour l'insertion d'un Aeroport en BD
        /// </summary>
        /// <param name="AeroportInsertion">Un Aeroport</param>
        public void Inserer(Aeroport AeroportInsertion)
        {
            string ins = "INSERT INTO aeroports VALUE(NULL,'" + AeroportInsertion.Ville + "','" +   
                                                                   AeroportInsertion.CodeAITA + "')";
            MaBd.Commande(ins);
        }

        /// <summary>
        /// Une méthode pour la suppression d'un aeroport en BD
        /// </summary>
        /// <param name="AeroportSuppression">Un Aeroport</param>
        public void Supprimer(int AeroportSuppression)
        {
            string asup = "DELETE FROM aeroports WHERE idAeroport=" + AeroportSuppression;
            MaBd.Commande(asup);
        }

        /// <summary>
        /// Une méthode pour la modification d'un aeroport en BD
        /// </summary>
        /// <param name="AeroportModification">Un Aeroport</param>
        public void Modifier(Aeroport AeroportModification)
        {
            string amod = "UPDATE aeroport SET ville = '" + AeroportModification.Ville +
                                                "',rue = '" + AeroportModification.CodeAITA +
                                                                                            
                                                "' WHERE idAeroport = " +
                                                AeroportModification.IdAeroport;
            MaBd.Commande(amod);
        }

        /// <summary>
        /// Une méthode pour sélectionner un seul aeroport en BD
        /// </summary>
        /// <param name="AeroportSelection">Un Aeroport</param>
        /// <returns></returns>
        public Aeroport Recuperer(int AeroportSelection)
        {

            string sel = "SELECT * FROM aeroports WHERE idAeroport = " + AeroportSelection;

            DataSet dsAeroport = MaBd.Selection(sel);

            DataTable dtAeroport = dsAeroport.Tables[0];

            return new Aeroport(dtAeroport.Rows[0]);
        }

        /// <summary>
        /// Une méthode pour sélectionner tous les aeroports
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Aeroport> RecupererTous()
        {

            string sel = "SELECT * FROM aeroports";
            ObservableCollection<Aeroport> ObservableDesAeroports = new ObservableCollection<Aeroport>();

            DataSet dsAeroports = MaBd.Selection(sel);

            DataTable dtAeroports = dsAeroports.Tables[0];

            foreach (DataRow RowAeroport in dtAeroports.Rows)
            {
                ObservableDesAeroports.Add(new Aeroport(RowAeroport));
            }

            return ObservableDesAeroports;
        }
    }

}
