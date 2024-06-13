using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit
{
    public static Weapon_ItemObject defaultWeapon;
    public static Engine_ItemObject defaultEngine;
    public static Armor_ItemObject defaultArmor;
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
    public Unit(Unit values)
    {
        this.maxHealth = values.maxHealth;
        this.health = values.maxHealth;
        this.speed = values.speed;
        this.damage = values.damage;
        this.weapon = values.weapon;
        this.engine = values.engine;
        this.armor = values.armor;
    }

    public static Unit defaultValues = new Unit(15000, 3.5f, 150, defaultWeapon, defaultEngine, defaultArmor);
}
