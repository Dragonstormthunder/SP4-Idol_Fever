using UnityEngine;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class ProgressBar: MonoBehaviour {
        #region Fields

        [SerializeField] private AsynchronousSceneTransition asyncSceneTransition;
        [SerializeField] private Slider slider;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

	    private void Update() {
            slider.value = asyncSceneTransition.progressVal;

        }

        #endregion

        public ProgressBar() {
            asyncSceneTransition = null;
            slider = null;
        }
    }
}