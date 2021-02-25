using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace IdolFever {
    internal sealed class LeaveButton: MonoBehaviour {
        #region Fields

        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOut;

        #endregion

        #region Properties
        #endregion

        public LeaveButton() {
            asyncSceneTransitionOut = null;
        }

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            _ = StartCoroutine(nameof(DcAndChangeScene));
        }

        private IEnumerator DcAndChangeScene() {
            PhotonNetwork.Disconnect();

            while(PhotonNetwork.IsConnected) {
                yield return null;
            }

            asyncSceneTransitionOut.ChangeScene();
        }
    }
}