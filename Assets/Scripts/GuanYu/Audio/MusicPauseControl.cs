using UnityEngine;

namespace IdolFever {
    internal sealed class MusicPauseControl: MonoBehaviour {
        #region Fields

        [SerializeField] private MusicCentralControl musicCentralControl;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public MusicPauseControl() {
            musicCentralControl = null;
        }

        public void PauseAllMusic() {
            foreach(AudioSource audioSrc in musicCentralControl.AudioSrcs) {
                audioSrc.Pause();
            }
        }

        public void PlayAllMusic() {
            foreach(AudioSource audioSrc in musicCentralControl.AudioSrcs) {
                audioSrc.Play();
            }
        }
    }
}