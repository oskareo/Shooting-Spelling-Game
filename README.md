# Shooting-Spelling-Game
This was an educational game that I developed on my second year of University for the Application Development module
It consists of a character/shooter who is presented with a word that has to be spelled correctly. Different letters will appear from random places of the screen at a random order. 
The aim is for the character to shoot the letters in the correct order, hence spelling the word correctly
#issue
The path to the databases has to be changed according to the machine that the solution/project is on.
this paths are in the following classes.
  1. UserName.xaml.cs  line 128  //saves user names and scors into the database(.mdb) 
  2. playGameScreen.xaml.cs   line 763   //get the 50 words that have to be spelled from the database
  3. letters.cs   line 52    //gets the picture of each letter from the current word(word that has to be spelled)