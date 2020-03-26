using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
class Killhighking : Informationtask
{
    public static bool iskill = false;
    public override void Init()
    {
        AwardName = "战士服";
    }
    public override bool CheckIsComplete()
    {
        return iskill;
    }
    public override string DescribeQuestText()
    {
        return "任务：\n" + "击杀山丘之神，他为了保护子民牺牲了大自然必须阻止他\n" + "\n" + "奖励：\n" + "战士服";
    }

    public override string ProcessQuestText()
    {
        return "任务：" + "击杀" + "山丘之王\n" + "\n" + "奖励：\n" + "战士服金币";
    }
}

