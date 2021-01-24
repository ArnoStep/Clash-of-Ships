using System;
using System.IO;
using Game_Helper;

namespace Clash_of_ships_OOP
{
    class Program
    {
        public static void View() 
        {
            Player Bot1;
            SmartPlayer Bot2;
            int key = 1;
            while (key == 1)
            {
                Bot1 = new Player();
                Bot2 = new SmartPlayer();
                Boolean yes = true;
                GameHelper.OutputLeftUp(Bot1.OwnField.Box);
                _ = Console.ReadKey();
                GameHelper.OutputLeftDown(Bot2.OwnField.Box);
                _ = Console.ReadKey();
                while (yes)
                {
                    while (true)
                    {
                        Bot1.Shoot(Bot2.OwnField.Box);
                        if (Bot1.CheckVictory("Bot1 won!"))
                        {
                            break;
                        }
                        Bot2.Shoot(Bot1.OwnField.Box);
                        if (Bot2.CheckVictory("Smart Bot2 won!"))
                        {
                            break;
                        }
                    }

                    _ = Console.ReadKey();
                    GameHelper.OutputRightUp(Bot1.OwnField.Box);
                    _ = Console.ReadKey();
                    GameHelper.OutputRightDown(Bot2.OwnField.Box);
                    _ = Console.ReadKey();
                    GC.Collect();
                    break;
                }
                Console.Clear();
                Console.WriteLine("Play again? 0 = No, 1 - Yes", '\n');
                key = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }
        }
        public static void Test(int testCount) 
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
            int key = 0, Bot1_Victory = 0, Bot2_Victory = 0;


            while (key < testCount)
            {
                Player Bot1 = new Player();
                Player Bot2 = new Player();
                while (true)
                {
                    Bot1.Shoot(Bot2.OwnField.Box);
                    if (Bot1.CheckVictory(""))
                    {
                        writer_11.WriteLine("Bot1 won!");
                        Bot1_Victory++;
                        break;
                    }
                    Bot2.Shoot(Bot1.OwnField.Box);
                    if (Bot2.CheckVictory(""))
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


            while (key < testCount)
            {
                Player Bot1 = new Player();
                SmartPlayer Bot2 = new SmartPlayer();
                while (true)
                {
                    Bot1.Shoot(Bot2.OwnField.Box);
                    if (Bot1.CheckVictory(""))
                    {
                        writer_12.WriteLine("Bot won!");
                        Bot1_Victory++;
                        break;
                    }
                    Bot2.Shoot(Bot1.OwnField.Box);
                    if (Bot2.CheckVictory(""))
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


            while (key < testCount)
            {
                SmartPlayer Bot1 = new SmartPlayer();
                SmartPlayer Bot2 = new SmartPlayer();
                while (true)
                {

                    Bot1.Shoot(Bot2.OwnField.Box);
                    if (Bot1.CheckVictory(""))
                    {
                        writer_22.WriteLine("High_Bot1 won!");
                        Bot1_Victory++;
                        break;
                    }
                    Bot2.Shoot(Bot1.OwnField.Box);
                    if (Bot2.CheckVictory(""))
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
            Test(1000);
            return 0;
        }
    }
}
