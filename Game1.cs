using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_animation_project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D jetTexture;
        Texture2D skyTexture;
        Texture2D skyTexture2;
        Texture2D missileTexture;
        Texture2D explosion;
        SoundEffect explode;
        SoundEffectInstance explodeInstance;
        SoundEffect music;
        SoundEffectInstance musicInstance;
        bool detonated;
        float seconds;
        float startTime;
        MouseState mouseState;
        SpriteFont startFont;
        Screen currentScreen;

        enum Screen
        {
            Intro,
            Flight,
            Target,
            End
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            jetTexture = Content.Load<Texture2D>("F-4e");
            skyTexture = Content.Load<Texture2D>("sky");
            missileTexture = Content.Load<Texture2D>("missile");
            explode = Content.Load<SoundEffect>("explosion.wav");
            explosion = Content.Load<Texture2D>("explosion.jpg");
            explodeInstance = explode.CreateInstance();
            music = Content.Load<SoundEffect>("Magic Spear I");
            musicInstance = music.CreateInstance();
            startFont = Content.Load<SpriteFont>("Start");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (currentScreen == Screen.Intro)
            {
                musicInstance.Play();
                if (mouseState.LeftButton == ButtonState.Pressed)
                    currentScreen = Screen.Flight;
            }
            else if (currentScreen == Screen.Flight)
            {

            }
            else if (currentScreen == Screen.Target)
            {

            }
            else if (currentScreen == Screen.End)
            {
                musicInstance.Stop();
                explodeInstance.Play();
                if (explodeInstance.State == SoundState.Stopped)
                    Exit();
            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (currentScreen == Screen.Intro)
            {
                _spriteBatch.Draw(skyTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(startFont, "Bunker Buster", new Vector2(10, 30), Color.Gold);
                if (mouseState.LeftButton == ButtonState.Pressed)
                    currentScreen = Screen.Flight;
            }
            else if (currentScreen == Screen.Flight)
            {
                

            }
            else if (currentScreen == Screen.Target)
            {

            }
            else if (currentScreen == Screen.End)
            {
                musicInstance.Stop();
                explodeInstance.Play();
                if (explodeInstance.State == SoundState.Stopped)
                    Exit();
            }


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
