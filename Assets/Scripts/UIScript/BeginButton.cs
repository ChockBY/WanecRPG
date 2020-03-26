using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class BeginButton : MonoBehaviour
{
    public GameObject CountinuePlane;
    public GameObject errorPlane;
    public void BeginGameButton()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        if (operationDB.instance.FindData() == string.Empty)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            CountinuePlane.SetActive(true);
        }

    }
    public void ContinueButton()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        if (operationDB.instance.FindData() != string.Empty)
        {
            Plotconversation.OverTalk = false;
            operationDB.instance.GetCharacter();
            StartCoroutine(loadCountineGame());
        }
        else
        {
            errorPlane.SetActive(true);
        }
    }
    IEnumerator loadCountineGame()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Load");
    }
    public void EnsureButton()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        operationDB.instance.DelectSave();
        this.gameObject.SetActive(false);
    }
    public void CancelButton()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        CountinuePlane.SetActive(false);
    }
    public void CancelErrorPlane()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        errorPlane.SetActive(false);
    }
}
