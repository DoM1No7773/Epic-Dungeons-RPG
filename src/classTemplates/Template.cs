using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EpicDungeonsRPG;

public abstract class Basic2D{
    protected Texture2D texture;
    protected Vector2 position;
}
public abstract class Entity 
{
    protected Texture2D texture;
    protected Vector2 position;
    protected List<Item> items;
    public List<Stat> stats;
    protected Rectangle sourceRect;
    protected ProgressBar healthBar;
    public byte currentAnimFrame;
    protected byte frameSize;
    protected float timer;
    protected float animationSpeed;
}