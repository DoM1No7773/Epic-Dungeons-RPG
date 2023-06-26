using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Util;

namespace EpicDungeonsRPG;
public struct LevelBtn{
    private byte number;
    private Texture2D texture;
    private Vector2 position;
    private SpriteFont font;
    private Rectangle sourceRect;
    private float scale;
    private Vector2 positionString;
    private Color fontColor = Color.Red;

    public LevelBtn(Texture2D texture, Vector2 position, byte number, float posOrder,byte rowOrder){
        this.texture = texture;
        this.position = position;
        this.number = (byte)(number+1);
        this.sourceRect = new Rectangle(0,0,texture.Width,texture.Height);

        this.scale = ((float)Global.graphics.GraphicsDevice.Viewport.Width/(float)texture.Width)/3f;
        this.font = Global.content.Load<SpriteFont>("Fonts/contBrush");

        this.position = new Vector2(((Global.graphics.GraphicsDevice.Viewport.Width/2) - (texture.Width * scale)) / posOrder ,position.Y + (rowOrder*(texture.Height * scale)+ (rowOrder*50)+50));
        this.positionString = new Vector2(this.position.X + ((texture.Width * scale)/2) - 10, this.position.Y + ((texture.Height * scale)/2));
    }

    public void Update(){
        var touch = Global.touchState;

        foreach (var item in touch){
            if(new Rectangle((int)position.X,(int)position.Y,(int)(texture.Width * scale),(int)(texture.Height * scale)).Contains(item.Position)){
                Global.battle.Reset();
                Global.battle.enemy = Global.level.levels[number-1].enemy;

                Global.battle.enemy.attakTimeOutBar.currentValue = Global.battle.enemy.stats[2].value;
                Global.battle.enemy.attakTimeOutBar.maxValue = Global.battle.enemy.stats[2].value;

                Global.battle.enemy.healthBar.currentValue = Global.battle.enemy.stats[1].value;
                Global.battle.enemy.healthBar.maxValue = Global.battle.enemy.stats[1].value;

                Global.battle.gold = Global.level.levels[number-1].gold;
                Global.battle.battleState = BattleState.inBattle;
                Global.gameState = GameState.battle;
                Log.Info("cosTakiego", "::"+Global.level.levels[number-1].gold);
            }
        }
    }

    public void Draw(){
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.2f);
        Global.spriteBatch.DrawString(font, ""+number, positionString, fontColor, 0f, new Vector2(font.MeasureString(""+number).X / 2f,font.MeasureString(""+number).Y / 2f), 12f, SpriteEffects.None, 0.21f);
    }
} 