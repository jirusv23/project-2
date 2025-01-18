using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BloomShootServer;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Vector2 _middleOfScreen;
    
    private Server _server;
    
    private SpriteFont _fontBig;
    private SpriteFont _fontSmol;
    private textInputBox _inputBox;
    
    private int[] _inputBoxSize = new int[]{100, 50};
    private KeyboardState _oldKeyboardState;
    private KeyboardState _newKeyboardState;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _middleOfScreen = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        
        _inputBox = new textInputBox(new Vector2(_middleOfScreen.X - _inputBoxSize[0]/2, _middleOfScreen.Y - _inputBoxSize[1]/2), GraphicsDevice, 100, 50, _fontSmol);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _fontBig = Content.Load<SpriteFont>("big_font");
        _fontSmol = Content.Load<SpriteFont>("small_font");
    }

    protected override void Update(GameTime gameTime)
    {
        _newKeyboardState = Keyboard.GetState();
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (_server == null)
        {
            Console.WriteLine("test");
            
            CheckHeldCharacters(_newKeyboardState);
        }
        else { _server.Update(); }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        
        if (_server == null) _inputBox.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    private void CheckHeldCharacters(KeyboardState keyboardState)
    {
        if (keyboardState.IsKeyDown(Keys.Enter)) { }
        else {
            if (_newKeyboardState.IsKeyDown(Keys.Back)) { if (!_oldKeyboardState.IsKeyDown(Keys.Back)) _inputBox.DelLetter(); }
            if (_newKeyboardState.IsKeyDown(Keys.Space)) { if (!_oldKeyboardState.IsKeyDown(Keys.Space)) _inputBox.UpdateText(" "); }
            if (_newKeyboardState.IsKeyDown(Keys.Q)) { if (!_oldKeyboardState.IsKeyDown(Keys.Q)) _inputBox.UpdateText("q"); }
            if (_newKeyboardState.IsKeyDown(Keys.W)) { if (!_oldKeyboardState.IsKeyDown(Keys.W)) _inputBox.UpdateText("w"); }
            if (_newKeyboardState.IsKeyDown(Keys.E)) { if (!_oldKeyboardState.IsKeyDown(Keys.E)) _inputBox.UpdateText("e"); }
            if (_newKeyboardState.IsKeyDown(Keys.R)) { if (!_oldKeyboardState.IsKeyDown(Keys.R)) _inputBox.UpdateText("r"); }
            if (_newKeyboardState.IsKeyDown(Keys.T)) { if (!_oldKeyboardState.IsKeyDown(Keys.T)) _inputBox.UpdateText("t"); }
            if (_newKeyboardState.IsKeyDown(Keys.Y)) { if (!_oldKeyboardState.IsKeyDown(Keys.Y)) _inputBox.UpdateText("y"); }
            if (_newKeyboardState.IsKeyDown(Keys.U)) { if (!_oldKeyboardState.IsKeyDown(Keys.U)) _inputBox.UpdateText("u"); }
            if (_newKeyboardState.IsKeyDown(Keys.I)) { if (!_oldKeyboardState.IsKeyDown(Keys.I)) _inputBox.UpdateText("i"); }
            if (_newKeyboardState.IsKeyDown(Keys.O)) { if (!_oldKeyboardState.IsKeyDown(Keys.O)) _inputBox.UpdateText("o"); }
            if (_newKeyboardState.IsKeyDown(Keys.P)) { if (!_oldKeyboardState.IsKeyDown(Keys.P)) _inputBox.UpdateText("p"); }
            if (_newKeyboardState.IsKeyDown(Keys.A)) { if (!_oldKeyboardState.IsKeyDown(Keys.A)) _inputBox.UpdateText("a"); }
            if (_newKeyboardState.IsKeyDown(Keys.S)) { if (!_oldKeyboardState.IsKeyDown(Keys.S)) _inputBox.UpdateText("s"); }
            if (_newKeyboardState.IsKeyDown(Keys.D)) { if (!_oldKeyboardState.IsKeyDown(Keys.D)) _inputBox.UpdateText("d"); }
            if (_newKeyboardState.IsKeyDown(Keys.F)) { if (!_oldKeyboardState.IsKeyDown(Keys.F)) _inputBox.UpdateText("f"); }
            if (_newKeyboardState.IsKeyDown(Keys.G)) { if (!_oldKeyboardState.IsKeyDown(Keys.G)) _inputBox.UpdateText("g"); }
            if (_newKeyboardState.IsKeyDown(Keys.H)) { if (!_oldKeyboardState.IsKeyDown(Keys.H)) _inputBox.UpdateText("h"); }
            if (_newKeyboardState.IsKeyDown(Keys.J)) { if (!_oldKeyboardState.IsKeyDown(Keys.J)) _inputBox.UpdateText("j"); }
            if (_newKeyboardState.IsKeyDown(Keys.K)) { if (!_oldKeyboardState.IsKeyDown(Keys.K)) _inputBox.UpdateText("k"); }
            if (_newKeyboardState.IsKeyDown(Keys.L)) { if (!_oldKeyboardState.IsKeyDown(Keys.L)) _inputBox.UpdateText("l"); }
            if (_newKeyboardState.IsKeyDown(Keys.Z)) { if (!_oldKeyboardState.IsKeyDown(Keys.Z)) _inputBox.UpdateText("z"); }
            if (_newKeyboardState.IsKeyDown(Keys.X)) { if (!_oldKeyboardState.IsKeyDown(Keys.X)) _inputBox.UpdateText("x"); }
            if (_newKeyboardState.IsKeyDown(Keys.C)) { if (!_oldKeyboardState.IsKeyDown(Keys.C)) _inputBox.UpdateText("c"); }
            if (_newKeyboardState.IsKeyDown(Keys.V)) { if (!_oldKeyboardState.IsKeyDown(Keys.V)) _inputBox.UpdateText("v"); }
            if (_newKeyboardState.IsKeyDown(Keys.B)) { if (!_oldKeyboardState.IsKeyDown(Keys.B)) _inputBox.UpdateText("b"); }
            if (_newKeyboardState.IsKeyDown(Keys.N)) { if (!_oldKeyboardState.IsKeyDown(Keys.N)) _inputBox.UpdateText("n"); }
            if (_newKeyboardState.IsKeyDown(Keys.M)) { if (!_oldKeyboardState.IsKeyDown(Keys.M)) _inputBox.UpdateText("m"); }
        }
        _oldKeyboardState = _newKeyboardState;
    }
}