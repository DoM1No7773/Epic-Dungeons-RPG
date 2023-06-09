using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Util;
using System;

namespace EpicDungeonsRPG;

public struct BattleButton{

    public Vector2 position;
    private Texture2D texture;
    private Rectangle sourceRect;

    public bool clicked = false;
    public bool isEnabled = true;
    private float opacity = 1f;
    public BattleButton(Vector2 position, Texture2D texture)
    {
        this.position = position;
        this.texture = texture;

        sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
    }

    private void OnClick(Vector2 position){
        var endPosition = new Vector2(this.position.X+(texture.Width*6f), this.position.Y+(texture.Height*6f));
        if((this.position.X <= position.X && this.position.Y <= position.Y && endPosition.X >= position.X && endPosition.Y >= position.Y)&&(isEnabled)){
            isEnabled = false;
            clicked = true;
        }
        Log.Info("cosTakiego", "mousePos: "+position+" btnXY:"+this.position+"btnEndXY"+endPosition);
    }
    public void Update(){
        var touch = TouchPanel.GetState();

        foreach (var item in touch)
        {  
            OnClick(item.Position);
        }

        if(!isEnabled) opacity = 0.5f;
        else opacity=1f;
    }

    public void Draw(){
        var scale = 6f;
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White * opacity, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1f);
    }
}