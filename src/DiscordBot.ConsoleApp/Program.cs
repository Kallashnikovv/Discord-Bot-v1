using DiscordBot.Discord;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DiscordBot.ConsoleApp
{
    public static class Program
    {
        private static DiscordBotClient _bot;

        private static void Main()
        {
            _bot = ActivatorUtilities.CreateInstance<DiscordBotClient>(InversionOfControl.Provider);

            _ = _bot.InitializeAsync();
            while (true)
            {
                Console.ReadLine();
            }
            //HandleInput();
        }

        private static void HandleInput()
        {
            while (true)
            {
                DisplayMenu();
                Console.Write(ConsoleStrings.PLEASE_ENTER_MENU_NUMBER);
                var success = int.TryParse(Console.ReadLine(), out var choice);

                if (!success)
                {
                    Console.WriteLine(ConsoleStrings.ERROR_CHOICE_NOT_A_NUMBER);
                }
                switch (choice)
                {
                    case 1:
                    {
                        break;
                    }
                    case 2:
                    {
                        _ = _bot.InitializeAsync();
                            Console.WriteLine("\n");
                        break;
                    }
                    case 3:
                    {
                        Environment.Exit(0);
                        break;
                    }
                    default:
                    {
                        Console.WriteLine(ConsoleStrings.UNKNOWN_OPTION_SELECTED);
                        AnyKeyToContinue();
                        break;
                    }
                }
            }
        }

        public static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(ConsoleStrings.MAIN_MENU_OPTIONS);
        }

        private static void AnyKeyToContinue()
        {
            Console.WriteLine(ConsoleStrings.ANY_KEY_TO_CONTINUE);
            Console.ReadKey();
        }

        public static void ClearCurrentConsoleLine()
        {
            var currentTop = Console.CursorTop;
            var currentLeft = Console.CursorLeft;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(currentLeft, currentTop);
        }
    }
}
