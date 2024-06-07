using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TheStarRaider.Code;

namespace TheStarRaider
{
    class HeadWindow
    {
        public static Texture2D background { get; set; }
        static int timeCounter = 0;
        static Color color;
        public static SpriteFont Font { get; set; }
        static Vector2 textPosition = new Vector2(100, 100);

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 1680, 1050), Color.White);
            spriteBatch.DrawString(Font, "STAR RAIDER!", textPosition, Color.Yellow);
            spriteBatch.DrawString(Font, "To move use W, A, S, D or arrows", textPosition + new Vector2(0, 100), Color.OrangeRed);
            spriteBatch.DrawString(Font, "To shoot use left control", textPosition + new Vector2(0, 200), Color.OrangeRed);
            spriteBatch.DrawString(Font, "Press Space to start game", textPosition + new Vector2(0, 300), color);
        }

        public static void Update()
        {
            color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 256);
            timeCounter++;
        }
    }
}
