using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLifespan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.3f);
        
        for(int i = 0; i < 30; ++i)
        {
            Color x = this.GetComponent<UnityEngine.UI.Image>().color;
            this.GetComponent<UnityEngine.UI.Image>().color = new Color(x.r, x.g, x.b, x.a - 0.034f);
            yield return null;
        }


        Destroy(this.gameObject);
    }
}
