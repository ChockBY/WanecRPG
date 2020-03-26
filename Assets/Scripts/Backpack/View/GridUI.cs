using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
namespace ARPGDemo.Backpack
{
    public class GridUI : MonoBehaviour
        , IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region Enter&&Exit
        // public static event Action<Transform> OnEnter;
        //public static event Action OnExit;
        public static Action<Transform> OnPressDown;
        public static Action OnHight;
        public Text text;
        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    if (eventData.pointerEnter.tag == "Grid")
        //    {
        //        if (OnEnter != null)
        //            OnEnter(transform);
        //    }
        //}

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    if (eventData.pointerEnter.tag == "Item")
        //    {
        //        if (OnExit != null)
        //            OnExit();
        //    }
        //}
        #endregion


        public static Action<Transform> OnLeftBeginDrag;
        public static Action<Transform, Transform> OnLeftEndDrag;

        public void OnBeginDrag(PointerEventData eventData)
        {

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (eventData.pointerDrag.tag == "shopitem")
                    return;
                if (OnLeftBeginDrag != null && transform.childCount > 0)
                {
                    OnLeftBeginDrag(transform);
                }

            }
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {

            // if (eventData.button == PointerEventData.InputButton.Left)
            //{
            // if (eventData.pointerEnter.transform.tag == "shopitem") return;
            if (OnLeftEndDrag != null)
            {
                OnHight();
                if (eventData.pointerEnter == null)
                {
                    OnLeftEndDrag(transform, null);
                }
                else if (eventData.pointerEnter.transform.tag != "shopitem")
                {
                    OnLeftEndDrag(transform, eventData.pointerEnter.transform);
                }


            }

            //}
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            GameObject select;
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (eventData.pointerEnter.transform.GetChild(0).gameObject.tag == "shopitem")
                {
                    if (this.transform.GetChild(0).GetComponent<itemInfromation>())
                    {
                        transform.parent.GetComponent<Description>().Show();
                        select = transform.parent.GetComponent<Description>().selectcircle;
                        select.transform.parent = transform.GetChild(0).transform;
                        select.transform.position = Vector3.one;
                        select.transform.localPosition = Vector3.one;
                        text.text = KnapsackManager.Instance.GetTooltipText(this.transform.GetChild(0).GetComponent<itemInfromation>().Item);
                    }

                }
                else if (OnPressDown != null && transform.childCount > 0)
                    OnPressDown(transform);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //if (eventData.pointerEnter.transform.gameObject!=null)
            //{
            if (eventData.pointerDrag.transform.tag == "shopitem")
            {
                eventData.pointerPress.transform.parent.GetComponent<Description>().Hide();
            }

            else if (OnHight != null && transform.childCount > 0 && eventData.pointerEnter.transform.tag != "shopitem")
            {
                OnHight();
            }
            // }

        }
    }
}
