using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStarRaider.Code;

namespace TheStarRaider.Game_Code.GameWindow
{
    class GameDead
    {
        public static SpriteFont Font { get; set; }

        private static Vector2 textPosition
            => new Vector2(1200 / 2, 600 / 2);

        private static Vector2 textGap
            => new Vector2(0, 100);
        public static void Draw(SpriteBatch spriteBatch)
        {
            var orderedNumber = 1;
            spriteBatch.DrawString(Font, "You Died!", textPosition, Color.Red);
            spriteBatch.DrawString(Font, "Destroyed: " + StarRaider.DestroyedCount.ToString(), textPosition + orderedNumber++ * textGap, Color.White);
            spriteBatch.DrawString(Font, "Total time: " + StarRaider.TimeCounter.ToString(), textPosition + orderedNumber++ * textGap, Color.White);
            spriteBatch.DrawString(Font, "Press ENTER to Restart", textPosition + orderedNumber++ * textGap, Color.Yellow);
            spriteBatch.DrawString(Font, "Press ESCAPE to Exit", textPosition + orderedNumber++ * textGap, Color.Yellow);
        }
    }
}
