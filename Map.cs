using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace niggigame
{
    class Map
    {
        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Red);
            GL.Vertex3(-50, -50, 0.0f);
            GL.Color3(Color.Blue);
            GL.Vertex3(-50, 50, 0.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(50, 50, 0.0f);
            GL.Color3(Color.Purple);
            GL.Vertex3(50, -50, 0.0f);
            GL.End();
        }
    }
}
