using Snake.Interfaces;
using System.Collections.Generic;
using System.Windows;

namespace Snake
{
  //Clas representing the snake
  class SnakeObject : ISnakeObject
  {
    //List of all the tiles that the snake consists of
    public List<ISnakePart> SnakeParts { get; set; } = new List<ISnakePart>();

    //The direction in which the snake is currently moving, initially always set to right
    public SnakeDirection SnakeDirection { get; set; } = SnakeDirection.Right;

    //Constructor for new snake used at the beginning of each game
    public SnakeObject()
    {
      //Snake initially always consists of 3 tiles, the tile at position 0 is snakes head
      //Snake is initially positioned in the center of game area
      SnakeParts.Add(new SnakePart(isHead: true)
      {
        Position = new Point(Constants.SNAKESQUARESIZE * 11, Constants.SNAKESQUARESIZE * 10),
      });

      SnakeParts.Add(new SnakePart()
      {
        Position = new Point(Constants.SNAKESQUARESIZE * 10, Constants.SNAKESQUARESIZE * 10)
      });

      SnakeParts.Add(new SnakePart()
      {
        Position = new Point(Constants.SNAKESQUARESIZE * 9, Constants.SNAKESQUARESIZE * 10)
      });
    }

    //Updating snake parts positions after each time interval to simulate movement
    public void UpdatePartsPositions()
    {
      double newHeadX = SnakeParts[0].Position.X;
      double newHeadY = SnakeParts[0].Position.Y;

      //Checking movement direction to determine where to position head
      switch (SnakeDirection)
      {
        case SnakeDirection.Left:
          newHeadX -= Constants.SNAKESQUARESIZE;
          break;
        case SnakeDirection.Right:
          newHeadX += Constants.SNAKESQUARESIZE;
          break;
        case SnakeDirection.Up:
          newHeadY -= Constants.SNAKESQUARESIZE;
          break;
        case SnakeDirection.Down:
          newHeadY += Constants.SNAKESQUARESIZE;
          break;
      }

      ISnakePart newHead = new SnakePart(isHead: true)
      {
        Position = new Point()
        {
          X = newHeadX,
          Y = newHeadY
        }
      };

      //Changing old head tile to become body part
      SnakeParts[0].SwitcHeadToBody();
      //Inserting new head at the beginning of the snake
      SnakeParts.Insert(0, newHead);
      //Removing snakes tail
      SnakeParts.RemoveAt(SnakeParts.Count - 1);
    }

    public void Grow()
    {
      double newTailX = SnakeParts[SnakeParts.Count - 1].Position.X;
      double newTailY = SnakeParts[SnakeParts.Count - 1].Position.Y;

      switch (SnakeDirection)
      {
        case SnakeDirection.Left:
          newTailX -= Constants.SNAKESQUARESIZE;
          break;
        case SnakeDirection.Right:
          newTailX += Constants.SNAKESQUARESIZE;
          break;
        case SnakeDirection.Up:
          newTailY -= Constants.SNAKESQUARESIZE;
          break;
        case SnakeDirection.Down:
          newTailY += Constants.SNAKESQUARESIZE;
          break;
      }

      //Adding new tail to the snake to simulate its growth
      ISnakePart newTail = Factory.CreateSnakePart();
      newTail.Position = new Point(newTailX, newTailY);
      SnakeParts.Add(newTail);
    }
  }
}
