using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AI.Perception;
namespace ARPGDemo.Character
{
    /// <summary>
    /// 小怪状态
    /// </summary>
    public class MonsterStatus : CharacterStatus
    {
        /// <summary>
        /// 贡献经验值
        /// </summary>
        public int GiveExp;

        public override void Dead()
        {
            print("MonsterStatus Dead ");
            if (this.transform.name == "EnemyBoss")
            {
                Killhighking.iskill = true;
            }
            else
            {
                Attackbarbarians.KillCount += 1;
            }
            GameObjectPool.instance.CollectObject(this.gameObject,2f);
        }
        public override void OnDamage(int damageVal)
        {
            base.OnDamage(damageVal);
        }

    }
}
