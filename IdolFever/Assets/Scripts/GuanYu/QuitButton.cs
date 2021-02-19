using Photon.Pun;
using UnityEngine;

namespace IdolFever {
    internal sealed class QuitButton: MonoBehaviour {
        #region Fields

        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOut;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public QuitButton() {
            asyncSceneTransitionOut = null;
        }

        public void OnClick() {
            _ = StartCoroutine(nameof(DisconnectAndChangeScene));
        }

        private System.Collections.IEnumerator DisconnectAndChangeScene() {
            PhotonNetwork.Disconnect();

            while(PhotonNetwork.IsConnected) {
                yield return null;
            }

            asyncSceneTransitionOut.ChangeScene();
        }
    }
}