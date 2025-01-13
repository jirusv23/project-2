using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BloomShootGame;

public class BloomShootGameProgram : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private PlayerLocal _playerLocal;
    private PlayerOther _playerOther;
    private SpriteFont _font;
    
    private Vector2 _middleOfScreen;

    public BloomShootGameProgram()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.IsFullScreen = true;
        
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _middleOfScreen = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        
        _playerLocal = new PlayerLocal(GraphicsDevice, _middleOfScreen);
        _playerOther = new PlayerOther(GraphicsDevice, _middleOfScreen);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("Font1");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        var KeyboardState = Keyboard.GetState();
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Vector2 direction = Vector2.Zero;
        
        if (KeyboardState.IsKeyDown(Keys.W)) { direction.Y -= 1; }
        if (KeyboardState.IsKeyDown(Keys.S)) { direction.Y += 1; }
        if (KeyboardState.IsKeyDown(Keys.A)) { direction.X -= 1; }
        if (KeyboardState.IsKeyDown(Keys.D)) { direction.X += 1; }
        
        _playerLocal.Move(direction);

        base.Update(gameTime);

    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        
        _playerLocal.Draw(_spriteBatch);
        _playerOther.Draw(_spriteBatch);
        
        // souřadnice od středu
        _spriteBatch.DrawString(_font,  (int)(_playerLocal.Position.X - _graphics.PreferredBackBufferWidth/2) + _playerLocal._width/2 + "    " + (int)(_playerLocal.Position.Y - _graphics.PreferredBackBufferHeight / 2 + _playerLocal._height/2), Vector2.Zero, Color.White);
        // normální souřadnice
        _spriteBatch.DrawString(_font, $"{(int)_playerLocal.Position.X} | {(int)_playerLocal.Position.Y}", new Vector2(_graphics.PreferredBackBufferWidth - 90, 0), Color.White);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void UnloadContent()
    {
        // TODO: zde zavřít klienta pro multiplayer
    }
}