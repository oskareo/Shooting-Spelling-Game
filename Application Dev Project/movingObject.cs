using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Application_Dev_Project
{
    abstract class movingObject
    {


        //First designed for polymorphism

        public virtual void move(Directions move) { }
        public virtual void rotate(Point location, bool checkmouseInScreen) { }

    }


}




