using UnityEngine;

namespace SimpleFogOfWar.Test
{
    public class FOWTestEntity: MonoBehaviour, IFOWEntity
    {
        [SerializeField] float _viewDistance = 0.5f;

        [SerializeField] bool suspendFOWInfluence = false;

        public Vector3 Position {
            get { return transform.position; } 
        }
        public float ViewDistance {
            get { return _viewDistance; }
        }
        public bool FOWContributionDisabled {
            get { return suspendFOWInfluence; }
        }

        FogOfWar registeredDrawer;

        void Start()
        {
            registeredDrawer = FindObjectOfType<FogOfWar>();
            if (registeredDrawer)
            {
                registeredDrawer.RegisterEntity(this);
            }
            else
            {
                Destroy(this);
            }
        }

        void OnDestroy()
        {
            if (registeredDrawer) registeredDrawer.RemoveEntity(this);
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _viewDistance*0.5f);
        }

    }
}
