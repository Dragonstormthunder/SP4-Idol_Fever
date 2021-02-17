using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IdolFever
{
    public class ScoreMeter : MonoBehaviour
    {
        private Image scoreMeterImg;
        private float score, maxscore;

        /// Sets the health bar value
        /// value should be between 0 to 1</param>
        public void SetScoreMeterValue(float value)
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

        public float GetScoreMeterValue()
        {
            return score;
        }

        public void AddScore(float a)
        {
            SetScoreMeterValue(score + a);
        }

        public void SetScoreMeterColor(Color healthColor)
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
}
