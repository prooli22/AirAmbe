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
    /// Classe TypeAS qui permet de définir les requêtes pour cette classe
    /// </summary>
    public class TypeAS
    {
        //Déclaration des attributs de la classe TypeAS
        private BdService MaBd;

        /// <summary>
        /// Constructeur par défaut de la classe TypeAS
        /// </summary>
        public TypeAS()
        {
            MaBd = new BdService();
        }

        /// <summary>
        /// Une méthode pour l'insertion d'un type en BD
        /// </summary>
        /// <param name="TypeInsertion">Un type</param>
        public void Inserer(Type TypeInsertion)
        {
            string ins = "INSERT INTO types VALUE(NULL,'" + TypeInsertion.Nom + "')";
            MaBd.Commande(ins);
        }

        /// <summary>
        /// Une méthode pour la suppression d'un type en BD
        /// </summary>
        /// <param name="TypeSuppression">Un type</param>
        public void Supprimer(int TypeSuppression)
        {
            string asup = "DELETE from types WHERE idType=" + TypeSuppression;
            MaBd.Commande(asup);
        }

        /// <summary>
        /// Une méthode pour la modification d'un type en BD
        /// </summary>
        /// <param name="UtilisateurModification">Un type</param>
        public void Modifier(Type TypeModification)
        {
            string amod = "UPDATE types SET idType = '" + TypeModification.Nom +                   

                                                "' WHERE idType = " +
                                                TypeModification.IdType;
            MaBd.Commande(amod);
        }

        /// <summary>
        /// Une méthode pour sélectionner un seul type en BD
        /// </summary>
        /// <param name="TypeSelection">Un type</param>
        /// <returns></returns>
        public Type Recuperer(int TypeSelection)
        {

            string sel = "SELECT * FROM types WHERE idType = " + TypeSelection;

            DataSet dsTypes = MaBd.Selection(sel);

            DataTable dtTypes = dsTypes.Tables[0];

            return new Type(dtTypes.Rows[0]);
        }

        /// <summary>
        /// Une méthode pour sélectionner tous les types
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Type> RecupererTous()
        {

            string sel = "SELECT * FROM types";
            ObservableCollection<Type> ObservableDesTypes = new ObservableCollection<Type>();

            DataSet dsTypes = MaBd.Selection(sel);

            DataTable dtTypes = dsTypes.Tables[0];

            foreach (DataRow RowType in dtTypes.Rows)
            {
                ObservableDesTypes.Add(new Type(RowType));
            }



            return ObservableDesTypes;
        }



    }







}
