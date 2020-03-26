using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Backpack;

public class IsDropButton : MonoBehaviour
{
    private KnapsackManager knapsack;
    private void Start()
    {
          knapsack = KnapsackManager.Instance;//
    }
    public void OnButtonDrop()
    {
        knapsack.isDrop = true;
        //knapsack.IsDropCanvas.SetActive(false);
        Destroy(knapsack.IsDrob);
;        ItemModel.DeleteItem(knapsack.prev.name);
        Debug.Log("物品已扔");
    }
    public void OnButtonCancel()
    {
        knapsack.isDrop = false;
        Destroy(knapsack.IsDrob);
        //knapsack.IsDropCanvas.SetActive(false);
        print("没有交换");
        Item item = ItemModel.GetItem(knapsack.prev.name);
        knapsack.CreatNewItem(item, knapsack.prev);
    }
}
