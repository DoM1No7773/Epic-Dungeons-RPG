using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Util;

namespace EpicDungeonsRPG;
public enum ArrowState
{
    notStarted, moving, finished
}
public class Arrow : Basic2D
{

    public Point moveArea;
    public bool isGoingLeft = true;
    public ArrowState arrowState;
    private int speed = 2000;
    public int power = 0;
    public byte defPower = 0;
    public byte lastButton = 0; 
    public Arrow(Texture2D texture, Point moveArea)
    {
        var windowWidth = Global.graphics.GraphicsDevice.Viewport.Width;
        var windowHeight = Global.graphics.GraphicsDevice.Viewport.Height;
        this.position = new Vector2((int)(windowWidth / 2f), (int)(windowHeight / 1.5f));
        this.texture = texture;
        this.moveArea = moveArea;
        this.arrowState = ArrowState.notStarted;
    }

    private void Moving()
    {
        var velocity = Vector2.Zero;
        var deltaTime = (float)Global.gameTime.ElapsedGameTime.TotalSeconds;

        if (this.position.X > moveArea.X && this.position.X + (this.texture.Width * 12f) < moveArea.Y)
        {
            if (isGoingLeft) velocity.X -= 1;
            else velocity.X += 1;
        }

        if (this.position.X < moveArea.X)
        {
            velocity.X += 1;
            isGoingLeft = false;
        }
        if (this.position.X + (this.texture.Width * 12f) > moveArea.Y)
        {
            velocity.X -= 1;
            isGoingLeft = true;
        }

        if (velocity != Vector2.Zero)
            velocity.Normalize();


        this.position += velocity * deltaTime * speed;
    }

    private void HittedArea(List<HitArea> Areas)
    {
        var centeredPosition = position.X + ((this.texture.Width * 12f) / 2.2f);

        for (int i = 0; i < Areas.Count; i++)
        {
            var itemStart = (moveArea.X) + ((Areas[i].sourceRect.X) / 42f * (moveArea.Y-moveArea.X));
            var itemEnd =  (moveArea.X) + ((Areas[i].sourceRect.X + Areas[i].sourceRect.Width) / 42f * (moveArea.Y-moveArea.X));

            //  Log.Info("cosTakiego","::"+Areas[i].name+" cn:"+centeredPosition+" is: "+itemStart+" ie: "+itemEnd+" moveAX:"+moveArea.X+" moveAY:"+moveArea.Y+" sr:"+Areas[i].sourceRect);

            if (centeredPosition >= itemStart && centeredPosition <= itemEnd)
            {
                switch(Areas[i].name){
                    case HitAreaType.weak:
                        power = 50;
                        defPower = 0;
                    break;
                    case HitAreaType.normal:
                        power = 100;
                        defPower = 30;
                    break;
                    case HitAreaType.strong:
                        power = 200;
                        defPower = 70;
                    break;
                    case HitAreaType.critical:
                        power = 400;
                        defPower = 100;
                    break;
                    default:
                    break;
                }
                Log.Info("cosTakiego", "::" + Areas[i].name);
                // lastButton = 0;
                arrowState = ArrowState.notStarted;
            }
        }
    }
    public void Update(List<HitArea> Areas)
    {
        switch (arrowState)
        {
            case ArrowState.moving:
                power = 0;
                Moving();
                break;
            case ArrowState.finished:
                HittedArea(Areas);
                break;
            default:
                break;
        }
    }

    public void Draw()
    {
        var sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
        var scale = 12f;
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White, 0f,
        new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 1f);
    }
}