namespace Coreficent.Generation
{
    using Coreficent.Utility;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class World
    {
        Dictionary<string, SuperPosition> map = new Dictionary<string, SuperPosition>();

        public World(SuperPosition superPosition, SuperPosition emptyPosition)
        {
            int size = 7;
            for (int x = 0; x < size; ++x)
            {
                for (int y = 0; y < size; ++y)
                {
                    if (x == 0 || y == 0 || x == size - 1 || y == size - 1)
                    {
                        SuperPosition position = UnityEngine.Object.Instantiate(emptyPosition);
                        position.transform.position = new Vector3(x, y, 0.0f);
                        map.Add("" + x + "::" + y, position);
                    }
                    else
                    {
                        SuperPosition position = UnityEngine.Object.Instantiate(superPosition);
                        position.transform.position = new Vector3(x, y, 0.0f);
                        map.Add("" + x + "::" + y, position);
                    }

                }
            }
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

                    if (position.Entropy < entropy)
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
