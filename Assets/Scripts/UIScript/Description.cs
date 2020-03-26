using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Description : MonoBehaviour {

    public GameObject texts;
    public GameObject selectcircle;
    public void Hide()
    {
        texts.SetActive(false);
    }
    public void Show()
    {
        texts.SetActive(true);
    }
}
