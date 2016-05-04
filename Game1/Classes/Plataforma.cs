using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Framework;

namespace Game1
{
    public class Plataforma : GameObject
    {
        //   public Plataforma(Texture2D Texture, Vector2 Posicao)
        //{
        //    this.texture = Texture;
        //    this.posicao = Posicao;
        //}

        //Texture2D Texture;
        //Vector2 Posicao;
        int BlockState;

           public Plataforma(Texture2D Texture, Vector2 Posicao, int BlockState)
           {
               this.texture = Texture;
               this.posicao = Posicao;
               this.BlockState = BlockState;
           }

           public Personagem ColisaoBloco(Personagem personagem)
           {
               Rectangle Topo = new Rectangle((int)Posicao.X + 5, (int)Posicao.Y - 10, Texture.Width - 10, 10);
               Rectangle Botao = new Rectangle((int)Posicao.X + 5, (int)Posicao.Y + Texture.Height, Texture.Width - 10, 10);
               Rectangle Esquerda = new Rectangle((int)Posicao.X - 10, (int)Posicao.Y + 5, 10, Texture.Height - 10);
               Rectangle Direita = new Rectangle((int)Posicao.X + Texture.Width, (int)Posicao.Y + 5, 10, Texture.Height - 10);

               if (BlockState != 2 || (BlockState == 2 && !personagem.cima))
               {
                   if (Topo.Intersects(new Rectangle((int)personagem.posicao.X, (int)personagem.posicao.Y, personagem.texture.Width, personagem.texture.Height)))
                   {
                       if (personagem.posicao.Y + personagem.texture.Height > Posicao.Y && personagem.posicao.Y + personagem.texture.Height < Posicao.Y + Texture.Height / 2)
                       {
                           personagem.posicao.Y = personagem.personagemchao = Posicao.Y - personagem.texture.Height;
                           personagem.velocidade.Y = 0;
                           personagem.pulando = false;
                           personagem.tempo = 0;
                       }

                   }
                   else if (personagem.pulando == false && personagem.personagemchao == Posicao.Y - personagem.texture.Height)
                   {
                       personagem.pulando = true;
                       personagem.VeloInicial = 0;
                   }

                   if (BlockState != 2)
                   {
                       if (Botao.Intersects(new Rectangle((int)personagem.posicao.X, (int)personagem.posicao.Y, personagem.texture.Width, personagem.texture.Height)))
                       {
                           if (personagem.posicao.Y < Posicao.Y + Texture.Height)
                           {
                               personagem.posicao.Y = Posicao.Y + Texture.Height;
                               personagem.tempo = 0;
                               personagem.VeloInicial = 0;
                           }
                       }

                       if (Esquerda.Intersects(new Rectangle((int)personagem.posicao.X, (int)personagem.posicao.Y, personagem.texture.Width, personagem.texture.Height)))
                       {
                           if (personagem.posicao.X + personagem.texture.Width > Posicao.X)
                           {
                               personagem.posicao.X = Posicao.X - personagem.texture.Width;
                               personagem.velocidade.X = 0;
                           }
                       }

                       if (Direita.Intersects(new Rectangle((int)personagem.posicao.X, (int)personagem.posicao.Y, personagem.texture.Width, personagem.texture.Height)))
                       {
                           if (personagem.posicao.X < Posicao.X + Texture.Width)
                           {
                               personagem.posicao.X = Posicao.X + Texture.Width;
                               personagem.velocidade.X = 0;
                           }
                       }
                   }
               }
               return personagem;
           }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, posicao, Color.White);
        }
    }
}