namespace GameServer
{
    public class ShipMovement
    {
        public void MoveShip(Ship ship, Direction direction, Func<int, int, bool> isWithinBounds)
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
            if (isWithinBounds(newX, newY))
            {
                // Перемещаем корабль в новую позицию
                ship.X = newX;
                ship.Y = newY;
            }
        }
    }
}
