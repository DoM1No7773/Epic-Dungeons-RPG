using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Android.Util;

namespace EpicDungeonsRPG;

public class Player{

    Texture2D texture;
    Vector2 position;
    List<Item> items;
    List<Stat> stats;
    Rectangle sourceRect;

    private byte currentAnimFrame = 0;
    private byte frameSize = 32;
    private float timer = 0;
    private float animationSpeed = 0.1f;

    public Player(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        this.items = new List<Item>(){
            new Item("item1",RarityTemplate.common, ItemTypeTemplate.other, null)
        };
        this.stats = new List<Stat>(){
            new Stat(){atributeName=Atribute.attack, value=60}
        };
    }

    public void Animate(){
        timer += (float) Global.gameTime.ElapsedGameTime.TotalSeconds;

        if(timer > animationSpeed){
            timer = 0;
            currentAnimFrame ++;
        }

        // Log.Info("cosTakiego", "timer: "+timer);
        if(currentAnimFrame > 2) currentAnimFrame = 0;
    } 

    public void Update(){
        Animate();
    }

    public void Draw(){
        this.sourceRect = new Rectangle(currentAnimFrame * frameSize,0,frameSize,frameSize);
        var scale = 12f;
        Global.spriteBatch.Draw(texture,position,sourceRect,Color.White,0,new Vector2(0, 0),scale,SpriteEffects.None,1f);
    }


}