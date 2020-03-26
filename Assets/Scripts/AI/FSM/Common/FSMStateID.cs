using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 状态编号枚举
/// </summary>
namespace AI.FSM
{
    public enum FSMStateID 
    {
        none,//无
        Idle,//待机
        Dead,//死亡
        Pursuit,//追逐
        Attacking,//攻击
        Default,//默认
        Patrolling//巡逻
    }
}
