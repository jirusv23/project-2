using BloomShootGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;


namespace BloomShootGameSinglePlayer;

public class BloomShootGameSinglePlayerProgram : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Vector2 _middleOfScreen;
    
    private PlayerLocal _player;
    private List<BoulderEnemy> listBouldersEnemies = [];
    private Texture2D boulderEnemyTexture;
    private SpriteFont _font;

    public BloomShootGameSinglePlayerProgram()
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

        _font = Content.Load<SpriteFont>("font1");
        boulderEnemyTexture = Content.Load<Texture2D>("boulder_texture");
        _player = new PlayerLocal(GraphicsDevice, _middleOfScreen);

        listBouldersEnemies.Add(new BoulderEnemy(boulderEnemyTexture, _player.PlayerMovement, new Vector2(250, 200)));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var KeyboardState = Keyboard.GetState();
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Vector2 direction = Vector2.Zero;
        
        if (KeyboardState.IsKeyDown(Keys.W)) { direction.Y -= 1; }
        if (KeyboardState.IsKeyDown(Keys.S)) { direction.Y += 1; }
        if (KeyboardState.IsKeyDown(Keys.A)) { direction.X -= 1; }
        if (KeyboardState.IsKeyDown(Keys.D)) { direction.X += 1; }
        
        _player.Move(direction);

        foreach (BoulderEnemy boulder in listBouldersEnemies)
        {
            if (KeyboardState.IsKeyDown(Keys.T)) { boulder.DebugMovement(); };
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        
        _player.Draw(_spriteBatch);

        foreach (BoulderEnemy boulder in listBouldersEnemies)
        {
            boulder.Draw(_spriteBatch, _player.PlayerMovement);
            _spriteBatch.DrawString(_font, $"{boulder.viewportPosition.X}      {boulder.viewportPosition.Y}", Vector2.Zero, Color.Red);
        }
        _spriteBatch.DrawString(_font, $"{_player.PlayerMovement.X}      {_player.PlayerMovement.Y}", new Vector2(_graphics.PreferredBackBufferWidth - 90, 0), Color.Red);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}