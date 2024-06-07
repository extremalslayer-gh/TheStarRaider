using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace TheStarRaider.Code
{
    class StarShip
    {
        public int Speed { get; set; } = 6;
        private Vector2 position;
        public Color Color { get; set; } = Color.White;

        public bool Dead;
        public static int Health { get; private set; } = 5;

        public static void HealthReset()
        {
            Health = 5;
        }
        public static Texture2D Texture2D { get; set; }

        public StarShip(Vector2 pos)
        {
            position = pos;
            this.Dead = false;
        }

        public int GetSize = 100;
        public void GetHit(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                Dead = true;
        }

        public Vector2 GetPosition => new Vector2(position.X + 30, position.Y + 30);
      
        public void Up()
        {
            if (this.position.Y > 0) this.position.Y -= Speed;
        }


        public void Down()
        {
            if (this.position.Y < StarRaider.Height - 160) this.position.Y += Speed;
        }
        public void Left()
        {
            if (this.position.X > 0) this.position.X -= Speed;
        }
        public void Right()
        {
            if (this.position.X < StarRaider.Width - 100) this.position.X += Speed;
        }

        public void Draw()
        {
            StarRaider.SpriteBatch.Draw(Texture2D, position, Color);
        }
    }
}
