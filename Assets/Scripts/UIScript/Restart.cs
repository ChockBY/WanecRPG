using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ARPGDemo.Backpack;
public class Restart : MonoBehaviour
{
    public void restartGame()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        StartCoroutine(DeleteEventSystem());
        
    }
    private IEnumerator DeleteEventSystem()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Begin");
    }
}
