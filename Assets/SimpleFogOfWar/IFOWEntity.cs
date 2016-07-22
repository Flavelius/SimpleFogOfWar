using UnityEngine;

namespace SimpleFogOfWar
{
    public interface IFOWEntity
    {
        /// <summary>
        /// Position of the entity
        /// </summary>
        Vector3 Position { get; }
        /// <summary>
        /// Uncovered radius around the entity
        /// </summary>
        float ViewDistance { get; }
        /// <summary>
        /// Suspends the fog influence for the entity
        /// </summary>
        bool FOWContributionDisabled { get; }
    }
}
