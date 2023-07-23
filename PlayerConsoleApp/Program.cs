using System;

namespace PlayerConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Добро пожаловать в консольное приложение для отображения битвы!");
            Console.WriteLine("Начинаем новую битву...\n");

            // Создаем экземпляр класса BattleDisplay
            BattleDisplay battleDisplay = new BattleDisplay();

            // Отображаем битву в консоли
            battleDisplay.DisplayBattle();

            Console.WriteLine("\nБитва завершена. Спасибо за игру!");
        }
    }
}
