using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine.UI;
using ARPGDemo.Character;
namespace ARPGDemo.Backpack
{
    public class KnapsackManager : MonoBehaviour
    {
        private static KnapsackManager _instance;
        public static KnapsackManager Instance { get { return _instance; } }
        [HideInInspector]
        public GridPanelUI GridPanelUI;
        [HideInInspector]
        public TooltipUI TooltipUI;
        [HideInInspector]
        public DragItemUI DragItemUI;

        private bool isDrag = false;
        private bool isShow = false;

        public  Dictionary<int, Item> ItemList=new Dictionary<int, Item>();
        //public GameObject IsDropCanvas;
        public  Dictionary<int, Item> ShopList;
        private void OnEnable()
        {
            _instance = this;
            GridPanelUI = this.transform.GetChild(0).GetChild(0).GetComponent<GridPanelUI>();
            DragItemUI = this.transform.GetChild(2).transform.GetComponent<DragItemUI>();
            TooltipUI= this.transform.GetChild(1).transform.GetComponent<TooltipUI>();
        }
        void Awake()
        {

            //单例
            _instance = this;
            //数据
            Load();
            //事件
            //GridUI.OnEnter += new Action<Transform>(GridUI_OnEnter);
            //GridUI.OnExit += new Action(GridUI_OnExit);
            GridUI.OnPressDown += GirdUI_OnPressDown;
            GridUI.OnHight += Grid_OnHight;
            GridUI.OnLeftBeginDrag += GridUI_OnLeftBeginDrag;
            GridUI.OnLeftEndDrag += GridUI_OnLeftEndDrag;

        }
        void Update()
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.FindGameObjectWithTag("KnapsackUI").transform as RectTransform, Input.mousePosition, null, out position);

            if (isDrag)
            {
                DragItemUI.Show();
                DragItemUI.SetLocalPosition(position);
            }
            else if (isShow)
            {
                TooltipUI.Show();
            }
        }
        public void StoreItem(int itemId, string bagName)
        {
            Transform emptyGrid = null;
            if (!ItemList.ContainsKey(itemId))
                return;
            if (bagName == "MyBag")
            {
                emptyGrid = GridPanelUI.GetEmptyGrid();
            }
            else if(bagName=="StateBag")
            {
                emptyGrid = GridPanelUI.GetStateEmptyGrid();
            }
           else if (emptyGrid == null)
            {
                Debug.LogWarning("背包已满!!");
                return;
            }

            Item temp = ItemList[itemId];
            this.CreatNewItem(temp, emptyGrid);
        }

        private void Load()
        {
            ItemList = new Dictionary<int, Item>();
            Weapon w1 = new Weapon(0, "木剑", "具有杀伤力的剑！", 100, 20, "Weapon", 5, "木剑");
            Weapon w2 = new Weapon(1, "宝剑", "剑圣才拥有此剑，具有超强的伤害", 500, 250, "Weapon", 15, "宝剑");

            //Consumable c1 = new Consumable(4, "红瓶", "加血", 25, 11, "", 20, 0);
            //Consumable c2 = new Consumable(5, "蓝瓶", "加蓝", 39, 19, "", 0, 20);

            Armor a1 = new Armor(2, "战士服", "勇敢的战士所拥有的防御甲！", 100, 20, "Armor", 5, "战士服");
            Armor a2 = new Armor(3, "骑士服", "只有圣神的骑士才能穿起它", 500, 250, "Armor", 10, "骑士服");

            ItemList.Add(w1.Id, w1);
            ItemList.Add(w2.Id, w2);
            //ItemList.Add(c1.Id, c1);
            //ItemList.Add(c2.Id, c2);
            ItemList.Add(a1.Id, a1);
            ItemList.Add(a2.Id, a2);
            //ItemList.Add(a3.Id, a3);
            //ItemList.Add(a4.Id, a4);


        }

        #region 事件回调
        public void GirdUI_OnPressDown(Transform girdTransform)
        {
            Item item = ItemModel.GetItem(girdTransform.name);
            print(girdTransform.name);
            if (item == null)
                return;
            string text = GetTooltipText(item);
            if (TooltipUI != null)
                TooltipUI.UpdateTooltip(text);
            isShow = true;
        }
        public void Grid_OnHight()
        {
            isShow = false;
            if(TooltipUI!=null)
            TooltipUI.Hide();
        }

        public void GridUI_OnLeftBeginDrag(Transform girdTransform)
        {
            if (girdTransform.childCount == 0)
                return;
            else
            {
                Item item = ItemModel.GetItem(girdTransform.name);
                if(DragItemUI!=null)
                {
                DragItemUI.UpdateItem(item);
                }

                Destroy(girdTransform.GetChild(0).gameObject);
                isDrag = true;
            }
        }
        public bool isDrop = false;
        [HideInInspector]
        public Transform prev;
        [HideInInspector]
        public Transform enter;
        public GameObject IsDrob;
        public void GridUI_OnLeftEndDrag(Transform prevTransform, Transform enterTransform)
        {
            isDrag = false;
            if (DragItemUI != null)
                DragItemUI.Hide();
            else
                return;
            prev = prevTransform;
            if (enterTransform == null)//扔东西
            {
                if (ItemModel.GetItem(prevTransform.name) != null)
                {
                    IsDrob= Instantiate(Resources.Load<GameObject>("BagPrefabs/IsDropCanvas"));
                }
                    //IsDropCanvas.SetActive(true);
            }
            else if (enterTransform.tag == "Grid")//拖到另一个或者当前格子里
            {
                if (enterTransform.childCount == 0)//直接扔进去
                {
                    Item item = ItemModel.GetItem(prevTransform.name);
                    this.CreatNewItem(item, enterTransform);
                    ItemModel.DeleteItem(prevTransform.name);
                }
                else //交换
                {
                    print("交换");
                    //删除原来的物品
                    Destroy(enterTransform.GetChild(0).gameObject);
                    //获取数据
                    Item prevGirdItem = ItemModel.GetItem(prevTransform.name);
                    Item enterGirdItem = ItemModel.GetItem(enterTransform.name);
                    //交换的两个物体
                    this.CreatNewItem(prevGirdItem, enterTransform);
                    this.CreatNewItem(enterGirdItem, prevTransform);
                }

            }
            else if (enterTransform.tag == "Weapon")//拖到另一个或者当前格子里
            {
                if (enterTransform.childCount == 0)//直接扔进去
                {
                    Item item = ItemModel.GetItem(prevTransform.name);
                    if (item.Icon == "Weapon")
                    {
                        this.CreatNewItem(item, enterTransform);
                        ItemModel.DeleteItem(prevTransform.name);
                    }
                    else
                    {
                        print("没有交换");
                        item = ItemModel.GetItem(prevTransform.name);
                        this.CreatNewItem(item, prevTransform);
                    }
                }
                else //交换
                {
                    print("交换");
                    //删除原来的物品
                    Destroy(enterTransform.GetChild(0).gameObject);
                    //获取数据
                    Item prevGirdItem = ItemModel.GetItem(prevTransform.name);
                    Item enterGirdItem = ItemModel.GetItem(enterTransform.name);
                    //交换的两个物体
                    this.CreatNewItem(prevGirdItem, enterTransform);
                    this.CreatNewItem(enterGirdItem, prevTransform);
                }
            }
            else if (enterTransform.tag == "Shoe")//拖到另一个或者当前格子里
            {
                if (enterTransform.childCount == 0)//直接扔进去
                {
                    Item item = ItemModel.GetItem(prevTransform.name);
                    if (item.Icon == "Shoe")
                    {
                        this.CreatNewItem(item, enterTransform);
                        ItemModel.DeleteItem(prevTransform.name);
                    }
                    else
                    {
                        print("没有交换");
                        item = ItemModel.GetItem(prevTransform.name);
                        this.CreatNewItem(item, prevTransform);
                    }
                }
                else //交换
                {
                    print("交换");
                    //删除原来的物品
                    Destroy(enterTransform.GetChild(0).gameObject);
                    //获取数据
                    Item prevGirdItem = ItemModel.GetItem(prevTransform.name);
                    Item enterGirdItem = ItemModel.GetItem(enterTransform.name);
                    //交换的两个物体
                    this.CreatNewItem(prevGirdItem, enterTransform);
                    this.CreatNewItem(enterGirdItem, prevTransform);
                }

            }
            else if (enterTransform.tag == "Armor")//拖到另一个或者当前格子里
            {
                if (enterTransform.childCount == 0)//直接扔进去
                {
                    Item item = ItemModel.GetItem(prevTransform.name);
                    if (item.Icon == "Armor")
                    {
                        this.CreatNewItem(item, enterTransform);
                        ItemModel.DeleteItem(prevTransform.name);
                    }
                    else
                    {
                        print("没有交换");
                        item = ItemModel.GetItem(prevTransform.name);
                        this.CreatNewItem(item, prevTransform);
                    }
                }
                else //交换
                {
                    print("交换");
                    //删除原来的物品
                    Destroy(enterTransform.GetChild(0).gameObject);
                    //获取数据
                    Item prevGirdItem = ItemModel.GetItem(prevTransform.name);
                    Item enterGirdItem = ItemModel.GetItem(enterTransform.name);
                    //交换的两个物体
                    this.CreatNewItem(prevGirdItem, enterTransform);
                    this.CreatNewItem(enterGirdItem, prevTransform);
                }
            }
            else//拖到UI的其他地方,让他还原
            {
                print("没有交换");
                Item item = ItemModel.GetItem(prevTransform.name);
                this.CreatNewItem(item, prevTransform);
            }
        }

        #endregion

        public string GetTooltipText(Item item)
        {
            if (item == null)
                return "";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<color=red>{0}</color>\n\n", item.Name);
            switch (item.ItemType)
            {
                case "Weapon":
                    Weapon weapon = item as Weapon;
                    sb.AppendFormat("攻击:{0}\n\n", weapon.Damage);
                    break;
                case "Armor":
                    Armor armor = item as Armor;
                    sb.AppendFormat("防御:{0}\n\n", armor.defend);
                    break;
                default:
                    break;
            }
            sb.AppendFormat("<size=12><color=white>购买价格：{0}\n出售价格：{1}</color></size>\n\n<color=yellow><size=12>描述：{2}</size></color>", item.BuyPrice, item.SellPrice, item.Description);
            return sb.ToString();
        }

        public void CreatNewItem(Item item, Transform parent)
        {
            if (item == null) return;
            GameObject itemPrefab = ResourceManager.Load<GameObject>("Item");
            itemPrefab.GetComponent<ItemUI>().UpdateItem(item);
            itemPrefab.GetComponent<Image>().sprite = item.sprite;
            GameObject itemGo = GameObject.Instantiate(itemPrefab);
            itemGo.transform.SetParent(parent);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.transform.localScale = Vector3.one;
            //存储数据            
            ItemModel.StoreItem(parent.name, item);

        }
    }
}
