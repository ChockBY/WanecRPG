using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器值
/// </summary>
public class Weaponsvalue :GetCharactorProperties {
   new private string name = "攻击力:";
    public override void Getnumer()
    {
           text.text = name + status.Damage;
    }
}
