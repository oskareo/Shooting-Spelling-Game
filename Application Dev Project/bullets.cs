using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Application_Dev_Project
{
    class bullets : movingObject
    {

        private double X { get; set; }//tracks the x cordinate of the bullet's location
        private double Y { get; set; }//tracks the y cordinate of the bullet's location
        public Ellipse theBullet = new Ellipse();//stores the last location of the bullets
        public Path therectPath = new Path();//the rectangle around the bullets
        public Canvas bulletCanvas = new Canvas();
        private bool isFired = false;//checks if bullet can move
        private double velocityx = 0;//speed in the x axes
        private double velocityy = 0;//speed in the y axes
        private double angle = 0;//angle of ratation of the fireArm
       

        public Rect shield = new Rect();//the rectangle around the bullet



        public bullets(Canvas c, double x, double y)
        {
            X = x;
            Y = y;
            bulletCanvas = c;
            theBullet = bullet();//adds bullets to the canvas

        }

        //Creating the body of the bullet
        public Ellipse bullet()

        {
            
            TranslateTransform bulletTranslate = new TranslateTransform();
            TransformGroup bulletAsALineTransGroup = new TransformGroup();
            GeometryGroup gG = new GeometryGroup();
            Ellipse bulletAsEllipse = new Ellipse();

            Path rectPath = new Path();
            rectPath.Stroke = Brushes.Black;
            rectPath.StrokeThickness = 1;

            shield.Width = bulletAsEllipse.Width = 10;
            shield.Height = bulletAsEllipse.Height = 10;
            SolidColorBrush sb = new SolidColorBrush(Colors.Red);
            bulletAsEllipse.Fill = sb;
            //specify its initial location
            bulletAsEllipse.Margin = new Thickness(X - 5, Y - 5, 0, 0);

            //specify its initial location
            shield.X = X - 5;
            shield.Y = Y - 5;



            if (isFired == true)
            {
                //make the bullet move
                bulletTranslate.X = bulletTranslate.X + velocityx;
                bulletTranslate.Y = bulletTranslate.Y - velocityy;
                shield.X = shield.X + velocityx;
                shield.Y = shield.Y - velocityy;


            }

            RectangleGeometry rG = new RectangleGeometry();

            rG.Rect = shield;
            gG.Children.Add(rG);
            rectPath.Data = gG;
            therectPath = rectPath;
            bulletAsALineTransGroup.Children.Add(bulletTranslate);
            bulletAsEllipse.RenderTransform = bulletAsALineTransGroup;

            //Adds bullets to the canvas
            bulletCanvas.Children.Add(bulletAsEllipse);
            bulletCanvas.Children.Add(rectPath);
            return bulletAsEllipse;
        }

        public override void move(Directions move)
        {

            isFired = true;
            //Deletes the previous bullet
            bulletCanvas.Children.Remove(theBullet);
            bulletCanvas.Children.Remove(therectPath);

            if (move == Directions.mouseTrajectory)
            {
                velocityx = velocityx + (8 * Math.Cos(getAngle()));
                velocityy = velocityy + (8 * Math.Sin(getAngle()));
            }

            // bullet redrawn 
            theBullet = bullet();

        }


        //the agle between the cursor and the body of the location of the character
        public void setAngle(double x1, double y1, double x2, double y2)
        {

            
            double numb = x2 - x1;
            double numb2 = y1 - y2;
            double newang = Math.Atan2(numb2, numb);
            angle = newang;
        }

        private double getAngle()
        {
            return angle;
        }

        public override void rotate(Point location, bool checkmouseInScreen)
        {

        }


    }
}




