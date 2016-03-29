using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Fruta
    {
        Texture2D texture;
        Rectangle posicao;

        public Fruta(Texture2D Texture, Vector2 Posicao)
        {
            this.texture = Texture;
            posicao = new Rectangle((int)Posicao.X, (int)Posicao.Y, Texture.Width, Texture.Height);
        }

        public bool colidindo(Rectangle outracaixa)
        {
            return posicao.Intersects(outracaixa);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, posicao, Color.White);
        }
    }
}