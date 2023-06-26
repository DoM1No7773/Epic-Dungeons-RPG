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
    public byte id;
    public Vector2 position;
    private Texture2D texture;
    private Rectangle sourceRect;
    public bool isEnabled = true;
    private float opacity = 1f;
    public BattleButton(Vector2 position, Texture2D texture, byte id)
    {
        this.position = position;
        this.texture = texture;
        this.id = id;
        sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
    }

    public void Update(Arrow arrow){
        var touch =Global.touchState;

        foreach (var item in touch)
        {  
            
            var endPosition = new Vector2(this.position.X+(texture.Width*6f), this.position.Y+(texture.Height*6f));

            if(arrow.arrowState == ArrowState.moving && isEnabled==false){
                arrow.arrowState = ArrowState.finished;
                isEnabled = true;
                arrow.lastButton = id;
            }

            if((this.position.X <= item.Position.X && this.position.Y <= item.Position.Y && endPosition.X >= item.Position.X && endPosition.Y >= item.Position.Y)&&(isEnabled)){
                isEnabled = !isEnabled;
                arrow.arrowState = ArrowState.moving;
            }
            
        }

        if(!isEnabled) opacity = 0.5f;
        else opacity=1f;
    }

    public void Draw(){
        var scale = 6f;
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White * opacity, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1f);
    }
}