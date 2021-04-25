namespace Coreficent.Tile
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public enum Socket
    {
        Grass,
        Path,
    }

    public class TileBase : Script
    {
        public static Direction InverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.East:
                    return Direction.West;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                default:
                    return Direction.Invalid;
            }
        }



        public enum Direction
        {
            North,
            East,
            South,
            West,
            Invalid,
        }

        protected HashSet<Socket> north = new HashSet<Socket>();
        protected HashSet<Socket> east = new HashSet<Socket>();
        protected HashSet<Socket> south = new HashSet<Socket>();
        protected HashSet<Socket> west = new HashSet<Socket>();

        public HashSet<Socket> North
        {
            get
            {
                Direction direction = Orientation;

                switch (direction)
                {
                    case Direction.North:
                        return north;
                    case Direction.East:
                        return east;
                    case Direction.South:
                        return south;
                    case Direction.West:
                        return west;
                    default:
                        Test.Warn("unexpected direction");
                        return null;
                }
            }
        }

        public HashSet<Socket> East
        {
            get
            {
                Direction direction = Orientation;

                switch (direction)
                {
                    case Direction.North:
                        return east;
                    case Direction.East:
                        return south;
                    case Direction.South:
                        return west;
                    case Direction.West:
                        return north;
                    default:
                        Test.Warn("unexpected direction");
                        return null;
                }
            }
        }

        public HashSet<Socket> South
        {
            get
            {
                Direction direction = Orientation;

                switch (direction)
                {
                    case Direction.North:
                        return south;
                    case Direction.East:
                        return west;
                    case Direction.South:
                        return north;
                    case Direction.West:
                        return east;
                    default:
                        Test.Warn("unexpected direction");
                        return null;
                }
            }
        }

        public HashSet<Socket> West
        {
            get
            {
                Direction direction = Orientation;

                switch (direction)
                {
                    case Direction.North:
                        return west;
                    case Direction.East:
                        return north;
                    case Direction.South:
                        return east;
                    case Direction.West:
                        return south;
                    default:
                        Test.Warn("unexpected direction");
                        return null;
                }
            }
        }

        public Direction Orientation
        {
            get
            {
                float angle = transform.localEulerAngles.z % 360.0f;

                if (Mathf.Approximately(angle, 0.0f) || Mathf.Approximately(angle, 360.0f) || Mathf.Approximately(angle, -0.0f) || Mathf.Approximately(angle, -360.0f))
                {
                    return Direction.North;
                }
                if (Mathf.Approximately(angle, 90.0f) || Mathf.Approximately(angle, -270.0f))
                {
                    return Direction.East;
                }
                if (Mathf.Approximately(angle, 180.0f) || Mathf.Approximately(angle, -180.0f))
                {
                    return Direction.South;
                }
                if (Mathf.Approximately(angle, 270.0f) || Mathf.Approximately(angle, -90.0f))
                {
                    return Direction.West;
                }

                Test.Warn("invalid direction found");
                return Direction.Invalid;
            }
        }

        public override string ToString()
        {
            string delimiter = ", ";
            return "name" + GetType().Name + delimiter + "orientation" + delimiter + Orientation;
        }

        public override bool Equals(object obj)
        {
            TileBase other = obj as TileBase;

            if (other == null)
            {
                return false;
            }

            return North.SetEquals(other.North) && West.SetEquals(other.West) && South.SetEquals(other.South) && East.SetEquals(other.East);
        }

        public override int GetHashCode()
        {
            int hash = 17;

            foreach (var i in North)
            {
                hash = hash * 23 + i.GetHashCode();
            }

            foreach (var i in West)
            {
                hash = hash * 23 + i.GetHashCode();
            }

            foreach (var i in South)
            {
                hash = hash * 23 + i.GetHashCode();
            }

            foreach (var i in East)
            {
                hash = hash * 23 + i.GetHashCode();
            }

            return hash;
        }
    }
}