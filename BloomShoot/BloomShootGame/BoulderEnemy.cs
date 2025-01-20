using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BloomShootGame
{
    // Enemy which will only float and act as a mindless asteroid
    // Will split up on death into smaller asteroids
    internal class BoulderEnemy
    {
        Texture2D _texture;

        // Position in world mean the position in regards of the game cordinates which start at [0, 0] in the middle of the screen
        internal Vector2 PositionInWorld;
        // Position in regards the window cordinates (from top left corner), used for drawing
        internal Vector2 viewportPosition;
        internal Rectangle rectangleBoulder;

        // How much the asset will be scaled on drawing, used in calculations
        int scalingFactor = 1;

        public BoulderEnemy(Texture2D sourceTexture, Vector2 PlayerMovement, Vector2 SpawnPosition)
        {
            // PlayerMovement mean how much the player moved so we can shift the position
            _texture = sourceTexture;
            
            PositionInWorld = new Vector2(SpawnPosition.X, SpawnPosition.Y);
        }

        public void DebugMovement()
        {
            PositionInWorld.X += 5;
        }

        public void Draw(SpriteBatch _spriteBatch, Vector2 PlayerMovement)
        {
            // Draws the player and updates the viewportPosition to math it with PlayerMovement
            // PlayerMovement is value by how much the player moved so we can shift every enemy accordíngly

            viewportPosition.X = PositionInWorld.X - PlayerMovement.X;
            viewportPosition.Y = PositionInWorld.Y - PlayerMovement.Y;

            _spriteBatch.Draw(_texture, viewportPosition, null, Color.White, 0f, Vector2.Zero, scalingFactor, SpriteEffects.None, 1f);
        }
    }
}
