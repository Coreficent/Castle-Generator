namespace Coreficent.Shading
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class Normalinator : Script
    {
        private static HashSet<string> processedMeshes = new HashSet<string>();

        private readonly float mergeDistance = 0.01f;
        private readonly int texCoordinate = 3;
        private List<Transform> reset = new List<Transform>();



        private void UsePrecalculatedNormal(Transform childTransform, bool value)
        {
            foreach (Material material in childTransform.GetComponent<MeshRenderer>().sharedMaterials)
            {
                material.SetFloat("_PrecalculatedNormal", value ? 1.0f : 0.0f);
            }
        }

        protected override void Start()
        {
            FindMesh(transform);

            base.Start();
        }

        private void FindMesh(Transform childTransform)
        {
            foreach (Transform i in childTransform)
            {
                FindMesh(i);
            }
            Rectify(childTransform);
        }

        private void Rectify(Transform childTransform)
        {
            if (!processedMeshes.Contains(childTransform.name))
            {



                MeshFilter meshFilter = childTransform.GetComponent<MeshFilter>();

                if (meshFilter == null)
                {
                    return;
                }

                Mesh mesh = meshFilter.mesh;

                if (mesh == null)
                {
                    SkinnedMeshRenderer skinnedMeshRenderer = childTransform.GetComponent<SkinnedMeshRenderer>();

                    if (skinnedMeshRenderer == null)
                    {
                        return;
                    }

                    mesh = skinnedMeshRenderer.sharedMesh;
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

                mesh.SetUVs(texCoordinate, normals);
                UsePrecalculatedNormal(childTransform, true);
                reset.Add(childTransform);

                processedMeshes.Add(childTransform.name);
            }
        }

        private void OnApplicationQuit()
        {
            foreach (var i in reset)
            {
                UsePrecalculatedNormal(i, false);
            }

            processedMeshes.Clear();
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

                int vertexIndex = cospatial.Accumulators.FindIndex(a => Vector3.Distance(a.Position, inspector.Position) <= mergeDistance);

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
