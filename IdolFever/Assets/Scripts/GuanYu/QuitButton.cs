using Photon.Pun;
using Photon.Realtime;
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
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions {
                Receivers = ReceiverGroup.Others
            };
            PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.EnemyDisconnectedEvent,
                null, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);

            PhotonNetwork.Disconnect();

            while(PhotonNetwork.IsConnected) {
                yield return null;
            }

            asyncSceneTransitionOut.ChangeScene();
        }
    }
}