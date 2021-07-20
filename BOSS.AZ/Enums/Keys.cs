using System;
using System.Threading;
using System.Windows.Forms;
using Bossaz.Abstracts;
using Bossaz.ConsoleInterface;
using Bossaz.Entities;
using Bossaz.Enums;
using Bossaz.Exceptions;

using Bossaz.Helpers;
using Bossaz.Logger;
using Bossaz.Sides.Employee;
using Bossaz.Sides.Employer;
using Bossaz.UserAccess;
using Bossaz.Sides.UserAccess;
using System.Collections.Generic;
namespace Keys
{

    public class Menu
    {
        private int SelectedIndex;
        public int MainSelectedIndex;
        public static IList<string> Options;
        public Menu(IList<string> options)
        {
            Options = options;
            SelectedIndex = 0;
        }
        private void DisplayOptions()
        {
            UserAccessSide.Bosik();
            for (int i = 0; i < Options.Count; i++)
            {
                string currentOption = Options[i];
                string prefix;
                if (i == SelectedIndex)
                {
                    Console.OutputEncoding = System.Text.Encoding.Unicode;
                    prefix = "»";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.OutputEncoding = System.Text.Encoding.Unicode;

                Console.WriteLine($"{prefix} << {currentOption} >>");
            }
            Console.ResetColor();
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Count - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Count)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);
            MainSelectedIndex = SelectedIndex;
            return SelectedIndex;
        }
    }


}