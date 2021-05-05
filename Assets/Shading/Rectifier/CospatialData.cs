using System.Collections.Generic;

namespace Coreficent.Shading
{
    internal class CospatialData
    {
        public List<int> CospatialIndexBuffer = new List<int>();
        public List<CospatialAccumulator> Accumulators = new List<CospatialAccumulator>();
    }
}
