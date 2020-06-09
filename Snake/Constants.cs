using System.Windows.Media;

namespace Snake
{
  //Container for all the constants used across project
  class Constants
  {
    //Game parameters
    public const int MAXSNAKESPEED = 100;
    public const int INITIALSNAKESPEED = 300;

    //Sizes
    public const int SNAKESQUARESIZE = 20;
    public const int TILESIZE = 20;
    public const int GAMEAREAWIDTH = 400;
    public const int GAMEAREAHEIGHT = 400;

    //Brushes
    public readonly static SolidColorBrush SNAKEBODYBRUSH = Brushes.Green;
    public readonly static SolidColorBrush SNAKEHEADBRUSH = Brushes.Yellow;
    public readonly static SolidColorBrush FOODBRUSH = Brushes.Red;

    //Links
    public readonly static string[] SNAKEIMAGELINKS = new string[]
    {
      @"https://i.imgur.com/o6MQr3Z.jpg",
      @"https://i.imgur.com/ULqvJix.jpg",
      @"https://i.imgur.com/jsH4krJ.jpg",
      @"https://i.imgur.com/SLg2JLs.jpg",
      @"https://i.imgur.com/MBiAD0K.jpg"
    };
  }
}

