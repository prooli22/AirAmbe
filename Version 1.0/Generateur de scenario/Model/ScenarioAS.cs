using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirAmbe.ViewModel;
using System.Data;
using System.Collections.ObjectModel;
using System.Windows;

namespace AirAmbe.Model
{
    public class ScenarioAS
    {
        private BdService MaBd;

        public ScenarioAS()
        {
            MaBd = new BdService();
        }

        public void Modifier(Scenario scenModification)
        {
            string modif = "UPDATE scenarios SET description='" 
                + scenModification.Description + "' WHERE idScenario=" + scenModification.IdScenario + ";";
            MaBd.Commande(modif);

            modif = " DELETE FROM volscenarios WHERE idScenario=" + scenModification.IdScenario + ";";
            MaBd.Commande(modif);

            for (int i = 0; i < scenModification.lstVols.Count; i++)
            {
                modif = " INSERT INTO VolScenarios (idVol, idScenario) VALUES((SELECT idVol FROM Vols WHERE numeroVol = '" + scenModification.lstVols[i] + "')," + scenModification.IdScenario + ");";
                MaBd.Commande(modif);
            }

        }

        public void Inserer(Scenario Sc)
        {
            string ins = "INSERT INTO Scenarios (description)VALUES('" + Sc.Description + "');";
            long idScen = MaBd.Commande(ins);

            for (int i = 0; i < Sc.lstVols.Count; i++)
            {
                string insVol = "INSERT INTO VolScenarios (idVol, idScenario) VALUES((SELECT idVol FROM Vols WHERE numeroVol = '" + Sc.lstVols[i] + "')," + idScen + ");";
                MaBd.Commande(insVol);
            }
        }
        public ObservableCollection<Scenario> RecupererTous()
        {
            string sel = "SELECT * FROM Scenarios;";
            ObservableCollection<Scenario> ObservableScenario = new ObservableCollection<Scenario>();

            DataSet dsScen = MaBd.Selection(sel);

            DataTable dtScen = dsScen.Tables[0];

            int compt = 0;
            foreach (DataRow RowScen in dtScen.Rows)
            {
                ObservableScenario.Add(new Scenario(RowScen));

                string selVolsAtt = "SELECT v.numeroVol FROM Vols v INNER JOIN volScenarios vs ON vs.idVol = v.idVol WHERE v.estAtterrissage = 1 AND vs.idScenario = " + ObservableScenario[compt].IdScenario + ";";

                DataSet dsVolAtt = MaBd.Selection(selVolsAtt);

                DataTable dtVolAtt = dsVolAtt.Tables[0];

                foreach (DataRow RowVol in dtVolAtt.Rows)
                {
                    ObservableScenario[compt].lstVolsAtt.Add(RowVol[0].ToString());
                }

                string selVolDec = "SELECT v.numeroVol FROM Vols v INNER JOIN volScenarios vs ON vs.idVol = v.idVol WHERE v.estAtterrissage = 0 AND vs.idScenario = " + ObservableScenario[compt].IdScenario + ";";

                DataSet dsVolDec = MaBd.Selection(selVolDec);

                DataTable dtVolDec = dsVolDec.Tables[0];

                foreach (DataRow RowVol in dtVolDec.Rows)
                {
                    ObservableScenario[compt].lstVolsDec.Add(RowVol[0].ToString());
                }

                string selVols = "SELECT v.numeroVol FROM Vols v INNER JOIN volScenarios vs ON vs.idVol = v.idVol WHERE vs.idScenario = " + ObservableScenario[compt].IdScenario + ";";

                DataSet dsVol = MaBd.Selection(selVols);

                DataTable dtVol = dsVol.Tables[0];

                foreach (DataRow RowVol in dtVol.Rows)
                {
                    ObservableScenario[compt].lstVols.Add(RowVol[0].ToString());
                }

                compt++;
            }

            return ObservableScenario;
        }
    }
}
