using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Vector2Visualizer
{
    private GraphicsDevice graphicsDevice;
    private Texture2D pixelTexture;
    private float lineThickness;
    private Color lineColor;
    private Color arrowColor;

    public Vector2 Origin { get; set; }

    public Vector2Visualizer(GraphicsDevice graphicsDevice, Vector2 origin, float lineThickness = 2f)
    {
        this.graphicsDevice = graphicsDevice;
        this.Origin = origin;
        this.lineThickness = lineThickness;
        this.lineColor = Color.White;
        this.arrowColor = Color.Yellow;

        // Create a 1x1 white texture for drawing lines
        pixelTexture = new Texture2D(graphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });
    }

    public void DrawVector(SpriteBatch spriteBatch, Vector2 vector)
    {
        Vector2 endPoint = Origin + vector;
        DrawLine(spriteBatch, Origin, endPoint, lineColor);
        DrawArrowhead(spriteBatch, endPoint, vector, arrowColor);
    }

    private void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
    {
        Vector2 edge = end - start;
        float angle = (float)Math.Atan2(edge.Y, edge.X);
        float length = edge.Length();

        spriteBatch.Draw(pixelTexture, start, null, color,
            angle, Vector2.Zero, new Vector2(length, lineThickness),
            SpriteEffects.None, 0);
    }

    private void DrawArrowhead(SpriteBatch spriteBatch, Vector2 position, Vector2 direction, Color color)
    {
        float arrowSize = lineThickness * 4;
        float angle = (float)Math.Atan2(direction.Y, direction.X);

        Vector2 arrowPoint1 = position - new Vector2(
            (float)Math.Cos(angle + Math.PI / 6) * arrowSize,
            (float)Math.Sin(angle + Math.PI / 6) * arrowSize
        );

        Vector2 arrowPoint2 = position - new Vector2(
            (float)Math.Cos(angle - Math.PI / 6) * arrowSize,
            (float)Math.Sin(angle - Math.PI / 6) * arrowSize
        );

        DrawLine(spriteBatch, position, arrowPoint1, color);
        DrawLine(spriteBatch, position, arrowPoint2, color);
    }

    public void SetColors(Color lineColor, Color arrowColor)
    {
        this.lineColor = lineColor;
        this.arrowColor = arrowColor;
    }

    public void Dispose()
    {
        pixelTexture?.Dispose();
    }
}