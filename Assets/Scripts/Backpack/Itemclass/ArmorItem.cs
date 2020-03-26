using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ARPGDemo.Backpack;
public class ArmorItem : itemInfromation
{
    public override void Init()
    {
        switch (this.transform.parent.name)
        {
            case "2":
                Item = KnapsackManager.Instance.ItemList[2];
                break;
            case "3":
                Item = KnapsackManager.Instance.ItemList[3];
                break;
        }
    }
}
