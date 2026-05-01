using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsScreenPopUp : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool mouseInside;

    // disable the pop up when clicked out
    public void OnDeselect(BaseEventData eventData)
    {
        
        // check whether the mouse pointer is inside of the panel
        if (!mouseInside)
            gameObject.SetActive(false);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseInside = false;
    }
}
