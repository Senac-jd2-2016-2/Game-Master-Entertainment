using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Classes
{
    class Fases
    {

        public int c;
        public int v;
        public Texture2D texture;
        public Texture2D Chao;
        public Rectangle Chao1 = new Rectangle();

        public Fases(int c1, int v1)
        {
            c = c1;
            v = v1;
        }

        public Fases()
        {
            // TODO: Complete member initialization
        }

        public Vector2 getVector()
        {
            return new Vector2(c, v);
        }
    }
}
