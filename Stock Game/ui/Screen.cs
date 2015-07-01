using System;

namespace Stock_Game.ui
{
    public interface Screen
    {
        void Draw();
        void KeyPress(ConsoleKeyInfo key);
    }
}