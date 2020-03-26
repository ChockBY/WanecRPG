using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ARPGDemo.Backpack;
public class itemInfromation : MonoBehaviour
{
    public Item Item;
    private void Start()
    {
        Init();
    }
    virtual public void Init()
    {

    }

    // public void OnPointerDown(PointerEventData eventData)
    //{
    //    print(eventData.pointerEnter.gameObject.name);
    //    if(eventData.pointerEnter.gameObject!=null&&eventData.pointerEnter.tag=="shopitem")
    //    {
    //        Description.text = KnapsackManager.Instance.GetTooltipText(Item);
    //    }
    //}
}
