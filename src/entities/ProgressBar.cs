using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EpicDungeonsRPG;

public struct ProgressBar{
    private Texture2D background;
    private Texture2D foreground;
    private Vector2 position;
    public float maxValue;
    public float currentValue;
    private Rectangle backgroundRect;
    private Rectangle foregroundRect;

    private float scale=6f;
    public ProgressBar(Texture2D background, Texture2D foreground, Vector2 position, float maxValue)
    {
        this.background = background;
        this.foreground = foreground;
        this.position = position;
        this.maxValue = maxValue;
        this.currentValue = maxValue;
        this.backgroundRect = new Rectangle(0,0, background.Width, background.Height);
        this.foregroundRect = new Rectangle(0,0, foreground.Width, foreground.Height);
    }

    public void Update(float value){
        currentValue = value;
        foregroundRect.Width = (int)(currentValue / maxValue * foreground.Width);
    }

    public void attackTimeOutUpdate(){
        if(currentValue < 0) currentValue = maxValue;
        currentValue -= (float) Global.gameTime.ElapsedGameTime.TotalSeconds;
        foregroundRect.Width = (int)(currentValue / maxValue * foreground.Width);
    }

    public void Draw(){
        Global.spriteBatch.Draw(background, position, backgroundRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.8f);
        Global.spriteBatch.Draw(foreground, position, foregroundRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.9f);
    }

}