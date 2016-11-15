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
    /// </summary>
    public class Animation
    {
        //Déclaration des constantes de la classe Animation
        public const int VITESSE = 10;

        //Déclaration des attributs de la classe Animation
        public DispatcherTimer GereDroit;

        public DispatcherTimer GereGauche;

        public DispatcherTimer GereHaut;

        public DispatcherTimer GereBas;

        public EcranControleur Ec { get; set; }

        public int CoordY { get; set; }

        public int CoordX { get; set; }

        public int FinPiste { get; set; }

        public int FinVoieService { get; set; }

        public int LongueurVerticale {get; set;}

        public int LongueurHorizontale { get; set; }

        public int LongueurVerticaleVs { get; set; }

        public int LongueurHorizontaleVs { get; set; }

        public int i=0;
      
        public Direction DirectionVent { get; set; }

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
            Canvas.SetTop(Ec.Piste4, 355);
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
            Canvas.SetTop(Ec.Piste2, 420);
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
            Canvas.SetTop(Ec.Piste5, 40);

          
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
        public Rectangle GererAiguillageAvion(Rectangle Avion)
        {
            //Déclaration des variables
            int degre =180;

            Rectangle rectRotation = new Rectangle();



            RotateTransform aiguillageDroit;



            RotateTransform aiguillageGauche = new RotateTransform(270, 14, 14);

            RotateTransform aiguillageBas = new RotateTransform(180, 14, 14);

            while(degre!=90)
            {
                degre -= 20;

                aiguillageDroit = new RotateTransform(degre, 14, 14);
                Avion.RenderTransform = aiguillageDroit;

            }

           

            return rectRotation;

        }


        /// <summary>
        /// Une méthode pour dessiner le hangar sur la carte des pistes
        /// </summary>
        public void DessinerHangar()
        {        
            Ec.hangar.Stroke = new SolidColorBrush(Colors.Black);
            Ec.hangar.StrokeThickness = 2;
            Ec.hangar.Width = 750;
            Ec.hangar.Height = 260;

            Canvas.SetLeft(Ec.hangar, 160);
            Canvas.SetTop(Ec.hangar, 75);
            Canvas.SetZIndex(Ec.hangar, 100);
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



        //}

        //-----------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Une méthode pour gérer les attérissages uniquement 
        /// </summary>
        public void GererAtterrissage(Rectangle avionAtterrissage, int piste)
        {
            //Déclaration des variables
            GereBas = new DispatcherTimer();
           
            //MessageBox.Show(avionAtterrissage.Name.ToString());

            //Si il s'agit de la piste 1 ou 3
            if (piste == 1 || piste == 3)
            {              
                GereBas.Start(); 
                GereBas.Tick += new EventHandler((sender, e) => DeplacementAtterrissage(sender, e, avionAtterrissage, piste));
                GereBas.Interval = TimeSpan.FromMilliseconds(35);
            }
            else if (piste == 2 || piste == 4 || piste == 5)  //Si il s'agit de la piste 2, 4 ou 5
            {
                GereGauche = new DispatcherTimer();

                GereGauche.Start();
                GereGauche.Tick += new EventHandler((sender, e) => DeplacementAtterrissage(sender, e, avionAtterrissage, piste));
                GereGauche.Interval = TimeSpan.FromMilliseconds(50);
            }
        }
    
        /// <summary>
        /// Une méthode pour le déplacement des avions en atterrissage seulement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        /// <param name="coordX"></param>
        /// <param name="coordY"></param>
        public void DeplacementAtterrissage(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {       
            //Si il s'agit de la piste 1 ou 3         
            if (piste == 1 || piste == 3)
            {
                if (CoordY <= LongueurVerticale)
                {                                                
                        if (CoordY >= FinPiste )
                        {
                 
                            //On arrete le déplacement en cours
                            GereBas.Stop();

                            //On aiguille l'avion dans la bonne direction
                            //GererAiguillageAvion(avionAtterrissage);

                            ////On commence le nouveau déplacement
                            GereDroit = new DispatcherTimer();

                            GereDroit.Start();
                            GereDroit.Tick += new EventHandler((senderA, eA) => DeplacementAtterrissageDroit(sender, e, avionAtterrissage, piste));
                            GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                        }
                        else
                        {                     
                            CoordY += VITESSE;                                        
                        }                     
                    }                        
               
            
                //On génère le déplacement 
                Canvas.SetTop(avionAtterrissage, CoordY);
            }
            else if (piste == 2 || piste == 4 || piste == 5)    //Si il s'agit de la piste 2, 4 ou 5
            {               
                if(CoordX >= LongueurHorizontale)
                {
                    //if(CoordX<=950)
                    //{
                    //    ImageBrush a = new ImageBrush();
                    //    a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/avion.png"));
                    //    avionAtterrissage.Fill = a;
                    //}


                    if (CoordX <= FinPiste)
                    {
                       
                        //On arrete le déplacement en cours
                        GereGauche.Stop();

                        //On commence le nouveau déplacement
                        GereHaut = new DispatcherTimer();

                        GereHaut.Start();
                        GereHaut.Tick += new EventHandler((senderA, eA) => DeplacementHautBasAtterrissageVs(sender, e, avionAtterrissage, piste));
                        GereHaut.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {                   
                         CoordX -= VITESSE;                                       
                    }
                }

                //On génère le déplacement 
                Canvas.SetLeft(avionAtterrissage, CoordX);

            }                 
        }

        /// <summary>
        /// Une méthode pour les déplacements d'atterrissage sur la voie de service 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="avionAtterrissage"></param>
        /// <param name="piste"></param>
        /// <param name="coordX"></param>
        /// <param name="coordY"></param>
        public void DeplacementAtterrissageDroit(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {                           
            //Si il s'agit de la piste 1 ou 3
            //if (piste == 1 || piste == 3)
            //{
                if (CoordX <= LongueurHorizontale)
                {          
                    
                        
                    if (CoordX >= FinVoieService)
                    {
                        //On arrete le déplacement en cours
                        GereDroit.Stop();
               
                        //On commence le nouveau déplacement
                        GereHaut = new DispatcherTimer();

                        GereHaut.Start();
                        GereHaut.Tick += new EventHandler((senderA, eA) => DeplacementHautBasAtterrissageVs(sender, e, avionAtterrissage, piste));
                        GereHaut.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {
                        CoordX += VITESSE;
                    }
                }

                //On génère le déplacement 
                Canvas.SetLeft(avionAtterrissage, CoordX);
        }

        public void DeplacementHautBasAtterrissageVs(object sender, EventArgs e, Rectangle avionAtterrissage, int piste)
        {
            //Déclaration des variables
            int longueurVerticaleMax=0;
            FinVoieService = 600;
            LongueurHorizontale = 600;

            if (piste == 1 || piste == 2 || piste == 3 || piste == 4)
            {
                if (CoordY >= longueurVerticaleMax)
                {
               
                    if (CoordY <= LongueurVerticaleVs)
                    {
                        GereHaut.Stop();
                        //TODO coder le chemin pour se rendre au hangar
                        //if (piste == 1 || piste == 3)
                        //{
                        //    GereDroit = new DispatcherTimer();

                        //    GereDroit.Start();
                        //    GereDroit.Tick += new EventHandler((senderA, eA) => DeplacementAtterrissageDroit(sender, e, avionAtterrissage, piste));
                        //    GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                        //}
                    }
                    CoordY -= VITESSE;                 
                }
                Canvas.SetTop(avionAtterrissage, CoordY);
            }
            else if (piste == 5)    
            {
                longueurVerticaleMax = 500;

                if (CoordY <= longueurVerticaleMax)
                {
                    if (CoordY >= LongueurVerticaleVs)
                    {
                        GereHaut.Stop();

                        //TODO coder le chemin pour se rendre au hangar
                        //GereDroit = new DispatcherTimer();

                        //GereDroit.Start();
                        //GereDroit.Tick += new EventHandler((senderA, eA) => DeplacementAtterrissageDroit(sender, e, avionAtterrissage, piste));
                        //GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {
                        CoordY += VITESSE;
                    }
                }
                Canvas.SetTop(avionAtterrissage, CoordY);
            }           
        }




        //public void DeplacementHautVoieServiceA(object sender, EventArgs e, Image avionAtterrissage, int piste)
        //{


        //    if (VerticalA >= 0)
        //    {

        //        if (VerticalA == 100)
        //        {
        //            GereGauche = new DispatcherTimer();
        //            GereHautA.Stop();
        //            GereGauche.Start();
        //            GereGauche.Tick += new EventHandler((senderHaut, eHaut) => DeplacementGauchetHangarA(sender, e, avionAtterrissage));
        //            GereGauche.Interval = TimeSpan.FromMilliseconds(50);
        //        }
        //        else
        //        {

        //            if (piste == 1)
        //            {
        //                VerticalA += 1;
        //            }
        //            else
        //            {
        //                VerticalA -= 1;
        //            }

        //            Canvas.SetTop(avionAtterrissage, VerticalA);
        //        }
        //    }

        //}

        //public void DeplacementGauchetHangarA(object sender, EventArgs e, Image avionAtterrissage)
        //{
        //    if (HorizontalA >= 0)
        //    {
        //        //Pour tester la longueur de la distance horizontale à parcourir
        //        if (HorizontalA == 40)
        //        {
        //            GereGauche.Stop();
        //            Ec.cnvCarte.Children.Remove(avionAtterrissage);
        //            HorizontalA = 700;
        //        }
        //        else
        //        {
        //            HorizontalA -= 1;
        //        }

        //        Canvas.SetLeft(avionAtterrissage, HorizontalA);
        //    }
        //}

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
            AvionA.Name = "vol" + i.ToString();
            Canvas.SetZIndex(AvionA, 100);

        

            int coordY=0;
            int coordX=0;
            int finPiste=0;
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
                        LongueurVerticaleVs = 350;
                        AvionA.RenderTransform = aiguillageBas;                                              
                    break;

                case 2:                 
                        coordX = 2100;
                        coordY = 352;
                        finPiste = 330;
                        //LongueurVerticaleVs = 310;
                        LongueurVerticaleVs = 350;
                        AvionA.RenderTransform = aiguillageGauche;                 
                    break;

                case 3:
                  
                        coordX = 115;
                        coordY = 0;
                        finPiste = 405;
                        finVoieService = 160;
                        //LongueurVerticaleVs = 180;
                        LongueurVerticaleVs = 350;
                        AvionA.RenderTransform = aiguillageBas;                 
                    break;

                case 4:
                    coordX = 900;
                    coordY = 417;
                    finPiste = 280;
                    //LongueurVerticaleVs = 275;
                    LongueurVerticaleVs = 350;
                    AvionA.RenderTransform = aiguillageGauche;
                    break;

                case 5:
                    coordX = 900;
                    coordY = 37;
                    finPiste = 325;
                    LongueurVerticaleVs = 70;
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
  
            if(piste==1 || piste==3)
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
            GererAtterrissage(AvionA, piste);
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
            AvionD.Name = "vol" + i.ToString();
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
                    LongueurVerticaleVs = 230;
                    AvionD.RenderTransform = aiguillageHaut;
                    break;

                case 2:
                    coordX = 2100;
                    coordY = 352;
                    finPiste = 330;
                    LongueurVerticaleVs = 310;
                    AvionD.RenderTransform = aiguillageGauche;
                    break;

                case 3:

                    coordX = 115;
                    coordY = 0;
                    finPiste = 405;
                    finVoieService = 160;
                    LongueurVerticaleVs = 180;
                    AvionD.RenderTransform = aiguillageBas;
                    break;

                case 4:
                    coordX = 900;
                    coordY = 417;
                    finPiste = 280;
                    LongueurVerticaleVs = 275;

                    AvionD.RenderTransform = aiguillageGauche;
                    break;

                case 5:
                    coordX = 900;
                    coordY = 37;
                    finPiste = 325;
                    LongueurVerticaleVs = 70;
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

        
    }
}
