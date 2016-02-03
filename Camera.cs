using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace niggigame
{
    class Camera
    {
        float screenWidth, screenHeight, screenDepth;
        float x = 0.0f, y = 0.0f, z = 0.0f;

        public Camera(float width, float height, float depth)
        {
            screenWidth = width;
            screenHeight = height;
            screenDepth = depth;
        }

        public void Draw()
        {
            GL.ClearColor(Color.CornflowerBlue);
            GL.ClearDepth(1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Ortho(-(screenWidth / 2), screenWidth / 2, -(screenHeight / 2), screenHeight / 2, -(screenDepth / 2), screenDepth / 2);
            GL.Translate(x, y, z);
        }

        public void ConsoleText()
        {
            Console.WriteLine("Camera: " + x + ", " + y + ", " + z);
        }

        public PointF GetPosition()
        {
            return new PointF(x, y);
        }

        public void SetPosition(float posX, float posY)
        {
            x = posX;
            y = posY;
        }

        public void Move(float fx, float fy)
        {
            x += fx;
            y += fy;
        }
    }
}
