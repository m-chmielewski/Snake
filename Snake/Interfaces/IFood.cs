using System.Windows;

namespace Snake.Interfaces
{
  public interface IFood
  {
    Point Position { get; set; }
    UIElement UiElement { get; set; }
  }
}
