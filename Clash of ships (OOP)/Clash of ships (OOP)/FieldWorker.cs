using System;
using Game_Helper;

namespace Clash_of_ships_OOP
{
    public class FieldWorker
    {       
        private int[,] _box;
        private bool _flag;

        public FieldWorker()
        {
            _box = new int[10, 10];
        }

        public int[,] Box { get => _box; private set => _box = value; }
        public bool Flag { get => _flag; set => _flag = value; }

        public void CreateField()
        {
            RandomShipFour();
            GameHelper.ShipsNumber = 0;
            while (GameHelper.ShipsNumber < 2)
            {
                RandomShipThree();
            }
            GameHelper.ShipsNumber = 0;
            while (GameHelper.ShipsNumber < 3)
            {
                RandomShipTwo();
            }
            GameHelper.ShipsNumber = 0;
            while (GameHelper.ShipsNumber < 4)
            {
                RandomShipOne();
            }
        }

        public bool CheckHitSuccess(int letter, int index, int[,] field)
        {

            if (field[letter, index] == 0)
            {
                field[letter, index] = 3;
                _box[letter, index] = 3;
                return false;
            }
            if (field[letter, index] == 1)
            {
                field[letter, index] = 2;
                _box[letter, index] = 2;
                Flag = IsKilled(field, letter, index);
                return true;
            }
            return false;
        }

        public bool IsKilled(int[,] field, int letter, int index)
        {
            bool flag = true;
            Ship ship = new Ship(1);
            int upperBound = index;
            int leftBound = letter;
            FindShipLocation(field, letter, index, ref leftBound, 1, ref ship, -1, 0, ref flag); // проверяем есть ли еще палубы слева от клетки 

            if (flag == false) return false;

            FindShipLocation(field, letter, index, ref leftBound, 0, ref ship, 1, 0, ref flag); // справа

            if (flag == false) return false;

            if (ship.Size > 1)
            {
                PutMisses(field, leftBound, upperBound, ship.Size + 1, 2);
                return true;
            }

            FindShipLocation(field, letter, index, ref upperBound, 1, ref ship, 0, -1, ref flag); // сверху

            if (flag == false) return false;

            FindShipLocation(field, letter, index, ref upperBound, 0, ref ship, 0, 1, ref flag); // снизу

            if (flag == false) return false;

            if (ship.Size >= 1)
            {
                PutMisses(field, leftBound, upperBound, 2, ship.Size + 1);
                return true;
            }

            return true;
        }

        private void FindShipLocation(int[,] field, int letter, int index, ref int bound, int step, 
            ref Ship ship, int letterCoef, int indexCoef, ref bool flag)
        {
            for (int k = 1; k < 4; k++)
            {
                try
                {
                    if (field[letter + (letterCoef * k), index + (indexCoef * k)] == 2)
                    {
                        ship.Size++;
                        bound -= step;
                    }
                    if (field[letter + (letterCoef * k), index + (indexCoef * k)] == 1) //попалась нетронутая палуба => еще не убит
                    {
                        flag = false;
                        break;
                    }
                    if (field[letter + (letterCoef * k), index + (indexCoef * k)] == 0 || field[letter + (letterCoef * k), index + (indexCoef * k)] == 3)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
        }
        private void PutMisses(int[,] field, int leftBound, int upperBound, int missWidth, int missLength)
        {
            for (int l = leftBound - 1; l < leftBound + missWidth && l < 10; l++)
            {
                for (int k = upperBound - 1; k < upperBound + missLength && k < 10; k++)
                {
                    if (k < 0)
                    {
                        k++;
                    }
                    if (l < 0)
                    {
                        l++;
                    }
                    if (field[l, k] != 2)
                    {
                        field[l, k] = 3;
                        _box[l, k] = 3;
                    }
                }
            }
        }

        private void RandomShipFour()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int index = random.Next(10);
            int letter = random.Next(10);
            if (index > 5)
            {
                letter = random.Next(5);
                for (int i = letter; i < letter + 4; i++)
                {
                    _box[i, index] = 1;
                }
                return;
            }
            if (letter > 5)
            {
                index = random.Next(5);
                for (int j = index; j < index + 4; j++)
                {
                    _box[letter, j] = 1;
                }
                return;
            }
            Direction shipDirection = (Direction)random.Next(1);
            if (shipDirection == Direction.horizontal)
            {
                for (int i = letter; i < letter + 4; i++)
                {
                    _box[i, index] = 1;
                }
            }
            else
            {
                for (int j = index; j < index + 4; j++)
                {
                    _box[letter, j] = 1;
                }
            }
        }
        private void RandomShipThree()
        {
            bool flag = true;
            var random = new Random(DateTime.Now.Millisecond);
            var index = random.Next(10);
            var letter = random.Next(10);
            if (letter > 6)
            {
                index = random.Next(7);
                RunShipCells(letter, index, 2, 4, ref flag);
                if (flag == false) { return; }
                for (int j = index; j < index + 3; j++)
                {
                    _box[letter, j] = 1;
                }
                GameHelper.ShipsNumber++;
                return;
            }
            if (index > 6)
            {
                letter = random.Next(7);
                RunShipCells(letter, index, 4, 2, ref flag);
                if (flag == false) { return; }
                for (int i = letter; i < letter + 3; i++)
                {
                    _box[i, index] = 1;
                }
                GameHelper.ShipsNumber++;
                return;
            }
            Direction shipDirection = (Direction)random.Next(1);
            if (shipDirection == Direction.horizontal)
            {
                RunShipCells(letter, index, 4, 2, ref flag);
                if (flag == false) { return; }
                for (int i = letter; i < letter + 3; i++)
                {
                    _box[i, index] = 1;
                }
                GameHelper.ShipsNumber++;
            }
            else
            {
                RunShipCells(letter, index, 2, 4, ref flag);
                if (flag == false) { return; }
                for (int j = index; j < index + 3; j++)
                {
                    _box[letter, j] = 1;
                }
                GameHelper.ShipsNumber++;
            }
        }
        private void RandomShipTwo()
        {
            bool flag = true;
            var random = new Random(DateTime.Now.Millisecond);
            var index = random.Next(10);
            var letter = random.Next(10);
            if (letter > 7)
            {
                index = random.Next(8);
                RunShipCells(letter, index, 2, 3, ref flag);
                if (flag == false) { return; }
                for (int j = index; j < index + 2; j++)
                {
                    _box[letter, j] = 1;
                }
                GameHelper.ShipsNumber++;
                return;
            }
            if (index > 7)
            {
                letter = random.Next(8);
                RunShipCells(letter, index, 3, 2, ref flag);
                if (flag == false) { return; }
                for (int i = letter; i < letter + 2; i++)
                {
                    _box[i, index] = 1;
                }
                GameHelper.ShipsNumber++;
                return;
            }
            Direction shipDirection = (Direction)random.Next(1);
            if (shipDirection == Direction.horizontal)
            {
                RunShipCells(letter, index, 3, 2, ref flag);
                if (flag == false) { return; }
                for (int i = letter; i < letter + 2; i++)
                {
                    _box[i, index] = 1;
                }
                GameHelper.ShipsNumber++;
            }
            else
            {
                RunShipCells(letter, index, 2, 3, ref flag);
                if (flag == false) { return; }
                for (int j = index; j < index + 2; j++)
                {
                    _box[letter, j] = 1;
                }
                GameHelper.ShipsNumber++;
            }
        }
        private void RandomShipOne()
        {
            bool flag = true;
            var random = new Random(DateTime.Now.Millisecond);
            var index = random.Next(10);
            var letter = random.Next(10);
            RunShipCells(letter, index, 2, 2, ref flag);
            if (flag == false) { return; }
            _box[letter, index] = 1;
            GameHelper.ShipsNumber++;
        }

        private void RunShipCells(int letter, int index, int letterDelta, int indexDelta, ref bool flag)
        {
            for (int i = letter - 1; i < letter + letterDelta; i++)
            {
                if (i < 0)
                {
                    i++;
                }
                if (i > 9)
                {
                    break;
                }
                for (int j = index - 1; j < index + indexDelta; j++)
                {
                    if (j < 0)
                    {
                        j++;
                    }
                    if (j > 9)
                    {
                        break;
                    }
                    if (_box[i, j] != 0)
                    {
                        flag = false;
                        return;
                    }
                }
            }
        }
    }
}
