using System.Collections.Generic;

namespace Snake.Interfaces
{
  public interface ISnakeObject
  {
    List<ISnakePart> SnakeParts { get; set; }
    SnakeDirection SnakeDirection { get; set; }

    void UpdatePartsPositions();

    void Grow();
  }
}
