using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace itransition_3
{
    class Program
    {
        private static void SendError(int errorNumber)
        {
            switch (errorNumber)
            {
                case 0:
                    Console.WriteLine("Incorrect input of parameters. Please, make sure that the amount of parameters is even, greater than three & there are no repeating parameters.");
                    Console.WriteLine("Examples of incorrect launch:");
                    Console.WriteLine("game.exe Rock Paper Scissors Well");
                    Console.WriteLine("game.exe Yin Yang");
                    Console.WriteLine("game.exe StopCopyingMe StopCopyingMe StopCopyingMe");
                    break;
                case 1:
                    Console.WriteLine("Incorrect input! Please, input the number of your turn!");
                    Console.WriteLine("Example:");
                    Console.WriteLine("Choose your turn!");
                    Console.WriteLine("1: Rock");
                    Console.WriteLine("2: Paper");
                    Console.WriteLine("3: Scissors");
                    Console.WriteLine("0: Exit");
                    Console.WriteLine("help: Rules & application information");
                    Console.WriteLine("YOUR INPUT WITHOUT THIS MESSAGE: 1");
                    Console.WriteLine("You chose Rock!");
                    Console.WriteLine("Your opponent chose Paper, you lost...");
                    break;
            }

            Console.WriteLine("Press enter when you're ready to continue...");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            if (args.Length % 2 == 1 && args.Length >= 3 && args.Length == args.Distinct().Count())
            {
                bool isPlaying = true;
                while (isPlaying)
                {
                    // AI turn, then AI HMAC generation
                    byte[] key = RandomNumberGenerator.GetBytes(256);
                    int AIturn = GameProcesses.GetAITurn(args);
                    GameProcesses.GetAIHMAC(args, AIturn, key);

                    // Player menu
                    Console.WriteLine("Choose your turn!");
                    for (int i = 0; i < args.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}: {args[i]}");
                    }
                    Console.WriteLine("0: Exit");
                    Console.WriteLine("help: Rules & application information");

                    // Player choice
                    string playerInput = Console.ReadLine();
                    if (playerInput == "0")
                    {
                        isPlaying = false;
                    }
                    else if (playerInput == "help")
                    {
                        HelpTable.GenerateHelpTable(args);
                    }

                    else if (playerInput.All(Char.IsDigit) && playerInput != string.Empty)
                    {
                        int intPlayerInput = Convert.ToInt32(playerInput) - 1;
                        Console.WriteLine($"You chose {args[intPlayerInput]}!");
                        GameProcesses.ComputeAITurn(args, playerInput, AIturn, GameProcesses.GetShiftedTable(args, intPlayerInput));
                        Console.WriteLine($"HMAC key: {Convert.ToHexString(key)}");
                        Console.WriteLine("Press enter when you're ready to continue...");
                        Console.ReadLine();
                    }
                    else
                    {
                        SendError(1);
                    }
                }

                Environment.Exit(0);
            }
            else
            {
                SendError(0);
            }
        }
    }
}
