using UnityEngine;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class ProgressBar: MonoBehaviour {
        #region Fields

        [SerializeField] private AsynchronousSceneTransition asyncSceneTransition;
        [SerializeField] private Image img;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

	    private void Update() {
            img.fillAmount = asyncSceneTransition.ProgressVal;
        }

        #endregion

        public ProgressBar() {
            asyncSceneTransition = null;
            img = null;
        }
    }
}