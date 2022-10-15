using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class TilemapExtensions
    {
        /// <summary>
        /// Get all positions of filled tiles in the <paramref name="tilemap"/>
        /// </summary>
        /// <param name="tilemap"></param>
        /// <returns></returns>
        public static IEnumerable<Vector3Int> GetTilePositions(this Tilemap tilemap)
        {
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                if (tilemap.GetTile(pos) != null)
                {
                    yield return pos;
                }
            }
        }
        
        /// <summary>
        /// Get tilemap related position of an object
        /// </summary>
        /// <param name="tilemap"></param>
        /// <returns></returns>
        public static Vector3Int GetTilePosition(this Tilemap tilemap, Vector3 position)
        {
            return tilemap.WorldToCell(position);
        }
    }
}