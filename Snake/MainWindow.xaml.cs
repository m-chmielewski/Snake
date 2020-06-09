using Snake.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
  public partial class MainWindow : Window
  {
    private readonly DispatcherTimer gameTimer = new DispatcherTimer();
    private IFood food;
    private ISnakeObject snake;
    private string currentImageURL;

    public MainWindow()
    {
      InitializeComponent();
      gameTimer.Tick += GameTimer_Tick;
    }

    //Handler informing when chessboard can be drawn on the screen (as soon as all the rest of UI is alredy rendered)
    private void Window_ContentRendered(object sender, EventArgs e)
    {
      DrawChessboard();
    }

    private void DrawChessboard()
    {
      //Initial values
      bool doneDrawingChessboard = false;
      int nextTileX = 0, nextTileY = 0;
      int rowCounter = 0;
      bool nextIsOdd = false;

      while (doneDrawingChessboard == false)
      {
        Rectangle rect = new Rectangle
        {
          Width = Constants.TILESIZE,
          Height = Constants.TILESIZE,
          Fill = nextIsOdd ? Brushes.White : Brushes.LightGray
        };

        //Adding tile to UI
        GameArea.Children.Add(rect);
        Canvas.SetTop(rect, nextTileX);
        Canvas.SetLeft(rect, nextTileY);

        nextIsOdd = !nextIsOdd;
        nextTileX += Constants.TILESIZE;

        //Checking whether right edge of game area hea been reached, if so tiles are being added to next row
        if (nextTileX >= GameArea.ActualWidth)
        {
          nextTileX = 0;
          nextTileY += Constants.TILESIZE;
          rowCounter++;
          nextIsOdd = (rowCounter % 2 != 0);
        }

        //when tiles reach bottom of game area while loop is terminated
        if (nextTileY >= GameArea.ActualHeight)
          doneDrawingChessboard = true;
      }
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
      //UI preparation
      StartButton.IsEnabled = false;
      Score.Text = "Score: 0";
      SnakeImage.Source = null;
      if (food != null && snake != null)
      {
        GameArea.Children.Remove(food.UiElement);
        RemoveSnakeFromScreen();
      }

      //Setting back to defaults
      snake = Factory.CreateSnake();
      food = Factory.CreateFood(snake);
      GameParameters.Score = 0;

      //Laying out new snake and food
      DrawSnake();
      DrawFood();

      //Setting sped back to initial and starting the game
      gameTimer.Interval = TimeSpan.FromMilliseconds(Constants.INITIALSNAKESPEED);
      gameTimer.IsEnabled = true;
    }

    private void RemoveSnakeFromScreen()
    {
      foreach (ISnakePart snakePart in snake.SnakeParts)
      {
        GameArea.Children.Remove(snakePart.UiElement);
      }
    }

    private void DrawSnake()
    {
      foreach (ISnakePart snakePart in snake.SnakeParts)
      {
        GameArea.Children.Add(snakePart.UiElement);
        Canvas.SetTop(snakePart.UiElement, snakePart.Position.Y);
        Canvas.SetLeft(snakePart.UiElement, snakePart.Position.X);
      }
    }

    private void DrawFood()
    {
      food = Factory.CreateFood(snake);
      GameArea.Children.Add(food.UiElement);
      Canvas.SetTop(food.UiElement, food.Position.Y);
      Canvas.SetLeft(food.UiElement, food.Position.X);
    }

    //Updating game state after each time interval
    private void GameTimer_Tick(object sender, EventArgs e)
    {
      RemoveSnakeFromScreen();
      snake.UpdatePartsPositions();
      DrawSnake();

      switch (CollisionChecker.Check(snake, food))
      {
        case CollisionType.None:
          break;
        case CollisionType.Food:
          GameParameters.Score++;
          gameTimer.Interval = TimeSpan.FromMilliseconds(GameParameters.SnakeSpeed);
          GameArea.Children.Remove(food.UiElement);
          Score.Text = $"Score: {GameParameters.Score}";
          ShowSnakeImage();
          snake.Grow();
          DrawFood();
          break;
        case CollisionType.Obstacle:
          EndGame();
          break;
      }
    }

    private void ShowSnakeImage()
    {
      //Getting random ImageURL
      string imageURL = Randomizer.GetSnakeImageURL(currentImageURL);

      currentImageURL = imageURL;

      BitmapImage bitmap = new BitmapImage();
      bitmap.BeginInit();
      bitmap.UriSource = new Uri(imageURL, UriKind.Absolute);
      bitmap.EndInit();

      //Laying out snake image in UI
      SnakeImage.Source = bitmap;
    }

    private void EndGame()
    {
      StartButton.IsEnabled = true;
      gameTimer.IsEnabled = false;
      MessageBox.Show($"You failed :(\nYour score: { GameParameters.Score}");
    }

    //Snake control
    private void Window_KeyUp(object sender, KeyEventArgs e)
    {
      //Checking if user chose valid direction
      switch (e.Key)
      {
        case Key.Up:
          if (snake.SnakeDirection != SnakeDirection.Down)
            snake.SnakeDirection = SnakeDirection.Up;
          break;
        case Key.Down:
          if (snake.SnakeDirection != SnakeDirection.Up)
            snake.SnakeDirection = SnakeDirection.Down;
          break;
        case Key.Left:
          if (snake.SnakeDirection != SnakeDirection.Right)
            snake.SnakeDirection = SnakeDirection.Left;
          break;
        case Key.Right:
          if (snake.SnakeDirection != SnakeDirection.Left)
            snake.SnakeDirection = SnakeDirection.Right;
          break;
      }
    }
  }
}
