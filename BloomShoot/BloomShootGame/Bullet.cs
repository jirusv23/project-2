using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BloomShootGame;

public class Bullet
{
    private Vector2 _position; public Vector2 Position => _position;
    private Vector2 _velocity;
    
    private Texture2D _texture;
    private int radius = 5;
    
    public Bullet(GraphicsDevice graphicsDevice, Vector2 position)
    {
        _position = position;
        _velocity = new Vector2(0, 0);
        
        _texture = new Texture2D(graphicsDevice, radius, radius);
        CreateCircleTexture();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    public void CreateCircleTexture()
    {
        Color[] colorData = new Color[radius*radius];

        float diam = radius / 2f;
        float diamsq = diam * diam;

        for (int x = 0; x < radius; x++)
        {
            for (int y = 0; y < radius; y++)
            {
                int index = x * radius + y;
                Vector2 pos = new Vector2(x - diam, y - diam);
                if (pos.LengthSquared() <= diamsq)
                {
                    colorData[index] = Color.White;
                }
                else
                {
                    colorData[index] = Color.Transparent;
                }
            }
        }
        _texture.SetData(colorData);
    }
}