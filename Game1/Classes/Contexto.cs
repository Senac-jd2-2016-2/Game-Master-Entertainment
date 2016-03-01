using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class Contexto
    {
        public static Personagem[] perso = new Personagem[5];
        public static Personagem PG = new Personagem(150, 240);
        public static inimigos[] inimg = new inimigos[2];
        public static inimigos IN = new inimigos(500, 60);
        public static Texture2D BG;

        public static void Inicializar (ContentManager content)
        {
            PG.texture = content.Load<Texture2D>("Pe");
            IN.texture = content.Load<Texture2D>("Panda");
            BG = content.Load<Texture2D>("Algo");
        }
    }
}
