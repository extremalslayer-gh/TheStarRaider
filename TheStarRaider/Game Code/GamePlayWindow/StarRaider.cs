using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
namespace TheStarRaider.Code
{
    class StarRaider
    {
        public static int Width, Height;
        public static Random random = new Random();
        public static SpriteBatch SpriteBatch;
        private static Star[] stars;
        private static List<Shot> shots = new List<Shot>();
        private static Asteroid[] asteroids;
        public static StarShip StarShip { get; set; }
        public static int DestroyedCount { get; private set; } = 0;
        public static Texture2D background { get; set; }
        public static SpriteFont Font { get; set; }
        public static int GetRandom(int min, int max) => random.Next(min, max);

        public static void ShipShot() => shots.Add(new Shot(StarShip.GetPosition + new Vector2(10, 0)));
        public static void DestroyedCountReset() => DestroyedCount = 0;
        public static int TimeCounter { get; private set; } = 0;
        public static void Init(SpriteBatch SpriteBatch, int Width, int Height)
        {
            StarRaider.Width = Width;
            StarRaider.Height = Height;
            StarRaider.SpriteBatch = SpriteBatch;
            asteroids = new Asteroid[10];
            stars = new Star[75];

            for (int i = 0; i < stars.Length; i++)
                stars[i] = new Star(new Vector2(-random.Next(1, 10), 0));

            for (int i = 0; i < asteroids.Length; i++)
                asteroids[i] = new Asteroid();

            StarShip = new StarShip(new Vector2(StarRaider.Width / 2 - 100, StarRaider.Height - 160));
        }

        public static void Update()
        {
            foreach (var star in stars)
                star.Update();

            for (var i = 0; i < shots.Count; i++)
            {
                shots[i].Update();
                if (shots[i].IsHidden)
                {
                    shots.RemoveAt(i);
                    i--;
                }
            }

            foreach (var asteroid in asteroids)
                asteroid.Update();

            if (IsCollideStarshipAndAsteroids())
            {
                StarShip.Color = Color.Red;
            }
            
            else
            {
                StarShip.Color = Color.White;
            }

            if (IsBulletCollideWithAsteroid())
            {
                DestroyedCount++;
            }

            TimeCounter++;
        }

        public static void ResetTimer()
        {
            TimeCounter = 0;
        }
        public static bool IsCollide(Vector2 pos1, float size1, Vector2 pos2, float size2)
        {

            Rectangle goodSpriteRect = new Rectangle((int)pos1.X,
                    (int)pos1.Y, (int)size1, (int)size1);

            Rectangle evilSpriteRect = new Rectangle((int)pos2.X,
                    (int)pos2.Y, (int)size2, (int)size2);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }

        public static bool IsCollideStarshipAndAsteroids()
        {
            for (int i = 0; i < asteroids.Length; i++)
            {
                var asteroid = asteroids[i];
                var asteroidPos = asteroid.GetPosition;
                int asteroidSize = (int)asteroid.Size;
                if (IsCollide(StarShip.GetPosition, StarShip.GetSize, asteroid.GetPosition, asteroid.Size + 50))
                {
                    StarShip.GetHit(1);
                    asteroids[i].RandomSet();
                    return true;
                }
            }

            return false;
        }

        public static bool IsBulletCollideWithAsteroid()
        {
            for (int i = 0; i < shots.Count; i++)
            {
                var shot = shots[i];
                for (int j = 0; j < asteroids.Length; j++)
                {
                    var asteroid = asteroids[j];
                    if (IsCollide(shot.GetPosition, Shot.Size + 10, asteroid.GetPosition, asteroid.Size + 50))
                    {
                        asteroids[j].RandomSet();
                        shots.RemoveAt(i);
                        i--;
                        return true;
                    }
                }
            }

            return false;
        }

        public static void ResetAsteroids()
        {
            for (int i = 0; i < asteroids.Length; i++)
                asteroids[i] = new Asteroid();
        }
        public static void Draw()
        {
            foreach (var star in stars)
                star.Draw();

            foreach (var shot in shots)
                shot.Draw();

            foreach (var asteroid in asteroids)
                asteroid.Draw();

            StarShip.Draw();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "time:" + TimeCounter.ToString(), new Vector2(50, 100), Color.Yellow);
            spriteBatch.DrawString(Font, "Destroyed: " + DestroyedCount.ToString(), new Vector2(50, 150), Color.Yellow);
            spriteBatch.DrawString(Font, "Health:" + StarShip.Health.ToString(), new Vector2(50, 200), Color.Red);

        }


    }
}
