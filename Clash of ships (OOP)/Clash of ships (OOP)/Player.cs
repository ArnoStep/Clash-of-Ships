using System;
using System.Collections.Generic;

namespace Clash_of_ships_OOP
{
    public class Player
    {
        private FieldWorker _ownField;
        protected FieldWorker _opponentField;
        protected List<Cell> _allcellsContainer;
        protected int _letter;
        protected int _index;
        protected int _points;

        public Player()
        {
            OwnField = new FieldWorker();
            _opponentField = new FieldWorker();
            _allcellsContainer = new List<Cell>();

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    _allcellsContainer.Add(new Cell(i, j));
                }
            }

            OwnField.CreateField();

            _points = 0;

            _opponentField.Flag = true;
        }

        public FieldWorker OwnField { get => _ownField; private set => _ownField = value; }

        public bool CheckVictory(string str)
        {
            if (_points == 20)
            {
                Console.Write(str);
                return true;
            }
            return false;
        }

        public void Shoot(int[,] field)
        {
            if (CheckVictory(""))
            {
                return;
            }
            RandomizeCoordinate();
            if (_opponentField.CheckHitSuccess(_letter, _index, field))
            {
                _points++;
                Shoot(field);
            }
        }

        protected void RandomizeCoordinate()
        {
            Cell randomCoordinate = _allcellsContainer[new Random().Next(0, _allcellsContainer.Count)];

            _index = randomCoordinate.Y;
            _letter = randomCoordinate.X;
            _ = _allcellsContainer.Remove(randomCoordinate);
        }
        
    }
}
