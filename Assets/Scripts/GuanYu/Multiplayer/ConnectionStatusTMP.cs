using Photon.Pun;
using TMPro;
using UnityEngine;

namespace IdolFever {
    internal sealed class ConnectionStatusTMP: MonoBehaviour {
        #region Fields

        private readonly string textFront = "Connection Status: ";
        [SerializeField] private TextMeshProUGUI tmpComponent;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            tmpComponent.text = textFront + PhotonNetwork.NetworkClientState;
        }

        #endregion

        public ConnectionStatusTMP() {
            tmpComponent = null;
        }
    }
}