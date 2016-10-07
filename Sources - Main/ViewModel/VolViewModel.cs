using System;
using AirAmbe.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirAmbe.ViewModel
{  
    public class VolViewModel : INotifyPropertyChanged
    {
        public ICommand cmdVider { get; set; }
        public ICommand cmdAjouter { get; set; }
        public ICommand cmdModifier { get; set; }
        public ICommand cmdSupprimer { get; set; }

        private VolAS Vol_Service;

        public VolViewModel()
        {
            Vol_Service = new VolAS();
            SommaireVols = Vol_Service.RecupererTous();
            cmdVider = new Commande(ActionVider);
            cmdModifier = new Commande(ActionModifier);
            cmdAjouter = new Commande(ActionAjouter);
            cmdSupprimer = new Commande(ActionSupprimer);
        }

        private void ActionVider(object param)
        {
            VolSelectionne = null;
        }

        private void ActionAjouter(object param)
        {
            Vol v = new Vol();

            v.ModeleAvion = ModeleAvion;
            v.Aeroport = Aeroport;
            v.NumeroVol = NumeroVol;
            v.EstAtterissage = EstAtterissage;
         

            SommaireVols.Add(v);
            Vol_Service.Inserer(v);
        }

        private void ActionSupprimer(object param)
        {
            Vol_Service.Supprimer(VolSelectionne.IdVol);
            SommaireVols.Remove(VolSelectionne);
        }

        private void ActionModifier(object param)
        {
            Vol v = new Vol();

            v.IdVol = VolSelectionne.IdVol;
            v.ModeleAvion = VolSelectionne.ModeleAvion;
            v.Aeroport = VolSelectionne.Aeroport;
            v.NumeroVol = VolSelectionne.NumeroVol;
            v.EstAtterissage = VolSelectionne.EstAtterissage;

            Vol_Service.Modifier(v);
            SommaireVols = Vol_Service.RecupererTous();
            VolSelectionne = v;

        }

        private Vol volSelectionne;
        public Vol VolSelectionne
        {
            get { return volSelectionne; }
            set
            {
                if (value == null)
                {
                    volSelectionne = null;
                    ModeleAvion = null;
                    Aeroport = null;
                    NumeroVol = null;
                    EstAtterissage = false;
                }
                else
                {
                    volSelectionne = value;
                    ModeleAvion = volSelectionne.ModeleAvion;
                    Aeroport = volSelectionne.Aeroport;
                    NumeroVol = volSelectionne.NumeroVol;
                    EstAtterissage = volSelectionne.EstAtterissage;

                    OnPropertyChanged("VolSelectionne");
                }
            }
        }








        private string modeleAvion;
        public string ModeleAvion
        {
            get { return modeleAvion; }
            set
            {
                modeleAvion = value;
                OnPropertyChanged("ModeleAvion");
            }
        }

        private string aeroport;
        public string Aeroport
        {
            get { return aeroport; }
            set
            {
                aeroport = value;
                OnPropertyChanged("Aeroport");
            }
        }

        private string numeroVol;
        public string NumeroVol
        {
            get { return numeroVol; }
            set
            {
                numeroVol = value;
                OnPropertyChanged("NumeroVol");
            }
        }

        private bool estAtterissage;
        public bool EstAtterissage
        {
            get { return estAtterissage; }
            set
            {
                estAtterissage = value;
                OnPropertyChanged("EstAtterissage");
            }
        }

        private ObservableCollection<Vol> sommaireVols;
        public ObservableCollection<Vol> SommaireVols
        {
            get
            {
                return sommaireVols;
            }
            set
            {
                sommaireVols = value;
                OnPropertyChanged("SommaireVols");
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
