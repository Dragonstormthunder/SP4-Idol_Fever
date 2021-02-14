using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class ConnectionStatus: MonoBehaviour {
        #region Fields

        private readonly string textFront = "Connection Status: ";
        [SerializeField] private Text textComponent;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            textComponent.text = textFront + PhotonNetwork.NetworkClientState;
        }

        #endregion
    }
}