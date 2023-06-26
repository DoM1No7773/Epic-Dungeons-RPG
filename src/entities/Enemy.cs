using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Android.Util;

namespace EpicDungeonsRPG;

public class Enemy : Entity
{

    public ProgressBar attakTimeOutBar;

    private SpriteFont font;
    private Vector2 positionString;
    public Enemy(Texture2D texture, Vector2 position){
        this.texture = texture;
        this.position = position;

        this.stats = new List<Stat>(){
            new Stat(){atributeName=Atribute.health, value=50},
            new Stat(){atributeName=Atribute.maxHealth, value=50},
            new Stat(){atributeName=Atribute.attackTimeOut, value=4},
            new Stat(){atributeName=Atribute.attack, value=10}
        };

        currentAnimFrame = 2;
        frameSize = 32;
        timer = 0;
        animationSpeed = 0.1f;

        font = Global.content.Load<SpriteFont>("Fonts/contBrush");
        positionString = new Vector2((Global.graphics.GraphicsDevice.Viewport.Width - (100 * 6f)) / 2, Global.graphics.GraphicsDevice.Viewport.Height / 13f);
        
        var healthBarPos = new Vector2((Global.graphics.GraphicsDevice.Viewport.Width - (100 * 6f)) / 2, Global.graphics.GraphicsDevice.Viewport.Height / 10f);
        this.healthBar = new ProgressBar(Global.content.Load<Texture2D>("Hud/progressBar"), Global.content.Load<Texture2D>("Hud/enemyHealth"), healthBarPos, stats[1].value);

        var attackTimeOutBarPos = new Vector2((Global.graphics.GraphicsDevice.Viewport.Width - (100 * 6f)) / 2, Global.graphics.GraphicsDevice.Viewport.Height / 8.8f);
        this.attakTimeOutBar = new ProgressBar(Global.content.Load<Texture2D>("Hud/progressBar"), Global.content.Load<Texture2D>("Hud/enemyTimer"), attackTimeOutBarPos, stats[2].value);
    }
    public void TakeDamage(int dmg){
        healthBar.Update(healthBar.currentValue-dmg);
    }
    public void Update(){
        attakTimeOutBar.attackTimeOutUpdate();
        if(healthBar.currentValue < 0) healthBar.currentValue = 0;
    }

    public void Draw()
    {
        this.sourceRect = new Rectangle(currentAnimFrame * frameSize, 0, frameSize, frameSize);
        var scale = 12f;
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1f);

        var scaleString = 2f;
        var stringContent = ("HP: "+healthBar.currentValue+"/"+healthBar.maxValue);
        Global.spriteBatch.DrawString(font, stringContent, positionString, Color.White, 0f, new Vector2(0,0), scaleString, SpriteEffects.None, 1f);
        healthBar.Draw();
        attakTimeOutBar.Draw();
    }

}