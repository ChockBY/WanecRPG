using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.Perception
{
   public class SightSensor : AbstractSensor
    {
        //视距
        public float sightDistance;
        //启用角度检测
        public bool enableAngle;
        //启用遮挡检测
        public bool enableRay;
        //视觉
        public float sightAngle;

        //发射点
        public Transform sendpos;
        public override void Init()
        {
            if (sendpos == null) sendpos = transform;
        }
        
        public override void OnDestroy()
        {

        }
        protected override bool TextTrigger(AbstractTrigger Trigger)
        {
            if (Trigger.triggerType != TriggerType.Sight) return false;
            var temTrigger = Trigger as SightTriggercs;
            //距离
            var dir = temTrigger.recievePos.position - sendpos.position;
            var result = dir.magnitude < sightDistance;

            //角度
            if(enableAngle)
            {
                bool b1 = sightAngle/2 > Vector3.Angle(transform.forward, dir);
                result = b1 && result;
            }
            RaycastHit hit;
            //遮挡检测
            if(enableRay)
            {
                bool b1 = Physics.Raycast(sendpos.position, dir, out hit, sightDistance)
                          && hit.collider.gameObject == temTrigger.gameObject;
                result = b1 && result;

            }
            return result;
        }
    }
}
