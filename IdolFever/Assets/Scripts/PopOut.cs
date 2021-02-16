using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopOut : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelX;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPanel()
    {
        panelX.SetActive(true);
    }

    public void OffPanel()
    {
        panelX.SetActive(false);
    }


    public void OnPanelMainGame(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

}
