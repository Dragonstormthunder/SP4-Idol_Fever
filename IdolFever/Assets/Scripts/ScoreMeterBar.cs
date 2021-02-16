using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMeterBar : MonoBehaviour
{
    public Slider scoremeter;
    public ScoreMeter playerscoreMeter;

    private void Start()
    {
        playerscoreMeter = GameObject.FindGameObjectWithTag("ScoreMeters").GetComponent<ScoreMeter>();
        scoremeter = GetComponent<Slider>();
        scoremeter.maxValue = playerscoreMeter.maxScore;
        scoremeter.value = playerscoreMeter.currScore;
    }

    public void SetScore(int score)
    {
        scoremeter.value = score;
    }
}
