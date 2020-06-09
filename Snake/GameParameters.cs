using Snake.Interfaces;
using System;

namespace Snake
{
  public static class GameParameters
  {
    public static int SnakeSpeed
    {
      get
      {
        return Math.Max(Constants.MAXSNAKESPEED, Constants.INITIALSNAKESPEED - 2 * Score);
      }
    }

    public static int Score { get; set; }
  }
}
