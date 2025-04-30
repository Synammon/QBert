using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBert
{
    public class Helpers
    {
        public static bool BoundingCircleCollides(Vector2 positionA, Texture2D textureA, Vector2 positionB, Texture2D textureB)
        {
            Vector2 originA = new Vector2(textureA.Width / 2, textureA.Height / 2);
            Vector2 originB = new Vector2(textureB.Width / 2, textureB.Height / 2);

            float distance = Vector2.Distance(
                positionA + originA,
                positionB + originB);
            if (distance <=
                textureA.Width / 2 +
                textureB.Width / 2)
            {
                return true;
            }

            return false;
        }

        public static bool PerPixel(Rectangle boundingBoxA, Color[] colorDetailsA, Rectangle boundingBoxB, Color[] colorDetailsB)
        {
            // Find the bounds of the intersection rectangle
            int top = Math.Max(boundingBoxA.Top, boundingBoxB.Top);
            int bottom = Math.Min(boundingBoxA.Bottom, boundingBoxB.Bottom);
            int left = Math.Max(boundingBoxA.Left, boundingBoxB.Left);
            int right = Math.Min(boundingBoxA.Right, boundingBoxB.Right);

            // Iterate through each pixel in the intersection rectangle
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Calculate the corresponding pixel positions in each texture
                    int indexA = (x - boundingBoxA.Left) + (y - boundingBoxA.Top) * boundingBoxA.Width;
                    int indexB = (x - boundingBoxB.Left) + (y - boundingBoxB.Top) * boundingBoxB.Width;

                    // Get the colors of the overlapping pixels
                    Color colorA = colorDetailsA[indexA];
                    Color colorB = colorDetailsB[indexB];

                    // Check if both pixels are not fully transparent
                    if (colorA.A > 0 && colorB.A > 0)
                    {
                        return true; // Collision detected
                    }
                }
            }

            return false; // No collision detected
        }

        public static Vector2 CalcDirection(Vector2 origin1, Vector2 origin2)
        {
            Vector2 direction = origin2 - origin1;
            
            direction.Normalize();
            return direction;
        }

        public static Vector2 CalcDirection(Rectangle rectangle, Vector2 origin2)
        {
            Vector2 direction = new Vector2(rectangle.X, rectangle.Y) - origin2;

            direction.Normalize();
            return direction;
        }
    }
}
