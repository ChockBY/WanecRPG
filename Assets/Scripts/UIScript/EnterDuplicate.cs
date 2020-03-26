using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 进入副本
/// </summary>
public class EnterDuplicate : MonoBehaviour
{

    public void EnterButton()
    { 
        Record();
        SceneManager.LoadScene("Counterpart");
    }
    /// <summary>
    /// 存档
    /// </summary>
    public void Record()
    {
        SaveCharacter.instance.saveCharacter();
    }
}
