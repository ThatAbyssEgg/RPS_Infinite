using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itransition_3
{
    internal class GameProcesses
    {
        public static int GetAITurn(string[] args)
        {
            Random random = new Random();
            return random.Next(args.Length);
        }

        public static void GetAIHMAC(string[] args, int AIturn, byte[] key)
        {
            HMAC_Generation.GetHMAC(args[AIturn], key);
        }
        
        public static List<string> GetShiftedTable(string[] args, int intPlayerInput)
        {
            List<string> newGameParams = new List<string>(args);

            int argsHalfLength = (args.Length - 1) / 2;
            int middleValue = intPlayerInput;
            // Shifting list circularlly until active value is not in the center
            while (middleValue != argsHalfLength)
            {
                string firstValue = newGameParams[0];
                string playerInputFullString = newGameParams[middleValue];
                newGameParams.RemoveAt(0);
                newGameParams.Add(firstValue);
                middleValue = newGameParams.IndexOf(playerInputFullString);
            }
            newGameParams.RemoveAt(argsHalfLength);
            newGameParams.Add(args[intPlayerInput]);
            return newGameParams;
        }

        public static void ComputeAITurn(string[] args, string playerInput, int AIturn, List<string> newGameParams)
        {
            // The actual turn
            Console.Write($"Your opponent chose {newGameParams[AIturn]}, you ");
            if (AIturn == newGameParams.Count - 1)
            {
                Console.WriteLine("encountered a draw. Interesting...");
            }
            else if (AIturn < (args.Length - 1) / 2)
            {
                Console.WriteLine("won!");
            }
            else
            {
                Console.WriteLine("lost...");
            }
        }
    }
}
