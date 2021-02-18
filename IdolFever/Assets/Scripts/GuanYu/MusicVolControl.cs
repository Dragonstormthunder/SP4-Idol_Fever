using UnityEngine;

namespace IdolFever {
    internal sealed class MusicVolControl: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            AudioSource[] audioSrcs = FindObjectsOfType<AudioSource>();
            foreach(AudioSource audioSrc in audioSrcs) {
                audioSrc.volume = Options.MusicVol;
            }
        }

        #endregion
    }
}