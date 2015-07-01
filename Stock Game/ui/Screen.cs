using System;

namespace Stock_Game.ui
{
    interface Screen
    {
        void Draw();
        void KeyPress(ConsoleKeyInfo key);
    }
}