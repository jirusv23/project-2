using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BloomShootGame;

public class BloomShootMenuProgram : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private PlayerLocal _playerLocal;
    private PlayerOther _playerOther;
    private SpriteFont _font;
    
    private Vector2 _middleOfScreen;

    private int _selectedGameSettings;
    public int SelectedGameSettings => _selectedGameSettings;

    private Color _buttonColor = Color.White;

    private float _firstButtonHeight = 100f; private float _buttonDistance = 50f;
    private Vector2 _buttonSize = new Vector2(250, 100);
    private MenuButton _buttonSingle; private MenuButton _buttonMulti; private MenuButton _buttonExit;
    private SpriteFont _buttonFont;

    public BloomShootMenuProgram()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _middleOfScreen = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("Font1");
        _buttonFont = Content.Load<SpriteFont>("ButtonFont");

        _buttonSingle = new MenuButton(GraphicsDevice, new Vector2(_middleOfScreen.X - _buttonSize.X/2, _firstButtonHeight), _buttonSize, Color.White, 1, _buttonFont);
        _buttonMulti = new MenuButton(GraphicsDevice, new Vector2(_middleOfScreen.X - _buttonSize.X/2, _firstButtonHeight + _buttonSize.Y + _buttonDistance), _buttonSize, Color.White, 0, _buttonFont);
        _buttonExit = new MenuButton(GraphicsDevice, new Vector2(_middleOfScreen.X - _buttonSize.X/2, _firstButtonHeight + _buttonSize.Y*2 + _buttonDistance*2), _buttonSize, Color.White, 2, _buttonFont);
    }

    protected override void Update(GameTime gameTime)
    {
        var KeyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (_buttonSingle.WithinBounds(new Vector2(mouseState.X, mouseState.Y)))
            {
                _selectedGameSettings = 1;
            }
            else if (_buttonMulti.WithinBounds(new Vector2(mouseState.X, mouseState.Y)))
            {
                _selectedGameSettings = 0;
            }
            else if (_buttonExit.WithinBounds(new Vector2(mouseState.X, mouseState.Y)))
            {
                _selectedGameSettings = 2;
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkBlue);

        _spriteBatch.Begin();
        
        _buttonSingle.Draw(_spriteBatch, _buttonFont);
        _buttonMulti.Draw(_spriteBatch, _buttonFont);
        _buttonExit.Draw(_spriteBatch, _buttonFont);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void UnloadContent() { }
}