using System.Collections.Generic;
using UnityEngine;

// ReSharper disable ConvertToConstant.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace SimpleFogOfWar.Renderers
{
    /// <summary>
    /// Easier on the cpu/gpu if alot of small objects are shadowed, but increasingly inaccurate the higher or lower some objects are positioned relative to the ground (y=0)
    /// </summary>
    public class SeeThroughFogRenderer : FOWRenderer
    {
        Mesh displayMesh;
        Material displayMat;

        void GenerateDisplayMesh(float size)
        {
            displayMesh = new Mesh();
            displayMesh.SetVertices(new List<Vector3>
            {
                new Vector3(0, 0, 0),
                new Vector3(size, 0, 0),
                new Vector3(size, 0, size),
                new Vector3(0, 0, size)
            });
            displayMesh.uv = new[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1)
            };
            var baseNor = Vector3.up;
            displayMesh.normals = new[]
            {
                baseNor,
                baseNor,
                baseNor,
                baseNor
            };
            displayMesh.SetTriangles(new List<int>
            {
                0,
                1,
                2,
                2,
                3,
                0
            }, 0);
            displayMesh.RecalculateBounds();
        }

        public override void SetColor(Color value)
        {
            if (displayMat == null) return;
            if (displayMat.HasProperty(shaderColorID)) displayMat.SetColor(shaderColorID, value);
        }

        public override void SetBlur(float value)
        {
            if (displayMat == null) return;
            if (displayMat.HasProperty(shaderBlurID)) displayMat.SetFloat(shaderBlurID, value);
        }

        public override void Render(Vector3 basePosition)
        {
            Graphics.DrawMesh(displayMesh, basePosition, Quaternion.identity, displayMat, 0);
        }

        protected override void Initialize(FogOfWarSystem source, Texture displayTexture)
        {
            displayMat = new Material(Shader.Find("Hidden/FOWSeeThroughShader")) { mainTexture = displayTexture };
            GenerateDisplayMesh(source.Size);
        }
    }
}