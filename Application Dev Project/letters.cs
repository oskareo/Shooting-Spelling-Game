


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;

using System.Threading;

namespace Application_Dev_Project
{
    class letters :System.Windows.Controls.Image, IGameEngine
    {
        Canvas letterCanvas = new Canvas();
       public System.Windows.Shapes.Path therectPath = new System.Windows.Shapes.Path();
       // double angle = 0;
        double velocityx = 0; //x component of speed
        double velocityy = 0;//x component of speed
        double velx = 0;// x component of speed 
        double vely = 0;// x component of speed 
        public char letter;//gets the letter
        public double X { get; set; }//tracks the x cordinate of the letter's location
        public double Y { get; set; }//tracks the y cordinate of the letter's location
        
        BitmapImage imagePath;
        public Rect shield = new Rect();
      
        public letters(char charac, Canvas c)
        {
            letterCanvas = c;
            letter = charac;
            string path = @"C:\Users\user\Documents\APP DEVELOPMENT COURSEWORK\Application Development Project\Application Development Project\Application Dev Project\Application Dev Project\" + charac+".jpg";
          
            imagePath = new BitmapImage();
           

            this.Stretch = Stretch.Fill;
            shield.Width = this.Width = 30;
            shield.Height=this.Height = 30;
          
            appear();

            imagePath.BeginInit();
            imagePath.UriSource = new Uri(path,UriKind.RelativeOrAbsolute);
            imagePath.EndInit();

            this.Source = imagePath;
            
          
            letterCanvas.Children.Add(this);



        }
        public void move(Directions move)
        {
            //removes letter from  previous location
            letterCanvas.Children.Remove(therectPath);
            letterCanvas.Children.Remove(this);
           
            TranslateTransform letterTranslate = new TranslateTransform();
            TransformGroup letterTransGroup = new TransformGroup();
            GeometryGroup gG = new GeometryGroup();
           
            System.Windows.Shapes.Path  rectPath = new System.Windows.Shapes.Path();
            rectPath.Stroke = Brushes.Black;
            rectPath.StrokeThickness = 1;

            

            if (move == Directions.random)
            {

                //letters move
                velocityx = velocityx + velx;
                velocityy = velocityy + vely;
                letterTranslate.X = letterTranslate.X + velocityx;
                letterTranslate.Y = letterTranslate.Y + velocityy;
                X = X + velocityx;
                Y = Y + velocityy;
                shield.X = shield.X+velx;
                shield.Y = shield.Y+ +vely;
            }

            RectangleGeometry rG = new RectangleGeometry();

            rG.Rect = shield;
            gG.Children.Add(rG);
            rectPath.Data = gG;
            therectPath = rectPath;


            letterTransGroup.Children.Add(letterTranslate);
            this.RenderTransform = letterTransGroup;
            //adds letter into new location
            letterCanvas.Children.Add(rectPath);
            letterCanvas.Children.Add(this);
            

           
        }

        public void rotate(System.Windows.Point location, bool checkmouseInScreen)
        {
           
        }


        //each new letter shows from a different random location
        private void appear()
        {
            Random side= new Random();
            Random placex = new Random();
            Random placey = new Random();
            double x = placex.Next(50, Convert.ToInt16(letterCanvas.ActualWidth-50));
            double y = placey.Next(50, Convert.ToInt16(letterCanvas.ActualHeight - 50));

            int sidechoice = 0;
            Directions theside = new Directions();
            sidechoice = side.Next(0,4);
            if (sidechoice == 0)
            {
                theside = Directions.up;
                this.Margin= new Thickness(x,0,0,0);
                velx = 0;
                vely = 5;
                X = x;
               Y = 0;
                shield.X = x;
                shield.Y = 0;
                
            }
            else if (sidechoice==1)
            {
                theside = Directions.down;
                this.Margin = new Thickness(x, letterCanvas.ActualHeight, 0, 0);
                velx = 0;vely = -5;
               X = x;
               Y = letterCanvas.ActualHeight;
                shield.X = x;
                shield.Y = letterCanvas.ActualHeight;

            }
            else if (sidechoice == 2)
            {
                theside = Directions.right;
                this.Margin = new Thickness(0, y, 0, 0);
                velx = 5;
                vely = 0;
                X = 0;
               Y = y;
                shield.X = 0;
                shield.Y = y;

            }
            else if (sidechoice == 3)
            {
                theside = Directions.left;
                this.Margin = new Thickness(letterCanvas.ActualWidth, y, 0, 0);
                velx = -5;
                vely = 0;
               X = letterCanvas.ActualWidth;
               Y = y;
                shield.X = letterCanvas.ActualWidth;
                shield.Y = y;
            }
        }
    }
}



