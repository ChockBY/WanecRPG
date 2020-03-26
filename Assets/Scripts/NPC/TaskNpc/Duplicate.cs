using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 副本任务
/// </summary>
class Duplicate : Informationtask
{
    public static bool isPass;
    public override void Init()
    {
         AwardName = "coin-icon"; 
    }
    public override bool CheckIsComplete()
    {
        return isPass;
    }
    public override string DescribeQuestText()
    {
        return "任务：\n" + "寻找副本先生完成一次副本\n" + "\n" + "奖励：\n" + "1000金币";
    }

    public override string ProcessQuestText()
    {
        return "任务：" + "通过" + "副本\n" + "\n" + "奖励：\n" + "1000金币";
    }
}

