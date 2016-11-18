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
        public const int VITESSE = 8;

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

        public int FinPiste { get; set; }

        public int FinVoieService { get; set; }

        public int LongueurVerticale {get; set;}

        public int LongueurHorizontale { get; set; }

        public int LongueurHorizontaleMinVoiePrincip = 160;

        public int LongueurHorizontaleMaxVoiePrincip = 830;

        public int LongueurVerticaleMinVoiePrincip = 62;

        public int LongueurVerticaleMaxVoiePrincip = 350;
       
        public int PositionYFinPremiereRangeeHangar = 330;

        public int PositionYDebutPremiereRangeeHangar = 300;

        public int PositionYFinDeuxiemeRangeeHangar = 200;

        public int PositionYDebutDeuxiemeRangeeHangar = 120;

        public int PositionYDebutVoiePrincip { get; set; }

        public int LongueurHorizontaleVs { get; set; }

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
        /// Une méthode pour dessiner les voies de service
        /// </summary>
        public void DessinerVoieService()
        {
            //Rectangle rectRotation = new Rectangle();
            //ImageBrush a = new ImageBrush();
            //a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/asphalte.jpg"));

            //Ec.Asphalthe1.Stroke = new SolidColorBrush(Colors.Black);
            //Ec.Asphalthe1.StrokeThickness = 2;
            //Ec.Asphalthe1.Fill = a;
            //Ec.Asphalthe1.Width = 35;
            //Ec.Asphalthe1.Height = 40;
            //Canvas.SetLeft(Ec.Asphalthe1, 80);
            //Canvas.SetTop(Ec.Asphalthe1, 100);

            //Ec.Asphalthe2.Stroke = new SolidColorBrush(Colors.Black);
            //Ec.Asphalthe2.StrokeThickness = 2;
            //Ec.Asphalthe2.Fill = a;
            //Ec.Asphalthe2.Width = 35;
            //Ec.Asphalthe2.Height = 40;
            //Canvas.SetLeft(Ec.Asphalthe2, 300);
            //Canvas.SetTop(Ec.Asphalthe2, 175);


            Ec.AsphalthePrincipale.Stroke = new SolidColorBrush(Colors.Black);
            Ec.AsphalthePrincipale.StrokeThickness = 2;
            Ec.AsphalthePrincipale.Fill = new SolidColorBrush(Colors.DarkGray);
            Ec.AsphalthePrincipale.Width = 170;
            Ec.AsphalthePrincipale.Height = 340;
            Canvas.SetLeft(Ec.AsphalthePrincipale, 190);
            Canvas.SetTop(Ec.AsphalthePrincipale, 120);

            //RotateTransform rotation = new RotateTransform(90, 14, 14);
            //Ec.AsphalthePrincipale.RenderTransform = rotation;
            //Ec.AsphalthePrincipale.Stroke = new SolidColorBrush(Colors.Black);
            //Ec.AsphalthePrincipale.StrokeThickness = 2;
            //Ec.AsphalthePrincipale.Fill = a;
            //Ec.AsphalthePrincipale.Width = 210;
            //Ec.AsphalthePrincipale.Height = 30;
            //Canvas.SetLeft(Ec.AsphalthePrincipale, 110);
            //Canvas.SetTop(Ec.AsphalthePrincipale, 60);
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
        public void DemarreAtterrissage(int piste)
        {
            //Déclaration des variables
            Rectangle AvionA = new Rectangle();
            Rectangle rectRotation = new Rectangle();
            ImageBrush a = new ImageBrush();
            a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/avion.png"));

            //AvionA.Fill = new SolidColorBrush(Colors.Black); 
            AvionA.Fill = a;
            AvionA.Width = 35;
            AvionA.Height = 35;
            //AvionA.Name = "vol" + i.ToString();
            Canvas.SetZIndex(AvionA, 100);



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
                    coordY = 0;
                    finPiste = 450;
                    finVoieService = 210;
                    //LongueurVerticaleVs = 230;
                    PositionYDebutVoiePrincip = 350;
                    AvionA.RenderTransform = aiguillageBas;
                    break;

                case 2:
                    coordX = 2100;
                    coordY = 367;
                    finPiste = 330;
                    //LongueurVerticaleVs = 310;
                    PositionYDebutVoiePrincip = 350;
                    AvionA.RenderTransform = aiguillageGauche;
                    break;

                case 3:

                    coordX = 125;
                    coordY = 0;
                    finPiste = 405;
                    finVoieService = 160;
                    //LongueurVerticaleVs = 180;
                    PositionYDebutVoiePrincip = 350;
                    AvionA.RenderTransform = aiguillageBas;
                    break;

                case 4:
                    coordX = 900;
                    coordY = 432;
                    finPiste = 280;
                    //LongueurVerticaleVs = 275;
                    PositionYDebutVoiePrincip = 350;
                    AvionA.RenderTransform = aiguillageGauche;
                    break;

                case 5:
                    coordX = 900;
                    coordY = 22;
                    finPiste = 325;
                    PositionYDebutVoiePrincip = 62;
                    //finVoieService;

                    AvionA.RenderTransform = aiguillageGauche;
                    break;
            }

            //On dessine l'avion en fonction des coordonnées de la piste
            Canvas.SetLeft(AvionA, coordX);
            Canvas.SetTop(AvionA, coordY);

            //On ajoute l'avion au canvas

            Ec.cnvCarte.Children.Add(AvionA);

            //On initialise les attributs de la classe
            CoordY = coordY;
            CoordX = coordX;
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

          

            //PositionYFinPremiereRangeeHangar = 330;

            //On appelle la fonction d'atterrissage en fonction de sa piste ainsi que de ses coordonnées
            GererAtterrissage(AvionA, piste);
        }

        //-----------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Une méthode pour gérer les atterrissages uniquement 
        /// </summary>
        public void GererAtterrissage(Rectangle avionAtterrissage, int piste)
        {
            //Déclaration des variables
            GereBas = new DispatcherTimer();
            GereGauche = new DispatcherTimer();
         


            //Si il s'agit de la piste 1 ou 3, on génère un mouvement vers le bas
            if(piste==1 || piste ==3)
            {
                //Pour générer un déplacement vers la gauche ainsi que vers le bas pour les atterrissages
                GereBas.Start();
                GereBas.Tick += new EventHandler((sender, e) => GenererMouvHBPisteAtteri(sender, e, avionAtterrissage, piste));
                GereBas.Interval = TimeSpan.FromMilliseconds(50);
            }else if(piste ==2 || piste ==4 || piste ==5)   //Si il s'agit de la piste 2, 4 ou 5 on génère un mouvement vers la gauche
            {
                //Pour générer un déplacement vers la gauche ainsi que vers le bas pour les atterrissages
                GereGauche.Start();
                GereGauche.Tick += new EventHandler((sender, e) => GenererMouvGDVoieServ(sender, e, avionAtterrissage, piste));
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
        public void GenererMouvHBPisteAtteri(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
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
                        GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvGDVoieServ(sender, e, avionAtterrissage, piste));
                        GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {
                        CoordY += VITESSE;
                    }
                }
            }
                     
            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(avionAtterrissage, CoordY);

        }

        /// <summary>
        /// Une méthode pour générer un mouvement de gauche ou un mouvement de droite selon la piste sur la voie de service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste">Numéro de la piste</param>
        public void GenererMouvGDVoieServ(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {
            //Si il s'agit de la piste 1 ou 3
            if (piste == 1 || piste == 3)
            {
                if (CoordX <= LongueurHorizontale)
                {
                    if (CoordX >= FinVoieService)
                    {
                        //On arrete le déplacement en cours
                        GereDroit.Stop();
                   
                        //On commence le nouveau déplacement
                        GereHaut = new DispatcherTimer();

                        GereHaut.Start();
                        GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHBVoieServ(sender, e, avionAtterrissage, piste));
                        GereHaut.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {
                        CoordX += VITESSE;
                    }
                }

                //On génère le mouvement GAUCHE ou DROIT 
                Canvas.SetLeft(avionAtterrissage, CoordX);
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
                        GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHBVoieServ(sender, e, avionAtterrissage, piste));
                        GereHaut.Interval = TimeSpan.FromMilliseconds(50);

                    }
                    else
                    {
                        CoordX -= VITESSE;
                    }
                }

                //On génère le mouvement GAUCHE ou DROIT
                Canvas.SetLeft(avionAtterrissage, CoordX);
            }







        }

        /// <summary>
        /// Une méthode pour générer un mouvement de haut ou un mouvement de bas selon la piste sur la voie de service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste">Numéro de la piste</param>
        public void GenererMouvHBVoieServ(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {
            //Déclaration des variables
            int longueurVerticaleMax = 0;
            FinVoieService = 600;
            LongueurHorizontale = 600;

            //Si il s'agit de la piste 1 ou 3
            if (piste == 1 || piste == 3)
            {
                if (CoordY >= longueurVerticaleMax)
                {

                    if (CoordY <= PositionYDebutVoiePrincip)
                    {
                        //On arrete le mouvement en cours
                        GereHaut.Stop();
                        
                        GereDroit = new DispatcherTimer();

                        //Le prochain mouvement se fera dans la voie principale
                        GereDroit.Start();
                        GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvDVoiePrincip(sender, e, avionAtterrissage, piste));
                        GereDroit.Interval = TimeSpan.FromMilliseconds(50);

                    }
                    else
                    {
                        CoordY -= VITESSE;
                    }
                }
                //On génère le mouvement BAS ou HAUT
                Canvas.SetTop(avionAtterrissage, CoordY);
            }
            else if (piste == 2 || piste == 4)  //Si il s'agit de la piste 2 ou 4
            {
                if (CoordY >= longueurVerticaleMax)
                {

                    if (CoordY <= PositionYDebutVoiePrincip)
                    {
                        //On arrete le mouvement en cours
                        GereHaut.Stop();
                     
                        GereDroit = new DispatcherTimer();

                        //Le prochain mouvement se fera dans la voie principale
                        GereDroit.Start();
                        GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvDVoiePrincip(sender, e, avionAtterrissage, piste));
                        GereDroit.Interval = TimeSpan.FromMilliseconds(50);                                     
                    }
                    else
                    {
                        CoordY -= VITESSE;
                    }                     
                }
                //On génère le mouvement BAS ou HAUT
                Canvas.SetTop(avionAtterrissage, CoordY);
            }
            else if (piste == 5)    //Si il s'agit de la piste 5
            {
                longueurVerticaleMax = 500;

                if (CoordY <= longueurVerticaleMax)
                {
                    if (CoordY >= PositionYDebutVoiePrincip)
                    {
                        //On arrete le mouvement en cours
                        GereHaut.Stop();

                        GereGauche = new DispatcherTimer();
               
                        GereGauche.Start();
                        GereGauche.Tick += new EventHandler((senderA, eA) => GenererMouvGVoiePrincipDirectionB(sender, e, avionAtterrissage, piste));
                        GereGauche.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {
                        CoordY += VITESSE;
                    }
                }
                //On génère le mouvement BAS ou HAUT
                Canvas.SetTop(avionAtterrissage, CoordY);
            }

           
        }

        /// <summary>
        /// Une méthode pour générer un mouvement DROIT sur la voie de principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvDVoiePrincip(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {
            LongueurHorizontale = 1000;
                        
            if (CoordX <= LongueurHorizontale)
            {
              
                if (CoordX >= LongueurHorizontaleMaxVoiePrincip)
                {
                   
                    //On arrete le déplacement en cours
                    GereDroit.Stop();
                    
                    //On commence le nouveau déplacement
                    GereHaut = new DispatcherTimer();
                   
                    GereHaut.Start();
                    GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHVoiePrincipDirectionG(sender, e, avionAtterrissage, piste));
                    GereHaut.Interval = TimeSpan.FromMilliseconds(50);
                }
                else
                {
                    CoordX += VITESSE;
                }
            }      
            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(avionAtterrissage, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement DROIT sur la voie de principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvDVoiePrincipDirectionH(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {
            LongueurHorizontale = 1000;

            if (CoordX <= LongueurHorizontale)
            {

                if (CoordX >= LongueurHorizontaleMaxVoiePrincip)
                {

                    //On arrete le déplacement en cours
                    GereDroit.Stop();

                    //On commence le nouveau déplacement
                    GereHaut = new DispatcherTimer();

                    GereHaut.Start();
                    GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHVoiePrincipDirectionGFinCycle(sender, e, avionAtterrissage, piste));
                    GereHaut.Interval = TimeSpan.FromMilliseconds(50);
                }
                else
                {
                    CoordX += VITESSE;
                }
            }
            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(avionAtterrissage, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement GAUCHE sur la voie de principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvGVoiePrincipDirectionB(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {
            LongueurHorizontale = 0;
            if (CoordX >= LongueurHorizontale)
            {

                if (CoordX <= LongueurHorizontaleMinVoiePrincip)
                {

                    //On arrete le déplacement en cours
                    GereGauche.Stop();

                    //Le prochain mouvement à générer est HAUT
                    GereBas = new DispatcherTimer();
                    GereBas.Start();
                    GereBas.Tick += new EventHandler((senderA, eA) => GenererMouvBVoiePrincip(sender, e, avionAtterrissage, piste));
                    GereBas.Interval = TimeSpan.FromMilliseconds(50);

                }
                else
                {
                    CoordX -= VITESSE;
                }
            }

            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(avionAtterrissage, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement GAUCHE sur la voie de principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvGVoiePrincipDirectionH(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {
            LongueurHorizontale = 0;
          
            if (CoordX >= LongueurHorizontale)
            {

                if (CoordX <= PositionYFinPremiereRangeeHangar)
                {
                    
                    //On arrete le déplacement en cours
                    GereGauche.Stop();

                    //Le prochain mouvement à générer est HAUT
                    GereHaut = new DispatcherTimer();
                    GereHaut.Start();
                    GereHaut.Tick += new EventHandler((senderA, eA) => GenererMouvHVoiePrincipDirectionD(sender, e, avionAtterrissage, piste));
                    GereHaut.Interval = TimeSpan.FromMilliseconds(50);
                }
                else
                {
                    CoordX -= VITESSE;
                }
            }

            //On génère le mouvement GAUCHE ou DROIT 
            Canvas.SetLeft(avionAtterrissage, CoordX);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement BAS sur la voie de principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvBVoiePrincip(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {
            LongueurVerticale = 1000;

            if (CoordY <= LongueurVerticale)
            {
               
                if (CoordY >= LongueurVerticaleMaxVoiePrincip)
                {
                    //On arrete le déplacement en cours
                    GereBas.Stop();
                    
                    //On commence le nouveau déplacement
                    GereDroit = new DispatcherTimer();

                    GereDroit.Start();
                    GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvDVoiePrincip(sender, e, avionAtterrissage, piste));
                    GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                }
                else
                {
                    CoordY += VITESSE;
                }
            }
              
            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(avionAtterrissage, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de principale direction GAUCHE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvHVoiePrincipDirectionG(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {
            LongueurVerticale = 0;
            
            if (CoordY >= LongueurVerticale)
            {

                if (CoordY <= PositionYDebutPremiereRangeeHangar)
                {
                    //On arrete le mouvement en cours
                    GereHaut.Stop();

                    GereGauche = new DispatcherTimer();

                    //Le prochain mouvement se fera dans la voie principale
                    GereGauche.Start();
                    GereGauche.Tick += new EventHandler((senderA, eA) => GenererMouvGVoiePrincipDirectionH(sender, e, avionAtterrissage, piste));
                    GereGauche.Interval = TimeSpan.FromMilliseconds(50);
                }
                CoordY -= VITESSE;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(avionAtterrissage, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de principale direction DROIT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvHVoiePrincipDirectionD(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {
            LongueurVerticale = 0;

            if (CoordY >= LongueurVerticale)
            {

                if (CoordY <= PositionYDebutDeuxiemeRangeeHangar)
                {
                    //On arrete le mouvement en cours
                    GereHaut.Stop();

                    GereDroit = new DispatcherTimer();

                    //Le prochain mouvement se fera dans la voie principale
                    GereDroit.Start();
                    GereDroit.Tick += new EventHandler((senderA, eA) => GenererMouvDVoiePrincipDirectionH(sender, e, avionAtterrissage, piste));
                    GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                }
                CoordY -= VITESSE;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(avionAtterrissage, CoordY);
        }

        /// <summary>
        /// Une méthode pour générer un mouvement HAUT sur la voie de principale direction DROIT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        public void GenererMouvHVoiePrincipDirectionGFinCycle(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {
            LongueurVerticale = 0;

            if (CoordY >= LongueurVerticale)
            {

                if (CoordY <= LongueurVerticaleMinVoiePrincip)
                {
                    //On arrete le mouvement en cours
                    GereHaut.Stop();

                    GereGauche = new DispatcherTimer();

                    //Le prochain mouvement se fera dans la voie principale
                    GereGauche.Start();
                    GereGauche.Tick += new EventHandler((senderA, eA) => GenererMouvGVoiePrincipDirectionB(sender, e, avionAtterrissage, piste));
                    GereGauche.Interval = TimeSpan.FromMilliseconds(50);
                }
                CoordY -= VITESSE;
            }

            //On génère le mouvement HAUT ou BAS 
            Canvas.SetTop(avionAtterrissage, CoordY);
        }


        /// <summary>
        /// Une méthode qui permet de démarrer un décollage 
        /// </summary>
        /// <param name="piste"></param>
        public void DemarreDecollage(int piste)
        {
            Rectangle AvionD = new Rectangle();
            Rectangle rectRotation = new Rectangle();
            ImageBrush a = new ImageBrush();
            a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/avion.png"));

            AvionD.Fill = a;
            AvionD.Width = 35;
            AvionD.Height = 35;
            //AvionD.Name = "vol" + i.ToString();
            Canvas.SetZIndex(AvionD, 100);

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
                    coordX = 395;
                    coordY = 230;
                    finPiste = 450;
                    finVoieService = 200;
                    PositionYDebutVoiePrincip = 230;
                    AvionD.RenderTransform = aiguillageHaut;
                    break;

                case 2:
                    coordX = 2100;
                    coordY = 352;
                    finPiste = 330;
                    PositionYDebutVoiePrincip = 330;
                    AvionD.RenderTransform = aiguillageGauche;
                    break;

                case 3:

                    coordX = 115;
                    coordY = 0;
                    finPiste = 405;
                    finVoieService = 160;
                    PositionYDebutVoiePrincip = 180;
                    AvionD.RenderTransform = aiguillageBas;
                    break;

                case 4:
                    coordX = 900;
                    coordY = 417;
                    finPiste = 280;
                    PositionYDebutVoiePrincip = 275;

                    AvionD.RenderTransform = aiguillageGauche;
                    break;

                case 5:
                    coordX = 900;
                    coordY = 37;
                    finPiste = 325;
                    PositionYDebutVoiePrincip = 70;
                    //finVoieService;

                    AvionD.RenderTransform = aiguillageGauche;
                    break;
            }

            //On dessine l'avion en fonction des coordonnées de la piste
            Canvas.SetLeft(AvionD, coordX);
            Canvas.SetTop(AvionD, coordY);

            //On ajoute l'avion au canvas

            Ec.cnvCarte.Children.Add(AvionD);

            //On initialise les attributs de la classe
            CoordY = coordY;
            CoordX = coordX;
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
            GererDecollage(AvionD, piste);

        }



        /// <summary>
        /// Une méthode pour gérer les décollages uniquement (VERSION 0.5) 
        /// </summary>
        public void GererDecollage(Rectangle avionDecollage, int piste)
        {
            GereDroit = new DispatcherTimer();

            //Ici l'avion quitte le hangar pour aller se placer sur une piste
            GereDroit.Start();
            // GereDroit.Tick += new EventHandler((sender, e) => DeplacementDroitHangar(sender, e, avionDecollage, piste));
            GereDroit.Interval = TimeSpan.FromMilliseconds(50);
        }

        /// <summary>
        /// Une méthode temporaire qui permet de déplacer un avion du hangar jusqua la voie de service (VERSION 0.5)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void DeplacementDroitHangar(object sender, EventArgs e, Image avionDecollage, int piste)
        //{
        //    if (HorizontalD >= 0)
        //    {
        //        if (HorizontalD == 100)
        //        {
        //            GereHautD = new DispatcherTimer();

        //            GereDroit.Stop();
        //            GereHautD.Start();
        //            GereHautD.Tick += new EventHandler((senderD, eD) => DeplacementHautVoieServiceD(sender, e, avionDecollage, piste));
        //            GereHautD.Interval = TimeSpan.FromMilliseconds(50);
        //        }
        //        else
        //        {
        //            HorizontalD += 1;
        //        }
        //    }

        //    Canvas.SetLeft(avionDecollage, HorizontalD);
        //}

        ///// <summary>
        ///// Une méthode temporaire qui permet de déplacer un avion de la voie de service jusqu'à la piste pour le décollage(VERSION 0.5)
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public void DeplacementHautVoieServiceD(object sender, EventArgs e, Image avionDecollage, int piste)
        //{
        //    if (VerticalD <= 700)
        //    {
        //        if ((VerticalD == 15 && piste == 1) || (VerticalD == 110 && piste == 2) || (VerticalD == 190 && piste == 3) || (VerticalD == 264 && piste == 4))
        //        {
        //            GereDroit = new DispatcherTimer();
        //            GereHautD.Stop();
        //            GereDroit.Start();
        //            GereDroit.Tick += new EventHandler((senderHD, eHD) => DeplacementDroitPisteD(sender, e, avionDecollage));
        //            GereDroit.Interval = TimeSpan.FromMilliseconds(50);
        //        }
        //        else
        //        {
        //            switch (piste)
        //            {
        //                case 1:
        //                    VerticalD -= 1;
        //                    break;
        //                case 2:
        //                    VerticalD -= 1;
        //                    break;
        //                case 3:
        //                    VerticalD += 1;
        //                    break;
        //                case 4:
        //                    VerticalD += 1;
        //                    break;
        //            }

        //            Canvas.SetTop(avionDecollage, VerticalD);
        //        }
        //    }

        //}


        //public void DeplacementDroitPisteD(object sender, EventArgs e, Image avionDecollage)
        //{

        //    if (HorizontalD >= 0)
        //    {
        //        if (HorizontalD == 800)
        //        {
        //            GereDroit.Stop();
        //            Ec.cnvCarte.Children.Remove(avionDecollage);

        //        }
        //        else
        //        {
        //            HorizontalD += 1;
        //        }

        //        Canvas.SetLeft(avionDecollage, HorizontalD);
        //    }
    }
}
