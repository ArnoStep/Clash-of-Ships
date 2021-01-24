using System;
using System.Collections.Generic;
using System.Linq;

namespace Clash_of_ships_OOP
{
    class SmartPlayer : Player
    {
        
        private readonly List<Cell> _near12CellsContainer;
        private readonly List<Cell> _near4CellsContainer;
        private readonly List<Cell> _directionalContainer;
        private List<Cell> _hurtedShipCellsContainer;

        private int _shotCounter;

        public SmartPlayer()
        {
            _near12CellsContainer = new List<Cell>();
            _near4CellsContainer = new List<Cell>();
            _hurtedShipCellsContainer = new List<Cell>();
            _directionalContainer = new List<Cell>();
            _shotCounter = 0;
        }

        public new void Shoot(int[,] field)
        {
            if (CheckVictory(""))
            {
                return;
            }
            if (_opponentField.Flag == true)
            {
                _near12CellsContainer.Clear();
                _near4CellsContainer.Clear();
                _hurtedShipCellsContainer.Clear();
                _directionalContainer.Clear();
                _shotCounter = 0;
                RandomizeCoordinate();
            }
            else
            {
                _shotCounter++;
                RandomizeSmartCoordinate();
            }

            if (_opponentField.CheckHitSuccess(_letter, _index, field))
            {
                _hurtedShipCellsContainer.Add(new Cell(_letter, _index));
                _points++;
                Shoot(field);
            }
        }

        private void RandomizeSmartCoordinate()
        {
            Cell randomCoordinate = new Cell(0, 0);
            if (_shotCounter == 1)
            {
                Fill_near12cellsContainer();

                Fill_near4cellsContainer();
            }

            if (_hurtedShipCellsContainer.Count == 1)
            {
                randomCoordinate = _near4CellsContainer[new Random().Next(0, _near4CellsContainer.Count)];
                _near4CellsContainer.Remove(randomCoordinate);
            }

            if (_hurtedShipCellsContainer.Count >= 2)
            {
                _hurtedShipCellsContainer = _hurtedShipCellsContainer.OrderBy(cell => cell.X).ThenBy(cell => cell.Y).ToList();

                if (_hurtedShipCellsContainer[0].X == _hurtedShipCellsContainer[^1].X)
                {
                    Fill_directionalXContainer();
                }

                if (_hurtedShipCellsContainer[0].Y == _hurtedShipCellsContainer[^1].Y)
                {
                    Fill_directionalYContainer();
                }

                randomCoordinate = _directionalContainer[new Random().Next(0, _directionalContainer.Count)];
                _directionalContainer.Remove(randomCoordinate);
            }

            _letter = randomCoordinate.X;
            _index = randomCoordinate.Y;
            _ = _allcellsContainer.Remove(randomCoordinate);
            _ = _near12CellsContainer.Remove(randomCoordinate);
        }

        private void Fill_near12cellsContainer()
        {
            for (int i = _letter - 1; i > _letter - 4 && i >= 0; --i)
            {
                if (_allcellsContainer.Contains(new Cell(i, _index)))
                {
                    _near12CellsContainer.Add(new Cell(i, _index));
                }
            }

            for (int i = _letter + 1; i < _letter + 4 && i < 10; ++i)
            {
                if (_allcellsContainer.Contains(new Cell(i, _index)))
                {
                    _near12CellsContainer.Add(new Cell(i, _index));
                }
            }

            for (int i = _index - 1; i > _index - 4 && i >= 0; --i)
            {
                if (_allcellsContainer.Contains(new Cell(_letter, i)))
                {
                    _near12CellsContainer.Add(new Cell(_letter, i));
                }
            }

            for (int i = _index + 1; i < _index + 4 && i < 10; ++i)
            {
                if (_allcellsContainer.Contains(new Cell(_letter, i)))
                {
                    _near12CellsContainer.Add(new Cell(_letter, i));
                }
            }
        }

        private void Fill_near4cellsContainer()
        {
            if (_near12CellsContainer.Contains(new Cell((_letter - 1), _index))) { _near4CellsContainer.Add(new Cell((_letter - 1), _index)); }

            if (_near12CellsContainer.Contains(new Cell((_letter + 1), _index))) { _near4CellsContainer.Add(new Cell((_letter + 1), _index)); }

            if (_near12CellsContainer.Contains(new Cell(_letter, _index - 1))) { _near4CellsContainer.Add(new Cell(_letter, _index - 1)); }

            if (_near12CellsContainer.Contains(new Cell(_letter, _index + 1))) { _near4CellsContainer.Add(new Cell(_letter, _index + 1)); }
        }

        private void Fill_directionalXContainer()
        {
            if (_near12CellsContainer.Contains(new Cell(_hurtedShipCellsContainer[0].X, _hurtedShipCellsContainer[0].Y - 1)))
            {
                _directionalContainer.Add(new Cell(_hurtedShipCellsContainer[0].X, _hurtedShipCellsContainer[0].Y - 1));
            }

            if (_near12CellsContainer.Contains(new Cell(_hurtedShipCellsContainer[0].X, _hurtedShipCellsContainer[0].Y + 1)))
            {
                _directionalContainer.Add(new Cell(_hurtedShipCellsContainer[0].X, _hurtedShipCellsContainer[0].Y + 1));
            }

            if (_near12CellsContainer.Contains(new Cell(_hurtedShipCellsContainer[^1].X,
                _hurtedShipCellsContainer[^1].Y - 1)))
            {
                _directionalContainer.Add(new Cell(_hurtedShipCellsContainer[^1].X,
                _hurtedShipCellsContainer[^1].Y - 1));
            }

            if (_near12CellsContainer.Contains(new Cell(_hurtedShipCellsContainer[^1].X,
                _hurtedShipCellsContainer[^1].Y + 1)))
            {
                _directionalContainer.Add(new Cell(_hurtedShipCellsContainer[^1].X,
                _hurtedShipCellsContainer[^1].Y + 1));
            }
        }

        private void Fill_directionalYContainer()
        {
            if (_near12CellsContainer.Contains(new Cell(_hurtedShipCellsContainer[0].X - 1, _hurtedShipCellsContainer[0].Y)))
            {
                _directionalContainer.Add(new Cell(_hurtedShipCellsContainer[0].X - 1, _hurtedShipCellsContainer[0].Y));
            }

            if (_near12CellsContainer.Contains(new Cell(_hurtedShipCellsContainer[0].X + 1, _hurtedShipCellsContainer[0].Y)))
            {
                _directionalContainer.Add(new Cell(_hurtedShipCellsContainer[0].X + 1, _hurtedShipCellsContainer[0].Y));
            }

            if (_near12CellsContainer.Contains(new Cell(_hurtedShipCellsContainer[^1].X - 1,
                _hurtedShipCellsContainer[^1].Y)))
            {
                _directionalContainer.Add(new Cell(_hurtedShipCellsContainer[^1].X - 1,
                _hurtedShipCellsContainer[^1].Y));
            }

            if (_near12CellsContainer.Contains(new Cell(_hurtedShipCellsContainer[^1].X + 1,
                _hurtedShipCellsContainer[^1].Y)))
            {
                _directionalContainer.Add(new Cell(_hurtedShipCellsContainer[^1].X + 1,
                _hurtedShipCellsContainer[^1].Y));
            }
        }
    }
}
