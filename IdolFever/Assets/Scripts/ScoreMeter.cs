using IdolFever;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace IdolFever
{
    public class ScoreMeter : MonoBehaviour
    {
        private Image scoreMeterImg;
        private float score;
        public float maxscore;

        public Character.CharacterSkill skills;


        /// Sets the health bar value
        /// value should be between 0 to 1</param>
        public void SetScoreMeterValue(float value)
        {
            score = value;
            scoreMeterImg.fillAmount = score / maxscore;

        }

        public float GetScoreMeterValue()
        {
            return score;
        }

        public void AddScore(float a)
        {
            SetScoreMeterValue(score + a);

            //if (skills != null)
            //    score = skills.ApplyBonuses(score);

            if(PhotonNetwork.IsConnected) {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions {
                    Receivers = ReceiverGroup.Others
                };
                PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.SetScoreEvent,
                    score, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);
            }
        }

        public void SetScore(float score)
        {
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
}