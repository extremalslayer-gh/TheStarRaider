using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata.Ecma335;
using System;
namespace TheStarRaider.Code
{
    class Asteroid
    {
        private Vector2 position;
        private Color color;
        private Vector2 direction;
        const int Scale = 200;

        public Vector2 GetPosition { get { return position; } private set { } }
        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public float Size { get; private set; }
        public float Rotation { get; private set; } = 0;
        public float RotationSpeed { get; private set; } = 0;

        private static Vector2 center
            => new Vector2(Asteroid.Texture2D.Width / 2, Asteroid.Texture2D.Height / 2);

        public static Texture2D Texture2D { get; set; }

        public Asteroid(Vector2 pos, Vector2 dir, float size, float rotation, float rotationSpeed)
        {
            this.position = pos;
            this.Direction = dir;
            this.Size = size;
            this.Rotation = rotation;
            this.RotationSpeed = rotationSpeed;
        }

        public Asteroid()
        {
            RandomSet();
        }

        public Asteroid(Vector2 dir)
        {
            this.Direction = dir;
            RandomSet();
        }

        public double GetDistance(Vector2 obj1, Vector2 obj2)
            => Math.Sqrt((obj1.X - obj2.X) * (obj1.X - obj2.X) + (obj1.Y - obj2.Y) * (obj1.Y - obj2.Y));

        public void CheckIntersects(StarShip starShip)
        {
            if (GetDistance(starShip.GetPosition, position) <= 15)
            {
                starShip.GetHit(1);
            }
        }

        public void Update()
        {
            position += direction;
            Rotation += RotationSpeed;

            if (position.Y > StarRaider.Height)
            {
                RandomSet();
            }
        }

        public void RandomSet()
        {
            var fraction = 50f;
            //random.Next(min, max) + random.NextDouble()) / fraction) * fraction;
            position = new Vector2((StarRaider.GetRandom(20, StarRaider.Width - 20) + (float)StarRaider.random.NextDouble()) / fraction * fraction,
            StarRaider.GetRandom(-150, -100));
            Size = (float)StarRaider.GetRandom(21, 51) / Scale;
            Direction = new Vector2(0, StarRaider.GetRandom(2, 10));

            RotationSpeed = (float)StarRaider.GetRandom(1, 3) / Scale;
            color = Color.White;
        }

        public void Draw()
        {
            StarRaider.SpriteBatch.Draw(Texture2D, position, null, color, Rotation, center, Size,
                SpriteEffects.None, 0);
        }
    }
}
