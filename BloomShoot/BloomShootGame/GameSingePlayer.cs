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

    private PlayerLocal _mainPlayer;
    private List<BoulderEnemy> listBouldersEnemies = [];
    private Texture2D boulderEnemyTexture;
    private SpriteFont _font;

    private Texture2D[] BackgroundTextureList;
    private BackgroundManager BackgroundManager;

    private Border[] listBorder;
    private int borderEdgeDistance = 4;
    private int borderThickness = 3;

    private Vector2Visualizer visuliazer;
    // border is left from left top corder by -(windowWidth/borderEdgeDistance)
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

        // Camera follows this player
        _mainPlayer = new PlayerLocal(GraphicsDevice, _middleOfScreen);

        listBouldersEnemies.Add(new BoulderEnemy(boulderEnemyTexture, _mainPlayer.PlayerMovement, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2)));
        visuliazer = new Vector2Visualizer(GraphicsDevice, _mainPlayer._position);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        listBorder =
        [
            // In order: left, right, top, down
            new Border(GraphicsDevice, new Vector2(-_graphics.PreferredBackBufferWidth/borderEdgeDistance,-_graphics.PreferredBackBufferHeight/borderEdgeDistance), Color.Red, _graphics.PreferredBackBufferHeight + (borderThickness*_graphics.PreferredBackBufferHeight)/borderEdgeDistance, borderThickness),
            new Border(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth + _graphics.PreferredBackBufferWidth/borderEdgeDistance,-_graphics.PreferredBackBufferHeight/borderEdgeDistance),Color.Red,_graphics.PreferredBackBufferHeight + (borderThickness*_graphics.PreferredBackBufferHeight)/borderEdgeDistance,borderThickness),
            new Border(GraphicsDevice, new Vector2(-_graphics.PreferredBackBufferWidth/borderEdgeDistance,-_graphics.PreferredBackBufferHeight/borderEdgeDistance), Color.Red,borderThickness,_graphics.PreferredBackBufferWidth + (borderThickness*_graphics.PreferredBackBufferWidth)/borderEdgeDistance),
            new Border(GraphicsDevice, new Vector2(-_graphics.PreferredBackBufferWidth/borderEdgeDistance,_graphics.PreferredBackBufferHeight + _graphics.PreferredBackBufferHeight/borderEdgeDistance),Color.Red,borderThickness,_graphics.PreferredBackBufferWidth + (borderThickness*_graphics.PreferredBackBufferWidth)/borderEdgeDistance)
        ];

        BackgroundTextureList = [
            Content.Load<Texture2D>("space_background")
        ];

        BackgroundManager = new BackgroundManager(_spriteBatch, BackgroundTextureList, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
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

        // Handles player movement
        Vector2 direction = Vector2.Zero;

        if (KeyboardState.IsKeyDown(Keys.W)) { direction.Y -= 1; }
        if (KeyboardState.IsKeyDown(Keys.S)) { direction.Y += 1; }
        if (KeyboardState.IsKeyDown(Keys.A)) { direction.X -= 1; }
        if (KeyboardState.IsKeyDown(Keys.D)) { direction.X += 1; }

        _mainPlayer.Move(direction);

        if (KeyboardState.IsKeyDown(Keys.H))
        {
            _mainPlayer._position = new Vector2(_mainPlayer._position.X += 15, _mainPlayer._position.Y += 6);
            _mainPlayer.PlayerMovement = new Vector2(_mainPlayer.PlayerMovement.X += 15, _mainPlayer.PlayerMovement.Y += 6);

            base.Update(gameTime);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        BackgroundManager.Draw(_mainPlayer.PlayerMovement);

        _mainPlayer.Draw(_spriteBatch);
        _mainPlayer.Update();

        foreach (BoulderEnemy boulder in listBouldersEnemies)
        {
            boulder.Draw(_spriteBatch, _mainPlayer.PlayerMovement);
        }

        foreach (Border border in listBorder)
        {
            border.Draw(_spriteBatch);
            border.Update(_mainPlayer.PlayerMovement);

            if (border.borderRectangle.Intersects(_mainPlayer.playerRectangle))
            {
                Debug.WriteLine("DD");
            }
        }


        visuliazer.DrawVector(_spriteBatch, _mainPlayer._velocity*10);


        _spriteBatch.End();
        base.Draw(gameTime);

    }
}