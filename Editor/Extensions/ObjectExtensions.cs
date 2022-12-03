using System.Collections.Generic;
using UnityEditor;

namespace OmegaLeo.Toolbox.Editor.Omega_Leo_Toolbox.Editor.Extensions
{
    public static class ObjectExtensions
    {
        public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            List<T> assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            for( int i = 0; i < guids.Length; i++ )
            {
                string assetPath = AssetDatabase.GUIDToAssetPath( guids[i] );
                T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
                if( asset != null )
                {
                    assets.Add(asset);
                }
            }
            return assets;
        }
    }
}