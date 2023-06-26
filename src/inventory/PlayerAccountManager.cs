using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Android.Util;

namespace EpicDungeonsRPG;

public struct PlayerAccount{

    public int attack;
    public int health;
    public int gold;
    public PlayerAccount(){
        attack = 10;
        health = 10;
        gold = 0;
    }
    public void AddAttack(int att){
        this.attack += att;
    }

    public void AddHealth(int hp){
        this.health += hp;
    }

    public void AddGold(int gold){
        this.gold += gold;
    }
}