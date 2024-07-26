using Microsoft.Maui.Controls;

namespace TetrisDesktop
{
    public class CustomGameView : ContentView
    {
        public event EventHandler<Direction> ArrowKeyPressed;

        public void OnArrowKeyPressed(Direction direction)
        {
            ArrowKeyPressed?.Invoke(this, direction);
        }
    }
}