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
        private bool caindo;
        private bool pulando;
        private float alturaPulo;
        private Vector2 posicaoInicial;

        public Personagem(Texture2D Texture, Vector2 Posicao, Rectangle screenBound)
        {
            this.texture = Texture;
            this.posicao = Posicao;
            this.posicaoInicial = Posicao;
            this.screenBound = screenBound;
        }

        public override void Update(GameTime gameTime)
        {

            verificarPerdeu();
            
               if (pulando)
            {
                caindo = false;
                float diff = 600 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                alturaPulo += diff;
                posicao.Y -= diff;
            }

            if (alturaPulo > 300)
               {
                   caindo = true;
                   pulando = false;
                   alturaPulo = 0;
               }
               
            executarGravidade(gameTime);


            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.A))
            {
                posicao.X = posicao.X - 500 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (state.IsKeyDown(Keys.D)){
                posicao.X = posicao.X + 500 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (state.IsKeyDown(Keys.Space) && (!pulando && !caindo))
            {
                alturaPulo = 0;
                pulando = true;
            }


            //sempre deve ser a ultima linha
            caindo = true;
        }

      
         

        private void executarGravidade(GameTime gameTime)
        {
            if (caindo )
            {
                posicao.Y = posicao.Y + 600 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void verificarPerdeu()
        {
            if (posicao.Y > screenBound.Height)
            {
                posicao.X = posicaoInicial.X;
                posicao.Y = posicaoInicial.Y;
            }
        }


        public override void Colidir(Cenario cenario, GameObject obj, Posicao posicaoObj)
        {
            if (obj is Fruta)
            {
                cenario.RemoveGameObject(obj);
            }
            if (obj is Plataforma)
            {
                Plataforma plat = (Plataforma)obj;
                if (plat.BlockState == 2 && posicaoObj == Posicao.CIMA)
                {
                    caindo = false;
                }
               if(plat.BlockState == 1 && posicaoObj == Posicao.CIMA )
               {
                   caindo = false;
               }

               caindo = false;
                
            }

        }

    }
}
