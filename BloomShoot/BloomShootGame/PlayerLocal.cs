using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace BloomShootGame;

public class PlayerLocal
{
    private Vector2 _position;
    private Texture2D _texture;
    
    public int _width, _height;

    private Vector2 _velocity, _acceleration, _deceleration;
    
    public PlayerLocal(Vector2 position, GraphicsDevice graphicsDevice)
    {

        _width = 30; _height = 30;
        _position = new Vector2(position.X - _width/2, position.Y - _height/2);
        
        _velocity = Vector2.Zero;
        _acceleration = new Vector2(0.1f, 0.1f);
        _deceleration = new Vector2(0.05f, 0.05f);
        
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

    public void Move(Vector2 direction)
    {
        _velocity += _acceleration*direction;
        
        if (_velocity.X > 3) _velocity.X = 3;
        if (_velocity.Y > 3) _velocity.Y = 3;

        if (_velocity.X > 0) { _velocity.X -= _deceleration.X; }
        else if (_velocity.X < 0) { _velocity.X += _deceleration.X; }
        if (_velocity.Y > 0) { _velocity.Y -= _deceleration.Y; }
        else if (_velocity.Y < 0) { _velocity.Y += _deceleration.Y; }
        
        _position += _velocity;
    }
}