using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class BattleResult
    {
        public FleetType WinningFleet { get; set; }
        public int TotalTurns { get; set; }

        public BattleResult(FleetType winningFleet, int totalTurns)
        {
            WinningFleet = winningFleet;
            TotalTurns = totalTurns;
        }

        public void PrintResult()
        {
            string winner = WinningFleet == FleetType.Player ? "Игрок" : "Противник";
            Console.WriteLine($"Победитель: {winner}");
            Console.WriteLine($"Количество ходов: {TotalTurns}");
        }
    }

}
