using System.Windows;

namespace Snake.Interfaces
{
  public interface ISnakePart
  {
    UIElement UiElement { get; set; }
    Point Position { get; set; }

    void SwitcHeadToBody();
  }
}
