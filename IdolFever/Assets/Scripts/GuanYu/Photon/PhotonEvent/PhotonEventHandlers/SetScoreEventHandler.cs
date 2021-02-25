using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace IdolFever {
    internal sealed class SetScoreEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        [SerializeField] private ScoreMeter scoreMeterScript;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        private SetScoreEventHandler() {
            scoreMeterScript = null;
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }

        private void OnDisable() {
            PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        }

        #endregion

        public void OnEvent(EventData photonEvent) {
            if(photonEvent.Code == (byte)EventCodes.EventCode.SetScoreEvent) {
                scoreMeterScript.SetScore((float)photonEvent.CustomData);
            }
        }
    }
}