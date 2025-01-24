using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class BackgroundManager
{
    private readonly SpriteBatch _spriteBatch;
    private readonly Texture2D[] _backgroundTextures;
    private readonly int _screenWidth;
    private readonly int _screenHeight;
    private readonly int[,] _tileMap;
    private readonly int _mapSize = 100; // Adjust size as needed (how many textures will make one tile)
    private float scalingFactor = 0.6f;

    public BackgroundManager(SpriteBatch spriteBatch, Texture2D[] backgroundTextures, int screenWidth, int screenHeight)
    {
        _spriteBatch = spriteBatch;
        _backgroundTextures = backgroundTextures;
        _screenWidth = screenWidth;
        _screenHeight = screenHeight;

        // Generate random tile map
        Random random = new Random();
        _tileMap = new int[_mapSize, _mapSize];
        for (int y = 0; y < _mapSize; y++)
            for (int x = 0; x < _mapSize; x++)
                _tileMap[x, y] = random.Next(_backgroundTextures.Length);
    }

    public void Draw(Vector2 worldPosition)
    {
        int tileWidth = (int)(_backgroundTextures[0].Width * scalingFactor);
        int tileHeight = (int)(_backgroundTextures[0].Height * scalingFactor);

        float offsetX = ((worldPosition.X % tileWidth) + tileWidth) % tileWidth;
        float offsetY = ((worldPosition.Y % tileHeight) + tileHeight) % tileHeight;
        Vector2 backgroundOffset = new Vector2(offsetX, offsetY);

        int tilesX = (_screenWidth / tileWidth) + 2;
        int tilesY = (_screenHeight / tileHeight) + 2;

        int startX = (int)Math.Floor(worldPosition.X / tileWidth);
        int startY = (int)Math.Floor(worldPosition.Y / tileHeight);

        for (int y = -1; y < tilesY; y++)
        {
            for (int x = -1; x < tilesX; x++)
            {
                int mapX = Math.Abs((startX + x) % _mapSize);
                int mapY = Math.Abs((startY + y) % _mapSize);
                int textureIndex = _tileMap[mapX, mapY];

                Vector2 position = new Vector2(
                    (x * tileWidth) - backgroundOffset.X,
                    (y * tileHeight) - backgroundOffset.Y
                );

                _spriteBatch.Draw(_backgroundTextures[textureIndex], position, null, Color.White, 0f, Vector2.Zero, scalingFactor, SpriteEffects.None, 1f);
            }
        }
    }
}