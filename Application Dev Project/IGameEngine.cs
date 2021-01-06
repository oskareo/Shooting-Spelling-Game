using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Application_Dev_Project
{

    //interface to make the bodies move
    interface IGameEngine
    {
        void move(Directions move);
        void rotate(Point location, bool checkmouseInScreen);

    }
}




