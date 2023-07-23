using System;

namespace AgentApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Программа управления космическими кораблями");
            Console.WriteLine("Запускаем агента...");

            // Создаем экземпляр класса MyAgent
            IAgent agent = new MyAgent();

            // Запускаем агента
            agent.Run();

            Console.WriteLine("\nАгент завершил свою работу. Спасибо за использование!");
        }
    }
}
