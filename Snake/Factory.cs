using Snake.Interfaces;

namespace Snake
{
  //Dependency inversion container
  public static class Factory
  {
    public static ISnakeObject CreateSnake()
    {
      return new SnakeObject();
    }

    public static IFood CreateFood(ISnakeObject snake)
    {
      return new Food(snake);
    }

    public static ISnakePart CreateSnakePart(bool isHead = false)
    {
      return new SnakePart(isHead);
    }
  }
}
