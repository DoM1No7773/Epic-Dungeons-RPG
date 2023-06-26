using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Util;

namespace EpicDungeonsRPG;

public struct StartMenuBtn{

    private Vector2 position;
    private Texture2D texture;
    private Rectangle sourceRect;
    private float scale;
    private string btnName;
    private float opacity=1f;
    private bool isEnabled = true;
    public StartMenuBtn(Texture2D texture, Vector2 position,string btnName){
        this.texture = texture;
        this.position = position;
        this.sourceRect = new Rectangle(0,0,texture.Width, texture.Height);
        this.scale = (float)Global.graphics.GraphicsDevice.Viewport.Width/(float)texture.Width;
        this.btnName = btnName;
    }
    public void Update(){

        
        var touch =Global.touchState;

        foreach (var item in touch)
        {  
            
            var endPosition = new Vector2(this.position.X+(texture.Width*scale), this.position.Y+(texture.Height*scale));

            if((this.position.X <= item.Position.X && this.position.Y <= item.Position.Y && endPosition.X >= item.Position.X && endPosition.Y >= item.Position.Y)&(isEnabled)){
                
                if(item.State == TouchLocationState.Released){
                    if(btnName == "homeBtn")
                        Global.gameState = GameState.startMenu;
                    if(btnName == "playerViewBtn")
                        Global.gameState = GameState.inventory;
                }
            }
        }

        if(!isEnabled) opacity = 0.5f;
        else opacity=1f;

    }

    public void Draw(){
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White * opacity, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.8f);
    }
}