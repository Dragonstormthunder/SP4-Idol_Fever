using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    public int combo;
    // Start is called before the first frame update
    void Start()
    {
        combo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<UnityEngine.UI.Text>().text = combo.ToString();
    }
}
