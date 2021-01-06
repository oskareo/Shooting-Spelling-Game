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

    //Player clase made of a line and a an ellipse
    public class Player : IGameEngine
    {

        public int X { get; set; }
        public int Y { get; set; }
        //  public double xPlayer;
        // public double yplayer;

        double newx = 0;//new x2 coordinate of the fireArm
        double newy = 0;//new y2 coordinate of the fireArm
        public int Firearmlength { get; private set; }
        public Point newPoint = new Point();





        bool checkMouse = false;//checks if the mouse has entered the canvas
        bool keypressed = false;//checks if a key has been pressed



        public double temp1;//x1 coordinate of the rotating point
        public double temp2;//y1 coordinate of the rotating point
        public double temp3;//x2 coordinate of the rotating point
        public double temp4;//y2 coordinate of the rotating point

        double specialX = 0;
        double specialY = 0;
        Ellipse refreshedbody = new Ellipse();
        Line refreshedFireArm = new Line();
        bool checkBodyBoundaries = false;
        bool checkFireArmBoundaries = false;

        List<Line> locSaved = new List<Line>();
        List<Point> locBodySaved = new List<Point>();

        int velocity = 5;
        int velocityMovementx;
        int velocityMovementy;
        Canvas playerCanv;

        public Player(int x1, int y1, Canvas c)
        {
            playerCanv = c;
            X = x1;
            Y = y1;
            refreshedbody = theBody();
            refreshedFireArm = fireArm();




        }


        public void move(Directions move)
        {

            //Removing the old drawing from the canvas
            playerCanv.Children.Remove(refreshedbody);
            playerCanv.Children.Remove(refreshedFireArm);
            keypressed = true;
            //gets the last coordinates after every move
            newPoint.X = newx;
            newPoint.Y = newy;

            if (move == Directions.up)
            {
                //updates the position travalled in the vertical sense
                velocityMovementy = velocityMovementy - velocity;

                specialY = specialY - 5;
            }
            else if (move == Directions.down)
            {
                //updates the position travalled in the vertical sense
                velocityMovementy = velocityMovementy + velocity;
                specialY = specialY + 5;
            }

            if (move == Directions.right)
            {
                //updates the position travalled in the horizontal sense
                velocityMovementx = velocityMovementx + velocity;
                specialX = specialX + 5;
            }
            else if (move == Directions.left)
            {
                //updates the position travalled in the horizontal sense
                velocityMovementx = velocityMovementx - velocity;
                specialX = specialX - 5;

            }

            //redrawing the body in the new location
            refreshedbody = theBody();
            refreshedFireArm = fireArm();
            keypressed = false;


        }


        public Ellipse theBody()
        {
            playerCanv.Children.Remove(refreshedbody);

            Ellipse body = new Ellipse();
            TransformGroup bodyTransGroup = new TransformGroup();
            TranslateTransform bodyTranslate = new TranslateTransform();
            Point bodyLocation = new Point();
            body.Width = 50;
            body.Height = 50;
            SolidColorBrush sb = new SolidColorBrush(Colors.Black);
            body.Fill = sb;
            
            bodyTranslate.X = X + bodyTranslate.X + velocityMovementx;
            bodyTranslate.Y = Y + bodyTranslate.Y + velocityMovementy;


            //Sets the body to stay inside the boundaries
            bodyLocation.X = bodyLocation.X + bodyTranslate.X;
            bodyLocation.Y = bodyLocation.Y + bodyTranslate.Y;

            Point aTempPoint = new Point();
            aTempPoint = bodyLocation;
            int lastPointIndex = 0;
            savebODYLocation(aTempPoint);


            //Saves the locations of the player
            if (bodyLocation.X <= 1)
            {

                lastPointIndex = locBodySaved.LastIndexOf(aTempPoint);
                checkBodyBoundaries = true;


                bodyTranslate.X = 1 ;
                bodyLocation.X = 1;

                

            }
            else if ((bodyLocation.X >= playerCanv.ActualWidth - 50) && playerCanv.ActualWidth - 50 > 0)
            {

                lastPointIndex = locBodySaved.LastIndexOf(aTempPoint);
                checkBodyBoundaries = true;
                bodyTranslate.X = playerCanv.ActualWidth - 50 ;
                bodyLocation.X = playerCanv.ActualWidth - 50;
                
            }

            if (bodyLocation.Y <= 1)
            {

                lastPointIndex = locBodySaved.LastIndexOf(aTempPoint);
                checkBodyBoundaries = true;
                bodyTranslate.Y = 1 /*locBodySaved[locBodySaved.Count-3].X*/;
                bodyLocation.Y = 1;
                //bodyTranslate.Y = locBodySaved[lastPointIndex - 1].Y;
                //bodyLocation.Y = locBodySaved[lastPointIndex - 1].Y;
            }
            else if ((bodyLocation.Y >= playerCanv.ActualHeight - 50) && playerCanv.ActualHeight - 50 > 0)
            {
                savebODYLocation(aTempPoint);
                checkBodyBoundaries = true;
                bodyTranslate.Y = playerCanv.ActualHeight - 50/*locBodySaved[locBodySaved.Count-3].X*/;
                bodyLocation.Y = playerCanv.ActualHeight - 50;
                //bodyTranslate.Y = locBodySaved[lastPointIndex - 1].Y;
                //bodyLocation.Y = locBodySaved[lastPointIndex - 1].Y;
            }


            
            bodyTransGroup.Children.Add(bodyTranslate);
            body.RenderTransform = bodyTransGroup;


            //player Redrawn
            playerCanv.Children.Add(body);
            return body;
        }

        //line made up of two poits
        //2nd point is where the bullets "come out" from
        public Line fireArm()
        {
            //Removes fire arm from previous locations
            playerCanv.Children.Remove(refreshedFireArm);
            Line fireArm = new Line();

            TranslateTransform fireArmbodyTranslate = new TranslateTransform();
            TransformGroup fireArmbodyTransGroup = new TransformGroup();
            fireArm.Stroke = Brushes.Black;
            fireArm.StrokeThickness = 10;

            // double x1bound, x2bound, y1bound, y2bound;


            //dynamicaly redraws every single new location 
            //gives the impression of moving and rotating
            fireArm.X1 = fireArm.X1 + X + 25 + velocityMovementx;
            fireArm.Y1 = fireArm.Y1 + Y + 25 + velocityMovementy;
            temp1 = fireArm.X1;
            temp2 = fireArm.Y1;
            //if mouse outside the canvas control and player is being moved
            if (checkMouse == false && keypressed == false)
            {
                fireArm.X2 = fireArm.X2 + X + 75 + velocityMovementx;
                fireArm.Y2 = fireArm.Y2 + Y + 25 + velocityMovementy;
                temp3 = fireArm.X2;
                temp4 = fireArm.Y2;
                velocityMovementx = 0;
                velocityMovementy = 0;
            }
            if (checkMouse == true && keypressed == false)
            {
                fireArm.X2 = fireArm.X2 + newx;
                fireArm.Y2 = fireArm.Y2 + newy;
                temp3 = fireArm.X2;
                temp4 = fireArm.Y2;
            }

            if (checkMouse == false && keypressed == true)
            {
                if (newPoint.X == 0 && newPoint.Y == 0)
                {
                    fireArm.X2 = fireArm.X2 + X + 75 + velocityMovementx;
                    fireArm.Y2 = fireArm.Y2 + Y + 25 + velocityMovementy;
                    temp3 = fireArm.X2;
                    temp4 = fireArm.Y2;
                }
                else
                {
                    fireArm.X2 = fireArm.X2 + newPoint.X + specialX;
                    fireArm.Y2 = fireArm.Y2 + newPoint.Y + specialY;
                    temp3 = fireArm.X2;
                    temp4 = fireArm.Y2;
                }

            }
           



            //prevents the pregram from breaking
            if (temp3 == 0)
            {
                temp3 = 0.1;

            }
            else if (temp4 == 0)
            {
                temp4 = 0.1;
            }


            //backup fireArm
            Line tempLine = new Line();
            tempLine = fireArm;
            savePreviousLocation(tempLine);


            //boundaries. "keeps" the player from going out of bounds
            //it does not work properly. there is a bug in THIS class
            if (fireArm.X2 <= 1 || fireArm.X1 <= 1)

            {
                checkFireArmBoundaries = true;
                fireArm.X2 = locSaved[locSaved.Count - 2].X2;
                fireArm.X1 = locSaved[locSaved.Count - 2].X1;


            }
            else if ((fireArm.X2 >= playerCanv.ActualWidth || fireArm.X1 >= playerCanv.ActualWidth) && playerCanv.ActualWidth > 0)
            {
                checkFireArmBoundaries = true;
                fireArm.X2 = locSaved[locSaved.Count - 2].X2;
                fireArm.X1 = locSaved[locSaved.Count - 2].X1;
            }

            if (fireArm.Y2 <= 1 || fireArm.Y1 <= 1)

            {
                checkFireArmBoundaries = true;
                fireArm.Y2 = locSaved[locSaved.Count - 2].Y2;
                fireArm.Y1 = locSaved[locSaved.Count - 2].Y1;
            }
            else if ((fireArm.Y2 >= playerCanv.ActualHeight || fireArm.Y1 >= playerCanv.ActualHeight) && playerCanv.ActualHeight > 0)
            {
                checkFireArmBoundaries = true;
                fireArm.Y2 = locSaved[locSaved.Count - 2].Y2;
                fireArm.Y1 = locSaved[locSaved.Count - 2].Y1;
            }

           


            fireArm.RenderTransformOrigin = new Point(fireArm.X1 / temp3, fireArm.Y1 / temp4);
            fireArmbodyTransGroup.Children.Add(fireArmbodyTranslate);
            fireArm.RenderTransform = fireArmbodyTransGroup;


            playerCanv.Children.Add(fireArm);


            return fireArm;

        }

        //Calculates the location of a new point every time the mouse is inside the canvas control to create the fireAre(fireArm is a line )
        public void rotate(Point location, bool mouseinScreen)
        {

            playerCanv.Children.Remove(refreshedbody);
            playerCanv.Children.Remove(refreshedFireArm);

            checkMouse = mouseinScreen;


            double distantanceMtoB;



            distantanceMtoB = Math.Sqrt(Math.Pow((int)(location.X - temp1), 2) + Math.Pow((int)(location.Y - temp2), 2));

            //Avoids sigularitites when the mouse is places in the same place as the first point of the fireArm
            if (distantanceMtoB == 0)
            {
                distantanceMtoB = 1;
            }

            //calculates the new coordinates(x,y) of the line relative to the position of the mouse to redraw it
            newx = ((50 / distantanceMtoB) * (location.X - temp1)) + temp1;
            newy = ((50 / distantanceMtoB) * (location.Y - temp2)) + temp2;



            refreshedbody = theBody();
            refreshedFireArm = fireArm();
            specialX = 0;//resets it to 0 to keep the firearm from losing its shape
            specialY = 0;//resets it to 0 to keep the firearm from losing its shape


            checkMouse = false;
        }

        void savePreviousLocation(Line a)
        {
            locSaved.Add(a);
        }

        void savebODYLocation(Point a)
        {
            locBodySaved.Add(a);
        }
    }
}






