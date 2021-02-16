using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMeterBar : MonoBehaviour
{
    public KeyBindingManager key;
    public TMP_Text scoreText;

    void Start()
    {
        ScoreMeter.SetScoreMeterValue(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key.B1_Key) || Input.GetKeyDown(key.B2_Key) || Input.GetKeyDown(key.B3_Key) || Input.GetKeyDown(key.B4_Key))
        {
            ScoreMeter.SetScoreMeterValue(ScoreMeter.GetScoreMeterValue() + 0.01f);
            scoreText.text = "Score: " + Mathf.RoundToInt(ScoreMeter.GetScoreMeterValue() * 100);
        }
    }
}
