using System;

namespace Game_Helper
{
    public static class GameHelper
    {
        private static readonly string[] _lettersContainer = { "а", "б", "в", "г", "д", "е", "ж", "з", "и", "к" };
        private static readonly string[] _indexesContainer = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
        private static int _shipsnumber = 0;

        public static int ShipsNumber { get => _shipsnumber; set => _shipsnumber = value; }

        public static void OutputLeftUp(int[,] field) // вывод в левой верхней части
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(2 * i + 3, 0);
                Console.Write(GameHelper._indexesContainer[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0, i + 1);
                Console.Write(GameHelper._lettersContainer[i]);
                Console.SetCursorPosition(2, i + 1);
                Console.Write("| ");
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(2 * j + 3, i + 1);
                    Convert(field[i, j]);
                }
            }
        }
        public static void OutputLeftDown(int[,] field) // вывод в левой нижней части
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(2 * i + 3, 13);
                Console.Write(GameHelper._indexesContainer[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0, i + 14);
                Console.Write(GameHelper._lettersContainer[i]);
                Console.SetCursorPosition(2, i + 14);
                Console.Write("| ");
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(2 * j + 3, i + 14);
                    Convert(field[i, j]);

                }
            }
        }
        public static void OutputRightUp(int[,] field) // вывод в правой верхней части
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(2 * i + 3 + 40, 0);
                Console.Write(GameHelper._indexesContainer[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0 + 40, i + 1);
                Console.Write(GameHelper._lettersContainer[i]);
                Console.SetCursorPosition(2 + 40, i + 1);
                Console.Write("| ");
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(2 * j + 3 + 40, i + 1);
                    Convert(field[i, j]);
                }
            }
        }
        public static void OutputRightDown(int[,] field) // вывод в правой нижней части
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(2 * i + 3 + 40, 13);
                Console.Write(GameHelper._indexesContainer[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0 + 40, i + 14);
                Console.Write(GameHelper._lettersContainer[i]);
                Console.SetCursorPosition(2 + 40, i + 14);
                Console.Write("| ");
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(2 * j + 3 + 40, i + 14);
                    Convert(field[i, j]);

                }
            }
        }

        private static void Convert(int symbol)
        {
            switch (symbol)
            {
                case 0:
                    Console.Write('+');
                    break;
                case 1:
                    Console.Write('\u25A0');
                    break;
                case 2:
                    Console.Write('X');
                    break;
                case 3:
                    Console.Write('O');
                    break;
            }
        }
        
    }
}
