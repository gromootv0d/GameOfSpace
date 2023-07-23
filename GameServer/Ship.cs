using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer;

public class Ship
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
}