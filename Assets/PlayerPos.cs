using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;
public class PlayerPos : MonoBehaviour {

    public int KillAllEnemy;
    public int MustKillCount;
    public GameObject WinPlane;
    public GameObject[] enemys;
    int j = 0;
    private void Start()
    {
        GameObject.Find("Player").transform.position = new Vector3(4.25f, 0.64f, 22.41f);
    }
    private void Update()
    {
        if(KillAllEnemy>=MustKillCount)
        {
            WinPlane.SetActive(true);
            Duplicate.isPass = true;
        }
        for (int i = j; i < enemys.Length; i++)
        {
            if(enemys[i].GetComponent<CharacterStatus>().HP<0)
            {
                KillAllEnemy++;
                j = i;
            }
        }
    }
}
