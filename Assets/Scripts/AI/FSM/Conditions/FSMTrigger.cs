using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.FSM
{
    abstract public class FSMTrigger {

        public FSMTriggersID triggerid;
       virtual public bool HandleTrigger(BaseFSM baseFSM)
        {
            return false;
        }
        public FSMTrigger()
        {
            Init();
        }
        abstract public void Init();
    }
}
