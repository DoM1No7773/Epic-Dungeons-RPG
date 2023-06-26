using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Android.Util;

namespace EpicDungeonsRPG;

public enum GameState{
    startMenu, battle, inventory
}
public class GameManager
{
    private StartMenuManager startMenu;
    private InventoryManager inventory;
    public GameManager()
    {
        Global.gameState = GameState.startMenu;
        Global.playerAccount = new PlayerAccount();
        Global.battle = new BattleManager();
        startMenu = new StartMenuManager();
        inventory = new InventoryManager();
        Global.level = new Level();
    }

    private float waitSecs = 2;
    public void Update()
    {   
        if(Global.battle.battleState == BattleState.victory || Global.battle.battleState == BattleState.defeat){
            waitSecs -= (float) Global.gameTime.ElapsedGameTime.TotalSeconds;
            if(waitSecs <= 0){
                Global.gameState = GameState.startMenu;
                waitSecs = 2;
                if(Global.battle.battleState == BattleState.victory)
                    Global.playerAccount.AddGold(Global.battle.gold);
                Global.battle.battleState = BattleState.none;
            }
        }

            switch (Global.gameState)
            {
                case GameState.battle:
                    Global.battle.Update();
                    break;
                case GameState.startMenu:
                    startMenu.Update();
                    break;
                case GameState.inventory:
                    inventory.Update();
                    break;
                default:
                    break;
            }
    }
    public void Draw()
    {
        switch (Global.gameState)
        {
            case GameState.battle:
                Global.battle.Draw();
                break;
            case GameState.startMenu:
                startMenu.Draw();
                break;
            case GameState.inventory:
                inventory.Draw();
                break;
            default:
                break;
        }
    }
}