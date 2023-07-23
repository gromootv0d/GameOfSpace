using GameServer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer;

public class Ship : IMovable, IRotatable
{
    public int X { get; set; } // Координата X корабля
    public int Y { get; set; } // Координата Y корабля
    public Direction FacingDirection { get; set; } // Направление корабля

    public Ship(int initialX, int initialY, Direction initialDirection)
    {
        X = initialX;
        Y = initialY;
        FacingDirection = initialDirection;
    }

    public void PlaceShip(int x, int y, Direction direction)
    {
        X = x;
        Y = y;
        FacingDirection = direction;
    }
    private bool IsWithinBounds(int targetX, int targetY)
    {
        return targetX >= 0 && targetX < 6 && targetY >= 0 && targetY < 6;
    }
    public void Rotate(RotationDirection rotationDirection)
    {
        switch (rotationDirection)
        {
            case RotationDirection.Counterclockwise:
                // Логика поворота корабля против часовой стрелки
                break;
            case RotationDirection.Clockwise:
                // Логика поворота корабля по часовой стрелке
                break;
            default:
                break;
        }
    }


    public void Move(Direction direction)
    {
        // Получаем текущие координаты корабля
        int currentX = X;
        int currentY = Y;

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
            X = newX;
            Y = newY;
        }
    }
}