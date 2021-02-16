using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreMeterBar : MonoBehaviour
{
    public Slider scoremeter;
    public ScoreMeter playerscoreMeter;
    public TMP_Text scoreText;

    private void Start()
    {
        playerscoreMeter = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreMeter>();
        scoremeter = GetComponent<Slider>();
        scoremeter.maxValue = playerscoreMeter.maxScore;
        scoremeter.value = playerscoreMeter.maxScore;
    }

    void Update()
    {
        scoreText.text = playerscoreMeter.currScore.ToString();

    }
    public void SetScore(int score)
    {
        scoremeter.value = score;
    }
}
