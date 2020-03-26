using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using ARPGDemo.Backpack;
public class operationDB : MonoSingleton<operationDB>
{
    private InputField InputField;

    /// <summary>    /// 数据库路径    /// </summary>
    private string appDBPath;
    /// <summary>    /// 数据库文件    /// </summary>
    private DbAccess db;
    /// <summary>
    /// 创建数据库
    /// </summary>
    public void CreateDataBase()
    {
#if UNITY_EDITOR|| UNITY_STANDALONE_WIN
        appDBPath = Application.streamingAssetsPath + "/Arpg.db";
#elif UNITY_ANDROID || UNITY_IPHONE
        appDBPath = Application.persistentDataPath + "/Arpg.db";
        if (!File.Exists(appDBPath))
            StartCoroutine(CopyDatabase());
#endif
        db = new DbAccess("URI=file:" + appDBPath);
    }
    private IEnumerator CopyDatabase()
    {
        WWW www = new WWW(Application.streamingAssetsPath + "/Arpg.db");
        yield return www;

        File.WriteAllBytes(appDBPath, www.bytes);
    }
    // <summary>//
    // / 获取角色名称///
    //  / </summary>//
    public void GetName()
    {
        CreateDataBase();
        SqliteDataReader name = db.Select("_Account", "AccountID", "1");
        name.Read();
        Charactertemplate.Name = name.GetValue(name.GetOrdinal("AccountName")).ToString();
        db.CloseSqlConnection();
    }
    /// <summary>
    /// 获取保存数据
    /// </summary>
    public void GetCharacter()
    {
        CreateDataBase();
        SqliteDataReader ChInfo = db.Select("_Character", "1", "CharacterID");
        ChInfo.Read();
        int i = 0;
        Charactertemplate.Name = ChInfo[i].ToString();
        Charactertemplate.CharacterID = int.Parse(ChInfo[++i].ToString());
        Charactertemplate.CharacterMode = ChInfo[++i].ToString();
        Charactertemplate.attackDistance = int.Parse(ChInfo[++i].ToString());
        Charactertemplate.attackSpeed = int.Parse(ChInfo[++i].ToString());
        Charactertemplate.Damage = int.Parse(ChInfo[++i].ToString());
        Charactertemplate.Defence = int.Parse(ChInfo[++i].ToString());
        Charactertemplate.HP = int.Parse(ChInfo[++i].ToString());
        Charactertemplate.MaxHP = int.Parse(ChInfo[++i].ToString());
        Charactertemplate.SP = int.Parse(ChInfo[++i].ToString());
        Charactertemplate.MaxSP = int.Parse(ChInfo[++i].ToString());
        Charactertemplate.Money = int.Parse(ChInfo[++i].ToString());
        db.CloseSqlConnection();

        CreateDataBase();
        SqliteDataReader chpos = db.Select("_CharacterPos", "1", "CharacterPosID");
        chpos.Read();
        Charactertemplate.CurrentPos.x = chpos.GetFloat(chpos.GetOrdinal("posX"));
        Charactertemplate.CurrentPos.y = chpos.GetFloat(chpos.GetOrdinal("posY"));
        Charactertemplate.CurrentPos.z = chpos.GetFloat(chpos.GetOrdinal("posZ"));

        db.CloseSqlConnection();

    }
    /// <summary>
    /// 新的游戏开始
    /// </summary>
    /// <param name="Jobid">职业ID</param>
    public void CreateCharacterData(int Jobid)
    {
        CreateDataBase();
        int i = 0;
        SqliteDataReader JobData = db.Select("_Job", "JobID", Jobid.ToString());
        JobData.Read();
        Charactertemplate.JobID = int.Parse(JobData[i].ToString());
        Charactertemplate.CharacterMode = JobData[++i].ToString();
        Charactertemplate.attackDistance = int.Parse(JobData[++i].ToString());
        Charactertemplate.attackSpeed = int.Parse(JobData[++i].ToString());
        Charactertemplate.Damage = int.Parse(JobData[++i].ToString());
        Charactertemplate.Defence = int.Parse(JobData[++i].ToString());
        Charactertemplate.HP = int.Parse(JobData[++i].ToString());
        Charactertemplate.MaxHP = int.Parse(JobData[++i].ToString());
        Charactertemplate.SP = int.Parse(JobData[++i].ToString());
        Charactertemplate.MaxSP = int.Parse(JobData[++i].ToString());
        Charactertemplate.Money = int.Parse(JobData[++i].ToString());

        db.CloseSqlConnection();

        CreateDataBase();
        db.InsertInto("_Character", new string[] {
         "'"+ Charactertemplate.Name+"'",
         "1",
        "'"+ Charactertemplate.CharacterMode+"'",
        Charactertemplate.attackDistance.ToString(),
        Charactertemplate.attackSpeed.ToString(),
        Charactertemplate.Damage.ToString(),
        Charactertemplate.Defence.ToString(),
        Charactertemplate.HP.ToString(),
        Charactertemplate.MaxHP.ToString(),
        Charactertemplate.SP.ToString(),
        Charactertemplate.MaxSP.ToString(),
        Charactertemplate.Money.ToString()
        });
        db.CloseSqlConnection();
        CreateDataBase();
        db.InsertInto("_Account", new string[] { "1", "'" + GameObject.Find("InputField").GetComponent<InputField>().text + "'" });
        SaveCharacter.instance.listitem.Clear();
        db.InsertInto("_CharacterPos", new string[] { "1", "12", "11", "5" });
        db.InsertInto("_Task", new string[] { "0", "'" + "www" + "'" });
        db.CloseSqlConnection();

        Charactertemplate.CurrentPos = new Vector3(12f, 11f, 5f);
        Plotconversation.OverTalk = true;
    }
    /// <summary>
    /// 删除存档
    /// </summary>
    public void DelectSave()
    {
        CreateDataBase();
        db.DeleteContents("_Account");
        db.DeleteContents("_Character");
        db.DeleteContents("_knapsack");
        db.DeleteContents("_CharacterPos");
        db.DeleteContents("_StateBag");
        db.DeleteContents("_Task");
        db.CloseSqlConnection();

    }
    /// <summary>
    /// 获取账号信息
    /// </summary>
    /// <returns></returns>
    public string FindData()
    {
        CreateDataBase();
        SqliteDataReader sqliteData = db.Select("_Account", "AccountID", "1");
        string name = sqliteData[1].ToString();
        db.CloseSqlConnection();
        return name;
    }
    /// <summary>
    /// 获取背包物品信息
    /// </summary>
    /// <returns></returns>
    public void GetBagInfo()
    {

        CreateDataBase();
        SqliteDataReader _knapsackData = db.ReadFullTable("_knapsack");
        while (_knapsackData.Read())
        {
            int i = 0;
            CharacterBagtemplate.ID = int.Parse(_knapsackData[i].ToString());
            CharacterBagtemplate.Name = _knapsackData[++i].ToString();
            CharacterBagtemplate.Description = _knapsackData[++i].ToString();
            CharacterBagtemplate.BuyPrice = int.Parse(_knapsackData[++i].ToString());
            CharacterBagtemplate.SellPrice = int.Parse(_knapsackData[++i].ToString());
            CharacterBagtemplate.Icon = _knapsackData[++i].ToString();
            CharacterBagtemplate.ItemType = _knapsackData[++i].ToString();
            CharacterBagtemplate.DmORDf = int.Parse(_knapsackData[++i].ToString());
            CharacterBagtemplate.Sprite = _knapsackData[++i].ToString();
            CharacterBagtemplate.NewItem(CharacterBagtemplate.ItemType);
        }
        db.CloseSqlConnection();
    }
    /// <summary>
    ///获取装备栏的装备 
    /// </summary>
    public void GetStateBag()
    {
        CreateDataBase();
        SqliteDataReader _StateBag = db.ReadFullTable("_StateBag");
        int i=0;
        while (_StateBag.Read())
        {
            SaveCharacter.StateID[i]=int.Parse(_StateBag[0].ToString());
            ++i;
        }
        db.CloseSqlConnection();

    }
    /// <summary>
    /// 获取任务
    /// </summary>
    public void GetTaskID()
    {
        CreateDataBase();
        SqliteDataReader task = db.Select("_Task", "TaskName", "'"+"www"+"'");
        task.Read();
        Charactertemplate.TaskID = int.Parse(task[0].ToString());
        Charactertemplate.TaskName = task[1].ToString();
        db.CloseSqlConnection();
    }
    /// <summary>
    /// 存档
    /// </summary>
    public void Save()
    {
        CreateDataBase();
        db.UpdateInto("_Character", new string[] { "Name", "CharacterID", "CharacterMode", "attackDistance", "attackSpeed", "Damage", "Defence", "HP", "MaxHP", "SP", "MaxSP" , "Money" },
            new string[] {"'"+ Charactertemplate.Name+"'",
               "1",
            "'"+ Charactertemplate.CharacterMode+"'",
        Charactertemplate.attackDistance.ToString(),
        Charactertemplate.attackSpeed.ToString(),
        Charactertemplate.Damage.ToString(),
        Charactertemplate.Defence.ToString(),
        Charactertemplate.HP.ToString(),
        Charactertemplate.MaxHP.ToString(),
        Charactertemplate.SP.ToString(),
        Charactertemplate.MaxSP.ToString(),
                Charactertemplate.Money.ToString()}, "CharacterID", "1");
        db.CloseSqlConnection();

        CreateDataBase();

        for (int i = 0; i < SaveCharacter.instance.listitem.Count; i++)
        {
            int id = SaveCharacter.instance.listitem[i];
            db.InsertInto("_knapsack", new string[] {
                KnapsackManager.Instance.ItemList[id].Id.ToString(),
                "'"+KnapsackManager.Instance.ItemList[id].Name+"'",
            "'"+KnapsackManager.Instance.ItemList[id].Description+"'",
             KnapsackManager.Instance.ItemList[id].BuyPrice.ToString(),
                        KnapsackManager.Instance.ItemList[id].SellPrice.ToString(),
             "'"+KnapsackManager.Instance.ItemList[id].Icon+"'",
                        "'"+KnapsackManager.Instance.ItemList[id].ItemType+"'",
             KnapsackManager.Instance.ItemList[id].Id.ToString(),
            "'"+KnapsackManager.Instance.ItemList[id].sprite+"'"});

        }
        db.CloseSqlConnection();

        CreateDataBase();
        db.UpdateInto("_CharacterPos", new string[] { "CharacterPosID", "posX", "posY", "posZ" }, new string[] {"1",
            Charactertemplate.CurrentPos.x.ToString(),
            Charactertemplate.CurrentPos.y.ToString(),
            Charactertemplate.CurrentPos.z.ToString()}, "CharacterPosID", "1");
        db.DeleteContents("_StateBag");
        db.CloseSqlConnection();
        CreateDataBase();
        for (int i = 0; i < SaveCharacter.StateID.Count; i++)
        {
            int id = SaveCharacter.StateID[i];           
            db.InsertInto("_StateBag", new string[] { id.ToString() });
        }
        db.UpdateInto("_Task", new string[] { "TaskID", "TaskName" },new string[] { Charactertemplate.TaskID.ToString(),"'"+Charactertemplate.TaskName+"'" }, "TaskName", "'" + "www" + "'");
        db.CloseSqlConnection();
    }
}