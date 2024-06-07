using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace TheStarRaider.Code
{
    class Shot
    {
        private Vector2 position;
        private Vector2 direction;
        public Color color = Color.White;
        const int speed = 5;
        public static int Size = 30;
        public static Texture2D Texture2D { get; set; }
        public Vector2 GetPosition { get { return position; } private set { } }
        public Shot(Vector2 pos)
        {
            position = pos;
            direction = new Vector2(0, speed);
        }

        public void Update()
        {
            if (position.Y >= 0)
            {
                position -= direction;
            }
        }

        public bool IsHidden => position.Y <= 0;

        public void Draw()
        {
            StarRaider.SpriteBatch.Draw(Texture2D, position, color);
        }
    }
}
