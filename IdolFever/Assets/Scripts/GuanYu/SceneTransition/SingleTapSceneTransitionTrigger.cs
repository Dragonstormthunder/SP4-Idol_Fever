using UnityEngine;

namespace IdolFever {
    internal sealed class SingleTapSceneTransitionTrigger: MonoBehaviour {
        #region Fields

        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOutScript;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            if(Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
                asyncSceneTransitionOutScript.ChangeScene();
            }
	    }

        #endregion

        public SingleTapSceneTransitionTrigger() {
            asyncSceneTransitionOutScript = null;
        }
    }
}