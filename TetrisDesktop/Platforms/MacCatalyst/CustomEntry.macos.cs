#if __MACCATALYST__

using System;

namespace TetrisDesktop
{
    public partial class CustomEntry : Entry
    {
        public event EventHandler<Direction> ArrowKeyPressed;

        public void OnArrowKeyPressed(Direction direction)
        {
            ArrowKeyPressed?.Invoke(this, direction);
        }
    }
}

#endif