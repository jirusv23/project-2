using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BloomShootGame.Menu;

public class Settings : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    private Vector2 _middleOfScreen;

    private float _firstButtonHeight = 100f; private float _buttonDistance = 150f;
    private Vector2 _buttonSize = new Vector2(250, 50);

    public Settings()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _middleOfScreen = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _font = Content.Load<SpriteFont>("ButtonFont");
    }

    protected override void Update(GameTime gameTime)
    {
        var KeyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();

        if (KeyboardState.IsKeyDown(Keys.Escape)) 
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkBlue);

        _spriteBatch.Begin();


        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void UnloadContent() 
    {

    }
}