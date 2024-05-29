using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit
{
    public int maxHealth;
    public int health;

    public float speed;

    public int damage;

    public Weapon_ItemObject weapon;
    public Engine_ItemObject engine;
    public Armor_ItemObject armor;

    public Unit(int maxHealth, float speed, int damage, Weapon_ItemObject weapon, Engine_ItemObject engine, Armor_ItemObject armor)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.speed = speed;
        this.damage = damage;
        this.weapon = weapon;
        this.engine = engine;
        this.armor = armor;
    }
}
