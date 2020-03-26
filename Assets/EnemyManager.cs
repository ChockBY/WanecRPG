using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;
using AI.FSM;
using AI.Perception;
public class EnemyManager : MonoBehaviour
{

    public GameObject[] Enemy;
    private  float timer = 0 ;
    public void CreateEnemy()
    {

        for (int i = 0; i < Enemy.Length; i++)
        {

            if (Enemy[i].GetComponent<CharacterStatus>().HP <= 0)
            {
                timer += Time.deltaTime;
                if (timer > 10f)
                {
                    Enemy[i].SetActive(true);
                    Enemy[i].GetComponent<BaseFSM>().enabled = true;
                    Enemy[i].GetComponent<SightSensor>().enabled = true;
                    Enemy[i].GetComponent<CharacterStatus>().HP = 100;
                    Enemy[i].GetComponent<CharacterStatus>().SP = 100;
                    timer = 0;
                }
            }
        }
    }
    private void Update()
    {
        CreateEnemy();
    }
}
