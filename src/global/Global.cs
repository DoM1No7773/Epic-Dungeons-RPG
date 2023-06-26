using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;

namespace EpicDungeonsRPG;
public struct Global{
    public static int a=5;
    public static GraphicsDeviceManager graphics;
    public static SpriteBatch spriteBatch;
    public static ContentManager content;
    public static GameTime gameTime;
    public static GameState gameState;
    public static TouchCollection touchState;
    public static BattleManager battle;
    public static Level level;
    public static PlayerAccount playerAccount;
}