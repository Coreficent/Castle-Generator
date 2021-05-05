namespace Coreficent.Shading
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class Normalinator : Script
    {
        private static readonly HashSet<string> processedMeshes = new HashSet<string>();

        protected bool precalculateNormal = true;
        protected float outlineDarkness = 0.0f;
        protected float outlineThickness = 0.5f;
        protected float shadingDarkness = 0.5f;
        protected float shadowThreshold = 0.5f;
        protected float shadeThreshold = 0.5f;

        private readonly string precalculatedNormalParameter = "_PrecalculatedNormal";
        private readonly string outlineDarknessParameter = "_OutlineDarkness";
        private readonly string outlineThicknessParameter = "_OutlineThickness";
        private readonly string shadingDarknessParameter = "_ShadingDarkness";
        private readonly string shadowThresholdParameter = "_ShadowThreshold";
        private readonly string shadeThresholdParameter = "_ShadeThreshold";

        private readonly List<Transform> reset = new List<Transform>();

        private readonly float mergeDistance = 0.05f;
        private readonly int texCoordinate = 3;

        protected override void Start()
        {
            string className = GetType().Name;

            if (!processedMeshes.Contains(className))
            {
                FindMesh(transform);
                processedMeshes.Add(className);
            }

            base.Start();
        }

        protected void SetShaderParameter(Transform childTransform, string parameterName, float value)
        {
            foreach (Material material in childTransform.GetComponent<MeshRenderer>().sharedMaterials)
            {
                material.SetFloat(parameterName, value);
            }
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

            if (!precalculateNormal)
            {
                Test.Debug("skipping");
                SetShaderParameter(childTransform, precalculatedNormalParameter, 0.0f);
            }
            else
            {
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
                SetShaderParameter(childTransform, precalculatedNormalParameter, 1.0f);
                reset.Add(childTransform);
            }

            SetShaderParameter(childTransform, outlineDarknessParameter, outlineDarkness);
            SetShaderParameter(childTransform, outlineThicknessParameter, outlineThickness);
            SetShaderParameter(childTransform, shadingDarknessParameter, shadingDarkness);
            SetShaderParameter(childTransform, shadowThresholdParameter, shadowThreshold);
            SetShaderParameter(childTransform, shadeThresholdParameter, shadeThreshold);
        }

        private void OnApplicationQuit()
        {
            foreach (var i in reset)
            {
                SetShaderParameter(i, precalculatedNormalParameter, 0.0f);
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
