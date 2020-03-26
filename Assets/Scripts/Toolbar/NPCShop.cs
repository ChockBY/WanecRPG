using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class NPCShop : MonoBehaviour
{
    protected playCanvasAnim canvasAnim;
    public string PlaneName;
    private void Start()
    {
        Init();
    }
    virtual public void Init()
    {
        canvasAnim = GameObject.Find(PlaneName).GetComponent<playCanvasAnim>();
    }
    virtual public void ClickNpc()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButton(0))
        {

            if (Physics.Raycast(ray, out hit, 15f) && !EventSystem.current.IsPointerOverGameObject())
            {

                if (hit.collider.gameObject.tag == this.PlaneName)
                {
                    if (canvasAnim == null) print(null);
                    canvasAnim.PlayAnim();
                }

            }
        }
    }
    private void Update()
    {
        ClickNpc();
    }
    virtual public void ClosePlane()
    {
        canvasAnim.CancelPlay();
    }
}
