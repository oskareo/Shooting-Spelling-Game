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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;

using System.Threading;


//The screen where the game is played
//There is a bug that will sometimes delete a letter acidentally
//The bug I believe is due the excesive use of global variables in multiple parts of my code

namespace Application_Dev_Project
{
    /// <summary>
    /// Interaction logic for playGameScreen.xaml
    /// </summary>
    public partial class playGameScreen : Window
    {
        bool reset;//checks if the textblock can be reset to "x" whenever a new word is spelled correctly
        int letterOrder = 0;//counts how many letters have been hit by a bullet
        Player thePlayer;
        Point mousePosition { get; set; }
        DispatcherTimer gameTimer;
        DispatcherTimer bulletsTimer;//animates the bullets
        DispatcherTimer lettersTimer;//animates the letters
        List<bullets> theBullets;
        List<letters> theLetters;
        List<Number> theNumbers;
        //Number aNumber;
        //letters aLetter;
        private int letterInterval;//interval between teh emergence of a letter and another one
        private string theTypeOfGame;//checks what type of game is going to be played
        private int bulletCount;//increments every time a bullets is fired
        int theTime = 0;
        int countWordsInterv = 0;//interval between teh emergence of a word and another one
        public string getLetter="";//gets new lleter from list of letters
        Thread theSpellingGame;//starts a thread to play the game
        //Thread theSquaresGame;
        List<String> listofWords;
        //Image theimage;
        //BitmapImage theBitImg;
        int countToStartSpelling = 0;
        int wordindex = 0;
        int tempint = 0;
        bool newWord=true;
        int startshooting;
        int thepoints;
        int realSCore = 0;//checks the score
        int indexLetter=0;
        bool finishGame = false;
        private bool GameOver=false;
        bool initiatingLetters = false;
        private int timeRemaining;
        string theWord = "";
        private string spelledWord;
        private string userNickName;//gets the player nickname

        public playGameScreen(string c, string nick)
        {
            InitializeComponent();
            WindowStartupLocation= System.Windows.WindowStartupLocation.CenterScreen;
            setTypeOfGame(c);
            setUserNick(nick);
            //string getLetter;
            gameTimer = new DispatcherTimer();
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);

            //theBitImg = new BitmapImage();
            //theimage = new Image();
            //theimage.Stretch = Stretch.Fill;
            //theimage.Width = 50;
            //theimage.Height = 50;

            //theBitImg.BeginInit();
            //theBitImg.UriSource = new Uri(@"C:\Users\oskar\Desktop\app dev proj\Application Dev Project\Application Dev Project/b.jpg");
            //theBitImg.EndInit();

            //theimage.Source = theBitImg;

            //gameCanvas.Children.Add(theimage);



            bulletsTimer = new DispatcherTimer();
            bulletsTimer.Tick += bulletsTimer_Tick;
            bulletsTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            bulletsTimer.Start();


            lettersTimer = new DispatcherTimer();
            lettersTimer.Tick += lettersTimer_Tick;
            lettersTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
           // lettersTimer.Start();


            gameCanvas.Background = Brushes.Gray;

            //initiates the location of a player
            thePlayer = new Player(250, 100, gameCanvas);
            


            theBullets = new List<bullets>();
            theLetters = new List<letters>();
            theNumbers = new List<Number>();

            typeOfGame(getTypeOfGame());
        }

        private void setUserNick(string nick)
        {
            userNickName = nick;
            
            //throw new NotImplementedException();
        }
        private string getUserNick()
        {
           

            return userNickName;
        }


        private void lettersTimer_Tick(object sender, EventArgs e)
        {


            //checks if the animation can start
            if (countToStartSpelling>=1)
            {
                for (int i = 0; i < theLetters.Count; i++)
                {
                    theLetters[i].move(Directions.random);
                    if (initiatingLetters == false)
                    {
                        //checks if a letter is outside of the canvas
                        LetterBoundaries(theLetters[i]);
                    }
                }
            }

            if (countToStartSpelling >= 1 && bulletCount >= 1/*getLetter.Length*/)
            {
                for (int i = 0; i < theBullets.Count; i++)
                {
                    // theLetters[i].move(Directions.random);
                    for (int j = 0; j < theLetters.Count; j++)
                    {
                        if(bulletCount >= 1)
                        {
                            //checks for collision between the bullets and the letters
                            boundaries(theBullets[i], theLetters[j]);
                        }
         
                    }

                }

            }

            //resets the value to zero for every new word
            if (countWordsInterv >= 30)
            {
                countToStartSpelling = 0;
            }


        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            theTime++;
            

            letterInterval++;
            
          
            gameWatch.Text = theTime.ToString();
            timeRemainingLbl.Text = timeRemaining.ToString();

            nicklbl.Text = getUserNick();
            pointstxtB.Text = Convert.ToString(GetScore());
            //removes the lletrs from the canvas if the time limit has passed
            if (countWordsInterv>=30&&countWordsInterv<=34)
            {
                for (int i=0;i<theLetters.Count;i++)
                {

                    RemoveLetters(theLetters[i]);
                }
               
               
               
            }

            //Puts a new word to spelled into the game
            if (countWordsInterv == 0 || countWordsInterv >= 35/*&&Wordcompleted==true*/)
            {

                //resets the textblocs to 0 for the new coming word
                if (timeRemaining < 30)
                {
                    timeRemaining = 30;

                    letter1.Text = "x";
                    letter2.Text = "x";
                    letter3.Text = "x";
                    letter4.Text = "x";
                    letter5.Text = "x";
                    letter6.Text = "x";
                    letter7.Text = "x";
                    letter8.Text = "x";
                    letter9.Text = "x";
                    letter10.Text = "x";
                    letter11.Text = "x";
                    letter12.Text = "x";
                    letter13.Text = "x";
                    //timeRemaining = 30;

                }
               
                getLetter = listofWords[wordindex];
                
                wordindex++;
                countWordsInterv = 0;
                
            }

            elementsLbl.Text = getLetter;
            

            
            countWordsInterv++;
            timeRemaining--;
            //if (timeRemaining < 0 || reset == true)
            //{
               
            //}

            if (letterInterval == 1)



            {
               

                //restes itself to 0 to allow the words of a new word the be added
                if (tempint >= countWordsInterv)
                {

                   tempint = 0;
                 
                }
                

                if (tempint <= getLetter.Length - 1)
                {
                    //ads a letter to the canvas until tempint is bigger that the word
                    AddLetter(getLetter[tempint]);
                    initiatingLetters = true;
                }
                else
                {
                    initiatingLetters = false;
                }
                   
               
                tempint++;
                letterInterval = 0;
            }


            //incerases to allow the motion of the letters and bullets
            countToStartSpelling++;

            if (timeRemaining <= 0)

            {

                letterOrder = 0;
            }
            
            checkIfSpellingCorrect();
        }
        //adds new letter to cnavas
        private void AddLetter(char character)
        {
            
            letters anyletter = new letters(character, gameCanvas);
            // gameCanvas.Children.Add(anyletter);
            theLetters.Add(anyletter);
            //starts animation timer of to animate letters
            lettersTimer.Start();
          

        }

        //moves the player

        private void theGame_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.Key == Key.Up)
            {
                thePlayer.move(Directions.up);
            }
            if (e.Key == Key.Down)
            {
                thePlayer.move(Directions.down);
            }
            if (e.Key == Key.Left)
            {
                thePlayer.move(Directions.left);
            }
            if (e.Key == Key.Right)
            {
                thePlayer.move(Directions.right);
            }
            
        }

        internal void setTypeOfGame(string v)
        {
            theTypeOfGame = v;

           
        }
        internal string getTypeOfGame()
        {


            return theTypeOfGame;
           
        }

        private void returnMainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindown = new MainWindow();

            newWindown.Show();
            SaveUserDetails(GetScore(), getUserNick());
            this.Close();
        }

        private void endGameBtn_Click_1(object sender, RoutedEventArgs e)
        {

            GameOver = true;
            SaveUserDetails(GetScore(),getUserNick());

            this.Close();
        }


        //creates new bullets every time leftButton of the mouse is clicked
        private void gameCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            bullets  bullet = new bullets(gameCanvas, thePlayer.temp3, thePlayer.temp4);
            bullet.setAngle(thePlayer.temp1, thePlayer.temp2, thePlayer.temp3, thePlayer.temp4);
            fireBullet(bullet);
            bulletCount += 1;
           
            //increase everytime bullets if fired
            startshooting++;

        }
        private void fireBullet(bullets b)
        {

            theBullets.Add(b);

        }
        private void bulletsTimer_Tick(object sender, EventArgs e)
        {
           //checks if there is any bullets to be moved
            if ( startshooting >= 1)
            {
                for (int i = 0; i < theBullets.Count; i++)
                {

                    theBullets[i].move(Directions.mouseTrajectory);
                    bulletBoundaries(theBullets[i]);
                }
            }

            //checks if ther is any bullet in the canvas 
            //if there is any it checks if it collides with any letter

            if (bulletCount >= 1 && countToStartSpelling >= 1/* getLetter.Length*/)
            {
                for (int i = 0; i < theBullets.Count; i++)
                {

                    

                    for (int j = 0; j < theLetters.Count - 1; j++)
                    {
                        if (bulletCount>=1)
                        {
                            //collision detection
                            boundaries(theBullets[i], theLetters[j]);
                        }
                       
                    }

                }

            }

            //checks if there is a new letter to be spelled if the one before as completely disappeared
            if (countWordsInterv >= 30)
            {
                countToStartSpelling = 0;
            }
           





        }

        //position of the mouse inside the canvas
        private void gameCanvas_MouseMove_1(object sender, MouseEventArgs e)
        {
            mousePosition = e.GetPosition(gameCanvas);

            thePlayer.rotate(mousePosition, true);
        }

        private void gameCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            thePlayer.newPoint = e.GetPosition(gameCanvas);
        }


        private void typeOfGame(string game)
        {
            if (game == "Spelling")
            {
                theSpellingGame = new Thread(playSpelling);
                theSpellingGame.Start();
                //playSpelling();
                //gameCanvas.Children.Add(/*aLetter*/);
            }
            if (game == "Squares")
            {
                //playSquares();

            }
        }



        private void boundaries(bullets b, letters l)
        {

          
            //if (b.shield.X.Equals(l.shield.X)&& b.shield.Y.Equals(l.shield.Y))
            //{
                

            //    for (int i=0;i<=getLetter.Length-1;i++)
            //    {
            //        if (l.letter == getLetter[i] && indexLetter == i)
            //        {
                       
            //        }
                    
                   
            //    }
               
            //    indexLetter++;
            //    bulletCount = 0;
            //    // CountScore();
            //    theLetters.Remove(l);
            //       gameCanvas.Children.Remove(l.therectPath);
            //      gameCanvas.Children.Remove(l);
            //    theBullets.Remove(b);
            //       gameCanvas.Children.Remove(b.theBullet);
            //       gameCanvas.Children.Remove(b.therectPath);
              
            //}

            if (b.shield.IntersectsWith(l.shield))
            {
               
                letterOrder++;
                
                DisplayLetters(letterOrder,l);

                indexLetter++;
                bulletCount = 0;
               
                theLetters.Remove(l);
                gameCanvas.Children.Remove(l.therectPath);
                gameCanvas.Children.Remove(l);
                theBullets.Remove(b);
                gameCanvas.Children.Remove(b.theBullet);
                gameCanvas.Children.Remove(b.therectPath);
              
            }
           

        }

        private void DisplayLetters(int letterOrder, letters l)
        {
           
           switch (letterOrder)
                {

                case 1:
                    letter1.Text =Convert.ToString( l.letter);
                    break;

                case 2:
                    letter2.Text = Convert.ToString(l.letter);
                    break;
                case 3:
                    letter3.Text = Convert.ToString(l.letter);
                    break;
                case 4:
                    letter4.Text = Convert.ToString(l.letter);
                    break;
                case 5:
                    letter5.Text = Convert.ToString(l.letter);
                    break;
                case 6:
                    letter6.Text = Convert.ToString(l.letter);
                    break;
                case 7:
                    letter7.Text = Convert.ToString(l.letter);
                    break;
                case 8:
                    letter8.Text = Convert.ToString(l.letter);
                    break;
                case 9:
                    letter9.Text = Convert.ToString(l.letter);
                    break;
                case 10:
                    letter10.Text = Convert.ToString(l.letter);
                    break;
                case 11:
                    letter11.Text = Convert.ToString(l.letter);
                    break;
                case 12:
                    letter12.Text = Convert.ToString(l.letter);
                    break;
                case 13:
                    letter13.Text = Convert.ToString(l.letter);
                    break;
            }


            
            
            // throw new NotImplementedException();
        }

        private void checkIfSpellingCorrect()
        {
            int completeTheWord = 13-getLetter.Length;

            switch (completeTheWord)

            {
                case 0:
                    theWord = getLetter;
                    break;

                case 1:
                    theWord = getLetter + "x";
                    break;
                case 2:
                    theWord = getLetter + "xx";
                    break;
                case 3:
                    theWord = getLetter + "xxx";
                    break;
                case 4:
                    theWord = getLetter + "xxxx";
                    break;
                case 5:
                    theWord = getLetter + "xxxxx";
                    break;
                case 6:
                    theWord = getLetter + "xxxxxx";
                    break;
                case 7:
                    theWord = getLetter + "xxxxxxx";
                    break;
            }

            spelledWord = letter1.Text + letter2.Text + letter3.Text + letter4.Text + letter5.Text + letter6.Text + letter7.Text + letter8.Text + letter9.Text + letter10.Text + letter11.Text + letter12.Text + letter13.Text;

            if (spelledWord==theWord)
            {
                 reset = true;
                
                if (reset == true)
                {
                    CountScore();
                  
                    letter1.Text = "x";
                    letter2.Text = "x";
                    letter3.Text = "x";
                    letter4.Text = "x";
                    letter5.Text = "x";
                    letter6.Text = "x";
                    letter7.Text = "x";
                    letter8.Text = "x";
                    letter9.Text = "x";
                    letter10.Text = "x";
                    letter11.Text = "x";
                    letter12.Text = "x";
                    letter13.Text = "x";
                }
               

            }

            
        }

        private void bulletBoundaries(bullets b)
        {
            if (b.shield.X >= gameCanvas.ActualWidth-10 || b.shield.X <= 1)
            {
                theBullets.Remove(b);
                gameCanvas.Children.Remove(b.theBullet);
                gameCanvas.Children.Remove(b.therectPath);
               
                bulletCount = 0;
            }
            else if (b.shield.Y >= gameCanvas.ActualHeight-10 || b.shield.Y <= 1)
            {
                theBullets.Remove(b);
                gameCanvas.Children.Remove(b.theBullet);
                gameCanvas.Children.Remove(b.therectPath);
                
                bulletCount = 0;
            }

        }

        private void LetterBoundaries(letters l)
        {
            if (l.shield.X >= gameCanvas.ActualWidth-50 || l.shield.X <= 1)
            {
               
                gameCanvas.Children.Remove(l.therectPath);
                gameCanvas.Children.Remove(l);
                theLetters.Remove(l);


            }
            else if (l.shield.Y >= gameCanvas.ActualHeight-50 || l.shield.Y <= 1)
            {
               
                gameCanvas.Children.Remove(l.therectPath);
                gameCanvas.Children.Remove(l);
                theLetters.Remove(l);
            }


        }

        private void CountScore()
        {
            
           
                realSCore++;
            
        }

        private int GetScore()
        {

            return realSCore;
        }


        void playSpelling()
        {
        
            listofWords = new List<string>();
           

            GetTheletters();

          
            gameTimer.Start();


           
        }

        //get the 50 words from the database and adds it to a list of letters
        private void GetTheletters()
        {
            int count = 0;
            listofWords.Clear();
            OleDbConnection connectionToWords = WordsDBConnection();
            string wordQuery;


            wordQuery = "SELECT * FROM student";
            OleDbCommand myCommand = new OleDbCommand(wordQuery, connectionToWords);
            try
            {
                connectionToWords.Open();
                OleDbDataReader wordReader = myCommand.ExecuteReader();
                while (wordReader.Read())
                {
                    listofWords.Add(wordReader["fullName"].ToString());
                    count++;
                }
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

        //private void playSquares()
        //{

            
        //}

        private static OleDbConnection WordsDBConnection()
        {

            

            String Wordsconn = @"Provider = Microsoft.Jet.OLEDB.4.0;Data Source = C:\Users\user\Documents\APP DEVELOPMENT COURSEWORK\Application Development Project\Application Development Project\Application Dev Project\cwDBExample.mdb;User Id= admin;password=;";
            // C: \Users\user\Documents\APP DEVELOPMENT COURSEWORK\Application Development Project\Application Development Project\Application Dev Project
            //
           // Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\mydatabase.mdb; User Id = admin; Password =;

            return new OleDbConnection(Wordsconn);
        }

        private void CheckIfGameOver()
        {
            if (theTime == 1500)
            {
                GameOver = true;
            }
        }

        //updated the players score when he decides to leave the game

        private void SaveUserDetails(int userscore, string theNickName)
        {
            OleDbConnection playerData = WordsDBConnection();

            OleDbCommand updateComand = new OleDbCommand();
            updateComand.CommandType = CommandType.Text;
            updateComand.CommandText = "UPDATE playerDetails SET userName = @userName, score = @score WHERE userName = @userName";
            updateComand.Parameters.AddWithValue("@userName", theNickName);
            updateComand.Parameters.AddWithValue("@score", userscore);
            updateComand.Connection = playerData;


            try
            {
                playerData.Open();
                
                updateComand.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in DBHandler" + ex);
            }
            finally
            {
                playerData.Close();
            }

           

        }

        private void RemoveLetters(letters l)
        {
            gameCanvas.Children.Remove(l.therectPath);
            gameCanvas.Children.Remove(l);
            theLetters.Remove(l);

        }
    }
}
