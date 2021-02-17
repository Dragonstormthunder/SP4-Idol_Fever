using UnityEngine;

namespace IdolFever {
    internal sealed class IntroToMultiplayerLobbyTrigger: MonoBehaviour {
        #region Fields

        [SerializeField] private AsynchronousSceneTransition asyncSceneTransitionScript;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            if(Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
                asyncSceneTransitionScript.ChangeScene();
            }
	    }

        #endregion

        public IntroToMultiplayerLobbyTrigger() {
            asyncSceneTransitionScript = null;
        }
    }
}