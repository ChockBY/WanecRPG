using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ARPGDemo.Backpack;
namespace ARPGDemo.Character
{
    public class CharacterManager : MonoBehaviour
    {
        private Text NameText;
        private Image HP;
        private Image SP;
        private PlayerStatus status;
        private void Awake()
        {
            NameText = GameObject.Find("CharacterCanvas").transform.GetChild(3).GetComponent<Text>();
            HP = GameObject.Find("CharacterCanvas").transform.GetChild(1).GetComponent<Image>();
            SP = GameObject.Find("CharacterCanvas").transform.GetChild(0).GetComponent<Image>();
            this.transform.position = Charactertemplate.CurrentPos;
            status = this.GetComponent<PlayerStatus>();
            GameObject go = Instantiate(Resources.Load<GameObject>(Charactertemplate.CharacterMode), this.transform.position, Quaternion.identity);
            //operationDB.instance.GetName();
            go.transform.parent = this.transform;
            InitCharacter();
            //operationDB.instance.GetTaskID();
            TaskManager.instance.TaskID = Charactertemplate.TaskID;
        }
        private void Start()
        {
           operationDB.instance.GetBagInfo();
            //operationDB.instance.GetStateBag();
            NameText.text = Charactertemplate.Name;
            InitStateBag();
        }
        private void Update()
        {
            UpdateCharacterInfo();
        }
        private void InitCharacter()
        {
            status.HP = Charactertemplate.HP;
            status.MaxHP = Charactertemplate.MaxHP;
            status.attackDistance = Charactertemplate.attackDistance;
            status.attackSpeed = Charactertemplate.attackSpeed;
            status.SP = Charactertemplate.SP;
            status.MaxSP = Charactertemplate.MaxSP;
            status.Damage = Charactertemplate.Damage;
            status.Defence = Charactertemplate.Defence;
            status.Money = Charactertemplate.Money;
            HP.fillAmount = (float)status.HP / 100;
            SP.fillAmount = (float)status.SP / 100;
        }
        private void InitStateBag()
        {
            for (int i = 0; i < 3; i++)
            {
                if (SaveCharacter.StateID[i] != -1)
                    KnapsackManager.Instance.StoreItem(SaveCharacter.StateID[i], "StateBag");
            }
        }
        private void UpdateCharacterInfo()
        {
            Charactertemplate.HP = status.HP; ;
            Charactertemplate.MaxHP = status.MaxHP;
            Charactertemplate.SP = status.SP;
            Charactertemplate.MaxSP = status.MaxSP;
            Charactertemplate.Money = status.Money;
            HP.fillAmount = (float)status.HP / 100;
            SP.fillAmount = (float)status.SP / 100;
        }

    }
}
