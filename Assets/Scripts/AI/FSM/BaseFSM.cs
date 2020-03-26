using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ARPGDemo.Character;//
using UnityEngine;
using UnityEngine.AI;
using AI.Perception;
namespace AI.FSM
{
    public class BaseFSM : MonoBehaviour
    {
        #region 1.0
        //字段 6
        private CharacterAnimation chAnim;
        private FSMState currentState;
        public FSMStateID currentStateId;

        private FSMState defaultState;
        public FSMStateID defaultStateId;
        //所有的状态对象=状态集合=状态库
        private List<FSMState> states = new List<FSMState>();
        //方法 1
        public void ChangActiveState(FSMTriggersID triggerid)
        {
            //1 根据当前条件 确定 下一个状态是谁？待机》条件 》死亡
            var nextStateId = currentState.GetOutputState(triggerid);
            //如果是None返回
            if (nextStateId == FSMStateID.none)
                return;
            //如果不是None，继续判断是不是默认
            FSMState nextState = null;//状态对象
            if (nextStateId == FSMStateID.Default)
            { nextState = defaultState; }
            else
            { nextState = states.Find(s => s.stateid == nextStateId); }
            //2 退出当前状态
            currentState.ExitState(this); //大领导 张三
            //*****更新当前状态对象和状态编号
            currentState = nextState;//更新当前状态对象=编号 //新大领导：李四
            currentStateId = currentState.stateid;//
            //3 进入下一个状态
            currentState.EnterState(this);
        }
        #endregion

        #region 2.0
        //字段
        public AnimationParms animParams;//?
        public CharacterStatus chState;
        //方法
        public void PlayAnimation(string animPara)
        {
            chAnim.PlayAnimation(animPara);
        }
        private void Awake()
        {
            ConfigFSM();
        }
        //配置状态机：
        //方法1 硬编码 
        //方法2 使用AI配置文件 确定 条件 状态的映射关系
        private void ConfigFSM()
        {

            //1 创建状态对象
            IdleState idle = new IdleState();
            //2 添加条件映射
            idle.AddTrigger(FSMTriggersID.NoHealth, FSMStateID.Dead);
            idle.AddTrigger(FSMTriggersID.SawPlayer, FSMStateID.Pursuit);
            //3 放入状态集合=状态库
            states.Add(idle);

            //如下同理。添加其它的 状态映射

            //1 创建状态对象
            DeadState dead = new DeadState();
            //2 添加条件映射

            //3 放入状态集合=状态库
            states.Add(dead);

            //1 创建状态对象
            //2 添加条件映射
            //3 放入状态集合=状态库

            PursuitState pursuit = new PursuitState();
            pursuit.AddTrigger(FSMTriggersID.NoHealth, FSMStateID.Dead);
            pursuit.AddTrigger(FSMTriggersID.ReachPlayer, FSMStateID.Attacking);
            pursuit.AddTrigger(FSMTriggersID.LosePlayer, FSMStateID.Default);
            states.Add(pursuit);

            AttackingState attackingState = new AttackingState();
            attackingState.AddTrigger(FSMTriggersID.NoHealth, FSMStateID.Dead);
            attackingState.AddTrigger(FSMTriggersID.KilledPlayer, FSMStateID.Default);
            attackingState.AddTrigger(FSMTriggersID.WithOutAttackRange, FSMStateID.Pursuit);
            states.Add(attackingState);

            PatrollingState patrollingState = new PatrollingState();
            patrollingState.AddTrigger(FSMTriggersID.NoHealth, FSMStateID.Dead);
            patrollingState.AddTrigger(FSMTriggersID.CompletePatrol, FSMStateID.Idle);
            patrollingState.AddTrigger(FSMTriggersID.SawPlayer, FSMStateID.Pursuit);
            states.Add(patrollingState);
        }
        /// <summary>
        /// 指定默认状态
        /// </summary>
        private void InitDefaultState()
        {
            //根据属性窗口指定的 默认状态编号 为其它三个字段赋值=初始化
            defaultState = states.Find(s => s.stateid == defaultStateId);
            currentState = defaultState;
            currentStateId = defaultStateId;
        }
        private void OnEnable()
        {
            InitDefaultState();//执行的时机 执行频率
            //临时调用 
            //InvokeRepeating("ResetTarget", 0, 0.2f);//3.0-1
        }
        /// <summary>
        /// 初始化 字段
        /// </summary>
        public void Start()
        {
            chState = GetComponent<CharacterStatus>();
            chAnim = GetComponent<CharacterAnimation>();
            navAgent = GetComponent<NavMeshAgent>();//3.0-2
            chSkillSys = GetComponent<CharacterSkillSystem>();//4.0

            sightSensor = GetComponent<SightSensor>();
            sightSensor.OnPerception += SightSensor_OnPerception;
            sightSensor.OnNonPerception += SightSensor_OnNonPerception;
        }



        //实时更新状态:实时检查条件的变化，条件一变-行为就变
        private void Update()
        {
            currentState.Reason(this);
            currentState.Action(this);
        }
        #endregion

        #region 3.0 ARPGDemo3.2 添加 待机》【发现目标】》追逐 需要的字段和功能
        //关注的目标tag
        public string[] targetTags = { "Player" };//
        //关注的目标物体
        public Transform targetObject = null;// string>Object
        //视距
        public float sightDistance = 10;//注意 测试时
        //跑【追】的速度
        public float moveSpeed = 5;//
        //寻路：方法 路点  寻路组件网格寻路 
        private NavMeshAgent navAgent;

        //方法
        /// <summary>
        /// 1 重置目标
        /// </summary>
        private void ResetTarget()
        {
            //1 有tag标记 通过tag找 性能高！  
            List<GameObject> listTargets = new List<GameObject>();
            for (int i = 0; i < targetTags.Length; i++)
            {
                var targets = GameObject.FindGameObjectsWithTag(targetTags[i]);

                if (targets != null && targets.Length > 0)
                { listTargets.AddRange(targets); }
            }
            if (listTargets.Count == 0) return;
            //2  过滤：比较距离【指定半径】 所有的物体  同时 活着的 HP>0
            var enemys = listTargets.FindAll(go =>
                (Vector3.Distance(go.transform.position,
                this.transform.position) < sightDistance)
                && (go.GetComponent<CharacterStatus>().HP > 0)
                );
            if (enemys == null || enemys.Count == 0) return;
            //3  取最近的一个 
            targetObject = ArrayHelper.Min(enemys.ToArray(),
                        e => Vector3.Distance(this.transform.position,
                            e.transform.position)).transform;
        }
        /// <summary>
        /// 2 向目标跑，移向目标
        /// </summary>
        /// <param name="pos">目标当前的位置</param>
        /// <param name="speed">跑的速度</param>
        /// <param name="stopDistance">停止距离</param>
        public void MoveToTarget(Vector3 pos, float speed, float stopDistance)
        {
            navAgent.speed = speed;
            navAgent.stoppingDistance = stopDistance;
            navAgent.SetDestination(pos);
        }
        /// <summary>
        /// 3 停止移动
        /// </summary>
        public void StopMove()
        {
            navAgent.enabled = false;//
            navAgent.enabled = true;//
        }
        private void OnDisable()
        {
            if (currentState.stateid != FSMStateID.Dead)
            {
                currentState.ExitState(this);
                currentState = states.Find(s => s.stateid == FSMStateID.Idle);
                currentStateId = currentState.stateid;
                PlayAnimation(animParams.Idle);
            }
            else
            {
                var sensors = GetComponents<AbstractSensor>();
                foreach (var items in sensors)
                {
                    items.enabled = false;
                }
            }
        }
        #endregion

        #region 4.0
        private CharacterSkillSystem chSkillSys;
        public void AutoUseSkill()
        {
            chSkillSys.UseRandomSkill();
        }

        #endregion

        #region 5.0 巡逻需要的字段

        public Transform[] WayPoints;

        public float Patrolspeed;

        public bool IsCompletePatrol;

        public float patrolArrivalDistance;

        public PatrollingMode patrollingMode = PatrollingMode.Once;
        #endregion

        #region 6.0添加感应系统
        public SightSensor sightSensor;

        private void SightSensor_OnPerception(List<AbstractTrigger> listTriggers)
        {
            targetObject = null;
            var listObj = listTriggers.FindAll(t => Array.IndexOf(targetTags, t.tag) >= 0);
            if (listObj.Count > 0)
            {
                listObj = listObj.FindAll(go => go.GetComponent<CharacterStatus>().HP > 0);
                if (listObj.Count > 0)
                {
                    targetObject = ArrayHelper.Min(listObj.ToArray(), t => Vector3.Distance(t.transform.position, this.transform.position)).transform;
                    temporaryObj = targetObject;
                }
            }

        }
        public Transform temporaryObj;
        private void SightSensor_OnNonPerception()
        {
            targetObject = null;
            if (temporaryObj != null)
            {
                if (Vector3.Distance(temporaryObj.position, transform.position) < sightSensor.sightDistance)
                {
                    targetObject = temporaryObj;
                }
                else
                {
                    targetObject = null;
                    temporaryObj = null;
                }
                return;
            }

        }
        #endregion
    }
}