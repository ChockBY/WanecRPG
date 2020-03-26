using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 状态转换条件枚举
/// </summary>
namespace AI.FSM
{
    public enum FSMTriggersID
    {
        NoHealth,//生命为0
        SawPlayer,//发现目标
        ReachPlayer,//目标进入攻击范围
        LosePlayer,//丢失玩家
        CompletePatrol,//完成巡逻
        KilledPlayer,//打死目标
        WithOutAttackRange//目标不在攻击范围玩家离开攻击范围
    }
}
