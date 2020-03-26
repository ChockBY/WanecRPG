using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ARPGDemo.Backpack;
public class WeaponItem : itemInfromation {
    public override void Init()
    {
        switch (this.transform.parent.name)
        {
            case "0":
                Item = KnapsackManager.Instance.ItemList[0];
                break;
            case "1":
                Item = KnapsackManager.Instance.ItemList[1];
                break;
        }
    }
}
