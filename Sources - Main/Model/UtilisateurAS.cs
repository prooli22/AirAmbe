using System;
using System.Collections.Generic;
using AirAmbe.ViewModel;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace AirAmbe.Model
{
    /// <summary>
    /// Classe UtilisateurAS sert à faire les requêtes en Bd pour la classe Utilisateurs
    /// </summary>
    public class UtilisateurAS
    {
        //Déclaration des attributs de la classe UtilisateursAS
        private BdService MaBd;

        /// <summary>
        /// Constructeur par défaut de la classe UtilisateursAS
        /// </summary>
        public UtilisateurAS()
        {
            MaBd = new BdService();
        }

        /// <summary>
        /// Une méthode pour l'insertion d'un utilisateur en BD
        /// </summary>
        /// <param name="UtilisateurInsertion">Un utilisateur</param>
        public void Inserer(Utilisateur UtilisateurInsertion)
        {
            string ins = "INSERT INTO utilisateurs VALUE(NULL,'" + UtilisateurInsertion.TypeUtilisateur + "','" +                                                                   UtilisateurInsertion.NomUtilisateur + "','" +
                                                                   UtilisateurInsertion.MotPasse + "','" +
                                                                   UtilisateurInsertion.Prenom + "','" +
                                                                   UtilisateurInsertion.Nom + "','" +
                                                                   UtilisateurInsertion.Poste + "','" +
                                                                   UtilisateurInsertion.DateEmbauche + "','" +
                                                                   UtilisateurInsertion.Telephone + "','" +
                                                                   UtilisateurInsertion.Courriel + "','" +
                                                                   UtilisateurInsertion.Adresse + "',NULL)";
            MaBd.Commande(ins);
        }

        /// <summary>
        /// Une méthode pour la suppression d'un utilisateur en BD
        /// </summary>
        /// <param name="UtilisateurSuppression">Un utilisateur</param>
        public void Supprimer(int UtilisateurSuppression)
        {
            string asup = "DELETE from utilisateurs WHERE idUtilisateur=" + UtilisateurSuppression;
            MaBd.Commande(asup);
        }
        
        /// <summary>
        /// Une méthode pour la modification d'un utilisateur en BD
        /// </summary>
        /// <param name="UtilisateurModification">Un utilisateur</param>
        public void Modifier(Utilisateur UtilisateurModification)
        {
            string amod = "UPDATE utilisateurs SET 'idType = '" + UtilisateurModification.TypeUtilisateur + 
                                                "',nomUtilisateur = '" + UtilisateurModification.NomUtilisateur + 
                                                "',motPasse = '" + UtilisateurModification.MotPasse + 
                                                "',prenom = '" + UtilisateurModification.Prenom +
                                                "',nom = '" + UtilisateurModification.Nom +
                                                "',poste = '" + UtilisateurModification.Poste +
                                                "',dateEmbauche = '" + UtilisateurModification.DateEmbauche +
                                                "',telephone = '" + UtilisateurModification.Telephone +
                                                "',courriel = '" + UtilisateurModification.Courriel +
                                                "',courriel = '" + UtilisateurModification.Adresse +

                                                "' WHERE idUtilisateur = " +
                                                UtilisateurModification.IdUtilisateur;
            MaBd.Commande(amod);
        }

        /// <summary>
        /// Une méthode pour sélectionner un seul utilisateur en BD
        /// </summary>
        /// <param name="UtilisateurSelection">Un utilisateur</param>
        /// <returns></returns>
        public Utilisateur Recuperer(string user, string mdp)
        {

            string sel = "SELECT * FROM utilisateurs WHERE nomUtilisateur LIKE '" + user + "' AND motPasse LIKE '" + mdp + "'";

            DataSet dsUtilisateurs = MaBd.Selection(sel);

            DataTable dtUtilisateurs = dsUtilisateurs.Tables[0];

            if (dtUtilisateurs.Rows.Count == 0)
            {
                Utilisateur u = new Utilisateur();
                u = null;
                return u;
            }

            return new Utilisateur(dtUtilisateurs.Rows[0]);
        }

        /// <summary>
        /// Une méthode pour sélectionner tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Utilisateur> RecupererTous()
        {

            string sel = "SELECT u.idUtilisateur" +
                                ",t.typeUtilisateur" +
                                ",u.nomUtilisateur" +
                                ",u.motPasse" +
                                ",u.prenom" +
                                ",u.nom" +
                                ",u.poste" +
                                ",u.dateEmbauche" +
                                ",u.telephone" +
                                ",u.courriel" +
                                ",u.adresse" +
                                ",u.photo " +
                         "FROM utilisateurs u " +
                                "INNER JOIN types t ON t.idType=u.idType";
            //string sel = "SELECT * FROM utilisateurs";

            ObservableCollection<Utilisateur> ObservableDesUtilisateurs = new ObservableCollection<Utilisateur>();

            DataSet dsUtilisateurs = MaBd.Selection(sel);
                   
            DataTable dtUtilisateurs = dsUtilisateurs.Tables[0];


            //foreach (DataRelation relation in dtUtilisateurs.ParentRelations)
            //{

                foreach (DataRow RowUtilisateur in dtUtilisateurs.Rows)
                {
                    //arrRows = RowUtilisateur.GetChildRows(relation);
                    //// Print values of rows.
                    //for (int i = 0; i < arrRows.Length; i++)
                    //{
                    //    foreach (DataColumn column in dtUtilisateurs.Columns)
                    //    {
                    //        Console.WriteLine(arrRows[i][column]);
                    //    }
                    //}
                    ObservableDesUtilisateurs.Add(new Utilisateur(RowUtilisateur));
                }

            //}




            //// DataRelation relation = thisTable.ParentRelations[0];
            //foreach (DataRow RowUtilisateur in dtUtilisateurs.Rows)
            //{
            //    //dr = RowUtilisateur.GetParentRow(relation);
            //    ObservableDesUtilisateurs.Add(new Utilisateur(RowUtilisateur));
            //}



            return ObservableDesUtilisateurs;
        }



    }
}
