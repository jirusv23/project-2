using System;
using System.Security.Cryptography;
using BloomShootGame.Player;
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

    private Client _client;
    private string _password, _ip;
    
    private string _connectionStatus = "";
    private Color _statusColor = Color.White;

    public BloomShootGameProgram(string password, string ip)
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.IsFullScreen = true;
        
        _password = password;
        _ip = ip;
        
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _middleOfScreen = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        
        _playerLocal = new PlayerLocal(GraphicsDevice, _middleOfScreen, true);
        _playerOther = new PlayerOther(GraphicsDevice, _middleOfScreen);

        _client = new Client(_password, _ip);
        _client.OnPlayerStateReceived += OnPlayerStateReceived;
        
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

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            _client.StopClient();
            Exit();
        }

        _client.Update();
        
        // Update connection status
        switch (_client.CurrentState)
        {
            case Client.ConnectionState.Connecting:
                _connectionStatus = "Connecting to server...";
                _statusColor = Color.Yellow;
                break;
            case Client.ConnectionState.Connected:
                _connectionStatus = "Connected!";
                _statusColor = Color.Green;
                break;
            case Client.ConnectionState.Failed:
                _connectionStatus = $"Connection failed: {_client.LastError}";
                _statusColor = Color.Red;
                break;
            case Client.ConnectionState.Disconnected:
                _connectionStatus = "Disconnected from server";
                _statusColor = Color.Red;
                break;
        }

        if (_client.CurrentState == Client.ConnectionState.Connected)
        {
            Console.Write("test");
            
            Vector2 direction = Vector2.Zero;
        
            if (KeyboardState.IsKeyDown(Keys.W)) { direction.Y -= 1; }
            if (KeyboardState.IsKeyDown(Keys.S)) { direction.Y += 1; }
            if (KeyboardState.IsKeyDown(Keys.A)) { direction.X -= 1; }
            if (KeyboardState.IsKeyDown(Keys.D)) { direction.X += 1; }
        
            _playerLocal.Move(direction);
            _playerLocal.Update();

            // Send local player state to server
            _client.SendPlayerState(
                _playerLocal.Position.X,
                _playerLocal.Position.Y,
                _playerLocal.Rotation,
                "player1" // You should have a proper player ID system
            );
        }
        else { }
        
        base.Update(gameTime);

    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        if (_client.IsRunning)
        {
            _playerLocal.Draw(_spriteBatch);
            _playerOther.Draw(_spriteBatch);
        
            // souřadnice od středu
            _spriteBatch.DrawString(_font,  (int)(_playerLocal.Position.X - _graphics.PreferredBackBufferWidth/2) + _playerLocal._width/2 + "    " + (int)(_playerLocal.Position.Y - _graphics.PreferredBackBufferHeight / 2 + _playerLocal._height/2), Vector2.Zero, Color.White);
            // normální souřadnice
            _spriteBatch.DrawString(_font, $"{(int)_playerLocal.Position.X} | {(int)_playerLocal.Position.Y}", new Vector2(_graphics.PreferredBackBufferWidth - 90, 0), Color.White);

        }
        else
        {
            Vector2 statusPos = new Vector2(
                _middleOfScreen.X - _font.MeasureString(_connectionStatus).X / 2,
                _middleOfScreen.Y - _font.MeasureString(_connectionStatus).Y / 2
            );
            _spriteBatch.DrawString(_font, _connectionStatus, statusPos, _statusColor);
        }
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void OnPlayerStateReceived(PlayerStateMessage state)
    {
        // Update other player's position when we receive state from server
        if (state.PlayerID != _client.OwnID) // Don't update if it's our own state
        {
            _playerOther.Position = new Vector2(state.X, state.Y);
            _playerOther.Rotation = state.Rotation;
        }
    }

    protected override void UnloadContent()
    {
        _client?.StopClient();
        base.UnloadContent();
    }
}