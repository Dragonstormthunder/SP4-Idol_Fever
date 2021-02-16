using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMeter : MonoBehaviour
{
    public int currScore = 0;
    public int maxScore = 10;

    public ScoreMeterBar scoremeter;
    public KeyBindingManager key;

    // Start is called before the first frame update
    void Start()
    {
        currScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key.B1_Key) || Input.GetKeyDown(key.B2_Key) || Input.GetKeyDown(key.B3_Key) || Input.GetKeyDown(key.B4_Key))
        {
            AddScore(1);
        }
    }

    public void AddScore(int addScore)
    {
        currScore += addScore;

        scoremeter.SetScore(currScore);
    }
}
