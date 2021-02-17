using UnityEngine;
using TMPro;

namespace IdolFever.UI
{
    public class ScoreMeterBar : MonoBehaviour
    {
        public KeyBindingManager key;
        public TMP_Text scoreText;
        public ScoreMeter score;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            scoreText.text = "Score: " + Mathf.RoundToInt(score.GetScoreMeterValue());
        }
    }
}
