using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public DispatcherTimer GereDroit;

        public DispatcherTimer GereGauche;

        public DispatcherTimer GereHautA;

        public DispatcherTimer GereHautD;

        public DispatcherTimer GereBas;

        public EcranControleur Ec { get; set; }

        public int HorizontalA = 700;

        public int HorizontalD = 10;

        public int VerticalA;

        public int VerticalD = 176;

       
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
        }

        /// <summary>
        /// Une méthode pour dessiner la piste 1
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
        /// Une méthode pour dessiner la piste 2
        /// </summary>
        /// <param name="ib">L'image d'une piste</param>
        public void DessinerPiste2(ImageBrush ib)
        {
           

            Ec.Piste2.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Piste2.StrokeThickness = 2;
            Ec.Piste2.Fill = ib;
            Ec.Piste2.Width = 380;
            Ec.Piste2.Height = 40;
            Canvas.SetLeft(Ec.Piste2, 437);
            Canvas.SetTop(Ec.Piste2, 410);
        }

        /// <summary>
        /// Une méthode pour dessiner la piste 3
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
        /// Une méthode pour dessiner la piste 4
        /// </summary>
        /// <param name="ib">L'image d'une piste</param>
        public void DessinerPiste4(ImageBrush ib)
        {
            Rectangle rectRotation = new Rectangle();

            RotateTransform rotation = new RotateTransform(90, 14, 14);

            Ec.Piste4.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Piste4.StrokeThickness = 2;
            Ec.Piste4.Fill = ib;
            Ec.Piste4.Width = 380;
            Ec.Piste4.Height = 40;
            Canvas.SetLeft(Ec.Piste4, 437);
            Canvas.SetTop(Ec.Piste4, 330);
        }

        /// <summary>
        /// Une méthode pour dessiner la piste 5
        /// </summary>
        /// <param name="ib">L'image d'une piste</param>
        public void DessinerPiste5(ImageBrush ib)
        {
            Ec.Piste4.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Piste4.StrokeThickness = 2;
            Ec.Piste4.Fill = ib;
            Ec.Piste4.Width = 370;
            Ec.Piste4.Height = 50;
            Canvas.SetLeft(Ec.Piste4, 95);
            Canvas.SetTop(Ec.Piste4, 260);
        }

        /// <summary>
        /// Une méthode pour dessiner les voies de service
        /// </summary>
        public void DessinerVoieService()
        {
            Rectangle rectRotation = new Rectangle();
            ImageBrush a = new ImageBrush();
            a.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/AirAmbe;component/Images/asphalte.jpg"));

            Ec.Asphalthe1.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Asphalthe1.StrokeThickness = 2;
            Ec.Asphalthe1.Fill = a;
            Ec.Asphalthe1.Width = 35;
            Ec.Asphalthe1.Height = 40;
            Canvas.SetLeft(Ec.Asphalthe1, 80);
            Canvas.SetTop(Ec.Asphalthe1, 100);

            Ec.Asphalthe2.Stroke = new SolidColorBrush(Colors.Black);
            Ec.Asphalthe2.StrokeThickness = 2;
            Ec.Asphalthe2.Fill = a;
            Ec.Asphalthe2.Width = 35;
            Ec.Asphalthe2.Height = 40;
            Canvas.SetLeft(Ec.Asphalthe2, 80);
            Canvas.SetTop(Ec.Asphalthe2, 175);

            RotateTransform rotation = new RotateTransform(90, 14, 14);
            Ec.AsphalthePrincipale.RenderTransform = rotation;
            Ec.AsphalthePrincipale.Stroke = new SolidColorBrush(Colors.Black);
            Ec.AsphalthePrincipale.StrokeThickness = 2;
            Ec.AsphalthePrincipale.Fill = a;
            Ec.AsphalthePrincipale.Width = 210;
            Ec.AsphalthePrincipale.Height = 30;
            Canvas.SetLeft(Ec.AsphalthePrincipale, 110);
            Canvas.SetTop(Ec.AsphalthePrincipale, 60);
        }


        public void DessinerHangar()
        {
            Ec.hangar.Fill = Brushes.Black;
            Ec.hangar.Width = 80;
            Ec.hangar.Height = 270;

            Canvas.SetTop(Ec.hangar, 29);
            Canvas.SetZIndex(Ec.hangar, 100);
        }

        /// <summary>
        /// Une méthode pour gérer les décollages uniquement (VERSION 0.5) 
        /// </summary>
        public void GererDecollage(Image avionDecollage, int piste)
        {
            GereDroit = new DispatcherTimer();

            //Ici l'avion quitte le hangar pour aller se placer sur une piste
            GereDroit.Start();
            GereDroit.Tick += new EventHandler((sender, e) => DeplacementDroitHangar(sender, e, avionDecollage, piste));
            GereDroit.Interval = TimeSpan.FromMilliseconds(50);
        }

        /// <summary>
        /// Une méthode temporaire qui permet de déplacer un avion du hangar jusqua la voie de service (VERSION 0.5)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeplacementDroitHangar(object sender, EventArgs e, Image avionDecollage, int piste)
        {
            if (HorizontalD >= 0)
            {
                if (HorizontalD == 100)
                {
                    GereHautD = new DispatcherTimer();

                    GereDroit.Stop();
                    GereHautD.Start();
                    GereHautD.Tick += new EventHandler((senderD, eD) => DeplacementHautVoieServiceD(sender, e, avionDecollage, piste));
                    GereHautD.Interval = TimeSpan.FromMilliseconds(50);
                }
                else
                {
                    HorizontalD += 1;
                }
            }

            Canvas.SetLeft(avionDecollage, HorizontalD);
        }

        /// <summary>
        /// Une méthode temporaire qui permet de déplacer un avion de la voie de service jusqu'à la piste pour le décollage(VERSION 0.5)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeplacementHautVoieServiceD(object sender, EventArgs e, Image avionDecollage, int piste)
        {
            if (VerticalD <= 700)
            {
                if ((VerticalD == 15 && piste == 1) || (VerticalD == 110 && piste == 2) || (VerticalD == 190 && piste == 3) || (VerticalD == 264 && piste == 4))
                {
                    GereDroit = new DispatcherTimer();
                    GereHautD.Stop();
                    GereDroit.Start();
                    GereDroit.Tick += new EventHandler((senderHD, eHD) => DeplacementDroitPisteD(sender, e, avionDecollage));
                    GereDroit.Interval = TimeSpan.FromMilliseconds(50);
                }
                else
                {
                    switch (piste)
                    {
                        case 1:
                            VerticalD -= 1;
                            break;
                        case 2:
                            VerticalD -= 1;
                            break;
                        case 3:
                            VerticalD += 1;
                            break;
                        case 4:
                            VerticalD += 1;
                            break;
                    }

                    Canvas.SetTop(avionDecollage, VerticalD);
                }
            }

        }


        public void DeplacementDroitPisteD(object sender, EventArgs e, Image avionDecollage)
        {

            if (HorizontalD >= 0)
            {
                if (HorizontalD == 800)
                {
                    GereDroit.Stop();
                    Ec.cnvCarte.Children.Remove(avionDecollage);

                }
                else
                {
                    HorizontalD += 1;
                }

                Canvas.SetLeft(avionDecollage, HorizontalD);
            }



        }


        /// <summary>
        /// Une méthode pour gérer les attérissages uniquement (VERSION 0.5) 
        /// </summary>
        public void GererAtterrissage(Image avionAtterrissage, int piste)
        {
            GereGauche = new DispatcherTimer();

            GereGauche.Start();
            GereGauche.Tick += new EventHandler((sender, e) => DeplacementGauchetAtterrissage(sender, e, avionAtterrissage, piste));
            GereGauche.Interval = TimeSpan.FromMilliseconds(50);
        }

        private void DeplacementGauchetAtterrissage(object sender, EventArgs e, Image avionAtterrissage, int piste)
        {
            if (HorizontalA >= 0)
            {

                //Pour tester la longueur de la distance horizontale à parcourir
                if (HorizontalA == 100)
                {
                    GereHautA = new DispatcherTimer();

                    GereGauche.Stop();
                    GereHautA.Start();
                    GereHautA.Tick += new EventHandler((senderHaut, eHaut) => DeplacementHautVoieServiceA(sender, e, avionAtterrissage, piste));
                    GereHautA.Interval = TimeSpan.FromMilliseconds(50);
                }
                else
                {
                    HorizontalA -= 1;
                }
            }


            Canvas.SetLeft(avionAtterrissage, HorizontalA);
        }

        public void DeplacementHautVoieServiceA(object sender, EventArgs e, Image avionAtterrissage, int piste)
        {


            if (VerticalA >= 0)
            {

                if (VerticalA == 100)
                {
                    GereGauche = new DispatcherTimer();
                    GereHautA.Stop();
                    GereGauche.Start();
                    GereGauche.Tick += new EventHandler((senderHaut, eHaut) => DeplacementGauchetHangarA(sender, e, avionAtterrissage));
                    GereGauche.Interval = TimeSpan.FromMilliseconds(50);
                }
                else
                {

                    if (piste == 1)
                    {
                        VerticalA += 1;
                    }
                    else
                    {
                        VerticalA -= 1;
                    }

                    Canvas.SetTop(avionAtterrissage, VerticalA);
                }
            }

        }

        public void DeplacementGauchetHangarA(object sender, EventArgs e, Image avionAtterrissage)
        {
            if (HorizontalA >= 0)
            {
                //Pour tester la longueur de la distance horizontale à parcourir
                if (HorizontalA == 40)
                {
                    GereGauche.Stop();
                    Ec.cnvCarte.Children.Remove(avionAtterrissage);
                    HorizontalA = 700;
                }
                else
                {
                    HorizontalA -= 1;
                }

                Canvas.SetLeft(avionAtterrissage, HorizontalA);
            }
        }

        public void DemarreAtterrissage(int piste)
        {
            HorizontalA = 700;

            Image AvionA = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/avion.png");
            b.EndInit();
            AvionA.Source = b;
            AvionA.Width = 40;

            switch (piste)
            {
                case 1:
                    VerticalA = 15;
                    Canvas.SetTop(AvionA, 15);

                    break;
                case 2:
                    VerticalA = 110;
                    Canvas.SetTop(AvionA, 110);

                    break;
                case 3:
                    VerticalA = 190;
                    Canvas.SetTop(AvionA, 190);
                    break;
                case 4:
                    VerticalA = 264;
                    Canvas.SetTop(AvionA, 264);
                    break;
            }

            Canvas.SetLeft(AvionA, HorizontalA);
            Ec.cnvCarte.Children.Add(AvionA);
            GererAtterrissage(AvionA, piste);
        }

        public void DemarreDecollage(int piste)
        {
            VerticalD = 176;
            HorizontalD = 0;
            Image AvionD = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("pack://application:,,,/AirAmbe;component/Images/avion.png");
            b.EndInit();
            AvionD.Source = b;
            AvionD.Width = 40;

            Canvas.SetTop(AvionD, VerticalD);

            Ec.cnvCarte.Children.Add(AvionD);

            GererDecollage(AvionD, piste);
        }
    }
}
