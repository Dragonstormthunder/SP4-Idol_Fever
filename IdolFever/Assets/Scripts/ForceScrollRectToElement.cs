using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceScrollRectToElement : MonoBehaviour
{

    [SerializeField] private ScrollRect scrollRect;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>(); // get the scroll rect component

        // force it to the element we want it to start at

        scrollRect.normalizedPosition = new Vector2(0, 0);

    }


}
