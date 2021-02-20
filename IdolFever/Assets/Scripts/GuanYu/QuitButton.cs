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
                Receivers = ReceiverGroup.All
            };
            PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.EnemyDisconnectedEvent,
                PhotonNetwork.LocalPlayer.ActorNumber, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);

            while(!EnemyDisconnectedEventHandler.IsLocalPlayer) { //Ensure event has been raised before disconnecting
                yield return null;
            }

            EnemyDisconnectedEventHandler.IsLocalPlayer = false;
            PhotonNetwork.Disconnect();

            while(PhotonNetwork.IsConnected) {
                yield return null;
            }

            asyncSceneTransitionOut.ChangeScene();
        }
    }
}