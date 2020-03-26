using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    public class IdleState : FSMState
    {
        public override void Init()
        {
            stateid = FSMStateID.Idle;
        }
        public override void Action(BaseFSM fSM)
        {

        }
        public override void EnterState(BaseFSM fSM)
        {
            fSM.PlayAnimation(fSM.animParams.Idle);
        }
    }
}
