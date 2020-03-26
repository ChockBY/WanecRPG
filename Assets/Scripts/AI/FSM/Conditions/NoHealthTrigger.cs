using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARPGDemo.Character;
namespace AI.FSM
{
    class NoHealthTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerid = FSMTriggersID.NoHealth;
        }
        public override bool HandleTrigger(BaseFSM baseFSM)
        {
           return baseFSM.chState.HP <= 0;
        }
    }
}
