using UnityEngine;

namespace IdolFever {
    internal sealed class MusicSrcVolControl: MonoBehaviour {
        #region Fields

        [SerializeField] private AudioSource audioSrc;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

	    private void FixedUpdate() {
	        audioSrc.volume = Options.MusicVol;
	    }

        #endregion

        public MusicSrcVolControl() {
            audioSrc = null;
        }
    }
}