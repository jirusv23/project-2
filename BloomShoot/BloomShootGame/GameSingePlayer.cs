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

        listBouldersEnemies.Add(new BoulderEnemy(boulderEnemyTexture, _mainPlayer.PlayerMovement, new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight / 2)));

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

        Vector2 direction = Vector2.Zero;

        if (KeyboardState.IsKeyDown(Keys.W)) { direction.Y -= 1; }
        if (KeyboardState.IsKeyDown(Keys.S)) { direction.Y += 1; }
        if (KeyboardState.IsKeyDown(Keys.A)) { direction.X -= 1; }
        if (KeyboardState.IsKeyDown(Keys.D)) { direction.X += 1; }
        
        _mainPlayer.Move(direction);

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
        BackgroundManager.Draw(_mainPlayer.PlayerMovement);

        _mainPlayer.Draw(_spriteBatch);
        _mainPlayer.Update();

        foreach (BoulderEnemy boulder in listBouldersEnemies)
        {
            boulder.Draw(_spriteBatch, _mainPlayer.PlayerMovement);
        }

        //_spriteBatch.DrawString(_font, $"{_mainPlayer.PlayerMovement.X}           {_mainPlayer.PlayerMovement.Y}", new Vector2(_graphics.PreferredBackBufferWidth - 90, 0), Color.Red);

        foreach (Border border in listBorder)
        {
            border.Draw(_spriteBatch);
            border.Update(_mainPlayer.PlayerMovement);

            if (border.borderRectangle.Intersects(_mainPlayer.playerRectangle))
            {
                Debug.WriteLine("DD");
            }
        }

        if (listBorder[0].borderRectangle.Intersects(_mainPlayer.playerRectangle))
        {
            Debug.Write("dsasad");
        }

        _spriteBatch.DrawString(_font, $"PlayerRectangle X:{(int)_mainPlayer.playerRectangle.X}   Y:{(int)_mainPlayer.playerRectangle.Y}   Width:{(int)_mainPlayer.playerRectangle.Width}   Height:{(int)_mainPlayer.playerRectangle.Height}   ", new Vector2(0, 0), Color.Red);
        _spriteBatch.DrawString(_font, $"Border[0] X:{(int)listBorder[0].borderRectangle.X}   Y:{(int)listBorder[0].borderRectangle.Y}   Width:{(int)listBorder[0].borderRectangle.Width}   Height:{(int)listBorder[0].borderRectangle.Height}   ", new Vector2(0, 50), Color.Red);

        _spriteBatch.End();
        base.Draw(gameTime);

    }
}