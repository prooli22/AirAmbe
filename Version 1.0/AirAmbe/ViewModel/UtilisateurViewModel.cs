﻿using System;
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


        public UtilisateurViewModel()
        {
            Utilisateur_Service = new UtilisateurAS();
            SommaireUtilisateurs = Utilisateur_Service.RecupererTous();
            cmdVider = new Commande(ActionVider);
            cmdModifier = new Commande(ActionModifier);
            cmdAjouter = new Commande(ActionAjouter);
            cmdSupprimer = new Commande(ActionSupprimer);
        }

        private void ActionVider(object param)
        {
            UtilisateurSelectionne = null;
        }

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
        private void ActionSupprimer(object param)
        {
            Utilisateur_Service.Supprimer(UtilisateurSelectionne.IdUtilisateur);
            SommaireUtilisateurs.Remove(UtilisateurSelectionne);
        }
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
