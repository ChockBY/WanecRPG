using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    class DeadState : FSMState
    {
        public override void Init()
        {
            stateid = FSMStateID.Dead;
        }
        public override void Action(BaseFSM fSM)
        {

        }
        public override void EnterState(BaseFSM fSM)
        {
            fSM.enabled = false;
            fSM.PlayAnimation(fSM.animParams.Dead);

        }
    }
}
