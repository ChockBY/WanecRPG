using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager :MonoSingleton<TaskManager>
{

    public GameObject[] Task;
    public int TaskID;
    private void Start()
    {
        ShowTask();
    }
    public void ShowTask()
    {
        if(Task[TaskID]!=null)
        Task[TaskID].SetActive(true);
    }
}
