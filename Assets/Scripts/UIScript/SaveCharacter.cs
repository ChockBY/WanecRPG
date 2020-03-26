using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Backpack;
public class SaveCharacter : MonoSingleton<SaveCharacter> {
    public List<int> listitem = new List<int>();
    public static List<int> StateID = new List<int>(3);
    public List<GameObject> StateItem = new List<GameObject>();
    public GridPanelUI panelUI;
    public void saveCharacter()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        Charactertemplate.CurrentPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Debug.Log(Charactertemplate.CurrentPos);
        SaveBag();
        SaveState();
        SaveTask();
        operationDB.instance.Save();
    }
    public void SaveBag()
    {
        for (int i = 0; i < panelUI.Grids.Length; i++)
        {
            if(panelUI.Grids[i].transform.childCount>0)
            {
                listitem.Add(panelUI.Grids[i].GetChild(0).GetComponent<ItemUI>().id);
            }
        }
    }
    public void SaveState()
    {
        StateID.Clear();
        for (int i = 0; i < StateItem.Count; i++)
        {
            if(StateItem[i].transform.childCount>0)
            {
                StateID.Add(StateItem[i].transform.GetChild(0).GetComponent<ItemUI>().id);
                Debug.Log(StateID[i]);
            }
            else
            {
                StateID.Add(-1);
                Debug.Log(StateID[i]);
            }
        }
    }
    public void SaveTask()
    {
        Charactertemplate.TaskID = TaskManager.instance.TaskID;
        Charactertemplate.TaskName = "www";
    }
}
