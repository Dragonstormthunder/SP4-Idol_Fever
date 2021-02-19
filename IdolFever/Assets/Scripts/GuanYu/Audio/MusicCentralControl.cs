﻿using System.Collections.Generic;
using UnityEngine;

namespace IdolFever {
    internal sealed class MusicCentralControl: MonoBehaviour {
        #region Fields

        private List<AudioSource> audioSrcs;

        #endregion

        #region Properties

        public List<AudioSource> AudioSrcs {
            get {
                return audioSrcs;
            }
            private set {
            }
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            foreach(Transform child in transform) {
                audioSrcs.Add(child.GetComponent<AudioSource>());
            }
        }

        #endregion

        public MusicCentralControl() {
            audioSrcs = new List<AudioSource>();
        }
    }
}