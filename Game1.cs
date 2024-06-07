using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using TheStarRaider.Code;
using TheStarRaider.Game_Code.GameWindow;

namespace TheStarRaider
{
    enum State
    {
        HeadWindow,
        Game,
        GameDead
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private State State = State.HeadWindow;
        KeyboardState keyboardState, keyboardStateOld;
        private SoundEffect shotSong;
        private Song splashSong;
        private Song gameSong;
        private Song deadWindow;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1680;
            graphics.PreferredBackBufferHeight = 1050;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            SoundEffect.MasterVolume = 0.2f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            HeadWindow.background = Content.Load<Texture2D>("background");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            HeadWindow.Font = Content.Load<SpriteFont>("Font");
            StarRaider.Font = Content.Load<SpriteFont>("Font");
            GameDead.Font = Content.Load<SpriteFont>("Font");
            StarRaider.Init(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Star.Texture2D = Content.Load<Texture2D>("star");
            StarShip.Texture2D = Content.Load<Texture2D>("starship");
            Shot.Texture2D = Content.Load<Texture2D>("shot");
            Asteroid.Texture2D = Content.Load<Texture2D>("asteroid");

            shotSong = Content.Load<SoundEffect>("laser");
            splashSong = Content.Load<Song>("SplashAudio");
            gameSong = Content.Load<Song>("GameAudio");
            deadWindow = Content.Load<Song>("DeadWindow");
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            switch (State)
            {
                case State.HeadWindow:
                    ChangeMusic(splashSong);
                    MediaPlayer.IsRepeating = true;
                    if (keyboardState.IsKeyDown(Keys.Space))
                    {
                        State = State.Game;
                        ChangeMusic(gameSong);
                    }

                    HeadWindow.Update();
                    break;

                case State.Game:
                    StarRaider.Update();

                    if (StarRaider.StarShip.Dead)
                    {
                        State = State.GameDead;
                    }

                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        State = State.HeadWindow;
                        ChangeMusic(splashSong);
                    }

                    if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) StarRaider.StarShip.Up();
                    if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) StarRaider.StarShip.Down();
                    if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) StarRaider.StarShip.Left();
                    if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)) StarRaider.StarShip.Right();
                    if (keyboardState.IsKeyDown(Keys.LeftControl) && keyboardStateOld.IsKeyUp(Keys.LeftControl))
                    {
                        StarRaider.ShipShot();
                        shotSong.Play();
                    }

                    break;

                case State.GameDead:
                    ChangeMusic(deadWindow);
                    MediaPlayer.IsRepeating = false;
                    
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        State = State.HeadWindow;
                        ResetGame();
                    }

                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        Exit(); 
                    }

                    break;
            }

            HeadWindow.Update();
            base.Update(gameTime);
            keyboardStateOld = keyboardState;

        }

        public GameTime GetGameTime(GameTime gameTime) => gameTime;
        public void ResetGame()
        {
            StarShip.HealthReset();
            StarRaider.ResetTimer();
            StarRaider.DestroyedCountReset();
            StarRaider.StarShip = new StarShip(new Vector2(StarRaider.Width / 2 - 100, StarRaider.Height - 160));
            StarRaider.ResetAsteroids();
        }

        public void ChangeMusic(Song song)
        {
            if (MediaPlayer.Queue.ActiveSong != song)
                MediaPlayer.Play(song);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            switch (State)
            {
                case State.HeadWindow:
                    HeadWindow.Draw(spriteBatch);
                    break;

                case State.Game:
                    StarRaider.Draw();
                    StarRaider.Draw(spriteBatch);
                    break;

                case State.GameDead:
                    GameDead.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
