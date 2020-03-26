using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 防御值
/// </summary>
public class ArmorValue : GetCharactorProperties
{
    new private string name = "防御力:";
    public override void Getnumer()
    {
        text.text = name + status.Defence;
    }
}
