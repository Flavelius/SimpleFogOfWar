using System;
using UnityEngine;

namespace SimpleFogOfWar.Test
{
    public class FOWVisibilityTester: MonoBehaviour
    {

        FogOfWar fow;
        Color gizmoColor;

        void Start()
        {
            fow = FindObjectOfType<FogOfWar>();
        }

        void Update()
        {
            if (!fow) return;
            var fv = fow.GetVisibility(transform.position);
            switch (fv)
            {
                case FogOfWar.FogVisibility.Undetermined:
                    gizmoColor = Color.white;
                    break;
                case FogOfWar.FogVisibility.Visible:
                    gizmoColor = Color.green;
                    break;
                case FogOfWar.FogVisibility.Invisible:
                    gizmoColor = Color.red;
                    break;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, 1f);
        }

    }
}
