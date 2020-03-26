using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.Perception
{
    class SensorTriggerSystem:MonoSingleton<SensorTriggerSystem>
    {
        public int checkInterval;//检查时间间隔
        private List<AbstractSensor>listSensor = new List<AbstractSensor>();//感应器列表
        private List<AbstractTrigger> listTrigger = new List<AbstractTrigger>();//触发器列表
        /// <summary>
        /// 添加触发器
        /// </summary>
        /// <param name="sensor"></param>
        public void AddSensor(AbstractSensor sensor)
        {
            listSensor.Add(sensor);
        }
        /// <summary>
        /// 添加感应器
        /// </summary>
        /// <param name="trigger"></param>
        public void AddTrigger(AbstractTrigger trigger)
        {
            listTrigger.Add(trigger);
        }
        /// <summary>
        /// 检查触发条件 每个感应器 检查 对应所有触发器
        /// </summary>
        private void CheckTrigger()
        {
            for (int i = 0; i < listSensor.Count; i++)
            {
                if(listSensor[i].enabled)
                {
                    listSensor[i].OnTestTrigger(listTrigger);
                }
            }
        }

        private void OnDisable()
        {
            CancelInvoke("Check");
        }
        private void OnEnable()
        {
            InvokeRepeating("Check",0,checkInterval);
        }
        /// <summary>
        /// 更新系统
        /// </summary>
        private void UpdateSystem()
        {
            listSensor.RemoveAll(senor => senor.isRemove);
            listTrigger.RemoveAll(trigger => trigger.isRemove);
        }
        private void Check()
        {
            InvokeRepeating("UpdateSystem", 0, checkInterval);
            InvokeRepeating("CheckTrigger", 0, checkInterval);
        }
    }
}
