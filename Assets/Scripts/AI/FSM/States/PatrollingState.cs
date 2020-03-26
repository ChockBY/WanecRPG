using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.FSM
{
    class PatrollingState : FSMState
    {
        private int CurrentWayPoint;
        public override void Init()
        {
            stateid = FSMStateID.Patrolling;
        }
        public override void Action(BaseFSM fSM)
        {
            //是否到达下一个路点
            if(Vector3.Distance(fSM.transform.position,fSM.WayPoints[CurrentWayPoint].position)<fSM.patrolArrivalDistance)
            {
                //是否到达最后一个路点
                if(CurrentWayPoint==fSM.WayPoints.Length-1)
                {
                    //确定巡逻模式
                    switch(fSM.patrollingMode)
                    {
                        case PatrollingMode.Once:
                            fSM.IsCompletePatrol = true;
                            break;
                        case PatrollingMode.PingPang:
                            Array.Reverse(fSM.WayPoints);
                            CurrentWayPoint += 1;
                            break;
                    }
                }
                CurrentWayPoint = (CurrentWayPoint+1) % fSM.WayPoints.Length;
            }
            fSM.MoveToTarget(fSM.WayPoints[CurrentWayPoint].position, fSM.Patrolspeed, fSM.patrolArrivalDistance);
            fSM.PlayAnimation(fSM.animParams.Walk);
        }
        public override void EnterState(BaseFSM fSM)
        {
            fSM.IsCompletePatrol = false;
        }
        public override void ExitState(BaseFSM fSM)
        {
            fSM.StopMove();
        }
    }
}
