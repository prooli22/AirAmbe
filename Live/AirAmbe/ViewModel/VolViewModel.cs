// Nom: Anthony Massé
// Date: 9 Décembre 2016
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

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public VolViewModel()
        {
            Vol_Service = new VolAS();
            SommaireVols = Vol_Service.RecupererTous();
            cmdVider = new Commande(ActionVider);
            cmdModifier = new Commande(ActionModifier);
            cmdAjouter = new Commande(ActionAjouter);
            cmdSupprimer = new Commande(ActionSupprimer);
        }

        /// <summary>
        /// Une méthode pour vider
        /// </summary>
        private void ActionVider(object param)
        {
            VolSelectionne = null;
        }

        /// <summary>
        /// Une méthode pour ajouter
        /// </summary>
        private void ActionAjouter(object param)
        {
            Vol v = new Vol();

            v.ModeleAvion = ModeleAvion;
            v.Aeroport = Aeroport;
            v.NumeroVol = NumeroVol;
            v.EstAtterrissage = EstAtterissage;
         

            SommaireVols.Add(v);
            Vol_Service.Inserer(v);
        }

        /// <summary>
        /// Une méthode pour supprimer
        /// </summary>
        private void ActionSupprimer(object param)
        {
            Vol_Service.Supprimer(VolSelectionne.IdVol);
            SommaireVols.Remove(VolSelectionne);
        }

        /// <summary>
        /// Une méthode pour modifier
        /// </summary>
        private void ActionModifier(object param)
        {
            Vol v = new Vol();

            v.IdVol = VolSelectionne.IdVol;
            v.ModeleAvion = VolSelectionne.ModeleAvion;
            v.Aeroport = VolSelectionne.Aeroport;
            v.NumeroVol = VolSelectionne.NumeroVol;
            v.EstAtterrissage = VolSelectionne.EstAtterrissage;

            Vol_Service.Modifier(v);
            SommaireVols = Vol_Service.RecupererTous();
            VolSelectionne = v;

        }

        private Vol volSelectionne;

        /// <summary>
        /// Une méthode pour gérer les vols
        /// </summary>
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
                    EstAtterissage = volSelectionne.EstAtterrissage;

                    OnPropertyChanged("VolSelectionne");
                }
            }
        }


        private string modeleAvion;

        /// <summary>
        /// Une méthode pour gérer le modèle des avions
        /// </summary>
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

        /// <summary>
        /// Une méthode pour gérer les aéroports
        /// </summary>
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

        /// <summary>
        /// Une méthode pour gérer les numéros de vol
        /// </summary>
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

        /// <summary>
        /// Une méthode pour les atterrissages
        /// </summary>
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
