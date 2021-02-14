using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingArrowScript : MonoBehaviour
{

    [SerializeField] private ScrollRect scrollRect;

    public GameObject maxArrow;
    public GameObject minArrow;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        //Debug.Log(scrollRect.verticalNormalizedPosition);

        //if (scrollRect.verticalNormalizedPosition > 0.9)
        //    Debug.Log("Yes");
        //else if (scrollRect.verticalNormalizedPosition < 0.1)
        //        Debug.Log("No");

        if (scrollRect.verticalNormalizedPosition > 0.85)
        {
            maxArrow?.SetActive(false);
            minArrow?.SetActive(true);
        }
        else if (scrollRect.verticalNormalizedPosition<0.15)
        {
            maxArrow?.SetActive(true);
            minArrow?.SetActive(false);
        }
        else
        {
            maxArrow?.SetActive(true);
            minArrow?.SetActive(true);
        }

    }


}
