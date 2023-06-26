using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Android.Util;

namespace EpicDungeonsRPG;
public struct InventoryManager{
    public StartMenuHud startMenuHud;
    private Texture2D background;
    private float scale;
    private InventoryView inventoryView;
    public InventoryManager(){
        startMenuHud = new(Global.content.Load<Texture2D>("Hud/overlay"),"homeBtn");
        background = Global.content.Load<Texture2D>("Hud/panel");
        scale = (float)Global.graphics.GraphicsDevice.Viewport.Width/(float)background.Width;
        inventoryView = new InventoryView();
    }

    public void Update(){
        inventoryView.Update();
        startMenuHud.Update();
    }

    public void Draw(){
        Global.spriteBatch.Draw(background, new Vector2(0,0),new Rectangle(0,0,background.Width, background.Height), Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.1f);
        inventoryView.Draw();
        startMenuHud.Draw();
    }
}