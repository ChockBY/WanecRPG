using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace ARPGDemo.Backpack
{
    public class ItemUI : MonoBehaviour
    {
        public int Damage;
        public int Defence;
        public int id;
        public int Image;
        public Item Item;
        public void UpdateItem(Item item)
        {
            if (item == null) return;
            ChangeImage(item);
            if (item.Icon == "Weapon")
            {
                Weapon weapon = item as Weapon;
                Damage = weapon.charactorDamage;
                Item = weapon as Weapon;
            }
           else if (item.Icon == "Armor")
            {
                Armor armor = item as Armor;
                Defence = armor.charactorDefence;
                Item = armor as Armor;
            }
            id = Item.Id;
        }
        private void ChangeImage(Item item)
        {
            this.GetComponent<Image>().sprite = item.sprite;
        }
    }
}
