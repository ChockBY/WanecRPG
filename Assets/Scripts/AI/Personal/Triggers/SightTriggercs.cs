using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.Perception
{
   public class SightTriggercs : AbstractTrigger
    {
        public Transform recievePos;
        public override void Init()
        {
            if (recievePos == null)
                recievePos = transform;
            triggerType = TriggerType.Sight;
        }

        public override void OnDestroy()
        {

        }
    }
}
