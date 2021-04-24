namespace Coreficent.Tile
{
    using Coreficent.Utility;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TileBase : Script
    {
        public enum Socket
        {
            GrassToGrass,
            GroundPathEnd,
        }

        private enum Direction
        {
            Up,
            Right,
            Down,
            Left,
            Invalid,
        }

        protected List<Socket> north = new List<Socket>();
        protected List<Socket> east = new List<Socket>();
        protected List<Socket> south = new List<Socket>();
        protected List<Socket> west = new List<Socket>();

        public List<Socket> North
        {
            get
            {
                Direction direction = Orientation;

                switch (direction)
                {
                    case Direction.Up:
                        return north;
                    case Direction.Right:
                        return east;
                    case Direction.Down:
                        return south;
                    case Direction.Left:
                        return west;
                    default:
                        Test.Warn("unexpected direction");
                        return null;
                }
            }
        }

        public List<Socket> East
        {
            get
            {
                Direction direction = Orientation;

                switch (direction)
                {
                    case Direction.Up:
                        return east;
                    case Direction.Right:
                        return south;
                    case Direction.Down:
                        return west;
                    case Direction.Left:
                        return north;
                    default:
                        Test.Warn("unexpected direction");
                        return null;
                }
            }
        }

        public List<Socket> South
        {
            get
            {
                Direction direction = Orientation;

                switch (direction)
                {
                    case Direction.Up:
                        return south;
                    case Direction.Right:
                        return west;
                    case Direction.Down:
                        return north;
                    case Direction.Left:
                        return east;
                    default:
                        Test.Warn("unexpected direction");
                        return null;
                }
            }
        }

        public List<Socket> West
        {
            get
            {
                Direction direction = Orientation;

                switch (direction)
                {
                    case Direction.Up:
                        return west;
                    case Direction.Right:
                        return north;
                    case Direction.Down:
                        return east;
                    case Direction.Left:
                        return south;
                    default:
                        Test.Warn("unexpected direction");
                        return null;
                }
            }
        }

        private Direction Orientation
        {
            get
            {
                float angle = transform.localEulerAngles.y % 360.0f;

                if (Mathf.Approximately(angle, 0.0f) || Mathf.Approximately(angle, 360.0f) || Mathf.Approximately(angle, -0.0f) || Mathf.Approximately(angle, -360.0f))
                {
                    return Direction.Up;
                }
                if (Mathf.Approximately(angle, 90.0f) || Mathf.Approximately(angle, -270.0f))
                {
                    return Direction.Right;
                }
                if (Mathf.Approximately(angle, 180.0f) || Mathf.Approximately(angle, -180.0f))
                {
                    return Direction.Down;
                }
                if (Mathf.Approximately(angle, 270.0f) || Mathf.Approximately(angle, -90.0f))
                {
                    return Direction.Left;
                }

                Test.Warn("invalid direction found");
                return Direction.Invalid;
            }
        }

    }
}
