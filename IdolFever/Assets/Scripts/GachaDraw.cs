using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IdolFever.UI;

public class GachaDraw : MonoBehaviour
{
    public GameObject R_imageGirl;
    // Start is called before the first frame update
    void Start()
    {
        R_imageGirl.gameObject.SetActive(false);
        Debug.Log("heeee222222");
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticDataStorage.R_Girl == true)
        {
            Instantiate(R_imageGirl);
            R_imageGirl.gameObject.SetActive(true);
            Debug.Log("heeee");
        }
    }

    public void R_Girl_false()
    {
        StaticDataStorage.R_Girl = false;
        R_imageGirl.gameObject.SetActive(false);
    }
}
