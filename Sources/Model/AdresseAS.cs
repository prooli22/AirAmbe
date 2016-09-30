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
        /// Une méthode pour l'insertion d'un utilisateur en BD
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
        /// Une méthode pour la suppression d'une en BD
        /// </summary>
        /// <param name="AdresseSuppression">Une adresse</param>
        public void Supprimer(int AdresseSuppression)
        {
            string asup = "DELETE FROM adresses WHERE idAdresse=" + AdresseSuppression;
            MaBd.Commande(asup);
        }

        /// <summary>
        /// Une méthode pour la modification d'un utilisateur en BD
        /// </summary>
        /// <param name="UtilisateurModification">Un utilisateur</param>
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
        /// Une méthode pour sélectionner un seul utilisateur en BD
        /// </summary>
        /// <param name="UtilisateurSelection">Un utilisateur</param>
        /// <returns></returns>
        public Utilisateur Recuperer(int UtilisateurSelection)
        {

            string sel = "SELECT * FROM utilisateurs WHERE idUtilisateur = " + UtilisateurSelection;

            DataSet dsUtilisateurs = MaBd.Selection(sel);

            DataTable dtUtilisateurs = dsUtilisateurs.Tables[0];

            return new Utilisateur(dtUtilisateurs.Rows[0]);
        }

        /// <summary>
        /// Une méthode pour sélectionner tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Utilisateur> RecupererTous()
        {

            string sel = "SELECT * FROM utilisateurs";
            ObservableCollection<Utilisateur> ObservableDesUtilisateurs = new ObservableCollection<Utilisateur>();

            DataSet dsUtilisateurs = MaBd.Selection(sel);

            DataTable dtUtilisateurs = dsUtilisateurs.Tables[0];

            foreach (DataRow RowUtilisateur in dtUtilisateurs.Rows)
            {
                ObservableDesUtilisateurs.Add(new Utilisateur(RowUtilisateur));
            }



            return ObservableDesUtilisateurs;
        }









    }
}
