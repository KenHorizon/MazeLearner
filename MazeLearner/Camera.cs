using MazeLearner;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeLeaner
{
    public class Camera
    {
        public Vector2 Position { get; private set; } = Vector2.Zero;
        public float Zoom { get; private set; } = 1.0F;
        public float Rotation { get; private set; } = 0.0F; 
        public Vector2 Origin { get; private set; } = Vector2.Zero;
        public Viewport Viewport { get; private set; }
        public Rectangle? Bounds { get; set; } = null;

        public Camera(Viewport viewport)
        {
            this.Bounds = Main.WindowScreen;
            this.Viewport = viewport;
            this.Origin = new Vector2(viewport.Width, viewport.Height) / 2.0F;
        }
        public Matrix GetViewMatrix()
        {
            var matrix =
                Matrix.CreateTranslation(new Vector3(-this.Position, 0.0F)) *
                Matrix.CreateTranslation(new Vector3(-this.Origin, 0.0F)) *
                Matrix.CreateRotationZ(this.Rotation) *
                Matrix.CreateScale(this.Zoom, this.Zoom, 1.0F) *
                Matrix.CreateTranslation(new Vector3(this.Origin, 0.0F));
            return matrix;
        }

        public void SetPosition(Vector2 worldPos)
        {
            this.Position = worldPos;
        }

        public void Move(Vector2 delta)
        {
            this.Position += delta;
        }

        public void SetZoom(float zoom)
        {
            this.Zoom = MathHelper.Clamp(zoom, 0.1F, 10F);
        }

        public void SetRotation(float radians)
        {
            this.Rotation = radians;
        }
        public void SmoothFollow(Vector2 targetWorldPos, float smoothing)
        {
            this.Position = Vector2.Lerp(this.Position, targetWorldPos, MathHelper.Clamp(smoothing, 0.0F, 1.0F));
        }
        public void SetFollow(Vector2 pos)
        {
            this.Position = pos;
        }
        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            Matrix inverse = Matrix.Invert(GetViewMatrix());
            return Vector2.Transform(screenPosition, inverse);
        }
        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, GetViewMatrix());
        }

        public void UpdateViewport(Viewport vp)
        {
            this.Viewport = vp;
            this.Origin = new Vector2(vp.Width, vp.Height) / 2.0F;
        }
    }
}
