using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Threading;


using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Threading;

namespace Application_Dev_Project
{
    //Opens new windows
    class windowOpener
    {
      
        private string newgame;
        private string userNick;
        private bool checkigUserNameWin;
        public windowOpener(typeOfWindow screen, string s, string nickname)
        {
            userNick = nickname;
            newgame = s;
            
            if (screen == typeOfWindow.gameScreen)
            {
                
                OpenAnewPlayingScreen();
            }
            else if (screen == typeOfWindow.mainMenu)
            {

               
            }
            else if (screen == typeOfWindow.settings)
            {
               
            }
            else if (screen == typeOfWindow.instructions)
            {
                
            }
            else if (screen == typeOfWindow.userInput)
            {
                
                OpenUserInput();
                



            }

            
        }


        internal string getTypeOfGame()
        {
           

            return newgame;
        }

        
        private void OpenAnewPlayingScreen()
        {


            playGameScreen aNewScreen = new playGameScreen(getTypeOfGame(),userNick);
           
            aNewScreen.Show();
            
            
        }
        private void OpenAnewMainMenu(object obj)
        {
            MainWindow aNewScreen = new MainWindow();
            aNewScreen.Show();
           
        }
        private void OpenSettings()
        {

        }
        private void OpenInstructions()
        {

        }
        private void OpenUserInput()
        {


            UserName uNWindow = new UserName();
          
            do
            {
                uNWindow.ShowDialog();
                checkigUserNameWin = uNWindow.Continue();
            } while (checkigUserNameWin==false);
          
            



        }

       
    }
}





