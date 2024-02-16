using System;
using System.Collections.Generic;
using UnityEngine;
using UnityFlow.DocumentationHelper.Library.Documentation;

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
        [Documentation(nameof(Split), "Split a Texture2D into multiple sprites of width x height", new []
        {
            "texture - Original Texture",
            "width - Width of the sprite",
            "height - Height of the sprites",
            "columns - How many columns of sprites(Texture's width / width passed as parameter)",
            "rows - How many rows of sprites(Texture's height / height passed as parameter)",
        })]
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

            for (int y = rows; y >= 0; y--)
            {
                for (int x = 0; x < columns; x++)
                {
                    try
                    {
                        Sprite newSprite = Sprite.Create(
                            croppedTexture,
                            new Rect(x * width, y * height, width, height),
                            new Vector2(width / 2, height / 2));

                        sprites.Add(newSprite);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }

            return sprites;
        }

        /// <summary>
        /// Generates a stroke effect around the given sprite.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="strokeColor"></param>
        /// <param name="outlineWidth"></param>
        /// <returns></returns>
        [Documentation(nameof(GenerateStroke), "Generates a stroke effect around the given sprite.")]
        public static Sprite GenerateStroke(this Sprite sprite, Color strokeColor, float outlineWidth)
        {
            // Get the texture of the original sprite
            Texture2D originalTexture = sprite.texture;

            // Create a new texture for the stroke
            Texture2D strokeTexture =
                new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false);
            strokeTexture.filterMode = FilterMode.Point;
            // Get the pixel data from the original texture
            Color32[] originalPixels = originalTexture.GetPixels32();

            // Clone the original pixels into the stroke texture
            Color32[] strokePixels = originalPixels.Clone() as Color32[];

            int width = originalTexture.width;
            int height = originalTexture.height;

            // Calculate the number of iterations based on the outline width
            int iterations = Mathf.CeilToInt(outlineWidth);

            // Apply the stroke effect to the stroke pixels
            for (int i = 0; i < iterations; i++)
            {
                float offset = (i + 1) * outlineWidth / iterations;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (IsEdgePixel(x, y, width, height, originalPixels))
                        {
                            for (int j = -i; j <= i; j++)
                            {
                                int offsetX = x + j;
                                int offsetY = y + j;

                                if (offsetX >= 0 && offsetX < width && offsetY >= 0 && offsetY < height)
                                {
                                    strokePixels[offsetY * width + offsetX] = strokeColor;
                                }

                                offsetX = x - j;
                                offsetY = y + j;

                                if (offsetX >= 0 && offsetX < width && offsetY >= 0 && offsetY < height)
                                {
                                    strokePixels[offsetY * width + offsetX] = strokeColor;
                                }
                            }
                        }
                    }
                }
            }

            // Apply the modified pixels to the stroke texture
            strokeTexture.SetPixels32(strokePixels);
            strokeTexture.Apply();

            // Create a new sprite from the stroke texture
            Rect rect = sprite.rect;
            Sprite strokeSprite = Sprite.Create(strokeTexture, rect, sprite.pivot);

            return strokeSprite;
        }

        private static bool IsEdgePixel(int x, int y, int width, int height, Color32[] pixels)
        {
            if (pixels[y * width + x].a < 1)
            {
                return false;
            }

            bool isEdge = false;

            // Check if the pixel is on the edge of the sprite
            if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
            {
                isEdge = true;
            }
            else
            {
                // Check if any of the adjacent pixels are transparent
                if (pixels[(y - 1) * width + x].a < 1 ||
                    pixels[(y + 1) * width + x].a < 1 ||
                    pixels[y * width + (x - 1)].a < 1 ||
                    pixels[y * width + (x + 1)].a < 1)
                {
                    isEdge = true;
                }
            }

            return isEdge;
        }
    }
}