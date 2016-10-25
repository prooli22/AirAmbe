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
    /// Classe VolScenarioAS qui permet de définir les requêtes pour cette classe
    /// </summary>
    public class VolScenarioAS
    {
        //Déclaration des attributs de la classe VolAS
        private BdService MaBd;

        /// <summary>
        /// Constructeur par défaut de la classe VolAS
        /// </summary>
        public VolScenarioAS()
        {
            MaBd = new BdService();
        }

        /// <summary>
        /// Une méthode pour l'insertion d'un vol en BD
        /// </summary>
        /// <param name="VolScenarioInsertion">Un volScenario</param>
        public void Inserer(VolScenario VolScenarioInsertion)
        {
            string ins = "INSERT INTO volscenarios VALUE(NULL,'" + VolScenarioInsertion.IdVol + "','" +
                                                                     VolScenarioInsertion.IdScenario + "')";

            MaBd.Commande(ins);
        }

        /// <summary>
        /// Une méthode pour la suppression d'un vol en BD
        /// </summary>
        /// <param name="VolScenarioSuppression">Un vol</param>
        public void Supprimer(int VolScenarioSuppression)
        {
            string asup = "DELETE from volscenarios WHERE idVolScenario=" + VolScenarioSuppression;
            MaBd.Commande(asup);
        }

        /// <summary>
        /// Une méthode pour la modification d'un vol en BD
        /// </summary>
        /// <param name="VolScenarioModification">Un vol</param>
        public void Modifier(VolScenario VolScenarioModification)
        {
            string amod = "UPDATE volscenarios SET idVol = '" + VolScenarioModification.IdVol +
                                                "',idAeroport = '" + VolScenarioModification.IdScenario +
                                        
                                                "' WHERE idVolScenario = " +
                                                VolScenarioModification.IdVolScenario;
            MaBd.Commande(amod);
        }

        /// <summary>
        /// Une méthode pour sélectionner un seul vol en BD
        /// </summary>
        /// <param name="VolSelection">Un vol</param>
        /// <returns></returns>
        public VolScenario Recuperer(int VolSelection)
        {

            string sel = "SELECT * FROM volscenarios WHERE idVol = " + VolSelection;

            DataSet dsVols = MaBd.Selection(sel);

            DataTable dtVols = dsVols.Tables[0];

            return new VolScenario(dtVols.Rows[0]);
        }

        /// <summary>
        /// Une méthode pour sélectionner tous les vols
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<VolScenario> RecupererTous()
        {

            string sel = "SELECT * FROM volscenarios";
            ObservableCollection<VolScenario> ObservableDesVolScenarios = new ObservableCollection<VolScenario>();

            DataSet dsVolScenarios = MaBd.Selection(sel);

            DataTable dtVolScenarios = dsVolScenarios.Tables[0];

            foreach (DataRow RowVolScenario in dtVolScenarios.Rows)
            {
                ObservableDesVolScenarios.Add(new VolScenario(RowVolScenario));
            }



            return ObservableDesVolScenarios;
        }

    }
}
