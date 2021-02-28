using System.Collections.Generic;
using UnityEngine;

namespace IdolFever {
    internal sealed class MusicCentralControl: MonoBehaviour {
        #region Fields

        private List<AudioSource> audioSrcs;
        [SerializeField] private bool dontDestroyOnLoad;

        #endregion

        #region Properties

        public List<AudioSource> AudioSrcs {
            get {
                return audioSrcs;
            }
        }

        #endregion

        public MusicCentralControl() {
            audioSrcs = new List<AudioSource>();
            dontDestroyOnLoad = false;
        }

        #region Unity User Callback Event Funcs

        private void Awake() {
            if(dontDestroyOnLoad) {
                DontDestroyOnLoad(gameObject);
            }

            foreach(Transform child in transform) {
                AudioSource[] audioSrcArr = child.GetComponents<AudioSource>();

                foreach(AudioSource audioSrc in audioSrcArr) {
                    audioSrcs.Add(audioSrc);
                }
            }
        }

        #endregion
    }
}