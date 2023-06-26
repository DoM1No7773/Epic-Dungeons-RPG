using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Android.Util;

namespace EpicDungeonsRPG;

public struct InventoryView{

    private AddAtributeBtn attackBtn;
    private AddAtributeBtn healthBtn;
    private SpriteFont font;
    public InventoryView(){
        attackBtn = new(Global.content.Load<Texture2D>("Hud/upgradeBtn"), new Vector2(400,200), Atribute.attack,"ATK: "+Global.playerAccount.attack,new Vector2(100,300));
        healthBtn = new(Global.content.Load<Texture2D>("Hud/upgradeBtn"), new Vector2(400,600), Atribute.health,"HP:  "+Global.playerAccount.health,new Vector2(100,700));
        font = Global.content.Load<SpriteFont>("Fonts/contBrush");
    }
    public void Update(){
        attackBtn.Update();
        healthBtn.Update();
    }

    public void Draw(){
        Global.spriteBatch.DrawString(font, "PLAYER STATS UPGRADE GOLD: "+Global.playerAccount.gold, new Vector2(20,20), Color.White, 0f, new Vector2(0,0), 2.5f, SpriteEffects.None, 1f);
        attackBtn.Draw();
        healthBtn.Draw();
    }
}