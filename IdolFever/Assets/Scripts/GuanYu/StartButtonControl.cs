using Photon.Pun;
using UnityEngine;

namespace IdolFever {
    internal sealed class StartButtonControl: MonoBehaviour {
        #region Fields

        [SerializeField] private GameObject startButton;

        #endregion

        #region Properties
        #endregion

        public StartButtonControl() {
            startButton = null;
        }

        #region Unity User Callback Event Funcs

        private void FixedUpdate() {
            if(PhotonNetwork.IsConnected) {
                startButton.SetActive(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2);
            }
        }

        #endregion
    }
}