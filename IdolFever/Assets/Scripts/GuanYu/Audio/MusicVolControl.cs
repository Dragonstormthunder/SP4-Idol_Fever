using UnityEngine;

namespace IdolFever {
    internal sealed class MusicVolControl: MonoBehaviour {
        #region Fields

        [SerializeField] private MusicCentralControl musicCentralControl;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Start() {
            foreach(AudioSource audioSrc in musicCentralControl.AudioSrcs) {
                audioSrc.volume = Options.MusicVol;
            }
        }

        #endregion

        public MusicVolControl() {
            musicCentralControl = null;
        }
    }
}