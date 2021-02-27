using UnityEngine;

namespace IdolFever {
    internal sealed class FireworksControl: MonoBehaviour {
        #region Fields

        [SerializeField] private GameObject fireworks;

        #endregion

        #region Properties
        #endregion

        public FireworksControl() {
            fireworks = null;
        }

        #region Unity User Callback Event Funcs

	    private void Awake() {
            /*if(SystemInfo.supportsComputeShaders && SystemInfo.maxComputeBufferInputsVertex != 0) {
                fireworks.SetActive(true);
            }*/
        }

        #endregion
    }
}