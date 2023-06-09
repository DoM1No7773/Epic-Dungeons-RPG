using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EpicDungeonsRPG;

struct BattleManager{

    private Player player;
    private BattleHud hud;
    private int[,] HitAreaData =new int[,]{{0,74},{74,86},{86,96,},{96,100}}; 
    public BattleManager(){
        player = new Player(texture: Global.content.Load<Texture2D>("Sprites/Player/player"),
        position: new Vector2(50, Global.graphics.GraphicsDevice.Viewport.Height / 2.8f));
        hud = new BattleHud(HitAreaData);
    }

    public void Update(){
        player.Update();
        hud.Update();
    }

    public void Draw(){
        player.Draw();
        hud.Draw();
    }
}