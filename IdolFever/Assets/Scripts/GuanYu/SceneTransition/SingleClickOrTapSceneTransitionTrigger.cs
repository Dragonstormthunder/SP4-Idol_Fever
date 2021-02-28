using UnityEngine;

namespace IdolFever {
    internal sealed class SingleClickOrTapSceneTransitionTrigger: MonoBehaviour {
        #region Fields

        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOutScript;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            if(Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved))) {
                asyncSceneTransitionOutScript.ChangeScene();
            }
	    }

        #endregion

        public SingleClickOrTapSceneTransitionTrigger() {
            asyncSceneTransitionOutScript = null;
        }
    }
}