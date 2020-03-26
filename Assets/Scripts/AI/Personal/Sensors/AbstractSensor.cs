using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace AI.Perception
{
   public abstract class AbstractSensor : MonoBehaviour
    {
        /// <summary>
        /// 是否删除，是否禁用
        /// </summary>
        public bool isRemove;
        /// <summary>
        /// 没有感知事件: F1>F2[没有看到 做什么]
        /// </summary>
        public event Action OnNonPerception;
        /// <summary>
        /// 感知事件:F1>F2[看到 作什么]
        /// </summary>
        public event Action<List<AbstractTrigger>> OnPerception;



        /// <summary>
        /// 销毁方法    把当前感应器 从感应系统中移除
        /// </summary>
        abstract public void OnDestroy();

        public void OnTestTrigger(List<AbstractTrigger> triggers)
        {
            triggers = triggers.FindAll(trigger => trigger.enabled
            &&trigger.gameObject!=this.gameObject
            &&TextTrigger(trigger));

            if(triggers.Count>0)
            {
                if (OnPerception != null)
                    OnPerception(triggers);
            }
            else
            {
                if (OnNonPerception != null)
                    OnNonPerception();
            }
        }
        abstract protected bool TextTrigger(AbstractTrigger Trigger);

        abstract public void Init();
        public void Start()
        {
            Init();
            //把当前感应器放到 感应系统中
            SensorTriggerSystem.instance.AddSensor(this);
        }     
        
    }
}
