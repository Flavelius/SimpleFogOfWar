using System;
using UnityEngine;

namespace SimpleFogOfWar.Test
{
    public class FOWVisibilityTester: MonoBehaviour
    {

        FogOfWarSystem fow;
        Color gizmoColor;

        void Start()
        {
            fow = FindObjectOfType<FogOfWarSystem>();
        }

        void Update()
        {
            if (!fow) return;
            var fv = fow.GetVisibility(transform.position);
            switch (fv)
            {
                case FogOfWarSystem.FogVisibility.Undetermined:
                    gizmoColor = Color.white;
                    break;
                case FogOfWarSystem.FogVisibility.Visible:
                    gizmoColor = Color.green;
                    break;
                case FogOfWarSystem.FogVisibility.Invisible:
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
