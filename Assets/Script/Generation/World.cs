namespace Coreficent.Generation
{
    using Coreficent.Setting;
    using Coreficent.Utility;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class World
    {
        private Dictionary<string, SuperPosition> map = new Dictionary<string, SuperPosition>();

        public World(SuperPosition superPosition, SuperPosition emptyPosition)
        {
            for (int x = 0; x < Tuning.Width; ++x)
            {
                for (int y = 0; y < Tuning.Height; ++y)
                {
                    if (x == 0 || y == 0 || x == Tuning.Width - 1 || y == Tuning.Height - 1)
                    {
                        SuperPosition position = UnityEngine.Object.Instantiate(emptyPosition);
                        position.transform.position = new Vector3(x, y, 0.0f);
                        map.Add(Hash(x, y), position);
                    }
                    else
                    {
                        SuperPosition position = UnityEngine.Object.Instantiate(superPosition);
                        position.transform.position = new Vector3(x, y, 0.0f);
                        map.Add(Hash(x, y), position);
                    }
                }
            }
        }

        public SuperPosition Find(int x, int y)
        {
            if (!map.ContainsKey(Hash(x, y)))
            {
                Test.Warn("position not found", Hash(x, y));
                return null;
            }

            return map[Hash(x, y)];
        }

        private string Hash(int x, int y)
        {
            string result = "" + x + "::" + y;
            return result;
        }

        public bool Collapsed
        {
            get
            {
                foreach (KeyValuePair<string, SuperPosition> entry in map)
                {
                    if (!entry.Value.Collapsed)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public SuperPosition Next
        {
            get
            {
                int entropy = int.MaxValue;
                SuperPosition result = null;

                foreach (KeyValuePair<string, SuperPosition> entry in map)
                {
                    SuperPosition position = entry.Value;

                    if (!position.Collapsed && position.Entropy < entropy)
                    {
                        entropy = position.Entropy;
                        result = position;
                    }
                }

                if (result == null)
                {
                    Test.Warn("unexpected null position for uncollapsed world");
                }

                return result;
            }
        }
    }
}
