using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMeter : MonoBehaviour
{
    private static Image scoreMeterImg;
    private static float score, maxscore;

    /// Sets the health bar value
    /// value should be between 0 to 1</param>
    public static void SetScoreMeterValue(float value)
    {
        score = value;
        scoreMeterImg.fillAmount = score / maxscore;
        if (scoreMeterImg.fillAmount < 0.2f)
        {
            SetScoreMeterColor(Color.red);
        }
        else if (scoreMeterImg.fillAmount < 0.4f)
        {
            SetScoreMeterColor(Color.yellow);
        }
        else if ((scoreMeterImg.fillAmount > 0.8f) && (scoreMeterImg.fillAmount <= 1f))
        {
            SetScoreMeterColor(Color.green);
        }
    }

    public static float GetScoreMeterValue()
    {
        return score;
    }

    public static void AddScore(float a)
    {
        SetScoreMeterValue(score + a);
    }

    public static void SetScoreMeterColor(Color healthColor)
    {
        scoreMeterImg.color = healthColor;
    }

    private void Start()
    {
        scoreMeterImg = GetComponent<Image>();
        score = 0;
        maxscore = 1000000;
    }
}
