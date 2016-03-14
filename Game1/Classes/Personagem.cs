using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Personagem
    {
        public Texture2D Texture;
        public Vector2 Velocidade;
        public Vector2 Posicao;

        private Rectangle screenBound;

        public bool Pulando; //are we jumping or not
        public bool Cima; //if going up or not

        public float u; //initial velocity
        private float jumpU;
        private float g; //gravity
        public float t; //time
        public float ground;
        private float Speed;

        private KeyboardState prevKB;

        public Personagem(Texture2D Texture, Vector2 Posicao, float Speed, Rectangle screenBound)
        {
            this.Texture = Texture;
            this.Posicao = Posicao;
            ground = Posicao.Y;
            this.Speed = Speed;
            this.screenBound = screenBound;
            Velocidade = Vector2.Zero;
            Pulando = Cima = false;
            jumpU = 2.5f;
            g = -9.8f;
            t = 0;
        }

        public void Update(GameTime gameTime)
        {
            Posicao.X += (Velocidade.X * Speed);
            //Set the Y position to be subtracted so that the upward movement would be done by decreasing the Y value
            Posicao.Y -= (Velocidade.Y * Speed);
            Cima = (Velocidade.Y > 0);

            if (Pulando == true)
            {
                //Motion equation using velocity: v = u + at
                Velocidade.Y = (float)(u + (g * t));
                //Increase the timer
                t += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Pulando == true && Posicao.Y > screenBound.Height - Texture.Height)
            {
                Posicao.Y = ground = screenBound.Height - Texture.Height;
                Velocidade.Y = 0;
                Pulando = false;
                t = 0;
            }

            if (Posicao.X < 0)
            {
                //If Texture touches left side of the screen, set the position to zero and the velocity to zero.
                Posicao.X = 0;
                Velocidade.X = 0;
            }
            else if (Posicao.X + Texture.Width > screenBound.Width)
            {
                //If Texture touches left side of the screen, set the position to zero and the velocity to zero.
                Posicao.X = screenBound.Width - Texture.Width;
                Velocidade.X = 0;
            }
            if (Posicao.Y < 0)
            {
                //If the Texture touches the top of the screen, reset the timer and set the initial velocity to zero.
                Posicao.Y = 0;
                t = 0;
                u = 0;
            }
        }

        public void Entrada(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Space) && (Pulando == false || Posicao.Y == ground))
            {
                Pulando = true;
                u = jumpU;
            }
            if (keyState.IsKeyDown(Keys.A) && !keyState.IsKeyDown(Keys.D))
            {
                if (keyState.IsKeyDown(Keys.A))
                {
                    if (Velocidade.X > -2.0f)
                        Velocidade.X -= (1.0f / 10);
                }
                else if (Velocidade.X > -1.0f)
                {
                    Velocidade.X -= (1.0f / 10);
                }
                else
                {
                    Velocidade.X = -1.0f;
                }
            }
            else if (!keyState.IsKeyDown(Keys.A) && keyState.IsKeyDown(Keys.D))
            {
                if (keyState.IsKeyDown(Keys.A))
                {
                    if (Velocidade.X < 2.0f)
                        Velocidade.X += (1.0f / 10);
                }
                else if (Velocidade.X < 1.0f)
                {
                    Velocidade.X += (1.0f / 10);
                }
                else
                {
                    Velocidade.X = 1.0f;
                }
            }
            else
            {
                if (Velocidade.X > 0.05 || Velocidade.X < -0.05)
                    Velocidade.X *= 0.90f;
                else
                    Velocidade.X = 0;
            }

            prevKB = keyState;
        }

        public void Fall()
        {
            t = 0;
            u = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Posicao.X, (int)Posicao.Y, Texture.Width, Texture.Height), Color.White);
        }
    }
}
