using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game1.Framework;

namespace Game1
{
    public class Personagem : GameObject
    {
        public Vector2 velocidade;

        private Rectangle screenBound;
        private KeyboardState tecla;

        public bool pulando;
        public bool cima;

        public float VeloInicial;
        private float jumpU;
        private float gravidade;
        public float tempo;
        public float personagemchao;
        private float Speed;

        public Personagem(Texture2D Texture, Vector2 Posicao, float Speed, Rectangle screenBound)
        {
            this.texture = Texture;
            this.posicao = Posicao;
            personagemchao = Posicao.Y;
            this.Speed = Speed;
            this.screenBound = screenBound;
            velocidade = Vector2.Zero;
            pulando = cima = false;
            jumpU = 2.5f;
            gravidade = -9.8f;
            tempo = 0;
        }

        public override void Update(GameTime gameTime)
        {
            Entrada(Keyboard.GetState());

            posicao.X += (velocidade.X * Speed);
            posicao.Y -= (velocidade.Y * Speed);
            cima = (velocidade.Y > 0);

            if (pulando == true)
            {
                velocidade.Y = (float)(VeloInicial + (gravidade * tempo));
                tempo += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (pulando == true && posicao.Y > screenBound.Height - texture.Height)
            {
                posicao.Y = personagemchao = screenBound.Height - texture.Height;
                velocidade.Y = 0;
                pulando = false;
                tempo = 0;
            }

            if (posicao.X < 0)
            {
                posicao.X = 0;
                velocidade.X = 0;
            }
            else if (posicao.X + texture.Width > screenBound.Width)
            {
                posicao.X = screenBound.Width - texture.Width;
                velocidade.X = 0;
            }
            if (posicao.Y < 0)
            {
                posicao.Y = 0;
                tempo = 0;
                VeloInicial = 0;
            }
        }

        public void Entrada(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Space) && (pulando == false || posicao.Y == personagemchao))
            {
                pulando = true;
                posicao.Y -= 1;
                VeloInicial = jumpU;
            }
            if (keyState.IsKeyDown(Keys.A) && !keyState.IsKeyDown(Keys.D))
            {
                if (keyState.IsKeyDown(Keys.A))
                {
                    if (velocidade.X > -2.0f)
                        velocidade.X -= (1.0f / 10);
                }
                else if (velocidade.X > -1.0f)
                {
                    velocidade.X -= (1.0f / 10);
                }
                else
                {
                    velocidade.X = -1.0f;
                }
            }
            else if (!keyState.IsKeyDown(Keys.A) && keyState.IsKeyDown(Keys.D))
            {
                if (keyState.IsKeyDown(Keys.A))
                {
                    if (velocidade.X < 2.0f)
                        velocidade.X += (1.0f / 10);
                }
                else if (velocidade.X < 1.0f)
                {
                    velocidade.X += (1.0f / 10);
                }
                else
                {
                    velocidade.X = 1.0f;
                }
            }
            else
            {
                if (velocidade.X > 0.05 || velocidade.X < -0.05)
                    velocidade.X *= 0.90f;
                else
                    velocidade.X = 0;
            }

            tecla = keyState;
        }

        public void Fall()
        {
            tempo = 0;
            VeloInicial = 0;
        }

        public override void Colidir(Cenario cenario, GameObject obj, Posicao posicaoObj)
        {
            if (obj is Fruta)
            {
                cenario.RemoveGameObject(obj);
            }
            if (obj is Plataforma && cenario.getGameObjectRectangle(this).Bottom < cenario.getGameObjectRectangle(obj).Top)
            {
                this.velocidade.Y = 0;
                pulando = false;
                tempo = 0;
            }

        }

    }
}
