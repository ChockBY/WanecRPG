using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Skill;
using UnityEngine.UI;
namespace ARPGDemo.Character
{

    public class SkillButton : MonoBehaviour
    {

        protected CharacterSkillSystem skillSystem;
        protected CharacterSkillManager skillManager;
        protected CharacterStatus status;
        private SkillData skillData;
        private float timer = -1f;
        private bool isStart;
        public Image Image;
        public int id;
        private void Start()
        {
            skillSystem = GameObject.Find("Player").GetComponent<CharacterSkillSystem>();
            skillManager = GameObject.Find("Player").GetComponent<CharacterSkillManager>();
            status = GameObject.Find("Player").GetComponent<CharacterStatus>();
            Init();
        }
        virtual public void Init()
        {
            skillData = skillManager.skills[id];
            if (Image != null)
                Image.fillAmount = 0;
        }
        public void UseSkill()
        {
            if (status.SP >= skillData.costSP)
            {
                skillSystem.AttackUseSkill(skillData.skillID, skillData.isdoublehit);
                isStart = true;
                this.GetComponent<Button>().enabled = false;
            }
        }
        public void Update()
        {
            UpdateImage();
        }
        public void UpdateImage()
        {
            if (Image == null)
            {
                isStart = true;
                this.GetComponent<Button>().enabled = true;
            }
            else if (isStart)
            {
                timer += Time.deltaTime;
                Image.fillAmount = (skillData.coolTime - timer) / skillData.coolTime;
                if (timer >= skillData.coolTime)
                {
                    Image.fillAmount = 0;
                    timer = -1f;
                    isStart = false;
                    this.GetComponent<Button>().enabled = true;
                }
            }
        }
    }
}
