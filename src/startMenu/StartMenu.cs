using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Android.Util;

namespace EpicDungeonsRPG;

public struct StartMenuManager{

    private ScrollView scrollView;
    public StartMenuHud startMenuHud;
    public StartMenuManager(){
        scrollView = new(Global.content.Load<Texture2D>("Hud/levelBackground"),new Vector2(0,0));
        startMenuHud = new(Global.content.Load<Texture2D>("Hud/overlay"),"playerViewBtn");
    }

    public void Update(){
        scrollView.Update();
        startMenuHud.Update();
    }

    public void Draw(){
        scrollView.Draw();
        startMenuHud.Draw();
    }
}