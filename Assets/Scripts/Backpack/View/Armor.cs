using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARPGDemo.Character;
using UnityEngine;
public class Armor : Item
{
    public int defend { get; private set; }
    public int charactorDefence { get; private set; }
    public Armor(int id, string name, string description, int buyPrice, int sellPrice, string icon, int defend, string image)
        : base(id, name, description, buyPrice, sellPrice, icon, image)
    {
        this.defend = defend;
        base.ItemType = "Armor";
        charactorDefence = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>().Defence + defend;
    }
}
