using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandLineTooltipCanvasGroup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  {

    public CanvasGroup helperWindow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        helperWindow.alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        helperWindow.alpha = 0;
    }
}
