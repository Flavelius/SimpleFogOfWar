using UnityEngine;

namespace SimpleFogOfWar
{
    public class FogOfWarInfluence : MonoBehaviour
    {

        /// <summary>
        /// Uncovered radius around the entity
        /// </summary>
        public float ViewDistance = 10;
        /// <summary>
        /// Suspends the fog influence for the entity
        /// </summary>
        public bool Suspended;

        void Start()
        {
            FogOfWarSystem.AddInfluence(this);
        }

        void OnDestroy()
        {
            FogOfWarSystem.RemoveInfluence(this);
        }

    }
}
