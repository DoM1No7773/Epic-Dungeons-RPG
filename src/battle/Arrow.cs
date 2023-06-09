using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Util;

namespace EpicDungeonsRPG;

public class Arrow : Basic2D{

    public Point moveArea;
    public bool isGoingLeft = true;
    public byte clicked = 0;
    private int speed = 2000;
    private float timer=10;
    private byte waitForAnim = 0;
    private BattleButton attack;
    private BattleButton defend;
    public Arrow(Texture2D texture, Point moveArea){
        var windowWidth = Global.graphics.GraphicsDevice.Viewport.Width;
        var windowHeight = Global.graphics.GraphicsDevice.Viewport.Height;
        this.position = new Vector2((int)(windowWidth / 2f),(int)(windowHeight / 1.5f)); 
        this.texture = texture;
        this.moveArea = moveArea;
    }
    public void GetButtons(BattleButton attack, BattleButton defend){
        this.attack = attack;
        this.defend = defend;
    }

    private void Moving(){
        var velocity = Vector2.Zero;
        var deltaTime = (float) Global.gameTime.ElapsedGameTime.TotalSeconds;

        if(this.position.X > moveArea.X && this.position.X < moveArea.Y){
            if(isGoingLeft) velocity.X -= 1;
            else velocity.X += 1;
        }

        if(this.position.X < moveArea.X){
            velocity.X += 1;
            isGoingLeft = false;
        }
        if(this.position.X > moveArea.Y){
            velocity.X -= 1;
            isGoingLeft = true;
        }

        if (velocity != Vector2.Zero)
            velocity.Normalize();


        this.position += velocity * deltaTime * speed;
    }

    private void HittedArea(List<HitArea> Areas){
        var centeredPosition = position.X;
        
        for(int i=0;i<Areas.Count;i++){
            var itemStart = (Areas[i].sourceRect.X)/42f * moveArea.Y;
            var itemEnd = ((Areas[i].sourceRect.X + Areas[i].sourceRect.Width)/42f * moveArea.Y);

            // Log.Info("cosTakiego","::"+Areas[i].name+" cn:"+centeredPosition+" is: "+itemStart+" ie: "+itemEnd+" moveAX:"+moveArea.X+" moveAY:"+moveArea.Y+" sr:"+Areas[i].sourceRect);

            if(centeredPosition >= itemStart && centeredPosition <= itemEnd){
                Log.Info("cosTakiego","::"+Areas[i].name);
                attack.isEnabled = true;

                defend.isEnabled = true;

                waitForAnim = 1;
                clicked = 1;
            }
        }
    }
    public void Update(List<HitArea> Areas){
        // var touch = TouchPanel.GetState();
        // foreach (var item in touch)
        // {  
        //     clicked = 1;
        // }
        timer += (float) Global.gameTime.ElapsedGameTime.TotalSeconds;


        if(waitForAnim==2){
            if(timer > 3){
                clicked=0;
                waitForAnim=0;
            }
        }
        else if(waitForAnim==1){
            timer = 0;
            waitForAnim = 2;
        }
        else{

            Log.Info("cosTakiego","::"+attack.position +"::"+ defend.position);
            if(attack.clicked){
                HittedArea(Areas);
            }

            if(defend.clicked){
                HittedArea(Areas);
            }


            if(clicked == 0)
                Moving();
        
        }
        // if(clicked == 1){
        //     HittedArea(Areas);
        //     clicked = 3;
        // }

    }

    public void Draw(){
        var sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
        var scale = 12f;
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White, 0f,
        new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 1f);
    }
}