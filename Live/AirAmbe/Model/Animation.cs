using System;
using System.Collections.Generic;
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
        public const int VITESSE = 5;

        public const int VITESSEAEROPORT = 50;

        public const int VITESSEAD= 70;
        //Déclaration des attributs de la classe Animation
        public DispatcherTimer GerePlusieurs;

        public DispatcherTimer GereDroit;

        public DispatcherTimer GereGauche;

        public DispatcherTimer GereHaut;

        public DispatcherTimer GereBas;

        public DispatcherTimer RotationAvion;
       
        public EcranControleur Ec { get; set; }

        public int CoordY { get; set; }

        public int CoordX { get; set; }

        public bool EstAtterrissage { get; set; }

        public int FinPiste { get; set; }

        public int FinVoieService { get; set; }

        public int LongueurVerticale {get; set;}

        public int LongueurHorizontale { get; set; }

        public int DistanceAParcourirVoiePrincip { get; set; }

        public int PositionYDebutVoiePrincip { get; set; }

        public int LongueurHorizontaleVs { get; set; }

        public int NumeroHangarDisponible { get; set; }

        public int NumeroAvionDisponible { get; set; }

        public Rectangle ImageAvion { get; set; }
    
        public int Angle { get; set; }  

        public int AngleDesire { get; set; }

        public bool Operation { get; set; }
        
        /// <summary>
        /// Constructeur de la classe Animation
        /// </summary>
        /// <param name="ec">Un objet écran contrôleur</param>
        public Animation(EcranControleur ec)
        {
            Ec = ec;          
        }

        /// <summary>
        /// Une méthode pour dessiner les pistes en fonction du nombre choisi 
        /// </summary>
        /// <param name="NbPistes">Nombre de piste choisi par l'utilisateur</param>
        public void GererDessinPiste(int NbPistes)
        {
            ImageBrush ImagePiste1 = new ImageBrush();
            ImagePiste1.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/piste1.png"));
            ImageBrush ImagePiste2 = new ImageBrush();
            ImagePiste2.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/piste2.png"));
            ImageBrush ImagePiste3 = new ImageBrush();
            ImagePiste3.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/piste3.png"));
            ImageBrush ImagePiste4 = new ImageBrush();
            ImagePiste4.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/piste4.png"));
            ImageBrush ImagePiste5 = new ImageBrush();
            ImagePiste5.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/piste5.png"));
       
            DessinerPiste1(ImagePiste1);
            DessinerPiste2(ImagePiste2);
            DessinerPiste3(ImagePiste3);
            DessinerPiste4(ImagePiste4);
            DessinerPiste5(ImagePiste5);
        }

        /// <summary>
        /// Une méthode pour dessiner la piste 1 (Horizontale)
        /// </summary>
        /// <param name="ib">L'image d'une piste</param>
        public void DessinerPiste1(ImageBrush ib)
        {
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
        /// Une méthode pour dessiner la piste 4 (verticale)
        /// </summary>
        /// <param name="ib">L'image d'une piste</param>
        public void DessinerPiste2(ImageBrush ib)
        {
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
        /// Une méthode pour dessiner la piste 3 (Horizontale)
        /// </summary>
        /// <param name="ib">L'image d'une piste</param>
        public void DessinerPiste3(ImageBrush ib)
        {
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
        /// Une méthode pour dessiner la piste 2 (verticale)
        /// </summary>
        /// <param name="ib">L'image d'une piste</param>
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
        /// Une méthode pour dessiner la piste 5 (Horizontale)
        /// </summary>
        /// <param name="ib">L'image d'une piste</param>
        public void DessinerPiste5(ImageBrush ib)
        {
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
        /// Une méthode pour gérer l'aiguillage des avions
        /// </summary>
        /// <param name="Avion"></param>
        public void GererAiguillageAvion(Rectangle Avion, int angle, int angleDesire, bool operation)
        {          
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


        /// <summary>
        /// Une méthode pour dessiner le hangar sur la carte des pistes
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
        /// Une méthode qui permet de démarrer un atterrissage
        /// </summary>
        /// <param name="piste"></param>
        /// 
        public void DemarreAtterrissage(int piste)
        {
            //Déclaration des variables
            //Rectangle AvionA=new Rectangle();
            Rectangle rectRotation = new Rectangle();
            ImageBrush a = new ImageBrush();
            a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/avion.png"));
            ImageAvion = new Rectangle();

            //On test la disponibilité des hangars 1 à 6 
            NumeroHangarDisponible = TesterDisponibiliteHangar();

            NumeroAvionDisponible = TesterDisponibiliteAvion();

            foreach(Avion avionA in Ec.LstAvion)
            {
                if(avionA.NumAvion==NumeroAvionDisponible)
                {
                    ImageAvion = avionA.ImageAvion;
                }
            }

                    ImageAvion.Fill = a;
                    ImageAvion.Width = 35;
                    ImageAvion.Height = 35;
                   
                    Canvas.SetZIndex(ImageAvion, 100);

                    int coordY = 0;
                    int coordX = 0;
                    int finPiste = 0;
                    int finVoieService = 0;

                    RotateTransform aiguillageDroit = new RotateTransform(90, 14, 14);
                    RotateTransform aiguillageGauche = new RotateTransform(270, 14, 14);
                    RotateTransform aiguillageBas = new RotateTransform(180, 14, 14);
                    RotateTransform aiguillageHaut = new RotateTransform(360, 14, 14);

                    //Selon le numéro de la piste on initialise les coordonnées de départ ainsi que l'orientation de l'avion
                    switch (piste)
                    {
                        case 1:
                            coordX = 40;
                            coordY = -500;
                            finPiste = 450;
                            finVoieService = 210;                       
                            PositionYDebutVoiePrincip = 350;
                            ImageAvion.RenderTransform = aiguillageBas;
                            break;

                        case 2:
                            coordX = 1700;
                            coordY = 378;
                            finPiste = 330;                         
                            PositionYDebutVoiePrincip = 350;
                            ImageAvion.RenderTransform = aiguillageGauche;
                            break;

                        case 3:

                            coordX = 125;
                            coordY = -500;
                            finPiste = 405;
                            finVoieService = 160;                        
                            PositionYDebutVoiePrincip = 350;
                            ImageAvion.RenderTransform = aiguillageBas;
                            break;

                        case 4:
                            coordX = 1700;
                            coordY = 432;
                            finPiste = 280;
                            PositionYDebutVoiePrincip = 350;
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

            //On appelle la fonction d'atterrissage en fonction de sa piste ainsi que de ses coordonnées
                    GererAtterrissage(ImageAvion, piste, NumeroHangarDisponible,NumeroAvionDisponible);
          
        }

        //-----------------------------------------------------------------------------------------------------------------------
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste">Numéro de la piste</param>
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
                        CoordY += VITESSE;
                    }
                }
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);

        }

        /// <summary>
        /// Une méthode pour générer un mouvement de gauche ou un mouvement de droite selon la piste sur la voie de service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste">Numéro de la piste</param>
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
                        CoordX += VITESSE;
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
                        CoordX -= VITESSE;
                    }
                }

                //On génère le mouvement GAUCHE ou DROIT
                Canvas.SetLeft(imageAvion, CoordX);
            }







        }

        /// <summary>
        /// Une méthode pour générer un mouvement de haut ou un mouvement de bas selon la piste sur la voie de service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste">Numéro de la piste</param>
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
                        CoordY -= VITESSE;
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
                        CoordY -= VITESSE;
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
                        CoordY += VITESSE;
                    }
                }
                //On génère le mouvement BAS ou HAUT
                Canvas.SetTop(imageAvion, CoordY);
            }

           
        }

        /// <summary>
        /// Une méthode pour générer un mouvement DROIT sur la voie de principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvDVoiePrincip(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurHorizontale = 1000;
            DistanceAParcourirVoiePrincip = 830;

            //Pour gérer l'aiguillage de l'avion
            Angle = 0;
            AngleDesire = 90;
            Operation = true;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

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
                        CoordX += VITESSE;
                    }
                 }
                                   
            }
            else
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
                        CoordX += VITESSE;
                    }
                }
            }
            
            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement DROIT sur la voie de principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvDVoiePrincipDirectionH(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurHorizontale = 1000;
                      
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
                    DistanceAParcourirVoiePrincip = 570;
                    Angle = 180;
                    Operation = false;
                    break;
                case 10:
                    DistanceAParcourirVoiePrincip = 650;
                    Angle = 180;
                    Operation = false;
                    break;
                case 11:
                    DistanceAParcourirVoiePrincip = 730;
                    Angle = 180;
                    Operation = false;
                    break;
                case 12:
                    DistanceAParcourirVoiePrincip = 810;
                    Angle = 180;
                    Operation = false;
                    break;
                default:
                    DistanceAParcourirVoiePrincip = 830;
                    Angle = 0;
                    Operation = true;
                    break;
            }

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
                    CoordX += VITESSE;
                }
            }
            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement GAUCHE sur la voie de principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvGVoiePrincipDirectionB(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurHorizontale = 0;
            DistanceAParcourirVoiePrincip = 162;

            //Pour gérer l'aiguillage de l'avion
            Angle = 360;
            AngleDesire = 270;
            Operation = false;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

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
                    CoordX -= VITESSE;
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
                        CoordX -= VITESSE;
                    }
                }
            }


            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement GAUCHE sur la voie de principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvGVoiePrincipDirectionH(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            //Pour gérer l'aiguillage de l'avion
            Angle = 360;
            AngleDesire = 270;
            Operation = false;
            GererAiguillageAvion(imageAvion, Angle, AngleDesire, Operation);

            LongueurHorizontale = 0;    
            switch(hangar)
            {
                case 1:
                    DistanceAParcourirVoiePrincip = 810;
                    break;
                case 2:
                    DistanceAParcourirVoiePrincip = 730;
                    break;
                case 3:
                    DistanceAParcourirVoiePrincip = 650;
                    break;
                case 4:
                    DistanceAParcourirVoiePrincip = 570;
                    break;
                case 5:
                    DistanceAParcourirVoiePrincip = 490;
                    break;
                case 6:
                    DistanceAParcourirVoiePrincip = 410;
                    break;           
                default:
                    DistanceAParcourirVoiePrincip = 330;
                    break;
            }

          
            //MessageBox.Show(NumeroHangarDisponible.ToString());
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
                    CoordX -= VITESSE;
                }
            }

            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement BAS sur la voie de principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvBVoiePrincip(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 1000;
            DistanceAParcourirVoiePrincip = 350;

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
                    CoordY += VITESSE;
                }
            }
              
            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement BAS direction HANGAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        /// <param name="hangar"></param>
        /// <param name="avion"></param>
        public void GenererMouvBVoiePrincipDirectionHangar(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 1000;
            DistanceAParcourirVoiePrincip = 150;

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
                    CoordY += VITESSE;
                }
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de principale direction GAUCHE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvHVoiePrincipDirectionG(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 0;
            DistanceAParcourirVoiePrincip = 300;

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
                CoordY -= VITESSE;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de principale direction DROIT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvHVoiePrincipDirectionD(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 0;
            DistanceAParcourirVoiePrincip = 120;

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
                CoordY -= VITESSE;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de principale direction HANGAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        /// <param name="hangar"></param>
        public void GenererMouvHVoiePrincipDirectionHangar(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 0;
            DistanceAParcourirVoiePrincip = 250;

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
                CoordY -= VITESSE;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de principale direction DROIT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvHVoiePrincipDirectionGFinCycle(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 0;
            DistanceAParcourirVoiePrincip = 62;

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
                CoordY -= VITESSE;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode pour tester la disponibilité des hangars
        /// </summary>
        /// <returns></returns>
        public int TesterDisponibiliteHangar()
        {

            foreach(Hangar h in Ec.LstHangar)
            {
                if(h.EstDisponible)
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
        /// <returns></returns>
        public int TesterDisponibiliteAvion()
        {

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
        /// Une méthode pour disperser les avions dans les hangars selon le nombre de décollage
        /// </summary>
        public void DisperserDecollageHangar()
        {
            int hangar = 0;
            //Pour chaque vol dans la liste de decollage                         
            foreach (Vol v in Ec.LstDecollages)
            {                
                //Selon le nombre de décollage on initialise les paramètres des hangars
                //for (int i = 0; i < 2; i++)
                //{
                    foreach (Avion a in Ec.LstAvion)
                    {
                       
                        if (a.EstDisponibleDecollage && a.EstInitialisee==false)
                        {
                            Ec.LstHangar[hangar].NumHangar = hangar + 1;
                            Ec.LstHangar[hangar].EstDisponible = false;

                            DessinerAvionDecollage(v.IdVol, Ec.LstHangar[hangar].NumHangar,a);

                            hangar++;
                        }

                      
                    }
                //}                          
            }
        }

        /// <summary>
        /// Une méthode pour préparer les avions à un décollage en les dessinant
        /// </summary>
        /// <param name="idVol"></param>
        /// <param name="noHangar"></param>
        /// <param name="av"></param>
        public void DessinerAvionDecollage(int idVol, int noHangar,Avion av)
        {
            Rectangle avionD = new Rectangle();
            Rectangle rectRotation = new Rectangle();
            ImageBrush a = new ImageBrush();
            a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/avion.png"));

              
                    avionD.Fill = a;
                    avionD.Width = 35;
                    avionD.Height = 35;
                    //AvionD.Name = "vol" + i.ToString();
                    Canvas.SetZIndex(avionD, 100);

                    int coordY = 0;
                    int coordX = 0;
         
                    RotateTransform aiguillageDroit = new RotateTransform(90, 14, 14);
                    RotateTransform aiguillageGauche = new RotateTransform(270, 14, 14);
                    RotateTransform aiguillageBas = new RotateTransform(180, 14, 14);
                    RotateTransform aiguillageHaut = new RotateTransform(360, 14, 14);

                    //Selon le numéro de la piste on initialise les coordonnées de départ ainsi que l'orientation de l'avion
                    switch (noHangar)
                    {
                        case 1:
                            coordX = 810;
                            coordY = 230;            
              
                            break;

                        case 2:
                            coordX = 730;
                            coordY = 230;
                 
                            break;

                        case 3:
                            coordX = 650;
                            coordY = 230;                                 
                            break;

                        case 4:
                            coordX = 570;
                            coordY = 230;
      
                            break;

                        case 5:
                            coordX = 490;
                            coordY = 230;
                
                            break;
                        case 6:
                            coordX = 410;
                            coordY = 230;

                            break;
                        case 7:
                            coordX = 410;
                            coordY = 140;
                            
                            break;
                        case 8:
                            coordX = 490;
                            coordY = 140;

                            break;
                        case 9:
                            coordX = 570;
                            coordY = 140;

                            break;
                        case 10:
                            coordX = 650;
                            coordY = 140;

                            break;
                        case 11:
                            coordX = 730;
                            coordY = 140;

                            break;
                        case 12:
                            coordX = 810;
                            coordY = 140;

                            break;
                    }

                    if(noHangar==1 || noHangar==2 || noHangar == 3 || noHangar == 4 || noHangar == 5 || noHangar == 6)
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
     
                    av.CoordX = coordX;
                    av.CoordY = coordY;
                    av.HangarAssigne = noHangar;
                    av.EstInitialisee = true;
                    avionD.Name = "AvionNum" + av.NumAvion.ToString();
                    //Ec.ImageAvionD = avionD;
                    av.ImageAvion = avionD;
          
                    Ec.cnvCarte.Children.Add(av.ImageAvion);                                                  
        }

        /// <summary>
        /// Une méthode qui permet de démarrer un décollage 
        /// </summary>
        /// <param name="piste"></param>
        public void DemarreDecollage(int piste)
        {
            Rectangle r = new Rectangle();
            Rectangle rectRotation = new Rectangle();
            ImageBrush a = new ImageBrush();
            a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/avion.png"));

            foreach (Avion av in Ec.LstAvion)
            {
                //for (int i = 0; i < Ec.LstAvion.Count(); i++)
                //{
                if (av.EstDisponibleDecollage == true && av.EstInitialisee == true)
                {
                    Rectangle avionD = new Rectangle();
                    avionD.Fill = a;
                    avionD.Width = 35;
                    avionD.Height = 35;
                    //AvionD.Name = "vol" + i.ToString();
                    Canvas.SetZIndex(avionD, 100);

                    NumeroAvionDisponible = av.NumAvion;


                    CoordX = av.CoordX;
                    CoordY = av.CoordY;
                    EstAtterrissage = false;
                    //On dessine l'avion en fonction des coordonnées de la piste
                    Canvas.SetLeft(avionD, CoordX);
                    Canvas.SetTop(avionD, CoordY);

                    ImageAvion = avionD;
                    av.EstDisponibleDecollage = false;


                    foreach (Hangar h in Ec.LstHangar)
                    {
                        if (h.NumHangar == av.HangarAssigne)
                        {                         
                            h.EstDisponible = true;
                            break;
                        }
                    }
                 
                    

                    Ec.cnvCarte.Children.Remove(av.ImageAvion);

                    Ec.cnvCarte.Children.Add(ImageAvion);
                
                    GererDecollage(ImageAvion, piste,av.HangarAssigne);

                    break;
                }                         
            }        
        }

        /// <summary>
        /// Une méthode pour gérer les décollages uniquement 
        /// </summary>
        public void GererDecollage(Rectangle avionDecollage, int piste, int hangarAssigne)
        {

            if(hangarAssigne==1 || hangarAssigne ==2 || hangarAssigne == 3 || hangarAssigne == 4 || hangarAssigne == 5 || hangarAssigne == 6)
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
                CoordY -= VITESSE;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

        /// <summary>
        /// Une méthode qui permet de générer le mouvement de départ d'une avion au décollage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    CoordY += VITESSE;
                }
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(avionDecollage, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de service pour les décollages 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="imageAvion"></param>
        /// <param name="piste"></param>
        /// <param name="hangar"></param>
        /// <param name="avion"></param>
        public void GenererMouvHVoieServDecollage(object sender, EventArgs e, Rectangle imageAvion, int piste, int hangar, int avion)
        {
            LongueurVerticale = 0;
            DistanceAParcourirVoiePrincip = 22;

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
                CoordY -= VITESSE;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }

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
                    CoordX += VITESSE;
                }
            }
            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

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
                    DistanceAParcourirVoiePrincip = 432;
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
                    CoordY += VITESSE;
                }
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }


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
                DistanceAParcourirVoiePrincip = 40;
            }
            else if(piste==3)
            {
                DistanceAParcourirVoiePrincip = 125;
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
                    CoordX -= VITESSE;
                }
            }
            


            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(imageAvion, CoordX);
        }

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
                CoordY -= VITESSE;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(imageAvion, CoordY);
        }


        public void ChangerOpacitePiste(Piste piste)
        {         
            switch(piste.NumPiste)
            {
                case 1:
                    if (piste.estDisponible)
                    {
                        Ec.Piste1.Opacity = 1;
                    }
                    else
                    {
                        Ec.Piste1.Opacity = 0.3;
                    }

                    break;
                case 2:
                    if (piste.estDisponible)
                    {
                        Ec.Piste4.Opacity = 1;
                    }
                    else
                    {
                        Ec.Piste4.Opacity = 0.3;
                    }
                    break;
                case 3:
                    if (piste.estDisponible)
                    {
                        Ec.Piste3.Opacity = 1;
                    }
                    else
                    {
                        Ec.Piste3.Opacity = 0.3;
                    }
                    break;
                case 4:
                    if (piste.estDisponible)
                    {
                        Ec.Piste2.Opacity = 1;
                    }
                    else
                    {
                        Ec.Piste2.Opacity = 0.3;
                    }
                    break;
                case 5:
                    if (piste.estDisponible)
                    {
                        Ec.Piste5.Opacity = 1;
                    }
                    else
                    {
                        Ec.Piste5.Opacity = 0.3;
                    }
                    break;
            }                    
        }       
    }
}
