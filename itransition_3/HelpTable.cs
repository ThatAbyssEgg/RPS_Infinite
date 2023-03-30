using System;
using System.Collections.Generic;
using ConsoleTables;

namespace itransition_3
{
    internal class HelpTable
    {
        public static void GenerateHelpTable(string[] args)
        {
            Console.WriteLine("To play the game, you should input the number of your turn and press enter.");
            Console.WriteLine("The following table represents the possible combinations of turn & their results.");

            var table = new ConsoleTable("TURN", "WHAT IT BEATS", "WHAT IT LOSES TO");
            for (int i = 0; i < args.Length; i++)
            {
                List<string> shiftedTable = GameProcesses.GetShiftedTable(args, i);
                string wins = string.Empty;
                string losses = string.Empty;
                for (int j = 0; j < shiftedTable.Count - 1; j++)
                {
                    if (j < (shiftedTable.Count - 1) / 2)
                    {
                        wins += shiftedTable[j];
                        wins += " ";
                    }
                    else
                    {
                        losses += shiftedTable[j];
                        losses += " ";
                    }
                }
                table.AddRow(args[i], wins, losses);
            }
            table.Write();
        }
    }
}
