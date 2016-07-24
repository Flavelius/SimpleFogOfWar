using System;
using UnityEngine;

// ReSharper disable ConvertToConstant.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace SimpleFogOfWar.Renderers
{
    /// <summary>
    /// Accurate if objects are positioned at varying heights, but more taxing on the cpu/gpu the more objects are shadowed
    /// </summary>
    [Serializable]
    public class ProjectorFogRenderer : FOWRenderer
    {
        Projector projector;
        [SerializeField, Tooltip("The highest point where fog will be projected")]
        float clipTop = 100;
        [SerializeField, Tooltip("The lowest point where fog will be projected")]
        float clipBottom = -20;

        protected override void Initialize(FogOfWarSystem source, Texture displayTexture)
        {
            projector = new GameObject("FogProjector").AddComponent<Projector>();
            projector.transform.parent = source.transform;
            projector.transform.localPosition = source.transform.position + new Vector3(source.Size * 0.5f, clipTop, source.Size * 0.5f);
            projector.orthographic = true;
            projector.orthographicSize = source.Size * 0.5f;
            projector.farClipPlane = Mathf.Abs(clipTop - clipBottom);
            projector.transform.localRotation = Quaternion.Euler(90, 0, 0);
            projector.material = new Material(Shader.Find("Hidden/FOWProjectorShader")) { mainTexture = displayTexture };
            projector.gameObject.hideFlags = HideFlags.HideAndDontSave;
        }

        public override void SetColor(Color value)
        {
            if (projector == null) return;
            if (projector.material.HasProperty(shaderColorID))
            {
                projector.material.SetColor(shaderColorID, value);
            }
        }

        public override void SetBlur(float value)
        {
            if (projector == null) return;
            if (projector.material.HasProperty(shaderBlurID))
            {
                projector.material.SetFloat(shaderBlurID, value);
            }
        }

        // ReSharper disable once UnusedMember.Local
        void OnDestroy()
        {
            if (projector) Destroy(projector);
        }

        public override void DrawGizmos(FogOfWarSystem source)
        {
            var height = Mathf.Abs(clipTop - clipBottom);
            Gizmos.DrawWireCube(source.transform.position + new Vector3(source.Size * 0.5f, clipTop - height * 0.5f, source.Size * 0.5f), new Vector3(source.Size, height, source.Size));
        }
    }
}
