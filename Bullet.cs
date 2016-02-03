using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace niggigame
{
    class Bullet
    {
        float x, y;
        float startX, startY;
        float destX, destY;
        float speed = 0.05f;
        double radian = 0;
        float maxDistance = 3f;
        public bool active = true;

        public Bullet(float playerX, float playerY, float cursorX, float cursorY)
        {
            startX = playerX;
            startY = playerY;
            x = startX;
            y = startY;
            destX = cursorX;
            destY = cursorY;
            radian = Math.Atan2(destY - y, destX - x);
        }

        public void Move()
        {
            float dx = x - startX;
            float dy = y - startY;
            if (Math.Sqrt(dy * dy + dx * dx) < maxDistance)
            {
                x += (float)Math.Cos(radian) * speed;
                y += (float)Math.Sin(radian) * speed;
            }
            else
            {
                active = false;
            }
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.OrangeRed);
            GL.Vertex3(-0.1f + x, -0.1f + y, 0.5f);
            GL.Color3(Color.Green);
            GL.Vertex3(-0.1f + x, 0.1f + y, 0.5f);
            GL.Color3(Color.Red);
            GL.Vertex3(0.1f + x, 0.1f + y, 0.5f);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0.1f + x, -0.1f + y, 0.5f);
            GL.End();
        }
    }
}
