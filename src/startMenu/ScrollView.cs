using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Util;

namespace EpicDungeonsRPG;
public struct ScrollView{

    private Texture2D texture;
    private Vector2 position;
    private Rectangle sourceRect;
    private float scale;
    private List<LevelBtn> levelBtns;
    public ScrollView(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        this.sourceRect = new Rectangle(0,0,texture.Width,texture.Height);
        scale = (float)Global.graphics.GraphicsDevice.Viewport.Width/(float)texture.Width;

        levelBtns = new List<LevelBtn>();

        int row = -1;
        for(int i=0;i<8;i++)
        {   
            if(i % 2 == 0){
                row++;
                levelBtns.Add(new LevelBtn(Global.content.Load<Texture2D>("Hud/levelBtn"),new Vector2(0,0),(byte)(i), 2f,(byte)row));
            }
            else
                levelBtns.Add(new LevelBtn(Global.content.Load<Texture2D>("Hud/levelBtn"),new Vector2(0,0),(byte)(i), 0.29f,(byte)row));

        }
    }
    public void Update(){

        foreach (var item in levelBtns)
        {
            item.Update();
        }
    }

    public void Draw(){
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.01f);
        foreach (var item in levelBtns)
        {
            item.Draw();
        }
    }
}