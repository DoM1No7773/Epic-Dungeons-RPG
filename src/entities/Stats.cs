using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace EpicDungeonsRPG;


public enum Atribute
{
    attack, health, armor, critChance, maxHealth, attackTimeOut
}
public class Stat
{
    public Atribute atributeName;
    public int value { get; set; }
}