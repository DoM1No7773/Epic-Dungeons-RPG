using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using Android.Util;

namespace EpicDungeonsRPG;

public struct AddAtributeBtn{
    private Texture2D texture;
    private Vector2 position;
    private Atribute atribute;
    private bool isBuyable = false;
    private float opacity = 0.5f;
    private float scale;
    private SpriteFont font;
    string stringContent;
    Vector2 positionString;
    float scaleString = 3f;
    public AddAtributeBtn(Texture2D texture, Vector2 position, Atribute atribute, string stringContent, Vector2 positionString)
    {
        this.texture = texture;
        this.position = position;
        this.atribute = atribute;
        scale = (float)(Global.graphics.GraphicsDevice.Viewport.Width/(float)texture.Width)/3f;
        font = Global.content.Load<SpriteFont>("Fonts/contBrush");
        this.stringContent = stringContent;
        this.positionString = positionString;
    }
    public void Update(){
        if(isBuyable) opacity = 1f;
        else opacity = 0.5f;

        if(Global.playerAccount.gold - 10 < 0) isBuyable=false;
        else isBuyable=true;

        var touch = Global.touchState;

        foreach (var item in touch)
        {
            if(new Rectangle((int)position.X,(int)position.Y,(int)(texture.Width * scale),(int)(texture.Height * scale)).Contains(item.Position)&&(isBuyable)&&(item.State == TouchLocationState.Released)){
                if(atribute == Atribute.attack){
                    Global.playerAccount.AddAttack(10);
                    Global.playerAccount.AddGold(-10);
                    stringContent = "ATK: "+Global.playerAccount.attack;
                }
                if(atribute == Atribute.health){
                    Global.playerAccount.AddHealth(10);
                    Global.playerAccount.AddGold(-10);
                    stringContent = "HP:  "+Global.playerAccount.health;
                }
            }
        }
    }
    public void Draw(){
        Global.spriteBatch.DrawString(font, stringContent, positionString, Color.White, 0f, new Vector2(0,0), scaleString, SpriteEffects.None, 1f);
        Global.spriteBatch.Draw(texture, position,new Rectangle(0,0,texture.Width, texture.Height), Color.White * opacity, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.4f);
    }
}