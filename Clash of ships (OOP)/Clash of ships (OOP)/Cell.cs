using System;

namespace Clash_of_ships_OOP
{
    public class Cell
    {
        private int _x;
        private int _y;

        public Cell(int x, int y)
        {
            _x = x;

            _y = y;
        }

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }

        public override bool Equals(object obj)
        {
            Cell cell = (Cell)obj;
            return (_x == cell._x && _y == cell._y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y);
        }
    }
}
