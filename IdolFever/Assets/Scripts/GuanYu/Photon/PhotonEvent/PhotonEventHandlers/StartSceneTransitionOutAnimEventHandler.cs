using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

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
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
            //PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable() {
            PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
            //PhotonNetwork.RemoveCallbackTarget(this);
        }

        #endregion

        public void OnEvent(EventData photonEvent) {
            Debug.Log(photonEvent.Code, this);

            if(photonEvent.Code == (byte)EventCodes.EventCode.StartSceneTransitionOutAnimEvent) {
                Debug.Log("here000", this);

                AsyncSceneTransitionOut.IsStartSceneTransitionOutAnimReceived = true;

                string GOName = (string)photonEvent.CustomData;
                GameObject sceneTransitionGO = GameObject.Find(GOName);
                Animator animator = sceneTransitionGO.GetComponent<Animator>();

                GameObject progressBarGO = animator.transform.Find("ProgressBar").gameObject;
                GameObject progressGO = progressBarGO.transform.Find("Progress").gameObject;
                Image img = progressGO.GetComponent<Image>();

                img.fillAmount = 0.0f;

                animator.SetTrigger("Start");
            }
        }
    }
}