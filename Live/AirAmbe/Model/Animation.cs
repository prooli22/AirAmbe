using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AirAmbe
{
    /// <summary>
    /// Classe Animation pour gérer les déplacements des avion sur la carte
    /// À LIRE:
    ///     Définition des termes employés:
    ///         -Voie de service: Le chemin entre la fin d'une piste et le début de la voie principale
    ///         -Voie principale: Le contour de l'aéroport, c'est-à-dire le chemin que toutes les avions empruntent
    ///         
    /// </summary>
    public class Animation
    {
        //Déclaration des constantes de la classe Animation
        public float Vitesse { get; set;}

        public float VitesseAeroport { get; set; }

        public float TempsAttentePiste { get; set; }

        //Déclaration des attributs de la classe Animation
 
        private DispatcherTimer GereDroit;

        private DispatcherTimer GereGauche;

        private DispatcherTimer GereHaut;

        private DispatcherTimer GereBas;

        private EcranControleur Ec { get; set; }

        private float CoordY { get; set; }

        private float CoordX { get; set; }

        private bool EstAtterrissage { get; set; }

        private int FinPiste { get; set; }

        private int FinVoieService { get; set; }

        private int LongueurVerticale {get; set;}

        private int LongueurHorizontale { get; set; }

        private int DistanceAParcourirVoiePrincip { get; set; }

        private int PositionYDebutVoiePrincip { get; set; }

        private int LongueurHorizontaleVs { get; set; }

        private int NumeroHangarDisponible { get; set; }

        private int NumeroAvionDisponible { get; set; }

        private Rectangle ImageAvion { get; set; }

        private int Angle { get; set; }

        private int AngleDesire { get; set; }

        private bool Operation { get; set; }
        
        /// <summary>
        /// Constructeur de la classe Animation
        /// </summary>    
        public Animation(EcranControleur ec)
        {
            Ec = ec;
            Vitesse = 4.5F;
            VitesseAeroport = 2F;
            TempsAttentePiste = 4000;
        }

        /// <summary>
        /// Une méthode pour dessiner les pistes de l'aéroport
        /// </summary>   
        public void GererDessinPiste(int NbPistes)
        {
            //Une boucle pour dessiner les pistes
            for (int i = 1; i <=5 ; i++)
            {
                ImageBrush ImagePiste = new ImageBrush();
                ImagePiste.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/piste" + i + ".png"));

                switch(i)
                {
                    case 1:
                        DessinerPiste1(ImagePiste);
                        break;
                    case 2:
                        DessinerPiste2(ImagePiste);
                        break;
                    case 3:
                        DessinerPiste3(ImagePiste);
                        break;
                    case 4:
                        DessinerPiste4(ImagePiste);
                        break;
                    case 5:
                        DessinerPiste5(ImagePiste);
                        break;               
                }
            }              
        }

        /// <summary>
        /// Une méthode pour dessiner la piste 1
        /// </summary>
        public void DessinerPiste1(ImageBrush ib)
        {
            //Déclaration des variables
            Rectangle rectRotation = new Rectangle();
            RotateTransform rotation = new RotateTransform(90, 14, 14);

            Ec.Piste1.RenderTransform = rotation;
            Ec.Piste1.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Piste1.StrokeThickness = 2;
            Ec.Piste1.Fill = ib;
            Ec.Piste1.Width = 330;
            Ec.Piste1.Height = 40;
     
            Canvas.SetLeft(Ec.Piste1, 45);
            Canvas.SetTop(Ec.Piste1,75);           
        }

        /// <summary>
        /// Une méthode pour dessiner la piste 4 
        /// </summary>
        public void DessinerPiste2(ImageBrush ib)
        {
            //Déclaration des variables
            Rectangle rectRotation = new Rectangle();
            RotateTransform rotation = new RotateTransform(180, 14, 14);

            Ec.Piste4.RenderTransform = rotation;
            Ec.Piste4.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Piste4.StrokeThickness = 2;
            Ec.Piste4.Fill = ib;
            Ec.Piste4.Width = 450;
            Ec.Piste4.Height = 40;

            Canvas.SetLeft(Ec.Piste4, 792);
            Canvas.SetTop(Ec.Piste4, 380);
        }
        
        /// <summary>
        /// Une méthode pour dessiner la piste 3 
        /// </summary>
        public void DessinerPiste3(ImageBrush ib)
        {
            //Déclaration des variables
            Rectangle rectRotation = new Rectangle();
            RotateTransform rotation = new RotateTransform(90, 14, 14);

            Ec.Piste3.RenderTransform = rotation;
            Ec.Piste3.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Piste3.StrokeThickness = 2;
            Ec.Piste3.Fill = ib;
            Ec.Piste3.Width = 330;
            Ec.Piste3.Height = 40;

            Canvas.SetLeft(Ec.Piste3, 120);
            Canvas.SetTop(Ec.Piste3, 75);
        }

        /// <summary>
        /// Une méthode pour dessiner la piste 2 
        /// </summary>
        public void DessinerPiste4(ImageBrush ib)
        {
            Rectangle rectRotation = new Rectangle();
            RotateTransform rotation = new RotateTransform(180, 14, 14);

            Ec.Piste2.RenderTransform = rotation;
            Ec.Piste2.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Piste2.StrokeThickness = 2;
            Ec.Piste2.Fill = ib;
            Ec.Piste2.Width = 450;
            Ec.Piste2.Height = 40;

            Canvas.SetLeft(Ec.Piste2, 792);
            Canvas.SetTop(Ec.Piste2, 435);
        }

        /// <summary>
        /// Une méthode pour dessiner la piste 5 
        /// </summary>
        public void DessinerPiste5(ImageBrush ib)
        {
            //Déclaration des variables
            Rectangle rectRotation = new Rectangle();
            RotateTransform rotation = new RotateTransform(180, 14, 14);

            Ec.Piste5.RenderTransform = rotation;
            Ec.Piste5.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Piste5.StrokeThickness = 2;
            Ec.Piste5.Fill = ib;
            Ec.Piste5.Width = 450;
            Ec.Piste5.Height = 40;

            Canvas.SetLeft(Ec.Piste5, 737);
            Canvas.SetTop(Ec.Piste5, 25);      
        }

        /// <summary>
        /// Une méthode pour dessiner le hangar sur l'aéroport
        /// </summary>
        public void DessinerHangar()
        {
            Ec.hangar.Stroke = new SolidColorBrush(Colors.Black);
            Ec.hangar.StrokeThickness = 2;
            Ec.hangar.Width = 750;
            Ec.hangar.Height = 290;

            Canvas.SetLeft(Ec.hangar, 160);
            Canvas.SetTop(Ec.hangar, 65);
            Canvas.SetZIndex(Ec.hangar, 100);
        }

        /// <summary>
        /// Une méthode pour changer l'opacité sur les pistes
        /// </summary>
        public void ChangerOpacitePiste(Piste piste)
        {
            //Déclaration des variables
            double opaciteDisable = 0.3;
            double opaciteEnable = 1;

            switch (piste.NumPiste)
            {
                case 1:
                    if (piste.estDisponible)
                    {
                        Ec.Piste1.Opacity = opaciteEnable;
                    }
                    else
                    {
                        Ec.Piste1.Opacity = opaciteDisable;
                    }
                    break;

                case 2:
                    if (piste.estDisponible)
                    {
                        Ec.Piste4.Opacity = opaciteEnable;
                    }
                    else
                    {
                        Ec.Piste4.Opacity = opaciteDisable;
                    }
                    break;

                case 3:
                    if (piste.estDisponible)
                    {
                        Ec.Piste3.Opacity = opaciteEnable;
                    }
                    else
                    {
                        Ec.Piste3.Opacity = opaciteDisable;
                    }
                    break;

                case 4:
                    if (piste.estDisponible)
                    {
                        Ec.Piste2.Opacity = opaciteEnable;
                    }
                    else
                    {
                        Ec.Piste2.Opacity = opaciteDisable;
                    }
                    break;

                case 5:
                    if (piste.estDisponible)
                    {
                        Ec.Piste5.Opacity = opaciteEnable;
                    }
                    else
                    {
                        Ec.Piste5.Opacity = opaciteDisable;
                    }
                    break;
            }
        }

        /// <summary>
        /// Une méthode pour tester la disponibilité des hangars
        /// </summary>
        public int TesterDisponibiliteHangar()
        {
            //Pour chaque hangar
            foreach (Hangar h in Ec.LstHangar)
            {
                if (h.EstDisponible)
                {
                    h.EstDisponible = false;
                    return h.NumHangar;
                }
            }
            return 13;
        }
            
        /// <summary>
        /// Une méthode pour tester la disponibilité des avions
        /// </summary>
        public int TesterDisponibiliteAvion()
        {
            //Pour chaque avion
            foreach (Avion a in Ec.LstAvion)
            {
                if (a.EstDisponibleAtterrissage)
                {
                    a.EstDisponibleAtterrissage = false;
                    return a.NumAvion;
                }
            }
            return 13;
        }

        /// <summary>
        /// Une méthode pour gérer l'aiguillage des avions
        /// </summary>
        public void GererAiguillageAvion(Rectangle Avion, int angle, int angleDesire, bool operation)
        {
            //System.Drawing.Graphics g;

            //g = System.Drawing.Graphics.FromImage();
            while (angle != angleDesire)
            {
                RotateTransform aiguillageAvion = new RotateTransform(angle, 14, 14);
                Avion.RenderTransform = aiguillageAvion;
                
                if (operation)
                {
                    angle += 1;
                }else
                {
                    angle -= 1;
                }

             
            }        
        }


        ///------------------------------------------------------------------------------------------------------------------------------------------
        ///DÉBUT DE L'APPLICATION:
        /// <summary>
        /// Une méthode pour disperser les avions dans les hangars selon le nombre de décollage au début de l'application
        /// </summary>
        public void DisperserDecollageHangar()
        {
            int hangar = 0;
            //Pour chaque vol dans la liste de decollage                         
            foreach (Vol v in Ec.LstDecollages)
            {
                //Pour chaque avion dans la liste d'avion                 
                foreach (Avion a in Ec.LstAvion)
                {
                    //Si l'avion est disponible au décollage et qu'elle n'est pas initialisée à un décollage
                    if (a.EstDisponibleDecollage && a.EstInitialisee == false)
                    {
                        Ec.LstHangar[hangar].NumHangar = hangar + 1;
                        Ec.LstHangar[hangar].EstDisponible = false;
                        DessinerAvionDecollage(v.IdVol, Ec.LstHangar[hangar].NumHangar, a);

                        hangar++;
                    }
                }
            }
        }

        /// <summary>
        /// Une méthode pour préparer les avions à un décollage en les dessinant
        /// </summary>
        public void DessinerAvionDecollage(int idVol, int noHangar, Avion av)
        {
            //Déclaration des variables
            Rectangle avionD = new Rectangle();
            Rectangle rectRotation = new Rectangle();
            RotateTransform aiguillageDroit = new RotateTransform(90, 14, 14);
            RotateTransform aiguillageGauche = new RotateTransform(270, 14, 14);
            RotateTransform aiguillageBas = new RotateTransform(180, 14, 14);
            RotateTransform aiguillageHaut = new RotateTransform(360, 14, 14);
            ImageBrush a = new ImageBrush();

            //Initialisation des variables
            a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/avion.png"));
            avionD.Fill = a;
            avionD.Width = 35;
            avionD.Height = 35;
            Canvas.SetZIndex(avionD, 100);

            int coordY = 0;
            int coordX = 0;

            //Selon le numéro de la piste on initialise les coordonnées de départ ainsi que l'orientation de l'avion
            switch (noHangar)
            {
                case 1:
                    coordX = 813;
                    coordY = 230;
                    break;

                case 2:
                    coordX = 730;
                    coordY = 230;
                    break;

                case 3:
                    coordX = 648;
                    coordY = 230;
                    break;

                case 4:
                    coordX = 568;
                    coordY = 230;
                    break;

                case 5:
                    coordX = 485;
                    coordY = 230;
                    break;

                case 6:
                    coordX = 403;
                    coordY = 230;
                    break;

                case 7:
                    coordX = 410;
                    coordY = 160;
                    break;

                case 8:
                    coordX = 492;
                    coordY = 160;
                    break;

                case 9:
                    coordX = 575;
                    coordY = 160;
                    break;

                case 10:
                    coordX = 655;
                    coordY = 160;
                    break;

                case 11:
                    coordX = 737;
                    coordY = 160;
                    break;

                case 12:
                    coordX = 820;
                    coordY = 160;
                    break;
            }

            //Selon les hangars, les avions ne seront pas orienté du même sens
            if (noHangar == 1 || noHangar == 2 || noHangar == 3 || noHangar == 4 || noHangar == 5 || noHangar == 6)
            {
                avionD.RenderTransform = aiguillageHaut;
            }
            else
            {
                avionD.RenderTransform = aiguillageBas;
            }

            //On dessine l'avion en fonction des coordonnées de la piste
            Canvas.SetLeft(avionD, coordX);
            Canvas.SetTop(avionD, coordY);

            //On initialise les coordonnées de l'avion qui décolle
            av.CoordX = coordX;
            av.CoordY = coordY;
            av.HangarAssigne = noHangar;
            av.EstInitialisee = true;   //L'avion est maintenant initialisée
            av.ImageAvion = avionD;

            //On ajoute l'avion au canvas, mais ce n'est pas à ce moment qu'elle devra bouger
            Ec.cnvCarte.Children.Add(av.ImageAvion);
        }


        //------------------------------------------------------------------------------------------------------------------------------------------------
        ///ATTERRISSAGE:
        /// <summary>
        /// Une méthode qui permet de démarrer un atterrissage
        /// </summary>
        public void DemarreAtterrissage(int piste)
        {
            //Déclaration des variables          
            Rectangle rectRotation = new Rectangle();
            ImageBrush a = new ImageBrush();
            RotateTransform aiguillageDroit = new RotateTransform(90, 14, 14);
            RotateTransform aiguillageGauche = new RotateTransform(270, 14, 14);
            RotateTransform aiguillageBas = new RotateTransform(180, 14, 14);
            RotateTransform aiguillageHaut = new RotateTransform(360, 14, 14);          

            //Pour chaque avion
            foreach(Avion avionA in Ec.LstAvion)
            {
                if(avionA.NumAvion==NumeroAvionDisponible)
                {
                    ImageAvion = avionA.ImageAvion;
                }
            }

            //On initialise les variables
            ImageAvion = new Rectangle();
            a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/avion.png"));
            ImageAvion.Fill = a;
            ImageAvion.Width = 35;
            ImageAvion.Height = 35;
            Canvas.SetZIndex(ImageAvion, 100);

            int coordY = 0;
            int coordX = 0;
            int finPiste = 0;
            int finVoieService = 0;

            //On test les hangars ainsi que les pistes disponibles
            NumeroHangarDisponible = TesterDisponibiliteHangar();
            NumeroAvionDisponible = TesterDisponibiliteAvion();
            
            //Selon le numéro de la piste on initialise les coordonnées de départ ainsi que l'orientation de l'avion
            switch (piste)
            {
                case 1:
                    coordX = 42;
                    coordY = -500;
                    finPiste = 440;
                    finVoieService = 210;                       
                    PositionYDebutVoiePrincip = 332;
                    ImageAvion.RenderTransform = aiguillageBas;
                    break;

                case 2:
                    coordX = 1700;
                    coordY = 378;
                    finPiste = 330;                         
                    PositionYDebutVoiePrincip = 332;
                    ImageAvion.RenderTransform = aiguillageGauche;
                    break;

                case 3:

                    coordX = 118;
                    coordY = -500;
                    finPiste = 405;
                    finVoieService = 160;                        
                    PositionYDebutVoiePrincip = 332;
                    ImageAvion.RenderTransform = aiguillageBas;
                    break;

                case 4:
                    coordX = 1700;
                    coordY = 432;
                    finPiste = 280;
                    PositionYDebutVoiePrincip = 332;
                    ImageAvion.RenderTransform = aiguillageGauche;
                    break;

                case 5:
                    coordX = 1700;
                    coordY = 22;
                    finPiste = 325;
                    PositionYDebutVoiePrincip = 62;
                    ImageAvion.RenderTransform = aiguillageGauche;
                    break;
            }

           //On dessine l'avion en fonction des coordonnées de la piste
           Canvas.SetLeft(ImageAvion, coordX);
           Canvas.SetTop(ImageAvion, coordY);

           //On ajoute l'avion au canvas
           Ec.cnvCarte.Children.Add(ImageAvion);

           //On initialise les attributs de la classe
           CoordY = coordY;
           CoordX = coordX;
           EstAtterrissage = true;           
           FinPiste = finPiste;
           FinVoieService = finVoieService;

           //On détermine la longeur maximale à atteindre
           if (piste == 1 || piste == 3)
           {
               LongueurHorizontale = 500;
               LongueurVerticale = 500;
           }
           else
           {
               LongueurHorizontale = 0;
               LongueurVerticale = 500;
           }

           //On appelle la fonction d'atterrissage en fonction de sa piste, son numéro de hangar ainsi que le numéro de l'avion
           GererAtterrissage(ImageAvion, piste, NumeroHangarDisponible,NumeroAvionDisponible);         
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Une méthode pour gérer les atterrissages uniquement 
        /// </summary>
        public void GererAtterrissage(Rectangle imageAvion, int piste, int hangar, int avion)
        {
            //Déclaration des variables
            GereBas = new DispatcherTimer();
            GereGauche = new DispatcherTimer();
         
            //Si il s'agit de la piste 1 ou 3, on génère un mouvement vers le bas
            if(piste==1 || piste ==3)
            {
                //Pour générer un déplacement vers la gauche ainsi que vers le bas pour les atterrissages
                GereBas.Start();
                GereBas.Tick += new EventHandler((sender, e) => GenererMouvHBPisteAtteri(sender, e, imageAvion, piste,hangar, avion));
                GereBas.Interval = TimeSpan.FromMilliseconds(50);
            }else if(piste ==2 || piste ==4 || piste ==5)   //Si il s'agit de la piste 2, 4 ou 5 on génère un mouvement vers la gauche
            {
                //Pour générer un déplacement vers la gauche ainsi que vers le bas pour les atterrissages
                GereGauche.Start();
                GereGauche.Tick += new EventHandler((sender, e) => GenererMouvGDVoieServ(sender, e, imageAvion, piste,hangar, avion));
                GereGauche.Interval = TimeSpan.FromMilliseconds(50);
            }      
        }

        /// <summary>
        /// Une méthode pour générer un mouvement de haut ou un mouvement de bas pour l'atterrissage de la piste 1 et 3 uniquement
        /// </summary>
        public void GenererMouvHBPisteAtteri(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            //Si il s'agit de la piste 1 ou 3
            if (piste == 1 || piste == 3)
            {
                if (CoordY <= LongueurVerticale)
                {                
                    if (CoordY >= FinPiste)
                    {
                        //On arrete le déplacement en cours
                        GereBas.Stop();
                   
                        //On commence le nouveau déplacement
                        GereDroit = new DispatcherTimer();

                        GereDroit.Start();
                        GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvGDVoieServ(sender, e, imageAvion, piste, hangar, avion));
                        GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {
                        if (CoordY >= 180)
                        {                                                      
                             CoordY += VitesseAeroport;
                            GereBas.Interval = TimeSpan.FromMilliseconds(50);
                        }
                        else
                        {
                            CoordY += Vitesse;
                        } 
                        if(CoordY == 360)
                        {
                            //Pour simuler un arret de l'avion
                            GereBas.Interval = TimeSpan.FromMilliseconds(TempsAttentePiste);
                        }                    
                    }
                }
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement de gauche ou un mouvement de droite selon la piste sur la voie de service
        /// </summary>
        public void GenererMouvGDVoieServ(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            //Si il s'agit de la piste 1 ou 3
            if (piste == 1 || piste == 3)
            {
                //Pour gérer l'aiguillage de l'avion
                Angle = 180;
                AngleDesire = 90;
                Operation = false;
                GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

                if (CoordX <= LongueurHorizontale)
                {
                    if (CoordX >= FinVoieService)
                    {
                        //On arrete le déplacement en cours
                        GereDroit.Stop();
                   
                        //On commence le nouveau déplacement
                        GereHaut = new DispatcherTimer();

                        GereHaut.Start();
                        GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHBVoieServ(sender, e, imageAvion, piste, hangar, avion));
                        GereHaut.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {

                        CoordX += VitesseAeroport;
                    }
                }

                //On génère le mouvement GAUCHE ou DROIT 
                Canvas.SetLeft(imageAvion, CoordX);
            }
            else if (piste == 2 || piste == 4 || piste == 5)    //Si il s'agit de la piste 2 ou 4 ou 5
            {
              
                if (CoordX >= LongueurHorizontale)
                {

                    if (CoordX <= FinPiste)
                    {

                        //On arrete le déplacement en cours
                        GereGauche.Stop();

                        //Le prochain mouvement à générer est HAUT
                        GereHaut = new DispatcherTimer();
                        GereHaut.Start();
                        GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHBVoieServ(sender, e, imageAvion, piste, hangar, avion));
                        GereHaut.Interval = TimeSpan.FromMilliseconds(50);

                    }
                    else
                    {
                        if ((CoordX <= 650 && piste != 5) || (CoordX <= 575 && piste == 5))
                        {
                            CoordX -= VitesseAeroport;
                            GereGauche.Interval = TimeSpan.FromMilliseconds(50);
                        }
                        else
                        {
                            CoordX -= Vitesse;
                        }

                        if ((CoordX == 400 && piste != 5) || (CoordX == 340 && piste == 5))
                        {
                            //Pour simuler un arret de l'avion
                            GereGauche.Interval = TimeSpan.FromMilliseconds(TempsAttentePiste);
                        }

                    }
                }

                //On génère le mouvement GAUCHE ou DROIT
                Canvas.SetLeft(imageAvion, CoordX);
            }
        }

        /// <summary>
        /// Une méthode pour générer un mouvement de haut ou un mouvement de bas selon la piste sur la voie de service
        /// </summary>
        public void GenererMouvHBVoieServ(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            //Déclaration des variables
            int longueurVerticaleMax = 0;
            FinVoieService = 600;
            LongueurHorizontale = 600;

            //Si il s'agit de la piste 1 ou 3
            if (piste == 1 || piste == 3)
            {
                //Pour gérer l'aiguillage de l'avion
                Angle = 270;
                AngleDesire = 360;
                Operation = true;
                GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

                if (CoordY >= longueurVerticaleMax)
                {

                    if (CoordY <= PositionYDebutVoiePrincip)
                    {
                        //On arrete le mouvement en cours
                        GereHaut.Stop();
                        
                        GereDroit = new DispatcherTimer();

                        //Le prochain mouvement se fera dans la voie principale
                        GereDroit.Start();
                        GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvDVoiePrincip(sender, e, imageAvion, piste, hangar, avion));
                        GereDroit.Interval = TimeSpan.FromMilliseconds(50);

                    }
                    else
                    {
                        CoordY -= VitesseAeroport;
                    }
                }
                //On génère le mouvement BAS ou HAUT
                Canvas.SetTop(imageAvion, CoordY);
            }
            else if (piste == 2 || piste == 4)  //Si il s'agit de la piste 2 ou 4
            {
                //Pour gérer l'aiguillage de l'avion
                Angle = 90;
                AngleDesire = 0;
                Operation = false;
                GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

                if (CoordY >= longueurVerticaleMax)
                {

                    if (CoordY <= PositionYDebutVoiePrincip)
                    {
                        //On arrete le mouvement en cours
                        GereHaut.Stop();
                     
                        GereDroit = new DispatcherTimer();

                        //Le prochain mouvement se fera dans la voie principale
                        GereDroit.Start();
                        GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvDVoiePrincip(sender, e, imageAvion, piste, hangar, avion));
                        GereDroit.Interval = TimeSpan.FromMilliseconds(50);                                     
                    }
                    else
                    {
                        CoordY -= VitesseAeroport;
                    }                     
                }
                //On génère le mouvement BAS ou HAUT
                Canvas.SetTop(imageAvion, CoordY);
            }
            else if (piste == 5)    //Si il s'agit de la piste 5
            {
                longueurVerticaleMax = 500;

                //Pour gérer l'aiguillage de l'avion
                Angle = 90;
                AngleDesire = 180;
                Operation = true;
                GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

                if (CoordY <= longueurVerticaleMax)
                {
                    if (CoordY >= PositionYDebutVoiePrincip)
                    {
                        //On arrete le mouvement en cours
                        GereHaut.Stop();

                        GereGauche = new DispatcherTimer();
               
                        GereGauche.Start();
                        GereGauche.Tick += new EventHandler((senderA, eA) => GenererMouvGVoiePrincipDirectionB(sender, e, imageAvion, piste,hangar, avion));
                        GereGauche.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {
                        CoordY += VitesseAeroport;
                    }
                }
                //On génère le mouvement BAS ou HAUT
                Canvas.SetTop(imageAvion, CoordY);
            }         
        }


        ///--------------------------------------------------------------------------------------------------------------------------------------------
        /// UTILISÉ PAR ATTERRISSAGE ET DÉCOLLAGE
        /// <summary>
        /// Une méthode pour générer un mouvement DROIT sur la voie de principale autant utilisée par décollage ainsi qu'atterrissage
        /// </summary>
        public void GenererMouvDVoiePrincip(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            //On initialise les variables
            LongueurHorizontale = 1000;
            DistanceAParcourirVoiePrincip = 830;

            //Pour gérer l'aiguillage de l'avion
            Angle = 0;
            AngleDesire = 90;
            Operation = true;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            //Si il s'agit d'un décollage on change la distance à parcourir
            if (EstAtterrissage == false)
            {
                switch (piste)
                {
                    case 1:
                        DistanceAParcourirVoiePrincip = 210;
                        break;
                    case 2:
                        DistanceAParcourirVoiePrincip = 330;
                        break;
                    case 3:
                        DistanceAParcourirVoiePrincip = 160;
                        break;
                    case 4:
                        DistanceAParcourirVoiePrincip = 280;
                        break;
                }

                if (CoordX <= LongueurHorizontale)
                {

                    if (CoordX >= DistanceAParcourirVoiePrincip)
                    {

                        //On arrete le déplacement en cours
                        GereDroit.Stop();

                        ////On commence le nouveau déplacement
                        GereBas = new DispatcherTimer();

                        GereBas.Start();
                        GereBas.Tick += new EventHandler((senderA, eA) => GenererMouvBVoieServDirectionGD(sender, e, imageAvion, piste, hangar, avion));
                        GereBas.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {
                        CoordX += VitesseAeroport;
                    }
                 }
                                   
            }
            else //Si il s'agit d'un atterrissage
            {
                if (CoordX <= LongueurHorizontale)
                {

                    if (CoordX >= DistanceAParcourirVoiePrincip)
                    {

                        //On arrete le déplacement en cours
                        GereDroit.Stop();

                        //On commence le nouveau déplacement
                        GereHaut = new DispatcherTimer();

                        GereHaut.Start();
                        GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHVoiePrincipDirectionG(sender, e, imageAvion, piste, hangar, avion));
                        GereHaut.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {
                        CoordX += VitesseAeroport;
                    }
                }
            }
            
            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement DROIT sur la voie de principale
        /// </summary>
        public void GenererMouvDVoiePrincipDirectionH(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurHorizontale = 1000;
             
            //Selon le hangar 7-12         
            switch (hangar)
            {
                case 7:
                    DistanceAParcourirVoiePrincip = 410;
                    Angle = 180;
                    Operation = false;
                    break;
                case 8:
                    DistanceAParcourirVoiePrincip = 490;
                    Angle = 180;
                    Operation = false;
                    break;
                case 9:
                    DistanceAParcourirVoiePrincip = 572;
                    Angle = 180;
                    Operation = false;
                    break;
                case 10:
                    DistanceAParcourirVoiePrincip = 652;
                    Angle = 180;
                    Operation = false;
                    break;
                case 11:
                    DistanceAParcourirVoiePrincip = 734;
                    Angle = 180;
                    Operation = false;
                    break;
                case 12:
                    DistanceAParcourirVoiePrincip = 818;
                    Angle = 180;
                    Operation = false;
                    break;
                default:
                    DistanceAParcourirVoiePrincip = 828;
                    Angle = 0;
                    Operation = true;
                    break;
            }

            //Pour gérer l'aiguillage de l'avion
            AngleDesire = 90;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            if (CoordX <= LongueurHorizontale)
            {

                if (CoordX >= DistanceAParcourirVoiePrincip)
                {

                    //On arrete le déplacement en cours
                    GereDroit.Stop();

                    //On commence le nouveau déplacement
                    GereHaut = new DispatcherTimer();
                    GereBas = new DispatcherTimer();

                    //Si tous les hangars sont pris
                    if (hangar != 7 && hangar != 8 && hangar != 9 && hangar != 10 && hangar != 11 && hangar != 12)
                    {
                        GereHaut.Start();
                        GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHVoiePrincipDirectionGFinCycle(sender, e, imageAvion, piste, hangar, avion));
                        GereHaut.Interval = TimeSpan.FromMilliseconds(50);

                    }
                    else
                    {
                        GereBas.Start();
                        GereBas.Tick += new EventHandler((senderA, eA) => GenererMouvBVoiePrincipDirectionHangar(sender, e, imageAvion, piste, hangar, avion));
                        GereBas.Interval = TimeSpan.FromMilliseconds(50);
                    }
                }
                else
                {
                    CoordX += VitesseAeroport;
                }
            }
            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement GAUCHE sur la voie de principale
        /// </summary>
        public void GenererMouvGVoiePrincipDirectionB(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurHorizontale = 0;
            DistanceAParcourirVoiePrincip = 164;

            //Pour gérer l'aiguillage de l'avion
            Angle = 360;
            AngleDesire = 270;
            Operation = false;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            //Si il s'agit d'un décollage et que c'est la piste 5
            if (EstAtterrissage == false && piste==5)
            {
                DistanceAParcourirVoiePrincip = 220;

                if (CoordX <= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le déplacement en cours
                    GereGauche.Stop();

                    //Le prochain mouvement à générer est HAUT
                    GereHaut = new DispatcherTimer();
                    GereHaut.Start();
                    GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHVoieServDecollage(sender, e, imageAvion, piste, hangar, avion));
                    GereHaut.Interval = TimeSpan.FromMilliseconds(50);

                }
                else
                {
                    CoordX -= VitesseAeroport;
                }
            }
            else
            {

                if (CoordX >= LongueurHorizontale)
                {

                    if (CoordX <= DistanceAParcourirVoiePrincip)
                    {

                        //On arrete le déplacement en cours
                        GereGauche.Stop();

                        //Le prochain mouvement à générer est HAUT
                        GereBas = new DispatcherTimer();
                        GereBas.Start();
                        GereBas.Tick += new EventHandler((senderA, eA) => GenererMouvBVoiePrincip(sender, e, imageAvion, piste, hangar, avion));
                        GereBas.Interval = TimeSpan.FromMilliseconds(50);

                    }
                    else
                    {
                        CoordX -= VitesseAeroport;
                    }
                }
            }

            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement GAUCHE sur la voie de principale
        /// </summary>
        public void GenererMouvGVoiePrincipDirectionH(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            //Pour gérer l'aiguillage de l'avion
            Angle = 360;
            AngleDesire = 270;
            Operation = false;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            LongueurHorizontale = 0; 
           
            //Selon le hangar   
            switch(hangar)
            {
                case 1:
                    DistanceAParcourirVoiePrincip = 815;
                    break;
                case 2:
                    DistanceAParcourirVoiePrincip = 731;
                    break;
                case 3:
                    DistanceAParcourirVoiePrincip = 649;
                    break;
                case 4:
                    DistanceAParcourirVoiePrincip = 568;
                    break;
                case 5:
                    DistanceAParcourirVoiePrincip = 485;
                    break;
                case 6:
                    DistanceAParcourirVoiePrincip = 404;
                    break;           
                default:
                    DistanceAParcourirVoiePrincip = 318;
                    break;
            }
               
            if (CoordX >= LongueurHorizontale)
            {              
                if (CoordX <= DistanceAParcourirVoiePrincip)
                {
                    
                    //On arrete le déplacement en cours
                    GereGauche.Stop();

                    //Le prochain mouvement à générer est HAUT
                    GereHaut = new DispatcherTimer();
                    GereHaut.Start();
                    GereHaut.Interval = TimeSpan.FromMilliseconds(50);
                    //Si l'avion doit entrer dans un hangar
                    if(hangar!=1 && hangar != 2 && hangar != 3 && hangar != 4 && hangar != 5 && hangar != 6)
                    {
                        GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHVoiePrincipDirectionD(sender, e, imageAvion, piste, hangar, avion));
                    }
                    else
                    {
                        GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHVoiePrincipDirectionHangar(sender, e, imageAvion, piste, hangar, avion));
                    }                                    
                }
                else
                {
                    CoordX -= VitesseAeroport;
                }
            }

            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement BAS sur la voie de principale
        /// </summary>
        public void GenererMouvBVoiePrincip(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 1000;
            DistanceAParcourirVoiePrincip = 330;

            //Pour gérer l'aiguillage de l'avion
            Angle = 270;
            AngleDesire = 180;
            Operation = false;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            if (CoordY <= LongueurVerticale)
            {
               
                if (CoordY >= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le déplacement en cours
                    GereBas.Stop();
                    
                    //On commence le nouveau déplacement
                    GereDroit = new DispatcherTimer();

                    GereDroit.Start();
                    GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvDVoiePrincip(sender, e, imageAvion, piste, hangar, avion));
                    GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                }
                else
                {
                    CoordY += VitesseAeroport;
                }
            }
              
            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement BAS direction HANGAR
        /// </summary>
        public void GenererMouvBVoiePrincipDirectionHangar(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 1000;
            DistanceAParcourirVoiePrincip = 160;

            //Pour gérer l'aiguillage de l'avion
            Angle = 90;
            AngleDesire = 180;
            Operation = true;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            if (CoordY <= LongueurVerticale)
            {

                if (CoordY >= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le déplacement en cours
                    GereBas.Stop();

                    //L'avion est maintenant disponible pour un décollage
                    foreach (Avion a in Ec.LstAvion)
                    {
                        if (a.NumAvion == avion)
                        {
                            a.EstDisponibleDecollage = true;
                            a.HangarAssigne = hangar;
                            a.EstInitialisee = true;
                            a.CoordX = CoordX;
                            a.CoordY = CoordY;
                        }
                    }
                }
                else
                {
                    CoordY += VitesseAeroport;
                }
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de principale direction GAUCHE
        /// </summary>
        public void GenererMouvHVoiePrincipDirectionG(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 0;
            DistanceAParcourirVoiePrincip = 287;

            //Pour gérer l'aiguillage de l'avion
            Angle = 90;
            AngleDesire = 0;
            Operation = false;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            if (CoordY >= LongueurVerticale)
            {

                if (CoordY <= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le mouvement en cours
                    GereHaut.Stop();

                    GereGauche = new DispatcherTimer();

                    //Le prochain mouvement se fera dans la voie principale
                    GereGauche.Start();
                    GereGauche.Tick += new EventHandler((senderA, eA) => GenererMouvGVoiePrincipDirectionH(sender, e, imageAvion, piste, hangar, avion));
                    GereGauche.Interval = TimeSpan.FromMilliseconds(50);
                }
                CoordY -= VitesseAeroport;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de principale direction DROIT
        /// </summary>
        public void GenererMouvHVoiePrincipDirectionD(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 0;
            DistanceAParcourirVoiePrincip = 115;

            //Pour gérer l'aiguillage de l'avion
            Angle = 270;
            AngleDesire = 360;
            Operation = true;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            if (CoordY >= LongueurVerticale)
            {
                if (CoordY <= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le mouvement en cours
                    GereHaut.Stop();

                    GereDroit = new DispatcherTimer();

                    //Le prochain mouvement se fera dans la voie principale
                    GereDroit.Start();
                    GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvDVoiePrincipDirectionH(sender, e, imageAvion, piste, hangar, avion));
                    GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                }
                CoordY -= VitesseAeroport;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de principale direction HANGAR
        /// </summary>
        public void GenererMouvHVoiePrincipDirectionHangar(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 0;
            DistanceAParcourirVoiePrincip = 235;

            //Pour gérer l'aiguillage de l'avion
            Angle = 270;
            AngleDesire = 360;
            Operation = true;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            if (CoordY >= LongueurVerticale)
            {
                if (CoordY <= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le mouvement en cours
                    GereHaut.Stop();

                    //L'avion est maintenant disponible pour un décollage
                    foreach (Avion a in Ec.LstAvion)
                    {
                        if (a.NumAvion == avion)
                        {                         
                            a.EstDisponibleDecollage = true;
                            a.HangarAssigne = hangar;
                            a.EstInitialisee = true;           
                            a.CoordX = CoordX;
                            a.CoordY = CoordY;
                        }
                    }
                }
                CoordY -= VitesseAeroport;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de principale direction DROIT
        /// </summary>
        public void GenererMouvHVoiePrincipDirectionGFinCycle(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 0;
            DistanceAParcourirVoiePrincip = 70;

            //Pour gérer l'aiguillage de l'avion
            Angle = 90;
            AngleDesire = 0;
            Operation = false;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            if (CoordY >= LongueurVerticale)
            {
                if (CoordY <= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le mouvement en cours
                    GereHaut.Stop();

                    GereGauche = new DispatcherTimer();

                    //Le prochain mouvement se fera dans la voie principale
                    GereGauche.Start();
                    GereGauche.Tick += new EventHandler((senderA, eA) => GenererMouvGVoiePrincipDirectionB(sender, e, imageAvion, piste, hangar, avion));
                    GereGauche.Interval = TimeSpan.FromMilliseconds(50);
                }
                CoordY -= VitesseAeroport;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }
           
         
        ///-----------------------------------------------------------------------------------------------------------------------------------------
        /// DÉCOLLAGE:   
        /// <summary>
        /// Une méthode qui permet de démarrer un décollage uniquement
        /// </summary>
        public void DemarreDecollage(int piste)
        {
            //Déclaration des variables
            Rectangle r = new Rectangle();
            Rectangle rectRotation = new Rectangle();
            ImageBrush a = new ImageBrush();

            a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/avion.png"));

            //Pour chaque avion dans la liste d'avion
            foreach (Avion av in Ec.LstAvion)
            {
                //Si l'avion est disponible au décollage et qu'elle est initialisée
                if (av.EstDisponibleDecollage == true && av.EstInitialisee == true)
                {
                    Rectangle avionD = new Rectangle();

                    //On initialise l'avion avec des paramètre de décollage
                    avionD.Fill = a;
                    avionD.Width = 35;
                    avionD.Height = 35;
                    Canvas.SetZIndex(avionD, 100);

                    NumeroAvionDisponible = av.NumAvion;
                    CoordX = av.CoordX;
                    CoordY = av.CoordY;
                    EstAtterrissage = false;    //Il s'agit d'un décollage
                    ImageAvion = avionD;
                    av.EstDisponibleDecollage = false;

                    //On dessine l'avion en fonction des coordonnées de la piste
                    Canvas.SetLeft(avionD, CoordX);
                    Canvas.SetTop(avionD, CoordY);
               
                    //Pour chaque hangar dans la liste de hangar
                    foreach (Hangar h in Ec.LstHangar)
                    {
                        if (h.NumHangar == av.HangarAssigne)
                        {                         
                            h.EstDisponible = true;
                            break;
                        }
                    }
                 
                    //On enlève l'image temporaire de l'avion de décollage
                    Ec.cnvCarte.Children.Remove(av.ImageAvion);

                    //On ajoute l'avion de décollage au canvas
                    Ec.cnvCarte.Children.Add(ImageAvion);
                
                    //On appelle la fonction qui s'occupe des décollages
                    GererDecollage(ImageAvion, piste,av.HangarAssigne);

                    break;  //On sort du foreach
                }                         
            }        
        }

        /// <summary>
        /// Une méthode pour gérer les décollages uniquement 
        /// </summary>
        public void GererDecollage(Rectangle avionDecollage, int piste, int hangarAssigne)
        {
            //Selon le numéro du hangar
            if(hangarAssigne ==1 || hangarAssigne == 2 || hangarAssigne == 3 || hangarAssigne == 4 || hangarAssigne == 5 || hangarAssigne == 6)
            {
                GereBas = new DispatcherTimer();

                //Ici l'avion quitte le hangar pour aller se placer sur une piste
                GereBas.Start();
                GereBas.Tick += new EventHandler((sender, e) => GenererMouvBVoiePrincipDirectionG(sender, e, avionDecollage, piste));
                GereBas.Interval = TimeSpan.FromMilliseconds(50);
            }
            else
            {
                GereHaut = new DispatcherTimer();

                //Ici l'avion quitte le hangar pour aller se placer sur une piste
                GereHaut.Start();
                GereHaut.Tick += new EventHandler((sender, e) => GenererMouvHVoiePrincipDirectionD(sender, e, avionDecollage, piste));
                GereHaut.Interval = TimeSpan.FromMilliseconds(50);
            }               
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie principlae et ensuite direction Droit
        /// </summary>
        public void GenererMouvHVoiePrincipDirectionD(object sender, EventArgs e, Rectangle imageAvion, int piste)
        {
            LongueurVerticale = 0;
            DistanceAParcourirVoiePrincip = 120;
        
            if (CoordY >= LongueurVerticale)
            {

                if (CoordY <= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le mouvement en cours
                    GereHaut.Stop();

                    GereDroit = new DispatcherTimer();

                    //Le prochain mouvement se fera dans la voie principale
                    GereDroit.Start();
                    GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvDVoiePrincipDirectionH(sender, e, imageAvion, piste, 34, 34));
                    GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                }

                CoordY -= VitesseAeroport;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode qui permet de générer le mouvement de départ d'une avion au décollage
        /// </summary>
        public void GenererMouvBVoiePrincipDirectionG(object sender, EventArgs e, Rectangle avionDecollage, int piste)
        {
            LongueurVerticale = 1000;
            DistanceAParcourirVoiePrincip = 280;

            if (CoordY <= LongueurVerticale)
            {

                if (CoordY >= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le déplacement en cours
                    GereBas.Stop();

                    //On commence le nouveau déplacement
                    GereGauche = new DispatcherTimer();

                    GereGauche.Start();
                    GereGauche.Tick += new EventHandler((senderA, eA) => GenererMouvGVoiePrincipDirectionH(sender, e, avionDecollage, piste, 34, 34));
                    GereGauche.Interval = TimeSpan.FromMilliseconds(50);
                }
                else
                {
                    CoordY += VitesseAeroport;
                }
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(avionDecollage, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de service pour les décollages 
        /// </summary>
        public void GenererMouvHVoieServDecollage(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 0;
            DistanceAParcourirVoiePrincip = 20;

            //Pour gérer l'aiguillage de l'avion
            Angle = 270;
            AngleDesire = 360;
            Operation = true;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            if (CoordY >= LongueurVerticale)
            {

                if (CoordY <= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le mouvement en cours
                    GereHaut.Stop();

                    GereDroit = new DispatcherTimer();

                    //Le prochain mouvement se fera dans la voie principale
                    GereDroit.Start();
                    GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvDVoieServDirectionDecollage(sender, e, imageAvion, piste, hangar, avion));
                    GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                }
                CoordY -= VitesseAeroport;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement droit sur les voies de services direction Décollage
        /// </summary>
        public void GenererMouvDVoieServDirectionDecollage(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurHorizontale = 2000;
            DistanceAParcourirVoiePrincip = 1830;

            //Pour gérer l'aiguillage de l'avion
            Angle = 0;
            AngleDesire = 90;
            Operation = true;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            if (CoordX <= LongueurHorizontale)
            {

                if (CoordX >= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le déplacement en cours
                    GereDroit.Stop();             
                }
                else
                {                   
                    if ((CoordX < 400 && piste != 5) || (CoordX < 340 && piste == 5))
                    {
                        CoordX += VitesseAeroport;
                        GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {                    
                        CoordX += Vitesse;                                     
                    }                  
                }
            }
            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement BAS direction GAUCHE ou DROIT
        /// </summary>
        public void GenererMouvBVoieServDirectionGD(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 1000;

            //Pour gérer l'aiguillage de l'avion
            Angle = 90;
            AngleDesire = 180;
            Operation = true;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            switch (piste)
            {
                case 1:
                    DistanceAParcourirVoiePrincip = 450;
                    break;
                case 2:
                    DistanceAParcourirVoiePrincip = 367;
                    break;
                case 3:
                    DistanceAParcourirVoiePrincip = 405;
                    break;
                case 4:
                    DistanceAParcourirVoiePrincip = 425;
                    break;
            }

            if (CoordY <= LongueurVerticale)
            {
                if (CoordY >= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le déplacement en cours
                    GereBas.Stop();

                    if(piste == 1 || piste == 3)
                    {
                        GereGauche = new DispatcherTimer();

                        //Le prochain mouvement se fera dans la voie principale
                        GereGauche.Start();
                        GereGauche.Tick += new EventHandler((senderA, eA) => GenererMouvGVoieServDirectionDecollage(sender, e, imageAvion, piste, hangar, avion));
                        GereGauche.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {
                        GereDroit = new DispatcherTimer();

                        //Le prochain mouvement se fera dans la voie principale
                        GereDroit.Start();
                        GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvDVoieServDirectionDecollage(sender, e, imageAvion, piste, hangar, avion));
                        GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                    }
                }
                else
                {
                    CoordY += VitesseAeroport;
                }
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement GAUCHE direction décollage
        /// </summary>
        public void GenererMouvGVoieServDirectionDecollage(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurHorizontale = 0;

            //Pour gérer l'aiguillage de l'avion
            Angle = 180;
            AngleDesire = 270;
            Operation = true;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            if (piste==1)
            {
                DistanceAParcourirVoiePrincip = 37;
            }
            else if(piste==3)
            {
                DistanceAParcourirVoiePrincip = 112;
            }

            if (CoordX >= LongueurHorizontale)
            {

                if (CoordX <= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le déplacement en cours
                    GereGauche.Stop();

                    //Le prochain mouvement à générer est HAUT
                    GereHaut = new DispatcherTimer();
                    GereHaut.Start();
                    GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHVoieServDirectionDecollage(sender, e, imageAvion, piste, hangar, avion));
                    GereHaut.Interval = TimeSpan.FromMilliseconds(50);
                }
                else
                {
                    CoordX -= VitesseAeroport;
                }
            }
           
            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT direction décollage
        /// </summary>  
        public void GenererMouvHVoieServDirectionDecollage(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = -1000;
            DistanceAParcourirVoiePrincip = -2000;

            //Pour gérer l'aiguillage de l'avion
            Angle = 270;
            AngleDesire = 360;
            Operation = true;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            if (CoordY >= LongueurVerticale)
            {

                if (CoordY <= DistanceAParcourirVoiePrincip)
                {
                    //On arrete le mouvement en cours
                    GereHaut.Stop();                                
                }
                else
                {                  
                    if (CoordY > 360)
                    {
                        CoordY -= VitesseAeroport;
                        GereBas.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {                      
                        CoordY -= Vitesse;
                    }                  
                }               
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }       
    }
}
