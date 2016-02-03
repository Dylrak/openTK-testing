using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace niggigame
{
    class Cursor
    {
        float x = 0.0f, y = 0.0f;

        public void SetPosition(float cursorX, float cursorY)
        {
            x = cursorX;
            y = cursorY;
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.LineLoop);
            for (int a = 0; a < 360; a++)
            {
                double heading = a * Math.PI / 180;
                GL.Color3(Color.White);
                GL.Vertex3(Math.Cos(heading) * 0.05f + x, Math.Sin(heading) * 0.05f + y, 0.9f);
            }
            GL.End();
        }
    }
}