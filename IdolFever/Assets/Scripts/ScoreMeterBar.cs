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
        scoreText.text = "Score: " + Mathf.RoundToInt(ScoreMeter.GetScoreMeterValue());
    }
}
