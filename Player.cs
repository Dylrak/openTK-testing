using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Diagnostics;

namespace niggigame
{
    class Player
    {
        float x = 0.0f, y = 0.0f;
        float destX = 0.0f, destY = 0.0f;
        float speed = 0.025f;
        bool readyForShot = true;
        Stopwatch shotTimer = new Stopwatch();
        double minimumShotTime = 500;

        public void SetDestination(float destinationX, float destinationY)
        {
            destX = destinationX;
            destY = destinationY;
        }

        public void Update()
        {
            if (!readyForShot && shotTimer.Elapsed.TotalMilliseconds > minimumShotTime)
            {
                shotTimer.Stop();
                shotTimer.Reset();
                readyForShot = true;
            }
        }

        public void Move()
        {
            float dx = destX - x;
            float dy = destY - y;
            if (Math.Sqrt(dy*dy + dx*dx) < speed)
            {
                x = destX;
                y = destY;
            }
            else
            {
                double radian = Math.Atan2(dy, dx);
                x += (float)Math.Cos(radian) * speed;
                y += (float)Math.Sin(radian) * speed;
            }
        }

        public PointF GetPosition()
        {
            return new PointF(x, y);
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

        public void Shoot()
        {
            if (readyForShot)
            {
                shotTimer.Start();
                readyForShot = false;
            }
        }

        public bool ReadyToShoot()
        {
            return readyForShot;
        }
    }
}