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
        public const int VITESSE = 2;

        public const int VITESSEATTERRISSAGE = 4;

        //Déclaration des attributs de la classe Animation
        public DispatcherTimer GereDroit;

        public DispatcherTimer GereGauche;

        public DispatcherTimer GereHautA;

        public DispatcherTimer GereHautD;

        public DispatcherTimer GereBas;

        public EcranControleur Ec { get; set; }

        public int i=0;
        //public int HorizontalA = 700;

        //public int HorizontalD = 10;

        //public int VerticalA;

        //public int VerticalD = 176;

        public int CoordY { get; set; }

        public int CoordX { get; set; }
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
            ImageBrush ImagePiste = new ImageBrush();
            ImagePiste.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/piste.png"));

            //if (NbPistes == 2)
            //{

            //}
            //else if (NbPistes == 3)
            //{

            //}
            //else if (NbPistes == 4)
            //{

            //}
            //else if (NbPistes == 5)
            //{

            //}

            DessinerPiste1(ImagePiste);
            DessinerPiste2(ImagePiste);
            DessinerPiste3(ImagePiste);
            DessinerPiste4(ImagePiste);
            DessinerPiste5(ImagePiste);
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
            Ec.Piste1.Width = 350;
            Ec.Piste1.Height = 40;
            Canvas.SetLeft(Ec.Piste1, 45);
            Canvas.SetTop(Ec.Piste1, 30);
            
        }

        /// <summary>
        /// Une méthode pour dessiner la piste 2 (verticale)
        /// </summary>
        /// <param name="ib">L'image d'une piste</param>
        public void DessinerPiste2(ImageBrush ib)
        {           
            Ec.Piste2.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Piste2.StrokeThickness = 2;
            Ec.Piste2.Fill = ib;
            Ec.Piste2.Width = 450;
            Ec.Piste2.Height = 40;
            Canvas.SetLeft(Ec.Piste2, 437);
            Canvas.SetTop(Ec.Piste2, 410);
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
            Ec.Piste3.Width = 350;
            Ec.Piste3.Height = 40;
            Canvas.SetLeft(Ec.Piste3, 137);
            Canvas.SetTop(Ec.Piste3, 30);
        }

        /// <summary>
        /// Une méthode pour dessiner la piste 4 (verticale)
        /// </summary>
        /// <param name="ib">L'image d'une piste</param>
        public void DessinerPiste4(ImageBrush ib)
        {
            Rectangle rectRotation = new Rectangle();

            RotateTransform rotation = new RotateTransform(90, 14, 14);

            Ec.Piste4.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Piste4.StrokeThickness = 2;
            Ec.Piste4.Fill = ib;
            Ec.Piste4.Width = 450;
            Ec.Piste4.Height = 40;
            Canvas.SetLeft(Ec.Piste4, 437);
            Canvas.SetTop(Ec.Piste4, 330);
        }

        /// <summary>
        /// Une méthode pour dessiner la piste 5 (Horizontale)
        /// </summary>
        /// <param name="ib">L'image d'une piste</param>
        public void DessinerPiste5(ImageBrush ib)
        {          
            Ec.Piste5.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Piste5.StrokeThickness = 2;
            Ec.Piste5.Fill = ib;
            Ec.Piste5.Width = 450;
            Ec.Piste5.Height = 40;
            Canvas.SetLeft(Ec.Piste5, 337);
            Canvas.SetTop(Ec.Piste5, 30);

          
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
        public void GererAiguillageAvion(Rectangle Avion)
        {
            //TODO
        }


        /// <summary>
        /// Une méthode pour dessiner le hangar sur la carte des pistes
        /// </summary>
        public void DessinerHangar()
        {        
            Ec.hangar.Stroke = new SolidColorBrush(Colors.Black);
            Ec.hangar.StrokeThickness = 2;
            Ec.hangar.Width = 500;
            Ec.hangar.Height = 150;

            Canvas.SetLeft(Ec.hangar, 400);
            Canvas.SetTop(Ec.hangar, 129);
            Canvas.SetZIndex(Ec.hangar, 100);
        }

        /// <summary>
        /// Une méthode pour gérer les décollages uniquement (VERSION 0.5) 
        /// </summary>
        //public void GererDecollage(Image avionDecollage, int piste)
        //{
        //    GereDroit = new DispatcherTimer();

        //    //Ici l'avion quitte le hangar pour aller se placer sur une piste
        //    GereDroit.Start();
        //    GereDroit.Tick += new EventHandler((sender, e) => DeplacementDroitHangar(sender, e, avionDecollage, piste));
        //    GereDroit.Interval = TimeSpan.FromMilliseconds(50);
        //}

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

        /// <summary>
        /// Une méthode temporaire qui permet de déplacer un avion de la voie de service jusqu'à la piste pour le décollage(VERSION 0.5)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// Une méthode pour gérer les attérissages uniquement 
        /// </summary>
        public void GererAtterrissage(Rectangle avionAtterrissage, int piste, int coordX,int coordY)
        {
            //Déclaration des variables
            GereBas = new DispatcherTimer();
            CoordY = coordY;
            CoordX = coordX;

            //MessageBox.Show(avionAtterrissage.Name.ToString());

            //Si il s'agit de la piste 1 ou 3
            if (piste == 1 || piste == 3)
            {              
                GereBas.Start(); 
                GereBas.Tick += new EventHandler((sender, e) => DeplacementAtterrissage(sender, e, avionAtterrissage, piste, coordX, coordY));
                GereBas.Interval = TimeSpan.FromMilliseconds(50);
            }
            else if (piste == 2 || piste == 4 || piste == 5)  //Si il s'agit de la piste 2, 4 ou 5
            {
                GereGauche = new DispatcherTimer();

                GereGauche.Start();
                GereGauche.Tick += new EventHandler((sender, e) => DeplacementAtterrissage(sender, e, avionAtterrissage, piste, coordX,coordY));
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
        public void DeplacementAtterrissage(object sender, EventArgs e, Rectangle avionAtterrissage, int piste, int coordX, int coordY)
        {
            //Déclaration des variables
            int longueurMaxVerticale = 500;
            int longueurMinHorizontale = 0;          
            int finPiste;
        

            //Si il s'agit de la piste 1 ou 3
            if (piste == 1 || piste == 3)
            {
                if (CoordY <= longueurMaxVerticale)
                {
                    finPiste = 350;

                    if(CoordY == finPiste)
                    {
                        //On arrete le déplacement en cours
                        GereBas.Stop();

                        //On commence le nouveau déplacement
                        GereDroit = new DispatcherTimer();

                        GereDroit.Start();
                        GereDroit.Tick += new EventHandler((senderA, eA) => DeplacementAtterrissage(sender, e, avionAtterrissage, piste, coordX, coordY));
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
                if(CoordX >= longueurMinHorizontale)
                {          
                    //Pour tester la longueur de fin de piste en fonction de la piste         
                    if(piste==5)
                    {
                        finPiste = 300;
                    }
                    else
                    {
                        finPiste = 400;
                    }

                    if(CoordX == finPiste)
                    {
                        //On arrete le déplacement en cours
                        GereGauche.Stop();
                        
                        //On commence le nouveau déplacement


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


        public void DeplacementAtterrissageHaut(object sender, EventArgs e, Rectangle avionAtterrissage, int piste, int coordX, int coordY)
        {
            //Déclaration des variables


            if (piste == 1 || piste == 3)
            {
                if (CoordY <= 500)
                {
                    //GereDroit = new DispatcherTimer();

                    //GereBas.Stop();
                    // GereDroit.Start();

                    if (CoordY == 350)
                    {
                        GereBas.Stop();

                        GereDroit = new DispatcherTimer();

                        GereDroit.Start();
                        GereDroit.Tick += new EventHandler((senderA, eA) => DeplacementAtterrissage(sender, e, avionAtterrissage, piste, coordX, coordY));
                        GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                    }
                    else
                    {
                        CoordY = CoordY + VITESSE;
                    }
                }
                Canvas.SetTop(avionAtterrissage, CoordY);
            }
            else if (piste == 2 || piste == 4 || piste == 5)    //Si il s'agit de la piste 2, 4 ou 5
            {

            }


            //if (HorizontalA >= 0)
            //{

            //    //Pour tester la longueur de la distance horizontale à parcourir
            //    if (HorizontalA == 100)
            //    {
            //        GereHautA = new DispatcherTimer();

            //        GereGauche.Stop();
            //        GereHautA.Start();
            //        GereHautA.Tick += new EventHandler((senderHaut, eHaut) => DeplacementHautVoieServiceA(sender, e, avionAtterrissage, piste));
            //        GereHautA.Interval = TimeSpan.FromMilliseconds(50);
            //    }
            //    else
            //    {
            //        HorizontalA -= 1;
            //    }
            //}
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
            //string idVol = "0";//----------------->Variable temporaire en attente d'oli
            int coordY=0;
            int coordX=0;

            //Image AvionA = new Image();
            //BitmapImage b = new BitmapImage();
            //b.BeginInit();
            //b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/avion.png");
            //b.EndInit();
            //AvionA.Source = b;
            //AvionA.Width = 35;
            //AvionA.Name = "imgAvion" + idVol; //Le nom de l'image

            ////On réinitialise les variables par défaut
            //HorizontalA = 700;

            //foreach(Image img in TrouverEnfant<Image>(Ec.cnvCarte))
            //{

            //}
           
      


            Rectangle AvionA = new Rectangle();
            Rectangle rectRotation = new Rectangle();
            ImageBrush a = new ImageBrush();
            a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/avion.png"));           
            AvionA.Fill = a;
            AvionA.Width = 30;
            AvionA.Height = 35;

            
            AvionA.Name = "vol" + i.ToString();
            i++;
           
          
            RotateTransform aiguillageDroit = new RotateTransform(90, 14, 14);
            RotateTransform aiguillageGauche = new RotateTransform(270, 14, 14);
           
            RotateTransform aiguillageBas = new RotateTransform(180, 14, 14);

            //Selon le numéro de la piste on initialise les coordonnées de départ ainsi que l'orientation de l'avion
            switch (piste)
            {
                case 1:                   
                    coordX = 36;
                    coordY = 0;

                    AvionA.RenderTransform = aiguillageBas;
                    break;

                case 2:
                    coordX = 900;
                    coordY = 335;

                    AvionA.RenderTransform = aiguillageGauche;
                    break;

                case 3:
                    coordX = 128;
                    coordY = 0;

                    AvionA.RenderTransform = aiguillageBas;
                    break;

                case 4:
                    coordX = 900;
                    coordY = 413;
                    AvionA.RenderTransform = aiguillageGauche;
                    break;

                case 5:
                    coordX = 900;
                    coordY = 34;

                    AvionA.RenderTransform = aiguillageGauche;
                    break;
            }

            //On dessine l'avion en fonction des coordonnées de la piste
            Canvas.SetLeft(AvionA, coordX);
            Canvas.SetTop(AvionA, coordY);
      
            //On ajoute l'avion au canvas
            Ec.cnvCarte.Children.Add(AvionA);

            //On appelle la fonction d'atterrissage en fonction de sa piste ainsi que de ses coordonnées
            GererAtterrissage(AvionA, piste, coordX, coordY);
        }

        /// <summary>
        /// Une méthode qui permet de démarrer un décollage 
        /// </summary>
        /// <param name="piste"></param>
        public void DemarreDecollage(int piste)
        {
            //VerticalD = 176;
            //HorizontalD = 0;
            Image AvionD = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/avion.png");
            b.EndInit();
            AvionD.Source = b;
            AvionD.Width = 40;
           
            //Canvas.SetTop(AvionD, VerticalD);

            Ec.cnvCarte.Children.Add(AvionD);

            //GererDecollage(AvionD, piste);
        }

        public static IEnumerable<T> TrouverEnfant<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in TrouverEnfant<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        
    }
}
