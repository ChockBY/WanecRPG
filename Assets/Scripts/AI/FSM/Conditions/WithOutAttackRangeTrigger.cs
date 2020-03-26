using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.FSM
{
    /// <summary>
    /// 目标不在攻击范围
    ///玩家离开攻击范围
    /// </summary>
    class WithOutAttackRangeTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerid = FSMTriggersID.WithOutAttackRange;
        }
        public override bool HandleTrigger(BaseFSM baseFSM)
        {

            if (baseFSM.targetObject != null)
            {
                bool b;
                return b = Vector3.Distance(baseFSM.targetObject.position, baseFSM.transform.position) > baseFSM.chState.attackDistance
                    && Vector3.Distance(baseFSM.targetObject.position, baseFSM.transform.position) < baseFSM.sightDistance;
            }

            return true;
        }
    }
}
