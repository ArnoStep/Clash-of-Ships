namespace Clash_of_ships_OOP
{
    public class Ship
    {
        private int _size;
        private Direction _direction;
        public Ship(int size)
        {
            _size = size;
        }

        public int Size { get => _size; set => _size = value; }
        public Direction Direction { get => _direction; private set => _direction = value; }
    }
}
