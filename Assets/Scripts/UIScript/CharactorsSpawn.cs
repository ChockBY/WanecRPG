using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharactorsSpawn :MonoBehaviour {

    public InputField field;
    public GameObject []CharactorsPrefabs;
    private GameObject []CharactorGameObejct;
    public  int SelectIndex=0;
    int length;

	void Start () {
        length = CharactorsPrefabs.Length;
        CharactorGameObejct = new GameObject[length];       
        for (int i = 0; i < length; i++)
        {
            CharactorGameObejct[i] = Instantiate(CharactorsPrefabs[i]);
        }
        UpdateChatactors();
	}

    private void UpdateChatactors()
    {
        CharactorGameObejct[SelectIndex].gameObject.SetActive(true);
        for (int i = 0; i < length; i++)
        {
            if(i!=SelectIndex)
            {
                CharactorGameObejct[i].gameObject.SetActive(false);
            }
        }
    }
    public void OnPreButtonClick()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        SelectIndex--;
        if (SelectIndex == -1)
        {
            SelectIndex = length - 1;
        }
           UpdateChatactors();
        
    }
    public void OnNextButtonClick()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        SelectIndex++;
        if (SelectIndex == length)
        {
            SelectIndex = 0;
            
        }
        UpdateChatactors();
    }
    public void OnOKButtonClick()
    {
        AudioManager.Intance.PlaySoundAudio("button");
        if (field.textComponent.text == string.Empty)
            return;
        else
        {
            Charactertemplate.JobID = SelectIndex;
            operationDB.instance.CreateCharacterData(Charactertemplate.JobID);
            operationDB.instance.GetName();
           // operationDB.instance.GetBagInfo();
            operationDB.instance.GetStateBag();
            operationDB.instance.GetTaskID();
            StartCoroutine(Loadthe());
        }

    }
    private IEnumerator Loadthe()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Load");
    }
}
