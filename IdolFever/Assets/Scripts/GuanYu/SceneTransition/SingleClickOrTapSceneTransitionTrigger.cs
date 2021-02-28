using UnityEngine;

namespace IdolFever {
    internal sealed class SingleClickOrTapSceneTransitionTrigger: MonoBehaviour {
        #region Fields

        [SerializeField] private AudioSource audioSrc;
        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOutScript;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            if(Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
                if(audioSrc != null) {
                    audioSrc.Play();
                }

                asyncSceneTransitionOutScript.ChangeScene();
            }
	    }

        #endregion

        public SingleClickOrTapSceneTransitionTrigger() {
            audioSrc = null;
            asyncSceneTransitionOutScript = null;
        }
    }
}