using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BloomShootGame
{
    internal class Border
    {
        private int height;
        private int width;

        public Vector2 PositionInWorld;
        public Vector2 PositionInViewport;

        private Texture2D _texture;
        private Color[,] colors;

        public Border(GraphicsDevice graphicsDevice, Vector2 positionInWorld, Color color, int height, int width) 
        {
            this.PositionInWorld = positionInWorld;
            this.width = width;
            this.height = height;


            _texture = new Texture2D(graphicsDevice, width, height);
            Color[] colors = new Color[width * height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    colors[i + j * width] = color;
                }
            }

            _texture.SetData(colors);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 PlayerMovement)
        {
            // Shifts the player by the playerMovement, gives sense of moving
            PositionInViewport = new Vector2(PositionInWorld.X - PlayerMovement.X,
                                             PositionInWorld.Y - PlayerMovement.Y);

            spriteBatch.Draw(_texture, PositionInViewport, Color.White);
        }
    }
}
