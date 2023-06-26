using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Android.Util;
using System;

namespace EpicDungeonsRPG;

public struct BattlePanel
{

    public Vector2 position;

    private Texture2D texture;

    public BattleButton attackbtn;
    public BattleButton defendbtn;
    private Rectangle sourceRect;
    public BattlePanel(Texture2D texture, Vector2 position)
    {
        var windowWidth = Global.graphics.GraphicsDevice.Viewport.Width;
        var windowHeight = Global.graphics.GraphicsDevice.Viewport.Height;

        this.position = position;
        this.texture = texture;

        float marginRight = (texture.Width * 17.5f) + 50;
        attackbtn = new BattleButton(new Vector2(windowWidth - marginRight, windowHeight / 1.2f), Global.content.Load<Texture2D>("BattleBar/btnAttack"),1);
        defendbtn = new BattleButton(new Vector2(50, windowHeight / 1.2f), Global.content.Load<Texture2D>("BattleBar/btnDefend"),2);
        sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
    }

    public void Update(Arrow arrow)
    {
        if(arrow.arrowState == ArrowState.notStarted){
            attackbtn.isEnabled = true;
            defendbtn.isEnabled = true;
        }

        attackbtn.Update(arrow);
        defendbtn.Update(arrow);
    }

    public void Draw()
    {
        var scale = 45f;
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.01f);

        attackbtn.Draw();
        defendbtn.Draw();
    }
}