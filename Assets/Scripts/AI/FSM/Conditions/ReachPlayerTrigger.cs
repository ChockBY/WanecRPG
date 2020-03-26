using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 目标进入攻击范围
    /// </summary>
    class ReachPlayerTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerid = FSMTriggersID.ReachPlayer;
        }
        public override bool HandleTrigger(BaseFSM baseFSM)
        {

            if (baseFSM.targetObject != null)
            {
                bool b;
                return b = Vector3.Distance(baseFSM.transform.position, baseFSM.targetObject.position) <= baseFSM.chState.attackDistance;
            }
            return false;
        }
    }
}
