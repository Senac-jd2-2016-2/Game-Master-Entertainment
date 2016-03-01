using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class inimigos
    {
        public int VidaInim;
        public int VeloInim;
        public int AtckInim;
        public int a;
        public int b;
        public Texture2D inimigo;
        public Rectangle Panda = new Rectangle();
        public Texture2D texture;

         public inimigos(int a1, int b1)
        {
            a = a1;
            b = b1;
        }

        public Vector2 getVector()
        {
            return new Vector2(a, b);
        }
    }
}
