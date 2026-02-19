using Assimp;
using MazeLearner;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace MazeLeaner.Text
{
    public class Texts
    {
        public static class Regexes
        {
            public static readonly Regex Format = new Regex("(?<!\\\\)\\[(?<tag>[a-zA-Z]{1,10})(\\/(?<options>[^:]+))?:(?<text>.+?)(?<!\\\\)\\]", RegexOptions.Compiled);
        }
        private static readonly Vector2[] ShadowDirections = new Vector2[2] {
            
            Vector2.UnitX,
            -Vector2.UnitY
        };
        public static string WrapText(Asset<SpriteFont> spriteFont, string text, float maxLineWidth)
        {
            if (text == null) return "";
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0F;
            float spaceWidth = spriteFont.Value.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = Texts.MeasureString(spriteFont, word);
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
        public static void DrawStringBox(Asset<SpriteFont> font, string text, Rectangle rect, Vector2 paddingPos, Color color)
        {
            Vector2 sz = Texts.MeasureString(font, text);
            Texts.DrawString(font, WrapText(font, text, rect.Width + (sz.X/2)), new Vector2(rect.X + paddingPos.X, rect.Y + paddingPos.X), Vector2.Zero, color);
        }
        public static void DrawStringBox(string text, Rectangle rect, Vector2 paddingPos, Color color)
        {
            Vector2 sz = Texts.MeasureString(Fonts.Text, text);
            Texts.DrawString(Fonts.Text, WrapText(Fonts.Text, text, rect.Width), new Vector2(rect.X + paddingPos.X, rect.Y + paddingPos.X), Vector2.Zero, color);
        }
        public static void DrawCenteredString(Asset<SpriteFont> font, string text, Rectangle rect, Color color)
        {
            int screenWidth = rect.Width;
            int screenHeight = rect.Height;
            Vector2 baseSize = Texts.MeasureString(font, text);
            float scaleX = screenWidth / baseSize.X;
            float scaleY = screenHeight / baseSize.Y;
            Vector2 textPos = new Vector2(rect.X + baseSize.X, rect.Y + 40);
            Vector2 origin = baseSize / 2.0F;
            Texts.DrawString(font, text, textPos, origin, color);
        }
        #region Text no Origin
        public static void Text(Asset<SpriteFont> font, string text, Vector2 position, Color color)
        {
            Texts.DrawString(font, text, position, Vector2.Zero, color);
        }
        public static void DrawString(string text, Vector2 position)
        {
            Texts.DrawString(Fonts.Text, text, position, Vector2.Zero, Color.Black);
        }
        public static void DrawString(Asset<SpriteFont> font, string text, Vector2 position)
        {
            Texts.DrawString(font, text, position, Vector2.Zero, Color.Black);
        }
        public static void DrawString(string text, Vector2 position, Color color)
        {
            Texts.DrawString(Fonts.Text, text, position, Vector2.Zero, color);
        }
        #endregion
        #region Underlined Text
        public static void DrawStringUnderline(string text, Vector2 position, Color lineColor, Color textColor)
        {
            DrawStringUnderline(Fonts.Text, text, position, lineColor, textColor);
        }
        public static void DrawStringUnderline(string text, Vector2 position)
        {
            DrawStringUnderline(Fonts.Text, text, position, Color.White, Color.Black);
        }
        public static void DrawStringUnderline(Asset<SpriteFont> font, string text, Vector2 position, Color lineColor, Color textColor)
        {
            Vector2 textSize = Texts.MeasureString(font, text);
            Texts.Text(font, text, position, textColor);
            Vector2 startLine = new Vector2(position.X, position.Y + textSize.Y);
            Vector2 endLine = new Vector2(position.X + textSize.X, position.Y + textSize.Y);
            Main.SpriteBatch.DrawLine(startLine, endLine, lineColor);
        }
        #endregion

        public static void DrawString(
            Asset<SpriteFont> asset,
            string text,
            Vector2 position,
            Vector2 origin,
            Color color,
            Vector2? bounds = null,
            bool shadow = true,
            float rotation = 0.0F
            )
        {
            asset = asset == null ? Fonts.Text : asset;
            SpriteFont font = asset.Value;
            Vector2 cursor = position;

            foreach (var part in Texts.ParseTextParts(text, color))
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
            if (text.IsEmpty())
            {
                text = "null";
            }
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
        public static Vector2 MeasureString(Asset<SpriteFont> font, string text)
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
