using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Android.Util;

namespace EpicDungeonsRPG;

public struct StartMenuHud{

    private Vector2 position;
    private Texture2D texture;
    private Rectangle sourceRect;

    public StartMenuBtn startMenuBtn;
    private float scale;
    public StartMenuHud(Texture2D texture, string btnName)
    {
        this.texture = texture;
        sourceRect = new Rectangle(0,0,texture.Width, texture.Height);
        scale = (float)Global.graphics.GraphicsDevice.Viewport.Width/(float)texture.Width;
        this.position = new Vector2(0,Global.graphics.GraphicsDevice.Viewport.Height-(texture.Height * scale));

        startMenuBtn = new(Global.content.Load<Texture2D>("Hud/"+btnName),position,btnName);
    }

    public void Update(){
        startMenuBtn.Update();
    }

    public void Draw(){
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.5f);
        startMenuBtn.Draw();
    }
}