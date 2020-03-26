using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.Perception
{
   public abstract class AbstractTrigger : MonoBehaviour
    {
        //字段
        /// <summary>
        /// 是否删除，是否禁用
        /// </summary>
        public bool isRemove;
        /// <summary>
        /// 触发器类型
        /// </summary>
        public TriggerType triggerType;


        //方法

        /// <summary>
        /// 初始化
        /// </summary>
        abstract public void Init();
        /// <summary>
        /// 销毁方法 把当前触发器 从感应系统中移除
        /// </summary>
        abstract public void OnDestroy();

        private void Start()
        {
            Init();
            //把当前触发器放到 感应系统中
            SensorTriggerSystem.instance.AddTrigger(this);
        }

    }
}
