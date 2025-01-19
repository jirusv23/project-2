using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BloomShootGame;


public class PlayerOther
{
    private Vector2 _position;
    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    private float _rotation; public float Rotation
    {
        get => _rotation;
        set => _rotation = value;
    }

    private List<Bullet> _bullets;
    private Texture2D _texture;
    
    public PlayerOther(GraphicsDevice graphicsDevice, Vector2 position)
    {
        _position = new Vector2();
        _rotation = 0;
        
        // TODO: vyměnit za texturu
        _texture = new Texture2D(graphicsDevice, 35, 35);
        Color[] colors = new Color[35 * 35];

        for (int i = 0; i < 35; i++)
        {
            for (int j = 0; j < 35; j++)
            {
                colors[i + j * 35] = Color.Orange;
            }
        }
        
        _texture.SetData(colors);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }
    
    public void SetPosition(Vector2 position) { _position = position; }
}