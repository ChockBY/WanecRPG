using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Completeduplicate : MonoBehaviour {

	public void ReturnMainGame()
    {
        operationDB.instance.GetCharacter();
        SceneManager.LoadScene("Load");
    }
}
