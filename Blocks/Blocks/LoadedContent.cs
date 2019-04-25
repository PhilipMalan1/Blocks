using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class LoadedContent
    {
        public static Texture2D ground, player, block, door, goal;
        public static Texture2D ground, player, block, door, spikes;
        public static Texture2D[] button;

        public static Texture2D mainMenuBackground;
        public static Texture2D mainMenuButton;
        public static SpriteFont mainMenuFont;
        public static void LoadContent(ContentManager content)
        {
            ground = content.Load<Texture2D>("Ground");
            mainMenuBackground = content.Load<Texture2D>("start back round");
            mainMenuButton = content.Load<Texture2D>("Button");
            mainMenuFont = content.Load<SpriteFont>("MainMenuFont");
            ground = content.Load<Texture2D>("Ground2");
            player = content.Load<Texture2D>("Player");
            spikes = content.Load<Texture2D>("Spikes");
            block = content.Load<Texture2D>("Block");
            button = new Texture2D[4];
            for (int i = 0; i < button.Length; i++)
            {
                button[i]=content.Load<Texture2D>("button/button.000"+(i+1));
            }
            door = content.Load<Texture2D>("Door");
            goal = content.Load<Texture2D>("EndPole");
        }
    }
}
