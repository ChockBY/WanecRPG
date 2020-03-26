using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
/// <summary>
/// 攻打野蛮人任务
/// </summary>
class Attackbarbarians : Informationtask
{
    public static int KillCount;
    private int targetcount = 2;
    public override void Init()
    {
        taskName = "Attackbarbarians";
        AwardName = "coin-icon";
    }
    public override string DescribeQuestText()
    {
        return "任务：\n" + "击杀附近野蛮人\n" + "\n" + "奖励：\n" + "1000金币";
    }
    public override string ProcessQuestText()
    {
        return "任务：" + "击杀" + KillCount + "/" + targetcount + "野蛮人\n" + "\n" + "奖励：\n" + "1000金币";
    }
    override public bool CheckIsComplete()
    {
        if (KillCount >= targetcount)
            isComplete = true;
        else
            isComplete = false;
        return isComplete;
    }
}
