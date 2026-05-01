using UnityEngine;

namespace IdolFever {
    internal sealed class BGM: MonoBehaviour {
        #region Fields

        [SerializeField] private AudioSource[] audioSrcs;

        #endregion

        #region Properties
        #endregion

        internal BGM() {
            audioSrcs = null;
        }

        #region Unity User Callback Event Funcs

        private void Awake() {
            audioSrcs[Random.Range(0, audioSrcs.Length)].enabled = true;
        }

        #endregion
    }
}