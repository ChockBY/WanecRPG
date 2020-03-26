using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using ARPGDemo.Backpack;
namespace ARPGDemo.Character
{
    /// <summary>
    /// 主角状态
    /// </summary>
    public class PlayerStatus : CharacterStatus
    {
        public void ShowMoney()
        {
            text.text = Money.ToString();
        }

        /// <summary>
        /// 金钱
        /// </summary>
        public int Money;
        //更新装备转化的伤害
        public void UpdateWeaponValue()
        {
            if (wp == null) return;
            if (wp.transform.childCount != 0)
            {

                if (wp.transform.GetChild(0).GetComponent<ItemUI>())
                {
                    this.Damage = wp.transform.GetChild(0).GetComponent<ItemUI>().Damage;
                }

            }
            else
            {
                Damage = 10;
            }
        }
        //更新装备转化的防御
        public void UpdateArmorValue()
        {
            if (ar == null) return;
            if (ar.transform.childCount != 0)
            {
                if (ar.transform.GetChild(0).GetComponent<ItemUI>())
                {
                    this.Defence = ar.transform.GetChild(0).GetComponent<ItemUI>().Defence;
                }

            }
            else
            {
                Defence = 0;
            }
        }

        private void OnEnable()
        {
            InvokeRepeating("UpdateWeaponValue", 0.3f, 0.2f);
            InvokeRepeating("UpdateArmorValue", 0.3f, 0.2f);
            InvokeRepeating("ShowMoney", 0.2f, 0.2f);
        }
        public GameObject DeadPlane;
        public override void Dead()
        {
            DeadPlane.SetActive(true);
        }
        public override void OnDamage(int damageVal)
        {
            base.OnDamage(damageVal);
        }
    }
}