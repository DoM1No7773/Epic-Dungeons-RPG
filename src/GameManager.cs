using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Android.Util;

namespace EpicDungeonsRPG;
public class GameManager
{
    private enum gameState
    {
        startMenu, battle, inventory
    }

    gameState currentState;
    private BattleManager battle;
    private startMenuManager startMenu;
    private InventoryManager inventory;
    public GameManager()
    {
        currentState = gameState.battle;
        battle = new BattleManager();
        startMenu = new startMenuManager();
        inventory = new InventoryManager();
    }
    public void Update()
    {
        switch (currentState)
        {
            case gameState.battle:
                battle.Update();
                break;
            case gameState.startMenu:
                startMenu.Update();
                break;
            case gameState.inventory:
                inventory.Update();
                break;
            default:
                break;
        }
    }
    public void Draw()
    {
        switch (currentState)
        {
            case gameState.battle:
                battle.Draw();
                break;
            case gameState.startMenu:
                startMenu.Draw();
                break;
            case gameState.inventory:
                inventory.Draw();
                break;
            default:
                break;
        }
    }
}