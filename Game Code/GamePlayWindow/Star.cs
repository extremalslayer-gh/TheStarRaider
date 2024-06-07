using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace TheStarRaider.Code
{
    class Star
    {
        private Vector2 position;
        private Vector2 direction;
        private Color color;

        public static Texture2D Texture2D { get; set; }

        public Star(Vector2 pos, Vector2 dir)
        {
            position = pos;
            direction = dir;
        }

        public Star(Vector2 dir)
        {
            direction = dir;
            RandomSet();
        }

        public void Update()
        {
            position += direction;
            if (position.X < 0)
                RandomSet();
        }

        public void RandomSet()
        {
            position = new Vector2(StarRaider.GetRandom(StarRaider.Width, StarRaider.Width + 300),
                                    StarRaider.GetRandom(0, StarRaider.Height));
            color = Color.FromNonPremultiplied(StarRaider.GetRandom(0, 256), StarRaider.GetRandom(0, 256), StarRaider.GetRandom(0, 256), 100);
        }

        public void Draw()
        {
            StarRaider.SpriteBatch.Draw(Texture2D, position, color);
        }
    }
}
