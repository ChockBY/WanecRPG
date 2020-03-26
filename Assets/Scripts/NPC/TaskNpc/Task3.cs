using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using ARPGDemo.Backpack;
using DG.Tweening;
class Task3:Task
{
    public override void Init()
    {
        task = Informationtask.Create(taskName) as Duplicate;
        DescribeQuestText();
        OKButton.SetActive(false);
    }
    public override void AcceptClick()
    {
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
            taskNpc.task2.SetActive(true);
            TaskManager.instance.TaskID = 3;
            KnapsackManager.Instance.StoreItem(2,"MyBag");
            showcanvas.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Gui/coin-icon");
            showcanvas.transform.GetChild(1).GetComponent<Text>().text = "X1000";
            ShowCompleteCanvas();
        }
    }
}

