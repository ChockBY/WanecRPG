using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Backpack;
using ARPGDemo.Character;
public class BuyShopItem : MonoBehaviour {
    public GameObject selectCircle;
    private AudioSource source;
    private  PlayerStatus status;
    private void Start()
    {
        source = GameObject.Find("BuyButton").GetComponent<AudioSource>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>() as PlayerStatus;
    }
    public void Buy()
    {
        Item item = selectCircle.transform.parent.GetComponent<itemInfromation>().Item;
        if (item == null)
            return;
        if (status.Money >= item.BuyPrice)
        {
            source.clip = Resources.Load<AudioClip>("Music/Buysucceeds");
            KnapsackManager.Instance.StoreItem(item.Id,"MyBag");
            status.Money -= item.BuyPrice;
        }
        else
        {
            source.clip = Resources.Load<AudioClip>("Music/errorSound");
        }
        source.Play();
    }
}
