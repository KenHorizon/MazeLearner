using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MazeLearner
{
    public static class Utils
    {
        internal static byte[] ENCRYPTION_KEY = new UnicodeEncoding().GetBytes("h3y_gUyZ");
        public static List<string> WrapText(SpriteFont font, string text, float maxWidth)
        {
            List<string> lines = new List<string>();
            string[] words = text.Split(' ');

            StringBuilder currentLine = new StringBuilder();
            foreach (var word in words)
            {
                string testLine = (currentLine.Length == 0 ? word : currentLine + " " + word);
                if (font.MeasureString(testLine).X > maxWidth)
                {
                    lines.Add(currentLine.ToString());
                    currentLine.Clear();
                    currentLine.Append(word);
                }
                else
                {
                    if (currentLine.Length > 0)
                        currentLine.Append(" ");
                    currentLine.Append(word);
                }
            }

            if (currentLine.Length > 0)
                lines.Add(currentLine.ToString());

            return lines;
        }
        /// <summary>
         /// Split Text, seperate the string with any Uppercase letter (EX: SwordHand -> Sword Hand, AA -> A A)
         /// </summary>
         /// <param name="str"></param>
         /// <returns>return as a string with spaces between any Capitalized Word</returns>
        public static string SplitText(this String str)
        {
            return Regex.Replace(str, "([a-z0-9])([A-Z])", "$1 $2");
        }
        public static string Capitalize(this String str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
        public static bool IsEmpty(this String str)
        {
            return string.IsNullOrEmpty(str);
        }
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness = 2F)
        {
            DrawLine(spriteBatch, start, end, color, color, thickness);
        }
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color startColor, Color endColor, float thickness = 2F)
        {
            Vector2 delta = end - start;
            Texture2D texture = new Texture2D(Main.Graphics, 2, 1);
            texture.SetData(new[] { startColor, endColor });
            spriteBatch.Draw(texture, start, null, startColor, delta.ToAngle(), new Vector2(0, 0.5F), new Vector2(delta.Length() / 2.0F, thickness), SpriteEffects.None, 0F);
        }
        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Rectangle destRect)
        {
            
            spriteBatch.Draw(texture, destRect, Color.White);
        }
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle background, Color colorThickness, int thickness = 3)
        {
            Vector2 topLeft = new Vector2(background.Left, background.Top);
            Vector2 topRight = new Vector2(background.Right, background.Top);
            Vector2 bottomLeft = new Vector2(background.Left, background.Bottom);
            Vector2 bottomRight = new Vector2(background.Right, background.Bottom);

            spriteBatch.DrawLine(topLeft, topRight, colorThickness, thickness);
            spriteBatch.DrawLine(bottomLeft, topLeft, colorThickness, thickness);
            spriteBatch.DrawLine(topRight, bottomRight, colorThickness, thickness);
            spriteBatch.DrawLine(bottomRight, bottomLeft, colorThickness, thickness);
        }
        public static void DrawFillRectangle(this SpriteBatch spriteBatch, Rectangle background, Color colorThickness, Color backgroundColor, int thickness = 3)
        {
            spriteBatch.Draw(Main.FlatTexture, background, backgroundColor);

            Vector2 topLeft = new Vector2(background.Left, background.Top);
            Vector2 topRight = new Vector2(background.Right, background.Top);
            Vector2 bottomLeft = new Vector2(background.Left, background.Bottom);
            Vector2 bottomRight = new Vector2(background.Right, background.Bottom);

            spriteBatch.DrawLine(topLeft, topRight, colorThickness, thickness);
            spriteBatch.DrawLine(bottomLeft, topLeft, colorThickness, thickness);
            spriteBatch.DrawLine(topRight, bottomRight, colorThickness, thickness);
            spriteBatch.DrawLine(bottomRight, bottomLeft, colorThickness, thickness);
        }
        public static void DrawGradientRectangle(this SpriteBatch spriteBatch, Rectangle background,
            Color colorThicknessFrom, Color colocThicknessTo, Color backgroundColorFrom, Color backgroundColorTo, int thickness = 3)
        {
            int width = 255;
            int height = 2;
            Texture2D texture = new Texture2D(Main.Graphics, width, height);
            Color[] data = new Color[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float amount = (float)x / (width - 1);
                    data[x + y * width] = Color.Lerp(backgroundColorFrom, backgroundColorTo, amount);
                }
            }
            texture.SetData(data);
            spriteBatch.Draw(texture, background, backgroundColorTo);

            Vector2 topLeft = new Vector2(background.Left, background.Top);
            Vector2 topRight = new Vector2(background.Right, background.Top);
            Vector2 bottomLeft = new Vector2(background.Left, background.Bottom);
            Vector2 bottomRight = new Vector2(background.Right, background.Bottom);

            spriteBatch.DrawLine(topLeft, topRight, colorThicknessFrom, colocThicknessTo, thickness);
            spriteBatch.DrawLine(bottomLeft, topLeft, colorThicknessFrom, colocThicknessTo, thickness);
            spriteBatch.DrawLine(topRight, bottomRight, colorThicknessFrom, colocThicknessTo, thickness);
            spriteBatch.DrawLine(bottomRight, bottomLeft, colorThicknessFrom, colocThicknessTo, thickness);
        }
        public static float ToAngle(this Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }
        public static int ToInt(this bool value)
        {
            if (!value)
            {
                return 0;
            }
            return 1;
        }
        public static bool IntersectsCentered(this Rectangle a, Rectangle b)
        {
            float aCenterX = a.X + a.Width * 0.5f;
            float aCenterY = a.Y + a.Height * 0.5f;

            float bCenterX = b.X + b.Width * 0.5f;
            float bCenterY = b.Y + b.Height * 0.5f;

            return Math.Abs(aCenterX - bCenterX) * 2 < (a.Width + b.Width) && Math.Abs(aCenterY - bCenterY) * 2 < (a.Height + b.Height);
        }
        public static Vector2 MoveTowards(Vector2 vector2, Vector2 target, float maxDelta)
        {
            Vector2 delta = target - vector2;
            float distance = delta.Length();

            if (distance <= maxDelta || distance == 0f)
                return target;

            return vector2 + delta / distance * maxDelta;
        }
    }
}
