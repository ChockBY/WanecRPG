using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Plotconversation : MonoBehaviour
{
    public string[] conversations;
    public string[] NpcConversations;
    public GameObject talk;
    public Text text;
    public Text NpcText;
    public GameObject NpcTalk;
    public static bool OverTalk = false;
    private void Start()
    {
        if(OverTalk)
        talk.SetActive(true);
        text.DOText(conversations[0], 1f);
    }
    public  int count = 1;
    private int NpcCount = 0;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (talk.activeSelf)
            {
                PlayerTalk();
            }
            else if(NpcTalk.activeSelf)
            {
                PlayNpcTalk();
            }
        }
    }
    public void PlayNpcTalk()
    {
        NpcText.text = "";
        for (int i = NpcCount; i < NpcConversations.Length; i++)
        {
            if (NpcCount >= NpcConversations.Length - 1)
            {
                NpcCount = 1;
                NpcTalk.SetActive(false);
            }
            else
            {

                text.DOText(NpcConversations[i], 2.2f);
                //text.text = conversations[i];
                NpcCount++;
                break;
            }

        }
    }

    public void PlayerTalk()
    {
        text.text = "";
        for (int i = count; i < conversations.Length; i++)
        {
            if (count >= conversations.Length - 1)
            {
                count = 1;
                talk.SetActive(false);
            }
            else
            {

                text.DOText(conversations[i], 2.2f);
                //text.text = conversations[i];
                count++;
                break;
            }

        }
    }
}
