using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.FSM
{
    /// <summary>
    /// 丢失玩家
    /// </summary>
    class LosePlayerTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerid = FSMTriggersID.LosePlayer;
        }
        public override bool HandleTrigger(BaseFSM baseFSM)
        {

            if (baseFSM.targetObject != null)
            {
                bool b;

                return b = Vector3.Distance(baseFSM.targetObject.position, baseFSM.transform.position)> baseFSM.sightDistance;
            }
            return true;
        }
    }
}
