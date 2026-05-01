using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SongSelectButtonScroll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Fields

    [SerializeField] private bool mouseInside;

    #endregion

    #region Properties
    #endregion

    #region Unity Messages

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseInside = false;
    }

    private void Update()
    {
        if (mouseInside)
        {
            //Debug.Log("He");
        }
    }

    #endregion

}
