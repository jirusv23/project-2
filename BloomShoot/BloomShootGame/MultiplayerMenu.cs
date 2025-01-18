using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BloomShootGame;

public class MultiplayerMenu : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    private Vector2 _middleOfScreen;
    
    private string _password, _ipAddress;
    public string Password => _password; public string IpAddress => _ipAddress;
    
    private textInputBox _passwordBox;
    private textInputBox _ipAddressBox;
    
    private float _firstButtonHeight = 100f; private float _buttonDistance = 150f;
    private Vector2 _buttonSize = new Vector2(250, 100);
    
    private KeyboardState _oldKeyboardState;
    private KeyboardState _newKeyboardState;
    
    public MultiplayerMenu()
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
        
        _passwordBox = new textInputBox(new Vector2(_middleOfScreen.X - _buttonSize.X/2, _firstButtonHeight), GraphicsDevice, (int)_buttonSize.X, (int)_buttonSize.Y, _font);
        _ipAddressBox =  new textInputBox(new Vector2(_middleOfScreen.X - _buttonSize.X/2, _firstButtonHeight + _buttonDistance), GraphicsDevice, (int)_buttonSize.X, (int)_buttonSize.Y, _font);
    }

    protected override void Update(GameTime gameTime)
    {
        var KeyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || KeyboardState.IsKeyDown(Keys.Escape)) Exit();
        
        if (_passwordBox.WithinBounds(new Vector2(mouseState.X, mouseState.Y))) CheckHeldCharacters(KeyboardState); 
        if (_passwordBox.WithinBounds(new Vector2(mouseState.X, mouseState.Y))) CheckHeldCharacters2(KeyboardState);

        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkBlue);

        _spriteBatch.Begin();
        
        _passwordBox.Draw(_spriteBatch);
        _ipAddressBox.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    protected override void UnloadContent() { }
    
    private void CheckHeldCharacters(KeyboardState keyboardState)
    {
        if (keyboardState.IsKeyDown(Keys.Enter)) { }
        else {
            if (_newKeyboardState.IsKeyDown(Keys.Back)) { if (!_oldKeyboardState.IsKeyDown(Keys.Back)) _passwordBox.DelLetter(); }
            if (_newKeyboardState.IsKeyDown(Keys.Space)) { if (!_oldKeyboardState.IsKeyDown(Keys.Space)) _passwordBox.UpdateText(" "); }
            if (_newKeyboardState.IsKeyDown(Keys.Q)) { if (!_oldKeyboardState.IsKeyDown(Keys.Q)) _passwordBox.UpdateText("q"); }
            if (_newKeyboardState.IsKeyDown(Keys.W)) { if (!_oldKeyboardState.IsKeyDown(Keys.W)) _passwordBox.UpdateText("w"); }
            if (_newKeyboardState.IsKeyDown(Keys.E)) { if (!_oldKeyboardState.IsKeyDown(Keys.E)) _passwordBox.UpdateText("e"); }
            if (_newKeyboardState.IsKeyDown(Keys.R)) { if (!_oldKeyboardState.IsKeyDown(Keys.R)) _passwordBox.UpdateText("r"); }
            if (_newKeyboardState.IsKeyDown(Keys.T)) { if (!_oldKeyboardState.IsKeyDown(Keys.T)) _passwordBox.UpdateText("t"); }
            if (_newKeyboardState.IsKeyDown(Keys.Y)) { if (!_oldKeyboardState.IsKeyDown(Keys.Y)) _passwordBox.UpdateText("y"); }
            if (_newKeyboardState.IsKeyDown(Keys.U)) { if (!_oldKeyboardState.IsKeyDown(Keys.U)) _passwordBox.UpdateText("u"); }
            if (_newKeyboardState.IsKeyDown(Keys.I)) { if (!_oldKeyboardState.IsKeyDown(Keys.I)) _passwordBox.UpdateText("i"); }
            if (_newKeyboardState.IsKeyDown(Keys.O)) { if (!_oldKeyboardState.IsKeyDown(Keys.O)) _passwordBox.UpdateText("o"); }
            if (_newKeyboardState.IsKeyDown(Keys.P)) { if (!_oldKeyboardState.IsKeyDown(Keys.P)) _passwordBox.UpdateText("p"); }
            if (_newKeyboardState.IsKeyDown(Keys.A)) { if (!_oldKeyboardState.IsKeyDown(Keys.A)) _passwordBox.UpdateText("a"); }
            if (_newKeyboardState.IsKeyDown(Keys.S)) { if (!_oldKeyboardState.IsKeyDown(Keys.S)) _passwordBox.UpdateText("s"); }
            if (_newKeyboardState.IsKeyDown(Keys.D)) { if (!_oldKeyboardState.IsKeyDown(Keys.D)) _passwordBox.UpdateText("d"); }
            if (_newKeyboardState.IsKeyDown(Keys.F)) { if (!_oldKeyboardState.IsKeyDown(Keys.F)) _passwordBox.UpdateText("f"); }
            if (_newKeyboardState.IsKeyDown(Keys.G)) { if (!_oldKeyboardState.IsKeyDown(Keys.G)) _passwordBox.UpdateText("g"); }
            if (_newKeyboardState.IsKeyDown(Keys.H)) { if (!_oldKeyboardState.IsKeyDown(Keys.H)) _passwordBox.UpdateText("h"); }
            if (_newKeyboardState.IsKeyDown(Keys.J)) { if (!_oldKeyboardState.IsKeyDown(Keys.J)) _passwordBox.UpdateText("j"); }
            if (_newKeyboardState.IsKeyDown(Keys.K)) { if (!_oldKeyboardState.IsKeyDown(Keys.K)) _passwordBox.UpdateText("k"); }
            if (_newKeyboardState.IsKeyDown(Keys.L)) { if (!_oldKeyboardState.IsKeyDown(Keys.L)) _passwordBox.UpdateText("l"); }
            if (_newKeyboardState.IsKeyDown(Keys.Z)) { if (!_oldKeyboardState.IsKeyDown(Keys.Z)) _passwordBox.UpdateText("z"); }
            if (_newKeyboardState.IsKeyDown(Keys.X)) { if (!_oldKeyboardState.IsKeyDown(Keys.X)) _passwordBox.UpdateText("x"); }
            if (_newKeyboardState.IsKeyDown(Keys.C)) { if (!_oldKeyboardState.IsKeyDown(Keys.C)) _passwordBox.UpdateText("c"); }
            if (_newKeyboardState.IsKeyDown(Keys.V)) { if (!_oldKeyboardState.IsKeyDown(Keys.V)) _passwordBox.UpdateText("v"); }
            if (_newKeyboardState.IsKeyDown(Keys.B)) { if (!_oldKeyboardState.IsKeyDown(Keys.B)) _passwordBox.UpdateText("b"); }
            if (_newKeyboardState.IsKeyDown(Keys.N)) { if (!_oldKeyboardState.IsKeyDown(Keys.N)) _passwordBox.UpdateText("n"); }
            if (_newKeyboardState.IsKeyDown(Keys.M)) { if (!_oldKeyboardState.IsKeyDown(Keys.M)) _passwordBox.UpdateText("m"); }
        }
        _oldKeyboardState = _newKeyboardState;
    }
    
    private void CheckHeldCharacters2(KeyboardState keyboardState)
    {
        if (keyboardState.IsKeyDown(Keys.Enter)) { }
        else {
            if (_newKeyboardState.IsKeyDown(Keys.Back)) { if (!_oldKeyboardState.IsKeyDown(Keys.Back)) _ipAddressBox.DelLetter(); }
            if (_newKeyboardState.IsKeyDown(Keys.Space)) { if (!_oldKeyboardState.IsKeyDown(Keys.Space)) _ipAddressBox.UpdateText(" "); }
            if (_newKeyboardState.IsKeyDown(Keys.Q)) { if (!_oldKeyboardState.IsKeyDown(Keys.Q)) _ipAddressBox.UpdateText("q"); }
            if (_newKeyboardState.IsKeyDown(Keys.W)) { if (!_oldKeyboardState.IsKeyDown(Keys.W)) _ipAddressBox.UpdateText("w"); }
            if (_newKeyboardState.IsKeyDown(Keys.E)) { if (!_oldKeyboardState.IsKeyDown(Keys.E)) _ipAddressBox.UpdateText("e"); }
            if (_newKeyboardState.IsKeyDown(Keys.R)) { if (!_oldKeyboardState.IsKeyDown(Keys.R)) _ipAddressBox.UpdateText("r"); }
            if (_newKeyboardState.IsKeyDown(Keys.T)) { if (!_oldKeyboardState.IsKeyDown(Keys.T)) _ipAddressBox.UpdateText("t"); }
            if (_newKeyboardState.IsKeyDown(Keys.Y)) { if (!_oldKeyboardState.IsKeyDown(Keys.Y)) _ipAddressBox.UpdateText("y"); }
            if (_newKeyboardState.IsKeyDown(Keys.U)) { if (!_oldKeyboardState.IsKeyDown(Keys.U)) _ipAddressBox.UpdateText("u"); }
            if (_newKeyboardState.IsKeyDown(Keys.I)) { if (!_oldKeyboardState.IsKeyDown(Keys.I)) _ipAddressBox.UpdateText("i"); }
            if (_newKeyboardState.IsKeyDown(Keys.O)) { if (!_oldKeyboardState.IsKeyDown(Keys.O)) _ipAddressBox.UpdateText("o"); }
            if (_newKeyboardState.IsKeyDown(Keys.P)) { if (!_oldKeyboardState.IsKeyDown(Keys.P)) _ipAddressBox.UpdateText("p"); }
            if (_newKeyboardState.IsKeyDown(Keys.A)) { if (!_oldKeyboardState.IsKeyDown(Keys.A)) _ipAddressBox.UpdateText("a"); }
            if (_newKeyboardState.IsKeyDown(Keys.S)) { if (!_oldKeyboardState.IsKeyDown(Keys.S)) _ipAddressBox.UpdateText("s"); }
            if (_newKeyboardState.IsKeyDown(Keys.D)) { if (!_oldKeyboardState.IsKeyDown(Keys.D)) _ipAddressBox.UpdateText("d"); }
            if (_newKeyboardState.IsKeyDown(Keys.F)) { if (!_oldKeyboardState.IsKeyDown(Keys.F)) _ipAddressBox.UpdateText("f"); }
            if (_newKeyboardState.IsKeyDown(Keys.G)) { if (!_oldKeyboardState.IsKeyDown(Keys.G)) _ipAddressBox.UpdateText("g"); }
            if (_newKeyboardState.IsKeyDown(Keys.H)) { if (!_oldKeyboardState.IsKeyDown(Keys.H)) _ipAddressBox.UpdateText("h"); }
            if (_newKeyboardState.IsKeyDown(Keys.J)) { if (!_oldKeyboardState.IsKeyDown(Keys.J)) _ipAddressBox.UpdateText("j"); }
            if (_newKeyboardState.IsKeyDown(Keys.K)) { if (!_oldKeyboardState.IsKeyDown(Keys.K)) _ipAddressBox.UpdateText("k"); }
            if (_newKeyboardState.IsKeyDown(Keys.L)) { if (!_oldKeyboardState.IsKeyDown(Keys.L)) _ipAddressBox.UpdateText("l"); }
            if (_newKeyboardState.IsKeyDown(Keys.Z)) { if (!_oldKeyboardState.IsKeyDown(Keys.Z)) _ipAddressBox.UpdateText("z"); }
            if (_newKeyboardState.IsKeyDown(Keys.X)) { if (!_oldKeyboardState.IsKeyDown(Keys.X)) _ipAddressBox.UpdateText("x"); }
            if (_newKeyboardState.IsKeyDown(Keys.C)) { if (!_oldKeyboardState.IsKeyDown(Keys.C)) _ipAddressBox.UpdateText("c"); }
            if (_newKeyboardState.IsKeyDown(Keys.V)) { if (!_oldKeyboardState.IsKeyDown(Keys.V)) _ipAddressBox.UpdateText("v"); }
            if (_newKeyboardState.IsKeyDown(Keys.B)) { if (!_oldKeyboardState.IsKeyDown(Keys.B)) _ipAddressBox.UpdateText("b"); }
            if (_newKeyboardState.IsKeyDown(Keys.N)) { if (!_oldKeyboardState.IsKeyDown(Keys.N)) _ipAddressBox.UpdateText("n"); }
            if (_newKeyboardState.IsKeyDown(Keys.M)) { if (!_oldKeyboardState.IsKeyDown(Keys.M)) _ipAddressBox.UpdateText("m"); }
        }
        _oldKeyboardState = _newKeyboardState;
    }
}