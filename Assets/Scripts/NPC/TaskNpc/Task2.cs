using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using ARPGDemo.Backpack;
using DG.Tweening;
class Task2:Task
{
    public DOTweenAnimation tweenAnimation;
    public override void Init()
    {
        task = Informationtask.Create(taskName) as Killhighking;
        DescribeQuestText();
        OKButton.SetActive(false);
    }
    private bool IsMove = true;
    public override void AcceptClick()
    {
        if(IsMove)
        {
            tweenAnimation.gameObject.SetActive(true);
            tweenAnimation.DOPlay();
            IsMove = false;
        }
        AcceptButton.SetActive(false);
        ProcessQuestText();
        OKButton.SetActive(true);
        IsAccept = true;
    }
    public override void CompleteTask()
    {
        if (task.CheckIsComplete())
        {
            iscomplete = true;
            OKButton.SetActive(false);
            AcceptButton.SetActive(true);
            this.gameObject.SetActive(false);
            taskNpc.task3.SetActive(true);
            TaskManager.instance.TaskID = 2;
            KnapsackManager.Instance.StoreItem(2,"MyBag");
            showcanvas.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Gui/战士服");
            showcanvas.transform.GetChild(1).GetComponent<Text>().text = "X1";
            ShowCompleteCanvas();
        }
    }
    public void PlayAnim()
    {
        Destroy(tweenAnimation.gameObject, 2f);
    }
}

