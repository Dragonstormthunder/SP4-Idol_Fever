using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace IdolFever.Character
{

    public class CharacterSkillEventReceiver : MonoBehaviour, IOnEventCallback
    {
        #region Fields

        public CharacterSkill characterSkill;

        #endregion

        #region Unity Messages

        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }

        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        }

        #endregion

        public void OnEvent(EventData photonEvent)
        {
            //Debug.Log("Character Skill Event Receiver Outside");

            if (photonEvent.Code == (byte)EventCodes.EventCode.SendSkillOver)
            {
                //Debug.Log("Character Skill Event Receiver Inside");
                
                float[] data = (float[])photonEvent.CustomData;

                //Debug.Log("Send Opponent Skill " + data[0] + " " + data[1]);

                // activate the opponent skill
                characterSkill.OpponentActive = true;
                characterSkill.OpponentMultiplier = data[0];
                characterSkill.OpponentCooldown = data[1];

            }
        }

    }
}