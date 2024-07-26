namespace TetrisDesktop
{
    public enum Direction
    {
        Left,
        Right,
        Down,
        Up
    }

    public class TetrisGame
    {
        private int Rows;
        private int Columns;
        public int[,] Grid { get; private set; }
        private Tetromino currentTetromino;
        private int currentRow;
        private int currentCol;
        public int Score { get; private set; }

        public TetrisGame(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Grid = new int[Rows, Columns];
            SpawnNewTetromino();
        }

        private void SpawnNewTetromino()
        {
            currentTetromino = new Tetromino();
            currentRow = 0;
            currentCol = Columns / 2 - currentTetromino.Size / 2;

            if (!IsValidPosition(currentRow, currentCol))
            {
                // Game over logic
            }
            PlaceTetromino();
        }

        public void MovePiece(Direction direction)
        {
            ClearTetromino();

            switch (direction)
            {
                case Direction.Left:
                    if (IsValidPosition(currentRow, currentCol - 1)) currentCol--;
                    break;
                case Direction.Right:
                    if (IsValidPosition(currentRow, currentCol + 1)) currentCol++;
                    break;
                case Direction.Down:
                    if (IsValidPosition(currentRow + 1, currentCol))
                    {
                        currentRow++;
                    }
                    else
                    {
                        PlaceTetromino();
                        CheckForCompletedLines();
                        SpawnNewTetromino();
                        return;
                    }
                    break;
                case Direction.Up:
                    RotateTetromino();
                    break;
            }

            PlaceTetromino();
        }

        private void ClearTetromino()
        {
            for (int row = 0; row < currentTetromino.Size; row++)
            {
                for (int col = 0; col < currentTetromino.Size; col++)
                {
                    if (currentTetromino.Shape[row, col] != 0)
                    {
                        Grid[currentRow + row, currentCol + col] = 0;
                    }
                }
            }
        }

        private void PlaceTetromino()
        {
            for (int row = 0; row < currentTetromino.Size; row++)
            {
                for (int col = 0; col < currentTetromino.Size; col++)
                {
                    if (currentTetromino.Shape[row, col] != 0)
                    {
                        Grid[currentRow + row, currentCol + col] = currentTetromino.Shape[row, col];
                    }
                }
            }
        }

        private void RotateTetromino()
        {
            ClearTetromino();
            currentTetromino.Rotate();
            if (!IsValidPosition(currentRow, currentCol))
            {
                currentTetromino.Rotate();
                currentTetromino.Rotate();
                currentTetromino.Rotate();
            }
            PlaceTetromino();
        }

        private bool IsValidPosition(int row, int col)
        {
            for (int r = 0; r < currentTetromino.Size; r++)
            {
                for (int c = 0; c < currentTetromino.Size; c++)
                {
                    if (currentTetromino.Shape[r, c] != 0)
                    {
                        int newRow = row + r;
                        int newCol = col + c;

                        if (newRow < 0 || newRow >= Rows || newCol < 0 || newCol >= Columns || Grid[newRow, newCol] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void CheckForCompletedLines()
        {
            for (int row = 0; row < Rows; row++)
            {
                bool isLineComplete = true;
                for (int col = 0; col < Columns; col++)
                {
                    if (Grid[row, col] == 0)
                    {
                        isLineComplete = false;
                        break;
                    }
                }

                if (isLineComplete)
                {
                    ClearLine(row);
                    Score++;
                }
            }
        }

        private void ClearLine(int row)
        {
            for (int r = row; r > 0; r--)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Grid[r, col] = Grid[r - 1, col];
                }
            }

            for (int col = 0; col < Columns; col++)
            {
                Grid[0, col] = 0;
            }
        }
    }

    public class Tetromino
    {
        public int[,] Shape { get; private set; }
        public int Size { get; private set; }

        private static readonly int[][,] Shapes = new int[][,]
        {
            new int[,] { { 1, 1, 1, 1 } }, // I shape
            new int[,] { { 1, 1 }, { 1, 1 } }, // O shape
            new int[,] { { 0, 1, 0 }, { 1, 1, 1 } }, // T shape
            new int[,] { { 1, 0, 0 }, { 1, 1, 1 } }, // L shape
            new int[,] { { 0, 0, 1 }, { 1, 1, 1 } }, // J shape
            new int[,] { { 0, 1, 1 }, { 1, 1, 0 } }, // S shape
            new int[,] { { 1, 1, 0 }, { 0, 1, 1 } } // Z shape
        };

        public Tetromino()
        {
            Shape = Shapes[new Random().Next(Shapes.Length)];
            Size = Shape.GetLength(0);
        }

        public void Rotate()
        {
            int newSize = Shape.GetLength(1);
            int[,] newShape = new int[newSize, Size];

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < newSize; col++)
                {
                    newShape[col, Size - 1 - row] = Shape[row, col];
                }
            }

            Shape = newShape;
            Size = newSize;
        }
    }
}