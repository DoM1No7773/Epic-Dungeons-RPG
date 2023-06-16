using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Android.Util;

namespace EpicDungeonsRPG;

public class Player : Entity
{  
    public byte currentAnimRow;
    public Player(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        this.items = new List<Item>(){
            new Item("item1",RarityTemplate.common, ItemTypeTemplate.other, null)
        };
        this.stats = new List<Stat>(){
            new Stat(){atributeName=Atribute.health, value=50},
            new Stat(){atributeName=Atribute.maxHealth, value=50},
            new Stat(){atributeName=Atribute.attack, value=10},
            new Stat(){atributeName=Atribute.armor, value=0}
        };

        currentAnimFrame = 0;
        currentAnimRow = 0;
        frameSize = 32;
        timer = 0;
        animationSpeed = 0.5f;


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
        
        // Log.Info("cosTakiego", "timer: "+timer);
        if(currentAnimRow != 0 && currentAnimFrame > 2) currentAnimRow = 0;
        if (currentAnimFrame > 2) currentAnimFrame = 0;
    }

    public void TakeDamage(Enemy enemy){
        if(enemy.attakTimeOutBar.currentValue < 0){
            healthBar.Update(healthBar.currentValue-(enemy.stats[3].value * ((100 - this.stats[3].value)/100)));
        }
    }
    public void Update()
    {
        Animate();
    }

    public void Draw()
    {
        this.sourceRect = new Rectangle(currentAnimFrame * frameSize, currentAnimRow * frameSize, frameSize, frameSize);
        var scale = 12f;
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.9f);
        healthBar.Draw();
    }


}