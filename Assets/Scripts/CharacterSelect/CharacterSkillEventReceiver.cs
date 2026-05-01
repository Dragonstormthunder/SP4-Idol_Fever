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
                Debug.Log("Character Skill Event Receiver Inside");

                float[] data = (float[])photonEvent.CustomData;

                characterSkill.OpponentCharacterIndex = (CharacterFactory.eCHARACTER)data[(int)CharacterSkill.PHOTON_DATA_SEND.SEND_OPPONENT_INDEX];
                characterSkill.OpponentMultiplier = data[(int)CharacterSkill.PHOTON_DATA_SEND.SEND_MULTIPLIER];
                characterSkill.OpponentSkillDuration = data[(int)CharacterSkill.PHOTON_DATA_SEND.SEND_COOLDOWN];
                characterSkill.OpponentSkill_Type = (CharacterSkill.SKILL_TYPE)data[(int)CharacterSkill.PHOTON_DATA_SEND.SEND_SKILL_TYPE];
                characterSkill.OpponentActive = true;

            }
        }

    }
}