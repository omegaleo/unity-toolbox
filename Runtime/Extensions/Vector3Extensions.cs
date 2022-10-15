using System.Collections.Generic;
using UnityEngine;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Method to obtain a new Vector3 from an existing one with the data inputted
        /// Example: transform.position = objTransform.position.With(y: newY);
        /// </summary>
        /// <param name="original"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            float newX = x ?? original.x;
            float newY = y ?? original.y;
            float newZ = z ?? original.z;

            return new Vector3(newX, newY, newZ);
        }
        
        /// <summary>
        /// Finds the position closest to the given one.
        /// </summary>
        /// <param name="position">World position.</param>
        /// <param name="otherPositions">Other world positions.</param>
        /// <returns>Closest position.</returns>
        public static Vector3 GetClosest(this Vector3 position, IEnumerable<Vector3> otherPositions)
        {
            var closest = Vector3.zero;
            var shortestDistance = Mathf.Infinity;

            foreach (var otherPosition in otherPositions)
            {
                var distance = (position - otherPosition).sqrMagnitude;

                if (distance < shortestDistance)
                {
                    closest = otherPosition;
                    shortestDistance = distance;
                }
            }

            return closest;
        }
    }
}