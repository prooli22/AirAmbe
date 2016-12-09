// Nom: Anthony Massé
// Date: 9 Décembre 2016

using System;
using AirAmbe.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirAmbe.ViewModel
{
    public class UtilisateurViewModel : INotifyPropertyChanged
    {
        public ICommand cmdVider { get; set; }
        public ICommand cmdAjouter { get; set; }
        public ICommand cmdModifier { get; set; }
        public ICommand cmdSupprimer { get; set; }

        private UtilisateurAS Utilisateur_Service;

        /// <summary>
        /// Constructeur de la classe 
        /// </summary>
        public UtilisateurViewModel()
        {
            Utilisateur_Service = new UtilisateurAS();
            SommaireUtilisateurs = Utilisateur_Service.RecupererTous();
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
            UtilisateurSelectionne = null;
        }

        /// <summary>
        /// Une méthode pour ajouter
        /// </summary>
        private void ActionAjouter(object param)
        {
            Utilisateur u = new Utilisateur();

            u.Adresse = Adresse;
            u.TypeUtilisateur = TypeUtilisateur;
            u.NomUtilisateur = NomUtilisateur;
            u.MotPasse = MotPasse;
            u.Prenom = Prenom;
            u.Nom = Nom;
            u.Poste = Poste;
            u.DateEmbauche = DateEmbauche;
            u.Telephone = Telephone;
            u.Courriel = Courriel;
            //Manque attribut Photo

            SommaireUtilisateurs.Add(u);
            Utilisateur_Service.Inserer(u);
        }

        /// <summary>
        /// Une méthode pour supprimer
        /// </summary>
        private void ActionSupprimer(object param)
        {
            Utilisateur_Service.Supprimer(UtilisateurSelectionne.IdUtilisateur);
            SommaireUtilisateurs.Remove(UtilisateurSelectionne);
        }

        /// <summary>
        /// Une méthode pour modifier
        /// </summary>
        private void ActionModifier(object param)
        {
            Utilisateur u = new Utilisateur();

            u.IdUtilisateur = UtilisateurSelectionne.IdUtilisateur;
            u.Adresse = Adresse;
            u.TypeUtilisateur = TypeUtilisateur;
            u.NomUtilisateur = NomUtilisateur;
            u.MotPasse = MotPasse;
            u.Prenom = Prenom;
            u.Nom = Nom;
            u.Poste = Poste;
            u.DateEmbauche = DateEmbauche;
            u.Telephone = Telephone;
            u.Courriel = Courriel;
            //Manque attribut Photo

            Utilisateur_Service.Modifier(u);
            SommaireUtilisateurs = Utilisateur_Service.RecupererTous();
            UtilisateurSelectionne = u;
        }

        private Utilisateur utilisateurSelectionne;

        /// <summary>
        /// Une méthode pour gérer les utilisateurs
        /// </summary>
        public Utilisateur UtilisateurSelectionne
        {
            get { return utilisateurSelectionne; }
            set
            {
                if (value == null)
                {
                    utilisateurSelectionne = null;
                    Adresse = null;
                    TypeUtilisateur = null;
                    NomUtilisateur = null;
                    MotPasse = null;
                    Prenom = null;
                    Nom = null;
                    Poste = null;
                    DateEmbauche = new DateTime(0000 - 00 - 00);
                    Telephone = null;
                    courriel = null;
                    //Manque attribut Photo

                }
                else
                {
                    utilisateurSelectionne = value;
                    Adresse = utilisateurSelectionne.Adresse;
                    TypeUtilisateur = utilisateurSelectionne.TypeUtilisateur;
                    NomUtilisateur = utilisateurSelectionne.NomUtilisateur;
                    MotPasse = utilisateurSelectionne.MotPasse;
                    Prenom = utilisateurSelectionne.Prenom;
                    Nom = utilisateurSelectionne.Nom;
                    Poste = utilisateurSelectionne.Poste;
                    DateEmbauche = utilisateurSelectionne.DateEmbauche;
                    Telephone = utilisateurSelectionne.Telephone;
                    courriel = utilisateurSelectionne.Courriel;
                    //Manque attribut Photo

                    OnPropertyChanged("UtilisateurSelectionne");
                }

            }
        }


        private string adresse;

        /// <summary>
        /// Une méthode pour gérer les adresses
        /// </summary>
        public string Adresse
        {
            get { return adresse; }
            set
            {
                adresse = value;
                OnPropertyChanged("Adresse");
            }
        }

        private string typeUtilisateur;

        /// <summary>
        /// Une méthode pour gérer le type des utilisateurs
        /// </summary>
        public string TypeUtilisateur
        {
            get { return typeUtilisateur; }
            set
            {
                typeUtilisateur = value;
                OnPropertyChanged("TypeUtilisateur");
            }
        }

        private string nomUtilisateur;

        /// <summary>
        /// Une méthode pour gérer le nom des utilisateurs
        /// </summary>
        public string NomUtilisateur
        {
            get { return nomUtilisateur; }
            set
            {
                nomUtilisateur = value;
                OnPropertyChanged("NomUtilisateur");
            }
        }

        private string motPasse;

        /// <summary>
        /// Une méthode pour gérer les mot des passe
        /// </summary>
        public string MotPasse
        {
            get { return motPasse; }
            set
            {
                motPasse = value;
                OnPropertyChanged("MotPasse");
            }
        }



        private string prenom;

        /// <summary>
        /// Une méthode pour gérer les prénom
        /// </summary>
        public string Prenom
        {
            get { return prenom; }
            set
            {
                prenom = value;
                OnPropertyChanged("Prenom");
            }
        }


        private string nom;

        /// <summary>
        /// Une méthode pour gérer le nom
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set
            {
                nom = value;
                OnPropertyChanged("Nom");
            }
        }


        private string poste;

        /// <summary>
        /// Une méthode pour gérer le poste
        /// </summary>
        public string Poste
        {
            get { return poste; }
            set
            {
                poste = value;
                OnPropertyChanged("Poste");
            }
        }

        private DateTime dateEmbauche;

        /// <summary>
        /// Une méthode pour gérer la date d'embauche
        /// </summary>
        public DateTime DateEmbauche
        {
            get { return dateEmbauche; }
            set
            {
                dateEmbauche = value;
                OnPropertyChanged("DateEmbauche");
            }
        }


        private string telephone;

        /// <summary>
        /// Une méthode pour gérer le téléphone
        /// </summary>
        public string Telephone
        {
            get { return telephone; }
            set
            {
                telephone = value;
                OnPropertyChanged("Telephone");
            }
        }


        private string courriel;

        /// <summary>
        /// Une méthode pour gérer les courriels
        /// </summary>
        public string Courriel
        {
            get { return courriel; }
            set
            {
                courriel = value;
                OnPropertyChanged("Courriel");
            }
        }

        private ObservableCollection<Utilisateur> sommaireUtilisateurs;

        /// <summary>
        /// Une méthode pour gérer le sommaire des utilisateurs
        /// </summary>
        public ObservableCollection<Utilisateur> SommaireUtilisateurs
        {
            get
            {
                return sommaireUtilisateurs;
            }
            set
            {
                sommaireUtilisateurs = value;
                OnPropertyChanged("SommaireUtilisateurs");
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
