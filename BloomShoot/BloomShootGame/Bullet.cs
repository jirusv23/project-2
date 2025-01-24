using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BloomShootGame;

public class Bullet
{
    private Vector2 _position; public Vector2 Position => _position;
    private Vector2 _velocity;
    
    private Texture2D _texture;
    private int radius = 5;
    
    public Bullet(GraphicsDevice graphicsDevice, Vector2 position, Color playerColor)
    {
        _position = position;
        _velocity = new Vector2(0, 0);
        
        _texture = new Texture2D(graphicsDevice, radius, radius);
        CreateCircleTexture(playerColor);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    public void CreateCircleTexture(Color bulletColor)
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
                    colorData[index] = bulletColor;
                }
                else
                {
                    colorData[index] = Color.Transparent;
                }
            }
        }
        _texture.SetData(colorData);
    }

    private Vector2 _directionOfBullet(Vector2 position, Vector2 mousePosition)
    {
        Vector2 direction = mousePosition - position;
        direction.Normalize();
        
        Console.WriteLine(direction);
        
        return direction;
    }
}