using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace IdolFever {
    internal sealed class StartSceneTransitionOutAnimEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        private StartSceneTransitionOutAnimEventHandler() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable() {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        #endregion

        public void OnEvent(EventData photonEvent) {
            if(photonEvent.Code == (byte)EventCodes.EventCode.StartSceneTransitionOutAnimEvent) {
                img.fillAmount = 0.0f;

                animator.SetTrigger("Start");
            }
        }
    }
}