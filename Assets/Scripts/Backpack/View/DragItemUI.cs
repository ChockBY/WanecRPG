using UnityEngine;
using System.Collections;
namespace ARPGDemo.Backpack
{
    public class DragItemUI : ItemUI
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetLocalPosition(Vector2 position)
        {
            transform.localPosition = new Vector2(position.x + 30, position.y);
        }


    }
}