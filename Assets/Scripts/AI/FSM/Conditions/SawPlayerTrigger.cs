using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ARPGDemo.Character;
using AI.Perception;
namespace AI.FSM
{
    class SawPlayerTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerid = FSMTriggersID.SawPlayer;
        }
        public override bool HandleTrigger(BaseFSM baseFSM)
        {


            if (baseFSM.targetObject != null)
            {
                bool b;
                return b = Vector3.Distance(baseFSM.transform.position, baseFSM.targetObject.position) < baseFSM.sightDistance
                           && baseFSM.targetObject.GetComponent<CharacterStatus>().HP > 0;
            }

            return false;
        }
    }
}
