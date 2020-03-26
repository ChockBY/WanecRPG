using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using ARPGDemo.Skill;

namespace ARPGDemo.Character
{
    /// <summary>
    /// 角色系统和技能系统外观类：角色技能外观类
    /// </summary>
    public class CharacterSkillSystem : MonoBehaviour
    {
        //字段
        private CharacterAnimation chAnim = null;
        private CharacterSkillManager skillMgr = null;

        private GameObject currentAttackTarget = null;
        private SkillData currentUseSkill = null;
        //方法
        private void Start()
        {
            chAnim = GetComponent<CharacterAnimation>();
            skillMgr = GetComponent<CharacterSkillManager>();
            GetComponentInChildren<AnimationEventBehaviour>().
                attackHandler += DeploySkill;

        }
        public void DeploySkill()
        {
            if (currentUseSkill != null)
                skillMgr.DeploySkill(currentUseSkill);
        }
        /// <summary>
        /// 使用指定编号的技能 进行攻击
        /// </summary>
        /// <param name="skillId">技能编号</param>
        /// <param name="isBatter">连续攻击：连攻</param>
        public void AttackUseSkill(int skillId, bool isBatter)
        {
            //如果是连续攻击，获取下一个技能编号
            if (isBatter && currentUseSkill != null&&currentUseSkill.RemainContinuousAttack>0f)
                skillId = currentUseSkill.nextBatterId;//
            currentUseSkill = skillMgr.PrepareSkill(skillId);
            if (currentUseSkill == null) return;
            chAnim.PlayAnimation(currentUseSkill.animationName);
            //3 找出受攻击的目标
            var selectedTarget = SelectTarget();
            if (selectedTarget == null) return;//!!!
            ShowSelectedFx(false);
            currentAttackTarget = selectedTarget;//将刚刚找出的目标作为当前目标
            ShowSelectedFx(true);
            transform.LookAt(selectedTarget.transform.position);
        }

        /// <summary>
        /// 选择当前的攻击目标
        /// </summary>
        /// <returns></returns>
        private GameObject SelectTarget()
        {
            List<GameObject> listTargets = new List<GameObject>();
            for (int i = 0; i < currentUseSkill.attackTargetTags.Length; i++)
            {
                var targets = GameObject.FindGameObjectsWithTag(currentUseSkill.attackTargetTags[i]);

                if (targets != null && targets.Length > 0)
                { listTargets.AddRange(targets); }
            }
            if (listTargets.Count == 0) return null;
            var enemys = listTargets.FindAll(go =>
                (Vector3.Distance(go.transform.position,
                this.transform.position) < currentUseSkill.attackDistance)
                && (go.GetComponent<CharacterStatus>().HP > 0)
                );
            if (enemys == null || enemys.Count == 0) return null;
            return ArrayHelper.Min(enemys.ToArray(),
                        e => Vector3.Distance(this.transform.position,
                            e.transform.position));
        }
        /// <summary>
        /// 显示选中的目标效果
        /// </summary>
        /// <param name="isShow">显示或隐藏 </param>
        private void ShowSelectedFx(bool isShow)
        {
            //定义特效
            Transform selectedFx = null;
            if (currentAttackTarget != null)
            {
                selectedFx = TransformHelper.
                    FindChild(currentAttackTarget.transform, "selected");
            }
            if (selectedFx != null)
            {
                selectedFx.GetComponent<Renderer>().enabled = isShow;
            }
        }
        public void UseRandomSkill()
        {
            //从技能列表随机抽取一个可用的技能
            var usableSkills = skillMgr.skills.FindAll(skill => skill.coolRemain == 0
                && skill.costSP <= skill.Owner.GetComponent<CharacterStatus>().SP);
            //2>随机抽取集合中的一个技能对象 
            if (usableSkills != null && usableSkills.Count > 0)
            {
                var index = UnityEngine.Random.Range(0, usableSkills.Count);
                var skillId = usableSkills[index].skillID;  
                AttackUseSkill(skillId, false);
            }
        }
    }
}
