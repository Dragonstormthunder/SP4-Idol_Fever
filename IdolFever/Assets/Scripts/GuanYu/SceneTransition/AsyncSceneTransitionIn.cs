using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class AsyncSceneTransitionIn: MonoBehaviour {
        #region Fields

        [SerializeField] private string sceneName;
        [SerializeField] private Animator animator;
        [SerializeField] private Image img;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            if(!PhotonNetwork.IsConnected || (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)){
                TransitionIn();
            }
        }

        #endregion

        public AsyncSceneTransitionIn() {
            sceneName = string.Empty;
            animator = null;
            img = null;
        }

        public void TransitionIn() {
            img.fillAmount = 100.0f;

            if(SceneTracker.prevSceneName == sceneName) {
                animator.SetTrigger("End");
            }
        }
    }
}