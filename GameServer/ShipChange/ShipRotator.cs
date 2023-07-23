using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.ShipChange
{
    public static class ShipRotator
    {
        public static Direction GetCounterclockwiseDirection(Direction currentDirection)
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

        public static Direction GetClockwiseDirection(Direction currentDirection)
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
    }

}
