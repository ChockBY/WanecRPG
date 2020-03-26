using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    class PursuitState:FSMState
    {
        public override void Init()
        {
            stateid = FSMStateID.Pursuit;
        }
        public override void Action(BaseFSM fSM)
        {            
            if (fSM.targetObject == null)
                return;
            fSM.PlayAnimation(fSM.animParams.Run);
            fSM.MoveToTarget(fSM.targetObject.position, fSM.moveSpeed, fSM.chState.attackDistance);               
        }
        public override void ExitState(BaseFSM fSM)
        {
            //停下来转换状态 追逐=》攻击
            fSM.StopMove();
        }
    }
}
