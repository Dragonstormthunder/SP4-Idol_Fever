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
            if(!isAndroid()) {
                fireworks.SetActive(true);
            }
        }

        #endregion

        private static bool isAndroid() {
            #if UNITY_ANDROID && !UNITY_EDITOR
	            return true;
            #else
                return false;
            #endif
        }
    }
}