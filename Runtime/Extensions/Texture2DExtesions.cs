using UnityEngine;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class Texture2DExtesions
    {
        /// <summary>
        /// Generates a <paramref name="width"/> x <paramref name="height"/> Texture2D filled with a <paramref name="color"/>
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Texture2D GenerateTexture2D(this Color color, int width = 32, int height = 32)
        {
            // Create the texture2D
            var targetTexture= new Texture2D(width, height);
            
            // Get all the pixels to fill with a color
            var fillColorArray =  targetTexture.GetPixels();
 
            // Loop through the pixels and assign the color
            for(var i = 0; i < fillColorArray.Length; ++i)
            {
                fillColorArray[i] = color;
            }
  
            // Set the pixels to the texture
            targetTexture.SetPixels( fillColorArray );
   
            // Save the change to the texture
            targetTexture.Apply();

            return targetTexture;
        }
        
        /// <summary>
        /// Convert a Texture2D into a Sprite
        /// </summary>
        /// <param name="texture2D"></param>
        /// <returns></returns>
        public static Sprite ToSprite(this Texture2D texture2D)
        {
            return Sprite.Create(texture2D, new Rect(0,0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        }
        
        /// <summary>
        /// Convert the sprite into a Texture that only has the sprite we want, mostly to be used with Spritesheets
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public static Texture2D ToTexture(this Sprite sprite)
        {
            // If the sprite is already the same size as it's base texture, return the texture
            if (sprite.rect.width == sprite.texture.width && sprite.rect.height == sprite.texture.height)
                return sprite.texture;
        
            // Else, we will crop the texture to match the size of the sprite rect, example, a sprite inside a spritesheet
            // Create the new texture
            var croppedTexture = new Texture2D( (int)sprite.rect.width, (int)sprite.rect.height );
            
            // Obtain all the pixels that are inside the sprite rect's area
            var pixels = sprite.texture.GetPixels(  (int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height,  0);
            
            // Set the pixels we obtained into the new texture
            croppedTexture.SetPixels( pixels );

            // Save the changes
            croppedTexture.Apply();

            return croppedTexture;
        }
    }
}