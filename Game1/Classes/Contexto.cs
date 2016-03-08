using Game1.Classes;
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
        public static Personagem PG = new Personagem(20, 260);
        public static inimigos IN = new inimigos(450, 60);
        public static Fases CH = new Fases(0, 420);
        public static Texture2D BG;

        public static void Inicializar (ContentManager content)
        {
            CH.texture = content.Load<Texture2D>("Chão");
            PG.Texture = content.Load<Texture2D>("Pe");
            IN.texture = content.Load<Texture2D>("Panda");
            BG = content.Load<Texture2D>("Algo");
        }
    }
}
