using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
public class TaskNpc : NPCShop
{
    public Text Describetext;
    public int KillCount;
    public GameObject AcceptButton;
    public GameObject OKButton;
    public bool IsAccept;
    public string taskName;
    public Informationtask task;
    public bool iscomplete;
    public GameObject task1;
    public GameObject task2;
    public GameObject task3;
    private static bool isFirstTalk = true;
    public GameObject Npctalk;
    public override void Init()
    {
        canvasAnim = GameObject.Find(PlaneName).GetComponent<playCanvasAnim>();
    }
    public override void ClickNpc()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButton(0))
        {

            if (Physics.Raycast(ray, out hit, 15f)&&!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.collider.gameObject.tag == this.PlaneName)
                {
                    if (isFirstTalk)
                    {
                        Npctalk.SetActive(true);
                        Text text = Npctalk.GetComponentInChildren<Text>();
                        text.DOText("请少侠帮帮我吧,最近的野蛮人经常过来捣乱，扰乱了我的生活", 2f);
                        isFirstTalk = false;
                    }
                    else if (isFirstTalk == false)
                    {
                        canvasAnim.PlayAnim();
                    }

                }

            }
        }
    }
}
