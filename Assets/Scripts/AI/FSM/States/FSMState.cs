using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace AI.FSM
{
    abstract public class FSMState
    {
        //字段

        //状态编号
        public FSMStateID stateid;
        //条件列表
        private List<FSMTrigger> triggers = new List<FSMTrigger>();
        //转换映射表
        private Dictionary<FSMTriggersID, FSMStateID> map = new Dictionary<FSMTriggersID, FSMStateID>();
        //方法

        public FSMState()
        {
            Init();
            
        }

        //初始化
        abstract public void Init();
        //添加条件
        public void AddTrigger(FSMTriggersID triggersID, FSMStateID stateID)
        {
            if (map.ContainsKey(triggersID))
            {
                map[triggersID] = stateID;
            }
            else
            {
                map.Add(triggersID, stateID);
                AddTriggerObj(triggersID);
            }
        }
        private void AddTriggerObj(FSMTriggersID triggersID)
        {

            Type type = Type.GetType("AI.FSM." + triggersID + "Trigger");
            if (type != null)
            {
                var triggerobj = Activator.CreateInstance(type) as FSMTrigger;
                triggers.Add(triggerobj);
            }

        }
        //删除条件
        public void RemoveTrigger(FSMTriggersID triggersID)
        {
            if (map.ContainsKey(triggersID))
            {
                map.Remove(triggersID);
                RemoveTriggerObj(triggersID);
            }
        }
        private void RemoveTriggerObj(FSMTriggersID triggersID)
        {
            triggers.RemoveAll(t => t.triggerid == triggersID);
        }
        //查找映射
        public FSMStateID GetOutputState(FSMTriggersID triggersID)
        {
            if(map.ContainsKey(triggersID))
            {
                return map[triggersID];
            }
            return FSMStateID.none;
        }
        //状态行为--有很多的状态
        virtual public void Action(BaseFSM fSM)
        {

        }
        //条件检测--
        virtual public void Reason(BaseFSM fSM)
        {
            for (int i = 0; i < triggers.Count; i++)
            {
                if(triggers[i].HandleTrigger(fSM))
                {
                    fSM.ChangActiveState(triggers[i].triggerid);
                }
            }
        }
        //离开状态
        virtual public void ExitState(BaseFSM fSM)
        {

        }
        //进入状态
        virtual public void EnterState(BaseFSM fSM)
        {

        }
    }
}
