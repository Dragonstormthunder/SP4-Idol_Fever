using UnityEngine;

namespace IdolFever {
    internal sealed class SoundVolControl: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            foreach(Transform child in transform) {
                child.GetComponent<AudioSource>().volume = Options.SoundVol;
            }
        }

        #endregion
    }
}