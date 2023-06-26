using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Android.Util;

namespace EpicDungeonsRPG;

public class Player : Entity
{  
    public byte currentAnimRow;
    private SpriteFont font;
    private Vector2 positionString;
    public Player(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        
        this.stats = new List<Stat>(){
            new Stat(){atributeName=Atribute.health, value=Global.playerAccount.health},
            new Stat(){atributeName=Atribute.maxHealth, value=Global.playerAccount.health},
            new Stat(){atributeName=Atribute.attack, value=Global.playerAccount.attack},
            new Stat(){atributeName=Atribute.armor, value=0}
        };

        currentAnimFrame = 0;
        currentAnimRow = 0;
        frameSize = 32;
        timer = 0;
        animationSpeed = 0.2f;

        font = Global.content.Load<SpriteFont>("Fonts/contBrush");
        positionString = new Vector2((Global.graphics.GraphicsDevice.Viewport.Width - (100 * 6f)) / 2,Global.graphics.GraphicsDevice.Viewport.Height / 1.39f);

        var healthBarPos = new Vector2((Global.graphics.GraphicsDevice.Viewport.Width - (100 * 6f)) / 2,Global.graphics.GraphicsDevice.Viewport.Height / 1.3f);
        healthBar = new ProgressBar(Global.content.Load<Texture2D>("Hud/progressBar"), Global.content.Load<Texture2D>("Hud/playerHealth"), healthBarPos, stats[1].value);
    }

    public void Animate()
    {
        timer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;

        if (timer > animationSpeed)
        {
            timer = 0;
            currentAnimFrame++;

        }
        
        if(currentAnimRow != 0 && currentAnimFrame > 2) currentAnimRow = 0;
        if (currentAnimFrame > 2) currentAnimFrame = 0;
    }

    public void TakeDamage(Enemy enemy, Arrow arrow){
        if(enemy.attakTimeOutBar.currentValue < 0){
            var dmg = (enemy.stats[3].value * ((100 - this.stats[3].value)/100f));
            healthBar.Update(healthBar.currentValue-dmg);
            if(dmg != 0){
                currentAnimRow = 3;
                currentAnimFrame = 0;
            }

            if(healthBar.currentValue <= 0){
                currentAnimRow = 4;
                currentAnimFrame = 0;
            }

            arrow.defPower = 0;
            this.stats[3].value = 0;
        }
    }
    public void Update()
    {
        Animate();
        if(healthBar.currentValue < 0) healthBar.currentValue = 0;
    }



    public void Draw()
    {
        this.sourceRect = new Rectangle(currentAnimFrame * frameSize, currentAnimRow * frameSize, frameSize, frameSize);
        var scale = 12f;
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.9f);

        var scaleString = 2f;
        var stringContent = ("DMG: "+stats[2].value+"\n"+"HP: "+System.Math.Round(healthBar.currentValue)+"/"+healthBar.maxValue);
        Global.spriteBatch.DrawString(font, stringContent, positionString, Color.White, 0f, new Vector2(0,0), scaleString, SpriteEffects.None, 1f);
        healthBar.Draw();
    }


}