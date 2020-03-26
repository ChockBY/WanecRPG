using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ARPGDemo.Character;
namespace AI.FSM
{
    /// <summary>
    /// 打死目标
    /// </summary>
    class KilledPlayerTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerid = FSMTriggersID.KilledPlayer;
        }
        public override bool HandleTrigger(BaseFSM baseFSM)
        {
            if (baseFSM.targetObject != null)
            {
                bool b;
                return b = 
                     baseFSM.targetObject.GetComponent<CharacterStatus>().HP <=0;
            }
            return true;
        }
    }
}
