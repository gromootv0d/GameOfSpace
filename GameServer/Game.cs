using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public enum RotationDirection
{
    Clockwise,
    Counterclockwise
}

public enum FleetType
{
    Player,
    Enemy,
    None
}

public class Game
{
    private List<Ship> playerFleet;
    private List<Ship> enemyFleet;
    private bool isGameOver;
    private Random random;

    public Game()
    {
        playerFleet = new List<Ship>();
        enemyFleet = new List<Ship>();
        isGameOver = false;
        random = new Random();
    }

    public void InitializeGame()
    {
        // Очищаем списки флотилий перед инициализацией новой игры
        playerFleet.Clear();
        enemyFleet.Clear();

        // Инициализируем корабли для каждой флотилии
        InitializePlayerFleet();
        InitializeEnemyFleet();

        // Задаем другие начальные параметры, если это необходимо
        isGameOver = false;
    }

    private void InitializePlayerFleet()
    {
        // Создаем 3 корабля для игрока и размещаем их на поле
        for (int i = 0; i < 3; i++)
        {
            // Генерируем случайные координаты для каждого корабля
            int randomX = random.Next(6); // случайное число от 0 до 5
            int randomY = random.Next(6);

            // Проверяем валидность координат и, если координаты невалидны, генерируем новые, пока не найдем валидные
            while (!IsValidPlacement(randomX, randomY, playerFleet))
            {
                randomX = random.Next(6);
                randomY = random.Next(6);
            }

            // Создаем и размещаем корабль
            Ship newShip = new Ship(randomX, randomY, Direction.Up); // Устанавливаем направление как Up, так как корабли состоят из одной клетки
            playerFleet.Add(newShip);
        }
    }

    private void InitializeEnemyFleet()
    {
        // Создаем 3 корабля для противника и размещаем их на поле аналогично инициализации для игрока
        for (int i = 0; i < 3; i++)
        {
            int randomX = random.Next(6);
            int randomY = random.Next(6);

            while (!IsValidPlacement(randomX, randomY, enemyFleet))
            {
                randomX = random.Next(6);
                randomY = random.Next(6);
            }

            Ship newShip = new Ship(randomX, randomY, Direction.Up); // Устанавливаем направление как Up, так как корабли состоят из одной клетки
            enemyFleet.Add(newShip);
        }
    }

    private bool IsValidPlacement(int x, int y, List<Ship> fleet)
    {
        // Проверяем валидность координат для размещения корабля
        // Например, проверяем, что на данной клетке нет другого корабля
        // Если координаты валидны, возвращаем true, иначе false

        // Проверка на выход за границы поля
        if (x < 0 || x >= 6 || y < 0 || y >= 6)
        {
            return false;
        }

        // Проверка наличия другого корабля на данной клетке
        foreach (var ship in fleet)
        {
            if (ship.X == x && ship.Y == y)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void PlayNextTurn()
    {
        while (!isGameOver)
        {
            // Логика выполнения хода игры для флотилии игрока
            foreach (var playerShip in playerFleet)
            {
                // Предположим, что у нас есть методы для получения действий игрока (направление, стрельба и т.д.)
                Direction playerMoveDirection = GetPlayerMoveDirection(playerShip);
                bool shouldPlayerShoot = ShouldPlayerShoot(playerShip);

                // Выполнение перемещения и поворота корабля игрока
                MoveShip(playerShip, playerMoveDirection);
                // Метод RotateShip реализуется аналогично методу MoveShip

                // Если игрок решил стрелять, производим выстрел
                if (shouldPlayerShoot)
                {
                    // Предположим, у нас есть метод для получения цели для выстрела
                    Ship targetShip = GetPlayerTargetShip(playerShip);
                    FireTorpedo(playerShip, targetShip);
                }
            }

            // Логика выполнения хода игры для флотилии противника
            foreach (var enemyShip in enemyFleet)
            {
                // Предположим, у нас есть методы для получения действий противника (направление, стрельба и т.д.)
                Direction enemyMoveDirection = GetEnemyMoveDirection(enemyShip);
                bool shouldEnemyShoot = ShouldEnemyShoot(enemyShip);

                // Выполнение перемещения и поворота корабля противника
                MoveShip(enemyShip, enemyMoveDirection);
                // Метод RotateShip реализуется аналогично методу MoveShip

                // Если противник решил стрелять, производим выстрел
                if (shouldEnemyShoot)
                {
                    // Предположим, у нас есть метод для получения цели для выстрела
                    Ship targetShip = GetEnemyTargetShip(enemyShip);
                    FireTorpedo(enemyShip, targetShip);
                }
            }

            // Проверка наличия победителя после каждого хода
            CheckForWinner();
        }

    }

    private Ship GetEnemyTargetShip(Ship enemyShip)
    {
        // Здесь происходит логика определения случайной цели стрельбы для корабля противника.

        // Получаем текущие координаты корабля противника
        int currentX = enemyShip.X;
        int currentY = enemyShip.Y;

        // Получаем случайное направление для стрельбы противника
        Direction randomDirection = (Direction)random.Next(4);

        // Вычисляем координаты цели стрельбы в зависимости от случайного направления
        int targetX = currentX;
        int targetY = currentY;

        switch (randomDirection)
        {
            case Direction.Up:
                targetY--;
                break;
            case Direction.Down:
                targetY++;
                break;
            case Direction.Left:
                targetX--;
                break;
            case Direction.Right:
                targetX++;
                break;
        }

        // Проверяем, находится ли цель стрельбы в пределах игрового поля
        if (IsWithinBounds(targetX, targetY))
        {
            return new Ship(targetX, targetY, randomDirection); // Возвращаем координаты цели стрельбы (размер кораблей всего 1 клетку)
        }

        return null; // Возвращаем null, если цель стрельбы за пределами игрового поля
    }

    private bool ShouldEnemyShoot(Ship enemyShip)
    {
        // Здесь происходит логика определения, должен ли корабль противника производить стрельбу.

        // Получаем цель стрельбы для корабля противника
        Ship targetShip = GetPlayerTargetShip(enemyShip);

        // Если стрельба достигает корабля игрока, считаем стрельбу успешной
        if (targetShip != null && IsTargetHit(targetShip, playerFleet))
        {
            return true;
        }

        return false; // Стрельба не была успешной, либо цель стрельбы находится за пределами игрового поля или не достигла корабля игрока
    }


    private bool IsTargetHit(Ship targetShip, List<Ship> fleet)
    {
        foreach (var ship in fleet)
        {
            if (ship.X == targetShip.X && ship.Y == targetShip.Y)
            {
                return true; // Попадание произошло, если на клетке цели стрельбы есть корабль из флотилии противника
            }
        }

        return false; // Нет попадания, если на клетке цели стрельбы нет корабля из флотилии противника
    }

    private bool IsWithinBounds(int targetX, int targetY)
    {
        // Предполагаем, что игровое поле имеет размер 6x6 клеток.
        // Проверяем, что координаты находятся в пределах от 0 до 5 включительно.
        return targetX >= 0 && targetX < 6 && targetY >= 0 && targetY < 6;
    }

    private Direction GetEnemyMoveDirection(Ship enemyShip)
    {
        // Здесь происходит логика определения направления хода для корабля противника.

        // Генерируем случайное число от 0 до 2, чтобы определить тип движения: 0 - вперед, 1 - поворот против часовой стрелки, 2 - поворот по часовой стрелке.
        int moveType = random.Next(3);

        // Получаем текущее направление корабля противника
        Direction currentDirection = enemyShip.FacingDirection;

        // Определяем новое направление в зависимости от типа движения
        Direction newDirection;

        switch (moveType)
        {
            case 0:
                // Вперед
                newDirection = currentDirection;
                break;
            case 1:
                // Поворот против часовой стрелки
                newDirection = GetCounterclockwiseDirection(currentDirection);
                break;
            case 2:
                // Поворот по часовой стрелке
                newDirection = GetClockwiseDirection(currentDirection);
                break;
            default:
                newDirection = currentDirection;
                break;
        }

        return newDirection;
    }

    private Direction GetCounterclockwiseDirection(Direction currentDirection)
    {
        switch (currentDirection)
        {
            case Direction.Up:
                return Direction.Left;
            case Direction.Down:
                return Direction.Right;
            case Direction.Left:
                return Direction.Down;
            case Direction.Right:
                return Direction.Up;
            default:
                return currentDirection;
        }
    }

    private Direction GetClockwiseDirection(Direction currentDirection)
    {
        switch (currentDirection)
        {
            case Direction.Up:
                return Direction.Right;
            case Direction.Down:
                return Direction.Left;
            case Direction.Left:
                return Direction.Up;
            case Direction.Right:
                return Direction.Down;
            default:
                return currentDirection;
        }
    }

    private Ship GetPlayerTargetShip(Ship playerShip)
    {
        // Здесь происходит логика определения цели стрельбы для корабля игрока.

        // Получаем текущие координаты корабля игрока
        int currentX = playerShip.X;
        int currentY = playerShip.Y;

        // Вычисляем координаты цели стрельбы в зависимости от направления корабля
        int targetX = currentX;
        int targetY = currentY;

        switch (playerShip.FacingDirection)
        {
            case Direction.Up:
                targetY--;
                break;
            case Direction.Down:
                targetY++;
                break;
            case Direction.Left:
                targetX--;
                break;
            case Direction.Right:
                targetX++;
                break;
            default:
                break;
        }

        // Проверяем, находится ли цель стрельбы в пределах игрового поля
        if (IsWithinBounds(targetX, targetY))
        {
            // Создаем объект Ship для представления цели стрельбы (размер кораблей всего 1 клетку)
            Ship targetShip = new Ship(targetX, targetY, playerShip.FacingDirection);

            // Проверяем, есть ли на клетке цели стрельбы корабль из флотилии противника
            if (IsTargetHit(targetShip, enemyFleet))
            {
                return targetShip; // Возвращаем цель стрельбы (цель попадания)
            }
        }

        return null; // Возвращаем null, если цель стрельбы за пределами игрового поля или на клетке нет корабля противника
    }

    private bool ShouldPlayerShoot(Ship playerShip)
    {
        // Здесь происходит логика определения, хочет ли игрок произвести стрельбу.
        // Это может быть реализовано с помощью интерфейса взаимодействия с пользователем,
        // где игрок может выбрать, стрелять или нет, через UI или ввод команды в консольном приложении.
        // В данном примере, для упрощения, мы будем генерировать случайное булевое значение для стрельбы.

        return random.Next(2) == 0; // Вернет true или false случайным образом.
    }


    private Direction GetPlayerMoveDirection(Ship playerShip)
    {
        // Здесь происходит логика получения хода от игрока.
        // Это может быть реализовано с помощью интерфейса взаимодействия с пользователем,
        // где игрок выбирает направление с помощью UI или вводит команду в консольном приложении.
        // В данном примере, для упрощения, мы будем генерировать случайное направление для хода игрока.

        Array directions = Enum.GetValues(typeof(Direction));
        return (Direction)directions.GetValue(random.Next(directions.Length));
    }


    private void CheckForWinner()
    {
        if (playerFleet.Count == 0 || enemyFleet.Count == 0)
        {
            isGameOver = true;
        }
    }

    public void MoveShip(Ship ship, Direction direction)
    {
        // Получаем текущие координаты корабля
        int currentX = ship.X;
        int currentY = ship.Y;

        // Вычисляем новые координаты в зависимости от направления
        int newX = currentX;
        int newY = currentY;

        switch (direction)
        {
            case Direction.Up:
                newY--;
                break;
            case Direction.Down:
                newY++;
                break;
            case Direction.Left:
                newX--;
                break;
            case Direction.Right:
                newX++;
                break;
            default:
                break;
        }

        // Проверяем, находится ли новая позиция в пределах игрового поля
        if (IsWithinBounds(newX, newY))
        {
            // Перемещаем корабль в новую позицию
            ship.X = newX;
            ship.Y = newY;
        }
    }

    public void FireTorpedo(Ship ship, Ship targetShip)
    {
        // Логика стрельбы торпедой из одного корабля в другой
    }

    public FleetType GetWinner()
    {
        if (playerFleet.Count == 0)
        {
            return FleetType.Enemy;
        }
        else if (enemyFleet.Count == 0)
        {
            return FleetType.Player;
        }

        return FleetType.None;
    }

    public List<Ship> GetPlayerFleet()
    {
        return playerFleet;
    }

    public List<Ship> GetEnemyFleet()
    {
        return enemyFleet;
    }
}
