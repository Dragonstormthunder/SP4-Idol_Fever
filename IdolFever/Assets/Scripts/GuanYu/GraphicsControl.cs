using UnityEngine;

namespace IdolFever {
    internal sealed class GraphicsControl: MonoBehaviour {
        #region Fields

        [SerializeField] private GameObject[] toDisappear;

        #endregion

        #region Properties
        #endregion

        public GraphicsControl() {
            toDisappear = null;
        }

        #region Unity User Callback Event Funcs

	    private void Start() {
            if(Options.GraphicsOption == GraphicsQualityOptions.GraphicsQualityOption.Low) {
                foreach(GameObject GO in toDisappear) {
                    GO.SetActive(false);
                }
            }
	    }

        #endregion
    }
}