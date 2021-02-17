using IdolFever;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMeter : MonoBehaviour
{
    private Image scoreMeterImg;
    private float score;
    public float maxscore;

    /// Sets the health bar value
    /// value should be between 0 to 1</param>
    public void SetScoreMeterValue(float value)
    {
        score = value;
        scoreMeterImg.fillAmount = score / maxscore;

        float factor = scoreMeterImg.fillAmount * 0.5f + 0.8f;
        if(factor > 1.0f) {
            factor -= 1.0f;
        }
        SetScoreMeterColor(Color.HSVToRGB(factor, 1.0f, 1.0f));
    }

    public float GetScoreMeterValue()
    {
        return score;
    }

    public void AddScore(float a)
    {
        SetScoreMeterValue(score + a);
        
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {
            Receivers = ReceiverGroup.Others
        };
        PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.SetScoreEvent,
            score, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);
    }

    public void SetScore(float score) {
        SetScoreMeterValue(score);
    }

    public void SetScoreMeterColor(Color healthColor)
    {
        scoreMeterImg.color = healthColor;
    }

    private void Start()
    {
        scoreMeterImg = GetComponent<Image>();
        score = 0;
    }
}