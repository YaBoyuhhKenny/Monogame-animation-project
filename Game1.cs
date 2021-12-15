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
        Rectangle jetRectangle;
        Vector2 jetSpeed;

        Texture2D skyTexture;
        Rectangle skyRectangle;
        Vector2 skySpeed;
        Texture2D skyTexture2;
        Rectangle skyRectangle2;
        Vector2 skySpeed2;

        Texture2D bombTexture;
        Rectangle bombRectangle;
        Vector2 bombSpeed;

        Texture2D targetTexture;

        Texture2D explosion;
        SoundEffect explode;
        SoundEffectInstance explodeInstance;

        SoundEffect music;
        SoundEffectInstance musicInstance;


        bool detonated = false;
        


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
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            skyRectangle = new Rectangle(0, 0, 800, 500);
            skySpeed = new Vector2(-40, 0);

            skyRectangle2 = new Rectangle(800, 0, 800, 500);
            skySpeed2 = new Vector2(-40, 0);

            jetRectangle = new Rectangle(0, -50, 574, 252);
            jetSpeed = new Vector2(35, 0);

            bombRectangle = new Rectangle(357, 0, 182, 38);
            bombSpeed = new Vector2(0, 5);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            jetTexture = Content.Load<Texture2D>("F-4e");
            skyTexture = Content.Load<Texture2D>("sky");
            skyTexture2 = Content.Load<Texture2D>("sky");
            bombTexture = Content.Load<Texture2D>("ground_bomb");
            explode = Content.Load<SoundEffect>("explode");
            targetTexture = Content.Load<Texture2D>("bunker");
            explosion = Content.Load<Texture2D>("explosion");
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


                skyRectangle.X += (int)skySpeed.X;
                skyRectangle2.X += (int)skySpeed2.X;
                if (skyRectangle.Right == 800)
                {
                    skyRectangle2 = new Rectangle(800, 0, 800, 500);
                }
                if (skyRectangle2.Right == 800)
                {
                    skyRectangle = new Rectangle(800, 0, 800, 500);
                }

                if ((float)gameTime.TotalGameTime.TotalSeconds > 60)
                    currentScreen = Screen.Target;

            }
            else if (currentScreen == Screen.Target)
            {
                jetRectangle.X += (int)jetSpeed.X;
                bombRectangle.Y += (int)bombSpeed.Y;
                if (bombRectangle.Y > _graphics.PreferredBackBufferHeight)
                {
                    currentScreen = Screen.End;
                    detonated = true;
                }
            }
            else if (currentScreen == Screen.End)
            {
                musicInstance.Stop();
                if (detonated == true)
                {
                    explodeInstance.Play();
                    detonated = false;
                }
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
                _spriteBatch.DrawString(startFont, "Bunker Buster", new Vector2(200, 160), Color.Gold);
            }
            else if (currentScreen == Screen.Flight)
            {
                _spriteBatch.Draw(skyTexture, skyRectangle, Color.White);
                _spriteBatch.Draw(skyTexture2, skyRectangle2, Color.White);
                _spriteBatch.Draw(jetTexture, jetRectangle, Color.White);
            }
            else if (currentScreen == Screen.Target)
            {
                _spriteBatch.Draw(targetTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(jetTexture, jetRectangle, Color.White);
                if (jetRectangle.X > 70)
                    _spriteBatch.Draw(bombTexture, bombRectangle, Color.White);
                
            }
            else if (currentScreen == Screen.End)
            {
                _spriteBatch.Draw(explosion, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            }


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
