using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
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
using System.Windows.Shapes;

namespace Application_Dev_Project
{
    
    public partial class UserName : Window, IGameEngine
    {

        private string uName;
        public bool safeThreadClosing;
        private bool check = false;
        private string typeOfGame;

       
        public UserName()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            safeThreadClosing = false;
        }

        //gets the user nickname and type of game that wants to be played
        private void getUNbtn_Click(object sender, RoutedEventArgs e)
        {
            uName = userameNick.Text;
            

             checkUName(uName);

        }

        private void checkUName(string name)
        {

                if (name == "" || name == " ")
                {

                    MessageBox.Show("Incorrect nickname. Nickname can not be empty");
                }
            else if (typeOfGame == "Squares")
            {
                MessageBox.Show("Unavailable Option. Try the Spelling(available) option");
            }

            else
                {
                    check = true;
                    
                    
                        SaveUserDetails(name);
                        windowOpener newPlayingWindow = new windowOpener(typeOfWindow.gameScreen, typeOfGame,name);
                       

                        safeThreadClosing = true;
                        this.Close();
                    
                    
                  
                }
               

        }

        public void move(Directions move)
        {
           
        }

        public void rotate(Point location, bool checkmouseInScreen)
        {
           // throw new NotImplementedException();
        }

        private void SpellingRbtn_Checked(object sender, RoutedEventArgs e)
        {
            typeOfGame = "Spelling";
        }

        private void SquaresRbtn_Checked(object sender, RoutedEventArgs e)
        {
            typeOfGame = "Squares";
        }

        private void SaveUserDetails(string usernick)
        {
            OleDbConnection connectionToWords = WordsDBConnection();

            string wordQuery = "";
            

            wordQuery = "INSERT INTO playerDetails( userName) VALUES ( '" +usernick+ "'  )";
            OleDbCommand myCommand = new OleDbCommand(wordQuery, connectionToWords);
            try
            {
                connectionToWords.Open();
                myCommand.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in DBHandler" + ex);
            }
            finally
            {
                connectionToWords.Close();
            }
        }

        private static OleDbConnection WordsDBConnection()
        {

            

            String Wordsconn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = C:\Users\oskar\Documents\App Dev Project\Application Dev Project\Application Dev Project\cwDBExample.mdb; User Id= admin; password=;";
          

            return new OleDbConnection(Wordsconn);
        }

        public bool Continue()
        {
            bool theanswer=check;

            return theanswer;
        }
    }
}





