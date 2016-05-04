using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Framework;

namespace Game1
{
    public class Fruta : GameObject
    {

        public Fruta(Texture2D Texture, Vector2 Posicao, int score)
        {
            this.texture = Texture;
            this.posicao = Posicao;
        }

        public bool colidindo(Rectangle outracaixa)
        {
            //return posicao.Intersects(outracaixa);
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, posicao, Color.White);
        }
    }
}