using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Android.Util;

namespace EpicDungeonsRPG;

public class LevelDesc{
    public Enemy enemy;
    public int gold;
    public LevelDesc(Texture2D enemyTexture, Vector2 enemyPosition, int maxHP, int nextAttack, int dmg, int gold)
    {
        enemy = new Enemy(enemyTexture,enemyPosition);
        enemy.stats[0].value = maxHP;
        enemy.stats[1].value = maxHP;
        enemy.stats[2].value = nextAttack;
        enemy.stats[3].value = dmg;
        this.gold = gold;
    }
}
public class Level{
    public List<LevelDesc> levels;
    public Level(){
        
        levels = new List<LevelDesc>(){
            new LevelDesc(Global.content.Load<Texture2D>("player"), new Vector2(0,0),40,5,10,2),
            new LevelDesc(Global.content.Load<Texture2D>("player"), new Vector2(0,0),60,5,16,4),
            new LevelDesc(Global.content.Load<Texture2D>("player"), new Vector2(0,0),80,5,26,6),
            new LevelDesc(Global.content.Load<Texture2D>("player"), new Vector2(0,0),100,4,40,8),
            new LevelDesc(Global.content.Load<Texture2D>("player"), new Vector2(0,0),150,3,55,10),
            new LevelDesc(Global.content.Load<Texture2D>("player"), new Vector2(0,0),200,2,65,20),
            new LevelDesc(Global.content.Load<Texture2D>("player"), new Vector2(0,0),450,2,80,40),
            new LevelDesc(Global.content.Load<Texture2D>("player"), new Vector2(0,0),600,2,100,50)
        };
    }
}