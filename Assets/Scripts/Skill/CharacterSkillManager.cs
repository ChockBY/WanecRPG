using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using ARPGDemo.Character;
using System.Collections;

namespace ARPGDemo.Skill
{
    public class CharacterSkillManager : MonoBehaviour
    {
        //管理多个技能数据对象---容器 所有技能
        public List<SkillData> skills = new List<SkillData>();
        private void Start()
        {
            //为skills中的 skill的有些字段初始化！
            foreach (var skill in skills)
            {
                if (!(string.IsNullOrEmpty(skill.prefabName)) && skill.skillPrefab == null)
                {
                    skill.skillPrefab = LoadPrefab(skill.prefabName);
                }
                if (!(string.IsNullOrEmpty(skill.hitFxName)) && skill.hitFxPrefab == null)
                {
                    skill.hitFxPrefab = LoadPrefab(skill.hitFxName);
                }
                //为技能指定拥有者！！
                skill.Owner = this.gameObject;
            }
        }
        /// <summary>
        /// 动态加载 预制件资源 
        /// </summary>
        /// <param name="resName">预制件资源名称</param>
        private GameObject LoadPrefab(string resName)
        {
            //动态加载 预制件资源
            var prefabGo = ResourceManager.Load<GameObject>(resName);
            var tempGo = GameObjectPool.instance.CreateObject(
                resName, prefabGo,
                transform.position, transform.rotation
                );
            GameObjectPool.instance.CollectObject(tempGo);
            return prefabGo;
        }
        //2.准备技能
        public SkillData PrepareSkill(int id)
        {
            //1根据技能id找 技能容器中是否有这个技能
            var skill = skills.Find(s => s.skillID == id);
            //2如果找到，同时 技能已经冷却  而且 SP足够，返回
            if (skill != null)
            {
                if (skill.coolRemain == 0 && skill.costSP <=
                    skill.Owner.GetComponent<CharacterStatus>().SP)
                {
                    return skill;
                }
            }
            return null;
        }
        //3.施放技能  调用施放器的施放的方法即可
        public void DeploySkill(SkillData skillData)
        {
            //1 创建技能预制件对象  对象池创建
            var tempGo = GameObjectPool.instance.CreateObject(
               skillData.prefabName, skillData.skillPrefab,
               transform.position, transform.rotation
               );
            //2 为技能预制件对象 设置当前要使用 这个技能
            var deployer = tempGo.GetComponent<SkillDeployer>();
            //3 调用施放器的施放的方法
            deployer.skillData = skillData;
            deployer.DeploySkill();
            StartCoroutine(CoolTimeContinuousAttack(skillData));
            //4 冷却计时
            StartCoroutine(CoolTimeDown(skillData));
            //技能对象需要回收 留给施放器完成        
        }
        //4.技能冷却处理
        private IEnumerator CoolTimeDown(SkillData skillData)
        {
            skillData.coolRemain = skillData.coolTime;
            //冷却处理？ 循环 直到coolRemain=0 
            while (skillData.coolRemain > 0)
            {
                //每隔1秒减 1
                yield return new WaitForSeconds(1);
                skillData.coolRemain -= 1;
            }
            skillData.coolRemain = 0;//为了保险起见  计算机基础！ 
        }
        //5.获取技能冷却剩余时间
        public float GetSkillCoolRemain(int id)
        {
            return skills.Find(s => s.skillID == id).coolRemain;
        }

        //6.连续攻击间隔剩余时间
        public IEnumerator CoolTimeContinuousAttack(SkillData skillData)
        {
            skillData.RemainContinuousAttack = skillData.ContinuousAttack;
            while (skillData.RemainContinuousAttack > 0)
            {
                yield return new WaitForSeconds(1f);
                skillData.RemainContinuousAttack -= 1;
            }
            skillData.RemainContinuousAttack = 0f;
        }
    }
}
