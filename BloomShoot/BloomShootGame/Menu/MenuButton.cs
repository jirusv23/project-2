using System;
using System.Net.Http.Headers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BloomShootGame.Menu;

public class MenuButton
{
    private Vector2 _position;
    private Vector2 _size;
    private Color _color;

    private Texture2D _texture;

    private int _action;
    private string _actionText;

    private Vector2 _textSize;

    public MenuButton(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, Color color, int action, SpriteFont font)
    {
        _position = position;
        _size = size;
        _color = color;
        _action = action;

        switch (_action)
        {
            case 1:
                _actionText = "Singleplayer";
                break;
            case 0:
                _actionText = "Multiplayer";
                break;
            case 2:
                _actionText = "Exit";
                break;
            case 3:
                _actionText = "Settings";
                break;
        }

        _textSize = font.MeasureString(_actionText);

        _texture = new Texture2D(graphicsDevice, (int)_size.X, (int)_size.Y);
        Color[] colorData = new Color[_texture.Width * _texture.Height];

        for (int i = 0; i < colorData.Length; i++)
        {
            colorData[i] = _color;  // Set the color for each pixel
        }

        _texture.SetData(colorData);
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
        spriteBatch.DrawString(font, _actionText, _position + _size / 2 - _textSize / 2, Color.Black);
    }

    public bool WithinBounds(Vector2 position)
    {
        if (_position.X < position.X && _position.X + _size.X > position.X)
        {
            if (_position.Y < position.Y && _position.Y + _size.Y > position.Y)
            {
                return true;
            }
        }
        return false;
    }
}