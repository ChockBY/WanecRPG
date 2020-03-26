using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace ARPGDemo.Backpack
{
    public class TooltipUI : MonoBehaviour
    {
        //public Text OutlineText;
        public Text ContentText;

        public void UpdateTooltip(string text)
        {
            //OutlineText.text = text;
            ContentText.text = text;
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void SetLocalPosition(Vector2 position)
        {
            transform.localPosition = new Vector2(position.x + 150, position.y - 80);
        }
        private void OnDisable()
        {
            this.gameObject.SetActive(false);
        }
    }
}
