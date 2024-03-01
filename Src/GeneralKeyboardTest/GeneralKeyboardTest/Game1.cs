using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using System.Windows.Forms;

namespace GeneralKeyboardTest
{
    internal class Game1 : Microsoft.Xna.Framework.Game
    {
        KeyboardState oldState;
        Form1 form1 = new Form1();
        public Game1()
        {
            Initialize();
            BeginRun();
            oldState = Keyboard.GetState();
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void Update(GameTime gameTime)
        {
            UpdateInput();
            base.Update(gameTime);
        }
        public void UpdateInput()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Space))
            {
                MessageBox.Show("ok");
                form1.SetKeys();
            }
            else if (oldState.IsKeyDown(Keys.Space))
            {
                MessageBox.Show("ok");
                form1.SetKeys();
            }
            oldState = newState;
        }
        protected override void Draw(GameTime gameTime)
        {
            UpdateInput();
            base.Draw(gameTime);
        }
    }
}