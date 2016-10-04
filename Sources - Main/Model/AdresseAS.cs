using System;
using System.Collections.Generic;
using AirAmbe.ViewModel;
using System.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirAmbe.Model
{
    /// <summary>
    /// Classe AdressesAS qui permet de définir les requêtes pour cette classe
    /// </summary>
    public class AdresseAS
    {
        //Déclaration des attributs de la classe AdresseAS
        private BdService MaBd;

        /// <summary>
        /// Constructeur par défaut de la classe AdresseAS
        /// </summary>
        public AdresseAS()
        {
            MaBd = new BdService();
        }

        /// <summary>
        /// Une méthode pour l'insertion d'une adresse en BD
        /// </summary>
        /// <param name="AdresseInsertion">Une adresse</param>
        public void Inserer(Adresse AdresseInsertion)
        {
            string ins = "INSERT INTO adresses VALUE(NULL,'" + AdresseInsertion.Numero + "','" +
                                                                   AdresseInsertion.Rue + "','" +
                                                                   AdresseInsertion.Ville + "','" +
                                                                   AdresseInsertion.Province + "','" +
                                                                   AdresseInsertion.CodePostal + "')";
            MaBd.Commande(ins);
        }

        /// <summary>
        /// Une méthode pour la suppression d'une adresse en BD
        /// </summary>
        /// <param name="AdresseSuppression">Une adresse</param>
        public void Supprimer(int AdresseSuppression)
        {
            string asup = "DELETE FROM adresses WHERE idAdresse=" + AdresseSuppression;
            MaBd.Commande(asup);
        }

        /// <summary>
        /// Une méthode pour la modification d'une adresse en BD
        /// </summary>
        /// <param name="AdresseModification">Une adresse</param>
        public void Modifier(Adresse AdresseModification)
        {
            string amod = "UPDATE adresses SET numero = '" + AdresseModification.Numero +
                                                "',rue = '" + AdresseModification.Rue +
                                                "',ville = '" + AdresseModification.Ville +
                                                "',province = '" + AdresseModification.Province +
                                                "',codePostal = '" + AdresseModification.CodePostal +

                                                "' WHERE idAdresse = " +
                                                AdresseModification.IdAdresse   ;
            MaBd.Commande(amod);
        }

        /// <summary>
        /// Une méthode pour sélectionner une seule adresse en BD
        /// </summary>
        /// <param name="AdresseSelection">Une adresse</param>
        /// <returns></returns>
        public Adresse Recuperer(int AdresseSelection)
        {

            string sel = "SELECT * FROM adresses WHERE idAdresse = " + AdresseSelection;

            DataSet dsAdresses = MaBd.Selection(sel);

            DataTable dtAdresses = dsAdresses.Tables[0];

            return new Adresse(dtAdresses.Rows[0]);
        }

        /// <summary>
        /// Une méthode pour sélectionner toutes les adresses
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Adresse> RecupererTous()
        {

            string sel = "SELECT * FROM adresses";
            ObservableCollection<Adresse> ObservableDesAdresses = new ObservableCollection<Adresse>();

            DataSet dsAdresses = MaBd.Selection(sel);

            DataTable dtAdresses = dsAdresses.Tables[0];

            foreach (DataRow RowAdresse in dtAdresses.Rows)
            {
                ObservableDesAdresses.Add(new Adresse(RowAdresse));
            }

            return ObservableDesAdresses;
        }
    }
}
