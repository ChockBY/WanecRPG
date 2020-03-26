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
        //�ֶ� 6
        private CharacterAnimation chAnim;
        private FSMState currentState;
        public FSMStateID currentStateId;

        private FSMState defaultState;
        public FSMStateID defaultStateId;
        //���е�״̬����=״̬����=״̬��
        private List<FSMState> states = new List<FSMState>();
        //���� 1
        public void ChangActiveState(FSMTriggersID triggerid)
        {
            //1 ���ݵ�ǰ���� ȷ�� ��һ��״̬��˭������������ ������
            var nextStateId = currentState.GetOutputState(triggerid);
            //�����None����
            if (nextStateId == FSMStateID.none)
                return;
            //�������None�������ж��ǲ���Ĭ��
            FSMState nextState = null;//״̬����
            if (nextStateId == FSMStateID.Default)
            { nextState = defaultState; }
            else
            { nextState = states.Find(s => s.stateid == nextStateId); }
            //2 �˳���ǰ״̬
            currentState.ExitState(this); //���쵼 ����
            //*****���µ�ǰ״̬�����״̬���
            currentState = nextState;//���µ�ǰ״̬����=��� //�´��쵼������
            currentStateId = currentState.stateid;//
            //3 ������һ��״̬
            currentState.EnterState(this);
        }
        #endregion

        #region 2.0
        //�ֶ�
        public AnimationParms animParams;//?
        public CharacterStatus chState;
        //����
        public void PlayAnimation(string animPara)
        {
            chAnim.PlayAnimation(animPara);
        }
        private void Awake()
        {
            ConfigFSM();
        }
        //����״̬����
        //����1 Ӳ���� 
        //����2 ʹ��AI�����ļ� ȷ�� ���� ״̬��ӳ���ϵ
        private void ConfigFSM()
        {

            //1 ����״̬����
            IdleState idle = new IdleState();
            //2 �������ӳ��
            idle.AddTrigger(FSMTriggersID.NoHealth, FSMStateID.Dead);
            idle.AddTrigger(FSMTriggersID.SawPlayer, FSMStateID.Pursuit);
            //3 ����״̬����=״̬��
            states.Add(idle);

            //����ͬ����������� ״̬ӳ��

            //1 ����״̬����
            DeadState dead = new DeadState();
            //2 �������ӳ��

            //3 ����״̬����=״̬��
            states.Add(dead);

            //1 ����״̬����
            //2 �������ӳ��
            //3 ����״̬����=״̬��

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
        /// ָ��Ĭ��״̬
        /// </summary>
        private void InitDefaultState()
        {
            //�������Դ���ָ���� Ĭ��״̬��� Ϊ���������ֶθ�ֵ=��ʼ��
            defaultState = states.Find(s => s.stateid == defaultStateId);
            currentState = defaultState;
            currentStateId = defaultStateId;
        }
        private void OnEnable()
        {
            InitDefaultState();//ִ�е�ʱ�� ִ��Ƶ��
            //��ʱ���� 
            //InvokeRepeating("ResetTarget", 0, 0.2f);//3.0-1
        }
        /// <summary>
        /// ��ʼ�� �ֶ�
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



        //ʵʱ����״̬:ʵʱ��������ı仯������һ��-��Ϊ�ͱ�
        private void Update()
        {
            currentState.Reason(this);
            currentState.Action(this);
        }
        #endregion

        #region 3.0 ARPGDemo3.2 ��� ������������Ŀ�꡿��׷�� ��Ҫ���ֶκ͹���
        //��ע��Ŀ��tag
        public string[] targetTags = { "Player" };//
        //��ע��Ŀ������
        public Transform targetObject = null;// string>Object
        //�Ӿ�
        public float sightDistance = 10;//ע�� ����ʱ
        //�ܡ�׷�����ٶ�
        public float moveSpeed = 5;//
        //Ѱ·������ ·��  Ѱ·�������Ѱ· 
        private NavMeshAgent navAgent;

        //����
        /// <summary>
        /// 1 ����Ŀ��
        /// </summary>
        private void ResetTarget()
        {
            //1 ��tag��� ͨ��tag�� ���ܸߣ�  
            List<GameObject> listTargets = new List<GameObject>();
            for (int i = 0; i < targetTags.Length; i++)
            {
                var targets = GameObject.FindGameObjectsWithTag(targetTags[i]);

                if (targets != null && targets.Length > 0)
                { listTargets.AddRange(targets); }
            }
            if (listTargets.Count == 0) return;
            //2  ���ˣ��ȽϾ��롾ָ���뾶�� ���е�����  ͬʱ ���ŵ� HP>0
            var enemys = listTargets.FindAll(go =>
                (Vector3.Distance(go.transform.position,
                this.transform.position) < sightDistance)
                && (go.GetComponent<CharacterStatus>().HP > 0)
                );
            if (enemys == null || enemys.Count == 0) return;
            //3  ȡ�����һ�� 
            targetObject = ArrayHelper.Min(enemys.ToArray(),
                        e => Vector3.Distance(this.transform.position,
                            e.transform.position)).transform;
        }
        /// <summary>
        /// 2 ��Ŀ���ܣ�����Ŀ��
        /// </summary>
        /// <param name="pos">Ŀ�굱ǰ��λ��</param>
        /// <param name="speed">�ܵ��ٶ�</param>
        /// <param name="stopDistance">ֹͣ����</param>
        public void MoveToTarget(Vector3 pos, float speed, float stopDistance)
        {
            navAgent.speed = speed;
            navAgent.stoppingDistance = stopDistance;
            navAgent.SetDestination(pos);
        }
        /// <summary>
        /// 3 ֹͣ�ƶ�
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

        #region 5.0 Ѳ����Ҫ���ֶ�

        public Transform[] WayPoints;

        public float Patrolspeed;

        public bool IsCompletePatrol;

        public float patrolArrivalDistance;

        public PatrollingMode patrollingMode = PatrollingMode.Once;
        #endregion

        #region 6.0��Ӹ�Ӧϵͳ
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