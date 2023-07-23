using GameServer;
using System;

namespace AgentApp
{
    public class MyAgent : IAgent
    {
        // Здесь можно добавить поля или свойства для хранения информации или состояния агента

        public void Run()
        {
            Console.WriteLine("Агент запущен и готов управлять кораблями!");

            // Здесь реализуется логика агента для управления кораблями в битве
            // Например, можно использовать методы для перемещения, поворота и стрельбы кораблей

            Game game = new Game();
            game.InitializeGame();

            while (!game.IsGameOver())
            {
                foreach (var playerShip in game.GetPlayerFleet())
                {
                    Direction playerMoveDirection = GetRandomDirection();
                    bool shouldPlayerShoot = ShouldShoot();

                    MoveShip(playerShip, playerMoveDirection);

                    if (shouldPlayerShoot)
                    {
                        Ship targetShip = GetRandomTargetShip(game.GetEnemyFleet());
                        FireTorpedo(playerShip, targetShip);
                    }
                }

                foreach (var enemyShip in game.GetEnemyFleet())
                {
                    Direction enemyMoveDirection = GetRandomDirection();
                    bool shouldEnemyShoot = ShouldShoot();

                    MoveShip(enemyShip, enemyMoveDirection);

                    if (shouldEnemyShoot)
                    {
                        var a = game.GetPlayerFleet();
                        Ship targetShip = GetRandomTargetShip(a);
                        FireTorpedo(enemyShip, targetShip);
                    }
                }

                game.CheckForWinner();
            }

            Console.WriteLine("Агент завершил управление кораблями!");
        }

        private Direction GetRandomDirection()
        {
            // Здесь реализуется логика получения случайного направления
            Random random = new Random();
            int randomNumber = random.Next(0, 4);
            return (Direction)randomNumber;
        }

        private bool ShouldShoot()
        {
            // Здесь реализуется логика определения, нужно ли стрелять
            Random random = new Random();
            return random.Next(0, 2) == 0;
        }

        private Ship GetRandomTargetShip(List<Ship> ships)
        {
            // Здесь реализуется логика выбора случайной цели для выстрела
            Random random = new Random();
            int randomNumber = random.Next(0, ships.Count);
            return ships[randomNumber];
        }

        private void MoveShip(Ship ship, Direction direction)
        {
            // Здесь реализуется логика перемещения корабля в указанном направлении
            // Обратите внимание, что метод MoveShip реализуется в классе Game.cs
        }

        private void FireTorpedo(Ship sourceShip, Ship targetShip)
        {
            // Здесь реализуется логика стрельбы фотонными торпедами
            // Обратите внимание, что метод FireTorpedo реализуется в классе Game.cs
        }
    }
}
