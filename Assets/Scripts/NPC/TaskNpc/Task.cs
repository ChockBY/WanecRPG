using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ARPGDemo.Character;
public class Task : MonoBehaviour
{
    public Text Describetext;
    public int KillCount;
    public GameObject AcceptButton;
    public GameObject OKButton;
    public bool IsAccept;
    public string taskName;
    public Informationtask task;
    public bool iscomplete;
    protected TaskNpc taskNpc;
    protected AudioSource source;
    private PlayerStatus status;
    private GameObject NpcTalk;
    public void Start()
    {
        status = GameObject.Find("Player").GetComponent<PlayerStatus>();
        Init();
        taskNpc = GameObject.Find("keeper_prefab").GetComponent<TaskNpc>();
    }
    private void Update()
    {
        UpdateText();
        task.ProcessQuestText();
        if (showcanvas)
            StartCoroutine(HidetheCanvas());
    }
    virtual public void Init()
    {
        task = Informationtask.Create(taskName) as Attackbarbarians;
        DescribeQuestText();
        OKButton.SetActive(false);
    }
    virtual public void AcceptClick()
    {
        AcceptButton.SetActive(false);
        ProcessQuestText();
        OKButton.SetActive(true);
        IsAccept = true;
    }
    virtual public void CompleteTask()
    {
        if (task.CheckIsComplete())
        {
            iscomplete = true;
            showcanvas.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Gui/coin-icon");
            ShowCompleteCanvas();
            OKButton.SetActive(false);
            AcceptButton.SetActive(true);
            this.gameObject.SetActive(false);
            taskNpc.task2.SetActive(true);
            TaskManager.instance.TaskID = 1;
            status.Money += 1000;

        }
    }

    public void DescribeQuestText()
    {
        Describetext.text = task.DescribeQuestText();
    }
    public void ProcessQuestText()
    {
        Describetext.text = task.ProcessQuestText();
    }
    public void UpdateText()
    {
        if (IsAccept)
        {

            ProcessQuestText();
        }

        else
            DescribeQuestText();
    }
    public GameObject showcanvas;
    public void ShowCompleteCanvas()
    {
        showcanvas.SetActive(true);
    }
    public IEnumerator HidetheCanvas()
    {
        yield return new WaitForSeconds(1f);
        showcanvas.gameObject.SetActive(false);
    }
}
