using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ARPGDemo.Backpack;
public class CharacterBagtemplate
{
    /// <summary>   /// 物品ID    /// </summary>
    static public int ID;
    /// <summary>    /// 物品名字    /// </summary>
    static public string Name;
    /// <summary>    /// 描述   /// </summary>
    static public string Description;
    /// <summary>    /// 购入价格  /// </summary>
    static public int BuyPrice;
    /// <summary>    /// 出售价格    /// </summary>
    static public int SellPrice;
    /// <summary>    /// 物品标识    /// </summary>
    static public string Icon;
    /// <summary>    /// 物品类型    /// </summary>
    static public string ItemType;
    /// <summary>    ///防御或者伤害    /// </summary>
    static public int DmORDf;
    /// <summary>    /// 物品图片    /// </summary>
    static public string Sprite;
    public static void NewItem(string ItemType)
    {
        switch (ItemType)
        {
            case "Weapon":
                KnapsackManager.Instance.StoreItem(ID, "MyBag");
                break;
            case "Armor":
                KnapsackManager.Instance.StoreItem(ID, "MyBag");
                break;
        }
    }
}
