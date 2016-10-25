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
    public class ScenarioAS
    {
        //Déclaration des attributs de la classe VolAS
        private BdService MaBd;

        public ScenarioAS()
        {
            MaBd = new BdService();
        }

        public void Inserer(Scenario Sc)
        {

            string ins = "INSERT INTO Scenarios (description)VALUES(" + Sc.Description + ");";
            MaBd.Commande(ins);

            for (int i = 0; i < Sc.lstVols.Count; i++)
            {
                string insVol = "INSERT INTO VolScenarios (idVol, idScenario) VALUES((SELECT idVol FROM Vols WHERE numeroVol = '" + Sc.lstVols[i].NumeroVol + "')," + Sc.IdScenario + ");";
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
            foreach (DataRow RowVol in dtScen.Rows)
            {
                ObservableScenario.Add(new Scenario(RowVol));

                string selVolsAtt = "SELECT v.numeroVol FROM Vols v INNER JOIN volScenarios vs ON vs.idVol = v.idVol WHERE v.estAtterrissage = 1 AND vs.idScenario = " + ObservableScenario[compt].IdScenario + ";";

                DataSet dsScen = MaBd.Selection(sel);

                DataTable dtScen = dsScen.Tables[0];
                compt++;
            }


            

            return ObservableScenario;
        }
    }
}
