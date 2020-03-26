using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Settingup : MonoBehaviour {

    public GameObject Plane;
    public void RightButton()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        SceneManager.LoadScene("Begin");
    }
    public void CancelButton()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        Plane.gameObject.SetActive(false);
    }
    public void OpenSettingUp()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        Plane.gameObject.SetActive(true);
    }
}
