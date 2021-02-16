using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IdolFever.UI;

public class GachaDraw : MonoBehaviour
{
    public GameObject GameOverObject;
    // Start is called before the first frame update
    void Start()
    {
        GameOverObject.gameObject.SetActive(false);
        Debug.Log("heeee222222");
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticDataStorage.R_Girl == true)
        {
            Instantiate(GameOverObject);
            GameOverObject.gameObject.SetActive(true);
            Debug.Log("heeee");
        }
    }

    public void R_Girl_false()
    {
        StaticDataStorage.R_Girl = false;
        GameOverObject.gameObject.SetActive(false);
    }

    //public void RenderImage()
    //{
    //    if (StaticDataStorage.R_Girl == true)
    //    {
    //        img.gameObject.SetActive(false);
    //        Debug.Log("heeee");
    //    }
    //    else
    //    {
    //        img.enabled = false;
    //    }
    //}
}
