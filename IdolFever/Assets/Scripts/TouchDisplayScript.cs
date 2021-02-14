using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchDisplayScript : MonoBehaviour
{
    public enum TOUCH_KEYS
    {
        KEY_SINGLE,
        KEY_LONG_SINGLE,
        KEY_DOUBLE,
        NO_TOUCH,
        NUM_KEY
    }

    public Text phaseDisplayText;
    private Touch theTouch;
    private float timeTouchEnded;
    private float displayTime = 0.5f;

    private float touchDuration = 0f;

    // don't want other locations accessing it changing the value
    public TOUCH_KEYS TouchType
    {
        get { return key; }
    }

    [SerializeField] private TOUCH_KEYS key = TOUCH_KEYS.NUM_KEY;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touchDuration += Time.deltaTime; // increase the time taken
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Ended && touchDuration < 0.2f)
            {
                StartCoroutine("singleOrDouble");
            }
            else if (Time.time - timeTouchEnded > displayTime)
            {
                phaseDisplayText.text = theTouch.phase.ToString();
                key = TOUCH_KEYS.KEY_LONG_SINGLE;
                timeTouchEnded = Time.time;
                touchDuration = 0f;
            }
            else
            {
                touchDuration = 0f;
            }

        }
        else if (Time.time - timeTouchEnded > displayTime)
        {
            phaseDisplayText.text = "";
            key = TOUCH_KEYS.NO_TOUCH;
        }
    }

    IEnumerator singleOrDouble()
    {
        yield return new WaitForSeconds(0.3f);
        if (theTouch.tapCount == 1)
        {
            phaseDisplayText.text = "Single";
            key = TOUCH_KEYS.KEY_SINGLE;
            Debug.Log("Single");
        }
        else if (theTouch.tapCount == 2)
        {
            //this coroutine has been called twice. We should stop the next one here otherwise we get two double tap
            key = TOUCH_KEYS.KEY_DOUBLE;
            phaseDisplayText.text = "Double";
            StopCoroutine("singleOrDouble");
            Debug.Log("Double");
        }
    }

}
