using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace BloomShootGame;

public class PlayerLocal
{
    private Vector2 _position;
    private Texture2D _texture;
    
    private int _width, _height;
    
    public PlayerLocal(Vector2 position, GraphicsDevice graphicsDevice)
    {

        _width = 30; _height = 30;
        _position = new Vector2(position.X - _width/2, position.Y - _height/2);
        
        // TODO: vyměnit za texturu
        _texture = new Texture2D(graphicsDevice, 35, 35);
        Color[] colors = new Color[35 * 35];

        for (int i = 0; i < 35; i++)
        {
            for (int j = 0; j < 35; j++)
            {
                colors[i + j * 35] = Color.Red;
            }
        }
        
        _texture.SetData(colors);
    }
    
    public Vector2 Position => _position;

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }
}