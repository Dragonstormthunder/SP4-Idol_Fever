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
            AdjustVolOfMusic();
        }

        #endregion

        public MusicVolControl() {
            musicCentralControl = null;
        }

        private void AdjustVolOfMusic() {
            foreach(AudioSource audioSrc in musicCentralControl.AudioSrcs) {
                audioSrc.volume = Options.MusicVol;
            }
        }
    }
}