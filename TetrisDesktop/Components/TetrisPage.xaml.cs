using Microsoft.Maui.Controls;
using System;

namespace TetrisDesktop
{
    public partial class TetrisPage : ContentPage
    {
        private const int GridRows = 20;
        private const int GridColumns = 10;
        private TetrisGame game;
        private int score;

        public TetrisPage()
        {
            InitializeComponent();
            game = new TetrisGame(GridRows, GridColumns);
            CreateGrid(GridRows, GridColumns);
            InitializeGrid(GridRows, GridColumns);
            StartGameLoop();

            // Subscribe to the KeyPressed event of the CustomEntry
            customEntry.IsVisible = true;  // Make it visible
            customEntry.Focus();  // Give it focus
            customEntry.KeyPressed += OnKeyPressed;
        }

        private void CreateGrid(int rows, int columns)
        {
            for (int row = 0; row < rows; row++)
            {
                TetrisGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            }

            for (int col = 0; col < columns; col++)
            {
                TetrisGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
            }
        }

        private void InitializeGrid(int rows, int columns)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    var boxView = new BoxView
                    {
                        Color = Colors.Gray,
                        WidthRequest = 30,
                        HeightRequest = 30
                    };
                    Grid.SetRow(boxView, row);
                    Grid.SetColumn(boxView, col);
                    TetrisGrid.Children.Add(boxView);
                }
            }

            Console.WriteLine($"Grid initialized with {TetrisGrid.Children.Count} children.");
        }

        private void StartGameLoop()
        {
            Dispatcher.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                Console.WriteLine("Game loop tick");
                game.MovePiece(Direction.Down);
                UpdateUI();
                return true;
            });
        }

        private void UpdateUI()
        {
            for (int row = 0; row < GridRows; row++)
            {
                for (int col = 0; col < GridColumns; col++)
                {
                    var boxView = FindBoxView(row, col);
                    if (boxView != null)
                    {
                        boxView.Color = game.Grid[row, col] == 0 ? Colors.Gray : Colors.Blue;
                    }
                }
            }

            ScoreLabel.Text = $"Score: {game.Score}";
        }

        private BoxView FindBoxView(int row, int col)
        {
            foreach (var child in TetrisGrid.Children)
            {
                if (child is BindableObject bindableChild && Grid.GetRow(bindableChild) == row && Grid.GetColumn(bindableChild) == col)
                {
                    return child as BoxView;
                }
            }
            return null;
        }

        private bool OnKeyPressed(KeyEventArgs e)
        {
            bool handled = true;

            switch (e.Key)
            {
                case Keys.LeftArrow:
                    game.MovePiece(Direction.Left);
                    break;
                case Keys.RightArrow:
                    game.MovePiece(Direction.Right);
                    break;
                case Keys.DownArrow:
                    game.MovePiece(Direction.Down);
                    break;
                case Keys.UpArrow:
                    game.MovePiece(Direction.Up);
                    break;
                default:
                    handled = false;
                    break;
            }
            UpdateUI();

            return handled;
        }
    }
}