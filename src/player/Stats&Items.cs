using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace EpicDungeonsRPG;


public enum Atribute{
    attack,health,armor,critChance
}

public enum RarityTemplate{
        common, uncommon, epic, legendary, cursed
}

public enum ItemTypeTemplate{
        helmet, boots, chest, weapon, buffItem, storyItem, other
}
public class Stat{
    public Atribute atributeName;
    public int value {get;set;}
}

public class Item{
    public string name {get;set;}
    public RarityTemplate rarity;
    public ItemTypeTemplate itemType;
    public List<Stat> stats;
    public Item(string name, RarityTemplate rarity, ItemTypeTemplate itemType, List<Stat> stats)
    {
        this.name = name;
        this.rarity = rarity;
        this.itemType = itemType;
        this.stats = stats;
    }
}