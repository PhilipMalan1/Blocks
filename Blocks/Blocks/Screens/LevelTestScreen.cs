using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blocks.Screens
{
    public class LevelTestScreen:Screen
    {
        private GraphicsDevice graphicsDevice;
        private Game1 game1;
        private LevelEditorScreen levelEditor;
        private Level level;
        private GameScreen gameScreen;
        private KeyboardState key, keyi;

        public LevelTestScreen(GraphicsDevice graphicsDevice, Game1 game1, LevelEditorScreen levelEditor, Level level) : base(graphicsDevice, game1)
        {
            this.graphicsDevice = graphicsDevice;
            this.game1 = game1;
            this.levelEditor = levelEditor;
            this.level = level;

            gameScreen = new GameScreen(graphicsDevice, game1, level);
            key = Keyboard.GetState();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gameScreen.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            //exit game
            keyi = key;
            key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.T) && keyi.IsKeyUp(Keys.T))
            {
                level.Initialize(levelEditor.BlockWidth);
                game1.SetScreen(levelEditor);
            }

            gameScreen.Update(gameTime);
        }
    }
}
