using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMap : MonoBehaviour
{

    private Transform theCamera;
    private void Start()
    {
        theCamera = GameObject.Find("Camera").transform;

    }
    private void Update()
    {
        Vector3 MapRotation=new Vector3(0,0,theCamera.eulerAngles.y);
        this.transform.eulerAngles = MapRotation;
    }
}
