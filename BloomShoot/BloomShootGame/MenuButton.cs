using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BloomShootGame;

public class MenuButton
{
    private Vector2 _position;
    private Vector2 _size;
    private Color _color;

    private Texture2D _texture;
    
    private int _action;
    private string _actionText;
    
    public MenuButton(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, Color color, int action)
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
        }

        _texture = new Texture2D(graphicsDevice, (int)_size.X, (int)_size.Y);
        Color[] colorData = new Color[_texture.Width * _texture.Height];
        
        for (int i = 0; i < colorData.Length; i++)
        {
            colorData[i] = _color;  // Set the color for each pixel
        }
        
        _texture.SetData(colorData);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }
}