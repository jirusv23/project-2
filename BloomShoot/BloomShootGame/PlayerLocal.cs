using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace BloomShootGame;

public class PlayerLocal
{
    // change _position and _velocity, _acceleration, _deceleration => private (debuging)
    internal Vector2 _position;
    public Vector2 Position => _position;
    // PlayerMovement is value by how much the player moved so we can shift every enemy accordíngly, also serves as player coordinate (as long as they spawn on [0, 0]
    public Vector2 PlayerMovement;
    public Rectangle playerRectangle;

    private float _rotation; public float Rotation => _rotation;
    private Texture2D _texture;

    public int _width, _height;

    internal Vector2 _velocity, _acceleration, _deceleration;
    private List<Bullet> _bullets;

    public PlayerLocal(GraphicsDevice graphicsDevice, Vector2 position)
    {
        _width = 30; _height = 30;
        PlayerMovement = Vector2.Zero;
        _position = new Vector2(position.X - _width / 2, position.Y - _height / 2);
        _rotation = 0f;

        _velocity = Vector2.Zero;
        _acceleration = new Vector2(0.1f, 0.1f);
        _deceleration = new Vector2(0.05f, 0.05f);

        playerRectangle = new Rectangle((int)_position.X, (int)_position.Y, _width, _height);

        // TODO: vyměnit za texturu
        _texture = new Texture2D(graphicsDevice, _width, _height);
        Color[] colors = new Color[_height * _width];

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                colors[i + j * _width] = Color.Red;
            }
        }

        _texture.SetData(colors);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    public void Move(Vector2 direction)
    {
        _velocity += _acceleration * direction;

        if (_velocity.X > 10) _velocity.X = 10;
        if (_velocity.X < -10) _velocity.X = -10;
        if (_velocity.Y > 10) _velocity.Y = 10;
        if (_velocity.Y < -10) _velocity.Y = -10;

        if (_velocity.X > 0) { _velocity.X -= _deceleration.X; }
        else if (_velocity.X < 0) { _velocity.X += _deceleration.X; }
        if (_velocity.Y > 0) { _velocity.Y -= _deceleration.Y; }
        else if (_velocity.Y < 0) { _velocity.Y += _deceleration.Y; }

        if (double.Abs(_velocity.X) < 0.05f) { _velocity.X = 0; }
        if (double.Abs(_velocity.Y) < 0.05f) { _velocity.Y = 0; }
    }

    public void Update()
    {
        PlayerMovement += _velocity;
        playerRectangle = new Rectangle((int)_position.X, (int)_position.Y, _width, _height);
    }

    public void HitAWall(int direction, Rectangle borderRect)
    {
        // direction: 0 left, 1 right, 2 top, 3 down
        float velocityDecreaseMultiplier = 0.8f;

        if (direction == 0)  // left 
            {
            PlayerMovement.X += 1;
            // makes sure the player can't shift throught at low speed
            _velocity.X *= -1;
            // flips the velocity
            _velocity *= velocityDecreaseMultiplier;
            // decreases speed
            }
        else if (direction == 1) //right
        {
            PlayerMovement.X += -1;
            _velocity.X *= -1;
            _velocity *= velocityDecreaseMultiplier;
        }

        if (direction == 2)  // top 
        {
            PlayerMovement.Y += 1;
            _velocity.Y *= -1;
            _velocity *= velocityDecreaseMultiplier;
        }
        else if (direction == 3) //down
        {
            PlayerMovement.Y += -1;
            _velocity.Y *= -1;
            _velocity *= velocityDecreaseMultiplier;
        }


    }
}