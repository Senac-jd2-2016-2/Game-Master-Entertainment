﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class Personagem
    {
        public int vida;
        public int furia;
        public int veloc;
        public bool pulo = false;
        public bool atack = false;
        public Rectangle Pers = new Rectangle();
        public int x;
        public int y;
        public Texture2D texture;
        

        public Personagem(int x1, int y1)
        {
            x = x1;
            y = y1;
        }

        public Vector2 getVector()
        {
            return new Vector2(x, y);
        }

        public void moverX(int qtdPassos)
        {
            x += qtdPassos;
        }
        public void moverY(int qtdPassos)
        {
            y += qtdPassos;
        }

    }
}
