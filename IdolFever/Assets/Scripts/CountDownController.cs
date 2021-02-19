using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownController : MonoBehaviour
{
    public int countDownTime;
    public TMP_Text countDownDisplay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownToStart());
    }

    IEnumerator CountDownToStart()
    {
        while (countDownTime > 0)
        {
            countDownDisplay.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);
            --countDownTime;
        }
        countDownDisplay.text = "Go !";

        yield return new WaitForSeconds(1f);

        countDownDisplay.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
