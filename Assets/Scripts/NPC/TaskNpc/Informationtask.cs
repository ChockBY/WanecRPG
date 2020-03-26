using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
/// <summary>
/// 任务
/// </summary>
abstract public class Informationtask
{
    public string taskName;
    public bool isComplete;
    public string AwardName;
    abstract public bool CheckIsComplete();
    virtual public void Init()
    {

    }
    public static Informationtask Create(string taskName)
    {
        Informationtask informationtask = null;
        switch(taskName)
        {
            case "Attackbarbarians":
                informationtask = new Attackbarbarians();
                break;
            case "Killhighking":
                informationtask = new Killhighking();
                break;
            case "Duplicate":
                informationtask = new Duplicate();
                break;

        }
        return informationtask;
    }
    virtual public void TasksAward()
    {

    }
    abstract public string DescribeQuestText();
    abstract public string ProcessQuestText();
}
