using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Android.Util;

namespace EpicDungeonsRPG;

public struct BattleManager{

    private Player player;

    private Enemy enemy;
    private BattleBar battleBar;
    private BattlePanel battlePanel;
    private int[,] HitAreaData =new int[,]{{0,74},{74,86},{86,96,},{96,100}}; 
    public BattleManager(){
        var windowWidth = Global.graphics.GraphicsDevice.Viewport.Width;
        var windowHeight = Global.graphics.GraphicsDevice.Viewport.Height;

        enemy = new Enemy(Global.content.Load<Texture2D>("player"), new Vector2(0,0));
        player = new Player(Global.content.Load<Texture2D>("Sprites/Player/player"), new Vector2(50, windowHeight / 2.8f));
        battlePanel = new BattlePanel(Global.content.Load<Texture2D>("BattleBar/battlePanel"), new Vector2(0, windowHeight / 1.68f));
        battleBar = new BattleBar(Global.content.Load<Texture2D>("BattleBar/battleBar"), new Vector2((int)(windowWidth / 2f), (int)(windowHeight / 1.5f)),HitAreaData);
    }

    public void setArrowPow(){
        Log.Info("cosTakiego", "lastbtn:"+battleBar.arrow.lastButton+" pow:"+battleBar.arrow.power);
        int playerDmg = 0;
        if(battleBar.arrow.lastButton == 1 && battleBar.arrow.power != 0){//attack
            playerDmg = (int) player.stats[2].value * (battleBar.arrow.power / 100);
            Log.Info("cosTakiego", "::attacked");
            battleBar.arrow.lastButton = 0;
            player.currentAnimRow = 1;
            player.currentAnimFrame = 0;
        }
        if(battleBar.arrow.lastButton == 2 && battleBar.arrow.power != 0){//defend
            player.stats[3].value = battleBar.arrow.defPower;
            Log.Info("cosTakiego", "::defended");
            battleBar.arrow.lastButton = 0;
            player.currentAnimRow = 2;
            player.currentAnimFrame = 0;
        }
        enemy.TakeDamage(playerDmg);
        player.TakeDamage(enemy);
    }
    public void Update(){
        setArrowPow();
        enemy.Update();
        player.Update();
        battleBar.Update();
        battlePanel.Update(battleBar.arrow);
    }

    public void Draw(){
        enemy.Draw();
        player.Draw();
        battlePanel.Draw();
        battleBar.Draw();
    }
}