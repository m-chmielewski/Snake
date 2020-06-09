using Snake.Interfaces;
using System.Windows;
using System.Windows.Shapes;

namespace Snake
{
  public class SnakePart : ISnakePart
  {
    public UIElement UiElement { get; set; }

    public Point Position { get; set; }

    public SnakePart(bool isHead = false)
    {
      UiElement = new Rectangle()
      {
        Width = Constants.SNAKESQUARESIZE,
        Height = Constants.SNAKESQUARESIZE,
        Fill = (isHead ? Constants.SNAKEHEADBRUSH : Constants.SNAKEBODYBRUSH)
      };
    }

    public void SwitcHeadToBody()
    {
      Rectangle oldHead = UiElement as Rectangle;
      oldHead.Fill = Constants.SNAKEBODYBRUSH;
    }
  }
}
