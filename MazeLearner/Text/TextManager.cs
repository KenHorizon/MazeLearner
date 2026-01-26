using MazeLearner;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MazeLeaner.Text
{
    public class TextManager
    {
        public static class Regexes
        {
            public static readonly Regex Format = new Regex("(?<!\\\\)\\[(?<tag>[a-zA-Z]{1,10})(\\/(?<options>[^:]+))?:(?<text>.+?)(?<!\\\\)\\]", RegexOptions.Compiled);
        }
        private static readonly Vector2[] ShadowDirections = new Vector2[2] {
            
            Vector2.UnitX,
            -Vector2.UnitY
        };
        public static string WrapText(Assets<SpriteFont> spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0F;
            float spaceWidth = spriteFont.Value.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = TextManager.MeasureString(spriteFont, word);
                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }
            return sb.ToString();
        }
        public static void TextBox(Assets<SpriteFont> font, string text, Rectangle rect, Vector2 paddingPos, Color color)
        {
            TextManager.Text(font, WrapText(font, text, rect.Width), new Vector2(rect.X + paddingPos.X, rect.Y + paddingPos.X), Vector2.Zero, color);
        }

        public static void CenteredText(Assets<SpriteFont> font, string text, Rectangle rect, Color color)
        {
            int screenWidth = rect.Width;
            int screenHeight = rect.Height;
            Vector2 baseSize = TextManager.MeasureString(font, text);
            float scaleX = screenWidth / baseSize.X;
            float scaleY = screenHeight / baseSize.Y;
            Vector2 textPos = new Vector2(rect.X + baseSize.X, rect.Y + 40);
            Vector2 origin = baseSize / 2.0F;
            TextManager.Text(font, text, textPos, origin, color);
        }
        public static void CenteredText(Assets<SpriteFont> font, Vector2 position, string text, Color color)
        {
            int screenWidth = Main.Instance.GetScreenWidth();
            int screenHeight = Main.Instance.GetScreenHeight();
            Vector2 baseSize = TextManager.MeasureString(font, text);
            float scaleX = screenWidth / baseSize.X;
            float scaleY = screenHeight / baseSize.Y;
            Vector2 textPos = new Vector2(screenWidth , screenHeight) / 2.0F;
            Vector2 origin = baseSize / 2.0F;
            TextManager.Text(font, text, textPos + position, origin, color);
        }
        // Draw Text with No Origin
        public static void Text(Assets<SpriteFont> font, string text, Vector2 position, Color color)
        {
            TextManager.Text(font, text, position, Vector2.Zero, color);
        }
        public static void Text(Assets<SpriteFont> font, string text, Vector2 position)
        {
            TextManager.Text(font, text, position, Vector2.Zero, Color.White);
        }
        public static void Text(Font font, string text, Vector2 position)
        {
            TextManager.Text(font.FontStyle, text, position, Vector2.Zero, Color.White);
        }
        public static void Text(Assets<SpriteFont> font, string text, Vector2 position, Color color, bool shadow = true)
        {
            TextManager.Text(font, text, position, Vector2.Zero, color, shadow: shadow);
        }
        public static void Text(Font font, string text, Vector2 position, Color color, bool shadow = false)
        {
            TextManager.Text(font.FontStyle, text, position, Vector2.Zero, color, shadow: shadow);
        }
        // Draw Text with Origin
        public static void Text(Assets<SpriteFont> font, string text, Vector2 position, Vector2 origin)
        {
            TextManager.Text(font, text, position, origin, Color.White);
        }
        public static void Text(Assets<SpriteFont> font, string text, Vector2 position, Vector2 origin, Color color)
        {
            TextManager.Text(font, text, position, origin, color, null);
        }
        //
        public static void TextUnderline(Assets<SpriteFont> font, string text, Vector2 position, Color lineColor, Color textColor)
        {
            Vector2 textSize = TextManager.MeasureString(font, text);
            TextManager.Text(font, text, position, textColor);
            Vector2 startLine = new Vector2(position.X, position.Y + textSize.Y);
            Vector2 endLine = new Vector2(position.X + textSize.X, position.Y + textSize.Y);    
            Main.SpriteBatch.DrawLine(startLine, endLine, lineColor);
        }
        public static void TextUnderline(string text, Vector2 position, Color color)
        {
            TextUnderline(Fonts.Normal, text, position, color, Color.White);
        }
        public static void TextUnderline(string text, Vector2 position, Color lineColor, Color color)
        {
            TextUnderline(Fonts.Normal, text, position, lineColor, color);
        }

        public static void Text(
            Assets<SpriteFont> asset,
            string text,
            Vector2 position,
            Vector2 origin,
            Color color,
            Vector2? bounds = null,
            bool shadow = true,
            float rotation = 0.0F
            )
        {
            asset = asset == null ? Fonts.Normal : asset;
            SpriteFont font = asset.Value;
            Vector2 cursor = position;

            foreach (var part in TextManager.ParseTextParts(text, color))
            {
                DynamicSpriteFont dynamic = new DynamicSpriteFont(font);
                if (shadow == true)
                {
                    for (int i = 0; i < ShadowDirections.Length; i++)
                    {
                        dynamic.DrawString(Main.SpriteBatch, part.Text, cursor + ShadowDirections[i], origin, Color.Gray, rotation: rotation);
                    }
                }
                dynamic.DrawString(Main.SpriteBatch, part.Text, cursor, origin, color, rotation: rotation);

                cursor.X += font.MeasureString(part.Text).X;
            }
        }
        // End
        private static IEnumerable<TextPart> ParseTextParts(string text, Color defaultColor)
        {
            int lastIndex = 0;
            foreach (Match match in Regexes.Format.Matches(text))
            {
                if (match.Index > lastIndex)
                {
                    yield return new TextPart(text[lastIndex..match.Index], defaultColor);
                }
                string hex = match.Groups[1].Value;
                string content = match.Groups[2].Value;
                Color color = HexToColor(hex);

                yield return new TextPart(content, color);
                lastIndex = match.Index + match.Length;
            }
            if (lastIndex < text.Length)
            {
                yield return new TextPart(text[lastIndex..], defaultColor);
            }
        }
        public static Vector2 MeasureString(Assets<SpriteFont> font, string text)
        {
            float totalWidth = 0.0F;
            float height = 0.0F;
            foreach (var part in ParseTextParts(text, Color.White))
            {
                Vector2 size = font.Value.MeasureString(part.Text);
                totalWidth += size.X;
                height = Math.Max(height, size.Y);
            }
            return new Vector2(totalWidth, height);
        }
        public static Vector2 MeasureString(SpriteFont font, string text, int px)
        {
            return MeasureString(font, text, (float)(px / 256.0F));
        }
        public static Vector2 MeasureString(SpriteFont font, string text, float scale = 1.0F)
        {
            float totalWidth = 0.0F;
            float height = 0.0F;
            foreach (var part in ParseTextParts(text, Color.White))
            {
                Vector2 size = font.MeasureString(part.Text);
                totalWidth += size.X * scale;
                height = Math.Max(height, size.Y * scale);
            }
            return new Vector2(totalWidth, height);
        }
        private static Color HexToColor(string hex)
        {
            return new Color(
                Convert.ToInt32(hex.Substring(0, 2), 16),
                Convert.ToInt32(hex.Substring(2, 2), 16),
                Convert.ToInt32(hex.Substring(4, 2), 16)
            );
        }

        private record TextPart(string Text, Color Color);
    }
}
