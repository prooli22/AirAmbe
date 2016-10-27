using AirAmbe;
using AirAmbe.Model;
using AirAmbe.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Test.ViewModel
{
    public class ScenarioViewModel : INotifyPropertyChanged
    {
        public ICommand cmdVider { get; set; }
        public ICommand cmdAjouter { get; set; }

        private ScenarioAS Scenario_Service;

        public ScenarioViewModel()
        {
            Scenario_Service = new ScenarioAS();
            SommaireScenarios = Scenario_Service.RecupererTous();
            cmdVider = new Commande(ActionVider);
            cmdAjouter = new Commande(ActionAjouter);
        }

        private void ActionVider(object param)
        {
            ScenarioSelectionne = null;
        }

        private void ActionAjouter(object param)
        {
            Scenario s = new Scenario();

            s.Description = Description;
            s.lstVolsAtt = LstVolsAtt;
            s.lstVolsDec = LstVolsDec;

            SommaireScenarios.Add(s);
            Scenario_Service.Inserer(s);
        }

        private Scenario scenarioSelectionne;
        public Scenario ScenarioSelectionne
        {
            get { return scenarioSelectionne; }
            set
            {
                if (value == null)
                {
                    scenarioSelectionne = null;
                    Description = null;
                    LstVolsAtt = null;
                    LstVolsDec = null;
                }
                else
                {
                    scenarioSelectionne = value;
                    Description = scenarioSelectionne.Description;
                    LstVolsAtt = scenarioSelectionne.lstVolsAtt;
                    LstVolsDec = scenarioSelectionne.lstVolsDec;

                    OnPropertyChanged("ScenarioSelectionne");
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private List<string> lstVolsAtt;
        public List<string> LstVolsAtt
        {
            get { return lstVolsAtt; }
            set
            {
                lstVolsAtt = value;
                OnPropertyChanged("LstVolsAtt");
            }
        }

        private List<string> lstVolsDec;
        public List<string> LstVolsDec
        {
            get { return lstVolsDec; }
            set
            {
                lstVolsDec = value;
                OnPropertyChanged("LstVolsDec");
            }
        }

        private ObservableCollection<Scenario> sommaireScenarios;
        public ObservableCollection<Scenario> SommaireScenarios
        {
            get
            {
                return sommaireScenarios;
            }
            set
            {
                sommaireScenarios = value;
                OnPropertyChanged("SommaireScenarios");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
