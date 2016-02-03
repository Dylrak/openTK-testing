using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace niggigame
{
    class Game : GameWindow
    {
        float screenWidth = 8f, screenHeight = 4.5f, screenDepth = 2f;
        List<Bullet> bullets = new List<Bullet>();
        List<Bullet> removeBullets = new List<Bullet>();
        Camera camera;
        Cursor cursor;
        Map map;
        Player player;

        public Game()
            : base(1920, 1080, new OpenTK.Graphics.GraphicsMode(32, 8, 0, 0))
        {
            camera = new Camera(screenWidth, screenHeight, screenDepth);
            cursor = new Cursor();
            map = new Map();
            player = new Player();
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            this.CursorVisible = false;
            this.WindowBorder = WindowBorder.Hidden;
            this.WindowState = WindowState.Fullscreen;
        }

        //drawing shit
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            camera.Draw();
            cursor.Draw();
            map.Draw();
            player.Draw();
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw();
            }

            SwapBuffers();
        }

        //movement shit
        override protected void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Space])
            {
                PointF playerPosition = player.GetPosition();
                camera.SetPosition(-playerPosition.X, -playerPosition.Y);
            }
            if (Mouse.X < ClientSize.Width / 100)
            {
                camera.Move(0.1f, 0.0f);
            }
            else if (Mouse.X > ClientSize.Width - ClientSize.Width / 100)
            {
                camera.Move(-0.1f, 0.0f);
            }
            if ((Mouse.Y < ClientSize.Height / 100))
            {
                camera.Move(0.0f, -0.1f);
            }
            else if (Mouse.Y > ClientSize.Height - ClientSize.Height / 100)
            {
                camera.Move(0.0f, 0.1f);
            }

            //mouse position in world coordinates
            PointF cameraPosition = camera.GetPosition();
            float mouseX = ((float)Mouse.X - (float)ClientSize.Width / 2) / (float)ClientSize.Width * screenWidth - cameraPosition.X;
            float mouseY = -(((float)Mouse.Y - (float)ClientSize.Height / 2) / (float)ClientSize.Height * screenHeight + cameraPosition.Y);
            cursor.SetPosition(mouseX, mouseY);
            Console.WriteLine(System.Environment.NewLine);
            Console.WriteLine("Mouse: " + mouseX + ", " + mouseY);

            if (Mouse[MouseButton.Right])
            {
                player.SetDestination(mouseX, mouseY);
            }
            player.Move();
            player.Update();
            if (Mouse[MouseButton.Left])
            {
                if (player.ReadyToShoot())
                {
                    player.Shoot();
                    PointF playerPosition = player.GetPosition();
                    bullets.Add(new Bullet(playerPosition.X, playerPosition.Y, mouseX, mouseY));
                }
            }
            foreach (Bullet bullet in bullets)
            {
                if (bullet.active)
                {
                    bullet.Move();
                }
                else
                {
                    removeBullets.Add(bullet);
                }
            }
            foreach (Bullet bullet in removeBullets)
            {
                bullets.Remove(bullet);
            }
            removeBullets.Clear();
            if (Keyboard[Key.Escape])
                Exit();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
        }

        public static void Main(string[] args)
        {
            using (Game p = new Game())
            {
                p.Run();
            }
        }
    }
}