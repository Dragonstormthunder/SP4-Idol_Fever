using UnityEngine;

namespace IdolFever {
    internal sealed class SoundVolControl: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
/*            AudioSource[] audioSrcs = FindObjectsOfType<AudioSource>();
            foreach(AudioSource audioSrc in audioSrcs) {
                Debug.Log("here", this);
                audioSrc.volume = Options.SoundVol;
            }*/
        }

        #endregion
    }
}