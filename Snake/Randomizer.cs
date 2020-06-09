using Snake.Interfaces;
using System;
using System.Linq;
using System.Windows;

namespace Snake
{
  class Randomizer
  {
    //Supplies game with random values
    private static readonly Random random = new Random();

    public static Point GetFoodPosition(ISnakeObject snake)
    {
      //Getting maximum coordinates within game area and normalizing them against tile size
      int maxX = (int)(Constants.GAMEAREAWIDTH / Constants.TILESIZE);
      int maxY = (int)(Constants.GAMEAREAHEIGHT / Constants.TILESIZE);

      //Getting random tile coordinates for food positioning
      int foodX = random.Next(0, maxX) * Constants.TILESIZE;
      int foodY = random.Next(0, maxY) * Constants.TILESIZE;

      //Checking whether obtained food coordinates do not collide with all the snake parts coordinates, if so generating new coordinates
      foreach (ISnakePart snakePart in snake.SnakeParts)
      {
        if ((snakePart.Position.X == foodX) && (snakePart.Position.Y == foodY))
          return GetFoodPosition(snake);
      }

      return new Point(foodX, foodY);
    }

    public static string GetSnakeImageURL(string currentImageURL)
    {
      //Getting number of snake image to display
      int URLNumber = random.Next(0, Constants.SNAKEIMAGELINKS.Length - 1);

      string newImageURL = Constants.SNAKEIMAGELINKS[URLNumber];

      //Checking whether random image is not the same as currently displayed. if so, using another one
      if (newImageURL == currentImageURL)
        return GetSnakeImageURL(currentImageURL);

      return newImageURL;
    }
  }
}
