using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Android.Util;
using System;

namespace EpicDungeonsRPG;
public enum HitAreaType
{
    weak, normal, strong, critical
}
public struct HitArea
{
    public HitAreaType name;
    public Vector2 position;
    public Rectangle sourceRect;
    public Texture2D texture;
    public HitArea(HitAreaType name, Vector2 position, Rectangle sourceRect, Texture2D texture)
    {
        this.name = name;
        this.position = position;
        this.sourceRect = sourceRect;
        this.texture = texture;
    }
}
public struct BattleBar
{
    private Vector2 position;
    private Texture2D texture;
    private List<HitArea> Areas;
    public Arrow arrow;

    private float scale = 12f;
    public BattleBar(Texture2D texture, Vector2 position, int[,] HitAreaData)
    {
        this.texture = texture;
        this.position = position;

        arrow = new Arrow(Global.content.Load<Texture2D>("BattleBar/arrow"), new Point((int)(position.X / 3.4f), (int)(texture.Width * 12.2f)));

        this.Areas = new List<HitArea>();

        int helper = 0;
        foreach (var item in Enum.GetNames<HitAreaType>())
        {
            Areas.Add(new HitArea(Enum.Parse<HitAreaType>(item), new Vector2((int)(arrow.moveArea.X + (HitAreaData[helper, 0] / 100f * (arrow.moveArea.Y-arrow.moveArea.X))), (int)(position.Y / 1.07f)),
            new Rectangle((int)(HitAreaData[helper, 0] / 100f * 42), 0, (int)((HitAreaData[helper, 1] / 100f) * 42) - (int)(HitAreaData[helper, 0] / 100f * 42), texture.Height),
            Global.content.Load<Texture2D>("BattleBar/" + item)));
            helper++;
        }
    }

    private void DrawHud()
    {
        var sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
        Global.spriteBatch.Draw(texture, position, sourceRect, Color.White, 0f,
        new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0.92f);
    }

    private void DrawAreas()
    {
        var helper = 0.95f;
        foreach (var item in this.Areas)
        {
            Rectangle rect;
            if (item.name != HitAreaType.critical) rect = new Rectangle(item.sourceRect.X, item.sourceRect.Y, item.sourceRect.Width+1, item.sourceRect.Height);
            else rect = new Rectangle(item.sourceRect.X, item.sourceRect.Y, item.sourceRect.Width, item.sourceRect.Height);

            Global.spriteBatch.Draw(item.texture, item.position, rect, Color.White, 0f,
            new Vector2(0, 0), scale, SpriteEffects.None, helper);
            helper += 0.01f;
        }
    }
    public void Update()
    {
        arrow.Update(Areas);
    }

    public void Draw()
    {
        DrawHud();
        DrawAreas();
        arrow.Draw();
    }
}