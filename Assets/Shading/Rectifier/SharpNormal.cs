namespace Coreficent.Shading
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class SharpNormal : MonoBehaviour
    {
        [SerializeField]
        private float _mergeDistance = 0.01f;

        private readonly int _texCoord = 3;

        private bool UsePrecalculatedNormal
        {
            set
            {
                foreach (Material material in GetComponent<MeshRenderer>().sharedMaterials)
                {
                    material.SetFloat("_PrecalculatedNormal", value ? 1.0f : 0.0f);
                }
            }
        }

        private void Start()
        {
            Mesh mesh = GetComponent<MeshFilter>().mesh;

            if (mesh == null)
            {
                mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
            }

            Vector3[] vertexBuffer = mesh.vertices;
            int[] indexBuffer = mesh.triangles;

            CospatialData cospatialData = CalculateCospatialIndexBuffer(vertexBuffer);
            List<int> cospatialIndexBuffer = cospatialData.CospatialIndexBuffer;
            List<CospatialAccumulator> accumulators = cospatialData.Accumulators;

            for (var i = 0; i < indexBuffer.Length / 3; ++i)
            {
                int vertexOffset = i * 3;

                int indexX = indexBuffer[vertexOffset + 0];
                int indexY = indexBuffer[vertexOffset + 1];
                int indexZ = indexBuffer[vertexOffset + 2];

                Vector3 vertexA = vertexBuffer[indexX];
                Vector3 vertexB = vertexBuffer[indexY];
                Vector3 vertexC = vertexBuffer[indexZ];

                Vector3 normal = Vector3.Cross(vertexB - vertexA, vertexC - vertexA).normalized;
                Vector3 weight = new Vector3(Vector3.Angle(vertexB - vertexA, vertexC - vertexA), Vector3.Angle(vertexC - vertexB, vertexA - vertexB), Vector3.Angle(vertexA - vertexC, vertexB - vertexC));

                accumulators[cospatialIndexBuffer[indexX]].Normal += normal * weight.x;
                accumulators[cospatialIndexBuffer[indexY]].Normal += normal * weight.y;
                accumulators[cospatialIndexBuffer[indexZ]].Normal += normal * weight.z;
            }

            Vector3[] normals = new Vector3[vertexBuffer.Length];

            for (var i = 0; i < normals.Length; ++i)
            {
                normals[i] = accumulators[cospatialIndexBuffer[i]].Normal.normalized;
            }

            mesh.SetUVs(_texCoord, normals);
            UsePrecalculatedNormal = true;
        }

        private void OnApplicationQuit()
        {
            UsePrecalculatedNormal = false;
        }

        private CospatialData CalculateCospatialIndexBuffer(Vector3[] vertexBuffer)
        {
            CospatialData cospatial = new CospatialData
            {
                CospatialIndexBuffer = new List<int>(Enumerable.Range(0, vertexBuffer.Length).Select(a => 0)),
                Accumulators = new List<CospatialAccumulator>()
            };

            CospatialAccumulator inspector = new CospatialAccumulator();

            for (var i = 0; i < vertexBuffer.Length; ++i)
            {
                inspector.Position = vertexBuffer[i];

                int vertexIndex = cospatial.Accumulators.FindIndex(a => Vector3.Distance(a.Position, inspector.Position) <= _mergeDistance);

                if (vertexIndex == -1)
                {
                    cospatial.CospatialIndexBuffer[i] = cospatial.Accumulators.Count;
                    cospatial.Accumulators.Add(new CospatialAccumulator()
                    {
                        Position = vertexBuffer[i],
                        Normal = Vector3.zero,
                    });
                }
                else
                {
                    cospatial.CospatialIndexBuffer[i] = vertexIndex;
                }
            }

            return cospatial;
        }
    }
}
