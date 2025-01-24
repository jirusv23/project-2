using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BloomShootGame.Menu;


public class textInputBox
{
    private Texture2D _texture;
    private string _text;
    private Rectangle _rectangle;
    private SpriteFont _font;
    private Vector2 _position;
    public Vector2 Position => _position;
    private Vector2 _size;

    public textInputBox(Vector2 position, GraphicsDevice graphicsDevice, int width, int height, SpriteFont font)
    {
        _texture = new Texture2D(graphicsDevice, 1, 1);
        _texture.SetData(new Color[] { Color.White });

        _font = font;
        _position = position;
        _size = new Vector2(width, height);

        _rectangle = new Rectangle((int)position.X - 15, (int)position.Y - 15, width, height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _rectangle, Color.White);

        if (_text != null)
        {
            spriteBatch.DrawString(_font, _text, _position, Color.Black);
        }
    }

    public void UpdateText(string textToAdd)
    {
        _text += textToAdd;
    }

    public void DelLetter()
    {
        if (_text.Length > 0)
            _text = _text.Remove(_text.Length - 1, 1);
    }

    public string GetText()
    {
        string text = _text;
        //_text = string.Empty;

        return text;
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