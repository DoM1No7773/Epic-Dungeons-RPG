using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Android.Util;

namespace EpicDungeonsRPG;

public enum BattleState{
    none, inBattle, victory, defeat
}
public struct BattleManager{

    private Player player;

    public BattleState battleState;
    public Enemy enemy;
    private BattleBar battleBar;
    private BattlePanel battlePanel;

    public int gold= 0;
    private Texture2D backgroundTexture;
    private int[,] HitAreaData =new int[,]{{0,74},{74,86},{86,96,},{96,100}};

    private SpriteFont font;
    private Vector2 positionString; 

    private void Init(){
        var windowWidth = Global.graphics.GraphicsDevice.Viewport.Width;
        var windowHeight = Global.graphics.GraphicsDevice.Viewport.Height;

        enemy = new Enemy(Global.content.Load<Texture2D>("player"), new Vector2(0,0));
        player = new Player(Global.content.Load<Texture2D>("Sprites/Player/player"), new Vector2(50, windowHeight / 2.8f));
        battlePanel = new BattlePanel(Global.content.Load<Texture2D>("BattleBar/battlePanel"), new Vector2(0, windowHeight / 1.68f));
        battleBar = new BattleBar(Global.content.Load<Texture2D>("BattleBar/battleBar"), new Vector2((int)(windowWidth / 2f), (int)(windowHeight / 1.5f)),HitAreaData);
        backgroundTexture = Global.content.Load<Texture2D>("Background/background");
        battleState = BattleState.inBattle;

        font = Global.content.Load<SpriteFont>("Fonts/contBrush");
        positionString = new Vector2(Global.graphics.GraphicsDevice.Viewport.Width / 2f,Global.graphics.GraphicsDevice.Viewport.Height / 2f);
    }
    public BattleManager(){
        var windowWidth = Global.graphics.GraphicsDevice.Viewport.Width;
        var windowHeight = Global.graphics.GraphicsDevice.Viewport.Height;

        enemy = new Enemy(Global.content.Load<Texture2D>("player"), new Vector2(0,0));
        player = new Player(Global.content.Load<Texture2D>("Sprites/Player/player"), new Vector2(50, windowHeight / 2.8f));
        battlePanel = new BattlePanel(Global.content.Load<Texture2D>("BattleBar/battlePanel"), new Vector2(0, windowHeight / 1.68f));
        battleBar = new BattleBar(Global.content.Load<Texture2D>("BattleBar/battleBar"), new Vector2((int)(windowWidth / 2f), (int)(windowHeight / 1.5f)),HitAreaData);
        backgroundTexture = Global.content.Load<Texture2D>("Background/background");
        battleState = BattleState.inBattle;

        font = Global.content.Load<SpriteFont>("Fonts/contBrush");
        positionString = new Vector2(Global.graphics.GraphicsDevice.Viewport.Width / 2f,Global.graphics.GraphicsDevice.Viewport.Height / 2f);
    }
    public void setArrowPow(){
        int playerDmg = 0;
        if(battleBar.arrow.lastButton == 1 && battleBar.arrow.power != 0){
            playerDmg = (int)(player.stats[2].value * (battleBar.arrow.power / 100f));
            battleBar.arrow.lastButton = 0;
            player.currentAnimRow = 1;
            player.currentAnimFrame = 0;
        }
        if(battleBar.arrow.lastButton == 2 && battleBar.arrow.power != 0){
            player.stats[3].value = battleBar.arrow.defPower;
            battleBar.arrow.lastButton = 0;
            player.currentAnimRow = 2;
            player.currentAnimFrame = 0;
        }
        enemy.TakeDamage(playerDmg);
        player.TakeDamage(enemy, battleBar.arrow);
    }
    private float currentCountDownValue = 3;
    private string info = "";
    private Color infoColor = Color.White;

    public void Reset(){
        Init();
        info = "";
        infoColor = Color.White;
        currentCountDownValue = 3;
        gold= 0;
    }
    public void Update(){

        if(this.battleState == BattleState.victory){
            info = "VICTORY!!!";
            infoColor = Color.Yellow;
        }
        if(this.battleState == BattleState.defeat){
            info = "DEFEAT :C";
            infoColor = Color.Red;
        }
        if(this.battleState == BattleState.inBattle){

            info = ""+System.Math.Ceiling(currentCountDownValue);
            if(currentCountDownValue <= 0){
                info = "";
                setArrowPow();
                enemy.Update();
                player.Update();
                battleBar.Update();
                battlePanel.Update(battleBar.arrow);
            }else currentCountDownValue -= (float) Global.gameTime.ElapsedGameTime.TotalSeconds;
        }

        if(enemy.healthBar.currentValue <= 0 & enemy.currentAnimFrame == 2) battleState = BattleState.victory;
        else if(player.healthBar.currentValue <= 0 & player.currentAnimFrame == 2) battleState = BattleState.defeat;
    }

    public void Draw(){
        var scale = 12f;
        Global.spriteBatch.Draw(backgroundTexture, new Vector2(0,-170), new Rectangle(0,0,backgroundTexture.Width,backgroundTexture.Height), Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.01f);
        enemy.Draw();
        player.Draw();
        battlePanel.Draw();
        battleBar.Draw();

        var scaleString = 7f;
        Global.spriteBatch.DrawString(font, info, positionString, infoColor, 0f, new Vector2(font.MeasureString(info).X / 2,font.MeasureString(info).Y / 2), scaleString, SpriteEffects.None, 1f);
    }
}