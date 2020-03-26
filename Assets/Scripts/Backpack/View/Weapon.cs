using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARPGDemo.Character;
using UnityEngine;
public class Weapon : Item
{
    public int Damage { get;private set; }
    public int charactorDamage { get; private set; }
    public Weapon(int id, string name, string description, int buyPrice, int sellPrice, string icon, int damage,string image)
        : base(id, name, description, buyPrice, sellPrice, icon, image)
    {
        this.Damage = damage;
        base.ItemType = "Weapon";
        charactorDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>().Damage + damage;
    }
}
