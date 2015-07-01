using System;

namespace Stock_Game
{
    interface Screen
    {
        void Draw();
        void KeyPress(ConsoleKeyInfo key);
    }
}