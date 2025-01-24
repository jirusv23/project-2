using BloomShootGame.Enemies;
using BloomShootGame.Enviroment;
using BloomShootGame.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;


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
    private Color borderColor = Color.Red;

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
        //visuliazer = new Vector2Visualizer(GraphicsDevice, _mainPlayer._position);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        borderColor = new Color(41, 0, 148, 125);
        listBorder =
        [
            // In order: left[0], right[1], top[2], down[3]
            new Border(GraphicsDevice,new Vector2(-_graphics.PreferredBackBufferWidth/borderEdgeDistance,-_graphics.PreferredBackBufferHeight/borderEdgeDistance),borderColor ,_graphics.PreferredBackBufferHeight + (2*_graphics.PreferredBackBufferHeight)/borderEdgeDistance,borderThickness),
            new Border(GraphicsDevice,new Vector2(_graphics.PreferredBackBufferWidth + _graphics.PreferredBackBufferWidth/borderEdgeDistance,-_graphics.PreferredBackBufferHeight/borderEdgeDistance),borderColor ,_graphics.PreferredBackBufferHeight + (2*_graphics.PreferredBackBufferHeight)/borderEdgeDistance,borderThickness),
            new Border(GraphicsDevice, new Vector2(-_graphics.PreferredBackBufferWidth/borderEdgeDistance,-_graphics.PreferredBackBufferHeight/borderEdgeDistance), borderColor , borderThickness, _graphics.PreferredBackBufferWidth + (2*_graphics.PreferredBackBufferWidth)/borderEdgeDistance),
            new Border(GraphicsDevice, new Vector2(-_graphics.PreferredBackBufferWidth/borderEdgeDistance,_graphics.PreferredBackBufferHeight + _graphics.PreferredBackBufferHeight/borderEdgeDistance), borderColor , borderThickness, _graphics.PreferredBackBufferWidth + (2*_graphics.PreferredBackBufferWidth)/borderEdgeDistance)
        ];

        BackgroundTextureList = [
            Content.Load<Texture2D>("space_background")
        ];

        BackgroundManager = new BackgroundManager(_spriteBatch, BackgroundTextureList, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState KeyboardState = Keyboard.GetState();

        if (KeyboardState.IsKeyDown(Keys.Escape))
            Exit();

        // Handles player movement
        Vector2 direction = Vector2.Zero;

        if (KeyboardState.IsKeyDown(Keys.W)) { direction.Y -= 1; }
        if (KeyboardState.IsKeyDown(Keys.S)) { direction.Y += 1; }
        if (KeyboardState.IsKeyDown(Keys.A)) { direction.X -= 1; }
        if (KeyboardState.IsKeyDown(Keys.D)) { direction.X += 1; }

        _mainPlayer.Move(direction);
        _mainPlayer.Update();

        // Handles border
        for (int i = 0; i < listBorder.Length; i++)
        {
            listBorder[i].Update(_mainPlayer.PlayerMovement);

            // Checks collision with every border and adjust the player _velocity accordingly
            if (listBorder[i].borderRectangle.Intersects(_mainPlayer.PlayerRectangle))
            {
                _mainPlayer.HitAWall(i, listBorder[i].borderRectangle);
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        // Draws background
        BackgroundManager.Draw(_mainPlayer.PlayerMovement);

        // Handles character
        _mainPlayer.Draw(_spriteBatch);

        // Handles boulder enemy
        foreach (BoulderEnemy boulder in listBouldersEnemies)
        {
            boulder.Draw(_spriteBatch, _mainPlayer.PlayerMovement);
        }

        // Handles border
        for (int i = 0; i < listBorder.Length; i++)
        {
            listBorder[i].Draw(_spriteBatch);
        }

        // Debug
        visuliazer.DrawVector(_spriteBatch, _mainPlayer.Velocity*10);

        _spriteBatch.End();
        base.Draw(gameTime);

    }
}