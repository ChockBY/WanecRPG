using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI.FSM
{
    class AttackingState : FSMState
    {
        public override void Init()
        {
            stateid = FSMStateID.Attacking;
        }
        float attacktime = 0;
        public override void Action(BaseFSM fSM)
        {

            if (fSM.targetObject != null)
            {
                if (attacktime > fSM.chState.attackSpeed)
                {
                    fSM.AutoUseSkill();
                    attacktime = 0;
                }
                attacktime += Time.deltaTime;
            }

        }
        public override void EnterState(BaseFSM fSM)
        {
            fSM.StopMove();
            fSM.PlayAnimation(fSM.animParams.Idle);
        }
    }
}
