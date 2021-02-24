using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InputFieldClickClear : MonoBehaviour, IPointerClickHandler, IDeselectHandler
{

    #region Field

    //[SerializeField] private InputField inputField;

    public TextMeshProUGUI placementText;
    public TMP_InputField inputField;
    //public TextMeshProUGUI enteredText;

    [SerializeField] private string originalPlacementText;

    #endregion

    #region Unity Messages

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        originalPlacementText = placementText.text;
    }

    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        if (placementText.text == originalPlacementText)
        {
            // clear data
            placementText.text = "";
            EventSystem.current.SetSelectedGameObject(transform.gameObject);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (inputField.text == "")
        {
            // reset the field
            placementText.text = originalPlacementText;
        }

    }
}
