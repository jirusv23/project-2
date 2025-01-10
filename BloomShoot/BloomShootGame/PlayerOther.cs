using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BloomShootGame;


public class PlayerOther
{
    private Vector2 _position;
    public Vector2 Position => _position;
    
    private List<Bullet> _bullets;
    
    public PlayerOther(GraphicsDevice graphicsDevice, Vector2 position)
    {
        _position = new Vector2();
    }

    public void SetPosition(Vector2 position) { _position = position; }
}