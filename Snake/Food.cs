using System.Windows;
using System.Windows.Shapes;
using Snake.Interfaces;

namespace Snake
{
  class Food : IFood
  {
    public Point Position { get; set; }

    public UIElement UiElement { get; set; }

    public Food(ISnakeObject snake)
    {
      Position = Randomizer.GetFoodPosition(snake);

      UiElement = new Ellipse()
      {
        Width = Constants.TILESIZE,
        Height = Constants.TILESIZE,
        Fill = Constants.FOODBRUSH
      };
    }
  }
}
