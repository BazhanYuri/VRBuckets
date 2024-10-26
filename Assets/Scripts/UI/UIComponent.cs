using UnityEngine;

namespace UI
{
    public class UIComponent : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        
        public virtual void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
        
        public virtual void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}