using UnityEngine;
public class CameraMovement : MonoBehaviour
{  //摄像机跟踪速度
    public float smooth = 1.5f;
    public Transform player;
    private Vector3 relCameraPos;
    void Awake()
    {

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        player.position = Charactertemplate.CurrentPos;
        relCameraPos = transform.position - player.position;
        this.transform.position = relCameraPos;
    }
    void FixedUpdate()
    {
        this.transform.parent = null;
        transform.position =player.position + relCameraPos;
    }
}
