using UnityEngine;

namespace SimpleFogOfWar.Renderers
{
    public abstract class FOWRenderer: ScriptableObject
    {
        protected abstract void Initialize(FogOfWar source, Texture displayTexture);
        public virtual void Render(Vector3 basePosition) { }

        public virtual void DrawGizmos(FogOfWar source)
        {
            var box = new Vector3(source.Size, 0, source.Size);
            Gizmos.DrawWireCube(new Vector3(source.transform.position.x + box.x * 0.5f, 0, source.transform.position.z + box.z * 0.5f), box);
        }

        public abstract void SetBlur(float value);
        public abstract void SetColor(Color value);

        protected int shaderBlurID;
        protected int shaderColorID;

        public void Init(FogOfWar source, Texture displayTexture)
        {
            shaderBlurID = Shader.PropertyToID("_Blur");
            shaderColorID = Shader.PropertyToID("_Color");
            Initialize(source, displayTexture);
        }
    }
}