using System.Collections.Generic;
using UnityEngine;

namespace IdolFever {
    internal sealed class MusicPauseControl: MonoBehaviour {
        #region Fields

        private List<AudioSource> audioSrcs;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            foreach(Transform child in transform) {
                audioSrcs.Add(child.GetComponent<AudioSource>());
            }
        }

        #endregion

        public MusicPauseControl() {
            audioSrcs = new List<AudioSource>();
        }

        public void PauseAllMusic() {
            foreach(AudioSource audioSrc in audioSrcs) {
                audioSrc.Pause();
            }
        }

        public void PlayAllMusic() {
            foreach(AudioSource audioSrc in audioSrcs) {
                audioSrc.Play();
            }
        }
    }
}