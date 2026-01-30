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
        public static bool Empty<T>(this List<T> str)
        {
            return str.Count <= 0 || str == null;
        }

        /// <summary>
        /// Converts a comma separated string to an int array
        /// </summary>
        /// <param name="src">The comma separated string source</param>
        /// <returns>The parsed int array</returns>
        public static int[] AsIntArray(this string src)
        {
            return src.Select(x => int.Parse(x.ToString().Length == 0 ? "-1" : x.ToString())).ToArray();
        }
        /// <summary>
        /// Converts a string array whose values are actually all numbers to an int array
        /// </summary>
        /// <param name="src">The string array</param>
        /// <returns>The parsed int array</returns>
        public static int[] AsIntArray(this string[] src)
        {
            return src.Select(x => int.Parse(x.Length == 0 ? "-1" : x)).ToArray();
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
        public static void DrawMessageBox(this SpriteBatch spriteBatch, Texture2D texture, Rectangle destination, Color color, int scale)
        {
            // SRC: PSDK's logic of rendering message box!
            int width = texture.Width;
            int height = texture.Height;

            Rectangle srcTL = new Rectangle(0, 0, scale, scale);
            Rectangle srcT = new Rectangle(scale, 0, width - scale * 2, scale);
            Rectangle srcTR = new Rectangle(width - scale, 0, scale, scale);

            Rectangle srcL = new Rectangle(0, scale, scale, height - scale * 2);
            Rectangle srcC = new Rectangle(scale, scale, width - scale * 2, height - scale * 2);
            Rectangle srcR = new Rectangle(width - scale, scale, scale, height - scale * 2);

            Rectangle srcBL = new Rectangle(0, height - scale, scale, scale);
            Rectangle srcB = new Rectangle(scale, height - scale, width - scale * 2, scale);
            Rectangle srcBR = new Rectangle(width - scale, height - scale, scale, scale);

            Rectangle dstTL = new Rectangle(destination.Left, destination.Top, scale, scale);
            Rectangle dstT = new Rectangle(destination.Left + scale, destination.Top, destination.Width - scale * 2, scale);
            Rectangle dstTR = new Rectangle(destination.Right - scale, destination.Top, scale, scale);

            Rectangle dstL = new Rectangle(destination.Left, destination.Top + scale,scale, destination.Height - scale * 2);
            Rectangle dstC = new Rectangle(destination.Left + scale, destination.Top + scale, destination.Width - scale * 2, destination.Height - scale * 2);
            Rectangle dstR = new Rectangle(destination.Right - scale, destination.Top + scale,scale, destination.Height - scale * 2);

            Rectangle dstBL = new Rectangle(destination.Left, destination.Bottom - scale, scale, scale);
            Rectangle dstB = new Rectangle(destination.Left + scale, destination.Bottom - scale,destination.Width - scale * 2, scale);
            Rectangle dstBR = new Rectangle(destination.Right - scale, destination.Bottom - scale,scale, scale);

            spriteBatch.Draw(texture, dstTL, srcTL, color);
            spriteBatch.Draw(texture, dstT, srcT, color);
            spriteBatch.Draw(texture, dstTR, srcTR, color);

            spriteBatch.Draw(texture, dstL, srcL, color);
            spriteBatch.Draw(texture, dstC, srcC, color);
            spriteBatch.Draw(texture, dstR, srcR, color);

            spriteBatch.Draw(texture, dstBL, srcBL, color);
            spriteBatch.Draw(texture, dstB, srcB, color);
            spriteBatch.Draw(texture, dstBR, srcBR, color);
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
        public static Vector2 MoveTowards(this Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            Vector2 delta = target - current;
            float distance = delta.Length();

            if (distance <= maxDistanceDelta || distance == 0f)
                return target;

            return current + delta / distance * maxDistanceDelta;
        }
    }
}
