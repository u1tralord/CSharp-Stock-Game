using System;

namespace Stock_Game.ui
{
    public interface Screen
    {
        void Draw();
        bool KeyPress(ConsoleKeyInfo key);
    }
}