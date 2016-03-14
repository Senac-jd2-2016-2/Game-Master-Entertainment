using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Plataforma
    {
        Texture2D Texture;
        Vector2 Posicao;
        int BlockState;

        public Plataforma(Texture2D Texture, Vector2 Position, int BlockState)
        {
            this.Texture = Texture;
            this.Posicao = Position;
            this.BlockState = BlockState;
        }

        public Personagem BlockCollision(Personagem personagem)
        {
            Rectangle Topo = new Rectangle((int)Posicao.X + 5, (int)Posicao.Y - 10, Texture.Width - 10, 10);
            Rectangle Botao = new Rectangle((int)Posicao.X + 5, (int)Posicao.Y + Texture.Height, Texture.Width - 10, 10);
            Rectangle Esquerda = new Rectangle((int)Posicao.X - 10, (int)Posicao.Y + 5, 10, Texture.Height - 10);
            Rectangle Direita = new Rectangle((int)Posicao.X + Texture.Width, (int)Posicao.Y + 5, 10, Texture.Height - 10);

            if (BlockState != 2 || (BlockState == 2 && !personagem.Cima))
            {
                if (Topo.Intersects(new Rectangle((int)personagem.Posicao.X, (int)personagem.Posicao.Y, personagem.Texture.Width, personagem.Texture.Height)))
                {
                    if (personagem.Posicao.Y + personagem.Texture.Height > Posicao.Y && personagem.Posicao.Y + personagem.Texture.Height < Posicao.Y + Texture.Height / 2)
                    {
                        personagem.Posicao.Y = personagem.ground = Posicao.Y - personagem.Texture.Height;
                        personagem.Velocidade.Y = 0;
                        personagem.Pulando = false;
                        personagem.t = 0;
                    }

                }
                else if (personagem.Pulando == false && personagem.ground == Posicao.Y - personagem.Texture.Height)
                {
                    personagem.Pulando = true;
                    personagem.u = 0;
                }

                if (BlockState != 2)
                {
                    if (Botao.Intersects(new Rectangle((int)personagem.Posicao.X, (int)personagem.Posicao.Y, personagem.Texture.Width, personagem.Texture.Height)))
                    {
                        if (personagem.Posicao.Y < Posicao.Y + Texture.Height)
                        {
                            personagem.Posicao.Y = Posicao.Y + Texture.Height;
                            personagem.t = 0;
                            personagem.u = 0;
                        }

                    }

                    if (Esquerda.Intersects(new Rectangle((int)personagem.Posicao.X, (int)personagem.Posicao.Y, personagem.Texture.Width, personagem.Texture.Height)))
                    {
                        if (personagem.Posicao.X + personagem.Texture.Width > Posicao.X)
                        {
                            personagem.Posicao.X = Posicao.X - personagem.Texture.Width;
                            personagem.Velocidade.X = 0;
                        }
                    }

                    if (Direita.Intersects(new Rectangle((int)personagem.Posicao.X, (int)personagem.Posicao.Y, personagem.Texture.Width, personagem.Texture.Height)))
                    {
                        if (personagem.Posicao.X < Posicao.X + Texture.Width)
                        {
                            personagem.Posicao.X = Posicao.X + Texture.Width;
                            personagem.Velocidade.X = 0;
                        }
                    }
                }
            }
            return personagem;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Posicao.X, (int)Posicao.Y, Texture.Width, Texture.Height), Color.White);
        }
    }
}