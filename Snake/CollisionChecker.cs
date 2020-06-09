using Snake.Interfaces;

namespace Snake
{
  public static class CollisionChecker
  {
    public static CollisionType Check(ISnakeObject snake, IFood food)
    {
      ISnakePart snakeHead = snake.SnakeParts[0];

      //Checking heads coords against food coords
      if ((snakeHead.Position.X == food.Position.X) && (snakeHead.Position.Y == food.Position.Y))
        return CollisionType.Food;

      //Checking heads coords against game area boundaries
      if ((snakeHead.Position.Y < 0) || (snakeHead.Position.Y >= Constants.GAMEAREAHEIGHT) ||
      (snakeHead.Position.X < 0) || (snakeHead.Position.X >= Constants.GAMEAREAWIDTH))
        return CollisionType.Obstacle;

      //Checking heads coords against all snake parts coords (excluding head)
      foreach (ISnakePart snakeBodyPart in snake.SnakeParts.GetRange(1, snake.SnakeParts.Count - 1))
      {
        if ((snakeHead.Position.X == snakeBodyPart.Position.X) && (snakeHead.Position.Y == snakeBodyPart.Position.Y))
          return CollisionType.Obstacle;
      }

      //If none of above is true there is no collision
      return CollisionType.None;
    }
  }
}
