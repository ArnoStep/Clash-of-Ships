using System;
using System.Collections.Generic;
using System.IO;

namespace ShipFight
{
    public class ShipFight
    {
        public int[,] Opponent_Field = new int[10, 10]; //поле соперника, заполняется по ходу игры: 
                                                        //0 - пустая клетка, 1 - нетронутый корабль, 2 - попадание по кораблю, 3 - промах

        public int[,] Own_Field = new int[10, 10]; //свое поле

        private List<int> rnd = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
            21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
            40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59,
            60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79,
            80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; //список для выбора поля при стрельбе
        private List<int> rnd12 = new List<int>(); //список для выбора поля при ранении корабля в Shoot1 стрельбе в диапазоне +3
        private List<int> rnd4 = new List<int>(); //список для выбора клетки при ранении корабля в Shoot1 стрельбе в диапазоне +1
        private List<int> rnd1234 = new List<int>(); //список для хранения клеток поражения одного корабля в Shoot1 стрельбе

        public static readonly string[] str1 = { "а", "б", "в", "г", "д", "е", "ж", "з", "и", "к" };
        public static readonly string[] str2 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        private int Letter = new int(); //для записи хода (буква)
        private int Index = new int();  //для записи хода (цифра)

        private int Points = 0; //очки, 20 = победа

        private int Number = 0; //количество кораблей каждого типа

        private bool flag = true; // индикатор убит(true) / не убит(false)

        private int Entering = 0; //количество попыток добить корабль



        public ShipFight() //конструктор генерации полей с кораблями
        {
            Four(Own_Field);
            Number = 0;
            while (Number < 2)
            {
                Three(Own_Field);
            }
            Number = 0;
            while (Number < 3)
            {
                Two(Own_Field);
            }
            Number = 0;
            while (Number < 4)
            {
                One(Own_Field);
            }
        }

        private void Four(int[,] Field)
        {
            var random = new Random(DateTime.Now.Millisecond);
            int x = random.Next(10);
            int y = random.Next(10);
            if (x > 5)
            {
                y = random.Next(5);
                for (int i = y; i < y + 4; i++)
                {
                    Field[i, x] = 1;
                }
                return;
            }
            if (y > 5)
            {
                x = random.Next(5);
                for (int j = x; j < x + 4; j++)
                {
                    Field[y, j] = 1;
                }
                return;
            }
            int k = random.Next(1);
            if (k == 0)
            {
                for (int i = y; i < y + 4; i++)
                {
                    Field[i, x] = 1;
                }
            }
            else
            {
                for (int j = x; j < x + 4; j++)
                {
                    Field[y, j] = 1;
                }
            }
        }
        private void Three(int[,] Field)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var x = random.Next(10);
            var y = random.Next(10);
            if (y > 6)
            {
                x = random.Next(7);
                for (int i = y - 1; i < y + 2; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }
                    if (i > 9)
                    {
                        break;
                    }
                    for (int j = x - 1; j < x + 4; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }
                        if (j > 9)
                        {
                            break;
                        }
                        if (Field[i, j] != 0)
                        {
                            return;
                        }
                    }
                }
                for (int j = x; j < x + 3; j++)
                {
                    Field[y, j] = 1;
                }
                Number++;
                return;
            }
            if (x > 6)
            {
                y = random.Next(7);
                for (int i = y - 1; i < y + 4; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }
                    if (i > 9)
                    {
                        break;
                    }
                    for (int j = x - 1; j < x + 2; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }
                        if (j > 9)
                        {
                            break;
                        }
                        {
                            if (Field[i, j] != 0)
                            {
                                return;
                            }
                        }
                    }
                }
                for (int i = y; i < y + 3; i++)
                {
                    Field[i, x] = 1;
                }
                Number++;
                return;
            }
            int k = random.Next(1);
            if (k == 0)
            {
                for (int i = y - 1; i < y + 4; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }
                    if (i > 9)
                    {
                        break;
                    }
                    for (int j = x - 1; j < x + 2; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }
                        if (j > 9)
                        {
                            break;
                        }
                        if (Field[i, j] != 0)
                        {
                            return;
                        }
                    }
                }
                for (int i = y; i < y + 3; i++)
                {
                    Field[i, x] = 1;
                }
                Number++;
            }
            else
            {
                for (int i = y - 1; i < y + 2; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }
                    if (i > 9)
                    {
                        break;
                    }
                    for (int j = x - 1; j < x + 4; j++)
                    {
                        if (j < 0)
                        {
                            j = 0;
                        }
                        if (j > 9)
                        {
                            break;
                        }
                        if (Field[i, j] != 0)
                        {
                            return;
                        }
                    }
                }
                for (int j = x; j < x + 3; j++)
                {
                    Field[y, j] = 1;
                }
                Number++;
            }
        }
        private void Two(int[,] Field)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var x = random.Next(10);
            var y = random.Next(10);
            if (y > 7)
            {
                x = random.Next(8);
                for (int i = y - 1; i < y + 2; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }
                    if (i > 9)
                    {
                        break;
                    }
                    for (int j = x - 1; j < x + 3; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }
                        if (j > 9)
                        {
                            break;
                        }
                        if (Field[i, j] != 0)
                        {
                            return;
                        }
                    }
                }
                for (int j = x; j < x + 2; j++)
                {
                    Field[y, j] = 1;
                }
                Number++;
                return;
            }
            if (x > 7)
            {
                y = random.Next(8);
                for (int i = y - 1; i < y + 3; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }
                    if (i > 9)
                    {
                        break;
                    }
                    for (int j = x - 1; j < x + 2; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }
                        if (j > 9)
                        {
                            break;
                        }
                        if (Field[i, j] != 0)
                        {
                            return;
                        }
                    }
                }
                for (int i = y; i < y + 2; i++)
                {
                    Field[i, x] = 1;
                }
                Number++;
                return;
            }
            int k = random.Next(1);
            if (k == 0)
            {
                for (int i = y - 1; i < y + 3; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }
                    if (i > 9)
                    {
                        break;
                    }
                    for (int j = x - 1; j < x + 2; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }
                        if (j > 9)
                        {
                            break;
                        }
                        if (Field[i, j] != 0)
                        {
                            return;
                        }
                    }
                }
                for (int i = y; i < y + 2; i++)
                {
                    Field[i, x] = 1;
                }
                Number++;
            }
            else
            {
                for (int i = y - 1; i < y + 2; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }
                    if (i > 9)
                    {
                        break;
                    }
                    for (int j = x - 1; j < x + 3; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }
                        if (j > 9)
                        {
                            break;
                        }
                        if (Field[i, j] != 0)
                        {
                            return;
                        }
                    }
                }
                for (int j = x; j < x + 2; j++)
                {
                    Field[y, j] = 1;
                }
                Number++;
            }
        }
        private void One(int[,] Field)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var x = random.Next(10);
            var y = random.Next(10);
            for (int i = y - 1; i < y + 2; i++)
            {
                if (i < 0)
                {
                    i++;
                }
                if (i > 9)
                {
                    break;
                }
                for (int j = x - 1; j < x + 2; j++)
                {
                    if (j < 0)
                    {
                        j++;
                    }
                    if (j > 9)
                    {
                        break;
                    }
                    if (Field[i, j] != 0)
                    {
                        return;
                    }
                }
            }
            Field[y, x] = 1;
            Number++;
        }

        public bool Victory(string str)
        {
            if (Points == 20)
            {
                //Console.SetCursorPosition(0, 25);
                Console.Write(str);
                return true;
            }
            return false;
        }

        public void Output(int[,] Field) //вывод в левой верхней части
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(2 * i + 3, 0);
                Console.Write(str1[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0, i + 1);
                Console.Write(str2[i]);
                Console.SetCursorPosition(2, i + 1);
                Console.Write("| ");
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(2 * j + 3, i + 1);
                    Convert(Field[i, j]);
                }
            }
        }
        public void Output2(int[,] Field) //вывод в левой нижней части
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(2 * i + 3, 13);
                Console.Write(str1[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0, i + 14);
                Console.Write(str2[i]);
                Console.SetCursorPosition(2, i + 14);
                Console.Write("| ");
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(2 * j + 3, i + 14);
                    Convert(Field[i, j]);

                }
            }
        }
        public void Output3(int[,] Field) //вывод в правой верхней части
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(2 * i + 3 + 30, 0);
                Console.Write(str1[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0 + 30, i + 1);
                Console.Write(str2[i]);
                Console.SetCursorPosition(2 + 30, i + 1);
                Console.Write("| ");
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(2 * j + 3 + 30, i + 1);
                    Convert(Field[i, j]);
                }
            }
        }
        public void Output4(int[,] Field) //вывод в правой нижней части
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(2 * i + 3 + 30, 13);
                Console.Write(str1[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0 + 30, i + 14);
                Console.Write(str2[i]);
                Console.SetCursorPosition(2 + 30, i + 14);
                Console.Write("| ");
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(2 * j + 3 + 30, i + 14);
                    Convert(Field[i, j]);

                }
            }
        }
        public void Convert(int a) // + (0), 'белый квадрат' (1), Х (2), O (3)
        {
            switch (a)
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


        public void Shoot(int[,] Field) //стрельба по полю 
        {
            if (Victory(""))
            {
                return;
            }
            Random();
            if (On_Ship(Letter, Index, Field))
            {
                Points++;
                Shoot(Field);
            }
        }

        public void Shoot1(int[,] Field) //стрельба по полю (с добиванием)
        {
            if (Victory(""))
            {
                return;
            }
            if (flag == true)
            {
                rnd12.Clear();
                rnd4.Clear();
                rnd1234.Clear();
                Entering = 0;
                Random();
            }
            else
            {
                Entering++;
                High_Random();
            }
            
            if (On_Ship(Letter, Index, Field))
            {
                rnd1234.Add(Letter * 10 + Index);
                Points++;
                Shoot1(Field);
            }
        }

        public bool On_Ship(int i, int j, int[,] Field) //отображение результата выбранной клетки в масииве
        {

            if (Field[i, j] == 0)
            {
                Field[i, j] = 3;
                Opponent_Field[i, j] = 3;
                return false;
            }
            if (Field[i, j] == 1)
            {
                Field[i, j] = 2;
                Opponent_Field[i, j] = 2;
                flag = Around_Ship(Field, i, j);
                return true;
            }
            return false;
        }
        private bool Around_Ship(int[,] Field, int i, int j) //окружает полностью убитые корабли O (3) и возвращает убит(true), не убит(false)
        {
            int ship_length = 1; //длина корабля
            int x = j; //для определения верхней границы целого корабля
            int y = i; //для определения левой границы целого корабля
            for (int k = 1; k < 4; k++) //проверяем есть ли еще палубы слева у i, j
            {
                try
                {
                    if (Field[i - k, j] == 2)
                    {
                        ship_length++;
                        y--;
                    }
                    if (Field[i - k, j] == 1) //попалась нетронутая палуба => еще не убит
                    {
                        return false;
                    }
                    if (Field[i - k, j] == 0 || Field[i - k, j] == 3)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
            for (int k = 1; k < 4; k++) //проверяем есть ли еще палубы справа у i, j
            {
                try
                {
                    if (Field[i + k, j] == 2)
                    {
                        ship_length++;
                    }
                    if (Field[i + k, j] == 1)
                    {
                        return false;
                    }
                    if (Field[i + k, j] == 0 || Field[i + k, j] == 3)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
            if (ship_length > 1) //расстановка O(3) вокруг убитого корабля
            {
                for (int k = y - 1; k < y + ship_length + 1 && k < 10; k++)
                {
                    if (k < 0)
                    {
                        k++;
                    }
                    for (int l = x - 1; l < x + 2 && l < 10; l++)
                    {
                        if (l < 0)
                        {
                            l++;
                        }
                        if (Field[k, l] != 2)
                        {
                            Field[k, l] = 3;
                            Opponent_Field[k, l] = 3;
                        }
                    }
                }
                return true;
            }

            for (int k = 1; k < 4; k++) //проверяем есть ли еще палубы сверху у i, j
            {
                try
                {
                    if (Field[i, j - k] == 2)
                    {
                        ship_length++;
                        x--;
                    }
                    if (Field[i, j - k] == 1)
                    {
                        return false;
                    }
                    if (Field[i, j - k] == 0 || Field[i, j - k] == 3)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
            for (int k = 1; k < 4; k++) //проверяем есть ли еще палубы снизу у i, j
            {
                try
                {
                    if (Field[i, j + k] == 2)
                    {
                        ship_length++;
                    }
                    if (Field[i, j + k] == 1)
                    {
                        return false;
                    }
                    if (Field[i, j + k] == 0 || Field[i, j + k] == 3)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
            if (ship_length > 1)
            {
                for (int l = y - 1; l < y + 2 && l < 10; l++)
                {
                    for (int k = x - 1; k < x + ship_length + 1 && k < 10; k++)
                    {
                        if (k < 0)
                        {
                            k++;
                        }
                        if (l < 0)
                        {
                            l++;
                        }
                        if (Field[l, k] != 2)
                        {
                            Field[l, k] = 3;
                            Opponent_Field[l, k] = 3;
                        }
                    }
                }
                return true;
            }

            if (ship_length == 1)
            {
                for (int k = y - 1; k < y + 2 && k < 10; k++)
                {
                    if (k < 0)
                    {
                        k = 0;
                    }
                    for (int l = x - 1; l < x + 2 && l < 10; l++)
                    {
                        if (l < 0)
                        {
                            l = 0;
                        }
                        if (Field[k, l] != 2)
                        {
                            Field[k, l] = 3;
                            Opponent_Field[k, l] = 3;
                        }
                    }
                }
                return true;
            }

            return true; //условность для соблюдения типу функции, сюда мы никак не доходим 
        }


        private void Random() //случайный выбор клетки
        {
            var random = rnd[new Random().Next(0, rnd.Count)];

            Index = random % 10;
            Letter = random / 10;
            rnd.Remove(random);
        }
        private void High_Random() //улучшенный выбор при добивании
        {
            if (Entering == 1)
            {
                for (int i = Letter - 1; i > Letter - 4 && i >= 0; --i) //идем влево от пораженной палубы
                {
                    if (rnd.Contains(i * 10 + Index))
                    {
                        rnd12.Add(i * 10 + Index);
                    }
                }

                for (int i = Letter + 1; i < Letter + 4 && i < 10; ++i) //вправо
                {
                    if (rnd.Contains(i * 10 + Index))
                    {
                        rnd12.Add(i * 10 + Index);
                    }
                }

                for (int i = Index - 1; i > Index - 4 && i >= 0; --i) //вверх
                {
                    if (rnd.Contains(Letter * 10 + i))
                    {
                        rnd12.Add(Letter * 10 + i);
                    }
                }

                for (int i = Index + 1; i < Index + 4 && i < 10; ++i) //вниз
                {
                    if (rnd.Contains(Letter * 10 + i))
                    {
                        rnd12.Add(Letter * 10 + i);
                    }
                }

                if (rnd12.Contains((Letter - 1) * 10 + Index)) { rnd4.Add((Letter - 1) * 10 + Index); }

                if (rnd12.Contains((Letter + 1) * 10 + Index)) { rnd4.Add((Letter + 1) * 10 + Index); }

                if (rnd12.Contains(Letter * 10 + Index - 1)) { rnd4.Add(Letter * 10 + Index - 1); }

                if (rnd12.Contains(Letter * 10 + Index + 1)) { rnd4.Add(Letter * 10 + Index + 1); }
            }

            if (rnd1234.Count > 1)
            {
                rnd4.Clear();
                rnd1234.Sort();
                if (rnd1234[0] / 10 == rnd1234[rnd1234.Count - 1] / 10) //совпадают Letter
                    
                {
                    if (rnd12.Contains(rnd1234[0] - 1))
                    {
                        rnd4.Add(rnd1234[0] - 1);
                    }

                    if (rnd12.Contains(rnd1234[0] + 1))
                    {
                        rnd4.Add(rnd1234[0] + 1);
                    }

                    if (rnd12.Contains(rnd1234[rnd1234.Count - 1] - 1))
                    {
                        rnd4.Add(rnd1234[rnd1234.Count - 1] - 1);
                    }

                    if (rnd12.Contains(rnd1234[rnd1234.Count - 1] + 1))
                    {
                        rnd4.Add(rnd1234[rnd1234.Count - 1] + 1);
                    }
                }

                if (rnd1234[0] % 10 == rnd1234[rnd1234.Count - 1] % 10) //совпадают Index

                {
                    if (rnd12.Contains(rnd1234[0] - 10))
                    {
                        rnd4.Add(rnd1234[0] - 10);
                    }

                    if (rnd12.Contains(rnd1234[0] + 10))
                    {
                        rnd4.Add(rnd1234[0] + 10);
                    }

                    if (rnd12.Contains(rnd1234[rnd1234.Count - 1] - 10))
                    {
                        rnd4.Add(rnd1234[rnd1234.Count - 1] - 10);
                    }

                    if (rnd12.Contains(rnd1234[rnd1234.Count - 1] + 10))
                    {
                        rnd4.Add(rnd1234[rnd1234.Count - 1] + 10);
                    }
                }
            }

            var random4 = rnd4[new Random().Next(0, rnd4.Count)];
            Index = random4 % 10;
            Letter = random4 / 10;

            rnd.Remove(random4);
            rnd4.Remove(random4);
            rnd12.Remove(random4);
        }

    }

    internal class Program
    {
        public static void View() //наглядное демонстрирование полей (изначально / в конце)
        {
            ShipFight Bot1;
            ShipFight Bot2;
            while (true)
            {
                Bot1 = new ShipFight();
                Bot2 = new ShipFight();
                Boolean yes = true;
                Bot1.Output(Bot1.Own_Field);
                Console.ReadKey();
                Bot2.Output2(Bot2.Own_Field);
                Console.ReadKey();
                while (yes)
                {
                    while (true)
                    {
                        Bot1.Shoot(Bot2.Own_Field);
                        if (Bot1.Victory("Bot1 won!"))
                        {
                            break;
                        }
                        Bot2.Shoot1(Bot1.Own_Field);
                        if (Bot2.Victory("Bot2 won!"))
                        {
                            break;
                        }
                    }

                    Console.ReadKey();
                    Bot1.Output3(Bot1.Own_Field);
                    Console.ReadKey();
                    Bot2.Output4(Bot2.Own_Field);
                    Console.ReadKey();
                    GC.Collect();
                    break;
                }
            }
        }

        public static void Test() //тестирование стратегий 1 и 2
        {
            
            
            FileStream out12 = new FileStream("out12.txt", FileMode.Create, FileAccess.Write);
            FileStream out11 = new FileStream("out11.txt", FileMode.Create, FileAccess.Write);
            FileStream out22 = new FileStream("out22.txt", FileMode.Create, FileAccess.Write);
            FileStream results = new FileStream("results.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer_rez = new StreamWriter(results);
            StreamWriter writer_12 = new StreamWriter(out12);
            StreamWriter writer_11 = new StreamWriter(out11);
            StreamWriter writer_22 = new StreamWriter(out22);
            writer_11.WriteLine("TEST strategy 1 VS 1");
            writer_12.WriteLine("TEST strategy 1 VS 2");
            writer_22.WriteLine("TEST strategy 2 VS 2");
            int key = 0, Bot1_Victory = 0, Bot2_Victory = 0, test_count = 1000;


            while (key < test_count)
            {
                ShipFight Bot1 = new ShipFight();
                ShipFight Bot2 = new ShipFight();
                while (true)
                {
                    Bot1.Shoot(Bot2.Own_Field);
                    if (Bot1.Victory(""))
                    {
                        writer_11.WriteLine("Bot1 won!");
                        Bot1_Victory++;
                        break;
                    }
                    Bot2.Shoot(Bot1.Own_Field);
                    if (Bot2.Victory(""))
                    {
                        writer_11.WriteLine("Bot2 won!");
                        Bot2_Victory++;
                        break;
                    }
                    
                }
                key++;
            }
                

            writer_rez.WriteLine("Testing strategy 1 VS 1: " + Bot1_Victory + " - " + Bot2_Victory);
            Console.WriteLine("Test on Strategy 1 VS 1 was written to out11.txt");
            key = 0;
            Bot1_Victory = 0;
            Bot2_Victory = 0;
                

            while (key < test_count)
                {
                    ShipFight Bot1 = new ShipFight();
                    ShipFight Bot2 = new ShipFight();
                    while (true)
                    {
                        Bot1.Shoot(Bot2.Own_Field);
                        if (Bot1.Victory(""))
                        {
                            writer_12.WriteLine("Bot won!");
                            Bot1_Victory++;
                            break;
                        }
                        Bot2.Shoot1(Bot1.Own_Field);
                        if (Bot2.Victory(""))
                        {
                            writer_12.WriteLine("High_Bot won!");
                            Bot2_Victory++;
                            break;
                        }
                    
                    }
                    key++;
                }
                

            writer_rez.WriteLine("Testing strategy 1 VS 2: " + Bot1_Victory + " - " + Bot2_Victory);
            Console.WriteLine("Test on Strategy 1 VS 2 was written to out12.txt");
            key = 0;
            Bot1_Victory = 0;
            Bot2_Victory = 0;


            while (key < test_count)
                {
                    ShipFight Bot1 = new ShipFight();
                    ShipFight Bot2 = new ShipFight();
                    while (true)
                    {
                        
                        Bot1.Shoot1(Bot2.Own_Field);
                        if (Bot1.Victory(""))
                        {
                            writer_22.WriteLine("High_Bot1 won!");
                            Bot1_Victory++;
                            break;
                        }
                        Bot2.Shoot1(Bot1.Own_Field);
                        if (Bot2.Victory(""))
                        {
                            writer_22.WriteLine("High_Bot2 won!");
                            Bot2_Victory++;
                            break;
                        }
                        
                    }
                    key++;
                }


            writer_rez.WriteLine("Testing strategy 2 VS 2: " + Bot1_Victory + " - " + Bot2_Victory);
            Console.WriteLine("Test on Strategy 2 VS 2 was written to out22.txt");
            Console.WriteLine("Final results were written to results.txt");

            writer_11.Close();
            writer_12.Close();
            writer_22.Close();
            writer_rez.Close();



        }
        public static int Main()
        {
            //View(); //1 игра стратегии 1 VS 2 с наглядным демонстрированием полей (для отображения поля нажать 'пробел'
            Test();
            return 0;
        }

    }
}