using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    class CompletePatrolTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerid = FSMTriggersID.CompletePatrol;
        }
        public override bool HandleTrigger(BaseFSM baseFSM)
        {
            if (baseFSM.IsCompletePatrol)
                return true;
            return false;
        }
    }
}
