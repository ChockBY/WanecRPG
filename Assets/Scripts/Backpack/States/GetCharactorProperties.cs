using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ARPGDemo.Character;
/// <summary>
/// 属性值
/// </summary>
public class GetCharactorProperties : MonoBehaviour
{

    protected Text text;
    protected CharacterStatus status;
    // Use this for initialization
    private void Awake()
    {
        
    }
    void Start()
    {
        text = GetComponent<Text>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        Getnumer();
    }
    virtual public void Getnumer()
    {

    }
}
