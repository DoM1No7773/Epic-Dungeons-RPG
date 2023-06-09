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
public struct BattleHud
{

    private Vector2 hudPosition;
    private Texture2D hudTexture;
    private List<HitArea> Areas;
    private Arrow arrow;
    private BattlePanel battlePanel;
    public BattleHud(int[,] HitAreaData)
    {

        var windowWidth = Global.graphics.GraphicsDevice.Viewport.Width;
        var windowHeight = Global.graphics.GraphicsDevice.Viewport.Height;
        this.hudPosition = new Vector2((int)(windowWidth / 2f), (int)(windowHeight / 1.5f));
        this.hudTexture = Global.content.Load<Texture2D>("BattleBar/battleBar");

        battlePanel = new BattlePanel(new Vector2(0,windowHeight / 1.68f),Global.content.Load<Texture2D>("BattleBar/battlePanel"));
        arrow = new Arrow(texture: Global.content.Load<Texture2D>("BattleBar/arrow"), new Point((int)(hudPosition.X / 3.4f), (int)(hudTexture.Width * 12.2f)));
        arrow.GetButtons(battlePanel.attackbtn,battlePanel.defendbtn);

        this.Areas = new List<HitArea>();

        int helper = 0;
        foreach (var item in Enum.GetNames<HitAreaType>())
        {
            Areas.Add(new HitArea(name: Enum.Parse<HitAreaType>(item), position: new Vector2((int)(arrow.moveArea.X + (HitAreaData[helper, 0] / 100f * (arrow.moveArea.Y - arrow.moveArea.X))), (int)(hudPosition.Y / 1.07f)),
            sourceRect: new Rectangle((int)(HitAreaData[helper, 0] / 100f * 42), 0, (int)((HitAreaData[helper, 1] / 100f) * 42) - (int)(HitAreaData[helper, 0] / 100f * 42), hudTexture.Height),
            texture: Global.content.Load<Texture2D>("BattleBar/" + item)));
            helper++;
        }
    }

    private void DrawHud()
    {
        var sourceRect = new Rectangle(0, 0, hudTexture.Width, hudTexture.Height);
        float scale = 12f;
        Global.spriteBatch.Draw(hudTexture, hudPosition, sourceRect, Color.White, 0f,
        new Vector2(hudTexture.Width / 2, hudTexture.Height / 2), scale, SpriteEffects.None, 1f);
    }

    private void DrawArea(HitArea item)
    {
        float scale = 12f;

        // Log.Info("cosTakiego", "name: "+item.name+", position: "+item.position+", sourceRect: "+item.sourceRect+", texture: "+item.texture);
        Rectangle rect;
        if (item.name != HitAreaType.critical) rect = new Rectangle(item.sourceRect.X, item.sourceRect.Y, item.sourceRect.Width + 1, item.sourceRect.Height);
        else rect = new Rectangle(item.sourceRect.X, item.sourceRect.Y, item.sourceRect.Width, item.sourceRect.Height);

        Global.spriteBatch.Draw(item.texture, item.position, rect, Color.White, 0f,
        new Vector2(0, 0), scale, SpriteEffects.None, 1f);
    }

    private void DrawAreas()
    {
        foreach (var item in this.Areas)
        {
            DrawArea(item);
        }
    }
    public void Update()
    {
        battlePanel.Update();
        arrow.Update(Areas);
    }

    public void Draw()
    {
        battlePanel.Draw();
        DrawHud();
        DrawAreas();
        arrow.Draw();
    }
}