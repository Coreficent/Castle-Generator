namespace Coreficent.Module
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public enum Face
    {
        Dirt,
        Grass,
        Air,

        Wall,
        WallSpace,
        WallCornerCenterSouth,
        WallCornerCenterEast,
        WallCornerSouthSeast,
        WallCornerEastSeast,

        Foundation,

        Tower,
    }

    public enum Direction
    {
        North,
        East,
        South,
        West,
        Top,
        Bottom,
        Invalid,
    }

    public class ModuleBase : Script
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
                case Direction.Top:
                    return Direction.Bottom;
                case Direction.Bottom:
                    return Direction.Top;
                default:
                    return Direction.Invalid;
            }
        }

        protected HashSet<Face> north = new HashSet<Face>();
        protected HashSet<Face> east = new HashSet<Face>();
        protected HashSet<Face> south = new HashSet<Face>();
        protected HashSet<Face> west = new HashSet<Face>();

        protected HashSet<Face> top = new HashSet<Face>();
        protected HashSet<Face> bottom = new HashSet<Face>();

        protected int weight = 1024;

        public int Weight
        {
            get
            {
                return weight;
            }
        }

        public HashSet<Face> NorthSet
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

        public HashSet<Face> EastSet
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

        public HashSet<Face> SouthSet
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

        public HashSet<Face> WestSet
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

        public HashSet<Face> TopSet
        {
            get
            {
                return top;
            }
        }

        public HashSet<Face> BottomSet
        {
            get
            {
                return bottom;
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

        public bool Visible
        {
            set
            {
                foreach (Renderer i in GetComponentsInChildren<Renderer>())
                {
                    i.enabled = value;
                }
            }
        }

        protected override void Start()
        {
            name = ToString();

            if (NorthSet.Count == 0)
            {
                Test.Warn(name, "north is empty");
            }
            if (WestSet.Count == 0)
            {
                Test.Warn(name, "west is empty");
            }
            if (SouthSet.Count == 0)
            {
                Test.Warn(name, "south is empty");
            }
            if (EastSet.Count == 0)
            {
                Test.Warn(name, "east is empty");
            }
            if (TopSet.Count == 0)
            {
                Test.Warn(name, "top is empty");
            }
            if (BottomSet.Count == 0)
            {
                Test.Warn(name, "bottom is empty");
            }

            transform.Find("display").transform.localEulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);

            base.Start();
        }

        public override string ToString()
        {
            string delimiter = " - ";
            return GetType().Name + delimiter + Orientation;
        }

        public override bool Equals(object obj)
        {
            ModuleBase other = obj as ModuleBase;

            if (other == null)
            {
                return false;
            }

            if (GetType().Name != other.GetType().Name)
            {
                return false;
            }

            return NorthSet.SetEquals(other.NorthSet) && WestSet.SetEquals(other.WestSet) && SouthSet.SetEquals(other.SouthSet) && EastSet.SetEquals(other.EastSet) && TopSet.SetEquals(other.TopSet) && BottomSet.SetEquals(other.BottomSet);
        }

        public override int GetHashCode()
        {
            int hash = 17;

            foreach (var i in NorthSet)
            {
                hash = hash * 23 + i.GetHashCode();
            }

            foreach (var i in WestSet)
            {
                hash = hash * 23 + i.GetHashCode();
            }

            foreach (var i in SouthSet)
            {
                hash = hash * 23 + i.GetHashCode();
            }

            foreach (var i in EastSet)
            {
                hash = hash * 23 + i.GetHashCode();
            }

            foreach (var i in TopSet)
            {
                hash = hash * 23 + i.GetHashCode();
            }

            foreach (var i in BottomSet)
            {
                hash = hash * 23 + i.GetHashCode();
            }

            return hash;
        }
    }
}
