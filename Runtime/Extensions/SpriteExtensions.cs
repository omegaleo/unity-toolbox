using System;
using System.Collections.Generic;
using UnityEngine;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class SpriteExtensions
    {
        /// <summary>
        /// Split a Texture2D into multiple sprites of <paramref name="width"/> x <paramref name="height"/>
        /// </summary>
        /// <param name="texture">Original Texture</param>
        /// <param name="width">Width of the sprites</param>
        /// <param name="height">Height of the sprites</param>
        /// <param name="columns">How many columns of sprites(Texture width / <paramref name="width"/>)</param>
        /// <param name="rows">How many rows of sprites(Texture height / <paramref name="height"/>)</param>
        /// <returns></returns>
        public static List<Sprite> Split(this Texture2D texture, int width, int height, int columns, int rows)
        {
            return texture.ToSprite().Split(width, height, columns, rows);
        }
        
        /// <summary>
        /// Split a Sprite into multiple sprites of <paramref name="width"/> x <paramref name="height"/>
        /// </summary>
        /// <param name="sprite">Original Sprite</param>
        /// <param name="width">Width of the sprites</param>
        /// <param name="height">Height of the sprites</param>
        /// <param name="columns">How many columns of sprites(<paramref name="sprite"/> width / <paramref name="width"/>)</param>
        /// <param name="rows">How many rows of sprites(<paramref name="sprite"/> height / <paramref name="height"/>)</param>
        /// <returns></returns>
        public static List<Sprite> Split(this Sprite sprite, int width, int height, int columns, int rows)
        {
            List<Sprite> sprites = new List<Sprite>();

            var croppedTexture = sprite.texture;
        
            for (int y = rows; y >= 0; y --)
            {
                for (int x = 0; x < columns; x ++)
                {
                    try
                    {
                        Sprite newSprite = Sprite.Create(
                            croppedTexture,
                            new Rect(x * width, y * height, width, height),
                            new Vector2(width / 2, height / 2));
 
                        sprites.Add(newSprite);
                    }
                    catch(Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }

            return sprites;
        }
    }
}