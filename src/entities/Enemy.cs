using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Android.Util;

namespace EpicDungeonsRPG;

public class Enemy : Entity
{

    public ProgressBar attakTimeOutBar;

    public Enemy(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;

        this.items = new List<Item>(){
            new Item("item1",RarityTemplate.common, ItemTypeTemplate.other, null)
        };
        this.stats = new List<Stat>(){
            new Stat(){atributeName=Atribute.health, value=50},
            new Stat(){atributeName=Atribute.maxHealth, value=50},
            new Stat(){atributeName=Atribute.attackTimeOut, value=4},
            new Stat(){atributeName=Atribute.attack, value=10}
        };

        currentAnimFrame = 0;
        frameSize = 32;
        timer = 0;
        animationSpeed = 0.1f;

        var healthBarPos = new Vector2((Global.graphics.GraphicsDevice.Viewport.Width - (100 * 6f)) / 2, Global.graphics.GraphicsDevice.Viewport.Height / 10f);
        this.healthBar = new ProgressBar(Global.content.Load<Texture2D>("Hud/progressBar"), Global.content.Load<Texture2D>("Hud/enemyHealth"), healthBarPos, stats[1].value);

        var attackTimeOutBarPos = new Vector2((Global.graphics.GraphicsDevice.Viewport.Width - (100 * 6f)) / 2, Global.graphics.GraphicsDevice.Viewport.Height / 7f);
        this.attakTimeOutBar = new ProgressBar(Global.content.Load<Texture2D>("Hud/progressBar"), Global.content.Load<Texture2D>("Hud/enemyTimer"), attackTimeOutBarPos, stats[2].value);
    }

    public void TakeDamage(int dmg){
        healthBar.Update(healthBar.currentValue-dmg);
    }
    public void Update(){
        attakTimeOutBar.attackTimeOutUpdate();
    }

    public void Draw()
    {
        this.sourceRect = new Rectangle(currentAnimFrame * frameSize, 0, frameSize, frameSize);
        var scale = 12f;
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1f);
        healthBar.Draw();
        attakTimeOutBar.Draw();
    }

}