using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Android.Util;

namespace EpicDungeonsRPG;

public class Main : Game
{
    GameManager gameManager; 
    public Main()
    {
        Global.graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Global.graphics.IsFullScreen = true;
        Global.graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        Global.graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        Global.graphics.SupportedOrientations = DisplayOrientation.Portrait;
        Global.graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }
    protected override void LoadContent()
    {
        Global.spriteBatch = new SpriteBatch(GraphicsDevice);
        Global.content = Content;
        gameManager = new GameManager();
    }

    protected override void Update(GameTime gameTime)
    {
        Global.gameTime = gameTime;
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        gameManager.Update();
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        Global.spriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);
        gameManager.Draw();
        Global.spriteBatch.End();
        base.Draw(gameTime);
    }
}
